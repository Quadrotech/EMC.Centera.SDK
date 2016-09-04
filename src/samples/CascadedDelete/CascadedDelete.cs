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
using EMC.Centera;
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;

namespace CascadedDelete
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class CascadedDelete
	{
		static String defaultCluster = "128.221.200.64";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try 
			{
				FPLogger.ConsoleMessage("\nCluster to connect to [" + defaultCluster + "] :");
				String clusterAddress = System.Console.ReadLine();
				if ("" == clusterAddress) 
				{
					clusterAddress = defaultCluster;
				}

				FPLogger.ConsoleMessage("\nEnter the CA of the content (and ancestors) to delete : ");
				String clipID = System.Console.ReadLine();
                
				FPPool thePool = new FPPool(clusterAddress);
				FPClip clipRef = thePool.ClipOpen(clipID, FPMisc.OPEN_FLAT);

				while (clipID.CompareTo("") != 0)
				{
					clipID = clipRef.GetAttribute("prev.clip");
					FPLogger.ConsoleMessage("\n\tDeleting clip " + clipRef.ClipID);

					thePool.ClipAuditedDelete(clipRef.ClipID, "Cascaded Delete example", FPMisc.OPTION_DELETE_PRIVILEGED);
					clipRef.Close();
					if (clipID.CompareTo("") != 0)
						clipRef = thePool.ClipOpen(clipID, FPMisc.OPEN_FLAT);
				}

			} 
			catch (FPLibraryException e) 
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
			}
		}
	}
}

