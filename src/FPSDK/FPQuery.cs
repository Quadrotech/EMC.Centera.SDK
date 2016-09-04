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
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{	

	/** 
	 * An object representing a query to an existing pool.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPQuery : FPObject
	{
	    readonly FPPoolRef thePool;
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
			theExpression = Native.QueryExpression.Create();

			// Set default values - unbounded query on existing objects
			Native.QueryExpression.SetStartTime(theExpression, 0);
			Native.QueryExpression.SetEndTime(theExpression, (FPLong) (-1));
			Native.QueryExpression.SetType(theExpression, (FPInt) FPMisc.QUERY_TYPE_EXISTING);
		}

		/**
		 * Construct a Query using an existing FPPoolQueryRef. Used internally when implicitly converting
		 * an FPPoolQueryRef to a Query.
		 *
		 * @param	q	The existing FPPoolQueryRef
		 */ 

		internal FPQuery(FPPoolQueryRef q)
		{
			thePool = Native.PoolQuery.GetPoolRef(q);
			theQuery = q;
			theExpression = 0;
		}

		/**
		 * Implicit conversion between a Query and an FPPoolQueryRef
		 *
		 * @param	q	The Query.
		 * @return	The FPPoolQueryRef associated with the object
		 */
		public static implicit operator FPPoolQueryRef(FPQuery q) 
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
				Native.PoolQuery.Close(theQuery);
				theQuery = 0;
			}

			if (theExpression != 0)
			{
				Native.QueryExpression.Close(theExpression);
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
			theQuery = Native.PoolQuery.Open(thePool, theExpression);
		}


		/**
		 * The Pool object associated with this Query.
		 * See API Guide: FPPoolQuery_GetPoolRef
		 *
		 */
		public FPPool FPPool => Native.PoolQuery.GetPoolRef(this);

	    /**
		 * Retrieve the next member of the result set for the current open Query. See API Guide: FPPoolQuery_FetchResult
		 * 
		 * @param   outResult	The next available FPQueryResult in the FPQuery.
		 * @param	inTimeout	The timeout value to wait for the next result.
		 * @return	The ResultCode of the operation.
		 */
		public int FetchResult(ref FPQueryResult outResult, int inTimeout) 
		{
			outResult.Result = Native.PoolQuery.FetchResult(theQuery, (FPInt) inTimeout);
			return (int) Native.QueryResult.GetResultCode(outResult);
		}

		/**
		 * The Start Time for the Query to be executed.
		 *
		 */
		public DateTime StartTime
		{
			get
			{
				return FPMisc.GetDateTime(Native.QueryExpression.GetStartTime(theExpression));
			}

			set
			{
				Native.QueryExpression.SetStartTime(theExpression, FPMisc.GetTime(value));
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
                if (UnboundedEndTime)
                    return FPPool.ClusterTime;
                else
                    return FPMisc.GetDateTime(Native.QueryExpression.GetEndTime(theExpression));
			}

			set
			{
				Native.QueryExpression.SetEndTime(theExpression, FPMisc.GetTime(value));
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
                return (Native.QueryExpression.GetStartTime(theExpression) == 0);
            }

            set
            {
                Native.QueryExpression.SetStartTime(theExpression, 0);
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
                return (Native.QueryExpression.GetEndTime(theExpression) == (FPLong) (-1));
            }

            set
            {
                Native.QueryExpression.SetEndTime(theExpression, (FPLong) (-1));
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
				return (int) Native.QueryExpression.GetType(theExpression);
			}
			set
			{
				Native.QueryExpression.SetType(theExpression, (FPInt) value);
			}
		}


		/**
		 * Add a Clip level attrbute to the list of attributes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_SelectField
		 * 
		 * @param	inFieldName	The Attribute Name.
		 */
		public void SelectField(string inFieldName) 
		{
			Native.QueryExpression.SelectField(theExpression, inFieldName);
		}

		/**
		 * Remove a Clip level attrbute from the list of attributes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_DeselectField
		 * 
		 * @param	inFieldName	The Attribute Name.
		 */
		public void DeselectField(string inFieldName) 
		{
			Native.QueryExpression.DeselectField(theExpression, inFieldName);
		}

		/**
		 * Determine if an Attribute is in the list of selected attibutes to be retrieved in the Query to be executed.
		 * See API Guide: FPQueryExpression_IsFieldSelected
		 * 
		 * @param	inFieldName	The Attribute Name.
		 * @return	Boolean representing the Selected state for the Atrribute in the Query Expression.
		 */
		public bool IsSelected(string inFieldName) 
		{
			if (Native.QueryExpression.IsFieldSelected(theExpression, inFieldName) == FPBool.True)
				return true;
			else
				return false;
		}
	}

    // end of class QueryResult

}