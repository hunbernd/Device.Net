﻿using System;
using System.Timers;

namespace Device.Net.UWP
{
    public class UWPDevicePoller
    {
        #region Fields
        private Timer _PollTimer = new Timer(3000);
        private bool _IsPolling;
        #endregion

        #region Public Properties
        public int ProductId { get; }
        public int VendorId { get; }
        public UWPDeviceBase UWPDevice { get; private set; }
        #endregion

        #region Constructor
        public UWPDevicePoller(int productId, int vendorId, UWPDeviceBase uwpHidDevice)
        {
            _PollTimer.Elapsed += _PollTimer_Elapsed;
            _PollTimer.Start();
            ProductId = productId;
            VendorId = vendorId;
            UWPDevice = uwpHidDevice;
        }
        #endregion

        #region Event Handlers
        private async void _PollTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_IsPolling)
            {
                return;
            }

            _IsPolling = true;

            try
            {
                var deviceIds = await DeviceManager.GetDeviceIds(VendorId, ProductId);

                foreach (var deviceId in deviceIds)
                {
                    try
                    {
                        //Attempt to connect and move to the next one if this one doesn't connect
                        UWPDevice.DeviceId = deviceId;
                        await UWPDevice.InitializeAsync();
                        if (await UWPDevice.GetIsConnectedAsync())
                        {
                            //Connection was successful
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Error connecting to device", ex, nameof(UWPDevicePoller));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Hid polling error", ex, nameof(UWPDevicePoller));
            }

            _IsPolling = false;
        }
        #endregion

        #region Public Methods
        public void Stop()
        {
            _PollTimer.Stop();
        }
        #endregion
    }
}