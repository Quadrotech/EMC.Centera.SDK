using System;
using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPQueryResult : FPObject
    {
        FPQueryResultRef theResult;

        /**
		 * Empty constructor - create the object but do not set a value.
		 */
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

        /**
		 * Implicit conversion between an FPQueryResult object and an FPQueryResultRef
		 *
		 * @param	q	The FPQueryResult object.
		 * @return	The FPPoolQueryRef associated with it.
		 */
        public static implicit operator FPQueryResultRef(FPQueryResult q) 
        {
            return q.theResult;
        }

        /**
		 * Explicitly close the FPQueryResult.
		 */
        public override void Close()
        {
            if (theResult != 0)
            {
                RemoveObject(theResult);
                Native.QueryResult.Close(theResult);
                theResult = 0;
            }
        }

        /**
		 * The ID of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetClipID
		 * 
		 */
        public string ClipID 
        {
            get
            {
                StringBuilder outClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);
			
                Native.QueryResult.GetClipID(theResult, outClipID);

                return outClipID.ToString();
            }
        }

        /**
		 * The Timestamp of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetTimestamp
		 *
		 */
        public DateTime Timestamp => FPMisc.GetDateTime(Native.QueryResult.GetTimestamp(theResult));

        /**
		 * Retrieve the value of an Attribute of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetField
		 * 
		 * @param	inAttrName	The name of the attribute to retieve from the current member of the result set.
		 * @return	The value of the Attribute  of the Clip associated with the current member of the result set.
		 */
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

        /**
		 * The ResultCode indicating the status of the Query execution.
		 * See API Guide: FPQueryResult_GetResultCode
		 * 
		 */
        public int ResultCode => (int) Native.QueryResult.GetResultCode(theResult);

        /**
		 * The state of the Clip on the Centera i.e.
		 * 
		 * true Exists
		 * false Deleted
		 * 
		 * See API Guide: FPQueryResult_GetResultType
		 * 
		 */
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