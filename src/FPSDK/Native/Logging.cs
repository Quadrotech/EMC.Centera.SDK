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

using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Logging
    {
        public static void RegisterCallback(FPLogProc inProc)
        {
            SDK.FPLogging_RegisterCallback(inProc);
            SDK.CheckAndThrowError();
        }

        public static FPBool GetAppendMode(FPLogStateRef inRef)
        {
            FPBool retval = SDK.FPLogState_GetAppendMode(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPBool GetDisableCallback(FPLogStateRef inRef)
        {
            FPBool retval = SDK.FPLogState_GetDisableCallback(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPInt GetLogFilter(FPLogStateRef inRef)
        {
            FPInt retval = SDK.FPLogState_GetLogFilter(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPLogFormat GetLogFormat(FPLogStateRef inRef)
        {
            FPLogFormat retval = SDK.FPLogState_GetLogFormat(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPLogLevel GetLogLevel(FPLogStateRef inRef)
        {
            FPLogLevel retval = SDK.FPLogState_GetLogLevel(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static string GetLogPath(FPLogStateRef inRef)
        {
            string retval = SDK.FPLogState_GetLogPath(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPLong GetMaxLogSize(FPLogStateRef inRef)
        {
            FPLong retval = SDK.FPLogState_GetMaxLogSize(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPInt GetMaxOverflows(FPLogStateRef inRef)
        {
            FPInt retval = SDK.FPLogState_GetMaxOverflows(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPInt GetPollInterval(FPLogStateRef inRef)
        {
            FPInt retval = SDK.FPLogState_GetPollInterval(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void SetAppendMode(FPLogStateRef inRef, FPBool inValue)
        {
            SDK.FPLogState_SetAppendMode(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetDisableCallback(FPLogStateRef inRef, FPBool inValue)
        {
            SDK.FPLogState_SetDisableCallback(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetLogFilter(FPLogStateRef inRef, FPInt inValue)
        {
            SDK.FPLogState_SetLogFilter(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetLogFormat(FPLogStateRef inRef, FPLogFormat inValue)
        {
            SDK.FPLogState_SetLogFormat(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetLogLevel(FPLogStateRef inRef, FPLogLevel inValue)
        {
            SDK.FPLogState_SetLogLevel(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetLogPath(FPLogStateRef inRef, string inValue)
        {
            SDK.FPLogState_SetLogPath8(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetMaxLogSize(FPLogStateRef inRef, FPLong inValue)
        {
            SDK.FPLogState_SetMaxLogSize(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetMaxOverflows(FPLogStateRef inRef, FPInt inValue)
        {
            SDK.FPLogState_SetMaxOverflows(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void SetPollInterval(FPLogStateRef inRef, FPInt inValue)
        {
            SDK.FPLogState_SetPollInterval(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void Save(FPLogStateRef inRef, string inValue)
        {
            SDK.FPLogState_Save8(inRef, inValue);
            SDK.CheckAndThrowError();
        }

        public static void Delete(FPLogStateRef inRef)
        {
            SDK.FPLogState_Delete(inRef);
            SDK.CheckAndThrowError();
        }

        public static FPLogStateRef CreateLogState()
        {
            FPLogStateRef retval = SDK.FPLogging_CreateLogState();
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void Log(FPLogLevel inLevel, string inMessage)
        {
            SDK.FPLogging_Log8(inLevel, inMessage);
            SDK.CheckAndThrowError();
        }

        public static FPLogStateRef OpenLogState(string inName)
        {
            FPLogStateRef retval = SDK.FPLogging_OpenLogState8(inName);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void Start(FPLogStateRef inRef)
        {
            SDK.FPLogging_Start(inRef);
            SDK.CheckAndThrowError();
        }

        public static void Stop()
        {
            SDK.FPLogging_Stop();
            SDK.CheckAndThrowError();
        }


    }
}