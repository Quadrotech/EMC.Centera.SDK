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
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;

namespace EligibleForDeletion
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class EligibleForDeletion
	{
		static String clusterAddress = "128.221.200.64";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try 
			{
				FPLogger.ConsoleMessage("\nCluster to connect to [" + clusterAddress + "]: ");
				String answer = Console.ReadLine();

				if (answer != "")
					clusterAddress = answer;

				FPPool myPool = new FPPool(clusterAddress);

				FPLogger.ConsoleMessage("\nEnter the content address of the clip to check for deletion eligibility: ");
				String clipID = Console.ReadLine();

				FPClip clipRef = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);

				FPLogger.ConsoleMessage("\nCluster Time " + myPool.ClusterTime);
				FPLogger.ConsoleMessage("\nClip Creation Date " + clipRef.CreationDate + "\n");
				FPLogger.ConsoleMessage("\nRetention information for Clip " + clipRef + "\n");
				FPLogger.ConsoleMessage("\nRetention Class: " + clipRef.RetentionClassName);
				FPLogger.ConsoleMessage("\nRetention Period: " + clipRef.RetentionPeriod);
				FPLogger.ConsoleMessage("\nRetention Expiry: " + clipRef.RetentionExpiry);

				if (clipRef.RetentionExpiry < myPool.ClusterTime)
				{
					FPLogger.ConsoleMessage("\nThe clip is eligible for deletion.");
				}
				else
				{
					FPLogger.ConsoleMessage("\nThe clip is not eligible for deletion if Retention is enforced ");
					FPLogger.ConsoleMessage("\ni.e. Cluster is not a Basic Edition.");
				}

				clipRef.Close();

			} 
			catch (FPLibraryException e)
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
			}
			catch (Exception e)
			{
				FPLogger.ConsoleMessage("\nException thrown! " + e.Message);
			}
		}
	}
}
