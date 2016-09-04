using System;

namespace EMC.Centera.SDK.FPTypes
{
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
        public string trace
        {
            get
            {
                return errorInfo.trace;
            }
        }
        public string message
        {
            get
            {
                return errorInfo.message;
            }
        }
        public string errorString
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
}