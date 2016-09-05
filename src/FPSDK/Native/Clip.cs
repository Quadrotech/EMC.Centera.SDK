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

using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Clip 
    {
        public static FPClipRef Create(FPPoolRef inPool,  string inName) 
        {
            FPClipRef retval = SDK.FPClip_Create8(inPool, inName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPClipRef Open(FPPoolRef inPool,  string inClipID, FPInt inOpenMode) 
        {
            FPClipRef retval = SDK.FPClip_Open(inPool, inClipID, inOpenMode);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPClipRef inClip) 
        {
            SDK.FPClip_Close(inClip);
            SDK.CheckAndThrowError();
        }
        public static FPPoolRef GetPoolRef(FPClipRef inClip) 
        {
            FPPoolRef retval = SDK.FPClip_GetPoolRef(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPBool Exists(FPPoolRef inPool,  string inClipID) 
        {
            FPBool retval = SDK.FPClip_Exists(inPool, inClipID);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Delete(FPPoolRef inPool,  string inClipID) 
        {
            SDK.FPClip_Delete(inPool, inClipID);
            SDK.CheckAndThrowError();
        }
        public static void AuditedDelete(FPPoolRef inPool,  string inClipID,  string inReason, FPLong inOptions) 
        {
            SDK.FPClip_AuditedDelete8(inPool, inClipID, inReason, inOptions);
            SDK.CheckAndThrowError();
        }
        public static void Purge(FPPoolRef inPool,  string inClipID) 
        {
            SDK.FPClip_Purge(inPool, inClipID);
            SDK.CheckAndThrowError();
        }
        public static FPTagRef GetTopTag(FPClipRef inClip) 
        {
            FPTagRef retval = SDK.FPClip_GetTopTag(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPInt GetNumBlobs(FPClipRef inClip) 
        {
            FPInt retval = SDK.FPClip_GetNumBlobs(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPInt GetNumTags(FPClipRef inClip) 
        {
            FPInt retval = SDK.FPClip_GetNumTags(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPLong GetTotalSize(FPClipRef inClip) 
        {
            FPLong retval = SDK.FPClip_GetTotalSize(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void GetClipID(FPClipRef inClip,  StringBuilder outClipID) 
        {
            SDK.FPClip_GetClipID(inClip, outClipID);
            SDK.CheckAndThrowError();
        }
        public static void SetName(FPClipRef inClip,  string inClipName) 
        {
            SDK.FPClip_SetName8(inClip, inClipName);
            SDK.CheckAndThrowError();
        }
        public static void GetName(FPClipRef inClip,  ref byte[] outName, ref FPInt ioNameLen) 
        {
            SDK.FPClip_GetName8(inClip, outName, ref ioNameLen);
            SDK.CheckAndThrowError();
        }
        public static void GetCreationDate(FPClipRef inClip,  StringBuilder outDate, ref FPInt ioDateLen) 
        {
            SDK.FPClip_GetCreationDate8(inClip, outDate, ref ioDateLen);
            SDK.CheckAndThrowError();
        }
        public static void SetRetentionPeriod(FPClipRef inClip, FPLong inRetentionSecs) 
        {
            SDK.FPClip_SetRetentionPeriod(inClip, inRetentionSecs);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetRetentionPeriod(FPClipRef inClip) 
        {
            FPLong retval = SDK.FPClip_GetRetentionPeriod(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPBool IsModified(FPClipRef inClip) 
        {
            FPBool retval = SDK.FPClip_IsModified(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPTagRef FetchNext(FPClipRef inClip) 
        {
            FPTagRef retval = SDK.FPClip_FetchNext(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Write(FPClipRef inClip,  StringBuilder outClipID) 
        {
            SDK.FPClip_Write(inClip, outClipID);
            SDK.CheckAndThrowError();
        }
        public static void RawRead(FPClipRef inClip, FPStreamRef inStream) 
        {
            SDK.FPClip_RawRead(inClip, inStream);
            SDK.CheckAndThrowError();
        }
        public static FPClipRef RawOpen(FPPoolRef inPool,  string inClipID, FPStreamRef inStream, FPLong inOptions) 
        {
            FPClipRef retval = SDK.FPClip_RawOpen(inPool, inClipID, inStream, inOptions);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void SetDescriptionAttribute(FPClipRef inClip,  string inAttrName,  string inAttrValue) 
        {
            SDK.FPClip_SetDescriptionAttribute8(inClip, inAttrName, inAttrValue);
            SDK.CheckAndThrowError();
        }
        public static void RemoveDescriptionAttribute(FPClipRef inClip,  string inAttrName) 
        {
            SDK.FPClip_RemoveDescriptionAttribute8(inClip, inAttrName);
            SDK.CheckAndThrowError();
        }
        public static void GetDescriptionAttribute(FPClipRef inClip,  string inAttrName,  ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
        {
            SDK.FPClip_GetDescriptionAttribute8(inClip, inAttrName, outAttrValue, ref ioAttrValueLen);
            SDK.CheckAndThrowError();
        }
        public static void GetDescriptionAttributeIndex(FPClipRef inClip, FPInt inIndex, ref byte[] outAttrName, ref FPInt ioAttrNameLen, ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
        {
            SDK.FPClip_GetDescriptionAttributeIndex8(inClip, inIndex, outAttrName, ref ioAttrNameLen, outAttrValue, ref ioAttrValueLen);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetNumDescriptionAttributes(FPClipRef inClip) 
        {
            FPInt retval = SDK.FPClip_GetNumDescriptionAttributes(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void SetRetentionClass(FPClipRef inClipRef, FPRetentionClassRef inClassRef) 
        {
            SDK.FPClip_SetRetentionClass(inClipRef, inClassRef);
            SDK.CheckAndThrowError();
        }
        public static void RemoveRetentionClass(FPClipRef inClipRef) 
        {
            SDK.FPClip_RemoveRetentionClass(inClipRef);
            SDK.CheckAndThrowError();
        }
        public static void GetRetentionClassName(FPClipRef inClipRef, ref byte[] outClassName, ref FPInt ioNameLen) 
        {
            SDK.FPClip_GetRetentionClassName8(inClipRef, outClassName, ref ioNameLen);
            SDK.CheckAndThrowError();
        }
        public static FPBool ValidateRetentionClass(FPRetentionClassContextRef inContextRef, FPClipRef inClipRef) 
        {
            FPBool retval = SDK.FPClip_ValidateRetentionClass(inContextRef, inClipRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void GetCanonicalFormat( string inClipID,  byte[] outClipID) 
        {
            SDK.FPClip_GetCanonicalFormat(inClipID, outClipID);
            SDK.CheckAndThrowError();
        }
        public static void GetStringFormat( byte[] inClipID,  StringBuilder outClipID) 
        {
            SDK.FPClip_GetStringFormat(inClipID, outClipID);
            SDK.CheckAndThrowError();
        }

        public static void SetRetentionHold (FPClipRef inClip, FPBool inHoldFlag, string inHoldID)
        {
            SDK.FPClip_SetRetentionHold8(inClip, inHoldFlag, inHoldID);
            SDK.CheckAndThrowError();
        }

        public static FPBool GetRetentionHold (FPClipRef inClip)
        {
            FPBool retval = SDK.FPClip_GetRetentionHold(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPBool IsEBREnabled (FPClipRef inClip)
        {
            FPBool retval = SDK.FPClip_IsEBREnabled(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPLong GetEBRPeriod (FPClipRef inClip)
        {
            FPLong retval = SDK.FPClip_GetEBRPeriod(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void GetEBREventTime(FPClipRef inClip, StringBuilder outEBREventTime, ref FPInt ioEBREventTimeLen)
        {
            SDK.FPClip_GetEBREventTime8(inClip, outEBREventTime, ref ioEBREventTimeLen);
            SDK.CheckAndThrowError();
        }

        public static string GetEBRClassName (FPClipRef inClip)
        {
            string retval = SDK.FPClip_GetEBRClassName8(inClip);
            SDK.CheckAndThrowError();
            return retval;
        }

        public static void EnableEBRWithPeriod (FPClipRef inClip, FPLong inSeconds)
        {
            SDK.FPClip_EnableEBRWithPeriod(inClip, inSeconds);
            SDK.CheckAndThrowError();
        }

        public static void EnableEBRWithClass (FPClipRef inClip, FPRetentionClassRef inClass)
        {
            SDK.FPClip_EnableEBRWithClass(inClip, inClass);
            SDK.CheckAndThrowError();
        }

        public static void TriggerEBREvent(FPClipRef inClip)
        {
            SDK.FPClip_TriggerEBREvent(inClip);
            SDK.CheckAndThrowError();
        }

        public static void TriggerEBREventWithPeriod (FPClipRef inClip, FPLong inSeconds)
        {
            SDK.FPClip_TriggerEBREventWithPeriod(inClip, inSeconds);
            SDK.CheckAndThrowError();
        }

        public static void TriggerEBREventWithClass (FPClipRef inClip, FPRetentionClassRef inClass)
        {
            SDK.FPClip_TriggerEBREventWithClass(inClip, inClass);
            SDK.CheckAndThrowError();
        }

    }
}