using System.Runtime.InteropServices;

namespace EMC.Centera.SDK.FPTypes
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long FPCallback(ref FPStreamInfo info);
}