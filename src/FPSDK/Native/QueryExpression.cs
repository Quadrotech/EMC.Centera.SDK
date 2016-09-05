/*****************************************************************************

Copyright © 2006 EMC Corporation. All Rights Reserved
 
This file is part of .NET wrapper for the Centera SDK.

.NET wrapper is free software; you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation version 2.

In addition to the permissions granted in the GNU General Public License
version 2, EMC Corporation gives you unlimited permission to link the compiled
version of this file into combinations with other programs, and to distribute
those combinations without any restriction coming from the use of this file.
(The General Public License restrictions do apply in other respects; for
example, they cover modification of the file, and distribution when not linked
into a combined executable.)

.NET wrapper is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
PARTICULAR PURPOSE. See the GNU General Public License version 2 for more
details.

You should have received a copy of the GNU General Public License version 2
along with .NET wrapper; see the file COPYING. If not, write to:

 EMC Corporation 
 Centera Open Source Intiative (COSI) 
 80 South Street
 1/W-1
 Hopkinton, MA 01748 
 USA

******************************************************************************/

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