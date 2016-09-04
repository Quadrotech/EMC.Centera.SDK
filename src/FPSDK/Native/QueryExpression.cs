using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class QueryExpression 
    {

        public static FPQueryExpressionRef Create() 
        {
            FPQueryExpressionRef retval = SDK.FPQueryExpression_Create();
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPQueryExpressionRef inRef) 
        {
            SDK.FPQueryExpression_Close(inRef);
            SDK.CheckAndThrowError();
        }
        public static void SetStartTime(FPQueryExpressionRef inRef, FPLong inTime) 
        {
            SDK.FPQueryExpression_SetStartTime(inRef, inTime);
            SDK.CheckAndThrowError();
        }
        public static void SetEndTime(FPQueryExpressionRef inRef, FPLong inTime) 
        {
            SDK.FPQueryExpression_SetEndTime(inRef, inTime);
            SDK.CheckAndThrowError();
        }
        public static void SetType(FPQueryExpressionRef inRef, FPInt inType) 
        {
            SDK.FPQueryExpression_SetType(inRef, inType);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetStartTime(FPQueryExpressionRef inRef) 
        {
            FPLong retval = SDK.FPQueryExpression_GetStartTime(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPLong GetEndTime(FPQueryExpressionRef inRef) 
        {
            FPLong retval = SDK.FPQueryExpression_GetEndTime(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPInt GetType(FPQueryExpressionRef inRef) 
        {
            FPInt retval = SDK.FPQueryExpression_GetType(inRef);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void SelectField(FPQueryExpressionRef inRef,  string inFieldName) 
        {
            SDK.FPQueryExpression_SelectField8(inRef, inFieldName);
            SDK.CheckAndThrowError();
        }
        public static void DeselectField(FPQueryExpressionRef inRef,  string inFieldName) 
        {
            SDK.FPQueryExpression_DeselectField8(inRef, inFieldName);
            SDK.CheckAndThrowError();
        }
        public static FPBool IsFieldSelected(FPQueryExpressionRef inRef,  string inFieldName) 
        {
            FPBool retval = SDK.FPQueryExpression_IsFieldSelected8(inRef, inFieldName);
            SDK.CheckAndThrowError();
            return retval;
        }

    }
}