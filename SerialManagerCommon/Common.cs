using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialManagerCommon
{
    public class COMDeviceInfo
    {
        public string DevicePortName { get; set; } = string.Empty;
        public List<VCOMInfo> VirtualDevices { get; set; } = new List<VCOMInfo>();
    }

    public class VCOMInfo
    {
        public string DeviceName { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public string VirtualPortName { get; set; } = string.Empty;
    }
}
