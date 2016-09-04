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
	 * A RetentionClass object.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPRetentionClass : FPObject
	{
		FPRetentionClassRef theClass;

		/**
		 * Create a RetentionClass using an existing FPRetentionClassRef.
		 *
		 * @param	c	The FPRetentionClassRef.
		 */
		internal FPRetentionClass(FPRetentionClassRef c)
		{
			theClass = c;
			AddObject(theClass, this);
		}


		/**
		 * The name of the RetentionClass.
		 * See API Guide: FPRetentionClass_GetName
		 * 
		 */
		public String Name
		{
			get
			{
				try
				{
                    byte[] outString;
					FPInt bufSize = 0;
					FPInt len = 0;

					do
					{
						bufSize += FPMisc.STRING_BUFFER_SIZE;
						len = bufSize;
                        outString = new byte[(int)bufSize];

						FPApi.RetentionClass.GetName(this, ref outString, ref len);
					} while (len > bufSize);

                    return Encoding.UTF8.GetString(outString, 0, (int)len - 1);
				}
				catch
				{
					throw;
				}
			}
		}

		/**
		 * The Period (as a TimeSpan) associated with this RetentionClass. See API Guide: FPRetentionClass_GetPeriod
		 *
		 */
		public TimeSpan Period
		{
			get
			{
				return new TimeSpan(0, 0, (int) FPApi.RetentionClass.GetPeriod(this));
			}
		}

		/**
		 * Explicitly close the RetentionClass. See API Guide: FPRetentionClass_Close
		 *
		 */
		public override void Close() 
		{
			if (theClass != 0)
			{
				RemoveObject(theClass);
				FPApi.RetentionClass.Close(theClass);
				theClass = 0;
			}
		}
		

		/**
		 * Implicit conversion between an FPRetentionClass and an FPRetentionClassRef.
		 *
		 * @param c	An FPRetentionClass object.
		 * @return	The FPRetentionClassRef associated with this object.
		 */
		static public implicit operator FPRetentionClassRef(FPRetentionClass c) 
		{
			return c.theClass;
		}

		/**
		 * Implicit conversion between an FPRetentionClassRef and an FPRetentionClass. 
		 *
		 * @param	classRef	An FPRetentionClassRef.
		 * @return	The FPRetentionClass object associated with it.
		 */
		static public implicit operator FPRetentionClass(FPRetentionClassRef classRef) 
		{
			// Find the relevant Tag object in the hastable for this FPTagRef
			FPRetentionClass classObject = null;

			if (SDKObjects.Contains(classRef))
			{
				classObject = (FPRetentionClass) SDKObjects[classRef];
			}
            else
            {
                throw new FPLibraryException("FPRetentionClassRef is not asscociated with an FPRetentionClass object", FPMisc.WRONG_REFERENCE_ERR);
            }

			return classObject;
		}

		/**
		 * Get a String representation of this RetentionClass - the RetentionClass name and Period.
		 * 
		 * @return The String representation of this object.
		 */
		public override String ToString()
		{
			return Name + "("
                + Period.Days + "d "
                + Period.Hours + "h "
                + Period.Minutes + "m "
                + Period.Seconds + "s "
                + Period.Milliseconds + "ms)";

		}  // end of class RetentionClass

	}
	
	/** 
	 * A collection of RetentionClass objects stored in an ArrayList.
	 * @author Graham Stuart
	 * @version
	 */
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
			RCContext = FPApi.Pool.GetRetentionClassContext(p);

			FPRetentionClassRef c = FPApi.RetentionClassContext.GetFirstClass(RCContext);

			while(c != 0)
			{
				this.Add(new FPRetentionClass(c));
				c = FPApi.RetentionClassContext.GetNextClass(RCContext);
			}
		}	
		/**
		 * Get the Period associated with a named RetentionClass in the RetentionClassList.
		 *
		 * @param	inName	The name of the RetentionClass in the list to get the period for.
		 * @return	The period (as a TimeSpan) associated with the named RetentionClass.
		 */
		public TimeSpan GetPeriod(String inName) 
		{
			if (this.ValidateClass(inName))
			{
				FPRetentionClassRef theRef = FPApi.RetentionClassContext.GetNamedClass(RCContext, inName);
				return new TimeSpan(0, 0, (int) FPApi.RetentionClass.GetPeriod(theRef));
			}
			else
			{
				return new TimeSpan(0);
			}
		}

		/**
		 * Get a named RetentionClass from the RetentionClassList. The class must exist or an exception is thrown.
		 * See API Guide: FPRetentionClassContext_GetNamedClass
		 * 
		 * @param	inName	The name of the RetentionClass to get from the list.
		 * @return	The named RetentionClass
		 */
		public FPRetentionClass GetClass(String inName) 
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

		/**
		 * Check for the existence of a named RetentionClass in the RetentionClassList.
		 * See API Guide: FPRetentionClassContext_GetNamedClass
		 * 
		 * @param	inName	The name of the RetentionClass to get from the list.
		 * @return	The named RetentionClass
		 */
		public bool ValidateClass(String inName)
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
