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

using System;
using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPQueryResult : FPObject
    {
        FPQueryResultRef theResult;

        /// <summary>
		///Empty constructor - create the object but do not set a value.
		 /// </summary>
        public FPQueryResult()
        {
            theResult = 0;
        }

        internal FPQueryResultRef Result
        {
            set
            {
                theResult = value;
                AddObject(theResult, this);
            }
        }

        /// <summary>
		///Implicit conversion between an FPQueryResult object and an FPQueryResultRef
		///
		///@param	q	The FPQueryResult object.
		///@return	The FPPoolQueryRef associated with it.
		 /// </summary>
        public static implicit operator FPQueryResultRef(FPQueryResult q) 
        {
            return q.theResult;
        }

        /// <summary>
		///Explicitly close the FPQueryResult.
		 /// </summary>
        public override void Close()
        {
            if (theResult != 0)
            {
                RemoveObject(theResult);
                Native.QueryResult.Close(theResult);
                theResult = 0;
            }
        }

        /// <summary>
		///The ID of the Clip associated with this result.
		///See API Guide: FPQueryResult_GetClipID
		///
		 /// </summary>
        public string ClipID 
        {
            get
            {
                StringBuilder outClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);
			
                Native.QueryResult.GetClipID(theResult, outClipID);

                return outClipID.ToString();
            }
        }

        /// <summary>
		///The Timestamp of the Clip associated with this result.
		///See API Guide: FPQueryResult_GetTimestamp
		///
		 /// </summary>
        public DateTime Timestamp => FPMisc.GetDateTime(Native.QueryResult.GetTimestamp(theResult));

        /// <summary>
		///Retrieve the value of an Attribute of the Clip associated with this result.
		///See API Guide: FPQueryResult_GetField
		///
		///@param	inAttrName	The name of the attribute to retieve from the current member of the result set.
		///@return	The value of the Attribute  of the Clip associated with the current member of the result set.
		 /// </summary>
        public string GetField(string inAttrName) 
        {
            byte[] outString;
            FPInt bufSize = 0;
            FPInt len = 0;

            do
            {
                bufSize += FPMisc.STRING_BUFFER_SIZE;
                len = bufSize;
                outString = new byte[(int) bufSize];

                Native.QueryResult.GetField(theResult, inAttrName, ref outString, ref len);
            } while (len > bufSize);

            return Encoding.UTF8.GetString(outString, 0, (int)len - 1);

        }

        /// <summary>
		///The ResultCode indicating the status of the Query execution.
		///See API Guide: FPQueryResult_GetResultCode
		///
		 /// </summary>
        public int ResultCode => (int) Native.QueryResult.GetResultCode(theResult);

        /// <summary>
		///The state of the Clip on the Centera i.e.
		///
		///true Exists
		///false Deleted
		///
		///See API Guide: FPQueryResult_GetResultType
		///
		 /// </summary>
        public bool Exists 
        {
            get
            {
                if (Native.QueryResult.GetType(theResult) == (FPInt) FPMisc.QUERY_TYPE_EXISTING)
                    return true;
                else
                    return false;
            }
        }

        public override string ToString()
        {
            return "Clip: " + ClipID + 
                   " Status(" + (Exists ? "EXISTS" : "DELETED") + ")" + 
                   " Timestamp " + Timestamp;

        }


    }
}