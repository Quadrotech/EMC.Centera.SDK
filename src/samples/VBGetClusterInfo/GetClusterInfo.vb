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
Imports EMC.Centera.SDK
Imports EMC.Centera.FPTypes


Namespace GetClusterInfo
    _
    '/ <summary>
    '/ Summary description for Class1.
    '/ </summary>
    Class GetClusterInfo
        Private Shared clusterAddress As [String] = "128.221.200.64"

        Public Shared Sub Main(ByVal args() As String)
            Try
                FPLogger.ConsoleMessage("\nEnter cluster to connect to [" + clusterAddress + "]: ")
                Dim answer As [String] = Console.ReadLine()

                If answer <> "" Then
                    clusterAddress = answer
                End If
                Dim myPool As New FPPool(clusterAddress)

                ' Output pool information
                FPLogger.ConsoleMessage("\nPool Information")
                FPLogger.ConsoleMessage("\n================")
                FPLogger.ConsoleMessage("\nCluster ID:                            " + myPool.ClusterID)
                FPLogger.ConsoleMessage("\nCluster Name:                          " + myPool.ClusterName)
                FPLogger.ConsoleMessage("\nCentraStar software version:           " + myPool.CentraStarVersion)
                FPLogger.ConsoleMessage("\nCluster Capacity (Bytes):              " + myPool.Capacity.ToString())
                FPLogger.ConsoleMessage("\nCluster Free Space (Bytes):            " + myPool.FreeSpace.ToString())
                FPLogger.ConsoleMessage("\nCluster Replica Address:               " + myPool.ReplicaAddress)


                Dim rc As FPRetentionClass
                For Each rc In myPool.RetentionClasses
                    FPLogger.ConsoleMessage("\nClass found " + rc.ToString())
                Next rc

                ' Check for a ProfileClip associated with the pool connection.
                Dim clipID As [String] = myPool.ProfileClip
                FPLogger.ConsoleMessage(("Profile Clip id " + clipID + " has metadata: "))

                Dim clipRef As FPClip = myPool.ClipOpen(clipID, FPMisc.OPEN_FLAT)

                Dim attr As FPAttribute
                For Each attr In clipRef.Attributes
                    FPLogger.ConsoleMessage(("Attribute " + attr.ToString()))
                Next attr

                clipRef.Close()

            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo

                If e.errorInfo.error = CType(FPMisc.PROFILECLIPID_NOTFOUND_ERR, FPInt) Then
                    FPLogger.ConsoleMessage("\nNo Profile Clip exists for this application." + ControlChars.Lf)
                Else
                    If e.errorInfo.error = CType(FPMisc.SERVER_ERR, FPInt) Then
                        FPLogger.ConsoleMessage("\nThe Centera cluster you are attempting to access may not support profile clips." + ControlChars.Lf)
                    Else
                        FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error + " " + err.message))
                    End If
                End If
            End Try
        End Sub 'Main

    End Class 'GetClusterInfo
End Namespace 'GetClusterInfo