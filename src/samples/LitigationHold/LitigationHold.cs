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

namespace LitigationHold
{
	/// <summary>
	/// This sample demonstrates the methods associated with the Litigation (Retention)
	/// Hold functionality and how they are used. It also deliberately performs invalid
	/// operations to show the exceptions that will be thrown.
	/// </summary>
	class Class1
	{
		static String clusterAddress = "128.221.200.64";

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

				// Check that we can use LitigationHold
				// This property checks that it is supported on the cluster
				// and also that we have the capability granted to use it
				if (!myPool.HoldAllowed)
				{
					FPLogger.ConsoleMessage("\nRetention Hold is not supported");
				}
				else
				{				
					FPClip testClip = myPool.ClipCreate("testClip");
					testClip.RetentionPeriod = new TimeSpan(0);
				
					string clipID = testClip.Write();

					FPLogger.ConsoleMessage("\nCreated testClip " + clipID + "\n");

					// We now have a test clip with no retention set, which would normally
					// mean that we can always delete it.
					// Let's place it under LitigationHold and see what happens.
					testClip.SetRetentionHold(true, "123456789");
					clipID = testClip.Write();
					testClip.Close();

					
					// Multiple holds can be set on a clip so let's set another one just to 
					// demonstrate how it is done. As the mutable metadata associated with
					// the clip is being modified you need to open it in TREE mode.
					testClip = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);
					testClip.SetRetentionHold(true, "ABCDEFG");
					clipID = testClip.Write();
					FPLogger.ConsoleMessage("\nClip " + clipID + " now under holds '123456789' and 'ABCDEFG'");

					try
					{
						myPool.ClipDelete(clipID);
					}
					catch (FPLibraryException e)
					{
						FPLogger.ConsoleMessage("\nTrying to delete clip that is under Litigation Hold: ");
						FPLogger.ConsoleMessage("\n\t" + e.errorInfo.error + " " + e.errorInfo);
					}

					//Release one of the holds
					testClip.SetRetentionHold(false, "123456789");
					clipID = testClip.Write();

					FPLogger.ConsoleMessage("\nReleased '123456789' hold but 'ABCDEFG' still active");
					try
					{
						myPool.ClipDelete(clipID);
					}
					catch (FPLibraryException e)
					{
						FPLogger.ConsoleMessage("\nTrying to delete clip that is under Litigation Hold: ");
						FPLogger.ConsoleMessage("\n\t" + e.errorInfo.error + " " + e.errorInfo);
					}

					// Release the other hold
					testClip.SetRetentionHold(false, "ABCDEFG");
					clipID = testClip.Write();
					FPLogger.ConsoleMessage("\nReleased 'ABCDEFG' hold");
					testClip.Close();

					myPool.ClipDelete(clipID);
					FPLogger.ConsoleMessage("\nAll holds released, delete was allowed on " + clipID);
				}
				myPool.Close();
			}
			catch (FPLibraryException e) 
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: " + e);
			}
		}
	}
}
