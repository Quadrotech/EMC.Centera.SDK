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

namespace EMC.Centera.SDK
{	
		
	/** 
	 * A Pool connection object.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPPool:FPObject, IFPPool
	{
		FPPoolRef thePool = 0;

		/**
		 * Creates a pool conection with the clusters specified in the poolConnectionString. See API Guide: FPPool_Open
		 * 
		 * @param poolConnectionString A Pool connection string containing IP (or DNS names) of
		 *                       the target clusters and associated authorization / options. 
		 */
		public FPPool(String poolConnectionString)
		{
			thePool = Native.Pool.Open(poolConnectionString);
			AddObject(thePool, this);
		}


		/**
		 * Implicit cast between an existing Pool object and raw FPPoolRef. 
		 *
		 * @param p A Pool object..
		 * @return The FPPoolRef associated with this Pool.
		 */
		static public implicit operator FPPoolRef(FPPool p) 
		{
			return p.thePool;
		}

		/**
		 * Implicit cast between a raw FPPoolRef and a new Pool object. 
		 *
		 * @param poolRef FPPoolRef.
		 * @return The FPPool object associated with the FPPoolRef.
		 */
		static public implicit operator FPPool(FPPoolRef poolRef) 
		{
			// Find the relevant Pool object in the hastable for this FPPoolRef
			FPPool poolObject = null;

			if (SDKObjects.ContainsKey(poolRef))
			{
				poolObject = (FPPool) SDKObjects[poolRef];
			}
            else
            {
                throw new FPLibraryException("FPPoolRef is not asscociated with an FPPool object", FPMisc.WRONG_REFERENCE_ERR);
            }

			return poolObject;
		}

		
		private PoolInfo PoolInfo
		{
			get
			{
				FPPoolInfo outPoolInfo = new FPPoolInfo();

				Native.Pool.GetPoolInfo(thePool, ref outPoolInfo);
				return new PoolInfo(outPoolInfo);
			}
		}

		
		/**
		 * Explicitly close the FPPoolRef belonging to this Pool object. 
		 *
		 */
		public override void Close() 
		{
			if (thePool != 0)
			{
				RemoveObject(thePool);
				Native.Pool.Close(this);
				thePool = 0;
			}
		}


		/**
		 * The size of the buffer to use when creating a CDF before overflowing to a
		 * temporary file. Set this value to the typical maximum size of your application
		 * CDFs including the size of any base64 embedded blob data.
		 */
		public int ClipBufferSize
		{
			get
			{
				return (int) Native.Pool.GetIntOption(this, FPMisc.OPTION_BUFFERSIZE);
			}
			set
			{
				Native.Pool.SetIntOption(this, FPMisc.OPTION_BUFFERSIZE, (FPInt) value);
			}
		}

		/**
		 * The TCP/IP connection timeout, in milliseconds. Default is 2 minutes (12000ms),
		 * maximum is 10 minutes (600000ms).
		 */
		public int Timeout
		{
			get
			{
				return (int) Native.Pool.GetIntOption(this, FPMisc.OPTION_TIMEOUT);
			}
			set
			{
				Native.Pool.SetIntOption(this, FPMisc.OPTION_TIMEOUT, (FPInt) value);
			}
		}

		/**
		 * Is Failover to Replica / Secondary cluster enabled?
		 */
		public bool MultiClusterFailoverEnabled
		{
			get
			{
				if (Native.Pool.GetIntOption(this, FPMisc.OPTION_ENABLE_MULTICLUSTER_FAILOVER) == (FPInt) 1)
					return true;
				else
					return false;
			}
			set
			{
				if (value == true)
					Native.Pool.SetIntOption(this, FPMisc.OPTION_ENABLE_MULTICLUSTER_FAILOVER, (FPInt) 1);
				else
					Native.Pool.SetIntOption(this, FPMisc.OPTION_ENABLE_MULTICLUSTER_FAILOVER, (FPInt) 0);
			}
		}

