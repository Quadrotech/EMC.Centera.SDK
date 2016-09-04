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

        internal ErrorInfo(string s, int error)
        {
            errorInfo.errorString = s;
            errorInfo.error = (FPInt) error;
            errorInfo.trace = "";
            errorInfo.message = "";
            errorInfo.errorClass = (FPShort) 3;
        }

        public int error => (int) errorInfo.error;
        public int systemError => (int) errorInfo.systemError;

        public string trace => errorInfo.trace;
        public string message => errorInfo.message;
        public string errorString => errorInfo.errorString;

        public ushort errorClass => (ushort) errorInfo.errorClass;

        public override string ToString()
        {
            return errorString;
        }

    }
}