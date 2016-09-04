using System;
namespace EMC.Centera.SDK
{
    public interface IFPPool
    {
        string BlobNamingSchemes { get; }
        long Capacity { get; }
        string CenteraEdition { get; }
        string CentraStarVersion { get; }
        void ClipAuditedDelete(string inClipID, string inReason, long inOptions);
        void ClipAuditedDelete(string inClipID, string inReason);
        int ClipBufferSize { get; set; }
        FPClip ClipCreate(string inName);
        void ClipDelete(string inClipID);
        bool ClipExists(string inClipID);
        FPClip ClipOpen(string inClipID, int inOpenMode);
        FPClip ClipRawOpen(string inClipID, FPStream inStream);
        FPClip ClipRawOpen(string inClipID, FPStream inStream, long inOptions);
        string ClusterID { get; }
        string ClusterName { get; }
        DateTime ClusterTime { get; }
        bool CollisionAvoidanceEnabled { get; set; }
        string DefaultRetenionScheme { get; }
        bool DeleteAllowed { get; }
        string DeletePools { get; }
        bool DeletionsLogged { get; }
        bool EBRSupported { get; }
        bool ExistsAllowed { get; }
        string ExistsPools { get; }
        TimeSpan FixedRetentionMax { get; }
        TimeSpan FixedRetentionMin { get; }
        long FreeSpace { get; }
        bool HoldAllowed { get; }
        string HoldPools { get; }
        bool HoldSupported { get; }
        bool MultiClusterFailoverEnabled { get; set; }
        string PoolMappings { get; }
        string PoolProfiles { get; }
        int PrefetchBufferSize { get; set; }
        bool PrivilegedDeleteAllowed { get; }
        string PrivilegedDeletePools { get; }
        string ProfileClip { get; set; }
        bool QueryAllowed { get; }
        string QueryPools { get; }
        bool ReadAllowed { get; }
        string ReadPools { get; }
        string ReplicaAddress { get; }
        FPRetentionClassCollection RetentionClasses { get; }
        TimeSpan RetentionDefault { get; }
        bool RetentionMinMax { get; }
        int Timeout { get; set; }
        string ToString();
        TimeSpan VariableRetentionMax { get; }
        TimeSpan VariableRetentionMin { get; }
        bool WriteAllowed { get; }
        string WritePools { get; }
    }
}
