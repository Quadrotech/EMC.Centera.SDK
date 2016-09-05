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

	/// <summary> 
	///A RetentionClass object.
	///@author Graham Stuart
	///@version
	 /// </summary>
	public class FPRetentionClass : FPObject
	{
		FPRetentionClassRef theClass;

		/// <summary>
		///Create a RetentionClass using an existing FPRetentionClassRef.
		///
		///@param	c	The FPRetentionClassRef.
		 /// </summary>
		internal FPRetentionClass(FPRetentionClassRef c)
		{
			theClass = c;
			AddObject(theClass, this);
		}


		/// <summary>
		///The name of the RetentionClass.
		///See API Guide: FPRetentionClass_GetName
		///
		 /// </summary>
		public string Name
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

						Native.RetentionClass.GetName(this, ref outString, ref len);
					} while (len > bufSize);

                    return Encoding.UTF8.GetString(outString, 0, (int)len - 1);
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		///The Period (as a TimeSpan) associated with this RetentionClass. See API Guide: FPRetentionClass_GetPeriod
		///
		 /// </summary>
		public TimeSpan Period => new TimeSpan(0, 0, (int) Native.RetentionClass.GetPeriod(this));

	    /// <summary>
		///Explicitly close the RetentionClass. See API Guide: FPRetentionClass_Close
		///
		 /// </summary>
		public override void Close() 
		{
			if (theClass != 0)
			{
				RemoveObject(theClass);
				Native.RetentionClass.Close(theClass);
				theClass = 0;
			}
		}
		

		/// <summary>
		///Implicit conversion between an FPRetentionClass and an FPRetentionClassRef.
		///
		///@param c	An FPRetentionClass object.
		///@return	The FPRetentionClassRef associated with this object.
		 /// </summary>
		public static implicit operator FPRetentionClassRef(FPRetentionClass c) 
		{
			return c.theClass;
		}

		/// <summary>
		///Implicit conversion between an FPRetentionClassRef and an FPRetentionClass. 
		///
		///@param	classRef	An FPRetentionClassRef.
		///@return	The FPRetentionClass object associated with it.
		 /// </summary>
		public static implicit operator FPRetentionClass(FPRetentionClassRef classRef) 
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

		/// <summary>
		///Get a string representation of this RetentionClass - the RetentionClass name and Period.
		///
		///@return The string representation of this object.
		 /// </summary>
		public override string ToString()
		{
			return Name + "("
                + Period.Days + "d "
                + Period.Hours + "h "
                + Period.Minutes + "m "
                + Period.Seconds + "s "
                + Period.Milliseconds + "ms)";

		}  // end of class RetentionClass

	}
}
