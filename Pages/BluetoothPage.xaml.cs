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
using Plugin.BLE.Abstractions.Exceptions;
using System.Diagnostics;
using System.Security.Principal;

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
        this.collectionView.ItemsSource = items;
    }

    private async void OnOpenBluetooth(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            if (BluetoothService.BluetoothLE.State == BluetoothState.Off)
            {
                //bật bluetooth
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "ms-settings:bluetooth",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to open Bluetooth settings: " + ex.Message);
                }
            }
        }


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
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            if (BluetoothService.BluetoothLE.State == BluetoothState.Off)
            {
                DisplayAlert("Message", "Bạn chưa bật bluetooth", "OK");
                return;
            }
        }
        actIsBusy.IsRunning = true;
        await Permissions.RequestAsync<Permissions.Bluetooth>();
        await bluetoothService.ScanForDevicesAsync();
        this.devices = bluetoothService.deviceList;
        this.idevices = bluetoothService.idevices;
        actIsBusy.IsRunning = false;
        DisplayAlert("Message", "Scan hoàn thành", "OK");
        this.collectionView.ItemsSource = bluetoothService.deviceList;
    }

    private async void OnFrameTapped(object sender, TappedEventArgs e)
    {

        // Lấy thiết bị được chọn từ BindingContext của Label
        var selectedDevice = (sender as VisualElement).BindingContext as CustomBluetoothDevice;
       
        if (selectedDevice == null)
        {
            return;
        }
        //Kiểm tra xem thiết bị này đã từng kết nối trước đó chưa
        var shell = (App.Current.MainPage as AppShell);
        
        //nếu đã kết nối thì chuyển sang tab đó không cần tạo tab mới
        if(AppShell._createdTabs.ContainsKey(selectedDevice.Name))
        {
            shell.CurrentItem = AppShell._createdTabs[selectedDevice.Name];
        }

        else
        {
            
            //lấy danh sách các thiết bị vừa quét được
            IDevice? deviceToConnect = this.idevices.FirstOrDefault(d => d.Id.ToString() == selectedDevice.Id);

            if (deviceToConnect == null)
            {
                return;
            }

            ////kiểm tra thiết bị đã kết nối chưa
            //var isConnected = BluetoothService.Adapter.ConnectedDevices.Contains(deviceToConnect);
            //if (isConnected == true)
            //{
            //    DisplayAlert("Thông báo", "Bạn đã kết nối tới thiết bị này rồi", "OK");
            //    return;
            //}

            BluetoothService.currentDevice = deviceToConnect;

            // Chuyển sang trang khác và truyền giá trị của thiết bị
            //await Shell.Current.GoToAsync($"//BluetoothDevicePage?id={selectedDevice?.Id}&name={selectedDevice.Name}");

            //await Shell.Current.GoToAsync(nameof(BluetoothDevicePage), true,new Dictionary<string, object>
            //{
            //    {deviceToConnect.Id.ToString(), deviceToConnect}
            //});

            //await ConnectToDeviceAsync(deviceToConnect);


            // Tạo trang BluetoothDevice Page đã có sẵn
            var newPage = new BluetoothDevicePage(deviceToConnect.Id.ToString(), deviceToConnect.Name, deviceToConnect);

            //tạo 1 tab mới 
            var newTab = new Tab
            {
                Title = $"{deviceToConnect.Name}"
            };


            //Tạo ShellContent, ShellContent có thuộc tính ContentTemplate lấy giá trị là DataTemplate,
            //DataTemplate sẽ lấy Page được kế thừa từ class ContentPage
            var newShellContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(() => newPage)
            };

            //add ShellContent vào trong Tab
            newTab.Items.Add(newShellContent);


            shell.AddNewTab(newTab, deviceToConnect.Name);

        }

    }

    //public async Task ConnectToDeviceAsync(IDevice device)
    //{
    //    try
    //    {
    //        await BluetoothService.Adapter.ConnectToDeviceAsync(device);
    //        DisplayAlert("Message", "Kết nối thành công", "OK");
    //    }
    //    catch (DeviceConnectionException e)
    //    {
    //        DisplayAlert("Message", "Kết nối thất bại", "OK");
    //    }
    //}
}