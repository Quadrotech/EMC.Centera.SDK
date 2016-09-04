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
using System.Collections;
using System.Runtime.InteropServices;
using EMC.Centera.FPTypes;

namespace EMC.Centera.SDK
{
    /** 
     * Contains definitions of FP constants and useful helper methods.
     * @author Graham Stuart
     * @version
     */

    public class FPMisc
    {
        internal static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        /**
         * Converts a string with format of "YYYY:MM:DD:HH:MM:SS" into
         * a standard DateTime object. 
         *
         * @param s String containing the date to be converted.
         * @return A DateTime object representing this date.
         */
        public static DateTime GetDateTime(String s)
        {
            if (s.Length > 0)
                return new DateTime(Int32.Parse(s.Substring(0, 4)),  // YYYY
                Int32.Parse(s.Substring(5, 2)),  // MM
                Int32.Parse(s.Substring(8, 2)),  // DD
                Int32.Parse(s.Substring(11, 2)), // HH
                Int32.Parse(s.Substring(14, 2)), // MM
                Int32.Parse(s.Substring(17, 2)));// SS
            else
                throw new FPLibraryException("Invalid / null string passed to GetDateTime", FPMisc.OUT_OF_BOUNDS_ERR);
        }

        /**
         * Converts an  FPLong into a standard DateTime object 
         * identified by the provided connect string.
         *
         * @param l Number of milliseconds since the Epoch 
         * @return A DateTime object representing the converted FPLong value.
         */
        public static DateTime GetDateTime(FPLong l)
        {
            return FPMisc.Epoch.Add(new TimeSpan((long)l * 10000));
        }

        /**
         * Converts a DateTime object to an FPLong time value. 
         * identified by the provided connect string.
         *
         * @param d The DateTime to be converted 
         * @return An FPLong representing the ocnverted DateTime object.
         */
        public static FPLong GetTime(DateTime d)
        {
            return (FPLong)((d.Ticks - FPMisc.Epoch.Ticks) / 10000);
        }

        /**
         * Converts a Time (represented by a string value) to a long representation. 
         *
         * @param inTime	The string representing the Time. 
         * @return A long representing the Time value (number of seconds since the UNIX Epoch).
         */
        public static long FPTime_StringToLong(String inTime)
        {
            long retval = (long)FPApi.FP.FPTime_StringToSeconds(inTime);
            return retval;
        }
        /**
         * Converts a Time (represented by a string value) to a long representation in Seconds. 
         *
         * @param inTime	The string representing the Time. 
         * @return A long representing the Time value (number of seconds since the UNIX Epoch).
         */
        public static long FPTime_StringToSeconds(String inTime)
        {
            return FPTime_StringToLong(inTime);
        }

        /**
         * Converts a Time (represented by a string value) to a long representation in Milliseconds. 
         *
         * @param inTime	The string representing the Time. 
         * @return A long representing the Time value (number of milliseconds since the UNIX Epoch).
         */
        public static long FPTime_StringToMilliseconds(String inTime)
        {
            long retval = (long)FPApi.FP.FPTime_StringToMilliseconds(inTime);
            return retval;
        }

        /**
         * Converts a Time (represented by a long value) to a string representation. 
         *
         * @param inTime	The long representing the Time (number of seconds since the UNIX Epoch). 
         * @return An string representing the Time value.
         */
        public static String FPTime_LongToString(long inTime)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += FPMisc.STRING_BUFFER_SIZE;
                clusterTimeLen += FPMisc.STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                FPApi.FP.FPTime_LongToString((FPLong)inTime, outClusterTime, ref clusterTimeLen);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

