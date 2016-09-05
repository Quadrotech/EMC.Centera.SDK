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
using System.Collections;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{

    /// <summary> 
	///A collection of RetentionClass objects stored in an ArrayList.
	///@author Graham Stuart
	///@version
	 /// </summary>
    public class FPRetentionClassCollection:ArrayList
    {
        private FPRetentionClassContextRef myContext;

        internal FPRetentionClassContextRef RCContext
        {
            get
            {
                return myContext;
            }
            set
            {
                myContext = value;
            }
        }

        internal FPRetentionClassCollection(FPPool p) 
        { 
            RCContext = Native.Pool.GetRetentionClassContext(p);

            FPRetentionClassRef c = Native.RetentionClassContext.GetFirstClass(RCContext);

            while(c != 0)
            {
                Add(new FPRetentionClass(c));
                c = Native.RetentionClassContext.GetNextClass(RCContext);
            }
        }	
        /// <summary>
		///Get the Period associated with a named RetentionClass in the RetentionClassList.
		///
		///@param	inName	The name of the RetentionClass in the list to get the period for.
		///@return	The period (as a TimeSpan) associated with the named RetentionClass.
		 /// </summary>
        public TimeSpan GetPeriod(string inName) 
        {
            if (ValidateClass(inName))
            {
                FPRetentionClassRef theRef = Native.RetentionClassContext.GetNamedClass(RCContext, inName);
                return new TimeSpan(0, 0, (int) Native.RetentionClass.GetPeriod(theRef));
            }
            else
            {
                return new TimeSpan(0);
            }
        }

        /// <summary>
		///Get a named RetentionClass from the RetentionClassList. The class must exist or an exception is thrown.
		///See API Guide: FPRetentionClassContext_GetNamedClass
		///
		///@param	inName	The name of the RetentionClass to get from the list.
		///@return	The named RetentionClass
		 /// </summary>
        public FPRetentionClass GetClass(string inName) 
        {
            FPRetentionClass retVal = null;
            
            foreach (FPRetentionClass rc in this)
            {
                if (rc.Name.CompareTo(inName) == 0)
                {
                    retVal = rc;
                    break;
                }
            }

            if (retVal == null)
                throw new FPLibraryException("Invalid Retention Class name", -10019);
            else
                return retVal;
        }

        /// <summary>
		///Check for the existence of a named RetentionClass in the RetentionClassList.
		///See API Guide: FPRetentionClassContext_GetNamedClass
		///
		///@param	inName	The name of the RetentionClass to get from the list.
		///@return	The named RetentionClass
		 /// </summary>
        public bool ValidateClass(string inName)
        {
            foreach (FPRetentionClass rc in this)
            {
                if (rc.Name.CompareTo(inName) == 0)
                    return true;
            }

            return false;
        }

    }
}