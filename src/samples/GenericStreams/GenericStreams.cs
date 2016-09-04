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
using System.IO;
using System.Text;
using EMC.Centera;
using EMC.Centera.SDK;
using EMC.Centera.FPTypes;
using System.Runtime.InteropServices;

namespace GenericStreams
{
	public class GenericStreams
	{

		[STAThread]
		static void Main(string[] args)
		{
			IntPtr userData = new IntPtr(0);

			try
			{
                FPPool myPool = new FPPool("128.221.200.56?c:\\pea\\emea1.pea");
				FPTag myTag;

                FPLogger log = new FPLogger();
                log.LogPath = "C:\\GenStreamsLog.txt";
                log.Start();

				// First we'll write a test clip to the Centera
				string fileName = "c:\\discovery.xml";
				FileInfo info = new FileInfo(fileName);

				FPClip myClip = myPool.ClipCreate("GenericStreamWrite_testClip");
				myTag = myClip.AddTag("testTag");

				FPGenericStream myStream = new FPGenericStream(File.OpenRead(fileName), userData);
				myStream.StreamLen = info.Length;
				myTag.BlobWrite(myStream);
				myTag.Close();
				myStream.Close();
				string clipID = myClip.Write();
				myClip.Close();
				FPLogger.ConsoleMessage("\nGenericStreamWrite test succeeded");

				// Now we will test reading it back from the Centera			{	
				myClip = myPool.ClipOpen(clipID, FPMisc.OPEN_ASTREE);
				myTag = myClip.NextTag;

				
                myStream = new FPGenericStream(File.OpenWrite("c:\\test.out"), userData);
				myStream.StreamLen = myTag.BlobSize;

				myTag.BlobRead(myStream);
				myStream.Close();
				FPLogger.ConsoleMessage("\nGenericStreamRead test succeeded for FileStream");

                
                // We could use a Memory stream. As it is a bi-directional stream we
                // need to use a ctor that specifies the direction we want to use.
                // We'll set up the space first.

                MemoryStream s = new MemoryStream((int)myTag.BlobSize);
                myStream = new FPGenericStream(s, FPStream.StreamDirection.OutputFromCentera, userData);
				
                myStream.StreamLen = myTag.BlobSize;
				
                myTag.BlobRead(myStream, FPMisc.OPTION_DEFAULT_OPTIONS);
                myStream.Close();
                FPLogger.ConsoleMessage("\nGenericStreamRead test succeeded for MemoryStream");
                myTag.Close();
				myClip.Close();


				myPool.Close();
			}
			catch (FPLibraryException e)
			{
				ErrorInfo err = e.errorInfo;
				FPLogger.ConsoleMessage("\nException thrown in FP Library: Error " + err.error + " " + err.message);
			}
		}
	}
}
