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
using EMC.Centera;
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using EMC.Centera.BlobStreams;

/* The BlobStreams package is designed to allow the user to write and read content to the Centera using
 * standard streams rather than the Centera specific streams. It uses the underlying Generic Streams framework
 * and creates a temporary buffer stream; this buffer stores the content that the user writes or the Generic Stream
 * returns in order that the user can write and read when the application is ready (rather than when the
 * cluster is ready). Keepalives are used for writing in order that the cluster does not time out after 60 secs
 * and close the stream.
 * 
 * The temporary buffer stream is a MemoryStream by default, but the user can specify any stream required using the
 * alternate constructors.
 */

namespace EMC.Centera.BlobStreams
{
    // Underlying base class for all the Blob Streams

    public abstract class BlobStream : Stream
    {
        abstract class Callbacks { } // These will "plug in" to the Generic Streams framework to provide the I/O.

        static internal Hashtable openStreams = new Hashtable();

        protected Stream holdingArea; // This is the temporaty buffer stream
        protected Thread blobThread;
        protected FPGenericStream fpStream; // The underlying Generic Stream object used to transfer to /from Centera
        protected FPTag theTag; // The Tag we are writing to or reading from

        protected BlobStream(Stream s, FPTag t)
        {
            holdingArea = s;
            theTag = t;
        }

        protected BlobStream() { }

        ~BlobStream()
        {
            fpStream.Close();
        }

        public override bool CanSeek
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
            //fpStream.StreamLen = value;
        }

        public override long Length
        {
            get
            {
                return holdingArea.Length;
            }
        }

        public override long Position
        {
            get
            {
                return holdingArea.Position;
            }
            set
            {
                holdingArea.Seek(value, SeekOrigin.Begin);
            }
        }

