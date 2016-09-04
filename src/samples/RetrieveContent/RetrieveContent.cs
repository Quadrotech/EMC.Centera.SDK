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
using System.Runtime.InteropServices;

namespace RetrieveContent
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class RetrieveContent
	{
		static String defaultCluster = "us1cas1.centera.org?c:\\pea\\us1.pea";

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

				FPLogger.ConsoleMessage("\nEnter the CA of the content to be restored: ");
				String clipID = System.Console.ReadLine();
                
				FPPool thePool = new FPPool(clusterAddress);

                FPClip clipRef = new FPClip(thePool, clipID, FPMisc.OPEN_ASTREE);

				FPLogger.ConsoleMessage("\nThe clip retention expires on " + clipRef.RetentionExpiry);

				// Iterate across the  clip metadata
				foreach (FPAttribute attr in clipRef.Attributes)
				{
					FPLogger.ConsoleMessage(attr.ToString() + "\n");
				}

				// Iterate across the Tag (and their Attr) collections
				foreach (FPTag tg in clipRef.Tags)
				{
					FPLogger.ConsoleMessage(tg.ToString() + "\n");

					foreach (FPAttribute a in tg.Attributes)
					{
						FPLogger.ConsoleMessage(a.ToString() + "\n");
					}

				}

				FPTag t = (FPTag) clipRef.Tags[0];

				if (t.ToString() == "StoreContentObject")
				{
					String outfile = clipRef.ClipID + ".Tag." + 0;                    
					FPStream streamRef = new FPStream(outfile, "wb");

					t.BlobRead(streamRef, FPMisc.OPTION_DEFAULT_OPTIONS);
					streamRef.Close();
                
					FPLogger.ConsoleMessage("\nThe Blob has been stored into " + outfile);

					t.Close();

					t = (FPTag) clipRef.Tags[1];

					//Do the same for the second tag
					outfile = clipRef.ClipID + ".Tag." + 1;                    
					streamRef = new FPStream(outfile, "wb");

					t.BlobRead(streamRef, FPMisc.OPTION_DEFAULT_OPTIONS);
					streamRef.Close();
                
					FPLogger.ConsoleMessage("\nThe Blob has been stored into " + outfile);

					FPLogger.ConsoleMessage("\nThe CDF has been saved to " + clipRef.ClipID + ".xml");
					streamRef = new FPStream(clipRef.ClipID + ".xml", "wb");
					clipRef.RawRead(streamRef);

					// We could retrieve the blob into a buffer - we know we stored
					// a string in the StoreContent sample so let's get it back..
					int blobSize = (int) t.BlobSize;
					UTF8Encoding converter = new UTF8Encoding();

					byte[] buffer = new byte[blobSize];
					IntPtr source = Marshal.AllocHGlobal(blobSize);
					streamRef = new FPStream(source, blobSize, FPStream.StreamDirection.OutputFromCentera);                				
					t.BlobRead(streamRef);

					Marshal.Copy(source, buffer, 0, blobSize);
					Marshal.FreeHGlobal(source);
					FPLogger.ConsoleMessage("\n" + converter.GetString(buffer));
					streamRef.Close();


				}
				else
					FPLogger.ConsoleMessage("\nApplication Error: Not A C-Clip Created By StoreContent Example");
			} 
			catch (FPLibraryException e) 
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
			}
		}
	}
}

