/******************************************************************************

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
using System.Collections;
using System.Runtime.InteropServices;
using EMC.Centera.FPTypes;

namespace EMC.Centera.SDK
{	

	/** 
	 * An object representing a query to an existing pool.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPQuery : FPObject
	{
		FPPoolRef thePool;
		FPPoolQueryRef theQuery;
		FPQueryExpressionRef theExpression;

		/**
		 * Construct a Query for an existing Pool. Default values set for an unbounded time query on existing objects.
		 *
		 * @param	p	The existing Pool object.
		 */
		public FPQuery(FPPool p)
		{
			thePool = p;
			theQuery = 0;
			theExpression = FPApi.QueryExpression.Create();

			// Set default values - unbounded query on existing objects
			FPApi.QueryExpression.SetStartTime(theExpression, 0);
			FPApi.QueryExpression.SetEndTime(theExpression, (EMC.Centera.FPTypes.FPLong) (-1));
			FPApi.QueryExpression.SetType(theExpression, (FPInt) EMC.Centera.SDK.FPMisc.QUERY_TYPE_EXISTING);
		}

		/**
		 * Construct a Query using an existing FPPoolQueryRef. Used internally when implicitly converting
		 * an FPPoolQueryRef to a Query.
		 *
		 * @param	q	The existing FPPoolQueryRef
		 */ 

		internal FPQuery(FPPoolQueryRef q)
		{
			thePool = FPApi.PoolQuery.GetPoolRef(q);
			theQuery = q;
			theExpression = 0;
		}

		/**
		 * Implicit conversion between a Query and an FPPoolQueryRef
		 *
		 * @param	q	The Query.
		 * @return	The FPPoolQueryRef associated with the object
		 */
		static public implicit operator FPPoolQueryRef(FPQuery q) 
		{
			return q.theQuery;
		}

		/**
		 * Explicitly close this Query. See API Guide: FPPoolQuery_Close
		 */
		public override void Close() 
		{
			if (theQuery != 0)
			{
				FPApi.PoolQuery.Close(theQuery);
				theQuery = 0;
			}

			if (theExpression != 0)
			{
				FPApi.QueryExpression.Close(theExpression);
				theExpression = 0;
			}
		}

		/**
		 * Executes this Query on the associated Pool based on the associated QueryExpression. 
		 * See API Guide: FPPoolQuery_Open
		 *
		 */
		public void Execute()
		{
			theQuery = FPApi.PoolQuery.Open(thePool, theExpression);
		}


		/**
		 * The Pool object associated with this Query.
		 * See API Guide: FPPoolQuery_GetPoolRef
		 *
		 */
		public FPPool FPPool
		{
			get
			{
				return FPApi.PoolQuery.GetPoolRef(this);
			}
		}

		/**
		 * Retrieve the next member of the result set for the current open Query. See API Guide: FPPoolQuery_FetchResult
		 * 
		 * @param   outResult	The next available FPQueryResult in the FPQuery.
		 * @param	inTimeout	The timeout value to wait for the next result.
		 * @return	The ResultCode of the operation.
		 */
		public int FetchResult(ref FPQueryResult outResult, int inTimeout) 
		{
			outResult.Result = FPApi.PoolQuery.FetchResult(theQuery, (FPInt) inTimeout);
			return (int) FPApi.QueryResult.GetResultCode(outResult);
		}

		/**
		 * The Start Time for the Query to be executed.
		 *
		 */
		public DateTime StartTime
		{
			get
			{
				return FPMisc.GetDateTime(FPApi.QueryExpression.GetStartTime(theExpression));
			}

			set
			{
				FPApi.QueryExpression.SetStartTime(theExpression, FPMisc.GetTime(value));
			}
		}

		/**
		 * The End Time for the Query to be executed.
		 *
		 */
		public DateTime EndTime
		{
			get
			{
                if (this.UnboundedEndTime)
                    return FPPool.ClusterTime;
                else
                    return FPMisc.GetDateTime(FPApi.QueryExpression.GetEndTime(theExpression));
			}

			set
			{
				FPApi.QueryExpression.SetEndTime(theExpression, FPMisc.GetTime(value));
			}
		}

        /**
         * The Start Time for the Query to be executed is unbounded i.e. the Java Epoch.
         *
         */
        public bool UnboundedStartTime
        {
            get
            {
                return (FPApi.QueryExpression.GetStartTime(theExpression) == 0);
            }

            set
            {
                FPApi.QueryExpression.SetStartTime(theExpression, 0);
            }
        }

        /**
         * The End Time for the Query to be executed is unbounded i.e. the current cluster time.
         *
         */
        public bool UnboundedEndTime
        {
            get
            {
                return (FPApi.QueryExpression.GetEndTime(theExpression) == (FPLong) (-1));
            }

            set
            {
                FPApi.QueryExpression.SetEndTime(theExpression, (FPLong) (-1));
            }
        }

        /**
         * The Type of Query to be executed i.e. the type of clips to query for:
         * 
         * FPMisc.QUERY_TYPE_DELETED
         * FPMisc.QUERY_TYPE_EXISTING
         * FPMisc.QUERY_TYPE_DELETED | FPMisc.QUERY_TYPE_EXISTING
         * 
         */
		public int Type
		{
			get
			{
				return (int) FPApi.QueryExpression.GetType(theExpression);
			}
			set
			{
				FPApi.QueryExpression.SetType(theExpression, (FPInt) value);
			}
		}


		/**
		 * Add a Clip level attrbute to the list of attributes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_SelectField
		 * 
		 * @param	inFieldName	The Attribute Name.
		 */
		public void SelectField(String inFieldName) 
		{
			FPApi.QueryExpression.SelectField(theExpression, inFieldName);
		}

		/**
		 * Remove a Clip level attrbute from the list of attributes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_DeselectField
		 * 
		 * @param	inFieldName	The Attribute Name.
		 */
		public void DeselectField(String inFieldName) 
		{
			FPApi.QueryExpression.DeselectField(theExpression, inFieldName);
		}

		/**
		 * Determine if an Attribute is in the list of selected attibutes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_IsFieldSelected
		 * 
		 * @param	inFieldName	The Attribute Name.
		 * @return	Boolean representing the Selected state for the Atrribute in the Query Expression.
		 */
		public bool IsSelected(String inFieldName) 
		{
			if (FPApi.QueryExpression.IsFieldSelected(theExpression, inFieldName) == FPBool.True)
				return true;
			else
				return false;
		}
	}

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
		static public implicit operator FPQueryResultRef(FPQueryResult q) 
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
                FPApi.QueryResult.Close(theResult);
				theResult = 0;
			}
		}

		/**
		 * The ID of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetClipID
		 * 
		 */
		public String ClipID 
		{
			get
			{
				StringBuilder outClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);
			
				FPApi.QueryResult.GetClipID(theResult, outClipID);

				return outClipID.ToString();
			}
		}

		/**
		 * The Timestamp of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetTimestamp
		 *
		 */
		public DateTime Timestamp
		{
			get
			{
				return FPMisc.GetDateTime(FPApi.QueryResult.GetTimestamp(theResult));
			}
		}

		/**
		 * Retrieve the value of an Attribute of the Clip associated with this result.
		 * See API Guide: FPQueryResult_GetField
		 * 
		 * @param	inAttrName	The name of the attribute to retieve from the current member of the result set.
		 * @return	The value of the Attribute  of the Clip associated with the current member of the result set.
		 */
		public String GetField(String inAttrName) 
		{
            byte[] outString;
			FPInt bufSize = 0;
			FPInt len = 0;

			do
			{
				bufSize += FPMisc.STRING_BUFFER_SIZE;
				len = bufSize;
				outString = new byte[(int) bufSize];

				FPApi.QueryResult.GetField(theResult, inAttrName, ref outString, ref len);
			} while (len > bufSize);

            return Encoding.UTF8.GetString(outString, 0, (int)len - 1);

		}

		/**
		 * The ResultCode indicating the status of the Query execution.
		 * See API Guide: FPQueryResult_GetResultCode
		 * 
		 */
		public int ResultCode
		{
			get
			{
				return (int) FPApi.QueryResult.GetResultCode(theResult);
			}
		}

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
				if (FPApi.QueryResult.GetType(theResult) == (FPInt) FPMisc.QUERY_TYPE_EXISTING)
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


	}  // end of class QueryResult

}