		/**
		 * Does the SDK request that extended naming schemes are used in order to guarantee
		 * that no collisions can take place?
		 * 
		 * If set to true and StorageStrategyCapacity is active on the Cluster, then
		 * Single Instancing is disabled and each piece of content is stored separately
		 * with the addition of a GUID to the standard ContentAddress.
		 * 
		 * If StorageStratehyPerformance is active on the Cluster then an additional
		 * discriminator is added to the Content Address.
		 */
		public bool CollisionAvoidanceEnabled
		{
			get
			{
				if (Native.Pool.GetIntOption(this, FPMisc.OPTION_DEFAULT_COLLISION_AVOIDANCE) == (FPInt) 1)
					return true;
				else
					return false;
			}
			set
			{
				if (value == true)
					Native.Pool.SetIntOption(this, FPMisc.OPTION_DEFAULT_COLLISION_AVOIDANCE, (FPInt) 1);
				else
					Native.Pool.SetIntOption(this, FPMisc.OPTION_DEFAULT_COLLISION_AVOIDANCE, (FPInt) 0);
			}
		}

		/**
		 * The size of the PrefetchBuffer. This should be set to be no less than
		 * the EmbeddedBlob threshold (if one is used).
		 */
		public int PrefetchBufferSize
		{
			get
			{
				return (int) Native.Pool.GetIntOption(this, FPMisc.OPTION_PREFETCH_SIZE);
			}
			set
			{
				Native.Pool.SetIntOption(this, FPMisc.OPTION_PREFETCH_SIZE, (FPInt) value);
			}
		}

