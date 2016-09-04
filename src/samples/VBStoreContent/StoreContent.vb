'
'Copyright © 2006 EMC Corporation. All Rights Reserved
'
'This file is part of .NET wrapper for the Centera SDK.
'
'.NET wrapper is free software; you can redistribute it and/or modify it under
'the terms of the GNU General Public License as published by the Free Software
'Foundation version 2.
'
'In addition to the permissions granted in the GNU General Public License
'version 2, EMC Corporation gives you unlimited permission to link the compiled
'version of this file into combinations with other programs, and to distribute
'those combinations without any restriction coming from the use of this file.
'(The General Public License restrictions do apply in other respects; for
'example, they cover modification of the file, and distribution when not linked
'into a combined executable.)
'
'.NET wrapper is distributed in the hope that it will be useful, but WITHOUT ANY
'WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
'PARTICULAR PURPOSE. See the GNU General Public License version 2 for more
'details.
'
'You should have received a copy of the GNU General Public License version 2
'along with .NET wrapper; see the file COPYING. If not, write to:
'
' EMC Corporation 
' Centera Open Source Intiative (COSI) 
' 80 South Street
' 1/W-1
' Hopkinton, MA 01748 
' USA
'

Imports System
Imports System.Text
Imports System.IO
Imports EMC.Centera.SDK
Imports EMC.Centera.FPTypes




Module StoreContent
    Class StoreContent
        Private Shared defaultCluster As [String] = "128.221.200.64"

        Public Shared Sub Main()
            Try
                FPLogger.ConsoleMessage("\nCluster to connect to <" + defaultCluster + "> : ")
                Dim clusterAddress As [String] = System.Console.ReadLine()
                If "" = clusterAddress Then
                    clusterAddress = defaultCluster
                End If

                FPLogger.ConsoleMessage("\nEnter the name of the file containing the content to be stored: ")
                Dim fileName As [String] = System.Console.ReadLine()

                FPLogger.ConsoleMessage("\nEnter the threshold to use for Embedded Blobs: ")
                Dim blobString As [String] = System.Console.ReadLine()
                Dim blobThreshold As Long = CType(Int32.Parse(blobString), Long)

                If blobThreshold > 0 Then
                    FPLogger.ConsoleMessage(("Content less than " + blobString + " bytes will be embedded in the CDF."))
                Else
                    FPLogger.ConsoleMessage("\nContent will never be embedded in the CDF.")
                End If

                Dim thePool As New FPPool(clusterAddress)

                ' Create the clip
                Dim clipRef As New FPClip(thePool, fileName)
                clipRef.RetentionPeriod = New TimeSpan(FPMisc.NO_RETENTION_PERIOD)
                clipRef.SetAttribute("OriginalFilename", fileName)

                Dim fileTag As FPTag = clipRef.AddTag("StoreContentObject")
                ' Read the content of the blob to the output file 
                fileTag.SetAttribute("filename", fileName)

                Dim streamRef As New FPStream(fileName, 16 * 1024)
                fileTag.BlobWrite(streamRef, FPMisc.OPTION_CLIENT_CALCID)
                streamRef.Close()

                fileTag.Close()

                Dim clipID As [String] = clipRef.Write()


                ' Write the Clip ID to the output file, "inputFileName.clipID" 
                Dim outFileName As [String] = fileName + ".clipID"

                FPLogger.ConsoleMessage(("The C-Clip ID of the content is " + clipID.ToString()))

                Dim outFile As New FileStream(outFileName, FileMode.Create)
                Dim outWriter As New BinaryWriter(outFile)
                outWriter.Write(clipID.ToString())
                outWriter.Close()
                outFile.Close()

                Dim clipSize As Long = clipRef.TotalSize

                If blobThreshold > 0 And clipSize < blobThreshold Then
                    FPLogger.ConsoleMessage("\nContent was embedded in the CDF as it's size (" + clipSize.ToString() + " bytes) is less than the threshold.")
                End If


                clipRef.Close()
                thePool.Close()
            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo
                FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error.ToString() + " " + err.message))
            End Try
        End Sub 'Main
    End Class 'StoreContent
End Module 'StoreContent