/*****************************************************************************

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
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPLogger : FPObject
    {
        protected FPLogStateRef TheLogger;

        /// <summary>
        ///Basic Constructor which creates a default FPLogStateRef object.
        ///See API Guide: FPLogging_CreateLogState
        ///
        ///@return	A new FPLogger object.
         /// </summary>
        public FPLogger()
        {
            TheLogger = Native.Logging.CreateLogState();
            AddObject(TheLogger, this);
        }

        /// <summary>
        ///Constructor which creates an FPLogStateRef object using a config file.
        ///See API Guide: FPLogging_OpenLogState
        ///
        ///@param   inName  File system path name for a FPLogStateRef contained in a file.
        ///@return	A new FPLogger object.
         /// </summary>
        public FPLogger(string inName)
        {
            TheLogger = Native.Logging.OpenLogState(inName);
            AddObject(TheLogger, this);
        }

        /// <summary>
        ///Explicitly free the resources assoicated with an FPLogStateRef object
        ///See API Guide: FPLogState_Delete
        ///
         /// </summary>
        public override void Close()
        {
            if (TheLogger != 0)
            {
                RemoveObject(TheLogger);
                Native.Logging.Delete(TheLogger);
                TheLogger = 0;
            }
        }

        /// <summary>
        ///Static method to allow an application to log its own messages to the active LogState
        ///See API Guide: FPLogging_log
        ///
        ///@param   inLevel The FPLogLevel of the message.
        ///@param   inMessage   The actual message to be logged.
         /// </summary>
        public static void Log(FPLogLevel inLevel, string inMessage)
        {
            Native.Logging.Log(inLevel, inMessage);
        }

        /// <summary>
        ///Implicit conversion between a  Logger and an FPLogStateRef
        ///
        ///@param	l	The Logger.
        ///@return	The FPLogStateRef associated with this Logger.
         /// </summary>
        public static implicit operator FPLogStateRef(FPLogger l)
        {
            return l.TheLogger;
        }

        /// <summary>
        ///Implicit conversion between an FPLogStateRef and a  Stream
        ///
        ///@param	logRef	The FPLogStateRef.
        ///@return	The Logger.
         /// </summary>
        public static implicit operator FPLogger(FPLogStateRef logRef)
        {
            // Find the relevant Logger object in the hastable for this LogStateRef
            FPLogger logObject;

            if (SDKObjects.Contains(logRef))
            {
                logObject = (FPLogger)SDKObjects[logRef];
            }
            else
            {
                throw new FPLibraryException("FPLogStateRef is not asscociated with an FPLogger object", FPMisc.WRONG_REFERENCE_ERR);
            }

            return logObject;
        }

        /// <summary>
        ///Allow the application to save the current logger object to a config file.
        ///See API Guide: FPLogState_Save
        ///
        ///@param   inPath  The file system pathname of the file to be written to.
         /// </summary>
        public void Save(string inPath)
        {
            Native.Logging.Save(this, inPath);
        }

        /// <summary>
        ///Start the SDK logging using the current Logger object's state
        ///See API Guide: FPLogging_Start
        ///
         /// </summary>
        public void Start()
        {
            Native.Logging.Start(this);
        }

        /// <summary>
        ///Static method to stop the SDK lgging
        ///See API Guide: FPLogging_Stop
        ///
         /// </summary>
        public static void Stop()
        {
            Native.Logging.Stop();
        }

        /// <summary>
        ///Static method to allow an application to register its own method to perform logging
        ///See API Guide: FPLogging_RegisterCallback
        ///
        ///@param   inProc  The user Callback method (delegate)
         /// </summary>
        public static void RegisterCallback(FPLogProc inProc)
        {
            Native.Logging.RegisterCallback(inProc);
        }

        /// <summary>
        ///AppendMode property i.e. Append to existing log or replace on start.
        ///See API Guide: FPLogState_GetAppendMode / FPLogState_SetAppendMode
        ///
         /// </summary>
        public FPBool AppendMode
        {
            get
            {
                return Native.Logging.GetAppendMode(this);
            }
            set
            {
                Native.Logging.SetAppendMode(this, value);
            }
        }

        /// <summary>
        ///DisableCallback property i.e. do we use the user-registered callback for logging
        ///See API Guide: FPLogState_GetDisableCallback / FPLogState_SetDisableCallback
        ///
         /// </summary>
        public FPBool DisableCallback
        {
            get
            {
                return Native.Logging.GetDisableCallback(this);
            }
            set
            {
                Native.Logging.SetDisableCallback(this, value);
            }
        }

        /// <summary>
        ///LogFilter property - determines the type of calls which are logged
        ///See API Guide: FPLogState_GetLogFilter / FPLogState_SetLogFilter
        ///
         /// </summary>
        public FPInt LogFilter
        {
            get
            {
                return Native.Logging.GetLogFilter(this);
            }
            set
            {
                Native.Logging.SetLogFilter(this, value);
            }
        }

        /// <summary>
        ///LogLevel property - determines which severity level of SDK messages are logged
        ///See API Guide: FPLogState_GetLogLevel / FPLogState_SetLogLevel
        ///
         /// </summary>
        public FPLogLevel LogLevel
        {
            get
            {
                return Native.Logging.GetLogLevel(this);
            }
            set
            {
                Native.Logging.SetLogLevel(this, value);
            }
        }

        /// <summary>
        ///LogPath property - the file system path for the log file
        ///See API Guide: FPLogState_GetLogPath / FPLogState_SetLogPath
        ///
         /// </summary>
        public string LogPath
        {
            get
            {
                return Native.Logging.GetLogPath(this);
            }
            set
            {
                Native.Logging.SetLogPath(this, value);
            }
        }

        /// <summary>
        ///MaxLogSize property - the maximum file system size the log file can occupy (default 1GB)
        ///See API Guide: FPLogState_GetMaxLogSize / FPLogState_SetMaxLogSize
        ///
         /// </summary>
        public FPLong MaxLogSize
        {
            get
            {
                return Native.Logging.GetMaxLogSize(this);
            }
            set
            {
                Native.Logging.SetMaxLogSize(this, value);
            }
        }

        /// <summary>
        ///MaxOverflows property - the number of backup log files allowed (default 1)
        ///See API Guide: FPLogState_GetMaxOverflows / FPLogState_SetMaxOverflows
        ///
         /// </summary>
        public FPInt MaxOverflows
        {
            get
            {
                return Native.Logging.GetMaxOverflows(this);
            }
            set
            {
                Native.Logging.SetMaxOverflows(this, value);
            }
        }

        /// <summary>
        ///PollInterval property - the time (in minutes) between polling for changes
        ///in any config file that was used to enable logging (default 5)
        ///See API Guide: FPLogState_GetPollInterval / FPLogState_SetPollInterval
        ///
         /// </summary>
        public FPInt PollInterval
        {
            get
            {
                return Native.Logging.GetPollInterval(this);
            }
            set
            {
                Native.Logging.SetPollInterval(this, value);
            }
        }

        public static void ConsoleMessage(string message)
        {
            Console.Write(message);
        }
    }
}