        /**
         * Converts a Time (represented by a long value of the number of milliseconds since the UNIX Epoch) to a string representation. 
         *
         * @param inTime	The long representing the Time. 
         * @param inOptions	Allows for inclusion or exclusion of the millisecond element in the returned string.
         * @return A string representing the Time value.
         */
        public static String FPTime_MillisecondsToString(long inTime, FPInt inOptions)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += FPMisc.STRING_BUFFER_SIZE;
                clusterTimeLen += FPMisc.STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                FPApi.FP.FPTime_MillisecondsToString((FPLong)inTime, outClusterTime, ref clusterTimeLen, inOptions);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

        /**
         * Converts a Time (represented by a long valueof the number of seconds since the UNIX Epoch) to a string representation. 
         *
         * @param inTime	The long representing the Time.
         * @param inOptions	Allows for inclusion or exclusion of the millisecond element in the returned string.
         * @return A string representing the Time value.
         */
        public static String FPTime_SecondsToString(long inTime, FPInt inOptions)
        {
            StringBuilder outClusterTime = new StringBuilder();
            FPInt bufSize = 0;
            FPInt clusterTimeLen = 0;

            do
            {
                bufSize += FPMisc.STRING_BUFFER_SIZE;
                clusterTimeLen += FPMisc.STRING_BUFFER_SIZE;
                outClusterTime.EnsureCapacity((int)bufSize);
                FPApi.FP.FPTime_SecondsToString((FPLong)inTime, outClusterTime, ref clusterTimeLen, inOptions);
            } while (clusterTimeLen > bufSize);

            return outClusterTime.ToString();
        }

		public const String  OPTION_BUFFERSIZE                                 = "buffersize";
		public const String  OPTION_TIMEOUT                                    = "timeout";
		public const String  OPTION_RETRYCOUNT                                 = "retrycount";
		public const String  OPTION_RETRYSLEEP                                 = "retrysleep";
		public const String  OPTION_MAXCONNECTIONS                             = "maxconnections";
		public const String  OPTION_ENABLE_MULTICLUSTER_FAILOVER               = "multiclusterfailover";
		public const String  OPTION_DEFAULT_COLLISION_AVOIDANCE                = "collisionavoidance";
		public const String  OPTION_PREFETCH_SIZE                              = "prefetchsize";
		public const String  OPTION_CLUSTER_NON_AVAIL_TIME                     = "clusternonavailtime";
		public const String  OPTION_PROBE_LIMIT                                = "probetimelimit";
		public const String  OPTION_EMBEDDED_DATA_THRESHOLD                    = "embedding_threshold";
		public const String  OPTION_OPENSTRATEGY                               = "openstrategy";
		public const String  OPTION_ATTRIBUTE_LIMIT                            = "attributelimit";
		public const String  OPTION_MULTICLUSTER_READ_STRATEGY                 = "multicluster_read_strategy";
		public const String  OPTION_MULTICLUSTER_WRITE_STRATEGY                = "multicluster_write_strategy";
		public const String  OPTION_MULTICLUSTER_DELETE_STRATEGY               = "multicluster_delete_strategy";
		public const String  OPTION_MULTICLUSTER_EXISTS_STRATEGY               = "multicluster_exists_strategy";
		public const String  OPTION_MULTICLUSTER_QUERY_STRATEGY                = "multicluster_query_strategy";
		public const String  OPTION_MULTICLUSTER_READ_CLUSTERS                 = "multicluster_read_clusters";
		public const String  OPTION_MULTICLUSTER_WRITE_CLUSTERS                = "multicluster_write_clusters";
		public const String  OPTION_MULTICLUSTER_DELETE_CLUSTERS               = "multicluster_delete_clusters";
		public const String  OPTION_MULTICLUSTER_EXISTS_CLUSTERS               = "multicluster_exists_clusters";
		public const String  OPTION_MULTICLUSTER_QUERY_CLUSTERS                = "multicluster_query_clusters";
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
		public const String  TRUE                                              = "true";
		public const String  FALSE                                             = "false";
		public const String  READ                                              = "read";
		public const String  WRITE                                             = "write";
		public const String  DELETE                                            = "delete";
		public const String  PURGE                                             = "purge";
		public const String  EXIST                                             = "exist";
		public const String  CLIPENUMERATION                                   = "clip-enumeration";
		public const String  RETENTION                                         = "retention";
		public const String  BLOBNAMING                                        = "blobnaming";
		public const String  MONITOR                                           = "monitor";
		public const String  DELETIONLOGGING                                   = "deletionlogging";
		public const String  PRIVILEGEDDELETE                                  = "privileged-delete";
		public const String  ALLOWED                                           = "allowed";
		public const String  SUPPORTED                                         = "supported";
		public const String  DUPLICATEDETECTION                                = "duplicate-detection";
		public const String  DEFAULT                                           = "default";
		public const String  SUPPORTED_SCHEMES                                 = "supported-schemes";
		public const String  MD5                                               = "MD5";
		public const String  MG                                                = "MG";
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
		public const String POOLMAPPINGS									 = "poolmappings";
		public const String COMPLIANCE										 = "compliance";
		public const String POOLS											 = "pools";
		public const String PROFILES										 = "profiles";
		public const String MODE											 = "mode";
		public const String EVENT_BASED_RETENTION							 = "ebr";
		public const String RETENTION_HOLD									 = "retention-hold";
		public const String FIXED_RETENTION_MIN								 = "fixedminimum";
		public const String FIXED_RETENTION_MAX								 = "fixedmaximum";
		public const String VARIABLE_RETENTION_MIN							 = "variableminimum";
		public const String VARIABLE_RETENTION_MAX							 = "variablemaximum";
		public const String RETENTION_DEFAULT								 = "default";
		public const String RETENTION_MIN_MAX								 = "min-max";
		public const int   OPTION_SECONDS_STRING							 = (int) 0;
		public const int   OPTION_MILLISECONDS_STRING						 = (int) 1;

