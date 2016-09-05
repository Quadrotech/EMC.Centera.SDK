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

using System.Runtime.InteropServices;

namespace EMC.Centera.SDK.FPTypes
{
    /// <summary> The structure that holds error information, which is retrieved by the FPPool_GetLastErrorInfo() function.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FPErrorInfo
    {
        public FPInt     error;             /* The last FPLibrary error that occurred on this thread. */

        public FPInt systemError;       /* The last system error that occurred on this thread.    */

        [ MarshalAs( UnmanagedType.LPStr )]
        public string     trace;             /* The function trace for the last error that occurred.   */

        [ MarshalAs( UnmanagedType.LPStr )]
        public string    message;           /* The message associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.        */

        [ MarshalAs( UnmanagedType.LPStr )]
        public string    errorString;      /* The error string associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.   */

        public FPShort errorClass;       /* Keeps the class of message:<br>1: Network error <br>2: Server error <br>3: Client error                                                  */

    }
}