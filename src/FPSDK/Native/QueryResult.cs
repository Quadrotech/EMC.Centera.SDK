using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class QueryResult 
    {

        public static void Close(FPQueryResultRef inQueryResultRef) 
        {
            SDK.FPQueryResult_Close(inQueryResultRef);
            SDK.CheckAndThrowError();
        }
        public static void GetClipID(FPQueryResultRef inQueryResultRef,  StringBuilder outClipID) 
        {
            SDK.FPQueryResult_GetClipID(inQueryResultRef, outClipID);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetTimestamp(FPQueryResultRef inQueryResultRef) 
        {
            FPLong retval = SDK.FPQueryResult_GetTimestamp(inQueryResultRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void GetField(FPQueryResultRef inQueryResultRef, string inAttrName, ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
        {
            SDK.FPQueryResult_GetField8(inQueryResultRef, inAttrName, outAttrValue, ref ioAttrValueLen);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetResultCode(FPQueryResultRef inQueryResultRef) 
        {
            FPInt retval = SDK.FPQueryResult_GetResultCode(inQueryResultRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPInt GetType(FPQueryResultRef inQueryResultRef) 
        {
            FPInt retval = SDK.FPQueryResult_GetType(inQueryResultRef);
            SDK.CheckAndThrowError();
            return retval;
        }

    }
}