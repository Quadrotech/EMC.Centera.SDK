using System.Runtime.InteropServices;

namespace EMC.Centera.SDK.FPTypes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FPErrorInfo
    {
        public FPInt     error;             /**< The last FPLibrary error that occurred on this thread. */
   
        public FPInt     systemError;       /**< The last system error that occurred on this thread.    */
  
        [ MarshalAs( UnmanagedType.LPStr )]
        public string     trace;             /**< The function trace for the last error that occurred.   */
  
        [ MarshalAs( UnmanagedType.LPStr )]
        public string    message;           /**< The message associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.        */
  
        [ MarshalAs( UnmanagedType.LPStr )]
        public string    errorString ;      /**< The error string associated with the FPLibrary error. <br>The string should <b>not</b> be deallocated or modified by the application.   */
  
        public FPShort   errorClass ;       /**< Keeps the class of message:<br>1: Network error <br>2: Server error <br>3: Client error                                                  */

    }
}