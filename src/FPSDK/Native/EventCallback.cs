using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class EventCallback 
    {

        public static FPEventCallbackRef RegisterForAllEvents(FPMonitorRef inMonitor, FPStreamRef inStream) 
        {
            FPEventCallbackRef retval = SDK.FPEventCallback_RegisterForAllEvents(inMonitor, inStream);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPEventCallbackRef inRegisterRef) 
        {
            SDK.FPEventCallback_Close(inRegisterRef);
            SDK.CheckAndThrowError();
        }

    }
}