using System;
using System.IO;

namespace EMC.Centera.SDK
{
    /// <summary>
    ///This is a helper class that allows for the use of Windows files with Wide Character filenames without the
    ///need for additional special marshalling or Centera SDK support. It is derived from a GenericStream but the relevant
    ///FileStream object is created for you.
    /// </summary>

    public class FPWideFilenameStream : FPGenericStream
    {
        private FPWideFilenameStream(string filename, StreamDirection direction, FileMode mode)
            : base(File.Open(filename, mode), direction, new IntPtr())
        {
            if (direction == StreamDirection.InputToCentera)
                StreamLen = userStream.Length;
        }

        /// <summary>
        ///  Open a file for Reading to transfer data to the Centera 
        /// </summary>
        public FPWideFilenameStream(string filename)
            : this(filename, StreamDirection.InputToCentera, FileMode.Open) {}

        /// <summary>
        /// Open a file using the supplied mode for transferring data from the Centera 
        /// </summary>
        public FPWideFilenameStream(string filename, FileMode mode)
            : this(filename, StreamDirection.OutputFromCentera, mode) { }

        /// <summary>
        /// Open a partial file segment (bounded region) for transferring data to the Centera 
        /// </summary>
        public FPWideFilenameStream(string filename, long offset, long length)
            : base(new FPPartialInputStream(File.OpenRead(filename), offset, length), StreamDirection.InputToCentera, new IntPtr()) {}

        /// <summary>
        /// Open a partial file segment (bounded region) using the supplied mode for transferring data from the Centera 
        /// </summary>
        public FPWideFilenameStream(string filename, FileMode mode, long offset, long length, long maxFileSize)
            : base(new FPPartialOutputStream(File.Open(filename, mode), offset, length, maxFileSize), StreamDirection.OutputFromCentera, new IntPtr()) {}

        public override void Close()
        {
            userStream.Close();
            base.Close();
        }
    }
}