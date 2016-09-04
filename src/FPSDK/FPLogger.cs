using System;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPLogger : FPObject
    {
        protected FPLogStateRef theLogger;

        /**
         * Basic Constructor which creates a default FPLogStateRef object.
         * See API Guide: FPLogging_CreateLogState
         *
         * @return	A new FPLogger object.
         */
        public FPLogger()
        {
            theLogger = Native.Logging.CreateLogState();
            AddObject(theLogger, this);
        }

        /**
         * Constructor which creates an FPLogStateRef object using a config file.
         * See API Guide: FPLogging_OpenLogState
         *
         * @param   inName  File system path name for a FPLogStateRef contained in a file.
         * @return	A new FPLogger object.
         */
        public FPLogger(String inName)
        {
            theLogger = Native.Logging.OpenLogState(inName);
            AddObject(theLogger, this);
        }

        /**
         * Explicitly free the resources assoicated with an FPLogStateRef object
         * See API Guide: FPLogState_Delete
         *
         */
        public override void Close()
        {
            if (theLogger != 0)
            {
                RemoveObject(theLogger);
                Native.Logging.Delete(theLogger);
                theLogger = 0;
            }
        }

        /**
         * Static method to allow an application to log its own messages to the active LogState
         * See API Guide: FPLogging_log
         *
         * @param   inLevel The FPLogLevel of the message.
         * @param   inMessage   The actual message to be logged.
         */
        static public void Log(FPLogLevel inLevel, string inMessage)
        {
            Native.Logging.Log(inLevel, inMessage);
        }

        /**
         * Implicit conversion between a  Logger and an FPLogStateRef
         *
         * @param	l	The Logger.
         * @return	The FPLogStateRef associated with this Logger.
         */
        static public implicit operator FPLogStateRef(FPLogger l)
        {
            return l.theLogger;
        }

        /**
         * Implicit conversion between an FPLogStateRef and a  Stream
         *
         * @param	logRef	The FPLogStateRef.
         * @return	The Logger.
         */
        static public implicit operator FPLogger(FPLogStateRef logRef)
        {
            // Find the relevant Logger object in the hastable for this LogStateRef
            FPLogger logObject = null;

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

        /**
         * Allow the application to save the current logger object to a config file.
         * See API Guide: FPLogState_Save
         *
         * @param   inPath  The file system pathname of the file to be written to.
         */
        public void Save(String inPath)
        {
            Native.Logging.Save(this, inPath);
        }

        /**
         * Start the SDK logging using the current Logger object's state
         * See API Guide: FPLogging_Start
         *
         */
        public void Start()
        {
            Native.Logging.Start(this);
        }

        /**
         * Static method to stop the SDK lgging
         * See API Guide: FPLogging_Stop
         *
         */
        static public void Stop()
        {
            Native.Logging.Stop();
        }

        /**
         * Static method to allow an application to register its own method to perform logging
         * See API Guide: FPLogging_RegisterCallback
         *
         * @param   inProc  The user Callback method (delegate)
         */
        static public void RegisterCallback(FPLogProc inProc)
        {
            Native.Logging.RegisterCallback(inProc);
        }

        /**
         * AppendMode property i.e. Append to existing log or replace on start.
         * See API Guide: FPLogState_GetAppendMode / FPLogState_SetAppendMode
         *
         */
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

        /**
         * DisableCallback property i.e. do we use the user-registered callback for logging
         * See API Guide: FPLogState_GetDisableCallback / FPLogState_SetDisableCallback
         *
         */
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

        /**
         * LogFilter property - determines the type of calls which are logged
         * See API Guide: FPLogState_GetLogFilter / FPLogState_SetLogFilter
         *
         */
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

        /**
         * LogLevel property - determines which severity level of SDK messages are logged
         * See API Guide: FPLogState_GetLogLevel / FPLogState_SetLogLevel
         *
         */
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

        /**
         * LogPath property - the file system path for the log file
         * See API Guide: FPLogState_GetLogPath / FPLogState_SetLogPath
         *
         */
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

        /**
         * MaxLogSize property - the maximum file system size the log file can occupy (default 1GB)
         * See API Guide: FPLogState_GetMaxLogSize / FPLogState_SetMaxLogSize
         *
         */
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

        /**
         * MaxOverflows property - the number of backup log files allowed (default 1)
         * See API Guide: FPLogState_GetMaxOverflows / FPLogState_SetMaxOverflows
         *
         */
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

        /**
         * PollInterval property - the time (in minutes) between polling for changes
         * in any config file that was used to enable logging (default 5)
         * See API Guide: FPLogState_GetPollInterval / FPLogState_SetPollInterval
         *
         */
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

        public static void ConsoleMessage(String message)
        {
            Console.Write(message);
        }
    }
}