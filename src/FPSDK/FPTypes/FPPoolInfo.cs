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

using System.Runtime.InteropServices;

namespace EMC.Centera.SDK.FPTypes
{
    [StructLayout(LayoutKind.Sequential)]
	public struct FPPoolInfo
	{
		public FPInt     poolInfoVersion;   /* The current version of this structure (2)          */
		public FPLong    capacity;          /* The total capacity of the pool, in bytes.          */
		public FPLong    freeSpace;         /* The total free space of the pool, in bytes.        */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public string clusterID;         /* The cluster identifier of the pool.                */
			
		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public string clusterName;       /* The name of the cluster.                           */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public string version;           /* The version of the pool server software.           */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=256 )]
		public string replicaAddress;    /* The pool address (see FPPool_Open()) where the C-Clips are replicated; empty if there is no replication. */ 
	}



}