﻿using Device.Net;

namespace Device.Net.Windows
{
    public class WindowsDeviceDefinition : DeviceDefinition
    {
        public string Manufacturer { get; set; }
        public string Product { get; set; }
        public string SerialNumber { get; set; }
        public ushort Usage { get; set; }
        public ushort UsagePage { get; set; }
        public ushort VersionNumber { get; set; }
    }
}