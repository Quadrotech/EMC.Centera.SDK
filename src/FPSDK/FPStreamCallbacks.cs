using System;
using System.IO;
using System.Runtime.InteropServices;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    /// <summary>
    ///This class encapsulates the Callback Methods that are used to manipulate a GenericStream, and
    ///data members that are required while doing so. You should derive from this class is you wish to
    ///populate the data buffer or process reurned data in a way that does not make use of a Stream
    ///derived object or you wish do to things differently than the default behaviour.
    /// </summary>
    public class FPStreamCallbacks
    {
        protected int bufferSize;
        protected byte[] localBuffer;
        protected internal Stream userStream;
		
        protected FPStreamRef theStream;
	
        /// <summary>
		///Creates an FPStreamCallbacks object.
		///
		///@param s			The Stream derived object to use for transferring the data between the application
		///					and the SDK. This Stream should support Seek in order to allow Marker Support.
		/// </summary>
        public FPStreamCallbacks(Stream s)
        {
            BufferSize = 16 * 1024;
            userStream = s;
        }

        /// <summary>
		/// Zero parameter base class construtor for derivation purposes.
		 /// </summary>
        protected FPStreamCallbacks()
        {
            userStream = null;
        }

        /// <summary>
		///Change the buffer size for the transfer of the data between the application and the SDK. As
		///the SDK always uses 16k, this is the optimum value to use and is the initial default value
		///is set when the FPStreamCallbacks object is created.. 
		 /// </summary>
        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
            set
            {
                bufferSize = value;
                localBuffer = new byte[bufferSize];
            }
        }

        /// <summary>
		///The FPStreamRef object representing the GenericStream.
		 /// </summary>
        public FPStreamRef StreamRef
        {
            get
            {
                return theStream;
            }

            set
            {
                theStream = value;
            }
        }
		
        /// <summary>
		///This function is only required when writing to the Centera.
		///Allocate a buffer and populate it with the next chunk of data to be sent.
		///
		///@param info		The FPStreamInfo structure containing the data and control information.
		 /// </summary>
        public virtual unsafe long PrepareBuffer(ref FPStreamInfo info)
        {
            //Console.WriteLine(this.GetHashCode() + " prepare buffer " + info.ToString());
            // We allocate the buffer if this is the first time through
            if (info.mBuffer == null)
            {
                // Create a heap memory buffer that the stream can use
                info.mBuffer = Marshal.AllocHGlobal(bufferSize).ToPointer();
            }

            int dataSize = PopulateBuffer(ref localBuffer, bufferSize, userStream);

            // Copy the localBuffer into the Heap Allocated mBuffer for transfer.
            Marshal.Copy(localBuffer, 0, (IntPtr) info.mBuffer, dataSize);

            // Update the stream position and the amount transferred
            info.mStreamPos += dataSize;
            info.mTransferLen = dataSize;

            if (info.mStreamLen == -1)
            {
                if (dataSize < bufferSize)
                {
                    //Console.WriteLine("\tStream processed " + dataSize + "/" + bufferSize);
                    info.mAtEOF = 1;
                }
            }
            else if (info.mStreamLen <= info.mStreamPos)
            {
                //Console.WriteLine("\tStream processed " + info.mStreamLen + "/" + info.mStreamPos);
                info.mAtEOF = 1;
            }
            else
            {
                //Console.WriteLine("\tStream not exhausted " + info.mStreamLen + "/" + info.mStreamPos);
            }

            return 0;
        }
	

        /// <summary>
		///Populates the buffer that transfers data from the application to the SDK.
		///
		///@param localBuffer	The area of local storage allocated as the transfer buffer.
		///@param bufferSize	The size of this buffer.
		///@param userData		User object for whatever purpose they deem fit.
		 /// </summary>
        public virtual int PopulateBuffer(ref byte[] localBuffer, int bufferSize, object userData)
        {		
            // The localBuffer is populated from the Stream that the user provides.
            Stream userStream = (Stream) userData;
			
            if (userStream == null)
                return 0;

            return userStream.Read(localBuffer, 0, bufferSize);
        }

        /// <summary>
		///This function is called both for reading and writing to the Centera and it signifies
		///that the block has been to the SDK (when writing) or received from the SDK (when reading).
		///
		///@param info		The FPStreamInfo structure containing the data and control information.
		 /// </summary> 
        public virtual long BlockTransferred(ref FPStreamInfo info)
        {
            //Console.WriteLine(this.GetHashCode() + " Block transferred OK");
            if (info.mReadFlag == (byte) FPStream.StreamDirection.OutputFromCentera)
            {
                if (info.mTransferLen > 0)
                {
                    if (info.mTransferLen > bufferSize)
                    {
                        localBuffer = new byte[info.mTransferLen];
                        bufferSize = (int) info.mTransferLen;
                    }

                    unsafe
                    {
                        Marshal.Copy((IntPtr)info.mBuffer, localBuffer, 0, (int)info.mTransferLen);
                    }
                }

                ProcessReturnedData(localBuffer, (int)info.mTransferLen, userStream);
                info.mStreamPos += info.mTransferLen;
            }

            return 0;
        }

        /// <summary>
		///Extract the data from the buffer returned from the Centera and send it on to the
		///application.
		///
		///@param localBuffer	The area of local storage allocated as the transfer buffer.
		///@param bufferSize	The size of this buffer.
		///@param userData		User object for whatever purpose they deem fit.
		 /// </summary>
        public virtual void ProcessReturnedData(byte[] localBuffer, int bufferSize, object userData)
        {
            //Console.WriteLine(this.GetHashCode() + " Processing returned data");
            Stream userStream = (Stream) userData;

            if (userStream != null && userStream.CanWrite)
                userStream.Write(localBuffer, 0, bufferSize);
        }

        /// <summary>
		///Allows the SDK to set a marker at the current stream position. This effectively
		///means that data up to this point has been successfully transferred to the Centera.
		///
		///@param info		The FPStreamInfo structure containing the data and control information.
		 /// </summary>
        public virtual long SetMark(ref FPStreamInfo info)
        {
            //Console.WriteLine(this.GetHashCode() + " Mark set at " + info.mStreamPos);
            info.mMarkerPos = info.mStreamPos;

            return 0;
        }

        /// <summary>
		///Allows the SDK to request that the application resends data from the previously set
		///marker position due to problems in sending that data to the Centera. If using
		///a "normal" GenericStream that uses a Stream based class for transfer, this will be
		///achieved using the Seek member, and the derived class must implement this.
		///
		///Note: up to 100MB of data could be required to be resent!
		///
		///@param info		The FPStreamInfo structure containing the data and control information.
		 /// </summary>
        public virtual long ResetMark(ref FPStreamInfo info)
        {
            // The stream the user supplies must override Seek in order for this to work!!
            ////Console.WriteLine(this.GetHashCode() + " Mark reset to " + info.mMarkerPos);
            
            if (userStream == null)
                return -1;

            userStream.Seek(info.mMarkerPos, SeekOrigin.Begin);
            info.mStreamPos = info.mMarkerPos;
            return 0;
        }

        /// <summary>
		///The SDK has signalled that the operation is complete. Allocated buffers should now be freed
		///up.
		///
		///@param info		The FPStreamInfo structure containing the data and control information.
		 /// </summary>
        public virtual long TransferComplete(ref FPStreamInfo info)
        {
            // If we are writing to Centera, free the unmanaged buffer that we allocated
            //Console.WriteLine(this.GetHashCode() + " Transfer complete");
            if (info.mReadFlag == (byte) FPStream.StreamDirection.InputToCentera)
            {
                unsafe
                {
                    Marshal.FreeHGlobal((IntPtr) info.mBuffer);
                    info.mBuffer = null;
                }
            }
            return 0;
        }
    }
}