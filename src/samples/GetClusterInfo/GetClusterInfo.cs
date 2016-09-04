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
using System.Threading;

namespace GetClusterInfo
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class GetClusterInfo
	{
		static String clusterAddress = "128.221.200.56?c:\\us1.pea";

        static FPInt myLoggingCB(String inMessage)
        {
            FPLogger.ConsoleMessage("\nThis is a test log >> " + inMessage + " ***");
            return 0;
        }
        

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try 
			{
				FPLogger.ConsoleMessage("\nEnter cluster to connect to [" + clusterAddress + "]: ");
				String answer = Console.ReadLine();

				if (answer != "")
					clusterAddress = answer;

                // The 3.2 SDK introduced dynamic logging features
                FPLogger log = new FPLogger();
                log.LogPath = "c:\\mySDKLog.txt";
                log.Start();
         

				// New in 3.1 - applications can be registered
				FPPool.RegisterApplication("GetClusterInfo", "3.1");

				using (FPPool myPool = new FPPool(clusterAddress))
				{

					FPLogger.ConsoleMessage("\nCluster time is " + myPool.ClusterTime);

					// Display the pool information individually. We could just have used
					//
					//         FPLogger.ConsoleMessage(myPool);
					//
					// as the pool object overrides toString() to print exactly this.
					FPLogger.ConsoleMessage(
                        "\nPool Information" +
                        "\n================" + 
						"\nCluster ID:                            " + myPool.ClusterID +
						"\nCluster Name:                          " + myPool.ClusterName +
						"\nCentraStar software version:           " + myPool.CentraStarVersion +
						"\nSDK version:                           " + FPPool.SDKVersion +
						"\nCluster Capacity (Bytes):              " + myPool.Capacity +
						"\nCluster Free Space (Bytes):            " + myPool.FreeSpace +
						"\nCluster Replica Address:               " + myPool.ReplicaAddress);

                    FPLogger.Stop();
                    FPLogger.RegisterCallback(myLoggingCB);
                    log.Start();                    
                    FPLogger.Log(FPLogLevel.Error, "logged via our callback i.e. the Console");

                    FPLogger.Stop();
                    log.DisableCallback = FPBool.True;
                    log.LogPath = "c:\\mySDKLog2.txt";
                    log.Start();

                    // Print out the information on the Governors if we have Advanced Retention
                    if (myPool.HoldSupported)
                    {
                        FPLogger.ConsoleMessage(
                            "\nVariable Retention Min                 " + myPool.VariableRetentionMin +
                            "\nVariable Retention Max                 " + myPool.VariableRetentionMax +
                            "\nFixed Retention Min                    " + myPool.FixedRetentionMin +
                            "\nFixed Retention Max                    " + myPool.FixedRetentionMax +
                            "\nDefault Retention                      " + myPool.RetentionDefault);
                    }

                    FPLogger.ConsoleMessage("\nRetention classes configured on the cluster:");

					foreach(FPRetentionClass rc in myPool.RetentionClasses)
					{
						FPLogger.ConsoleMessage("\n\t" + rc);
					}

					// Check for a ProfileClip associated with the pool connection.
					try
					{
						String clipID = myPool.ProfileClip;
						FPLogger.ConsoleMessage("\nProfile Clip id " + clipID + " has metadata: ");

						using (FPClip clipRef = myPool.ClipOpen(clipID, FPMisc.OPEN_FLAT))
						{
							foreach (FPAttribute attr in clipRef.Attributes)
							{
								FPLogger.ConsoleMessage("\n\t" + attr);
							}
						}
					}
					catch (FPLibraryException e) 
					{
						ErrorInfo err = e.errorInfo;

						if (e.errorInfo.error == FPMisc.PROFILECLIPID_NOTFOUND_ERR)
							FPLogger.ConsoleMessage("\nNo Profile Clip exists for this application.\n");
						else if (e.errorInfo.error == FPMisc.SERVER_ERR)
							FPLogger.ConsoleMessage("\nThe Centera cluster you are attempting to access may not support profile clips.\n");
						else
							throw e;
					}

                    log.Save("c:\\myLogState.out");
				}
			} 
			catch (FPLibraryException e) 
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: " + e);
			}
		}

	}
}
