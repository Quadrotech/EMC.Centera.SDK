/******************************************************************************

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
using System.Text;
using System.Runtime.InteropServices;
using EMC.Centera;
using EMC.Centera.FPTypes;

namespace EMC.Centera.FPApi
{
	/// <summary>
	/// FPApi is a wrapper class for the Centera SDK DLL
	/// </summary>
	/** The structure that will store the pool information that
			FPPool_GetPoolInfo() retrieves.
		  */
	public class FP 
	{
		public static readonly String  OPTION_BUFFERSIZE                                 = "buffersize";
		public static readonly String  OPTION_TIMEOUT                                    = "timeout";
		public static readonly String  OPTION_RETRYCOUNT                                 = "retrycount";
		public static readonly String  OPTION_RETRYSLEEP                                 = "retrysleep";
		public static readonly String  OPTION_MAXCONNECTIONS                             = "maxconnections";
		public static readonly String  OPTION_ENABLE_MULTICLUSTER_FAILOVER               = "multiclusterfailover";
		public static readonly String  OPTION_DEFAULT_COLLISION_AVOIDANCE                = "collisionavoidance";
		public static readonly String  OPTION_PREFETCH_SIZE                              = "prefetchsize";
		public static readonly String  OPTION_CLUSTER_NON_AVAIL_TIME                     = "clusternonavailtime";
		public static readonly String  OPTION_PROBE_LIMIT                                = "probetimelimit";
		public static readonly String  OPTION_EMBEDDED_DATA_THRESHOLD                    = "embedding_threshold";
		public static readonly String  OPTION_OPENSTRATEGY                               = "openstrategy";
		public static readonly String  OPTION_ATTRIBUTE_LIMIT                            = "attributelimit";
		public static readonly String  OPTION_MULTICLUSTER_READ_STRATEGY                 = "multicluster_read_strategy";
		public static readonly String  OPTION_MULTICLUSTER_WRITE_STRATEGY                = "multicluster_write_strategy";
		public static readonly String  OPTION_MULTICLUSTER_DELETE_STRATEGY               = "multicluster_delete_strategy";
		public static readonly String  OPTION_MULTICLUSTER_EXISTS_STRATEGY               = "multicluster_exists_strategy";
		public static readonly String  OPTION_MULTICLUSTER_QUERY_STRATEGY                = "multicluster_query_strategy";
		public static readonly String  OPTION_MULTICLUSTER_READ_CLUSTERS                 = "multicluster_read_clusters";
		public static readonly String  OPTION_MULTICLUSTER_WRITE_CLUSTERS                = "multicluster_write_clusters";
		public static readonly String  OPTION_MULTICLUSTER_DELETE_CLUSTERS               = "multicluster_delete_clusters";
		public static readonly String  OPTION_MULTICLUSTER_EXISTS_CLUSTERS               = "multicluster_exists_clusters";
		public static readonly String  OPTION_MULTICLUSTER_QUERY_CLUSTERS                = "multicluster_query_clusters";
		public static readonly FPInt   NO_STRATEGY                                       = (FPInt )(0);
		public static readonly FPInt   FAILOVER_STRATEGY                                 = (FPInt )(1);
		public static readonly FPInt   REPLICATION_STRATEGY                              = (FPInt )(2);
		public static readonly FPInt   PRIMARY_ONLY                                      = (FPInt )(0);
		public static readonly FPInt   PRIMARY_AND_PRIMARY_REPLICA_CLUSTER_ONLY          = (FPInt )(1);
		public static readonly FPInt   NO_REPLICA_CLUSTERS                               = (FPInt )(2);
		public static readonly FPInt   ALL_CLUSTERS                                      = (FPInt )(3);
		public static readonly FPInt   NORMAL_OPEN                                       = (FPInt )(0);
		public static readonly FPInt   LAZY_OPEN                                         = (FPInt )(1);
		public static readonly FPInt   OPEN_ASTREE                                       = (FPInt )(1);
		public static readonly FPInt   OPEN_FLAT                                         = (FPInt )(2);
		public static readonly FPInt   OPTION_DEFAULT_OPTIONS                            = (FPInt )(0);
		public static readonly FPLong  OPTION_CALCID_MASK                                = (FPLong)(0x0000000F);
		public static readonly FPLong  OPTION_CLIENT_CALCID                              = (FPLong)(0x00000001);
		public static readonly FPLong  OPTION_CLIENT_CALCID_STREAMING                    = (FPLong)(0x00000002);
		public static readonly FPLong  OPTION_SERVER_CALCID_STREAMING                    = (FPLong)(0x00000003);
		public static readonly FPLong  OPTION_CALCID_NOCHECK                             = (FPLong)(0x00000010);
		public static readonly FPLong  OPTION_ENABLE_DUPLICATE_DETECTION                 = (FPLong)(0x00000020);
		public static readonly FPLong  OPTION_ENABLE_COLLISION_AVOIDANCE                 = (FPLong)(0x00000040);
		public static readonly FPLong  OPTION_DISABLE_COLLISION_AVOIDANCE                = (FPLong)(0x00000080);
		public static readonly FPLong  OPTION_EMBED_DATA                                 = (FPLong)(0x00000100);
		public static readonly FPLong  OPTION_LINK_DATA                                  = (FPLong)(0x00000200);
		public static readonly FPInt   OPTION_FULL_CLUSTER_LAYOUT                        = (FPInt )(0);
		public static readonly FPInt   OPTION_PARTIAL_CLUSTER_LAYOUT                     = (FPInt )(1);
		public static readonly FPInt   OPTION_NO_CLUSTER_LAYOUT                          = (FPInt )(2);
		public static readonly FPInt   OPTION_NO_COPY_OPTIONS                            = (FPInt )(0x00);
		public static readonly FPInt   OPTION_COPY_BLOBDATA                              = (FPInt )(0x01);
		public static readonly FPInt   OPTION_COPY_CHILDREN                              = (FPInt )(0x02);
		public static readonly FPInt   OPTION_DELETE_PRIVILEGED                          = (FPInt )(1);
		public static readonly FPInt   POOL_INFO_VERSION                                 = (FPInt )(2);
		public static readonly FPInt   QUERY_RESULT_CODE_OK                              = (FPInt )(0);
		public static readonly FPInt   QUERY_RESULT_CODE_INCOMPLETE                      = (FPInt )(1);
		public static readonly FPInt   QUERY_RESULT_CODE_COMPLETE                        = (FPInt )(2);
		public static readonly FPInt   QUERY_RESULT_CODE_END                             = (FPInt )(3);
		public static readonly FPInt   QUERY_RESULT_CODE_ABORT                           = (FPInt )(4);
		public static readonly FPInt   QUERY_RESULT_CODE_ERROR                           = (FPInt )(-1);
		public static readonly FPInt   QUERY_RESULT_CODE_PROGRESS                        = (FPInt )(99);
		public static readonly FPInt   QUERY_TYPE_EXISTING                               = (FPInt )(0x1);
		public static readonly FPInt   QUERY_TYPE_DELETED                                = (FPInt )(0x2);
		public static readonly FPLong  NO_RETENTION_PERIOD                               = (FPLong)(0);
		public static readonly FPLong  INFINITE_RETENTION_PERIOD                         = (FPLong)(-1);
		public static readonly FPLong  DEFAULT_RETENTION_PERIOD                          = (FPLong)(-2);
		public static readonly FPInt   VERSION_FPLIBRARY_DLL                             = (FPInt )(1);
		public static readonly FPInt   VERSION_FPLIBRARY_JAR                             = (FPInt )(2);
		public static readonly FPInt   NETWORK_ERRORCLASS                                = (FPInt )(1);
		public static readonly FPInt   SERVER_ERRORCLASS                                 = (FPInt )(2);
		public static readonly FPInt   CLIENT_ERRORCLASS                                 = (FPInt )(3);
		public static readonly String  TRUE                                              = "true";
		public static readonly String  FALSE                                             = "false";
		public static readonly String  READ                                              = "read";
		public static readonly String  WRITE                                             = "write";
		public static readonly String  DELETE                                            = "delete";
		public static readonly String  PURGE                                             = "purge";
		public static readonly String  EXIST                                             = "exist";
		public static readonly String  CLIPENUMERATION                                   = "clip-enumeration";
		public static readonly String  RETENTION                                         = "retention";
		public static readonly String  BLOBNAMING                                        = "blobnaming";
		public static readonly String  MONITOR                                           = "monitor";
		public static readonly String  DELETIONLOGGING                                   = "deletionlogging";
		public static readonly String  PRIVILEGEDDELETE                                  = "privileged-delete";
		public static readonly String  ALLOWED                                           = "allowed";
		public static readonly String  SUPPORTED                                         = "supported";
		public static readonly String  DUPLICATEDETECTION                                = "duplicate-detection";
		public static readonly String  DEFAULT                                           = "default";
		public static readonly String  SUPPORTED_SCHEMES                                 = "supported-schemes";
		public static readonly String  MD5                                               = "MD5";
		public static readonly String  MG                                                = "MG";
		public static readonly FPInt   ERROR_BASE                                        = (FPInt )(-10000);
		public static readonly FPInt   INVALID_NAME                                      = (FPInt )((ERROR_BASE-1));
		public static readonly FPInt   UNKNOWN_OPTION                                    = (FPInt )((ERROR_BASE-2));
		public static readonly FPInt   NOT_SEND_REQUEST_ERR                              = (FPInt )((ERROR_BASE-3));
		public static readonly FPInt   NOT_RECEIVE_REPLY_ERR                             = (FPInt )((ERROR_BASE-4));
		public static readonly FPInt   SERVER_ERR                                        = (FPInt )((ERROR_BASE-5));
		public static readonly FPInt   PARAM_ERR                                         = (FPInt )((ERROR_BASE-6));
		public static readonly FPInt   PATH_NOT_FOUND_ERR                                = (FPInt )((ERROR_BASE-7));
		public static readonly FPInt   CONTROLFIELD_ERR                                  = (FPInt )((ERROR_BASE-8));
		public static readonly FPInt   SEGDATA_ERR                                       = (FPInt )((ERROR_BASE-9));
		public static readonly FPInt   DUPLICATE_FILE_ERR                                = (FPInt )((ERROR_BASE-10));
		public static readonly FPInt   OFFSET_FIELD_ERR                                  = (FPInt )((ERROR_BASE-11));
		public static readonly FPInt   OPERATION_NOT_SUPPORTED                           = (FPInt )((ERROR_BASE-12));
		public static readonly FPInt   ACK_NOT_RCV_ERR                                   = (FPInt )((ERROR_BASE-13));
		public static readonly FPInt   FILE_NOT_STORED_ERR                               = (FPInt )((ERROR_BASE-14));
		public static readonly FPInt   NUMLOC_FIELD_ERR                                  = (FPInt )((ERROR_BASE-15));
		public static readonly FPInt   SECTION_NOT_FOUND_ERR                             = (FPInt )((ERROR_BASE-16));
		public static readonly FPInt   TAG_NOT_FOUND_ERR                                 = (FPInt )((ERROR_BASE-17));
		public static readonly FPInt   ATTR_NOT_FOUND_ERR                                = (FPInt )((ERROR_BASE-18));
		public static readonly FPInt   WRONG_REFERENCE_ERR                               = (FPInt )((ERROR_BASE-19));
		public static readonly FPInt   NO_POOL_ERR                                       = (FPInt )((ERROR_BASE-20));
		public static readonly FPInt   CLIP_NOT_FOUND_ERR                                = (FPInt )((ERROR_BASE-21));
		public static readonly FPInt   TAGTREE_ERR                                       = (FPInt )((ERROR_BASE-22));
		public static readonly FPInt   ISNOT_DIRECTORY_ERR                               = (FPInt )((ERROR_BASE-23));
		public static readonly FPInt   UNEXPECTEDTAG_ERR                                 = (FPInt )((ERROR_BASE-24));
		public static readonly FPInt   TAG_READONLY_ERR                                  = (FPInt )((ERROR_BASE-25));
		public static readonly FPInt   OUT_OF_BOUNDS_ERR                                 = (FPInt )((ERROR_BASE-26));
		public static readonly FPInt   FILESYS_ERR                                       = (FPInt )((ERROR_BASE-27));
		public static readonly FPInt   STACK_DEPTH_ERR                                   = (FPInt )((ERROR_BASE-29));
		public static readonly FPInt   TAG_HAS_NO_DATA_ERR                               = (FPInt )((ERROR_BASE-30));
		public static readonly FPInt   VERSION_ERR                                       = (FPInt )((ERROR_BASE-31));
		public static readonly FPInt   MULTI_BLOB_ERR                                    = (FPInt )((ERROR_BASE-32));
		public static readonly FPInt   PROTOCOL_ERR                                      = (FPInt )((ERROR_BASE-33));
		public static readonly FPInt   NO_SOCKET_AVAIL_ERR                               = (FPInt )((ERROR_BASE-34));
		public static readonly FPInt   BLOBIDFIELD_ERR                                   = (FPInt )((ERROR_BASE-35));
		public static readonly FPInt   BLOBIDMISMATCH_ERR                                = (FPInt )((ERROR_BASE-36));
		public static readonly FPInt   PROBEPACKET_ERR                                   = (FPInt )((ERROR_BASE-37));
		public static readonly FPInt   CLIPCLOSED_ERR                                    = (FPInt )((ERROR_BASE-38));
		public static readonly FPInt   POOLCLOSED_ERR                                    = (FPInt )((ERROR_BASE-39));
		public static readonly FPInt   BLOBBUSY_ERR                                      = (FPInt )((ERROR_BASE-40));
		public static readonly FPInt   SERVER_NOTREADY_ERR                               = (FPInt )((ERROR_BASE-41));
		public static readonly FPInt   SERVER_NO_CAPACITY_ERR                            = (FPInt )((ERROR_BASE-42));
		public static readonly FPInt   NETWORK_ERROR_BASE                                = (FPInt )(-10100);
		public static readonly FPInt   SOCKET_ERR                                        = (FPInt )((NETWORK_ERROR_BASE-1));
		public static readonly FPInt   PACKETDATA_ERR                                    = (FPInt )((NETWORK_ERROR_BASE-2));
		public static readonly FPInt   ACCESSNODE_ERR                                    = (FPInt )((NETWORK_ERROR_BASE-3));
		public static readonly FPInt   SERVER_ERROR_BASE                                 = (FPInt )(-10150);
		public static readonly FPInt   OPCODE_FIELD_ERR                                  = (FPInt )((SERVER_ERROR_BASE-1));
		public static readonly FPInt   PACKET_FIELD_MISSING_ERR                          = (FPInt )((SERVER_ERROR_BASE-2));
		public static readonly FPInt   AUTHENTICATION_FAILED_ERR                         = (FPInt )((SERVER_ERROR_BASE-3));
		public static readonly FPInt   UNKNOWN_AUTH_SCHEME_ERR                           = (FPInt )((SERVER_ERROR_BASE-4));
		public static readonly FPInt   UNKNOWN_AUTH_PROTOCOL_ERR                         = (FPInt )((SERVER_ERROR_BASE-5));
		public static readonly FPInt   TRANSACTION_FAILED_ERR                            = (FPInt )((SERVER_ERROR_BASE-6));
		public static readonly FPInt   PROFILECLIPID_NOTFOUND_ERR                        = (FPInt )((SERVER_ERROR_BASE-7));
		public static readonly FPInt   CLIENT_ERROR_BASE                                 = (FPInt )(-10200);
		public static readonly FPInt   OPERATION_REQUIRES_MARK                           = (FPInt )((CLIENT_ERROR_BASE-1));
		public static readonly FPInt   QUERYCLOSED_ERR                                   = (FPInt )((CLIENT_ERROR_BASE-2));
		public static readonly FPInt   WRONG_STREAM_ERR                                  = (FPInt )((CLIENT_ERROR_BASE-3));
		public static readonly FPInt   OPERATION_NOT_ALLOWED                             = (FPInt )((CLIENT_ERROR_BASE-4));
		public static readonly FPInt   SDK_INTERNAL_ERR                                  = (FPInt )((CLIENT_ERROR_BASE-5));
		public static readonly FPInt   OUT_OF_MEMORY_ERR                                 = (FPInt )((CLIENT_ERROR_BASE-6));
		public static readonly FPInt   OBJECTINUSE_ERR                                   = (FPInt )((CLIENT_ERROR_BASE-7));
		public static readonly FPInt   NOTYET_OPEN_ERR                                   = (FPInt )((CLIENT_ERROR_BASE-8));
		public static readonly FPInt   STREAM_ERR                                        = (FPInt )((CLIENT_ERROR_BASE-9));
		public static readonly FPInt   TAGCLOSED_ERR                                     = (FPInt )((CLIENT_ERROR_BASE-10));
		public static readonly FPInt   THREAD_ERR                                        = (FPInt )((CLIENT_ERROR_BASE-11));
		public static readonly FPInt   PROBE_TIME_EXPIRED_ERR                            = (FPInt )((CLIENT_ERROR_BASE-12));
		public static readonly FPInt   PROFILECLIPID_WRITE_ERR                           = (FPInt )((CLIENT_ERROR_BASE-13));
		public static readonly FPInt   INVALID_XML_ERR                                   = (FPInt )((CLIENT_ERROR_BASE-14));
		public static readonly FPInt   UNABLE_TO_GET_LAST_ERROR                          = (FPInt )((CLIENT_ERROR_BASE-15));
		public static readonly FPInt   UNDEFINED_ERROR_BASE                              = (FPInt )(-10250);
		public static readonly FPInt   ZOS_ERROR_BASE                                    = (FPInt )(-20000);
		public static readonly FPInt   SUBTASK_TCB_NOT_FOUND                             = (FPInt )((ZOS_ERROR_BASE-1));
		public static readonly FPInt   INVALID_FUNCTION_ID                               = (FPInt )((ZOS_ERROR_BASE-2));
		public static readonly FPInt   COMMAREA_MALLOC_ERR                               = (FPInt )((ZOS_ERROR_BASE-3));
		public static readonly FPInt   ZOS_SUBTASK_ABEND                                 = (FPInt )((ZOS_ERROR_BASE-4));
		public static readonly FPInt   ZOS_SUBTASK_UNKN_RC                               = (FPInt )((ZOS_ERROR_BASE-5));
		public static readonly FPInt   ZOS_IEANTCR_BASE                                  = (FPInt )(-20100);
		public static readonly FPInt   ZOS_IEANTDL_BASE                                  = (FPInt )(-20200);
		public static readonly FPInt   ZOS_IEANTRT_BASE                                  = (FPInt )(-20300);
		public static readonly FPInt   STREAMINFO_VERSION                                = (FPInt )(3);
		public static readonly FPInt   EMBEDDED_DATA_MAX_SIZE							 = (FPInt) 102400;
		public static readonly FPInt   FULL_CLUSTER_LAYOUT								 = (FPInt) 0;
		public static readonly FPInt   PARTIAL_CLUSTER_LAYOUT							 = (FPInt) 1;
		public static readonly FPInt   NO_CLUSTER_LAYOUT								 = (FPInt) 2;
		public static readonly String  POOLMAPPINGS										 = "poolmappings";
		public static readonly String  COMPLIANCE										 = "compliance";
		public static readonly String  POOLS											 = "pools";
		public static readonly String  PROFILES											 = "profiles";
		public static readonly String  MODE												 = "mode";
		public static readonly String  EVENT_BASED_RETENTION							 = "ebr";
		public static readonly String  RETENTION_HOLD									 = "retention-hold";
		public static readonly String  FIXED_RETENTION_MIN								 = "fixedminimum";
		public static readonly String  FIXED_RETENTION_MAX								 = "fixedmaximum";
		public static readonly String  VARIABLE_RETENTION_MIN							 = "variableminimum";
		public static readonly String  VARIABLE_RETENTION_MAX							 = "variablemaximum";
		public static readonly String  RETENTION_DEFAULT								 = "default";
		public static readonly String  RETENTION_MIN_MAX								 = "min-max";
		public static readonly FPInt   OPTION_SECONDS_STRING							 = (FPInt) 0;
		public static readonly FPInt   OPTION_MILLISECONDS_STRING						 = (FPInt) 1;
        public static readonly String  OPTION_STREAM_STRICT_MODE                         = "FP_OPTION_STREAM_STRICT_MODE";
        public static readonly FPInt   STREAM_EOF                                        = (FPInt) (-1);


		public static FPLong FPTime_StringToLong (String inClusterTime)
		{
			FPLong retval = SDK.FPTime_StringToLong8(inClusterTime);
			SDK.CheckAndThrowError();
			return retval;
		}

		public static void FPTime_LongToString (FPLong inTime, StringBuilder outClusterTime,  ref FPInt ioClusterTimeLen)
		{
			SDK.FPTime_LongToString8(inTime, outClusterTime, ref ioClusterTimeLen);
			SDK.CheckAndThrowError();
		}

		public static FPLong FPTime_StringToSeconds (String inClusterTime)
		{
			FPLong retval = SDK.FPTime_StringToSeconds8(inClusterTime);
			SDK.CheckAndThrowError();
			return retval;
		}

		public static void FPTime_SecondsToString (FPLong inTime, StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions)
		{
			SDK.FPTime_SecondsToString8(inTime, outClusterTime, ref ioClusterTimeLen, inOptions);
			SDK.CheckAndThrowError();
		}
		
		public static FPLong FPTime_StringToMilliseconds (String inClusterTime)
		{
			FPLong retval = SDK.FPTime_StringToMilliseconds8(inClusterTime);
			SDK.CheckAndThrowError();
			return retval;
		}
		
		public static void FPTime_MillisecondsToString (FPLong inTime, StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions)
		{
			SDK.FPTime_MillisecondsToString8(inTime, outClusterTime, ref ioClusterTimeLen, inOptions);
			SDK.CheckAndThrowError();
		}

	}

    public class MarshalPtrToUtf8 : ICustomMarshaler
    {
        static MarshalPtrToUtf8 marshaler = new MarshalPtrToUtf8();

        public void CleanUpManagedData(object ManagedObj)
        {

        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public int GetNativeDataSize()
        {
            return Marshal.SizeOf(typeof(byte));
        }

        public int GetNativeDataSize(IntPtr ptr)
        {
            int size = 0;
            for (size = 0; Marshal.ReadByte(ptr, size) > 0; size++)
                ;
            return size;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            if (ManagedObj == null)
                return IntPtr.Zero;

            if (ManagedObj.GetType() != typeof(string))
                throw new ArgumentException("CustomMarshal class MarshalPtrToUtf8 only works with System.String variables");

            byte[] array = Encoding.UTF8.GetBytes((string)ManagedObj);
            int size = Marshal.SizeOf(array[0]) * array.Length + Marshal.SizeOf(array[0]);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, array.Length);
            Marshal.WriteByte(ptr, size - 1, 0);
            return ptr;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return null;
            int size = GetNativeDataSize(pNativeData);
            byte[] array = new byte[size];
            Marshal.Copy(pNativeData, array, 0, size);
            return Encoding.UTF8.GetString(array);
        }

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return marshaler;
        }
    }

    internal sealed class SDK 
	{
        // These calls need special marshalling as they input AND output UTF-8 strings that may include "foreign" characters
        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetDescriptionAttribute8(FPClipRef inClip,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName,
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
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName,
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
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] outAttrValue,
            ref FPInt ioAttrValueLen);

        // These ones also use UTF-8 strings for input and output but will return standard strings from the cluster i.e. no "foreign" characters
        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_GetCapability8(FPPoolRef inPool,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inCapabilityName,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inCapabilityAttributeName,
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
        public static extern FPPoolRef FPPool_Open8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inPoolAddress);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_SetIntOption8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inOptionName, FPInt inOptionValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPPool_GetIntOption8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inOptionName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_SetGlobalOption8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inOptionName, FPInt inOptionValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPInt FPPool_GetGlobalOption8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inOptionName);

        [DllImport("FPLibrary.dll")]
        public static extern FPClipRef FPClip_Create8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_AuditedDelete8(FPPoolRef inPool, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClipID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inReason, FPLong inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetName8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClipName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetCreationDate8(FPClipRef inClip, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outDate, ref FPInt ioDateLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetDescriptionAttribute8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_RemoveDescriptionAttribute8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern FPTagRef FPTag_Create8(FPTagRef inParent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetStringAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetLongAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName, FPLong inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_SetBoolAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName, FPBool inAttrValue);

        [DllImport("FPLibrary.dll")]
        public static extern FPRetentionClassRef FPRetentionClassContext_GetNamedClass8(FPRetentionClassContextRef inContextRef,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inName);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTag_GetLongAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPTag_GetBoolAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTag_RemoveAttribute8(FPTagRef inTag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inAttrName);


        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_SelectField8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern void FPQueryExpression_DeselectField8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPQueryExpression_IsFieldSelected8(FPQueryExpressionRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inFieldName);

        [DllImport("FPLibrary.dll")]
        public static extern FPQueryRef FPQuery_Open8(FPPoolRef inPool, FPLong inStartTime, FPLong inStopTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inReserved);

        [DllImport("FPLibrary.dll")]
        public static extern FPMonitorRef FPMonitor_Open8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClusterAddress);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateFileForInput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pPerm, long pBuffSize);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreatePartialFileForInput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pPerm, long pBuffSize, long pOffset, long pLength);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreateFileForOutput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pPerm);

        [DllImport("FPLibrary.dll")]
        public static extern FPStreamRef FPStream_CreatePartialFileForOutput8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pFilePath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String pPerm, long pBuffSize, long pOffset, long pLength, long pMaxFileLength);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToLong8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_LongToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen);

        [DllImport("FPLibrary.dll")]
        public static extern void FPPool_RegisterApplication8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String appName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String appVersion);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_SetRetentionHold8(FPClipRef inClip, FPBool inHoldFlag, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inHoldID);

        [DllImport("FPLibrary.dll")]
        public static extern String FPClip_GetEBRClassName8(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_GetEBREventTime8(FPClipRef inClip, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outEBREventTime, ref FPInt ioEBREventTimeLen);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToSeconds8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_SecondsToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern FPLong FPTime_StringToMilliseconds8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inClusterTime);

        [DllImport("FPLibrary.dll")]
        public static extern void FPTime_MillisecondsToString8(FPLong inTime, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] StringBuilder outClusterTime, ref FPInt ioClusterTimeLen, FPInt inOptions);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_SetLogPath8(FPLogStateRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inPath);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogState_Save8(FPLogStateRef inRef, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inPath);

        [DllImport("FPLibrary.dll")]
        public static extern void FPLogging_Log8(FPLogLevel inLevel, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inMessage);

        [DllImport("FPLibrary.dll")]
        public static extern FPLogStateRef FPLogging_OpenLogState8([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MarshalPtrToUtf8))] String inPathName);

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
        public static extern FPClipRef FPClip_Open(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inClipID, FPInt inOpenMode);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Close(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPPoolRef FPClip_GetPoolRef(FPClipRef inClip);

        [DllImport("FPLibrary.dll")]
        public static extern FPBool FPClip_Exists(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Delete(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inClipID);

        [DllImport("FPLibrary.dll")]
        public static extern void FPClip_Purge(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inClipID);

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
        public static extern FPClipRef FPClip_RawOpen(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inClipID, FPStreamRef inStream, FPLong inOptions);

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
        public static extern void FPPool_SetClipID(FPPoolRef inPool, [MarshalAs(UnmanagedType.LPStr)] String inContentAddress);

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
        public static extern void FPClip_GetCanonicalFormat([MarshalAs(UnmanagedType.LPStr)] String inClipID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 40, ArraySubType = UnmanagedType.U1)] byte[] outClipID);

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

        /*
        * Generic error checking routine for transforming SDK errors into .NET exceptions
        */
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


	}; // end class SDK 


	public class Clip 
	{

		public static FPClipRef Create(FPPoolRef inPool,  String inName) 
		{
			FPClipRef retval = SDK.FPClip_Create8(inPool, inName);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPClipRef Open(FPPoolRef inPool,  String inClipID, FPInt inOpenMode) 
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
		public static FPBool Exists(FPPoolRef inPool,  String inClipID) 
		{
			FPBool retval = SDK.FPClip_Exists(inPool, inClipID);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Delete(FPPoolRef inPool,  String inClipID) 
		{
			SDK.FPClip_Delete(inPool, inClipID);
			SDK.CheckAndThrowError();
		}
		public static void AuditedDelete(FPPoolRef inPool,  String inClipID,  String inReason, FPLong inOptions) 
		{
			SDK.FPClip_AuditedDelete8(inPool, inClipID, inReason, inOptions);
			SDK.CheckAndThrowError();
		}
		public static void Purge(FPPoolRef inPool,  String inClipID) 
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
		public static void SetName(FPClipRef inClip,  String inClipName) 
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
		public static FPClipRef RawOpen(FPPoolRef inPool,  String inClipID, FPStreamRef inStream, FPLong inOptions) 
		{
			FPClipRef retval = SDK.FPClip_RawOpen(inPool, inClipID, inStream, inOptions);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void SetDescriptionAttribute(FPClipRef inClip,  String inAttrName,  String inAttrValue) 
		{
			SDK.FPClip_SetDescriptionAttribute8(inClip, inAttrName, inAttrValue);
			SDK.CheckAndThrowError();
		}
		public static void RemoveDescriptionAttribute(FPClipRef inClip,  String inAttrName) 
		{
			SDK.FPClip_RemoveDescriptionAttribute8(inClip, inAttrName);
			SDK.CheckAndThrowError();
		}
		public static void GetDescriptionAttribute(FPClipRef inClip,  String inAttrName,  ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
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
		public static void GetCanonicalFormat( String inClipID,  byte[] outClipID) 
		{
			SDK.FPClip_GetCanonicalFormat(inClipID, outClipID);
			SDK.CheckAndThrowError();
		}
		public static void GetStringFormat( byte[] inClipID,  StringBuilder outClipID) 
		{
			SDK.FPClip_GetStringFormat(inClipID, outClipID);
			SDK.CheckAndThrowError();
		}

		public static void SetRetentionHold (FPClipRef inClip, FPBool inHoldFlag, String inHoldID)
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

		public static String GetEBRClassName (FPClipRef inClip)
		{
			String retval = SDK.FPClip_GetEBRClassName8(inClip);
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

	}  // end of class Clip



	public class EventCallback 
	{

		public static FPEventCallbackRef RegisterForAllEvents(FPMonitorRef inMonitor, FPStreamRef inStream) 
		{
			FPEventCallbackRef retval = SDK.FPEventCallback_RegisterForAllEvents(inMonitor, inStream);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPEventCallbackRef inRegisterRef) 
		{
			SDK.FPEventCallback_Close(inRegisterRef);
			SDK.CheckAndThrowError();
		}

	}  // end of class EventCallback



	public class Monitor 
	{

		public static FPMonitorRef Open( String inClusterAddress) 
		{
			FPMonitorRef retval = SDK.FPMonitor_Open8(inClusterAddress);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPMonitorRef inMonitor) 
		{
			SDK.FPMonitor_Close(inMonitor);
			SDK.CheckAndThrowError();
		}
		public static void GetDiscovery(FPMonitorRef inMonitor,  StringBuilder outData, ref FPInt ioDataLen) 
		{
			SDK.FPMonitor_GetDiscovery(inMonitor, outData, ref ioDataLen);
			SDK.CheckAndThrowError();
		}
		public static void GetDiscoveryStream(FPMonitorRef inMonitor, FPStreamRef inStream) 
		{
			SDK.FPMonitor_GetDiscoveryStream(inMonitor, inStream);
			SDK.CheckAndThrowError();
		}
		public static void GetAllStatistics(FPMonitorRef inMonitor,  StringBuilder outData, ref FPInt ioDataLen) 
		{
			SDK.FPMonitor_GetAllStatistics(inMonitor, outData, ref ioDataLen);
			SDK.CheckAndThrowError();
		}
		public static void GetAllStatisticsStream(FPMonitorRef inMonitor, FPStreamRef inStream) 
		{
			SDK.FPMonitor_GetAllStatisticsStream(inMonitor, inStream);
			SDK.CheckAndThrowError();
		}

	}  // end of class Monitor



	public class Pool 
	{

		public static FPPoolRef Open( String inPoolAddress) 
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
		public static void SetIntOption(FPPoolRef inPool,  String inOptionName, FPInt inOptionValue) 
		{
			SDK.FPPool_SetIntOption8(inPool, inOptionName, inOptionValue);
			SDK.CheckAndThrowError();
		}
		public static FPInt GetIntOption(FPPoolRef inPool,  String inOptionName) 
		{
			FPInt retval = SDK.FPPool_GetIntOption8(inPool, inOptionName);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void SetGlobalOption( String inOptionName, FPInt inOptionValue) 
		{
			SDK.FPPool_SetGlobalOption8(inOptionName, inOptionValue);
			SDK.CheckAndThrowError();
		}
		public static FPInt GetGlobalOption( String inOptionName) 
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
		public static void GetCapability(FPPoolRef inPool,  String inCapabilityName,  String inCapabilityAttributeName,  StringBuilder outCapabilityValue, ref FPInt ioCapabilityValueLen) 
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
		public static void SetClipID(FPPoolRef inPool,  String inContentAddress) 
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

		public static void RegisterApplication(String appName, String appVersion)
		{
			SDK.FPPool_RegisterApplication8(appName, appVersion);
			SDK.CheckAndThrowError();
		}

	}  // end of class Pool



	public class PoolQuery 
	{

		public static FPPoolQueryRef Open(FPPoolRef inPoolRef, FPQueryExpressionRef inQueryExpressionRef) 
		{
			FPPoolQueryRef retval = SDK.FPPoolQuery_Open(inPoolRef, inQueryExpressionRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPPoolQueryRef inPoolQueryRef) 
		{
			SDK.FPPoolQuery_Close(inPoolQueryRef);
			SDK.CheckAndThrowError();
		}
		public static FPPoolRef GetPoolRef(FPPoolQueryRef inPoolQueryRef) 
		{
			FPPoolRef retval = SDK.FPPoolQuery_GetPoolRef(inPoolQueryRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPQueryResultRef FetchResult(FPPoolQueryRef inPoolQueryRef, FPInt inTimeout) 
		{
			FPQueryResultRef retval = SDK.FPPoolQuery_FetchResult(inPoolQueryRef, inTimeout);
			SDK.CheckAndThrowError();
			return retval;
		}

	}  // end of class PoolQuery



	public class Query 
	{

		public static FPQueryRef Open(FPPoolRef inPool, FPLong inStartTime, FPLong inStopTime,  String inReserved) 
		{
			FPQueryRef retval = SDK.FPQuery_Open8(inPool, inStartTime, inStopTime, inReserved);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPInt FetchResult(FPQueryRef inQuery,  StringBuilder outResultClip, ref FPLong outTimestamp, FPInt inTimeout) 
		{
			FPInt retval = SDK.FPQuery_FetchResult(inQuery, outResultClip, ref outTimestamp, inTimeout);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPQueryRef inQuery) 
		{
			SDK.FPQuery_Close(inQuery);
			SDK.CheckAndThrowError();
		}
		public static FPPoolRef GetPoolRef(FPQueryRef inQuery) 
		{
			FPPoolRef retval = SDK.FPQuery_GetPoolRef(inQuery);
			SDK.CheckAndThrowError();
			return retval;
		}

	}  // end of class Query



	public class QueryExpression 
	{

		public static FPQueryExpressionRef Create() 
		{
			FPQueryExpressionRef retval = SDK.FPQueryExpression_Create();
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPQueryExpressionRef inRef) 
		{
			SDK.FPQueryExpression_Close(inRef);
			SDK.CheckAndThrowError();
		}
		public static void SetStartTime(FPQueryExpressionRef inRef, FPLong inTime) 
		{
			SDK.FPQueryExpression_SetStartTime(inRef, inTime);
			SDK.CheckAndThrowError();
		}
		public static void SetEndTime(FPQueryExpressionRef inRef, FPLong inTime) 
		{
			SDK.FPQueryExpression_SetEndTime(inRef, inTime);
			SDK.CheckAndThrowError();
		}
		public static void SetType(FPQueryExpressionRef inRef, FPInt inType) 
		{
			SDK.FPQueryExpression_SetType(inRef, inType);
			SDK.CheckAndThrowError();
		}
		public static FPLong GetStartTime(FPQueryExpressionRef inRef) 
		{
			FPLong retval = SDK.FPQueryExpression_GetStartTime(inRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPLong GetEndTime(FPQueryExpressionRef inRef) 
		{
			FPLong retval = SDK.FPQueryExpression_GetEndTime(inRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPInt GetType(FPQueryExpressionRef inRef) 
		{
			FPInt retval = SDK.FPQueryExpression_GetType(inRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void SelectField(FPQueryExpressionRef inRef,  String inFieldName) 
		{
			SDK.FPQueryExpression_SelectField8(inRef, inFieldName);
			SDK.CheckAndThrowError();
		}
		public static void DeselectField(FPQueryExpressionRef inRef,  String inFieldName) 
		{
			SDK.FPQueryExpression_DeselectField8(inRef, inFieldName);
			SDK.CheckAndThrowError();
		}
		public static FPBool IsFieldSelected(FPQueryExpressionRef inRef,  String inFieldName) 
		{
			FPBool retval = SDK.FPQueryExpression_IsFieldSelected8(inRef, inFieldName);
			SDK.CheckAndThrowError();
			return retval;
		}

	}  // end of class QueryExpression



	public class QueryResult 
	{

		public static void Close(FPQueryResultRef inQueryResultRef) 
		{
			SDK.FPQueryResult_Close(inQueryResultRef);
			SDK.CheckAndThrowError();
		}
		public static void GetClipID(FPQueryResultRef inQueryResultRef,  StringBuilder outClipID) 
		{
			SDK.FPQueryResult_GetClipID(inQueryResultRef, outClipID);
			SDK.CheckAndThrowError();
		}
		public static FPLong GetTimestamp(FPQueryResultRef inQueryResultRef) 
		{
			FPLong retval = SDK.FPQueryResult_GetTimestamp(inQueryResultRef);
			SDK.CheckAndThrowError();
			return retval;
		}
        public static void GetField(FPQueryResultRef inQueryResultRef, String inAttrName, ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
		{
			SDK.FPQueryResult_GetField8(inQueryResultRef, inAttrName, outAttrValue, ref ioAttrValueLen);
			SDK.CheckAndThrowError();
		}
		public static FPInt GetResultCode(FPQueryResultRef inQueryResultRef) 
		{
			FPInt retval = SDK.FPQueryResult_GetResultCode(inQueryResultRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPInt GetType(FPQueryResultRef inQueryResultRef) 
		{
			FPInt retval = SDK.FPQueryResult_GetType(inQueryResultRef);
			SDK.CheckAndThrowError();
			return retval;
		}

	}  // end of class QueryResult



	public class RetentionClass 
	{

        public static void GetName(FPRetentionClassRef inClassRef, ref byte[] outName, ref FPInt ioNameLen) 
		{
			SDK.FPRetentionClass_GetName8(inClassRef, outName, ref ioNameLen);
			SDK.CheckAndThrowError();
		}
		public static FPLong GetPeriod(FPRetentionClassRef inClassRef) 
		{
			FPLong retval = SDK.FPRetentionClass_GetPeriod(inClassRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPRetentionClassRef inClassRef) 
		{
			SDK.FPRetentionClass_Close(inClassRef);
			SDK.CheckAndThrowError();
		}

	}  // end of class RetentionClass



	public class RetentionClassContext 
	{

		public static void Close(FPRetentionClassContextRef inContextRef) 
		{
			SDK.FPRetentionClassContext_Close(inContextRef);
			SDK.CheckAndThrowError();
		}
		public static FPInt GetNumClasses(FPRetentionClassContextRef inContextRef) 
		{
			FPInt retval = SDK.FPRetentionClassContext_GetNumClasses(inContextRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPRetentionClassRef GetFirstClass(FPRetentionClassContextRef inContextRef) 
		{
			FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetFirstClass(inContextRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPRetentionClassRef GetLastClass(FPRetentionClassContextRef inContextRef) 
		{
			FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetLastClass(inContextRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPRetentionClassRef GetNextClass(FPRetentionClassContextRef inContextRef) 
		{
			FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetNextClass(inContextRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPRetentionClassRef GetPreviousClass(FPRetentionClassContextRef inContextRef) 
		{
			FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetPreviousClass(inContextRef);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPRetentionClassRef GetNamedClass(FPRetentionClassContextRef inContextRef,  String inName) 
		{
			FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetNamedClass8(inContextRef, inName);
			SDK.CheckAndThrowError();
			return retval;
		}

	}  // end of class RetentionClassContext



	public class Stream 
	{

		public static FPStreamRef CreateFileForInput( String pFilePath,  String pPerm, long pBuffSize) 
		{
			FPStreamRef retval = SDK.FPStream_CreateFileForInput8(pFilePath, pPerm, pBuffSize);
			SDK.CheckAndThrowError();
			return retval;
		}
        public static FPStreamRef CreatePartialFileForInput(String pFilePath, String pPerm, long pBuffSize, long pOffset, long pSize)
        {
            FPStreamRef retval = SDK.FPStream_CreatePartialFileForInput8(pFilePath, pPerm, pBuffSize, pOffset, pSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateFileForOutput(String pFilePath, String pPerm) 
		{
			FPStreamRef retval = SDK.FPStream_CreateFileForOutput8(pFilePath, pPerm);
			SDK.CheckAndThrowError();
			return retval;
		}
        public static FPStreamRef CreatePartialFileForOutput(String pFilePath, String pPerm, long pBuffSize, long pOffset, long pSize, long pMaxFileSize)
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
	}  // end of class Stream

	public class Tag 
	{

		public static FPTagRef Create(FPTagRef inParent,  String inName) 
		{
			FPTagRef retval = SDK.FPTag_Create8(inParent, inName);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Close(FPTagRef inTag) 
		{
			SDK.FPTag_Close(inTag);
			SDK.CheckAndThrowError();
		}
		public static FPTagRef Copy(FPTagRef inTag, FPTagRef inNewParent, FPInt inOptions) 
		{
			FPTagRef retval = SDK.FPTag_Copy(inTag, inNewParent, inOptions);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPClipRef GetClipRef(FPTagRef inTag) 
		{
			FPClipRef retval = SDK.FPTag_GetClipRef(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPTagRef GetSibling(FPTagRef inTag) 
		{
			FPTagRef retval = SDK.FPTag_GetSibling(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPTagRef GetPrevSibling(FPTagRef inTag) 
		{
			FPTagRef retval = SDK.FPTag_GetPrevSibling(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPTagRef GetFirstChild(FPTagRef inTag) 
		{
			FPTagRef retval = SDK.FPTag_GetFirstChild(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPTagRef GetParent(FPTagRef inTag) 
		{
			FPTagRef retval = SDK.FPTag_GetParent(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void Delete(FPTagRef inTag) 
		{
			SDK.FPTag_Delete(inTag);
			SDK.CheckAndThrowError();
		}
        public static void GetTagName(FPTagRef inTag, ref byte[] outName, ref FPInt ioNameLen) 
		{
			SDK.FPTag_GetTagName8(inTag, outName, ref ioNameLen);
			SDK.CheckAndThrowError();
		}
		public static void SetStringAttribute(FPTagRef inTag,  String inAttrName,  String inAttrValue) 
		{
			SDK.FPTag_SetStringAttribute8(inTag, inAttrName, inAttrValue);
			SDK.CheckAndThrowError();
		}
		public static void SetLongAttribute(FPTagRef inTag,  String inAttrName, FPLong inAttrValue) 
		{
			SDK.FPTag_SetLongAttribute8(inTag, inAttrName, inAttrValue);
			SDK.CheckAndThrowError();
		}
		public static void SetBoolAttribute(FPTagRef inTag,  String inAttrName, FPBool inAttrValue) 
		{
			SDK.FPTag_SetBoolAttribute8(inTag, inAttrName, inAttrValue);
			SDK.CheckAndThrowError();
		}
		public static void GetStringAttribute(FPTagRef inTag,  String inAttrName,  ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
		{
			SDK.FPTag_GetStringAttribute8(inTag, inAttrName, outAttrValue, ref ioAttrValueLen);
			SDK.CheckAndThrowError();
		}
		public static FPLong GetLongAttribute(FPTagRef inTag,  String inAttrName) 
		{
			FPLong retval = SDK.FPTag_GetLongAttribute8(inTag, inAttrName);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static FPBool GetBoolAttribute(FPTagRef inTag,  String inAttrName) 
		{
			FPBool retval = SDK.FPTag_GetBoolAttribute8(inTag, inAttrName);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void RemoveAttribute(FPTagRef inTag,  String inAttrName) 
		{
			SDK.FPTag_RemoveAttribute8(inTag, inAttrName);
			SDK.CheckAndThrowError();
		}
		public static FPInt GetNumAttributes(FPTagRef inTag) 
		{
			FPInt retval = SDK.FPTag_GetNumAttributes(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
        public static void GetIndexAttribute(FPTagRef inTag, FPInt inIndex, ref byte[] outAttrName, ref FPInt ioAttrNameLen, ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
		{
			SDK.FPTag_GetIndexAttribute8(inTag, inIndex, outAttrName, ref ioAttrNameLen, outAttrValue, ref ioAttrValueLen);
			SDK.CheckAndThrowError();
		}
		public static FPLong GetBlobSize(FPTagRef inTag) 
		{
			FPLong retval = SDK.FPTag_GetBlobSize(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}
		public static void BlobWrite(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions) 
		{
			SDK.FPTag_BlobWrite(inTag, inStream, inOptions);
			SDK.CheckAndThrowError();
		}
		public static void BlobRead(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions) 
		{
			SDK.FPTag_BlobRead(inTag, inStream, inOptions);
			SDK.CheckAndThrowError();
		}
		public static void BlobReadPartial(FPTagRef inTag, FPStreamRef inStream, FPLong inOffset, FPLong inReadLength, FPLong inOptions) 
		{
			SDK.FPTag_BlobReadPartial(inTag, inStream, inOffset, inReadLength, inOptions);
			SDK.CheckAndThrowError();
		}
		public static void BlobPurge(FPTagRef inTag) 
		{
			SDK.FPTag_BlobPurge(inTag);
			SDK.CheckAndThrowError();
		}
		public static FPInt BlobExists(FPTagRef inTag) 
		{
			FPInt retval = SDK.FPTag_BlobExists(inTag);
			SDK.CheckAndThrowError();
			return retval;
		}


		public static void BlobWritePartial (FPTagRef inTag, FPStreamRef inStream, FPLong inOptions, FPLong inSequenceID)
		{
			SDK.FPTag_BlobWritePartial (inTag, inStream, inOptions, inSequenceID);
			SDK.CheckAndThrowError();
		}


	}  // end of class Tag
    
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

        public static String GetLogPath(FPLogStateRef inRef)
        {
            String retval = SDK.FPLogState_GetLogPath(inRef);
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

        public static void SetLogPath(FPLogStateRef inRef, String inValue)
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

        public static void Save(FPLogStateRef inRef, String inValue)
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

        public static void Log(FPLogLevel inLevel, String inMessage)
        {
            SDK.FPLogging_Log8(inLevel, inMessage);
            SDK.CheckAndThrowError();
        }

        public static FPLogStateRef OpenLogState(String inName)
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


    }  // end of class Logging
     
}