        public override void Flush()
        {
            holdingArea.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return holdingArea.Seek(offset, origin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CanTimeout
        {
            get
            {
                return holdingArea.CanTimeout;
            }
        }
    }

    public class BlobWriteStream : BlobStream
    {
        bool firstTime = true;
        protected BlobWriteStream() { }

        public class Callbacks : FPStreamCallbacks
        {
            public Callbacks(Stream s, int bufferSize)
                : base(s)
            {
                BufferSize = bufferSize;
            }

            public Callbacks(Stream s) : this(s, 16 * 1024) { }

            /* For Writing, the only non-default callback we need to implement is PrepareBuffer.
             * In this case, we will check the stream the user has supllied for more data that
             * must be written to the cluster. We'll also send zero length packets as keep alives.
             */

            unsafe public override long PrepareBuffer(ref FPStreamInfo info)
            {
                long userStreamLen = 0, userStreamPos = 0;
                bool streamClosed = false;

                int dataSize = 0;
                byte[] localBuffer = new byte[bufferSize];

                if (userStream == null || !userStream.CanRead)
                    return 0;

                // We allocate the buffer if this is the first time through            
                if (info.mBuffer == null)
                {
                    // Create a heap memory buffer that the stream can use
                    info.mBuffer = Marshal.AllocHGlobal(bufferSize).ToPointer();
                }

                // Check for data every 10ms, send keepalives every 50 seconds
                for (int keepAlive = 0; keepAlive < 50 * 100; keepAlive++)
                {
                    //FPLogger.ConsoleMessage("\nSeek to position " + info.mStreamPos + " ");
                    lock (userStream)
                    {
                        userStream.Seek(info.mStreamPos, SeekOrigin.Begin);
                        dataSize = userStream.Read(localBuffer, 0, bufferSize);

                        //if (dataSize > 0) FPLogger.ConsoleMessage("\nCallback " + this.GetHashCode() + " Block of " + dataSize);

                        userStreamLen = userStream.Length;
                        userStreamPos = userStream.Position;

                        // Check if user has signalled completion by removing this object from openStreams via Close
                        streamClosed = !openStreams.Contains(((IntPtr)info.mUserData).GetHashCode());
                    }

                    // If no data ready and stream not closed
                    if (dataSize == 0 && !streamClosed)
                    {
                        Thread.Sleep(10);
                    }
                    else // We have real data ready to send
                        break;
                }

                //if (dataSize != 0) FPLogger.ConsoleMessage("\n read and sent to SDK\tTotal ack'ed by SDK so far " + info.mStreamPos);
                //else FPLogger.ConsoleMessage("\n*** Keepalive sent *** Stream closed " + streamClosed + " Position " + userStreamPos + " Length " + userStreamLen);

                // Copy the localBuffer into the Heap Allocated mBuffer for transfer.
                Marshal.Copy(localBuffer, 0, (IntPtr)info.mBuffer, dataSize);

                // Update the stream position and the amount transferred
                info.mStreamPos += dataSize;
                info.mTransferLen = dataSize;

                if (userStreamLen == userStreamPos)
                {
                    if (info.mStreamLen != -1 || streamClosed)
                    {
                        FPLogger.ConsoleMessage("\n\t *** Thread finished - streamLen: " + info.mStreamLen + " Closed by user: " + streamClosed);
                                                
                        // Let the Generic Stream know we are finished
                        info.mAtEOF = 1;
                    }
                    else
                    {
                        //FPLogger.ConsoleMessage("\n" + this.GetHashCode() + " ******* Stream exhausted - closed: " + streamClosed + " StreamInfo.mStreamLen " + info.mStreamLen + " ***********");
                    }
                }
                else
                {
                    //FPLogger.ConsoleMessage("\n\tNot exhausted " + userStreamPos + "/" + userStreamLen);
                }

                return 0;
            }
        }

        protected Callbacks myCallbacks;

        // Standard ctor - Stream to be used to buffer, the Tag we write the data to
        public BlobWriteStream(Stream s, FPTag t)
            : base(s, t)
        {
            myCallbacks = new Callbacks(holdingArea);
            fpStream = new FPGenericStream(holdingArea, FPStream.StreamDirection.InputToCentera, myCallbacks, (IntPtr)this.GetHashCode());

            blobThread = new Thread(delegate() { theTag.BlobWrite(fpStream); });
            blobThread.Name = "BlobWriteThread";

            FPLogger.ConsoleMessage("\nStarted stream " + this.GetHashCode() + " blob write thread " + blobThread.GetHashCode());
            
            // We keep track of the current "open" streams being used in a hashtable
            // When user closes the stream we remove this to signal the callback that we are done writing
            openStreams.Add(this.GetHashCode(), blobThread.GetHashCode());
        }

        // Default ctor that uses a MemoryStream for the temporary buffer
        public BlobWriteStream(FPTag t) : this(new MemoryStream(), t) { }

        // Write user data into buffer and start the write thread to Centera on the first write
        public override void Write(byte[] buffer, int offset, int count)
        {
            lock (holdingArea)
            {
                holdingArea.Seek(0, SeekOrigin.End);
                holdingArea.Write(buffer, offset, count);
                //FPLogger.ConsoleMessage("\nBuffering " + count + " bytes. Stream now has " + holdingArea.Length + " bytes");
            }

            if (firstTime)
            {
                firstTime = false;
                blobThread.Start();
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /* Remove this object from the list of open streams, wait for the content in the buffer
         * to finish being sent to the Cluster and then clean up.
         */
        public override void  Close()
        {
            FPLogger.ConsoleMessage("\nAll content buffered for thread " + this.GetHashCode() + " - flushing to cluster ....");
            openStreams.Remove(this.GetHashCode());

            if (blobThread.IsAlive)
                blobThread.Join();

            holdingArea.Close();
            fpStream.Close();
            FPLogger.ConsoleMessage(" content flushed, thread " + this.GetHashCode() + " closed");
        }

    }

    public class BlobReadStream : BlobStream
    {
        protected Callbacks myCallbacks;
        protected long streamPos = 0;

        public class Callbacks : FPStreamCallbacks
        {
            public Callbacks(Stream s, int bufferSize)
                : base(s)
            {
                BufferSize = bufferSize;
            }

            public Callbacks(Stream s) : this(s, 16 * 1024) { }

            /* For a ReadStream we only need to override the PrepareBuffer callback.
             * Take the data returned from the cluster and write it into the buffer stream.
             */
            public override void ProcessReturnedData(byte[] localBuffer, int bufferSize, object userData)
            {
                userStream = (Stream)userData;
                //FPLogger.ConsoleMessage("\n\tProcessing returned data callback " + this.GetHashCode() + " - bufferSize " + bufferSize + " position " + userStream.Position);

                if (userStream != null && userStream.CanWrite)
                {
                    lock (userStream)
                    {
                        userStream.Seek(0, SeekOrigin.End);
                        userStream.Write(localBuffer, 0, bufferSize);
                    }
                }
            }

        }

        // Standard ctor - Stream to be used to buffer, the Tag we read the data from
        public BlobReadStream(Stream s, FPTag t)
            : base(s, t)
        {
            myCallbacks = new Callbacks(holdingArea);

            fpStream = new FPGenericStream(holdingArea, FPStream.StreamDirection.OutputFromCentera, myCallbacks, IntPtr.Zero);
            fpStream.StreamLen = t.BlobSize;

            FPLogger.ConsoleMessage("\nStarting blob read thread");
            blobThread = new Thread(delegate() { t.BlobRead(fpStream); });
            blobThread.Name = "BlobReadThread";
            blobThread.Start();
        }

        // Default ctor that uses a MemoryStream for the temporary buffer
        public BlobReadStream(FPTag t) : this(new MemoryStream(), t) { }

        protected BlobReadStream() { }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /* Read data from the buffer stream that has been populated by the underlying
         * Generic Stream.
         */
        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;

            lock (holdingArea)
            {
                holdingArea.Seek(streamPos, SeekOrigin.Begin);
                bytesRead = holdingArea.Read(buffer, offset, count);

                streamPos = holdingArea.Position;
            }
            return bytesRead;
        }

        public override void Close()
        {
            // If user attempts to close thse stream, make sure we finish the requested read and tidy up
            if (blobThread.IsAlive)
                blobThread.Join();

            fpStream.Close();
        }

        /* Convenience method - rather than have the user poll the buffer area to determine if the read
         * has finished, just allow them to Join the underlying read thread and wait for completion.
         */
        public void Join()
        {
            blobThread.Join();
        }

        public override long Length
        {
            get
            {
                return holdingArea.Length;
            }
        }

    }

    /* Identical to BlobReadStream in operation - difference is we only read part of the underlying blob
     * using the BlobReadPartial method rather than the BlobRead method.
     */
    public class PartialBlobReadStream : BlobReadStream
    {
        // Standard ctor - as for BlobReadStream but also specify the offset and number of bytes for the partial read.
        public PartialBlobReadStream(Stream s, FPTag t, long offset, long numBytes)
        {
            // We cannot call the base constructor because we need use the additional offset and numBytes parameters
            // Even if we split out some of the functionality using a virtual init method with no
            // parameters, these paraameters will not be initialized when the virtual function was called
            // by the base constructor. So we just have to duplicate the (small)initialization code.
            holdingArea = s;
            theTag = t;
            myCallbacks = new Callbacks(holdingArea, 16 * 1024);

            fpStream = new FPGenericStream(holdingArea, FPStream.StreamDirection.OutputFromCentera, myCallbacks, IntPtr.Zero);
            fpStream.StreamLen = numBytes;

            blobThread = new Thread(delegate() { theTag.BlobReadPartial(fpStream, offset, numBytes); });
            blobThread.Name = "PartialBlobReadThread";
            blobThread.Start();
        }

        // Default ctor using a MemoryStream for the buffer
        public PartialBlobReadStream(FPTag t, long offset, long numBytes) : this(new MemoryStream(), t, offset, numBytes) { }

        ~PartialBlobReadStream()
        {
            fpStream.Close();
        }

    }

    /* Identical to BlobWriteStream in operation - difference is we only write part of the underlying blob
     * onto the Tag using the BlobWritePartial method rather than the BlobRead method.
     * 
     * This allows us to easily ingest content via multiple threads, and have the cluster vitualize it into a single
     * contiguos piece of content. We need to specify a sequence number in order that the cluster knows what order
     * to "arrange" the segments in.
     */
    public class PartialBlobWriteStream : BlobWriteStream
    {
        int sequenceId = 0;

        // Standard ctor - as for BlobWriteStream but also specify the sequence number of the segment.
        public PartialBlobWriteStream(Stream s, FPTag t, int id)
        {
            // We cannot call the base constructor because we need use the additional id parameter
            // Even if we split out some of the functionality using a virtual init method with no
            // parameters, the sequenceId will not be initialized when the virtual function was called
            // by the base constructor. So we just have to duplicate the (small)initialization code.
            holdingArea = s;
            theTag = t;
            sequenceId = id;

            myCallbacks = new Callbacks(holdingArea);
            fpStream = new FPGenericStream(holdingArea, FPStream.StreamDirection.InputToCentera, myCallbacks, (IntPtr)this.GetHashCode());

            FPLogger.ConsoleMessage("\n ** Partial blob write stream with sequence id = " + sequenceId);
            blobThread = new Thread(delegate() { theTag.BlobWritePartial(fpStream, sequenceId); });
            blobThread.Name = "PartialBlobWriteThread";
        }

        // Default ctor using a MemoryStream for the buffer
        public PartialBlobWriteStream(FPTag t, int id) : this(new MemoryStream(), t, id) { }
    }

    public class SlicedBlobWriter
    {
        internal class PartialBlobWriteThread : PartialBlobWriteStream
        {
            private Thread myWriteThread;
            
            internal PartialBlobWriteThread(FPTag t, int sequenceNum, Stream s, int offset, int numBytes)
                : base(t, sequenceNum)
            {
                myWriteThread = new Thread(delegate()
                {
                    BinaryReader myReader = new BinaryReader(new FPPartialInputStream(s, offset, numBytes));
                    BinaryWriter myWriter = new BinaryWriter(this);

                    FPLogger.ConsoleMessage("\nStarted sliced write thread " + GetHashCode() + " at " + offset + " size " + numBytes);
                    myWriter.Write(myReader.ReadBytes(numBytes));
                    myWriter.Flush();
                    Close();
                });
            }

            static public implicit operator Thread(PartialBlobWriteThread t)
            {
                return t.myWriteThread;
            }
        }

        private SlicedBlobWriter(Stream inputStream, FPTag tag, int threads)
        {
            int sliceSize = (int)(inputStream.Length / threads);
            int lastSlice = (int)(inputStream.Length - (sliceSize * (threads - 1)));

            int numBytes = sliceSize;
            int i;

            Thread[] writeThread = new Thread[threads];
            //PartialBlobWriteStream[] slices = new PartialBlobWriteStream[threads];

            for (i = 0; i < threads; i++)
            {
                if (i == (threads - 1))
                    numBytes = lastSlice;

                writeThread[i] = new PartialBlobWriteThread(tag, i, inputStream, (i * sliceSize), numBytes);
                writeThread[i].Start();       
            }

            for (i = 0; i < threads; i++)
            {
                writeThread[i].Join();
            }

            FPLogger.ConsoleMessage("\nFinished");
        }

        public static void Instance(Stream inputStream, FPTag tag, int threads)
        {
            SlicedBlobWriter writer = new SlicedBlobWriter(inputStream, tag, threads);
        }
    }

    public class SlicedBlobReader
    {
        internal class PartialBlobReadThread : PartialBlobReadStream
        {
            private Thread myReadThread;

            internal PartialBlobReadThread(FPTag t, Stream s, int offset, int numBytes)
                : base(t, offset, numBytes)
            {
                myReadThread = new Thread(delegate()
                {
                    BinaryReader myReader = new BinaryReader(this);
                    BinaryWriter myWriter = new BinaryWriter(new FPPartialOutputStream(s, offset, numBytes));

                    FPLogger.ConsoleMessage("\nStarted sliced read thread " + GetHashCode() + " at " + offset + " size " + numBytes);
                    // Wait until we have all the read content back in the buffer
                    Join();

                    myWriter.Write(myReader.ReadBytes(numBytes));
                    Close();
                });
            }

            static public implicit operator Thread(PartialBlobReadThread t)
            {
                return t.myReadThread;
            }
        }

        private SlicedBlobReader(Stream outputStream, FPTag t, int threads)
        {
            int sliceSize = (int)(t.BlobSize / threads);
            int lastSlice = (int)(t.BlobSize - (sliceSize * (threads - 1)));
            int i, numBytes = sliceSize;
            Thread[] readThread = new Thread[threads];

            for (i = 0; i < threads; i++)
            {
                if (i == (threads - 1))
                    numBytes = lastSlice;

                readThread[i] = new PartialBlobReadThread(t, outputStream, i * sliceSize, numBytes);
                readThread[i].Start();
            }

            // Wait for all the threads to complete
            for (i = 0; i < threads; i++)
            {
                readThread[i].Join();
            }

            FPLogger.ConsoleMessage("\n** finished");
        }

        public static void Instance(Stream outputStream, FPTag tag, int threads)
        {
            SlicedBlobReader writer = new SlicedBlobReader(outputStream, tag, threads);
        }
    }
}


namespace BlobStreams
{
    public class BlobStreams
    {

        [STAThread]
        static void Main(string[] args)
        {
            IntPtr userData = new IntPtr(0);
            string clipID;
            BinaryReader myReader;
            BinaryWriter myWriter;
            BlobStream myStream;

            // For thsi test we are using a file as the input. *Any* stream could be used.
            // This would be your test PDF - in a file or memory stream.
            FileStream inputStream = new FileStream("e:\\testfile", FileMode.Open);
            FPLogger.ConsoleMessage("\nInput file length " + inputStream.Length);

            try
            {
                FPLogger log = new FPLogger();
                log.LogPath = "C:\\GenStreamsLog.txt";
                log.Start();

                // Create a test clip demonstrating how to write a blob using a stream
                FPPool myPool = new FPPool("cse1.centera.lab.emc.com");
                FPClip myClip = myPool.ClipCreate("GenericStreamWrite_testClip");
                FPTag myTag = myClip.AddTag("testTag");

                // Simple test of the BlobWriteStream - write contents of a local file
                // to Centera using a StreamReader and StreamWriter
                // We are using a temporary MemoryStream for our staging area
                myStream = new BlobWriteStream(myTag);
                myReader = new BinaryReader(inputStream);
                myWriter = new BinaryWriter(myStream);

                long writeTime1 = DateTime.Now.Ticks;

                myWriter.Write(myReader.ReadBytes((int)inputStream.Length));
                myWriter.Flush();
                ((BlobWriteStream)myStream).Close(); // If we don't do this it will stay open forever with keep alives!

                writeTime1 = DateTime.Now.Ticks - writeTime1;

                // If we assign values to existing FP objects we must explicitly close
                // them first to avoid potential "Object In Use" errors on future closure
                myTag.Close();

                myTag = myClip.AddTag("SlicedBlob");

                inputStream.Seek(0, SeekOrigin.Begin);
                long writeTime = DateTime.Now.Ticks;

                SlicedBlobWriter.Instance(inputStream, myTag, 8);

                writeTime = DateTime.Now.Ticks - writeTime;
                myTag.Close();

                clipID = myClip.Write();
                myClip.Close();

                FPLogger.ConsoleMessage("\nGenericStreamWrite test succeeded " + clipID);

                // Now we will test reading it back from the Centera			{	
                myClip = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);
                myTag = myClip.NextTag;

                // Here we create a stream to read back the blob
                long readTime1 = DateTime.Now.Ticks;
                myStream = new BlobReadStream(myTag);

                // This is only to test to verify when we read the content back it is the same as the
                // input file i.e. we are going to store it on the file system and then use file system tools
                // to compare the files
                myReader = new BinaryReader(myStream);
                myWriter = new BinaryWriter(new FileStream("c:\\testfile2.out", FileMode.Create));

                ((BlobReadStream)myStream).Join();
                myWriter.Write(myReader.ReadBytes((int)myTag.BlobSize));

                myWriter.Flush();

                myStream.Close();
                readTime1 = DateTime.Now.Ticks - readTime1;

                myTag.Close();

                // This shows how to read content back using multiple threads
                myTag = myClip.NextTag;
                long readTime = DateTime.Now.Ticks;
                SlicedBlobReader.Instance(new FileStream("c:\\testfile2.out.sliced", FileMode.Create), myTag, 8);
                readTime = DateTime.Now.Ticks - readTime;

                myTag.Close();
                myClip.Close();
                myPool.Close();

                FPLogger.ConsoleMessage("\nSerial tests - Write time: " + TimeSpan.FromTicks(writeTime1) + " Read time " + TimeSpan.FromTicks(readTime1));
                FPLogger.ConsoleMessage("\nMulti threaded tests - Write time: " + TimeSpan.FromTicks(writeTime) + " Read time " + TimeSpan.FromTicks(readTime));
            }
            catch (FPLibraryException e)
            {
                ErrorInfo err = e.errorInfo;
                FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
            }
        }
    }
}
