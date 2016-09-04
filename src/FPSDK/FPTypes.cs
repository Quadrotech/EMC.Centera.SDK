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
using System.Runtime.InteropServices;
using EMC.Centera.SDK;

namespace EMC.Centera.FPTypes
{
	/// <summary>
	/// FPApi is a wrapper class for the Centera SDK DLL
	/// </summary>
	/// 

	public enum FPBool						: byte  { False=0,True=1 }
	public enum FPShort						: ushort{ Zero }
	public enum FPInt						: int   { Zero }
	public enum FPLong						: long  { Zero }
	public enum FPPoolRef					: ulong { Zero }
	public enum FPStreamRef					: ulong { Zero }
	public enum FPClipRef					: ulong { Zero }
	public enum FPTagRef					: ulong { Zero }
	public enum FPQueryRef					: ulong { Zero }
	public enum FPQueryExpressionRef		: ulong { Zero }
	public enum FPPoolQueryRef				: ulong { Zero }
	public enum FPQueryResultRef			: ulong { Zero }
	public enum FPMonitorRef				: ulong { Zero }
	public enum FPEventCallbackRef			: ulong { Zero }
	public enum FPRetentionClassContextRef  : ulong { Zero }
	public enum FPRetentionClassRef			: ulong { Zero }
	public enum FPLogStateRef			    : long  { Zero }
    public enum FPLogLevel                  : int { Error = 1, Warn = 2, Log = 3, Debug = 4 }
    public enum FPLogFormat                 : int { XML = 0x00000001, Tab = 0x00000002 }
    
    [StructLayout(LayoutKind.Sequential)]
	public struct FPPoolInfo
	{
		public FPInt     poolInfoVersion;   /**< The current version of this structure (2)          */
		public FPLong    capacity;          /**< The total capacity of the pool, in bytes.          */
		public FPLong    freeSpace;         /**< The total free space of the pool, in bytes.        */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public String clusterID;         /**< The cluster identifier of the pool.                */
			
		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public String clusterName;       /**< The name of the cluster.                           */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 )]
		public String version;           /**< The version of the pool server software.           */

		[ MarshalAs( UnmanagedType.ByValTStr, SizeConst=256 )]
		public String replicaAddress;    /**< The pool address (see FPPool_Open()) where the C-Clips are replicated; empty if there is no replication. */ 
	}

	public class PoolInfo
	{
		private FPPoolInfo info;

		public PoolInfo(FPPoolInfo _info)
		{
			info = _info;
		}

		public int poolInfoVersion
		{
			get
			{
				return (int) info.poolInfoVersion;
			}
		}
		public long capacity
		{
			get
			{
				return (long) info.capacity;
			}
		}
		public long freeSpace
		{
			get
			{
				return (long) info.freeSpace;
			}
		}
		public String clusterName
		{
			get
			{
				return info.clusterName;
			}
		}
		public String clusterID
		{
			get
			{
				return info.clusterID;
			}
		}
		public String version
		{
			get
			{
				return info.version;
			}
		}
		public String replicaAddress
		{
			get
			{
				return info.replicaAddress;
			}
		}
		public override string ToString()
		{
			return "\nPool Information" +
			"\n================" +
			"\nCluster ID:                            " + clusterID +
			"\nCluster Name:                          " + clusterName +
			"\nCentraStar software version:           " + version +
			"\nCluster Capacity (Bytes):              " + capacity +
			"\nCluster Free Space (Bytes):            " + freeSpace +
			"\nCluster Replica Address:               " + replicaAddress + "\n";

		}

	}



	/** The structure that holds error information, which is retrieved by the FPPool_GetLastErrorInfo()
			function.
		  */
	[StructLayout(LayoutKind.Sequential)]
	public struct FPErrorInfo
	{
		public FPInt     error;             /**< The last FPLibrary error that occurred on this thread. */
   
		public FPInt     systemError;       /**< The last system error that occurred on this thread.    */
  
		[ MarshalAs( UnmanagedType.LPStr )]
		public String     trace;             /**< The function trace for the last error that occurred.   */
  
		[ MarshalAs( UnmanagedType.LPStr )]
		public String    message;           /**< The message associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.        */
  
		[ MarshalAs( UnmanagedType.LPStr )]
		public String    errorString ;      /**< The error string associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.   */
  
		public FPShort   errorClass ;       /**< Keeps the class of message:<br>1: Network error <br>2: Server error <br>3: Client error                                                  */

	}

	public class ErrorInfo
	{
		private FPErrorInfo	errorInfo;

		internal ErrorInfo(FPErrorInfo _errorInfo)
		{
			errorInfo = _errorInfo;
		}

        internal ErrorInfo(String s, int error)
        {
            errorInfo.errorString = s;
            errorInfo.error = (FPInt) error;
            errorInfo.trace = "";
            errorInfo.message = "";
            errorInfo.errorClass = (FPShort) 3;
        }

		public int error
		{
			get
			{
				return (int) errorInfo.error;
			}
		}
		public int systemError
		{
			get
			{
				return(int) errorInfo.systemError;
			}
		}
		public String trace
		{
			get
			{
				return errorInfo.trace;
			}
		}
		public String message
		{
			get
			{
				return errorInfo.message;
			}
		}
		public String errorString
		{
			get
			{
				return errorInfo.errorString;
			}
		}

		public ushort errorClass
		{
			get
			{
				return (ushort) errorInfo.errorClass;
			}
		}

		public override string ToString()
		{
			return errorString;
		}

	}


    [Serializable]
    public class FPLibraryException : Exception
	{
		public ErrorInfo	myErrorInfo;

		public ErrorInfo errorInfo
		{
			get
			{
				return myErrorInfo;
			}
		}

		public FPLibraryException(FPErrorInfo _errorInfo) 
		{
			myErrorInfo = new ErrorInfo(_errorInfo);
		}

        public FPLibraryException(String s, int error)
        {
            myErrorInfo = new ErrorInfo(s, error);
        }

        public override String ToString()	
		{
			StringBuilder retval = new StringBuilder();
			retval.Append("error: " + errorInfo.error + 
				", error text: " + errorInfo.errorString + 
				", syserror: " + errorInfo.systemError + 
				", message: " + errorInfo.message + 
				", trace: " + errorInfo.trace);
			return retval.ToString();
		}

	}

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long FPCallback(ref FPStreamInfo info);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate FPInt FPLogProc([MarshalAs(UnmanagedType.LPStr)] String inMessage);
}