using System;
using System.Text;

namespace EMC.Centera.SDK.FPTypes
{
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

        public override string ToString()	
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
}