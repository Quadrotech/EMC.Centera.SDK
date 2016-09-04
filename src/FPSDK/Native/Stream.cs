using System;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Stream 
    {

        public static FPStreamRef CreateFileForInput( string pFilePath,  string pPerm, long pBuffSize) 
        {
            FPStreamRef retval = SDK.FPStream_CreateFileForInput8(pFilePath, pPerm, pBuffSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreatePartialFileForInput(String pFilePath, string pPerm, long pBuffSize, long pOffset, long pSize)
        {
            FPStreamRef retval = SDK.FPStream_CreatePartialFileForInput8(pFilePath, pPerm, pBuffSize, pOffset, pSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateFileForOutput(String pFilePath, string pPerm) 
        {
            FPStreamRef retval = SDK.FPStream_CreateFileForOutput8(pFilePath, pPerm);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreatePartialFileForOutput(String pFilePath, string pPerm, long pBuffSize, long pOffset, long pSize, long pMaxFileSize)
        {
            FPStreamRef retval = SDK.FPStream_CreatePartialFileForOutput8(pFilePath, pPerm, pBuffSize, pOffset, pSize, pMaxFileSize);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateBufferForInput(IntPtr pBuffer, long pBuffLen) 
        {
            FPStreamRef retval = SDK.FPStream_CreateBufferForInput(pBuffer, pBuffLen);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPStreamRef CreateBufferForOutput( IntPtr pBuffer, long pBuffLen) 
        {
            FPStreamRef retval = SDK.FPStream_CreateBufferForOutput(pBuffer, pBuffLen);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPStreamRef pStream) 
        {
            SDK.FPStream_Close(pStream);
            SDK.CheckAndThrowError();
        }

        public static FPStreamRef FPStream_CreateToStdio()
        {
            FPStreamRef retval = SDK.FPStream_CreateToStdio();
            SDK.CheckAndThrowError();
            return retval;
        }

        public static FPStreamRef FPStream_CreateToNull()
        {
            FPStreamRef retval = SDK.FPStream_CreateToNull();
            SDK.CheckAndThrowError();
            return retval;
        }
		
        public static FPStreamRef FPStream_CreateTemporaryFile (long pMemBuffSize)
        {
            FPStreamRef retval = SDK.FPStream_CreateTemporaryFile(pMemBuffSize);
            SDK.CheckAndThrowError();
            return retval;
        }
    }
}