        public const String OPTION_STREAM_STRICT_MODE                        = "FP_OPTION_STREAM_STRICT_MODE";

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
    /** 
     * Abstract disposable base class for FP objects.
     * @author Graham Stuart
     * @version
     */
  
    public abstract class FPObject : IDisposable
    {
        protected bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The "disposing" boolean allows derived objects to add an additional step to dispose
        // of any IDisposable objects they own dependent on who is calling it i.e. diectly or via the dtor.
        // We don't have any others so it's effectively not utilised.
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
                Close();
            }
        }

        ~FPObject()
        {
            Dispose(false);
        }

        public abstract void Close();

        static internal Hashtable SDKObjects = Hashtable.Synchronized(new Hashtable());

        protected void AddObject(object key, FPObject obj)
        {
            SDKObjects.Add(key, obj);
        }

        protected void RemoveObject(object key)
        {
            SDKObjects.Remove(key);
        }
    }

    /** 
     * An object representing an FPStreamRef.
     * @author Graham Stuart
     * @version
     */
    public class FPStream : FPObject
    {
        /**
         * StreamType indicator used when creating "special" FPStream objects.
         * 
         * Members: Null / Stdio.
         */
        public enum StreamType { Null, Stdio };

        /**
         * The StreamDirection indicator used when creating buffer based FPStream objects.
         * 
         * Members: InputToCentera / OutputFromCentera.
         */
        public enum StreamDirection { OutputFromCentera = 0, InputToCentera = 1 };

        protected FPStreamRef theStream;

        /**
         * Create a Stream using a buffer to write to (StreamType.InputToCentera)
         * or read from (StreamType.OutputFromCentera) the Centera.
         * See API Guide: FPStream_CreateBufferForInput, FPStream_CreateBufferForOutput
         * 
         * @param	streamBuffer	The buffer that the Stream will read from (for INPUT to the Centera)
         *							or write to (for OUTPUT from the Centera).
         * @param	bufferSize	The size of the buffer.
         * @param	streamDirection	The StreamDirection enum indicating input or output.
         */
        public FPStream(IntPtr streamBuffer, int bufferSize, StreamDirection streamDirection)
        {
            if (streamDirection == StreamDirection.InputToCentera)
                theStream = FPApi.Stream.CreateBufferForInput(streamBuffer, (long)bufferSize);
            else
            {
                theStream = FPApi.Stream.CreateBufferForOutput(streamBuffer, (long)bufferSize);
            }
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads from the Centera and writes the content to a file.
         * See API Guide: FPStream_CreateFileForOutput
         *
         * @param	fileName	The name of the file to write to.
         * @param	permissions	The permissions to create the file with.
         */
        public FPStream(String fileName, String permissions)
        {
            theStream = FPApi.Stream.CreateFileForOutput(fileName, permissions);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads a file and writes the content to Centera.
         * See API Guide: FPStream_CreateFileForInput
         *
         * @param	fileName	The name of the file to read from.
         * @param	bufferSize	The size of the buffer to use for writing.
         */
        public FPStream(String fileName, long bufferSize)
        {
            theStream = FPApi.Stream.CreateFileForInput(fileName, "rb", bufferSize);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads part of a file and writes the content to Centera.
         * See API Guide: FPStream_CreatePartialFileForInput
         *
         * @param	fileName	The name of the file to read from.
         * @param	bufferSize	The size of the buffer to use for writing.
         * @param	offset  	The position in the file to start reading from.
         * @param	length  	The length of the file segment to read from.
         */
        public FPStream(String fileName, long bufferSize, long offset, long length)
        {
            theStream = FPApi.Stream.CreatePartialFileForInput(fileName, "rb", bufferSize, offset, length);
            AddObject(theStream, this);
        }

        /**
         * Create a Stream that reads from Centera and writes the content at an offset in a file.
         * See API Guide: FPStream_CreatePartialFileForOutput
         *
         * @param	fileName	The name of the file to read from.
         * @param   permission  The write mode that the file is opened in.
         * @param	bufferSize	The size of the buffer to use for writing.
         * @param	offset  	The position in the file to start writing to.
         * @param	length  	The length of the file segment to write to.
         * @param	maxFileSize	The maximum size that the output file max grow to.
         */
        public FPStream(String fileName, String permission, long bufferSize, long offset, long length, long maxFileSize)
        {
            theStream = FPApi.Stream.CreatePartialFileForOutput(fileName, permission, bufferSize, offset, length, maxFileSize);
            AddObject(theStream, this);
        }
        /**
         * Create a Stream that reads from the Centera and writes the content to stdio or
         * a null stream.
         * See API Guide: FPStream_CreateToStdio / FPStream_CreateToNull
         *
         * @param	streamType	StreamType enum - Stdio or Null.
         */
        public FPStream(FPStream.StreamType streamType)
        {
            if (streamType == StreamType.Stdio)
                theStream = FPApi.Stream.FPStream_CreateToStdio();
            else
                theStream = FPApi.Stream.FPStream_CreateToNull();

            AddObject(theStream, this);
        }

        /**
         * Creates a Stream for temporary storage. If the length of the stream is greater than
         * pMemBuffSize the overflow is flushed to a temporary file.
         * See API Guide: FPStream_CreateTemporaryFile
         *
         * @param	pMemBuffSize	The size of the in-memory buffer to use.
         */
        public FPStream(long pMemBuffSize)
        {
            theStream = FPApi.Stream.FPStream_CreateTemporaryFile(pMemBuffSize);
            AddObject(theStream, this);
        }

        /**
         * Implicit conversion between a Stream and an FPStreamRef
         *
         * @param	s	The Stream.
         * @return	The FPStreamRef associated with this Stream.
         */
        static public implicit operator FPStreamRef(FPStream s)
        {
            return s.theStream;
        }

        /**
         * Implicit conversion between an FPStreamRef and a  Stream
         *
         * @param	streamRef	The FPStreamRef.
         * @return	The new Stream.
         */
        static public implicit operator FPStream(FPStreamRef streamRef)
        {
            // Find the relevant Tag object in the hastable for this FPTagRef
            FPStream streamObject = null;

            if (SDKObjects.Contains(streamRef))
            {
                streamObject = (FPStream)SDKObjects[streamRef];
            }
            else
            {
                throw new FPLibraryException("FPStreamRef is not asscociated with an FPStream object", FPMisc.WRONG_REFERENCE_ERR);
            }

            return streamObject;
        }

        /**
         * Explicitly close this Stream. See API Guide: FPStream_Close
         */
        public override void Close()
        {
            if (theStream != 0)
            {
                RemoveObject(theStream);
                FPApi.Stream.Close(theStream);
                theStream = 0;
            }
        }

        protected FPStream()
        {
        }

    }  // end of class Stream

    /** 
     * Utility class for Logging useing FPLogStateRef and FPLogging APIs.
     * @author Graham Stuart
     * @version
     */
    public class FPLogger : FPObject
    {
        protected FPLogStateRef theLogger;

        /**
         * Basic Constructor which creates a default FPLogStateRef object.
         * See API Guide: FPLogging_CreateLogState
         *
         * @return	A new FPLogger object.
         */
        public FPLogger()
        {
            theLogger = FPApi.Logging.CreateLogState();
            AddObject(theLogger, this);
        }

        /**
         * Constructor which creates an FPLogStateRef object using a config file.
         * See API Guide: FPLogging_OpenLogState
         *
         * @param   inName  File system path name for a FPLogStateRef contained in a file.
         * @return	A new FPLogger object.
         */
        public FPLogger(String inName)
        {
            theLogger = FPApi.Logging.OpenLogState(inName);
            AddObject(theLogger, this);
        }

        /**
         * Explicitly free the resources assoicated with an FPLogStateRef object
         * See API Guide: FPLogState_Delete
         *
         */
        public override void Close()
        {
            if (theLogger != 0)
            {
                RemoveObject(theLogger);
                FPApi.Logging.Delete(theLogger);
                theLogger = 0;
            }
        }

        /**
         * Static method to allow an application to log its own messages to the active LogState
         * See API Guide: FPLogging_log
         *
         * @param   inLevel The FPLogLevel of the message.
         * @param   inMessage   The actual message to be logged.
         */
        static public void Log(FPLogLevel inLevel, String inMessage)
        {
            FPApi.Logging.Log(inLevel, inMessage);
        }

        /**
         * Implicit conversion between a  Logger and an FPLogStateRef
         *
         * @param	l	The Logger.
         * @return	The FPLogStateRef associated with this Logger.
         */
        static public implicit operator FPLogStateRef(FPLogger l)
        {
            return l.theLogger;
        }

        /**
         * Implicit conversion between an FPLogStateRef and a  Stream
         *
         * @param	logRef	The FPLogStateRef.
         * @return	The Logger.
         */
        static public implicit operator FPLogger(FPLogStateRef logRef)
        {
            // Find the relevant Logger object in the hastable for this LogStateRef
            FPLogger logObject = null;

            if (SDKObjects.Contains(logRef))
            {
                logObject = (FPLogger)SDKObjects[logRef];
            }
            else
            {
                throw new FPLibraryException("FPLogStateRef is not asscociated with an FPLogger object", FPMisc.WRONG_REFERENCE_ERR);
            }

            return logObject;
        }

        /**
         * Allow the application to save the current logger object to a config file.
         * See API Guide: FPLogState_Save
         *
         * @param   inPath  The file system pathname of the file to be written to.
         */
        public void Save(String inPath)
        {
            FPApi.Logging.Save(this, inPath);
        }

        /**
         * Start the SDK logging using the current Logger object's state
         * See API Guide: FPLogging_Start
         *
         */
        public void Start()
        {
            FPApi.Logging.Start(this);
        }

        /**
         * Static method to stop the SDK lgging
         * See API Guide: FPLogging_Stop
         *
         */
        static public void Stop()
        {
            FPApi.Logging.Stop();
        }

        /**
         * Static method to allow an application to register its own method to perform logging
         * See API Guide: FPLogging_RegisterCallback
         *
         * @param   inProc  The user Callback method (delegate)
         */
        static public void RegisterCallback(FPLogProc inProc)
        {
            FPApi.Logging.RegisterCallback(inProc);
        }

        /**
         * AppendMode property i.e. Append to existing log or replace on start.
         * See API Guide: FPLogState_GetAppendMode / FPLogState_SetAppendMode
         *
         */
        public FPBool AppendMode
        {
            get
            {
                return FPApi.Logging.GetAppendMode(this);
            }
            set
            {
                FPApi.Logging.SetAppendMode(this, value);
            }
        }

        /**
         * DisableCallback property i.e. do we use the user-registered callback for logging
         * See API Guide: FPLogState_GetDisableCallback / FPLogState_SetDisableCallback
         *
         */
        public FPBool DisableCallback
        {
            get
            {
                return FPApi.Logging.GetDisableCallback(this);
            }
            set
            {
                FPApi.Logging.SetDisableCallback(this, value);
            }
        }

        /**
         * LogFilter property - determines the type of calls which are logged
         * See API Guide: FPLogState_GetLogFilter / FPLogState_SetLogFilter
         *
         */
        public FPInt LogFilter
        {
            get
            {
                return FPApi.Logging.GetLogFilter(this);
            }
            set
            {
                FPApi.Logging.SetLogFilter(this, value);
            }
        }

        /**
         * LogLevel property - determines which severity level of SDK messages are logged
         * See API Guide: FPLogState_GetLogLevel / FPLogState_SetLogLevel
         *
         */
        public FPLogLevel LogLevel
        {
            get
            {
                return FPApi.Logging.GetLogLevel(this);
            }
            set
            {
                FPApi.Logging.SetLogLevel(this, value);
            }
        }

        /**
         * LogPath property - the file system path for the log file
         * See API Guide: FPLogState_GetLogPath / FPLogState_SetLogPath
         *
         */
        public String LogPath
        {
            get
            {
                return FPApi.Logging.GetLogPath(this);
            }
            set
            {
                FPApi.Logging.SetLogPath(this, value);
            }
        }

        /**
         * MaxLogSize property - the maximum file system size the log file can occupy (default 1GB)
         * See API Guide: FPLogState_GetMaxLogSize / FPLogState_SetMaxLogSize
         *
         */
        public FPLong MaxLogSize
        {
            get
            {
                return FPApi.Logging.GetMaxLogSize(this);
            }
            set
            {
                FPApi.Logging.SetMaxLogSize(this, value);
            }
        }

        /**
         * MaxOverflows property - the number of backup log files allowed (default 1)
         * See API Guide: FPLogState_GetMaxOverflows / FPLogState_SetMaxOverflows
         *
         */
        public FPInt MaxOverflows
        {
            get
            {
                return FPApi.Logging.GetMaxOverflows(this);
            }
            set
            {
                FPApi.Logging.SetMaxOverflows(this, value);
            }
        }

        /**
         * PollInterval property - the time (in minutes) between polling for changes
         * in any config file that was used to enable logging (default 5)
         * See API Guide: FPLogState_GetPollInterval / FPLogState_SetPollInterval
         *
         */
        public FPInt PollInterval
        {
            get
            {
                return FPApi.Logging.GetPollInterval(this);
            }
            set
            {
                FPApi.Logging.SetPollInterval(this, value);
            }
        }

        public static void ConsoleMessage(String message)
        {
            Console.Write(message);
        }
    } // end of class Logger

}
