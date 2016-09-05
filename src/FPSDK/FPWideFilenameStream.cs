/*****************************************************************************

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