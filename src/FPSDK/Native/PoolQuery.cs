using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class PoolQuery 
    {

        public static FPPoolQueryRef Open(FPPoolRef inPoolRef, FPQueryExpressionRef inQueryExpressionRef) 
        {
            FPPoolQueryRef retval = SDK.FPPoolQuery_Open(inPoolRef, inQueryExpressionRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPPoolQueryRef inPoolQueryRef) 
        {
            SDK.FPPoolQuery_Close(inPoolQueryRef);
            SDK.CheckAndThrowError();
        }
        public static FPPoolRef GetPoolRef(FPPoolQueryRef inPoolQueryRef) 
        {
            FPPoolRef retval = SDK.FPPoolQuery_GetPoolRef(inPoolQueryRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPQueryResultRef FetchResult(FPPoolQueryRef inPoolQueryRef, FPInt inTimeout) 
        {
            FPQueryResultRef retval = SDK.FPPoolQuery_FetchResult(inPoolQueryRef, inTimeout);
            SDK.CheckAndThrowError();
            return retval;
        }

    }
}