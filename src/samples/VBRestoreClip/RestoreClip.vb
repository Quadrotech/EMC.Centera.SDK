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



Namespace RestoreClip
    _
    '/ <summary>
    '/ Store content contained in a file to the Centera.
    '/ </summary>
    Class RestoreClip
        Private Shared defaultCluster As [String] = "128.221.200.64"

        Public Shared Sub Main(ByVal args() As String)
            Try
                FPLogger.ConsoleMessage(("Cluster to connect to <" + defaultCluster + "> : "))
                Dim clusterAddress As [String] = System.Console.ReadLine()
                If "" = clusterAddress Then
                    clusterAddress = defaultCluster
                End If

                FPLogger.ConsoleMessage("\nEnter the name of the file containing the clip to be restored: ")
                Dim fileName As [String] = Console.ReadLine()
                If Not fileName.EndsWith(".xml") Then
                    FPLogger.ConsoleMessage("\nInvalid file name format - should be <clipID>.xml")
                    Return
                End If

                Dim clipID As [String] = fileName.Substring(0, fileName.Length - 4)
                Dim thePool As New FPPool(clusterAddress)

                Dim fpStream As New FPStream(fileName, 16 * 1024)

                ' Create the clip
                Dim clipRef As FPClip = thePool.ClipRawOpen(clipID, fpStream, FPMisc.OPTION_DEFAULT_OPTIONS)

                fpStream = New FPStream(clipID + ".Tag.0", 16 * 1024)

                Dim t As FPTag = clipRef.NextTag

                t.BlobWrite(fpStream, FPMisc.OPTION_DEFAULT_OPTIONS)
                t.Close()
                clipRef.Write()


                clipRef.Close()
                thePool.Close()

                FPLogger.ConsoleMessage("\nClip and Blob successfully restored to Cluster")
            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo
                FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error + " " + err.message))
            End Try
        End Sub 'Main
    End Class 'RestoreClip
End Namespace 'RestoreClip

