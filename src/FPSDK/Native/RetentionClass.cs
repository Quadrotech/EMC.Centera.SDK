using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class RetentionClass 
    {

        public static void GetName(FPRetentionClassRef inClassRef, ref byte[] outName, ref FPInt ioNameLen) 
        {
            SDK.FPRetentionClass_GetName8(inClassRef, outName, ref ioNameLen);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetPeriod(FPRetentionClassRef inClassRef) 
        {
            FPLong retval = SDK.FPRetentionClass_GetPeriod(inClassRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPRetentionClassRef inClassRef) 
        {
            SDK.FPRetentionClass_Close(inClassRef);
            SDK.CheckAndThrowError();
        }

    }
}