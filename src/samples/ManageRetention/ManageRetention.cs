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

namespace ManageRetention
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class ManageRetention
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

				FPLogger.ConsoleMessage("\nEnter the content address of the clip to manage: ");
				String clipID = Console.ReadLine();

				FPClip clipRef = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);

				FPLogger.ConsoleMessage("\nThe following RetentionClasses are configured in the pool:\n");

				foreach(FPRetentionClass rc in myPool.RetentionClasses)
				{
					FPLogger.ConsoleMessage(rc.ToString());
				}

				FPLogger.ConsoleMessage("\nEnter RetentionClass or Period (in seconds) to be set: ");
				String newValue = Console.ReadLine();


				if (myPool.RetentionClasses.ValidateClass(newValue))
				{
					clipRef.RetentionClassName = newValue;
				}
				else
				{
					clipRef.RetentionPeriod = new TimeSpan(0, 0, int.Parse(newValue));
				}
				
				clipID = clipRef.Write();

				FPLogger.ConsoleMessage("\nThe new content address for the clip is " + clipID);
				FPLogger.ConsoleMessage("\nCluster time " + myPool.ClusterTime);
				FPLogger.ConsoleMessage("\nClip created " + clipRef.CreationDate);
				FPLogger.ConsoleMessage("\nThe retention period " + clipRef.RetentionPeriod);
				FPLogger.ConsoleMessage("\nThe retention will expire on " + clipRef.RetentionExpiry);

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
