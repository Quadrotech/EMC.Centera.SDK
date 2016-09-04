using System;
using System.IO;

namespace EMC.Centera.SDK
{
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
}