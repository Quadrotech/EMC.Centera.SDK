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


Module CascadedDelete
    _
    '/ <summary>
    '/ Summary description for Class1.
    '/ </summary>
    Class CascadedDelete
        Private Shared defaultCluster As [String] = "128.221.200.64"

        'Entry point which delegates to C-style main Private Function
        Public Shared Sub Main()
            Try
                FPLogger.ConsoleMessage(("Cluster to connect to <" + defaultCluster + "> :"))
                Dim clusterAddress As [String] = System.Console.ReadLine()
                If "" = clusterAddress Then
                    clusterAddress = defaultCluster
                End If

                FPLogger.ConsoleMessage("\nEnter the CA of the content (and ancestors) to delete : ")
                Dim clipID As [String] = System.Console.ReadLine()

                Dim thePool As New FPPool(clusterAddress)
                Dim clipRef As FPClip = thePool.ClipOpen(clipID, FPMisc.OPEN_FLAT)

                While clipID.CompareTo("") <> 0
                    clipID = clipRef.GetAttribute("prev.clip")
                    FPLogger.ConsoleMessage((ControlChars.Tab + "Deleting clip " + clipRef.ClipID))

                    thePool.ClipAuditedDelete(clipRef.ClipID, "Cascaded Delete example", FPMisc.OPTION_DELETE_PRIVILEGED)
                    clipRef.Close()
                    If clipID.CompareTo("") <> 0 Then
                        clipRef = thePool.ClipOpen(clipID, FPMisc.OPEN_FLAT)
                    End If
                End While
            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo
                FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error + " " + err.message))
            End Try
      End Sub 'Main
    End Class 'CascadedDelete
End Module 'CascadedDelete
