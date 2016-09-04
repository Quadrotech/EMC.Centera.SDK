namespace EMC.Centera.SDK.FPTypes
{
    public class PoolInfo
    {
        private FPPoolInfo info;

        public PoolInfo(FPPoolInfo _info)
        {
            info = _info;
        }

        public int poolInfoVersion
        {
            get
            {
                return (int) info.poolInfoVersion;
            }
        }
        public long capacity
        {
            get
            {
                return (long) info.capacity;
            }
        }
        public long freeSpace
        {
            get
            {
                return (long) info.freeSpace;
            }
        }
        public string clusterName
        {
            get
            {
                return info.clusterName;
            }
        }
        public string clusterID
        {
            get
            {
                return info.clusterID;
            }
        }
        public string version
        {
            get
            {
                return info.version;
            }
        }
        public string replicaAddress
        {
            get
            {
                return info.replicaAddress;
            }
        }
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