using System;
using System.Text;

namespace EMC.Centera.SDK.FPTypes
{
    [Serializable]
    public class FPLibraryException : Exception
    {
        public ErrorInfo ErrorInfo { get; }

        public FPLibraryException(FPErrorInfo errorInfo) 
        {
            ErrorInfo = new ErrorInfo(errorInfo);
        }

        public FPLibraryException(string s, int error)
        {
            ErrorInfo = new ErrorInfo(s, error);
        }

        public override string ToString()	
        {
            StringBuilder retval = new StringBuilder();
            retval.Append("error: " + ErrorInfo.Error + 
                          ", error text: " + ErrorInfo.ErrorString + 
                          ", syserror: " + ErrorInfo.SystemError + 
                          ", message: " + ErrorInfo.Message + 
                          ", trace: " + ErrorInfo.Trace);
            return retval.ToString();
        }
    }
}