using System.Runtime.InteropServices;

namespace EMC.Centera.SDK.FPTypes
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate FPInt FPLogProc([MarshalAs(UnmanagedType.LPStr)] string inMessage);
}