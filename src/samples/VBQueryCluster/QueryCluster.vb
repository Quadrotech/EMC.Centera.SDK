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
Imports System.Globalization
Imports EMC.Centera
Imports EMC.Centera.SDK
Imports EMC.Centera.FPTypes



Namespace QueryCluster
    _
    '/ <summary>
    '/ Summary description for Class1.
    '/ </summary>
    Class QueryCluster
        Private Shared defaultCluster As [String] = "128.221.200.64"

        Public Shared Sub Main(ByVal args() As String)
            'int exitCode = 0;
            Try

                FPLogger.ConsoleMessage(("Cluster to connect to <" + defaultCluster + "> :"))
                Dim clusterAddress As [String] = Console.ReadLine()
                If "" = clusterAddress Then
                    clusterAddress = defaultCluster
                End If


                ' New feature for 2.3 lazy pool open
                FPPool.OpenStrategy = FPMisc.LAZY_OPEN

                ' open cluster connection
                Dim thePool As New FPPool(clusterAddress)

                ' Check that query is supported on this connection; query can be 
                ' disabled by the Centera administrator
                If thePool.QueryAllowed Then
                    FPLogger.ConsoleMessage("\nQuery is supported for this pool connection.")
                Else
                    FPLogger.ConsoleMessage("\nQuery is not supported for this pool connection.")
                    Return
                End If

                Dim myQuery As New FPQuery(thePool)

                FPLogger.ConsoleMessage("\nEnter the start time (YYYY/MM/DD HH:MM:SS) : ")
                myQuery.StartTime = FPMisc.GetDateTime(Console.ReadLine())

                FPLogger.ConsoleMessage("\nEnter the end time (YYYY/MM/DD HH:MM:SS) : ")
                myQuery.EndTime = FPMisc.GetDateTime(Console.ReadLine())

                ' New for 2.3 two types to query for EXISTING or DELETED. We'll get both.
                myQuery.Type = FPMisc.QUERY_TYPE_EXISTING Or FPMisc.QUERY_TYPE_DELETED

                ' We want to retrieve two fields during our query, select them beforehand.
                myQuery.SelectField("creation.date")
                myQuery.SelectField("modification.date")

                myQuery.Execute()

                FPLogger.ConsoleMessage(("Current Cluster Time is " + thePool.ClusterTime))

                '	Retrieve Query results one at a time, checking for incomplete gaps. 
                '  Gaps can occur when data is temporarily unavailable on the Centera backend
                Dim queryResult As New FPQueryResult

                Dim queryStatus As Integer = myQuery.FetchResult(queryResult, -1)
                Dim count As Integer = 0

                While queryStatus <> FPMisc.QUERY_RESULT_CODE_END And queryStatus <> FPMisc.QUERY_RESULT_CODE_ABORT
                    If queryStatus = FPMisc.QUERY_RESULT_CODE_OK Then
                        'Print clip
                        FPLogger.ConsoleMessage(("Clip ID: " + queryResult.ClipID + " Query timestamp: " + queryResult.Timestamp + " creation date: " + queryResult.GetField("creation.date") + " modification time on clip : " + queryResult.GetField("modification.date")))
                        count += 1
                    Else
                        If queryStatus = FPMisc.QUERY_RESULT_CODE_INCOMPLETE Then
                            ' Error occured one or more nodes on centera could not be queried.
                            FPLogger.ConsoleMessage("\nReceived FP_QUERY_RESULT_CODE_INCOMPLETE error, invalid C-Clip, trying again.")

                        Else
                            If queryStatus = FPMisc.QUERY_RESULT_CODE_COMPLETE Then
                                ' Indicate error should have been received after incomplete error
                                FPLogger.ConsoleMessage(("Received FP_QUERY_RESULT_CODE_COMPLETE, there should have been a previous " + "FP_QUERY_RESULT_CODE_INCOMPLETE error reported."))

                            Else
                                If queryStatus = FPMisc.QUERY_RESULT_CODE_ERROR Then
                                    'Server error
                                    FPLogger.ConsoleMessage("\nreceived FP_QUERY_RESULT_CODE_ERROR error, retrying again")

                                Else
                                    If queryStatus = FPMisc.QUERY_RESULT_CODE_PROGRESS Then
                                        FPLogger.ConsoleMessage("\nreceived FP_QUERY_RESULT_CODE_PROGRESS, continuing.")

                                    Else
                                        ' Unknown error, stop running query
                                        FPLogger.ConsoleMessage(("Aborting - received unkown error: " + queryStatus))
                                        Exit While
                                    End If
                                End If
                            End If
                        End If
                    End If
                    queryStatus = myQuery.FetchResult(queryResult, -1)
                End While


                If queryStatus = FPMisc.QUERY_RESULT_CODE_END Then
                    ' all results have been received finish query.
                    FPLogger.ConsoleMessage("\nEnd of query.")

                Else
                    If queryStatus = FPMisc.QUERY_RESULT_CODE_ABORT Then
                        ' query aborted due to server side issue or start time
                        ' is later than server time.
                        FPLogger.ConsoleMessage("\nQuery ended abnormally - FP_QUERY_RESULT_CODE_ABORT error.")
                    End If
                End If
                FPLogger.ConsoleMessage((ControlChars.Lf + "Total number of clips " + ControlChars.Tab + count.ToString()))
                myQuery.Close()

                ' Always close the Pool connection when finished.  Not a
                ' good practice to open and close for each transaction.
                thePool.Close()
                FPLogger.ConsoleMessage((ControlChars.Lf + "Closed connection to Centera cluster (" + clusterAddress + ")"))

            Catch e As FPLibraryException
                Dim err As ErrorInfo = e.errorInfo
                FPLogger.ConsoleMessage(("Exception thrown in FP Library: Error " + err.error + " " + err.message))
            End Try
        End Sub 'Main 
    End Class 'QueryCluster
End Namespace 'QueryCluster

