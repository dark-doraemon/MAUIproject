using BluetoothApp.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothApp.Services
{
    class BluetoothService
    {
        public static IBluetoothLE BluetoothLE { get; set; }
        public static IAdapter Adapter { get; set; }
        public List<CustomBluetoothDevice> deviceList { get; set; }
        public List<IDevice> idevices { get; set; }

        public static IDevice currentDevice { get; set; } 
        public BluetoothService()
        {
            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;
            Adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
            deviceList = new List<CustomBluetoothDevice>();
            idevices = new List<IDevice>();
        }
        private async void Adapter_DeviceDiscovered(object sender, DeviceEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var devices = deviceList ?? new List<CustomBluetoothDevice>();
                devices.Add(new CustomBluetoothDevice
                {
                    Id = e.Device.Id.ToString(),
                    Name = e.Device.Name,
                });
                idevices.Add(e.Device);

            });

            
        }

        public async Task ScanForDevicesAsync()
        {
            deviceList = new List<CustomBluetoothDevice>();
            await Adapter.StartScanningForDevicesAsync();
            
        }

        public async Task ConnectToDeviceAsync(IDevice device)
        {
            try
            {
                await Adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException e)
            {

            }
        }
    }
}
