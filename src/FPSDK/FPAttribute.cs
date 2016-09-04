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
	 * A Name-Value pair containing String representations of the Name and Value of an attribute..
	 * @author Graham Stuart
	 * @version
	 */

    public interface IFPAttribute
    {
        String Name { get; }
        String Value { get; }
        string ToString();
    }

    public class FPAttribute : IFPAttribute
    {
		private String myName;
		private String myValue;

		/**
		 * The Attribute Name
		 */
		public String Name
		{
			get
			{
				return myName;
			}
		}

		/**
		 * The Attribute Value
		 */
		public String Value
		{
			get
			{
				return myValue;
			}
		}

		/**
		 * Create an FPAttribute object using the name-value string parameters.
		 * 
		 * @param n	FPAttribute Name.
		 * @param v	FPAttribute Value.
		 */
		public FPAttribute(String n, String v)
		{
			myName = n;
			myValue = v;
		}

		public override string ToString()
		{
			return "Name (" + Name + ") Value (" + Value + ")";
		}

	}

	/**
	 * A collection of Attributes existing on a Tag or DescriptionAttributes on a Clip..
	 */
	public class FPAttributeCollection:ArrayList
	{
		internal FPAttributeCollection(FPTag t) 
		{ 
			for (int i = 0; i < t.NumAttributes; i++)
			{
				this.Add(t.GetAttributeByIndex(i));
			}
		} 
                       
		internal FPAttributeCollection(FPClip c) 
		{ 
			for (int i = 0; i < c.NumAttributes; i++)
			{
				this.Add(c.GetAttributeByIndex(i));
			}
		} 
	}
}