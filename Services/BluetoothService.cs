using BluetoothApp.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
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
        public IBluetoothLE BluetoothLE { get; set; }
        public IAdapter Adapter { get; set; }
        public List<CustomBluetoothDevice> deviceList { get; set; }
        public List<IDevice> idevices { get; set; }

        public BluetoothService()
        {
            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;
            Adapter.ScanTimeout = 10000;
            Adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
            deviceList = new List<CustomBluetoothDevice>();
            idevices = new List<IDevice>();
        }
        private async void Adapter_DeviceDiscovered(object sender, DeviceEventArgs e)
        {
            deviceList.Add(new CustomBluetoothDevice
            {
                Id = e.Device.Id.ToString(),
                Name = e.Device.Name,
            });

            idevices.Add(e.Device);
        }

        public async Task ScanForDevicesAsync()
        {
            deviceList = new List<CustomBluetoothDevice>();
            await Adapter.StartScanningForDevicesAsync();
        }
    }
}
