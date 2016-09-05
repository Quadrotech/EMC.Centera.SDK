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
using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    /// <summary> 
    ///Contains definitions of FP constants and useful helper methods.
    ///@author Graham Stuart
    ///@version
     /// </summary>

    public class FPMisc
    {
        internal static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        /// <summary>
        ///Converts a string with format of "YYYY:MM:DD:HH:MM:SS" into
        ///a standard DateTime object. 
        ///
        ///@param s string containing the date to be converted.
        ///@return A DateTime object representing this date.
         /// </summary>
        public static DateTime GetDateTime(string s)
        {
            if (s.Length > 0)
                return new DateTime(int.Parse(s.Substring(0, 4)),  // YYYY
                int.Parse(s.Substring(5, 2)),  // MM
                int.Parse(s.Substring(8, 2)),  // DD
                int.Parse(s.Substring(11, 2)), // HH
                int.Parse(s.Substring(14, 2)), // MM
                int.Parse(s.Substring(17, 2)));// SS
            else
                throw new FPLibraryException("Invalid / null string passed to GetDateTime", OUT_OF_BOUNDS_ERR);
        }

        /// <summary>
        ///Converts an  FPLong into a standard DateTime object 
        ///identified by the provided connect string.
        ///
        ///@param l Number of milliseconds since the Epoch 
        ///@return A DateTime object representing the converted FPLong value.
         /// </summary>
        public static DateTime GetDateTime(FPLong l)
        {
            return Epoch.Add(new TimeSpan((long)l * 10000));
        }

        /// <summary>
        ///Converts a DateTime object to an FPLong time value. 
        ///identified by the provided connect string.
        ///
        ///@param d The DateTime to be converted 
        ///@return An FPLong representing the ocnverted DateTime object.
         /// </summary>
        public static FPLong GetTime(DateTime d)
        {
            return (FPLong)((d.Ticks - Epoch.Ticks) / 10000);
        }

        /// <summary>
        ///Converts a Time (represented by a string value) to a long representation. 
        ///
        ///@param inTime	The string representing the Time. 
        ///@return A long representing the Time value (number of seconds since the UNIX Epoch).
         /// </summary>
        public static long FPTime_StringToLong(string inTime)
        {
            long retval = (long)Native.FP.FPTime_StringToSeconds(inTime);
            return retval;
        }
        /// <summary>
        ///Converts a Time (represented by a string value) to a long representation in Seconds. 
        ///
        ///@param inTime	The string representing the Time. 
        ///@return A long representing the Time value (number of seconds since the UNIX Epoch).
         /// </summary>
        public static long FPTime_StringToSeconds(string inTime)
        {
            return FPTime_StringToLong(inTime);
        }

        /// <summary>
        ///Converts a Time (represented by a string value) to a long representation in Milliseconds. 
        ///
        ///@param inTime	The string representing the Time. 
        ///@return A long representing the Time value (number of milliseconds since the UNIX Epoch).
         /// </summary>
        public static long FPTime_StringToMilliseconds(string inTime)
        {
            long retval = (long)Native.FP.FPTime_StringToMilliseconds(inTime);
            return retval;
        }

        /// <summary>
        ///Converts a Time (represented by a long value) to a string representation. 
        ///
        ///@param inTime	The long representing the Time (number of seconds since the UNIX Epoch). 
        ///@return An string representing the Time value.
         /// </summary>
        public static string FPTime_LongToString(long inTime)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += STRING_BUFFER_SIZE;
                clusterTimeLen += STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                Native.FP.FPTime_LongToString((FPLong)inTime, outClusterTime, ref clusterTimeLen);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

        /// <summary>
        ///Converts a Time (represented by a long value of the number of milliseconds since the UNIX Epoch) to a string representation. 
        ///
        ///@param inTime	The long representing the Time. 
        ///@param inOptions	Allows for inclusion or exclusion of the millisecond element in the returned string.
        ///@return A string representing the Time value.
         /// </summary>
        public static string FPTime_MillisecondsToString(long inTime, FPInt inOptions)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += STRING_BUFFER_SIZE;
                clusterTimeLen += STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                Native.FP.FPTime_MillisecondsToString((FPLong)inTime, outClusterTime, ref clusterTimeLen, inOptions);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

        /// <summary>
        ///Converts a Time (represented by a long valueof the number of seconds since the UNIX Epoch) to a string representation. 
        ///
        ///@param inTime	The long representing the Time.
        ///@param inOptions	Allows for inclusion or exclusion of the millisecond element in the returned string.
        ///@return A string representing the Time value.
         /// </summary>
        public static string FPTime_SecondsToString(long inTime, FPInt inOptions)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += STRING_BUFFER_SIZE;
                clusterTimeLen += STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                Native.FP.FPTime_SecondsToString((FPLong)inTime, outClusterTime, ref clusterTimeLen, inOptions);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

		public const string  OPTION_BUFFERSIZE                                 = "buffersize";
		public const string  OPTION_TIMEOUT                                    = "timeout";
		public const string  OPTION_RETRYCOUNT                                 = "retrycount";
		public const string  OPTION_RETRYSLEEP                                 = "retrysleep";
		public const string  OPTION_MAXCONNECTIONS                             = "maxconnections";
		public const string  OPTION_ENABLE_MULTICLUSTER_FAILOVER               = "multiclusterfailover";
		public const string  OPTION_DEFAULT_COLLISION_AVOIDANCE                = "collisionavoidance";
		public const string  OPTION_PREFETCH_SIZE                              = "prefetchsize";
		public const string  OPTION_CLUSTER_NON_AVAIL_TIME                     = "clusternonavailtime";
		public const string  OPTION_PROBE_LIMIT                                = "probetimelimit";
		public const string  OPTION_EMBEDDED_DATA_THRESHOLD                    = "embedding_threshold";
		public const string  OPTION_OPENSTRATEGY                               = "openstrategy";
		public const string  OPTION_ATTRIBUTE_LIMIT                            = "attributelimit";
		public const string  OPTION_MULTICLUSTER_READ_STRATEGY                 = "multicluster_read_strategy";
		public const string  OPTION_MULTICLUSTER_WRITE_STRATEGY                = "multicluster_write_strategy";
		public const string  OPTION_MULTICLUSTER_DELETE_STRATEGY               = "multicluster_delete_strategy";
		public const string  OPTION_MULTICLUSTER_EXISTS_STRATEGY               = "multicluster_exists_strategy";
		public const string  OPTION_MULTICLUSTER_QUERY_STRATEGY                = "multicluster_query_strategy";
		public const string  OPTION_MULTICLUSTER_READ_CLUSTERS                 = "multicluster_read_clusters";
		public const string  OPTION_MULTICLUSTER_WRITE_CLUSTERS                = "multicluster_write_clusters";
		public const string  OPTION_MULTICLUSTER_DELETE_CLUSTERS               = "multicluster_delete_clusters";
		public const string  OPTION_MULTICLUSTER_EXISTS_CLUSTERS               = "multicluster_exists_clusters";
		public const string  OPTION_MULTICLUSTER_QUERY_CLUSTERS                = "multicluster_query_clusters";
		public const int   NO_STRATEGY                                       = (int )(0);
		public const int   FAILOVER_STRATEGY                                 = (int )(1);
		public const int   REPLICATION_STRATEGY                              = (int )(2);
		public const int   PRIMARY_ONLY                                      = (int )(0);
		public const int   PRIMARY_AND_PRIMARY_REPLICA_CLUSTER_ONLY          = (int )(1);
		public const int   NO_REPLICA_CLUSTERS                               = (int )(2);
		public const int   ALL_CLUSTERS                                      = (int )(3);
		public const int   NORMAL_OPEN                                       = (int )(0);
		public const int   LAZY_OPEN                                         = (int )(1);
		public const int   OPEN_ASTREE                                       = (int )(1);
		public const int   OPEN_FLAT                                         = (int )(2);
		public const int   OPTION_DEFAULT_OPTIONS                            = (int )(0);
		public const long  OPTION_CALCID_MASK                                = (long)(0x0000000F);
		public const long  OPTION_CLIENT_CALCID                              = (long)(0x00000001);
		public const long  OPTION_CLIENT_CALCID_STREAMING                    = (long)(0x00000002);
		public const long  OPTION_SERVER_CALCID_STREAMING                    = (long)(0x00000003);
		public const long  OPTION_CALCID_NOCHECK                             = (long)(0x00000010);
		public const long  OPTION_ENABLE_DUPLICATE_DETECTION                 = (long)(0x00000020);
		public const long  OPTION_ENABLE_COLLISION_AVOIDANCE                 = (long)(0x00000040);
		public const long  OPTION_DISABLE_COLLISION_AVOIDANCE                = (long)(0x00000080);
		public const long  OPTION_EMBED_DATA                                 = (long)(0x00000100);
		public const long  OPTION_LINK_DATA                                  = (long)(0x00000200);
		public const int   OPTION_FULL_CLUSTER_LAYOUT                        = (int )(0);
		public const int   OPTION_PARTIAL_CLUSTER_LAYOUT                     = (int )(1);
		public const int   OPTION_NO_CLUSTER_LAYOUT                          = (int )(2);
		public const int   OPTION_NO_COPY_OPTIONS                            = (int )(0x00);
		public const int   OPTION_COPY_BLOBDATA                              = (int )(0x01);
		public const int   OPTION_COPY_CHILDREN                              = (int )(0x02);
		public const int   OPTION_DELETE_PRIVILEGED                          = (int )(1);
		public const int   POOL_INFO_VERSION                                 = (int )(2);
		public const int   QUERY_RESULT_CODE_OK                              = (int )(0);
		public const int   QUERY_RESULT_CODE_INCOMPLETE                      = (int )(1);
		public const int   QUERY_RESULT_CODE_COMPLETE                        = (int )(2);
		public const int   QUERY_RESULT_CODE_END                             = (int )(3);
		public const int   QUERY_RESULT_CODE_ABORT                           = (int )(4);
		public const int   QUERY_RESULT_CODE_ERROR                           = (int )(-1);
		public const int   QUERY_RESULT_CODE_PROGRESS                        = (int )(99);
		public const int   QUERY_TYPE_EXISTING                               = (int )(0x1);
		public const int   QUERY_TYPE_DELETED                                = (int )(0x2);
		public const long  NO_RETENTION_PERIOD                               = (long)(0);
		public const long  INFINITE_RETENTION_PERIOD                         = (long)(-1);
		public const long  DEFAULT_RETENTION_PERIOD                          = (long)(-2);
		public const int   VERSION_FPLIBRARY_DLL                             = (int )(1);
		public const int   VERSION_FPLIBRARY_JAR                             = (int )(2);
		public const int   NETWORK_ERRORCLASS                                = (int )(1);
		public const int   SERVER_ERRORCLASS                                 = (int )(2);
		public const int   CLIENT_ERRORCLASS                                 = (int )(3);
		public const string  TRUE                                              = "true";
		public const string  FALSE                                             = "false";
		public const string  READ                                              = "read";
		public const string  WRITE                                             = "write";
		public const string  DELETE                                            = "delete";
		public const string  PURGE                                             = "purge";
		public const string  EXIST                                             = "exist";
		public const string  CLIPENUMERATION                                   = "clip-enumeration";
		public const string  RETENTION                                         = "retention";
		public const string  BLOBNAMING                                        = "blobnaming";
		public const string  MONITOR                                           = "monitor";
		public const string  DELETIONLOGGING                                   = "deletionlogging";
		public const string  PRIVILEGEDDELETE                                  = "privileged-delete";
		public const string  ALLOWED                                           = "allowed";
		public const string  SUPPORTED                                         = "supported";
		public const string  DUPLICATEDETECTION                                = "duplicate-detection";
		public const string  DEFAULT                                           = "default";
		public const string  SUPPORTED_SCHEMES                                 = "supported-schemes";
		public const string  MD5                                               = "MD5";
		public const string  MG                                                = "MG";
		public const int   ERROR_BASE                                        = (int )(-10000);
		public const int   INVALID_NAME                                      = (int )((ERROR_BASE-1));
		public const int   UNKNOWN_OPTION                                    = (int )((ERROR_BASE-2));
		public const int   NOT_SEND_REQUEST_ERR                              = (int )((ERROR_BASE-3));
		public const int   NOT_RECEIVE_REPLY_ERR                             = (int )((ERROR_BASE-4));
		public const int   SERVER_ERR                                        = (int )((ERROR_BASE-5));
		public const int   PARAM_ERR                                         = (int )((ERROR_BASE-6));
		public const int   PATH_NOT_FOUND_ERR                                = (int )((ERROR_BASE-7));
		public const int   CONTROLFIELD_ERR                                  = (int )((ERROR_BASE-8));
		public const int   SEGDATA_ERR                                       = (int )((ERROR_BASE-9));
		public const int   DUPLICATE_FILE_ERR                                = (int )((ERROR_BASE-10));
		public const int   OFFSET_FIELD_ERR                                  = (int )((ERROR_BASE-11));
		public const int   OPERATION_NOT_SUPPORTED                           = (int )((ERROR_BASE-12));
		public const int   ACK_NOT_RCV_ERR                                   = (int )((ERROR_BASE-13));
		public const int   FILE_NOT_STORED_ERR                               = (int )((ERROR_BASE-14));
		public const int   NUMLOC_FIELD_ERR                                  = (int )((ERROR_BASE-15));
		public const int   SECTION_NOT_FOUND_ERR                             = (int )((ERROR_BASE-16));
		public const int   TAG_NOT_FOUND_ERR                                 = (int )((ERROR_BASE-17));
		public const int   ATTR_NOT_FOUND_ERR                                = (int )((ERROR_BASE-18));
		public const int   WRONG_REFERENCE_ERR                               = (int )((ERROR_BASE-19));
		public const int   NO_POOL_ERR                                       = (int )((ERROR_BASE-20));
		public const int   CLIP_NOT_FOUND_ERR                                = (int )((ERROR_BASE-21));
		public const int   TAGTREE_ERR                                       = (int )((ERROR_BASE-22));
		public const int   ISNOT_DIRECTORY_ERR                               = (int )((ERROR_BASE-23));
		public const int   UNEXPECTEDTAG_ERR                                 = (int )((ERROR_BASE-24));
		public const int   TAG_READONLY_ERR                                  = (int )((ERROR_BASE-25));
		public const int   OUT_OF_BOUNDS_ERR                                 = (int )((ERROR_BASE-26));
		public const int   FILESYS_ERR                                       = (int )((ERROR_BASE-27));
		public const int   STACK_DEPTH_ERR                                   = (int )((ERROR_BASE-29));
		public const int   TAG_HAS_NO_DATA_ERR                               = (int )((ERROR_BASE-30));
		public const int   VERSION_ERR                                       = (int )((ERROR_BASE-31));
		public const int   MULTI_BLOB_ERR                                    = (int )((ERROR_BASE-32));
		public const int   PROTOCOL_ERR                                      = (int )((ERROR_BASE-33));
		public const int   NO_SOCKET_AVAIL_ERR                               = (int )((ERROR_BASE-34));
		public const int   BLOBIDFIELD_ERR                                   = (int )((ERROR_BASE-35));
		public const int   BLOBIDMISMATCH_ERR                                = (int )((ERROR_BASE-36));
		public const int   PROBEPACKET_ERR                                   = (int )((ERROR_BASE-37));
		public const int   CLIPCLOSED_ERR                                    = (int )((ERROR_BASE-38));
		public const int   POOLCLOSED_ERR                                    = (int )((ERROR_BASE-39));
		public const int   BLOBBUSY_ERR                                      = (int )((ERROR_BASE-40));
		public const int   SERVER_NOTREADY_ERR                               = (int )((ERROR_BASE-41));
		public const int   SERVER_NO_CAPACITY_ERR                            = (int )((ERROR_BASE-42));
        public const int   DUPLICATE_ID_ERR                                  = (int )((ERROR_BASE-43));
        public const int   STREAM_VALIDATION_ERR                             = (int )((ERROR_BASE-44));
        public const int   STREAM_BYTECOUNT_MISMATCH_ERR                     = (int )((ERROR_BASE-45));

        public const int   NETWORK_ERROR_BASE                                = (int )(-10100);
		public const int   SOCKET_ERR                                        = (int )((NETWORK_ERROR_BASE-1));
		public const int   PACKETDATA_ERR                                    = (int )((NETWORK_ERROR_BASE-2));
		public const int   ACCESSNODE_ERR                                    = (int )((NETWORK_ERROR_BASE-3));

        public const int   SERVER_ERROR_BASE                                 = (int )(-10150);
		public const int   OPCODE_FIELD_ERR                                  = (int )((SERVER_ERROR_BASE-1));
		public const int   PACKET_FIELD_MISSING_ERR                          = (int )((SERVER_ERROR_BASE-2));
		public const int   AUTHENTICATION_FAILED_ERR                         = (int )((SERVER_ERROR_BASE-3));
		public const int   UNKNOWN_AUTH_SCHEME_ERR                           = (int )((SERVER_ERROR_BASE-4));
		public const int   UNKNOWN_AUTH_PROTOCOL_ERR                         = (int )((SERVER_ERROR_BASE-5));
		public const int   TRANSACTION_FAILED_ERR                            = (int )((SERVER_ERROR_BASE-6));
		public const int   PROFILECLIPID_NOTFOUND_ERR                        = (int )((SERVER_ERROR_BASE-7));
        public const int   ADVANCED_RETENTION_DISABLED_ERR                   = (int )((SERVER_ERROR_BASE-8));
        public const int   NON_EBR_CLIP_ERR                                  = (int )((SERVER_ERROR_BASE-9));
        public const int   EBR_OVERRIDE_ERR                                  = (int )((SERVER_ERROR_BASE-10));
        public const int   NO_EBR_EVENT_ERR                                  = (int )((SERVER_ERROR_BASE-11));
        public const int   RETENTION_OUT_OF_BOUNDS_ERR                       = (int )((SERVER_ERROR_BASE-12));
        public const int   RETENTION_HOLD_COUNT_ERR_ERR                      = (int )((SERVER_ERROR_BASE-13));
        public const int   METADATA_MISMATCH_ERR                             = (int )((SERVER_ERROR_BASE-14));

        public const int   CLIENT_ERROR_BASE                                 = (int )(-10200);
		public const int   OPERATION_REQUIRES_MARK                           = (int )((CLIENT_ERROR_BASE-1));
		public const int   QUERYCLOSED_ERR                                   = (int )((CLIENT_ERROR_BASE-2));
		public const int   WRONG_STREAM_ERR                                  = (int )((CLIENT_ERROR_BASE-3));
		public const int   OPERATION_NOT_ALLOWED                             = (int )((CLIENT_ERROR_BASE-4));
		public const int   SDK_INTERNAL_ERR                                  = (int )((CLIENT_ERROR_BASE-5));
		public const int   OUT_OF_MEMORY_ERR                                 = (int )((CLIENT_ERROR_BASE-6));
		public const int   OBJECTINUSE_ERR                                   = (int )((CLIENT_ERROR_BASE-7));
		public const int   NOTYET_OPEN_ERR                                   = (int )((CLIENT_ERROR_BASE-8));
		public const int   STREAM_ERR                                        = (int )((CLIENT_ERROR_BASE-9));
		public const int   TAGCLOSED_ERR                                     = (int )((CLIENT_ERROR_BASE-10));
		public const int   THREAD_ERR                                        = (int )((CLIENT_ERROR_BASE-11));
		public const int   PROBE_TIME_EXPIRED_ERR                            = (int )((CLIENT_ERROR_BASE-12));
		public const int   PROFILECLIPID_WRITE_ERR                           = (int )((CLIENT_ERROR_BASE-13));
		public const int   INVALID_XML_ERR                                   = (int )((CLIENT_ERROR_BASE-14));
		public const int   UNABLE_TO_GET_LAST_ERROR                          = (int )((CLIENT_ERROR_BASE-15));
        public const int   LOGGING_CALLBACK_ERR                              = (int )((CLIENT_ERROR_BASE-16));
        public const int   MIGRATION_ERR                                     = (int )((CLIENT_ERROR_BASE-17));
        public const int   COLLOCATION_ERR                                   = (int )((CLIENT_ERROR_BASE-18));
        public const int   REDIRECTED_ERR                                    = (int )((CLIENT_ERROR_BASE-19));
        public const int   INVALID_REDIRECT_ERR                              = (int )((CLIENT_ERROR_BASE-20));
        public const int   FEDERATED_VER_ERR                                 = (int )((CLIENT_ERROR_BASE-21));
        public const int   INVALID_FEDERATION_ERR                            = (int )((CLIENT_ERROR_BASE-22));

        public const int   UNDEFINED_ERROR_BASE                              = (int )(-10250);
		public const int   ZOS_ERROR_BASE                                    = (int )(-20000);
		public const int   SUBTASK_TCB_NOT_FOUND                             = (int )((ZOS_ERROR_BASE-1));
		public const int   INVALID_FUNCTION_ID                               = (int )((ZOS_ERROR_BASE-2));
		public const int   COMMAREA_MALLOC_ERR                               = (int )((ZOS_ERROR_BASE-3));
		public const int   ZOS_SUBTASK_ABEND                                 = (int )((ZOS_ERROR_BASE-4));
		public const int   ZOS_SUBTASK_UNKN_RC                               = (int )((ZOS_ERROR_BASE-5));
		public const int   ZOS_IEANTCR_BASE                                  = (int )(-20100);
		public const int   ZOS_IEANTDL_BASE                                  = (int )(-20200);
		public const int   ZOS_IEANTRT_BASE                                  = (int )(-20300);

        public const int   STREAMINFO_VERSION                                = (int )(3);
		public const int   STRING_BUFFER_SIZE								 = (int) 256;
		public const int   EMBEDDED_DATA_MAX_SIZE							 = (int) 102400;
		public const int   FULL_CLUSTER_LAYOUT								 = (int) 0;
		public const int   PARTIAL_CLUSTER_LAYOUT							 = (int) 1;
		public const int   NO_CLUSTER_LAYOUT								 = (int) 2;
		public const string POOLMAPPINGS									 = "poolmappings";
		public const string COMPLIANCE										 = "compliance";
		public const string POOLS											 = "pools";
		public const string PROFILES										 = "profiles";
		public const string MODE											 = "mode";
		public const string EVENT_BASED_RETENTION							 = "ebr";
		public const string RETENTION_HOLD									 = "retention-hold";
		public const string FIXED_RETENTION_MIN								 = "fixedminimum";
		public const string FIXED_RETENTION_MAX								 = "fixedmaximum";
		public const string VARIABLE_RETENTION_MIN							 = "variableminimum";
		public const string VARIABLE_RETENTION_MAX							 = "variablemaximum";
		public const string RETENTION_DEFAULT								 = "default";
		public const string RETENTION_MIN_MAX								 = "min-max";
		public const int   OPTION_SECONDS_STRING							 = (int) 0;
		public const int   OPTION_MILLISECONDS_STRING						 = (int) 1;

        public const string OPTION_STREAM_STRICT_MODE                        = "FP_OPTION_STREAM_STRICT_MODE";

        public const int LOGGING_COMPONENT_POOL                              = (int) (0x00000001);
        public const int LOGGING_COMPONENT_RETRY                             = (int) (0x00000002);
        public const int LOGGING_COMPONENT_XML                               = (int) (0x00000004);
        public const int LOGGING_COMPONENT_API                               = (int) (0x00000008);
        public const int LOGGING_COMPONENT_NET                               = (int) (0x00000010);
        public const int LOGGING_COMPONENT_TRANS                             = (int) (0x00000020);
        public const int LOGGING_COMPONENT_PACKET                            = (int) (0x00000040);
        public const int LOGGING_COMPONENT_EXCEPT                            = (int) (0x00000080);
        public const int LOGGING_COMPONENT_REFS                              = (int) (0x00000100);
        public const int LOGGING_COMPONENT_MOPI                              = (int) (0x00000200);
        public const int LOGGING_COMPONENT_STREAM                            = (int) (0x00000400);
        public const int LOGGING_COMPONENT_CSOD                              = (int) (0x00000800);
        public const int LOGGING_COMPONENT_CSO                               = (int) (0x00001000);
        public const int LOGGING_COMPONENT_MD5                               = (int) (0x00002000);
        public const int LOGGING_COMPONENT_APP                               = (int) (0x00004000);
        public const int LOGGING_COMPONENT_SHA                               = (int) (0x00040000);
        public const int LOGGING_COMPONENT_LIB                               = (int) (0x00080000);
        public const int LOGGING_COMPONENT_ALL                               = (int) -1; // 0xFFFFFFFF
        public const int LOG_NO_POLLING                                      = (int) -1;
        public const int LOG_SIZE_UNBOUNDED                                  = (int) -1;
    }



    // end of class Stream

    /// <summary> 
    ///Utility class for Logging useing FPLogStateRef and FPLogging APIs.
    ///@author Graham Stuart
    ///@version
     /// </summary>
    // end of class Logger

}
