/******************************************************************************

Copyright © 2006 EMC Corporation. All Rights Reserved
 
This file is part of .NET wrapper for the Centera SDK.

.NET wrapper is free software; you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation version 2.

In addition to the permissions granted in the GNU General Public License
version 2, EMC Corporation gives you unlimited permission to link the compiled
version of this file into combinations with other programs, and to distribute
those combinations without any restriction coming from the use of this file.
(The General Public License restrictions do apply in other respects; for
example, they cover modification of the file, and distribution when not linked
into a combined executable.)

.NET wrapper is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
PARTICULAR PURPOSE. See the GNU General Public License version 2 for more
details.

You should have received a copy of the GNU General Public License version 2
along with .NET wrapper; see the file COPYING. If not, write to:

 EMC Corporation 
 Centera Open Source Intiative (COSI) 
 80 South Street
 1/W-1
 Hopkinton, MA 01748 
 USA

******************************************************************************/

using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using EMC.Centera;
using EMC.Centera.FPTypes;
using EMC.Centera.SDK;
using System.Threading;

namespace EMC.Centera.SDK
{
	/** 
	 * FPStreamInfo contains the control information and data buffer that is used to transfer data
	 * between the application and the SDK.
	 * @author Graham Stuart
	 * @version
	 */
	[StructLayout(LayoutKind.Sequential)]
	unsafe public struct FPStreamInfo
	{

		public short	mVersion ;         // current version of FPStreamInfo
		public void* 	mUserData ;        // application-specific data, untouched by Generic Streams

		public long		mStreamPos ;       // current position
		public long		mMarkerPos ;       // position of marker
		public long		mStreamLen ;       // length of stream, if known, else -1

		public byte		mAtEOF ;           // have we reached the end of the stream?
		public byte		mReadFlag ;        // indicator for the direction of the transfer

		public void*	 mBuffer ;          // databuffer supplied by application
		public long		mTransferLen ;     // number of bytes actually transferred
	}
    
