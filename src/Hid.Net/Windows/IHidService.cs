﻿using System;
using Device.Net;
using Microsoft.Win32.SafeHandles;

namespace Hid.Net.Windows
{
    public interface IHidService
    {
        ConnectedDeviceDefinition GetDeviceDefinition(string deviceId, SafeFileHandle safeFileHandle);
        HidAttributes GetHidAttributes(SafeFileHandle safeFileHandle);
        HidCollectionCapabilities GetHidCapabilities(SafeFileHandle readSafeFileHandle);
        Guid GetHidGuid();
        string GetManufacturer(SafeFileHandle safeFileHandle);
        string GetProduct(SafeFileHandle safeFileHandle);
        string GetSerialNumber(SafeFileHandle safeFileHandle);
    }
}