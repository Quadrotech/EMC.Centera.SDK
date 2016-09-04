using System.Text;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Monitor 
    {

        public static FPMonitorRef Open( string inClusterAddress) 
        {
            FPMonitorRef retval = SDK.FPMonitor_Open8(inClusterAddress);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPMonitorRef inMonitor) 
        {
            SDK.FPMonitor_Close(inMonitor);
            SDK.CheckAndThrowError();
        }
        public static void GetDiscovery(FPMonitorRef inMonitor,  StringBuilder outData, ref FPInt ioDataLen) 
        {
            SDK.FPMonitor_GetDiscovery(inMonitor, outData, ref ioDataLen);
            SDK.CheckAndThrowError();
        }
        public static void GetDiscoveryStream(FPMonitorRef inMonitor, FPStreamRef inStream) 
        {
            SDK.FPMonitor_GetDiscoveryStream(inMonitor, inStream);
            SDK.CheckAndThrowError();
        }
        public static void GetAllStatistics(FPMonitorRef inMonitor,  StringBuilder outData, ref FPInt ioDataLen) 
        {
            SDK.FPMonitor_GetAllStatistics(inMonitor, outData, ref ioDataLen);
            SDK.CheckAndThrowError();
        }
        public static void GetAllStatisticsStream(FPMonitorRef inMonitor, FPStreamRef inStream) 
        {
            SDK.FPMonitor_GetAllStatisticsStream(inMonitor, inStream);
            SDK.CheckAndThrowError();
        }

    }
}