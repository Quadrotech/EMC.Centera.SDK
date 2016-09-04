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

namespace EventBasedRetention
{
    /// <summary>
    /// This sample demonstrates the methods associated with using Event Base Retention. It also
    /// deliberately performs invalid operations to show the exceptions that will be thrown.
    /// In order that these concepts can be demostrated quickly it uses very short values
    /// for the Retention Periods. This may cause problems if the Minimum Governors are set to
    /// high values.
    /// Throughout the sample we use EBRPeriods but you could equally use EBRClass.
    /// </summary>
    class EventBasedRetention
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
                string clipID;
                FPLogger.ConsoleMessage("\nEnter cluster to connect to [" + clusterAddress + "]: ");
                String answer = Console.ReadLine();

                if (answer != "")
                    clusterAddress = answer;

                using (FPPool myPool = new FPPool(clusterAddress))
                {
                    FPLogger.ConsoleMessage("\nEBR supported: " + myPool.EBRSupported);
                    FPLogger.ConsoleMessage("\nHold Supported: " + myPool.HoldSupported);
                    FPLogger.ConsoleMessage("\nHold allowed: " + myPool.HoldAllowed);

                    FPLogger.ConsoleMessage("\nCluster time is " + myPool.ClusterTime);
                    FPLogger.ConsoleMessage("\nMin retention periods: F:" + myPool.FixedRetentionMin + " V:" + myPool.VariableRetentionMin);

                    // By utilising the "using" clause we can allow for the
                    // dispose method to tidy the Pool connection
                    // Create a test clip that we will use to show the way EBR works
                    FPClip testClip = myPool.ClipCreate("testClip");
                    testClip.RetentionPeriod = new TimeSpan(0, 0, 1); // myPool.FixedRetentionMin;
                    testClip.EBRPeriod = new TimeSpan(0, 0, 4); // myPool.VariableRetentionMin;

                    clipID = testClip.Write();
                    FPLogger.ConsoleMessage("\nCreated test clip " + clipID);
                    FPLogger.ConsoleMessage("\nCluster time is         " + myPool.ClusterTime +
                                      "\nFixed Retention expires " + testClip.RetentionExpiry);
                    //testClip.Close();

                    // Check that we cannot delete the clip because the EBR Event has not fired
                    Thread.Sleep(1005);

                    try
                    {
                        myPool.ClipDelete(clipID);
                    }
                    catch (FPLibraryException e)
                    {
                        // You cannot delete a clip with EBR enabled before the EBREventhappens!
                        FPLogger.ConsoleMessage("\nTried to delete Clip before EBR Event occurred");
                        FPLogger.ConsoleMessage("\n\t" + e.errorInfo.error + " " + e.errorInfo);
                    }

                    // Here's how we fire the EBR Event on an existing clip that is effectively
                    // in a "wait" state. As the mutable metadata associated with
                    // the clip is being modified you need to open it in TREE mode.
                    // We could equally have used TriggerEBRPeriod here to set a different
                    // value to that which we originally used when putting the clip in the
                    // "wait" state.
                    //testClip = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);
                    testClip.TriggerEBREvent();
                    testClip.Write();
                    FPLogger.ConsoleMessage("\nEBR event triggered: expires on " + testClip.EBRExpiry);


                    // You cannot trigger the Event twice. If you do try and do this
                    // then you get an nasty -10160 FP_EBR_OVERRIDE_ERR. This seems to invalidate
                    // SDK reference objects so we can't demonstrate it!
                                      
                    Thread.Sleep(2005);
                    
                    // At this point, the EBR Event has occurred but the period
                    // has the not expired
                    try
                    {
                        FPLogger.ConsoleMessage("\nCluster time is         " + myPool.ClusterTime +
                                          "\nFixed Retention expires " + testClip.RetentionExpiry +
                                          "\nEBR expires             " + testClip.EBRExpiry);
                        myPool.ClipDelete(clipID);
                    }
                    catch (FPLibraryException e)
                    {
                        FPLogger.ConsoleMessage("\nTried to delete clip before EBR period expired");
                        FPLogger.ConsoleMessage("\n\t" + e.errorInfo.error + " " + e.errorInfo);
                    }
                    
                    // Let's wait and try it again later
                    Thread.Sleep(4005);

                    FPLogger.ConsoleMessage("\nCluster time is         " + myPool.ClusterTime +
                                       "\nFixed Retention expires " + testClip.RetentionExpiry +
                                       "\nEBR expires             " + testClip.EBRExpiry);
                    testClip.Close();
                    
                    myPool.ClipDelete(clipID);

                    FPLogger.ConsoleMessage("\nClip successfully deleted as EBR and fixed retention periods have expired");
                }
            }
            catch (FPLibraryException e)
            {
                if (e.errorInfo.error.Equals(FPMisc.OUT_OF_BOUNDS_ERR))
                {
                    FPLogger.ConsoleMessage("\nThe cluster is configured with Mimumum Retention governors " +
                        "whose values are too high to use this sample");
                }

                ErrorInfo err = e.errorInfo;
                FPLogger.ConsoleMessage("\nException thrown in FP Library: " + e);
            }
        }
    }
}
