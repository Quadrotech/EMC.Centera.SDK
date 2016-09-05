using System;
using System.Runtime.InteropServices;
using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    internal sealed class SDK 
    {
        // These calls need special marshalling as they input AND output UTF-8 strings that may include "foreign" characters
        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetDescriptionAttribute8(FPClipRef inClip,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetDescriptionAttributeIndex8(FPClipRef inClip,
            FPInt inIndex,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrName,
            ref FPInt ioAttrNameLen,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetName8(FPClipRef inClip,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] outName,
            ref FPInt ioNameLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetRetentionClassName8(FPClipRef inClipRef,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] outClassName,
            ref FPInt ioNameLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_GetStringAttribute8(FPTagRef inTag,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_GetIndexAttribute8(FPTagRef inTag,
            FPInt inIndex,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrName,
            ref FPInt ioAttrNameLen,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_GetTagName8(FPTagRef inTag,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] outName,
            ref FPInt ioNameLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPRetentionClass_GetName8(FPRetentionClassRef inClassRef,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] outName,
            ref FPInt ioNameLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryResult_GetField8(FPQueryResultRef inQueryResultRef,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        // These ones also use UTF-8 strings for input and output but will return standard strings from the cluster i.e. no "foreign" characters
        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetCapability8(FPPoolRef inPool,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inCapabilityName,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inCapabilityAttributeName,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder outCapabilityValue,
            ref FPInt ioCapabilityValueLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetClusterTime8(FPPoolRef inPool,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder outClusterTime,
            ref FPInt ioClusterTimeLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetComponentVersion8(FPInt inComponent,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder outVersion,
            ref FPInt ioVersionLen);

        // These calls have UTF-8 string inpuit parameters that require special marshalling
        [DllImport("FPLibrary.dll")]
        public static extern FPPoolRef FPPool_Open8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inPoolAddress);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_SetIntOption8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inOptionName, FPInt inOptionValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPPool_GetIntOption8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inOptionName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_SetGlobalOption8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inOptionName, FPInt inOptionValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPPool_GetGlobalOption8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inOptionName);

        [DllImport("FPLibrary.dll")]
        public static extern FPClipRef FPClip_Create8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_AuditedDelete8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClipID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inReason, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetName8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClipName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetCreationDate8(FPClipRef inClip, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outDate, ref FPInt ioDateLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetDescriptionAttribute8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_RemoveDescriptionAttribute8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_Create8(FPTagRef inParent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetStringAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetLongAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName, FPLong inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetBoolAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName, FPBool inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetNamedClass8(FPRetentionClassContextRef inContextRef,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inName);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTag_GetLongAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPTag_GetBoolAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_RemoveAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inAttrName);


        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_SelectField8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_DeselectField8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPQueryExpression_IsFieldSelected8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern FPQueryRef FPQuery_Open8(FPPoolRef inPool, FPLong inStartTime, FPLong inStopTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inReserved);

        [DllImport("FPLibrary.dll")]
        public static extern FPMonitorRef FPMonitor_Open8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClusterAddress);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateFileForInput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pPerm, long pBuffSize);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreatePartialFileForInput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pPerm, long pBuffSize, long pOffset, long pLength);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateFileForOutput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pPerm);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreatePartialFileForOutput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string pPerm, long pBuffSize, long pOffset, long pLength, long pMaxFileLength);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToLong8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_LongToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_RegisterApplication8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string appName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string appVersion);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetRetentionHold8(FPClipRef inClip, FPBool inHoldFlag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inHoldID);

        [DllImport("FPLibrary.dll")]
        public static extern string FPClip_GetEBRClassName8(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetEBREventTime8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outEBREventTime, ref FPInt ioEBREventTimeLen);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToSeconds8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_SecondsToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToMilliseconds8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_MillisecondsToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetLogPath8(FPLogStateRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inPath);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_Save8(FPLogStateRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inPath);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogging_Log8(FPLogLevel inLevel, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inMessage);

        [DllImport("FPLibrary.dll")]
        public static extern FPLogStateRef FPLogging_OpenLogState8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] string inPathName);

        // Standard data types no UTF-8 marshalling effort
        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_Close(FPPoolRef inPool);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPPool_GetLastError();

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetLastErrorInfo(IntPtr outErrorInfo);

        [DllImport("FPLibrary.dll")]
        public static extern void _FPPool_GetPoolInfo(FPPoolRef inPool, ref FPPoolInfo outPoolInfo);

        [DllImport("FPLibrary.dll")]
        public static extern FPClipRef FPClip_Open(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inClipID, FPInt inOpenMode);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Close(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPPoolRef FPClip_GetPoolRef(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_Exists(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Delete(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Purge(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inClipID);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPClip_GetTopTag(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPClip_GetNumBlobs(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPClip_GetNumTags(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPClip_GetTotalSize(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetClipID(FPClipRef inClip, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetRetentionPeriod(FPClipRef inClip, FPLong inRetentionSecs);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPClip_GetRetentionPeriod(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_IsModified(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPClip_FetchNext(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Write(FPClipRef inClip, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_RawRead(FPClipRef inClip, FPStreamRef inStream);

        [DllImport("FPLibrary.dll")]
        public static extern FPClipRef FPClip_RawOpen(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inClipID, FPStreamRef inStream, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPClip_GetNumDescriptionAttributes(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_Close(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_Copy(FPTagRef inTag, FPTagRef inNewParent, FPInt inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern FPClipRef FPTag_GetClipRef(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_GetSibling(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_GetPrevSibling(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_GetFirstChild(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_GetParent(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_Delete(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPTag_GetNumAttributes(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTag_GetBlobSize(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_BlobWrite(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_BlobRead(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_BlobReadPartial(FPTagRef inTag, FPStreamRef inStream, FPLong inOffset, FPLong inReadLength, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_BlobPurge(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPTag_BlobExists(FPTagRef inTag);

        [DllImport("FPLibrary.dll")]
        public static extern FPQueryExpressionRef FPQueryExpression_Create();

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_Close(FPQueryExpressionRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_SetStartTime(FPQueryExpressionRef inRef, FPLong inTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_SetEndTime(FPQueryExpressionRef inRef, FPLong inTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_SetType(FPQueryExpressionRef inRef, FPInt inType);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPQueryExpression_GetStartTime(FPQueryExpressionRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPQueryExpression_GetEndTime(FPQueryExpressionRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPQueryExpression_GetType(FPQueryExpressionRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPPoolQueryRef FPPoolQuery_Open(FPPoolRef inPoolRef, FPQueryExpressionRef inQueryExpressionRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPoolQuery_Close(FPPoolQueryRef inPoolQueryRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPPoolRef FPPoolQuery_GetPoolRef(FPPoolQueryRef inPoolQueryRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPQueryResultRef FPPoolQuery_FetchResult(FPPoolQueryRef inPoolQueryRef, FPInt inTimeout);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryResult_Close(FPQueryResultRef inQueryResultRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryResult_GetClipID(FPQueryResultRef inQueryResultRef, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outClipID);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPQueryResult_GetTimestamp(FPQueryResultRef inQueryResultRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPQueryResult_GetResultCode(FPQueryResultRef inQueryResultRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPQueryResult_GetType(FPQueryResultRef inQueryResultRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPQuery_FetchResult(FPQueryRef inQuery, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outResultClip, ref FPLong outTimestamp, FPInt inTimeout);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQuery_Close(FPQueryRef inQuery);

        [DllImport("FPLibrary.dll")]
        public static extern FPPoolRef FPQuery_GetPoolRef(FPQueryRef inQuery);

        [DllImport("FPLibrary.dll")]
        public static extern void FPMonitor_Close(FPMonitorRef inMonitor);

        [DllImport("FPLibrary.dll")]
        public static extern void FPMonitor_GetDiscovery(FPMonitorRef inMonitor, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outData, ref FPInt ioDataLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPMonitor_GetDiscoveryStream(FPMonitorRef inMonitor, FPStreamRef inStream);

        [DllImport("FPLibrary.dll")]
        public static extern void FPMonitor_GetAllStatistics(FPMonitorRef inMonitor, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outData, ref FPInt ioDataLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPMonitor_GetAllStatisticsStream(FPMonitorRef inMonitor, FPStreamRef inStream);

        [DllImport("FPLibrary.dll")]
        public static extern FPEventCallbackRef FPEventCallback_RegisterForAllEvents(FPMonitorRef inMonitor, FPStreamRef inStream);

        [DllImport("FPLibrary.dll")]
        public static extern void FPEventCallback_Close(FPEventCallbackRef inRegisterRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_SetClipID(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] string inContentAddress);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetClipID(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outContentAddress);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassContextRef FPPool_GetRetentionClassContext(FPPoolRef inPoolRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPRetentionClassContext_Close(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPRetentionClassContext_GetNumClasses(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetFirstClass(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetLastClass(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetNextClass(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetPreviousClass(FPRetentionClassContextRef inContextRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPRetentionClass_GetPeriod(FPRetentionClassRef inClassRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPRetentionClass_Close(FPRetentionClassRef inClassRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetRetentionClass(FPClipRef inClipRef, FPRetentionClassRef inClassRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_RemoveRetentionClass(FPClipRef inClipRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_ValidateRetentionClass(FPRetentionClassContextRef inContextRef, FPClipRef inClipRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetCanonicalFormat([MarshalAs(UnmanagedType.LPStr)] string inClipID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 40, ArraySubType = UnmanagedType.U1)] byte[] outClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetStringFormat(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 40, ArraySubType = UnmanagedType.U1)] byte[] inClipID,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder outClipID);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateBufferForInput(IntPtr pBuffer, long pBuffLen);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateBufferForOutput(IntPtr pBuffer, long pBuffLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPStream_Close(FPStreamRef pStream);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateToStdio();

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateToNull();

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateTemporaryFile(long pMemBuffSize);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_BlobWritePartial(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions, FPLong inSequenceID);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_GetRetentionHold(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_IsEBREnabled(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_EnableEBRWithPeriod(FPClipRef inClip, FPLong inRetentionSeconds);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_EnableEBRWithClass(FPClipRef inClipconst, FPRetentionClassRef inRetentionClass);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_TriggerEBREvent(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_TriggerEBREventWithPeriod(FPClipRef inClip, FPLong inRetentionSeconds);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_TriggerEBREventWithClass(FPClipRef inClipconst, FPRetentionClassRef inRetentionClass);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPClip_GetEBRPeriod(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPLogState_GetAppendMode(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPLogState_SetAppendMode(FPLogStateRef inRef, FPBool inState);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPLogState_GetDisableCallback(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPLogState_SetDisableCallback(FPLogStateRef inRef, FPBool inState);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPLogState_GetLogFilter(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetLogFilter(FPLogStateRef inRef, FPInt inComponents);

        [DllImport("FPLibrary.dll")]
        public static extern FPLogFormat FPLogState_GetLogFormat(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetLogFormat(FPLogStateRef inRef, FPLogFormat inFormat);

        [DllImport("FPLibrary.dll")]
        public static extern FPLogLevel FPLogState_GetLogLevel(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetLogLevel(FPLogStateRef inRef, FPLogLevel inLevel);

        [DllImport("FPLibrary.dll")]
        public static extern string FPLogState_GetLogPath(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPLogState_GetMaxLogSize(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetMaxLogSize(FPLogStateRef inRef, FPLong inSize);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPLogState_GetMaxOverflows(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetMaxOverflows(FPLogStateRef inRef, FPInt inSize);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPLogState_GetPollInterval(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetPollInterval(FPLogStateRef inRef, FPInt inInterval);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_Delete(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern FPLogStateRef FPLogging_CreateLogState();

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogging_Start(FPLogStateRef inRef);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogging_Stop();

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogging_RegisterCallback(FPLogProc inProc);

        /// <summary>
        /// Generic error checking routine for transforming SDK errors into .NET exceptions
        /// </summary>
        public static void CheckAndThrowError()
        {
            /* Get the error code of the last SDK API function call */
            FPInt errorCode = FPPool_GetLastError();
            if (0 != errorCode)
            {
                FPErrorInfo errInfo = new FPErrorInfo();

                unsafe
                {
                    IntPtr ptr = (IntPtr) Marshal.AllocHGlobal((int)1024).ToPointer();

                    /* Get the error message of the last SDK API function call */
                    FPPool_GetLastErrorInfo(ptr);
                    errInfo = (FPErrorInfo)Marshal.PtrToStructure(ptr, typeof(FPErrorInfo));
                    Marshal.FreeHGlobal(ptr);
                }

                throw new FPLibraryException(errInfo);
            }
        }
    }
}