using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class RetentionClassContext 
    {

        public static void Close(FPRetentionClassContextRef inContextRef) 
        {
            SDK.FPRetentionClassContext_Close(inContextRef);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetNumClasses(FPRetentionClassContextRef inContextRef) 
        {
            FPInt retval = SDK.FPRetentionClassContext_GetNumClasses(inContextRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPRetentionClassRef GetFirstClass(FPRetentionClassContextRef inContextRef) 
        {
            FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetFirstClass(inContextRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPRetentionClassRef GetLastClass(FPRetentionClassContextRef inContextRef) 
        {
            FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetLastClass(inContextRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPRetentionClassRef GetNextClass(FPRetentionClassContextRef inContextRef) 
        {
            FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetNextClass(inContextRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPRetentionClassRef GetPreviousClass(FPRetentionClassContextRef inContextRef) 
        {
            FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetPreviousClass(inContextRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPRetentionClassRef GetNamedClass(FPRetentionClassContextRef inContextRef,  string inName) 
        {
            FPRetentionClassRef retval = SDK.FPRetentionClassContext_GetNamedClass8(inContextRef, inName);
            SDK.CheckAndThrowError();
            return retval;
        }

    }
}