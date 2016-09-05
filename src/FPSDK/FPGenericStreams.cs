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
using System.Runtime.InteropServices;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
	/// <summary> 
	///FPStreamInfo contains the control information and data buffer that is used to transfer data
	///between the application and the SDK.
	///@author Graham Stuart
	///@version
	 /// </summary>

    internal sealed class SDK 
	{
		[DllImport("FPLibrary.dll")]
		public static extern		   FPStreamRef FPStream_CreateGenericStream (FPCallback prepareProc,
			FPCallback completeProc,
			FPCallback setMarkerProc,
			FPCallback resetMarkerProc,
			FPCallback closeProc,
			IntPtr userData);
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void         FPStream_Close (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern unsafe	    FPStreamInfo* FPStream_GetInfo (FPStreamRef pStream);
		
		[DllImport("FPLibrary.dll")]
		public static extern		   IntPtr		 FPStream_PrepareBuffer (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   	IntPtr	 FPStream_Complete (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void          FPStream_SetMark (FPStreamRef pStream) ;
		
		[DllImport("FPLibrary.dll")]
		public static extern		   void          FPStream_ResetMark (FPStreamRef pStream) ;

	}


    

   

   
}


