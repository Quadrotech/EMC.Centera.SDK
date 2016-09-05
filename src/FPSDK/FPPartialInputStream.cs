using System.IO;

namespace EMC.Centera.SDK
{
    /// <summary> The FPPartialInputStream reads data from a section of an
    /// underlying stream using offset and size to determine the "section" boundaries.
    /// </summary>
    public class FPPartialInputStream : FPPartialStream
    {
        public FPPartialInputStream(Stream s, long o, long c) : base(s, o, c) { }

        public override bool CanRead => true;

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
}