		/**
		 * The maximum number of sockets the SDK will allocate for the client application.
		 */
		public static int MaxConnections
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MAXCONNECTIONS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MAXCONNECTIONS, (FPInt) value);
			}
		}

		/**
		 * The maximum number of retries that will be made after a failed oepration before
		 * returning a failure to the client application.
		 */
		public static int RetryLimit
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_RETRYCOUNT);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_RETRYCOUNT, (FPInt) value);
			}
		}

		/**
		 * The threshold for how long an application probe is allowed to attempt communication with an
		 * Access Node. The maximum value is 3600 seconds (1 hour)
		 */
		public static int ProbeLimit
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_PROBE_LIMIT);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_PROBE_LIMIT, (FPInt) value);
			}
		}

		/**
		 * The time to wait before retrying a failed API call.
		 */
		public static int RetrySleepTime
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_RETRYSLEEP);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_RETRYSLEEP, (FPInt) value);
			}
		}

		/**
		 * After a failure, the amount of time a Cluster is marked as Non Available
		 * before retrying.
		 */
		public static int ClusterNonAvailableTime
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_CLUSTER_NON_AVAIL_TIME);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_CLUSTER_NON_AVAIL_TIME, (FPInt) value);
			}
		}


		/**
		 * The strategy used for opening the Pool connection.
		 */
		public static int OpenStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_OPENSTRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_OPENSTRATEGY, (FPInt) value);
			}
		}
		
		/**
		 * The Threshold value to stop embedding data within the CDF.
		 */
		public static int EmbeddedBlobThreshold
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_EMBEDDED_DATA_THRESHOLD);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_EMBEDDED_DATA_THRESHOLD, (FPInt) value);
			}
		}

		
		/*
		 * 3.0
		public static int ProbeLimit
		{
			get
			{
				return (int) EMC.Centera.SDK.Native.Pool.GetGlobalOption(FPMisc.OPTION_PROBE_LIMIT);
			}
			set
			{
				EMC.Centera.SDK.Native.Pool.SetGlobalOption(FPMisc.OPTION_PROBE_LIMIT, (FPInt) value);
			}
		}
		*/
		
		/**
		 * The Failover strategy for Read operations.
		 */
		public static int MultiClusterReadStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_READ_STRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_READ_STRATEGY, (FPInt) value);
			}
		}
		
		/**
		 * The Failover strategy for Write operations.
		 */
		public static int MultiClusterWriteStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_WRITE_STRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_WRITE_STRATEGY, (FPInt) value);
			}
		}
		
		/**
		 * The Failover strategy for Delete operations.
		 */
		public static int MultiClusterDeleteStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_DELETE_STRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_DELETE_STRATEGY, (FPInt) value);
			}
		}
		
		/**
		 * The Failover strategy for Exists operations.
		 */
		public static int MultiClusterExistsStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_EXISTS_STRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_EXISTS_STRATEGY, (FPInt) value);
			}
		}
		
		/**
		 * The Failover strategy for Query operations.
		 */
		public static int MultiClusterQueryStrategy
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_QUERY_STRATEGY);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_QUERY_STRATEGY, (FPInt) value);
			}
		}

		/**
		 * The Cluster types that are available for Read failover.
		 */
		public static int MultiClusterReadClusters
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_READ_CLUSTERS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_READ_CLUSTERS, (FPInt) value);
			}
		}

		/**
		 * The Cluster types that are available for Write failover.
		 */
		public static int MultiClusterWriteClusters
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_WRITE_CLUSTERS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_WRITE_CLUSTERS, (FPInt) value);
			}
		}
		
		/**
		 * The Cluster types that are available for Delete failover.
		 */
		public static int MultiClusterDeleteClusters
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_DELETE_CLUSTERS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_DELETE_CLUSTERS, (FPInt) value);
			}
		}
		
		/**
		 * The Cluster types that are available for Exists failover.
		 */
		public static int MultiClusterExistsClusters
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_EXISTS_CLUSTERS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_EXISTS_CLUSTERS, (FPInt) value);
			}
		}

		/**
		 * The Cluster types that are available for Query failover.
		 */
		public static int MultiClusterQueryClusters
		{
			get
			{
				return (int) Native.Pool.GetGlobalOption(FPMisc.OPTION_MULTICLUSTER_QUERY_CLUSTERS);
			}
			set
			{
				Native.Pool.SetGlobalOption(FPMisc.OPTION_MULTICLUSTER_QUERY_CLUSTERS, (FPInt) value);
			}
		}
		
		/**
		 * The capacity of the Cluster.
		 */
		public long Capacity
		{
			get
			{
				return PoolInfo.capacity;
			}
		}

		/**
		 * The amount of FreeSpace on the Cluster.
		 */
		public long FreeSpace
		{
			get
			{
				return PoolInfo.freeSpace;
			}
		}

		/**
		 * The ID string of the Cluster.
		 */
		public string ClusterID
		{
			get
			{
				return PoolInfo.clusterID;
			}
		}

		/**
		 * The name of the Cluster.
		 */
		public string ClusterName
		{
			get
			{
				return PoolInfo.clusterName;
			}
		}

		/**
		 * The version of CentraStar software on the Cluster.
		 */
		public string CentraStarVersion
		{
			get
			{
				return PoolInfo.version;
			}
		}

		/**
		 * The ReplicaAddress of the Cluster.
		 */
		public string ReplicaAddress
		{
			get
			{
				return PoolInfo.replicaAddress;
			}
		}

		/**
		 * The error status for the last SDK operation with any pool. See API Guide: FPPool_GetLastError
		 *
		 */
		public static int LastError
		{
			get
			{
				return (int) Native.Pool.GetLastError();
			}
		}
			

		/**
		 * A structure containing information relating to the last SDK operation. See API Guide: FPPool_GetLastErrorInfo
		 *
		 */
		public static FPErrorInfo LastErrorInfo 
		{
			get
			{
				FPErrorInfo errorInfo = new FPErrorInfo();
				Native.Pool.GetLastErrorInfo(ref errorInfo);

				return errorInfo;
			}
		}

		/**
		 * The naming schemes that are available on the Cluster.
		 */
		public string BlobNamingSchemes
		{
			get
			{
				return GetCapability(FPMisc.BLOBNAMING, FPMisc.SUPPORTED_SCHEMES);
			}
		}

		/**
		 * Is Query available in the Capabilities list of the Pool object?
		 */
		public bool QueryAllowed
		{
			get
			{
				if (GetCapability(FPMisc.CLIPENUMERATION, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Query capability.
		 */
		public string QueryPools
		{
			get
			{
				return GetCapability(FPMisc.CLIPENUMERATION, FPMisc.POOLS);
			}
		}
		/**
		 * Is Delete available in the Capabilities list of the Pool object?
		 */
		public bool DeleteAllowed
		{
			get
			{
				if (GetCapability(FPMisc.DELETE, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Delete capability.
		 */
		public string DeletePools
		{
			get
			{
				return GetCapability(FPMisc.DELETE, FPMisc.POOLS);
			}
		}
		/**
		 * Are Delete operations logged i.e. are Reflections created?
		 */
		public bool DeletionsLogged
		{
			get
			{
				if (GetCapability(FPMisc.DELETIONLOGGING, FPMisc.SUPPORTED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}
		
		/**
		 * Is Exists available in the Capabilities list of the Pool object?
		 */
		public bool ExistsAllowed
		{
			get
			{
				if (GetCapability(FPMisc.EXIST, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Exists capability.
		 */
		public string ExistsPools
		{
			get
			{
				return GetCapability(FPMisc.EXIST, FPMisc.POOLS);
			}
		}
		/**
		 * Is PrivilegedDelete available in the Capabilities list of the Pool object?
		 */
		public bool PrivilegedDeleteAllowed
		{
			get
			{
				if (GetCapability(FPMisc.PRIVILEGEDDELETE, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have PrivilegedDelete capability.
		 */
		public string PrivilegedDeletePools
		{
			get
			{
				return GetCapability(FPMisc.PRIVILEGEDDELETE, FPMisc.POOLS);
			}
		}
		/**
		 * Is Read available in the Capabilities list of the Pool object?
		 */
		public bool ReadAllowed
		{
			get
			{
				if (GetCapability(FPMisc.READ, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Read capability.
		 */
		public string ReadPools
		{
			get
			{
				return GetCapability(FPMisc.READ, FPMisc.POOLS);
			}
		}
		/**
		 * What is the Default retention Strategy of the cluster?
		 */
		public string DefaultRetenionScheme
		{
			get
			{
				return GetCapability(FPMisc.RETENTION, FPMisc.DEFAULT);
			}
		}

		/**
		 * Is Write available in the Capabilities list of the Pool object?
		 */
		public bool WriteAllowed
		{
			get
			{
				if (GetCapability(FPMisc.WRITE, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Write capability.
		 */
		public string WritePools
		{
			get
			{
				return GetCapability(FPMisc.WRITE, FPMisc.POOLS);
			}
		}

		/**
		 * A list containing the Pool mappings for all Profiles / Pools.
		 */
		public string PoolMappings
		{
			get
			{
				return GetCapability(FPMisc.RETENTION, FPMisc.POOLMAPPINGS);
			}
		}

		/**
		 * A list containing the Profiles for which a Pool mapping exists.
		 */
		public string PoolProfiles
		{
			get
			{
				return GetCapability(FPMisc.RETENTION, FPMisc.PROFILES);
			}
		}

		/**
		 * The Edition of the Centera this pool is connected to i.e. 
		 * 
		 * basic	Basic Edition
		 * ce		Governance Edition (formerly known as Compliance Edition)
		 * ce+		Compliane Edition Plus
		 */
		public string CenteraEdition
		{
			get
			{
				return GetCapability(FPMisc.COMPLIANCE, FPMisc.MODE);
			}
		}

		internal string GetCapability(String inCapabilityName,  string inCapabilityAttributeName) 
		{
			StringBuilder outString = new StringBuilder();
			FPInt bufSize = 0;
			FPInt len = 0;

			do
			{
				bufSize += FPMisc.STRING_BUFFER_SIZE;
				len += FPMisc.STRING_BUFFER_SIZE;
				outString.EnsureCapacity((int) bufSize);
                try
                {
                    Native.Pool.GetCapability(this, inCapabilityName, inCapabilityAttributeName, outString, ref len);
                }
                catch (FPLibraryException e)
                {
                    ErrorInfo err = e.errorInfo;

                    if (e.errorInfo.error == FPMisc.ATTR_NOT_FOUND_ERR)
                        return "";
                    else
                        throw e;
                }

			} while (len > bufSize);
			
			return outString.ToString();

		}
				
		/**
		 * A DateTime object representing the time of the Cluster associated
		 * with this object
		 */
		public DateTime ClusterTime
		{
			get
			{
				StringBuilder outClusterTime = new StringBuilder();
				FPInt bufSize = 0;
				FPInt ioClusterTimeLen = 0;

				do
				{
					bufSize += FPMisc.STRING_BUFFER_SIZE;
					ioClusterTimeLen += FPMisc.STRING_BUFFER_SIZE;
					outClusterTime.EnsureCapacity((int) bufSize);
					Native.Pool.GetClusterTime(this, outClusterTime, ref ioClusterTimeLen);
				} while (ioClusterTimeLen > bufSize);
			
				return FPMisc.GetDateTime(outClusterTime.ToString());
			}
		}

		/**
		 * A string containing the version information for the supplied component.
		 * See API Guide: FPPool_GetComponentVersion
		 *
		 */
		public static string SDKVersion
		{
			get
			{
				StringBuilder outVersion = new StringBuilder();
				FPInt bufSize = 0;
				FPInt ioVersionLen = 0;

				do
				{
					bufSize += FPMisc.STRING_BUFFER_SIZE;
					ioVersionLen += FPMisc.STRING_BUFFER_SIZE;
					outVersion.EnsureCapacity((int) bufSize);
					Native.Pool.GetComponentVersion((FPInt) FPMisc.VERSION_FPLIBRARY_DLL, outVersion, ref ioVersionLen);
				} while (ioVersionLen > bufSize);
			
				return outVersion.ToString();
			}
		}
		
		/**
		 * The ID of the clip containing information about the
		 * profile being used to connect to the pool.
		 *
		 */
		public string ProfileClip
		{
			get
			{
				StringBuilder profileClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);
				Native.Pool.GetClipID(this, profileClipID);
				return profileClipID.ToString();
			}
			set
			{
				Native.Pool.SetClipID(this, value); 
			}
		}


		/**
		 * Check for the existence of a given ClipID in the Pool associated with
		 * this object. See API Guide: FPClip_Exists
		 *
		 * @param inClipID	The clip to check for existence.
		 * @return A boolean value realting to existence.
		 */
		public bool ClipExists(String inClipID) 
		{
			if (Native.Clip.Exists(this, inClipID) == FPBool.True)
				return true;
			else
				return false;
		}


		/**
		 * Delete the clip with the the supplied ID from the Pool associated with this
		 * object.  See API Guide: FPClip_Delete
		 *
		 * @param inClipID	The clip to delete.
		 */
		public void ClipDelete(String inClipID) 
		{
			Native.Clip.Delete(this, inClipID);
		}


		/**
		 * Delete the clip (using DEFAULT_OPTIONS) with the the supplied ID from the Pool
		 * associated with this object and record the reason in an Audit Trail. 
		 * See API Guide: FPClip_AuditedDelete
		 *
		 * @param inClipID	The clip to delete.
		 * @param inReason	A string contining free test relating to the reason for deletion.
		 */
		public void ClipAuditedDelete(String inClipID,  string inReason) 
		{
			ClipAuditedDelete(inClipID, inReason, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Delete the clip with the the supplied ID from the Pool associated with this
		 * object and record the reason in an Audit Trail. See API Guide: FPClip_AuditedDelete
		 *
		 * @param inClipID	The clip to delete.
		 * @param inReason	A string contining free test relating to the reason for deletion.
		 * @param inOptions	The type of deletion being performed (Normal or Privileged).
		 */
		public void ClipAuditedDelete(String inClipID,  string inReason, long inOptions) 
		{
			Native.Clip.AuditedDelete(this, inClipID, inReason, (FPLong) inOptions);
		}

		/**
		 * Create a new clip in the Pool. See API Guide: FPClip_Create 
		 *
		 * @param inName The name of the clip to be created.
		 * @return The resulting Clip object.
		 */
		public FPClip ClipCreate(String inName) 
		{
			return new FPClip(Native.Clip.Create(this, inName));
		}


		/**
		 * Open a Clip that exists in the Pool. See API Guide: FPClip_Open
		 *
		 * @param inClipID		The Content Address string of the clip to be opened.
		 * @param inOpenMode	How the clip should be opened (Flat or Tree)
		 * @return The reulting Clip object.
		 */
		public FPClip ClipOpen(String inClipID, int inOpenMode) 
		{
			return new FPClip(Native.Clip.Open(this, inClipID, (FPInt) inOpenMode));
		}


		/**
		 * Create a new clip by reading a raw clip from a stream using DEFAULT_OPTIONS.
		 * See API Guide: FPClip_RawOpen
		 *
		 * @param inClipID	The ID of the Clip being read - must match the new Clip ID.
		 * @param inStream	The stream to read the clip from.
		 * return The resultng new Clip in this Pool.
		 */
		public FPClip ClipRawOpen(String inClipID, FPStream inStream) 
		{
			return ClipRawOpen(inClipID, inStream, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Create a new clip by reading a raw clip from a stream.
		 * See API Guide: FPClip_RawOpen
		 *
		 * @param inClipID	The ID of the Clip being read - must match the new Clip ID.
		 * @param inStream	The stream to read the clip from.
		 * @param inOptions	Options relating to how the Open is performed.
		 * return The resultng new Clip in this Pool.
		 */
		public FPClip ClipRawOpen(String inClipID, FPStream inStream, long inOptions) 
		{
			return new FPClip(Native.Clip.RawOpen(thePool, inClipID, inStream, (FPLong) inOptions));
		}
		
		public override string ToString()
		{
			return PoolInfo.ToString();
		}


		private FPRetentionClassCollection myRetentionClasses = null;
		
		/**
		 * The RetentionClasses configured within this pool. These are configured on the Centera so no modification
		 * should be made to the Collection.
		 */
		public FPRetentionClassCollection RetentionClasses
		{
			get
			{
				if (myRetentionClasses == null)
					myRetentionClasses = new FPRetentionClassCollection(this);

				return myRetentionClasses;
			}
		}
	
		// New in 3.1

		/**
		 * Registers the application with the Centera. This is not required but does allow the administrator to see which apoplications
		 * are using the Centera at any point in time.
		 **/
		public static void RegisterApplication(string appName, string appVersion)
		{
			Native.Pool.RegisterApplication(appName, appVersion);
		}

		/**
		 * Is Event Based Retention supported on the Cluster.
		 **/
		public bool EBRSupported
		{
			get
			{
				if (GetCapability(FPMisc.COMPLIANCE, FPMisc.EVENT_BASED_RETENTION).Equals(FPMisc.SUPPORTED))
					return true;
				else
					return false;
			}
		}

		/**
		 * Is Litigation / Retention Hold supported on the cluster.
		 **/
		public bool HoldSupported
		{
			get
			{
				if (GetCapability(FPMisc.COMPLIANCE, FPMisc.RETENTION_HOLD).Equals(FPMisc.SUPPORTED))
					return true;
				else
					return false;
			}
		}

		/**
		 * Is Litigation / Retention Hold allowed on the Cluster. Ensures that it is supported before checking.
		 **/
		public bool HoldAllowed
		{
			get
			{
				if (HoldSupported &&
					GetCapability(FPMisc.RETENTION_HOLD, FPMisc.ALLOWED).Equals(FPMisc.TRUE))
					return true;
				else
					return false;
			}
		}

		/**
		 * A list of pools that have Retention Hold capability.
		 */
		public string HoldPools
		{
			get
			{
				return GetCapability(FPMisc.RETENTION_HOLD, FPMisc.POOLS);
			}
		}
		/**
		 * The minimum period that may be specified for a normal (fixed) retention period.
		 **/
		public TimeSpan FixedRetentionMin
		{
			get
			{
				return new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.FIXED_RETENTION_MIN)));
			}
		}

		/**
		 * The maximum period that may be specified for a normal (fixed) retention period.
		 **/
		public TimeSpan FixedRetentionMax
		{
			get
			{
				return new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.FIXED_RETENTION_MAX)));
			}
		}

		/**
		 * The minimum period that may be specified for a variable (EBR) retention period.
		 **/
		public TimeSpan VariableRetentionMin
		{
			get
			{
				return new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.VARIABLE_RETENTION_MIN)));
			}
		}
		
		/**
		 * The maximum period that may be specified for a variable (EBR) retention period.
		 **/
		public TimeSpan VariableRetentionMax
		{
			get
			{
				return new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.VARIABLE_RETENTION_MAX)));
			}
		}

		/**
		 * The default retention period that may will be used if the application does not specifically set it.
		 **/
		public TimeSpan RetentionDefault
		{
			get
			{
				return new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.RETENTION_DEFAULT)));
			}
		}

		/**
		 * Are Retention Governors supported on the Centera i.e. Minimum / Maximum values for Fixed and Variable retention.
		 **/
		public bool RetentionMinMax
		{
			get
			{
				if (GetCapability(FPMisc.COMPLIANCE, FPMisc.RETENTION_MIN_MAX).Equals("supported"))
					return true;
				else
					return false;
			}
		}

        public static bool StrictStreamMode
        {
            get
            {
                if (Native.Pool.GetGlobalOption(FPMisc.OPTION_STREAM_STRICT_MODE).Equals(FPMisc.TRUE))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Native.Pool.SetGlobalOption(FPMisc.OPTION_STREAM_STRICT_MODE, (FPInt) 0);
                else
                    Native.Pool.SetGlobalOption(FPMisc.OPTION_STREAM_STRICT_MODE, (FPInt) 1);
            }
        }
	}
}