namespace EMC.Centera.SDK.FPTypes
{
    public class PoolInfo
    {
        private FPPoolInfo info;

        public PoolInfo(FPPoolInfo _info)
        {
            info = _info;
        }

        public int poolInfoVersion => (int) info.poolInfoVersion;
        public long capacity => (long) info.capacity;

        public long freeSpace => (long) info.freeSpace;

        public string clusterName => info.clusterName;

        public string clusterID => info.clusterID;
        public string version => info.version;

        public string replicaAddress => info.replicaAddress;

        public override string ToString()
        {
            return "\nPool Information" +
                   "\n================" +
                   "\nCluster ID:                            " + clusterID +
                   "\nCluster Name:                          " + clusterName +
                   "\nCentraStar software version:           " + version +
                   "\nCluster Capacity (Bytes):              " + capacity +
                   "\nCluster Free Space (Bytes):            " + freeSpace +
                   "\nCluster Replica Address:               " + replicaAddress + "\n";

        }

    }
}