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
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Stream 
    {

        public static FPStreamRef CreateFileForInput( string pFilePath,  string pPerm, long pBuffSize) 
        {
            FPStreamRef retval = SDK.FPStream_CreateFileForInput8(pFilePath, pPerm, pBuffSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreatePartialFileForInput(string pFilePath, string pPerm, long pBuffSize, long pOffset, long pSize)
        {
            FPStreamRef retval = SDK.FPStream_CreatePartialFileForInput8(pFilePath, pPerm, pBuffSize, pOffset, pSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateFileForOutput(string pFilePath, string pPerm) 
        {
            FPStreamRef retval = SDK.FPStream_CreateFileForOutput8(pFilePath, pPerm);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreatePartialFileForOutput(string pFilePath, string pPerm, long pBuffSize, long pOffset, long pSize, long pMaxFileSize)
        {
            FPStreamRef retval = SDK.FPStream_CreatePartialFileForOutput8(pFilePath, pPerm, pBuffSize, pOffset, pSize, pMaxFileSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateBufferForInput(IntPtr pBuffer, long pBuffLen) 
        {
            FPStreamRef retval = SDK.FPStream_CreateBufferForInput(pBuffer, pBuffLen);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateBufferForOutput( IntPtr pBuffer, long pBuffLen) 
        {
            FPStreamRef retval = SDK.FPStream_CreateBufferForOutput(pBuffer, pBuffLen);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPStreamRef pStream) 
        {
            SDK.FPStream_Close(pStream);
            SDK.CheckAndThrowError();
        }

        public static FPStreamRef FPStream_CreateToStdio()
        {
            FPStreamRef retval = SDK.FPStream_CreateToStdio();
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPStreamRef FPStream_CreateToNull()
        {
            FPStreamRef retval = SDK.FPStream_CreateToNull();
            SDK.CheckAndThrowError();
            return retval;
        }
		
        public static FPStreamRef FPStream_CreateTemporaryFile (long pMemBuffSize)
        {
            FPStreamRef retval = SDK.FPStream_CreateTemporaryFile(pMemBuffSize);
            SDK.CheckAndThrowError();
            return retval;
        }
    }
}