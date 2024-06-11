using BluetoothApp.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Extensions;
using Microsoft.Maui.Controls;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using BluetoothApp.Services;
using Microsoft.Maui.Controls.Compatibility;
using System.Threading;


namespace BluetoothApp.Pages;

public partial class BluetoothPage : ContentPage
{

    List<CustomBluetoothDevice> devices = new List<CustomBluetoothDevice>();
    List<CustomBluetoothDevice> items = new List<CustomBluetoothDevice>();
    List<IDevice> idevices = new List<IDevice>();
    BluetoothService bluetoothService = new BluetoothService();

    public BluetoothPage()
	{
		InitializeComponent();

		items = new List<CustomBluetoothDevice>
		{
			new CustomBluetoothDevice {Name = "Device 1" ,Description ="Des1"},
			new CustomBluetoothDevice {Name = "Device 2" ,Description ="Des2"},
			new CustomBluetoothDevice {Name = "Device 3" ,Description ="Des3"},
			new CustomBluetoothDevice {Name = "Device 4" ,Description ="Des4"},
        };
        bluetoothService = new BluetoothService();
        this.listView.ItemsSource = items;
	}

    private void OnOpenBluetooth(object sender, EventArgs e)
    {
        //var enable = new Android.Content.Intent(Android.Bluetooth.BluetoothAdapter.ActionRequestEnable);
        //enable.SetFlags(Android.Content.ActivityFlags.NewTask);

        //var bluetoothManager = (Android.Bluetooth.BluetoothManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.BluetoothService);
        //var bluetoothAdapter = bluetoothManager.Adapter;
        //if (bluetoothAdapter.IsEnabled == true)
        //{
        //    // Disable the Bluetooth;
        //}
        //else
        //{
        //    // Enable the Bluetooth
        //    Android.App.Application.Context.StartActivity(enable);
        //}
    }

    bool isScanning = false;
    private async void OnScanBluetoothDevice(object sender, EventArgs e)
    {

        actIsBusy.IsRunning = true;
        await Permissions.RequestAsync<Permissions.Bluetooth>();
        await bluetoothService.ScanForDevicesAsync();
        this.devices = bluetoothService.deviceList;
        this.idevices = bluetoothService.idevices;
        actIsBusy.IsRunning = false;
        DisplayAlert("Message", "Scan hoàn thành","OK");
        this.listView.ItemsSource = bluetoothService.deviceList;
    }

    private async void OnLabelTapped(object sender, TappedEventArgs e)
    {
        // Lấy thiết bị được chọn từ BindingContext của Label
        var selectedDevice = (sender as Label).BindingContext as CustomBluetoothDevice;
        var deviceToConnect = this.idevices.FirstOrDefault(d => d.Id.ToString() == selectedDevice.Id);
        // Chuyển sang trang khác và truyền giá trị của thiết bị
        //await Shell.Current.GoToAsync($"BluetoothDevicePage?id={selectedDevice?.Id}&name={selectedDevice.Name}");

        await ConnectToDeviceAsync(deviceToConnect);
    }

    public async Task ConnectToDeviceAsync(IDevice device)
    {
        await bluetoothService.Adapter.ConnectToDeviceAsync(device);
    }
}