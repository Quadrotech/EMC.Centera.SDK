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
using System.IO;
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;


namespace RestoreClip
{
    /// <summary>
    /// Store content contained in a file to the Centera.
    /// </summary>
    class RestoreClip
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
                FPLogger.ConsoleMessage("\nCluster to connect to [" + defaultCluster + "] : ");
                String clusterAddress = System.Console.ReadLine();
                if ("" == clusterAddress) 
                {
                    clusterAddress = defaultCluster;
                }

                FPLogger.ConsoleMessage("\nEnter the name of the file containing the clip to be restored: ");
                String fileName = Console.ReadLine();
				if (!fileName.EndsWith(".xml"))
				{
					FPLogger.ConsoleMessage("\nInvalid file name format - should be <clipID>.xml");
					return;
				}

				String clipID = fileName.Substring(0, fileName.Length - 4);
                FPPool thePool = new FPPool(clusterAddress);

                FPStream fpStream = new FPStream(fileName, 16 * 1024);

				// Create the clip
                FPClip clipRef = thePool.ClipRawOpen(clipID, fpStream, FPMisc.OPTION_DEFAULT_OPTIONS);

				FPTag t = clipRef.NextTag;
				fpStream = new FPStream(clipID + ".Tag.0", 16 * 1024);
				t.BlobWrite(fpStream, FPMisc.OPTION_DEFAULT_OPTIONS);
				fpStream.Close();
				t.Close();

				t = clipRef.NextTag;
				fpStream = new FPStream(clipID + ".Tag.1", 16 * 1024);
				t.BlobWrite(fpStream, FPMisc.OPTION_DEFAULT_OPTIONS);
				fpStream.Close();
				t.Close();

				clipRef.Write();
				clipRef.Close();
                thePool.Close();

				FPLogger.ConsoleMessage("\nClip and Blobs successfully restored to Cluster");
            } 
            catch (FPLibraryException e) 
            {
                ErrorInfo err = e.errorInfo;                
				FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
            }
        }
    }
}

