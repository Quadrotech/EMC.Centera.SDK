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
using System.Globalization;
using EMC.Centera;
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;
using System.Collections;



namespace QueryCluster
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class QueryCluster
    {
        static String defaultCluster = "192.168.242.131";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //int exitCode = 0;
            try
            {

                FPLogger.ConsoleMessage("\nCluster to connect to [" + defaultCluster + "] :");
                String clusterAddress = Console.ReadLine();
                if ("" == clusterAddress)
                {
                    clusterAddress = defaultCluster;
                }


                // New feature for 2.3 lazy pool open
                FPPool.OpenStrategy = FPMisc.LAZY_OPEN;

                // open cluster connection
                FPPool thePool = new FPPool(clusterAddress);

                // Check that query is supported on this connection; query can be 
                // disabled by the Centera administrator
                if (!thePool.QueryAllowed)
                {
                    FPLogger.ConsoleMessage("\nQuery is not supported for this pool connection.");
                    return;
                }

                FPLogger.ConsoleMessage("\nQuery capability is enabled for this pool connection.");

                FPQuery myQuery = new FPQuery(thePool);

                //FPLogger.ConsoleMessage("\nEnter the start time (YYYY/MM/DD HH:MM:SS) : ");
                //myQuery.StartTime = FPMisc.GetDateTime(Console.ReadLine());

                //FPLogger.ConsoleMessage("\nEnter the end time (YYYY/MM/DD HH:MM:SS) : ");
                //myQuery.EndTime = FPMisc.GetDateTime(Console.ReadLine());
                myQuery.UnboundedStartTime = true;
                myQuery.UnboundedEndTime = true;

                // New for 2.3 two types to query for EXISTING or DELETED. We'll get both.
                myQuery.Type = FPMisc.QUERY_TYPE_EXISTING | FPMisc.QUERY_TYPE_DELETED;

                // We want to retrieve two fields during our query, select them beforehand.
                myQuery.SelectField("creation.date");
                myQuery.SelectField("modification.date");

                myQuery.Execute();

                FPLogger.ConsoleMessage("\nCurrent Cluster Time is " + thePool.ClusterTime);

                //	Retrieve Query results one at a time, checking for incomplete gaps. 
                //  Gaps can occur when data is temporarily unavailable on the Centera backend
                int count = 0;
                int queryStatus = 0;
                ArrayList resultsCollection = new ArrayList();

                do
                {
                    FPQueryResult queryResult = new FPQueryResult();
                    queryStatus = myQuery.FetchResult(ref queryResult, -1);

                    switch (queryStatus)
                    {
                        case FPMisc.QUERY_RESULT_CODE_OK:
                            FPLogger.ConsoleMessage("\n" + queryResult +
                                " creation date: " + queryResult.GetField("creation.date") +
                                " modification time on clip : " + queryResult.GetField("modification.date"));
                            count++;

                            // And store in a collection for later use
                            resultsCollection.Add(queryResult);
                            break;

                        case FPMisc.QUERY_RESULT_CODE_INCOMPLETE: // One or more nodes on centera could not be queried.
                            FPLogger.ConsoleMessage("\nReceived FP_QUERY_RESULT_CODE_INCOMPLETE error, invalid C-Clip, trying again.");
                            break;

                        case FPMisc.QUERY_RESULT_CODE_COMPLETE: // Indicate error should have been received after incomplete error
                            FPLogger.ConsoleMessage("\nReceived FP_QUERY_RESULT_CODE_COMPLETE, there should have been a previous "
                                + "FP_QUERY_RESULT_CODE_INCOMPLETE error reported.");
                            break;

                        case FPMisc.QUERY_RESULT_CODE_ERROR: //Server error
                            FPLogger.ConsoleMessage("\nreceived FP_QUERY_RESULT_CODE_ERROR error, retrying again");
                            break;

                        case FPMisc.QUERY_RESULT_CODE_PROGRESS: // Waiting on results coming back
                            FPLogger.ConsoleMessage("\nreceived FP_QUERY_RESULT_CODE_PROGRESS, continuing.");
                            break;

                        case FPMisc.QUERY_RESULT_CODE_END:
                            FPLogger.ConsoleMessage("\nEnd of query.");
                            break;

                        case FPMisc.QUERY_RESULT_CODE_ABORT: // server side issue or start time is later than server time.
                            FPLogger.ConsoleMessage("\nQuery ended abnormally - FP_QUERY_RESULT_CODE_ABORT error.");
                            break;

                        default: // Unknown error, stop running query
                            FPLogger.ConsoleMessage("\nAborting - received unkown error: " + queryStatus);
                            break;
                    }
                } while (queryStatus != FPMisc.QUERY_RESULT_CODE_END && queryStatus != FPMisc.QUERY_RESULT_CODE_ABORT);


                FPLogger.ConsoleMessage("\nTotal number of clips \t" + count);

                foreach (FPQueryResult q in resultsCollection)
                {
                    FPLogger.ConsoleMessage("\n" + q.ToString());
                }

                myQuery.Close();

                // Always close the Pool connection when finished.  Not a
                // good practice to open and close for each transaction.
                FPLogger.ConsoleMessage("\nClosing connection to Centera cluster (" + thePool.ClusterName + ")");
                thePool.Close();

            }
            catch (FPLibraryException e)
            {
                ErrorInfo err = e.errorInfo;
                FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
            }

        }
    }

}
