using System;
using System.IO;

namespace EMC.Centera.SDK
{
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

        public override bool CanRead => false;

        public override bool CanWrite => false;

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

        public override long Length => length;

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
}