using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Query 
    {

        public static FPQueryRef Open(FPPoolRef inPool, FPLong inStartTime, FPLong inStopTime,  string inReserved) 
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

    }
}