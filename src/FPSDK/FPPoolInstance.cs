using System.Collections;
using System.Text;

namespace EMC.Centera.SDK.Extension
{
    public sealed class FPPoolInstance : FPPool
    {
        private static readonly Hashtable connectionString2PoolConnection = Hashtable.Synchronized(new Hashtable());

        public static IFPPool Get(string connectionString)
        {
            IFPPool myPool;
            if (connectionString2PoolConnection.Contains(connectionString))
            {
                myPool = (IFPPool)connectionString2PoolConnection[connectionString];
            }
            else
            {
                myPool = new FPPoolInstance(connectionString);

                connectionString2PoolConnection.Add(connectionString, myPool);
            }
            return myPool;
        }

        public static IFPPool Get(byte[] theBytes)
        {
            string connectionString = Encoding.Unicode.GetString(theBytes);

            return Get(connectionString);
        }

        private FPPoolInstance(string poolConnectionString) : base(poolConnectionString)
        {
        }

        ~FPPoolInstance()
        {
            Dispose(false);
        }
    }
}
