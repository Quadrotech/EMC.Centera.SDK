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
Imports EMC.Centera
Imports EMC.Centera.SDK
Imports EMC.Centera.FPTypes


Namespace RetrieveContent
    _
    '/ <summary>
    '/ Summary description for Class1.
    '/ </summary>
    Class RetrieveContent
        Private Shared defaultCluster As [String] = "128.221.200.64"

        Public Shared Sub Main(ByVal args() As String)
            Try
                FPLogger.ConsoleMessage(("Cluster to connect to <" + defaultCluster + "> :"))
                Dim clusterAddress As [String] = System.Console.ReadLine()
                If "" = clusterAddress Then
                    clusterAddress = defaultCluster
                End If

                FPLogger.ConsoleMessage("\nEnter the CA of the content to be restored: ")
                Dim clipID As [String] = System.Console.ReadLine()

                Dim thePool As New FPPool(clusterAddress)

                Dim clipRef As New FPClip(thePool, clipID, FPMisc.OPEN_ASTREE)

                FPLogger.ConsoleMessage(("The clip retention expires on " + clipRef.RetentionExpiry))

                ' Iterate across the  clip metadata
                Dim attr As FPAttribute
                For Each attr In clipRef.Attributes
                    FPLogger.ConsoleMessage(attr.ToString())
                Next attr

                ' Iterate across the Tag (and their Attr) collections
                Dim tg As FPTag
                For Each tg In clipRef.Tags
                    FPLogger.ConsoleMessage(tg)

                    Dim a As FPAttribute
                    For Each a In tg.Attributes
                        FPLogger.ConsoleMessage(a.ToString())
                    Next a
                Next tg

                Dim t As FPTag = CType(clipRef.Tags(0), FPTag)

                If t.ToString() = "StoreContentObject" Then
                    ' Retrieve the "filename" attribute 
                    Dim outfile As [String] = clipRef.ClipID + ".Tag." + "0"

                    Dim fpStreamRef As New FPStream(outfile, "wb")

                    t.BlobRead(fpStreamRef, FPMisc.OPTION_DEFAULT_OPTIONS)
                    fpStreamRef.Close()

                    FPLogger.ConsoleMessage(("The Blob has been stored into " + outfile))

                    FPLogger.ConsoleMessage(("The CDF has been saved to " + clipRef.ClipID + ".xml"))
                    fpStreamRef = New FPStream(clipRef.ClipID + ".xml", "wb")
                    clipRef.RawRead(fpStreamRef)
                Else
                    FPLogger.ConsoleMessage("\nApplication Error: Not A C-Clip Created By StoreContent Example")
                End If
            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo
                FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error + " " + err.message))
            End Try
        End Sub 'Main
    End Class 'RetrieveContent
End Namespace 'RetrieveContent