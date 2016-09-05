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
using System.Runtime.InteropServices;
using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Pool 
    {
        public static FPPoolRef Open( string inPoolAddress) 
        {
            FPPoolRef retval = SDK.FPPool_Open8(inPoolAddress);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPPoolRef inPool) 
        {
            SDK.FPPool_Close(inPool);
            SDK.CheckAndThrowError();
        }
        public static void SetIntOption(FPPoolRef inPool,  string inOptionName, FPInt inOptionValue) 
        {
            SDK.FPPool_SetIntOption8(inPool, inOptionName, inOptionValue);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetIntOption(FPPoolRef inPool,  string inOptionName) 
        {
            FPInt retval = SDK.FPPool_GetIntOption8(inPool, inOptionName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void SetGlobalOption( string inOptionName, FPInt inOptionValue) 
        {
            SDK.FPPool_SetGlobalOption8(inOptionName, inOptionValue);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetGlobalOption( string inOptionName) 
        {
            FPInt retval = SDK.FPPool_GetGlobalOption8(inOptionName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPInt GetLastError() 
        {
            FPInt retval = SDK.FPPool_GetLastError();
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void GetLastErrorInfo(ref FPErrorInfo outErrorInfo) 
        {
            unsafe
            {

                IntPtr ptr = (IntPtr)Marshal.AllocHGlobal((int)1024).ToPointer();

                /* Get the error message of the last SDK API function call */
                SDK.FPPool_GetLastErrorInfo(ptr);
                outErrorInfo = (FPErrorInfo)Marshal.PtrToStructure(ptr, typeof(FPErrorInfo));
                Marshal.FreeHGlobal(ptr);
            }

            SDK.CheckAndThrowError();
        }

        public static void GetPoolInfo(FPPoolRef inPool, ref FPPoolInfo outPoolInfo) 
        {
            outPoolInfo.poolInfoVersion = FP.POOL_INFO_VERSION;
            SDK._FPPool_GetPoolInfo(inPool, ref outPoolInfo);
            SDK.CheckAndThrowError();
        }
        public static void GetCapability(FPPoolRef inPool,  string inCapabilityName,  string inCapabilityAttributeName,  StringBuilder outCapabilityValue, ref FPInt ioCapabilityValueLen) 
        {
            SDK.FPPool_GetCapability8(inPool, inCapabilityName, inCapabilityAttributeName, outCapabilityValue, ref ioCapabilityValueLen);
            SDK.CheckAndThrowError();
        }
        public static void GetClusterTime(FPPoolRef inPool,  StringBuilder outClusterTime, ref FPInt ioClusterTimeLen) 
        {
            SDK.FPPool_GetClusterTime8(inPool, outClusterTime, ref ioClusterTimeLen);
            SDK.CheckAndThrowError();
        }
        public static void GetComponentVersion(FPInt inComponent,  StringBuilder outVersion, ref FPInt ioVersionLen) 
        {
            SDK.FPPool_GetComponentVersion8(inComponent, outVersion, ref ioVersionLen);
            SDK.CheckAndThrowError();
        }
        public static void SetClipID(FPPoolRef inPool,  string inContentAddress) 
        {
            SDK.FPPool_SetClipID(inPool, inContentAddress);
            SDK.CheckAndThrowError();
        }
        public static void GetClipID(FPPoolRef inPool,  StringBuilder outContentAddress) 
        {
            SDK.FPPool_GetClipID(inPool, outContentAddress);
            SDK.CheckAndThrowError();
        }
        public static FPRetentionClassContextRef GetRetentionClassContext(FPPoolRef inPoolRef) 
        {
            FPRetentionClassContextRef retval = SDK.FPPool_GetRetentionClassContext(inPoolRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void RegisterApplication(string appName, string appVersion)
        {
            SDK.FPPool_RegisterApplication8(appName, appVersion);
            SDK.CheckAndThrowError();
        }
    }
}