	internal sealed class SDK 
	{
		[DllImport("FPLibrary.dll")]
		public static extern		   FPStreamRef FPStream_CreateGenericStream (FPCallback prepareProc,
			FPCallback completeProc,
			FPCallback setMarkerProc,
			FPCallback resetMarkerProc,
			FPCallback closeProc,
			IntPtr userData);
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void         FPStream_Close (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		unsafe public static extern	    FPStreamInfo* FPStream_GetInfo (FPStreamRef pStream);
		
		[DllImport("FPLibrary.dll")]
		public static extern		   IntPtr		 FPStream_PrepareBuffer (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   	IntPtr	 FPStream_Complete (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void          FPStream_SetMark (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void          FPStream_ResetMark (FPStreamRef pStream) ;

	}
/* The FPPartialStream is a convenience classes to utilise a section of an
 * underlying stream using offset and size to determine the "section" boundaries.
 */
    public abstract class FPPartialStream : Stream
    {
        protected Stream theStream;
        protected long start, end, position, length;

        protected FPPartialStream(Stream s, long offset, long size)
        {
            start = offset;
            position = offset;
            length = size;
            end = offset + length;
            theStream = s;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CanSeek
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Length
        {
            get { return length; }
        }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                if ((start <= value) && (value <= end))
                {
                    position = value;
                }
                else
                    throw new Exception("Attempt to position to " + value + " - outside range of partial stream ("
                        + start + "/" + position + "/" + end + ")");
            }
        }

        public override void Flush()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newPosition = 0;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newPosition = start + offset;
                    break;
                case SeekOrigin.Current:
                    newPosition = Position + offset;
                    break;
                case SeekOrigin.End:
                    newPosition = end - offset;
                    break;
            }

            return Position = newPosition;
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    /* The FPPartialInputStream reads data from a section of an
     * underlying stream using offset and size to determine the "section" boundaries.
     */
    public class FPPartialInputStream : FPPartialStream
    {
        public FPPartialInputStream(Stream s, long o, long c) : base(s, o, c) { }

        public override bool CanRead
        {
            get { return true; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead;

            lock (theStream)
            {
                theStream.Seek(Position, SeekOrigin.Begin);

                if ((Position + count) > end)
                    count = (int)(end - Position);

                bytesRead = theStream.Read(buffer, offset, count);
                Position += bytesRead;
            }

            return bytesRead;
        }
    }

    /* The FPPartialOutputStream writes data to a section of an
     * underlying stream using offset and size to determine the "section" boundaries.
     * An additonal constructor places constraints on the maximum size of the resulting stream.
     */
    public class FPPartialOutputStream : FPPartialStream
    {
        public FPPartialOutputStream(Stream s, long o, long c) : base(s, o, c) { }

        public FPPartialOutputStream(Stream s, long o, long c, long max) : base(s, o, (o + c) > max ? max - o : 0)
        {
            if (o > max)
                throw new Exception("Offset > max file size for PartialOutputStream");
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            lock (theStream)
            {
                theStream.Seek(Position, SeekOrigin.Begin);

                if ((Position + count) > end)
                    count = (int)(end - Position);

                theStream.Write(buffer, offset, count);
                Position += count;
            }
        }
    }

	/** 
	 * A Generic Stream  object.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPGenericStream : FPStream
	{
		unsafe FPStreamInfo	*theInfo;
        protected Stream userStream;
        FPCallback prepare;
        FPCallback complete;
        FPCallback mark;
        FPCallback reset;
        FPCallback close;

       		/**
		 * Creates a Generic Stream object using the standard callback methods. See API Guide: FPStream_CreateGenericStream
		 * See API Guide: FPStream_CreateGenericStream
		 *
		 * @param userStream	Instance of an object derived from the Stream class. This object
		 *						is used by the calling application to transfer the data to the Generic
		 *						Stream. Stream direction is set to input to the Centera if the CanRead
		 *						property of the userStream is true. Note: The derived Stream class should
		 *						implement the Seek functionality is this is used to provide Marker Support
		 *						for the SDK. The direction of the Stream is determined by the CanRead property
         *                      of the supplied Stream.
		 * @param userData		An IntPtr which can be used to reference a user object that may be required
		 *						when working with the input or output data buffer.
		 */
		public FPGenericStream(Stream userStream, IntPtr userData)
            :this(userStream.CanRead ? StreamDirection.InputToCentera : StreamDirection.OutputFromCentera, 
                new FPStreamCallbacks(userStream),
                userData)
		{
		}

		/**
		 * Creates a Generic Stream object using the standard callbacks. A Stream derived class is still used for transfer
         * but the user supplies the direction required (supports bi-directional streams).
		 * See API Guide: FPStream_CreateGenericStream
		 *
		 * @param userStream	Instance of an object derived from the Stream class. This object
		 *						is used by the calling application to transfer the data to the Generic
		 *						Stream. Note: The derived Stream class should implement the Seek functionality
		 *                      is this is used to provide Marker Support for the SDK.
		 * @param direction     The direction of the stream.
		 * @param userData		An IntPtr which can be used to reference a user object that may be required
		 *						when working with the input or output data buffer.
		 */
		public FPGenericStream(Stream userStream, StreamDirection direction, IntPtr userData)
            : this(direction, new FPStreamCallbacks(userStream), userData)
		{
		}
		
		/**
		 * Creates a Generic Stream object with user supplied callbacks. A Stream derived class is still used for transfer.
		 * See API Guide: FPStream_CreateGenericStream
		 *
		 * @param userStream	Instance of an object derived from the Stream class. This object
		 *						is used by the calling application to transfer the data to the Generic
		 *						Stream. Stream direction is set to input to the Centera if the CanRead
		 *						property of the userStream is true. Note: The derived Stream class should
		 *						implement the Seek functionality is this is used to provide Marker Support
		 *						for the SDK.
		 * @param userCBS		Callbacks provided by the user. Typically the derived class will only
		 *						require to override PopulateBuffer and / or ProcessReturnedData.
		 * @param userData		An IntPtr which can be used to reference a user object that may be required
		 *						when working with the input or output data buffer.
		 */
		public FPGenericStream(Stream userStream, FPStreamCallbacks userCBS, IntPtr userData)
            :this(userStream.CanRead ? StreamDirection.InputToCentera : StreamDirection.OutputFromCentera,
                userCBS,
                userData)
		{
		}

        /**
         * Creates a Generic Stream object with user supplied callbacks. A Stream derived class is still used for transfer.
         * but the user supplies the direction required (supports bi-directional streams).
         * See API Guide: FPStream_CreateGenericStream
         *
         * @param userStream	Instance of an object derived from the Stream class. This object
         *						is used by the calling application to transfer the data to the Generic
         *						Stream. Note: The derived Stream class should implement the Seek functionality is
         *                      this is used to provide Marker Support for the SDK.
         * @param direction     The direction of the stream.
         * @param userCBS		Callbacks provided by the user. Typically the derived class will only
         *						require to override PopulateBuffer and / or ProcessReturnedData.
         * @param userData		An IntPtr which can be used to reference a user object that may be required
         *						when working with the input or output data buffer.
         */
        public FPGenericStream(Stream userStream, StreamDirection direction, FPStreamCallbacks userCBS, IntPtr userData)
            :this(direction, userCBS, userData)
		{
        }

		/**
		 * Creates a Generic Stream object with user supplied callbacks. This is the most complex way of using
		 * a GenericStream and will require the user to implement more of the functionality normally handled by
		 * the standard callbacks. In particular Marker Support will require the user to make their transfer
		 * method "rewindable" for up to 100MB of previously transferred data. See API Guide: FPStream_CreateGenericStream
		 * See API Guide: FPStream_CreateGenericStream
		 *
		 * @param direction		Indicator for the type of GenericStream that is to be created i.e. input to
		 *						Centera or output from Centera.
		 * @param userCBS		Callbacks provided by the user. Typically the derived class will only
		 *						require to override PopulateBuffer and / or ProcessReturnedData.
		 * @param userData		An IntPtr which can be used to reference a user object that may be required
		 *						when working with the input or output data buffer.
		 */
		public FPGenericStream(StreamDirection direction, FPStreamCallbacks userCBS, IntPtr userData)
		{
			prepare = new FPCallback(userCBS.PrepareBuffer);
			complete = new FPCallback(userCBS.BlockTransferred);
			mark = new FPCallback(userCBS.SetMark);
			reset = new FPCallback(userCBS.ResetMark);
			close = new FPCallback(userCBS.TransferComplete);

			if (direction == StreamDirection.InputToCentera)
				theStream = SDK.FPStream_CreateGenericStream(prepare, complete, mark, reset, close, userData);
			else
				theStream = SDK.FPStream_CreateGenericStream(null, complete, mark, reset, close, userData);
			AddObject(theStream, this);

            userCBS.StreamRef = theStream;
            userStream = userCBS.userStream;

            unsafe
            {
                theInfo = SDK.FPStream_GetInfo(theStream);
                theInfo->mAtEOF = 0;
                theInfo->mStreamLen = -1;
                theInfo->mTransferLen = 0;
                theInfo->mStreamPos = 0;
                theInfo->mMarkerPos = 0;
                theInfo->mBuffer = null;

                theInfo->mReadFlag = (byte)direction;
            }
		}
		
		/**
		 * Close the underlying FPStream object.
		 */
		public override void Close()
		{
			if (theStream != 0)
			{
				RemoveObject(theStream);
				FPApi.Stream.Close(theStream);
				theStream = 0;
			}
		}

		/**
		 * The length of the stream being transferred. Defaults to -1 (unknown length).
		 */
		unsafe public long StreamLen
		{
			get
			{
				return theInfo->mStreamLen;
			}
			set
			{
				theInfo->mStreamLen = value;
			}
		}

		/**
		 * The stream direction: 
		 */

		unsafe public StreamDirection Direction
		{
			get
			{
				return (StreamDirection) theInfo->mReadFlag;
			}
		}

		/**
		 * Prints out values of the FPStreamInfo control structure.
		 */
		unsafe public override string ToString()
		{
			return "End of file: " + theInfo->mAtEOF +
				"\nMarker " + theInfo->mMarkerPos +
				"\nRead flag " + theInfo->mReadFlag +
				"\nStreamLen " + theInfo->mStreamLen +
				"\nPos "+ theInfo->mStreamPos +
				"\nTransferLen " + theInfo->mTransferLen +
				"\nVersion " + theInfo->mVersion;
		}

	}

    /**
     * This is a helper class that allows for the use of Windows files with Wide Character filenames without the
     * need for additional special marshalling or Centera SDK support. It is derived from a GenericStream but the relevant
     * FileStream object is created for you.
     */

    public class FPWideFilenameStream : FPGenericStream
    {
        private FPWideFilenameStream(String filename, StreamDirection direction, FileMode mode)
            : base(File.Open(filename, mode), direction, new IntPtr())
        {
            if (direction == StreamDirection.InputToCentera)
                this.StreamLen = userStream.Length;
        }

        /* Open a file for Reading to transfer data to the Centera */
        public FPWideFilenameStream(String filename)
            : this(filename, StreamDirection.InputToCentera, FileMode.Open) {}

        /* Open a file using the supplied mode for transferring data from the Centera */
        public FPWideFilenameStream(String filename, FileMode mode)
            : this(filename, StreamDirection.OutputFromCentera, mode) { }

        /* Open a partial file segment (bounded region) for transferring data to the Centera */
        public FPWideFilenameStream(String filename, long offset, long length)
            : base(new FPPartialInputStream(File.OpenRead(filename), offset, length), StreamDirection.InputToCentera, new IntPtr()) {}

        /* Open a partial file segment (bounded region) using the supplied mode for transferring data from the Centera */
        public FPWideFilenameStream(String filename, FileMode mode, long offset, long length, long maxFileSize)
            : base(new FPPartialOutputStream(File.Open(filename, mode), offset, length, maxFileSize), StreamDirection.OutputFromCentera, new IntPtr()) {}

        public override void Close()
		{
            userStream.Close();
            base.Close();
		}
    }

    /**
     * This class encapsulates the Callback Methods that are used to manipulate a GenericStream, and
     * data members that are required while doing so. You should derive from this class is you wish to
     * populate the data buffer or process reurned data in a way that does not make use of a Stream
     * derived object or you wish do to things differently than the default behaviour.
     */

	public class FPStreamCallbacks
	{
		protected int bufferSize;
		protected byte[] localBuffer;
        protected internal Stream userStream;
		
		protected FPStreamRef theStream;
	
		/**
		 * Creates an FPStreamCallbacks object.
		 * 
		 * @param s			The Stream derived object to use for transferring the data between the application
		 *					and the SDK. This Stream should support Seek in order to allow Marker Support.
		 */
		public FPStreamCallbacks(Stream s)
		{
			BufferSize = 16 * 1024;
			userStream = s;
		}

		/**
		 *  Zero parameter base class construtor for derivation purposes.
		 */
		protected FPStreamCallbacks()
		{
            userStream = null;
		}

		/**
		 * Change the buffer size for the transfer of the data between the application and the SDK. As
		 * the SDK always uses 16k, this is the optimum value to use and is the initial default value
		 * is set when the FPStreamCallbacks object is created.. 
		 */
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

		/**
		 * The FPStreamRef object representing the GenericStream.
		 */
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
		
		/**
		 * This function is only required when writing to the Centera.
		 * Allocate a buffer and populate it with the next chunk of data to be sent.
		 * 
		 * @param info		The FPStreamInfo structure containing the data and control information.
		 */
		unsafe public virtual long PrepareBuffer(ref FPStreamInfo info)
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
	

		/**
		 * Populates the buffer that transfers data from the application to the SDK.
		 * 
		 * @param localBuffer	The area of local storage allocated as the transfer buffer.
		 * @param bufferSize	The size of this buffer.
		 * @param userData		User object for whatever purpose they deem fit.
		 */
		public virtual int PopulateBuffer(ref byte[] localBuffer, int bufferSize, object userData)
		{		
			// The localBuffer is populated from the Stream that the user provides.
			Stream userStream = (Stream) userData;
			
			if (userStream == null)
				return 0;

			return userStream.Read(localBuffer, 0, bufferSize);
		}

		/**
		 * This function is called both for reading and writing to the Centera and it signifies
		 * that the block has been to the SDK (when writing) or received from the SDK (when reading).
		 * 
		 * @param info		The FPStreamInfo structure containing the data and control information.
		 */ 
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

		/**
		 * Extract the data from the buffer returned from the Centera and send it on to the
		 * application.
		 * 
		 * @param localBuffer	The area of local storage allocated as the transfer buffer.
		 * @param bufferSize	The size of this buffer.
		 * @param userData		User object for whatever purpose they deem fit.
		 */
		public virtual void ProcessReturnedData(byte[] localBuffer, int bufferSize, object userData)
		{
            //Console.WriteLine(this.GetHashCode() + " Processing returned data");
			Stream userStream = (Stream) userData;

			if (userStream != null && userStream.CanWrite)
				userStream.Write(localBuffer, 0, bufferSize);
		}

		/**
		 * Allows the SDK to set a marker at the current stream position. This effectively
		 * means that data up to this point has been successfully transferred to the Centera.
		 * 
		 * @param info		The FPStreamInfo structure containing the data and control information.
		 */
		public virtual long SetMark(ref FPStreamInfo info)
		{
            //Console.WriteLine(this.GetHashCode() + " Mark set at " + info.mStreamPos);
			info.mMarkerPos = info.mStreamPos;

			return 0;
		}

		/**
		 * Allows the SDK to request that the application resends data from the previously set
		 * marker position due to problems in sending that data to the Centera. If using
		 * a "normal" GenericStream that uses a Stream based class for transfer, this will be
		 * achieved using the Seek member, and the derived class must implement this.
		 * 
		 * Note: up to 100MB of data could be required to be resent!
		 * 
		 * @param info		The FPStreamInfo structure containing the data and control information.
		 */
		public virtual long ResetMark(ref FPStreamInfo info)
		{
			// The stream the user supplies must override Seek in order for this to work!!
            ////Console.WriteLine(this.GetHashCode() + " Mark reset to " + info.mMarkerPos);
            
            if (userStream == null)
				return -1;

            userStream.Seek(info.mMarkerPos, System.IO.SeekOrigin.Begin);
			info.mStreamPos = info.mMarkerPos;
			return 0;
		}

		/**
		 * The SDK has signalled that the operation is complete. Allocated buffers should now be freed
		 * up.
		 * 
		 * @param info		The FPStreamInfo structure containing the data and control information.
		 */
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


