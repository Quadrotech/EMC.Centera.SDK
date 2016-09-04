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
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
	/// <summary>
	/// EMC.Centera.SDK.Native is a wrapper class for the Centera SDK DLL
	/// </summary>
	/** The structure that will store the pool information that
			FPPool_GetPoolInfo() retrieves.
		  */
	public class FP 
	{
		public static readonly string  OPTION_BUFFERSIZE                                 = "buffersize";
		public static readonly string  OPTION_TIMEOUT                                    = "timeout";
		public static readonly string  OPTION_RETRYCOUNT                                 = "retrycount";
		public static readonly string  OPTION_RETRYSLEEP                                 = "retrysleep";
		public static readonly string  OPTION_MAXCONNECTIONS                             = "maxconnections";
		public static readonly string  OPTION_ENABLE_MULTICLUSTER_FAILOVER               = "multiclusterfailover";
		public static readonly string  OPTION_DEFAULT_COLLISION_AVOIDANCE                = "collisionavoidance";
		public static readonly string  OPTION_PREFETCH_SIZE                              = "prefetchsize";
		public static readonly string  OPTION_CLUSTER_NON_AVAIL_TIME                     = "clusternonavailtime";
		public static readonly string  OPTION_PROBE_LIMIT                                = "probetimelimit";
		public static readonly string  OPTION_EMBEDDED_DATA_THRESHOLD                    = "embedding_threshold";
		public static readonly string  OPTION_OPENSTRATEGY                               = "openstrategy";
		public static readonly string  OPTION_ATTRIBUTE_LIMIT                            = "attributelimit";
		public static readonly string  OPTION_MULTICLUSTER_READ_STRATEGY                 = "multicluster_read_strategy";
		public static readonly string  OPTION_MULTICLUSTER_WRITE_STRATEGY                = "multicluster_write_strategy";
		public static readonly string  OPTION_MULTICLUSTER_DELETE_STRATEGY               = "multicluster_delete_strategy";
		public static readonly string  OPTION_MULTICLUSTER_EXISTS_STRATEGY               = "multicluster_exists_strategy";
		public static readonly string  OPTION_MULTICLUSTER_QUERY_STRATEGY                = "multicluster_query_strategy";
		public static readonly string  OPTION_MULTICLUSTER_READ_CLUSTERS                 = "multicluster_read_clusters";
		public static readonly string  OPTION_MULTICLUSTER_WRITE_CLUSTERS                = "multicluster_write_clusters";
		public static readonly string  OPTION_MULTICLUSTER_DELETE_CLUSTERS               = "multicluster_delete_clusters";
		public static readonly string  OPTION_MULTICLUSTER_EXISTS_CLUSTERS               = "multicluster_exists_clusters";
		public static readonly string  OPTION_MULTICLUSTER_QUERY_CLUSTERS                = "multicluster_query_clusters";
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
		public static readonly string  TRUE                                              = "true";
		public static readonly string  FALSE                                             = "false";
		public static readonly string  READ                                              = "read";
		public static readonly string  WRITE                                             = "write";
		public static readonly string  DELETE                                            = "delete";
		public static readonly string  PURGE                                             = "purge";
		public static readonly string  EXIST                                             = "exist";
		public static readonly string  CLIPENUMERATION                                   = "clip-enumeration";
		public static readonly string  RETENTION                                         = "retention";
		public static readonly string  BLOBNAMING                                        = "blobnaming";
		public static readonly string  MONITOR                                           = "monitor";
		public static readonly string  DELETIONLOGGING                                   = "deletionlogging";
		public static readonly string  PRIVILEGEDDELETE                                  = "privileged-delete";
		public static readonly string  ALLOWED                                           = "allowed";
		public static readonly string  SUPPORTED                                         = "supported";
		public static readonly string  DUPLICATEDETECTION                                = "duplicate-detection";
		public static readonly string  DEFAULT                                           = "default";
		public static readonly string  SUPPORTED_SCHEMES                                 = "supported-schemes";
		public static readonly string  MD5                                               = "MD5";
		public static readonly string  MG                                                = "MG";
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
		public static readonly string  POOLMAPPINGS										 = "poolmappings";
		public static readonly string  COMPLIANCE										 = "compliance";
		public static readonly string  POOLS											 = "pools";
		public static readonly string  PROFILES											 = "profiles";
		public static readonly string  MODE												 = "mode";
		public static readonly string  EVENT_BASED_RETENTION							 = "ebr";
		public static readonly string  RETENTION_HOLD									 = "retention-hold";
		public static readonly string  FIXED_RETENTION_MIN								 = "fixedminimum";
		public static readonly string  FIXED_RETENTION_MAX								 = "fixedmaximum";
		public static readonly string  VARIABLE_RETENTION_MIN							 = "variableminimum";
		public static readonly string  VARIABLE_RETENTION_MAX							 = "variablemaximum";
		public static readonly string  RETENTION_DEFAULT								 = "default";
		public static readonly string  RETENTION_MIN_MAX								 = "min-max";
		public static readonly FPInt   OPTION_SECONDS_STRING							 = (FPInt) 0;
		public static readonly FPInt   OPTION_MILLISECONDS_STRING						 = (FPInt) 1;
        public static readonly string  OPTION_STREAM_STRICT_MODE                         = "FP_OPTION_STREAM_STRICT_MODE";
        public static readonly FPInt   STREAM_EOF                                        = (FPInt) (-1);


		public static FPLong FPTime_StringToLong (string inClusterTime)
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

		public static FPLong FPTime_StringToSeconds (string inClusterTime)
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
		
		public static FPLong FPTime_StringToMilliseconds (string inClusterTime)
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
}