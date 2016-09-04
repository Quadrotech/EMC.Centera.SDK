using System;
using System.IO;

namespace EMC.Centera.SDK
{
    public class FPWideFilenameStream : FPGenericStream
    {
        private FPWideFilenameStream(string filename, StreamDirection direction, FileMode mode)
            : base(File.Open(filename, mode), direction, new IntPtr())
        {
            if (direction == StreamDirection.InputToCentera)
                StreamLen = userStream.Length;
        }

        /* Open a file for Reading to transfer data to the Centera */
        public FPWideFilenameStream(string filename)
            : this(filename, StreamDirection.InputToCentera, FileMode.Open) {}

        /* Open a file using the supplied mode for transferring data from the Centera */
        public FPWideFilenameStream(string filename, FileMode mode)
            : this(filename, StreamDirection.OutputFromCentera, mode) { }

        /* Open a partial file segment (bounded region) for transferring data to the Centera */
        public FPWideFilenameStream(string filename, long offset, long length)
            : base(new FPPartialInputStream(File.OpenRead(filename), offset, length), StreamDirection.InputToCentera, new IntPtr()) {}

        /* Open a partial file segment (bounded region) using the supplied mode for transferring data from the Centera */
        public FPWideFilenameStream(string filename, FileMode mode, long offset, long length, long maxFileSize)
            : base(new FPPartialOutputStream(File.Open(filename, mode), offset, length, maxFileSize), StreamDirection.OutputFromCentera, new IntPtr()) {}

        public override void Close()
        {
            userStream.Close();
            base.Close();
        }
    }
}