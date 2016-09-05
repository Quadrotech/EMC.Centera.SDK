namespace EMC.Centera.SDK.FPTypes
{
    public class ErrorInfo
    {
        private readonly FPErrorInfo _errorInfo;

        internal ErrorInfo(FPErrorInfo errorInfo)
        {
            _errorInfo = errorInfo;
        }

        internal ErrorInfo(string s, int error)
        {
            _errorInfo.errorString = s;
            _errorInfo.error = (FPInt) error;
            _errorInfo.trace = "";
            _errorInfo.message = "";
            _errorInfo.errorClass = (FPShort) 3;
        }

        public int Error => (int) _errorInfo.error;
        public int SystemError => (int) _errorInfo.systemError;

        public string Trace => _errorInfo.trace;
        public string Message => _errorInfo.message;
        public string ErrorString => _errorInfo.errorString;

        public ushort ErrorClass => (ushort) _errorInfo.errorClass;

        public override string ToString()
        {
            return ErrorString;
        }

    }
}