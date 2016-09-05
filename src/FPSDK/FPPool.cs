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
	///A Pool connection object.
	///@author Graham Stuart
	///@version
	 /// </summary>
	public class FPPool:FPObject, IFPPool
	{
	    private FPPoolRef _thePool = 0;

		/// <summary>
		///Creates a pool conection with the clusters specified in the poolConnectionString. See API Guide: FPPool_Open
		///
		///@param poolConnectionString A Pool connection string containing IP (or DNS names) of
		///                      the target clusters and associated authorization / options. 
		 /// </summary>
		public FPPool(string poolConnectionString)
		{
			_thePool = Native.Pool.Open(poolConnectionString);
			AddObject(_thePool, this);
		}


		/// <summary>
		///Implicit cast between an existing Pool object and raw FPPoolRef. 
		///
		///@param p A Pool object..
		///@return The FPPoolRef associated with this Pool.
		 /// </summary>
		public static implicit operator FPPoolRef(FPPool p) 
		{
			return p._thePool;
		}

		/// <summary>
		///Implicit cast between a raw FPPoolRef and a new Pool object. 
		///
		///@param poolRef FPPoolRef.
		///@return The FPPool object associated with the FPPoolRef.
		 /// </summary>
		public static implicit operator FPPool(FPPoolRef poolRef) 
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

				Native.Pool.GetPoolInfo(_thePool, ref outPoolInfo);
				return new PoolInfo(outPoolInfo);
			}
		}

		
		/// <summary>
		///Explicitly close the FPPoolRef belonging to this Pool object. 
		///
		 /// </summary>
		public override void Close() 
		{
			if (_thePool != 0)
			{
				RemoveObject(_thePool);
				Native.Pool.Close(this);
				_thePool = 0;
			}
		}


		/// <summary>
		///The size of the buffer to use when creating a CDF before overflowing to a
		///temporary file. Set this value to the typical maximum size of your application
		///CDFs including the size of any base64 embedded blob data.
		 /// </summary>
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

		/// <summary>
		///The TCP/IP connection timeout, in milliseconds. Default is 2 minutes (12000ms),
		///maximum is 10 minutes (600000ms).
		 /// </summary>
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

		/// <summary>
		///Is Failover to Replica / Secondary cluster enabled?
		 /// </summary>
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

		/// <summary>
		///Does the SDK request that extended naming schemes are used in order to guarantee
		///that no collisions can take place?
		///
		///If set to true and StorageStrategyCapacity is active on the Cluster, then
		///Single Instancing is disabled and each piece of content is stored separately
		///with the addition of a GUID to the standard ContentAddress.
		///
		///If StorageStratehyPerformance is active on the Cluster then an additional
		///discriminator is added to the Content Address.
		 /// </summary>
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

		/// <summary>
		///The size of the PrefetchBuffer. This should be set to be no less than
		///the EmbeddedBlob threshold (if one is used).
		 /// </summary>
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

		/// <summary>
		///The maximum number of sockets the SDK will allocate for the client application.
		 /// </summary>
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

		/// <summary>
		///The maximum number of retries that will be made after a failed oepration before
		///returning a failure to the client application.
		 /// </summary>
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

		/// <summary>
		///The threshold for how long an application probe is allowed to attempt communication with an
		///Access Node. The maximum value is 3600 seconds (1 hour)
		 /// </summary>
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

		/// <summary>
		///The time to wait before retrying a failed API call.
		 /// </summary>
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

		/// <summary>
		///After a failure, the amount of time a Cluster is marked as Non Available
		///before retrying.
		 /// </summary>
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


		/// <summary>
		///The strategy used for opening the Pool connection.
		 /// </summary>
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
		
		/// <summary>
		///The Threshold value to stop embedding data within the CDF.
		 /// </summary>
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
		///3.0
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
		/// </summary>
		*/

		/// <summary>
		///The Failover strategy for Read operations.
		 /// </summary>
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
		
		/// <summary>
		///The Failover strategy for Write operations.
		 /// </summary>
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
		
		/// <summary>
		///The Failover strategy for Delete operations.
		 /// </summary>
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
		
		/// <summary>
		///The Failover strategy for Exists operations.
		 /// </summary>
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
		
		/// <summary>
		///The Failover strategy for Query operations.
		 /// </summary>
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

		/// <summary>
		///The Cluster types that are available for Read failover.
		 /// </summary>
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

		/// <summary>
		///The Cluster types that are available for Write failover.
		 /// </summary>
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
		
		/// <summary>
		///The Cluster types that are available for Delete failover.
		 /// </summary>
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
		
		/// <summary>
		///The Cluster types that are available for Exists failover.
		 /// </summary>
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

		/// <summary>
		///The Cluster types that are available for Query failover.
		 /// </summary>
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
		
		/// <summary>
		///The capacity of the Cluster.
		 /// </summary>
		public long Capacity => PoolInfo.capacity;

	    /// <summary>
		///The amount of FreeSpace on the Cluster.
		 /// </summary>
		public long FreeSpace => PoolInfo.freeSpace;

	    /// <summary>
		///The ID string of the Cluster.
		 /// </summary>
		public string ClusterID => PoolInfo.clusterID;

	    /// <summary>
		///The name of the Cluster.
		 /// </summary>
		public string ClusterName => PoolInfo.clusterName;

	    /// <summary>
		///The version of CentraStar software on the Cluster.
		 /// </summary>
		public string CentraStarVersion => PoolInfo.version;

	    /// <summary>
		///The ReplicaAddress of the Cluster.
		 /// </summary>
		public string ReplicaAddress => PoolInfo.replicaAddress;

	    /// <summary>
		///The error status for the last SDK operation with any pool. See API Guide: FPPool_GetLastError
		///
		 /// </summary>
		public static int LastError => (int) Native.Pool.GetLastError();


	    /// <summary>
		///A structure containing information relating to the last SDK operation. See API Guide: FPPool_GetLastErrorInfo
		///
		 /// </summary>
		public static FPErrorInfo LastErrorInfo 
		{
			get
			{
				FPErrorInfo errorInfo = new FPErrorInfo();
				Native.Pool.GetLastErrorInfo(ref errorInfo);

				return errorInfo;
			}
		}

		/// <summary>
		///The naming schemes that are available on the Cluster.
		 /// </summary>
		public string BlobNamingSchemes => GetCapability(FPMisc.BLOBNAMING, FPMisc.SUPPORTED_SCHEMES);

	    /// <summary>
		///Is Query available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have Query capability.
		 /// </summary>
		public string QueryPools => GetCapability(FPMisc.CLIPENUMERATION, FPMisc.POOLS);
	    /// <summary>
		///Is Delete available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have Delete capability.
		 /// </summary>
		public string DeletePools => GetCapability(FPMisc.DELETE, FPMisc.POOLS);
	    /// <summary>
		///Are Delete operations logged i.e. are Reflections created?
		 /// </summary>
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
		
		/// <summary>
		///Is Exists available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have Exists capability.
		 /// </summary>
		public string ExistsPools => GetCapability(FPMisc.EXIST, FPMisc.POOLS);
	    /// <summary>
		///Is PrivilegedDelete available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have PrivilegedDelete capability.
		 /// </summary>
		public string PrivilegedDeletePools => GetCapability(FPMisc.PRIVILEGEDDELETE, FPMisc.POOLS);
	    /// <summary>
		///Is Read available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have Read capability.
		 /// </summary>
		public string ReadPools => GetCapability(FPMisc.READ, FPMisc.POOLS);
	    /// <summary>
		///What is the Default retention Strategy of the cluster?
		 /// </summary>
		public string DefaultRetenionScheme => GetCapability(FPMisc.RETENTION, FPMisc.DEFAULT);

	    /// <summary>
		///Is Write available in the Capabilities list of the Pool object?
		 /// </summary>
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

		/// <summary>
		///A list of pools that have Write capability.
		 /// </summary>
		public string WritePools => GetCapability(FPMisc.WRITE, FPMisc.POOLS);

	    /// <summary>
		///A list containing the Pool mappings for all Profiles / Pools.
		 /// </summary>
		public string PoolMappings => GetCapability(FPMisc.RETENTION, FPMisc.POOLMAPPINGS);

	    /// <summary>
		///A list containing the Profiles for which a Pool mapping exists.
		 /// </summary>
		public string PoolProfiles => GetCapability(FPMisc.RETENTION, FPMisc.PROFILES);

	    /// <summary>
		///The Edition of the Centera this pool is connected to i.e. 
		///
		///basic	Basic Edition
		///ce		Governance Edition (formerly known as Compliance Edition)
		///ce+		Compliane Edition Plus
		 /// </summary>
		public string CenteraEdition => GetCapability(FPMisc.COMPLIANCE, FPMisc.MODE);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inCapabilityName"></param>
        /// <param name="inCapabilityAttributeName"></param>
        /// <returns></returns>
	    internal string GetCapability(string inCapabilityName,  string inCapabilityAttributeName) 
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
                    ErrorInfo err = e.ErrorInfo;

                    if (e.ErrorInfo.Error == FPMisc.ATTR_NOT_FOUND_ERR)
                        return "";
                    else
                        throw e;
                }

			} while (len > bufSize);
			
			return outString.ToString();

		}
				
		/// <summary>
		///A DateTime object representing the time of the Cluster associated
		///with this object
		 /// </summary>
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

		/// <summary>
		///A string containing the version information for the supplied component.
		///See API Guide: FPPool_GetComponentVersion
		///
		 /// </summary>
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
		
		/// <summary>
		///The ID of the clip containing information about the
		///profile being used to connect to the pool.
		///
		 /// </summary>
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


		/// <summary>
		///Check for the existence of a given ClipID in the Pool associated with
		///this object. See API Guide: FPClip_Exists
		///
		///@param inClipID	The clip to check for existence.
		///@return A boolean value realting to existence.
		 /// </summary>
		public bool ClipExists(string inClipID) 
		{
			if (Native.Clip.Exists(this, inClipID) == FPBool.True)
				return true;
			else
				return false;
		}


		/// <summary>
		///Delete the clip with the the supplied ID from the Pool associated with this
		///object.  See API Guide: FPClip_Delete
		///
		///@param inClipID	The clip to delete.
		 /// </summary>
		public void ClipDelete(string inClipID) 
		{
			Native.Clip.Delete(this, inClipID);
		}


		/// <summary>
		///Delete the clip (using DEFAULT_OPTIONS) with the the supplied ID from the Pool
		///associated with this object and record the reason in an Audit Trail. 
		///See API Guide: FPClip_AuditedDelete
		///
		///@param inClipID	The clip to delete.
		///@param inReason	A string contining free test relating to the reason for deletion.
		 /// </summary>
		public void ClipAuditedDelete(string inClipID,  string inReason) 
		{
			ClipAuditedDelete(inClipID, inReason, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/// <summary>
		///Delete the clip with the the supplied ID from the Pool associated with this
		///object and record the reason in an Audit Trail. See API Guide: FPClip_AuditedDelete
		///
		///@param inClipID	The clip to delete.
		///@param inReason	A string contining free test relating to the reason for deletion.
		///@param inOptions	The type of deletion being performed (Normal or Privileged).
		 /// </summary>
		public void ClipAuditedDelete(string inClipID,  string inReason, long inOptions) 
		{
			Native.Clip.AuditedDelete(this, inClipID, inReason, (FPLong) inOptions);
		}

		/// <summary>
		///Create a new clip in the Pool. See API Guide: FPClip_Create 
		///
		///@param inName The name of the clip to be created.
		///@return The resulting Clip object.
		 /// </summary>
		public FPClip ClipCreate(string inName) 
		{
			return new FPClip(Native.Clip.Create(this, inName));
		}


		/// <summary>
		///Open a Clip that exists in the Pool. See API Guide: FPClip_Open
		///
		///@param inClipID		The Content Address string of the clip to be opened.
		///@param inOpenMode	How the clip should be opened (Flat or Tree)
		///@return The reulting Clip object.
		 /// </summary>
		public FPClip ClipOpen(string inClipID, int inOpenMode) 
		{
			return new FPClip(Native.Clip.Open(this, inClipID, (FPInt) inOpenMode));
		}


		/// <summary>
		///Create a new clip by reading a raw clip from a stream using DEFAULT_OPTIONS.
		///See API Guide: FPClip_RawOpen
		///
		///@param inClipID	The ID of the Clip being read - must match the new Clip ID.
		///@param inStream	The stream to read the clip from.
		///return The resultng new Clip in this Pool.
		 /// </summary>
		public FPClip ClipRawOpen(string inClipID, FPStream inStream) 
		{
			return ClipRawOpen(inClipID, inStream, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/// <summary>
		///Create a new clip by reading a raw clip from a stream.
		///See API Guide: FPClip_RawOpen
		///
		///@param inClipID	The ID of the Clip being read - must match the new Clip ID.
		///@param inStream	The stream to read the clip from.
		///@param inOptions	Options relating to how the Open is performed.
		///return The resultng new Clip in this Pool.
		 /// </summary>
		public FPClip ClipRawOpen(string inClipID, FPStream inStream, long inOptions) 
		{
			return new FPClip(Native.Clip.RawOpen(_thePool, inClipID, inStream, (FPLong) inOptions));
		}
		
		public override string ToString()
		{
			return PoolInfo.ToString();
		}


		private FPRetentionClassCollection myRetentionClasses;
		
		/// <summary>
		///The RetentionClasses configured within this pool. These are configured on the Centera so no modification
		///should be made to the Collection.
		 /// </summary>
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

		/// <summary>
		///Registers the application with the Centera. This is not required but does allow the administrator to see which apoplications
		///are using the Centera at any point in time.
		////// </summary>
		public static void RegisterApplication(string appName, string appVersion)
		{
			Native.Pool.RegisterApplication(appName, appVersion);
		}

		/// <summary>
		///Is Event Based Retention supported on the Cluster.
		////// </summary>
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

		/// <summary>
		///Is Litigation / Retention Hold supported on the cluster.
		////// </summary>
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

		/// <summary>
		///Is Litigation / Retention Hold allowed on the Cluster. Ensures that it is supported before checking.
		////// </summary>
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

		/// <summary>
		///A list of pools that have Retention Hold capability.
		 /// </summary>
		public string HoldPools => GetCapability(FPMisc.RETENTION_HOLD, FPMisc.POOLS);
	    /// <summary>
		///The minimum period that may be specified for a normal (fixed) retention period.
		////// </summary>
		public TimeSpan FixedRetentionMin => new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.FIXED_RETENTION_MIN)));

	    /// <summary>
		///The maximum period that may be specified for a normal (fixed) retention period.
		////// </summary>
		public TimeSpan FixedRetentionMax => new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.FIXED_RETENTION_MAX)));

	    /// <summary>
		///The minimum period that may be specified for a variable (EBR) retention period.
		////// </summary>
		public TimeSpan VariableRetentionMin => new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.VARIABLE_RETENTION_MIN)));

	    /// <summary>
		///The maximum period that may be specified for a variable (EBR) retention period.
		////// </summary>
		public TimeSpan VariableRetentionMax => new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.VARIABLE_RETENTION_MAX)));

	    /// <summary>
		///The default retention period that may will be used if the application does not specifically set it.
		////// </summary>
		public TimeSpan RetentionDefault => new TimeSpan(0, 0, int.Parse(GetCapability(FPMisc.RETENTION, FPMisc.RETENTION_DEFAULT)));

	    /// <summary>
		///Are Retention Governors supported on the Centera i.e. Minimum / Maximum values for Fixed and Variable retention.
		////// </summary>
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