using System;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPStream : FPObject
    {
        /**
         * StreamType indicator used when creating "special" FPStream objects.
         * 
         * Members: Null / Stdio.
         */
        public enum StreamType { Null, Stdio };

        /**
         * The StreamDirection indicator used when creating buffer based FPStream objects.
         * 
         * Members: InputToCentera / OutputFromCentera.
         */
        public enum StreamDirection { OutputFromCentera = 0, InputToCentera = 1 };

        protected FPStreamRef theStream;

        /**
         * Create a Stream using a buffer to write to (StreamType.InputToCentera)
         * or read from (StreamType.OutputFromCentera) the Centera.
         * See API Guide: FPStream_CreateBufferForInput, FPStream_CreateBufferForOutput
         * 
         * @param	streamBuffer	The buffer that the Stream will read from (for INPUT to the Centera)
         *							or write to (for OUTPUT from the Centera).
         * @param	bufferSize	The size of the buffer.
         * @param	streamDirection	The StreamDirection enum indicating input or output.
         */
        public FPStream(IntPtr streamBuffer, int bufferSize, StreamDirection streamDirection)
        {
            if (streamDirection == StreamDirection.InputToCentera)
                theStream = Native.Stream.CreateBufferForInput(streamBuffer, (long)bufferSize);
            else
            {
                theStream = Native.Stream.CreateBufferForOutput(streamBuffer, (long)bufferSize);
            }
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads from the Centera and writes the content to a file.
         * See API Guide: FPStream_CreateFileForOutput
         *
         * @param	fileName	The name of the file to write to.
         * @param	permissions	The permissions to create the file with.
         */
        public FPStream(String fileName, string permissions)
        {
            theStream = Native.Stream.CreateFileForOutput(fileName, permissions);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads a file and writes the content to Centera.
         * See API Guide: FPStream_CreateFileForInput
         *
         * @param	fileName	The name of the file to read from.
         * @param	bufferSize	The size of the buffer to use for writing.
         */
        public FPStream(String fileName, long bufferSize)
        {
            theStream = Native.Stream.CreateFileForInput(fileName, "rb", bufferSize);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads part of a file and writes the content to Centera.
         * See API Guide: FPStream_CreatePartialFileForInput
         *
         * @param	fileName	The name of the file to read from.
         * @param	bufferSize	The size of the buffer to use for writing.
         * @param	offset  	The position in the file to start reading from.
         * @param	length  	The length of the file segment to read from.
         */
        public FPStream(String fileName, long bufferSize, long offset, long length)
        {
            theStream = Native.Stream.CreatePartialFileForInput(fileName, "rb", bufferSize, offset, length);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads from Centera and writes the content at an offset in a file.
         * See API Guide: FPStream_CreatePartialFileForOutput
         *
         * @param	fileName	The name of the file to read from.
         * @param   permission  The write mode that the file is opened in.
         * @param	bufferSize	The size of the buffer to use for writing.
         * @param	offset  	The position in the file to start writing to.
         * @param	length  	The length of the file segment to write to.
         * @param	maxFileSize	The maximum size that the output file max grow to.
         */
        public FPStream(String fileName, string permission, long bufferSize, long offset, long length, long maxFileSize)
        {
            theStream = Native.Stream.CreatePartialFileForOutput(fileName, permission, bufferSize, offset, length, maxFileSize);
            AddObject(theStream, this);
        }
        /**
         * Create a Stream that reads from the Centera and writes the content to stdio or
         * a null stream.
         * See API Guide: FPStream_CreateToStdio / FPStream_CreateToNull
         *
         * @param	streamType	StreamType enum - Stdio or Null.
         */
        public FPStream(StreamType streamType)
        {
            if (streamType == StreamType.Stdio)
                theStream = Native.Stream.FPStream_CreateToStdio();
            else
                theStream = Native.Stream.FPStream_CreateToNull();

            AddObject(theStream, this);
        }

        /**
         * Creates a Stream for temporary storage. If the length of the stream is greater than
         * pMemBuffSize the overflow is flushed to a temporary file.
         * See API Guide: FPStream_CreateTemporaryFile
         *
         * @param	pMemBuffSize	The size of the in-memory buffer to use.
         */
        public FPStream(long pMemBuffSize)
        {
            theStream = Native.Stream.FPStream_CreateTemporaryFile(pMemBuffSize);
            AddObject(theStream, this);
        }

        /**
         * Implicit conversion between a Stream and an FPStreamRef
         *
         * @param	s	The Stream.
         * @return	The FPStreamRef associated with this Stream.
         */
        static public implicit operator FPStreamRef(FPStream s)
        {
            return s.theStream;
        }

        /**
         * Implicit conversion between an FPStreamRef and a  Stream
         *
         * @param	streamRef	The FPStreamRef.
         * @return	The new Stream.
         */
        static public implicit operator FPStream(FPStreamRef streamRef)
        {
            // Find the relevant Tag object in the hastable for this FPTagRef
            FPStream streamObject = null;

            if (SDKObjects.Contains(streamRef))
            {
                streamObject = (FPStream)SDKObjects[streamRef];
            }
            else
            {
                throw new FPLibraryException("FPStreamRef is not asscociated with an FPStream object", FPMisc.WRONG_REFERENCE_ERR);
            }

            return streamObject;
        }

        /**
         * Explicitly close this Stream. See API Guide: FPStream_Close
         */
        public override void Close()
        {
            if (theStream != 0)
            {
                RemoveObject(theStream);
                Native.Stream.Close(theStream);
                theStream = 0;
            }
        }

        protected FPStream()
        {
        }

    }
}