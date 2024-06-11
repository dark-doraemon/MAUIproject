using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Security.Cryptography;

namespace BluetoothApp.Pages;


[QueryProperty(nameof(BluetoothDeviceId), "id")]
[QueryProperty(nameof(DeviceName), "name")]
public partial class BluetoothDevicePage : ContentPage
{
	string bluetoothDeviceId;
	public string BluetoothDeviceId
	{
		get => bluetoothDeviceId;
		set
		{
            bluetoothDeviceId = value;
            this.lblId.Text = bluetoothDeviceId;    
        }
    }

    string deviceName;
    public string DeviceName
    {
        get => deviceName;
        set
        {
            deviceName = value;
            this.lblWelcome.Text = deviceName;
        }
    }


    public IAdapter Adapter;
    public BluetoothDevicePage()
    {
        InitializeComponent();
        Adapter = CrossBluetoothLE.Current.Adapter;
    }


    public async Task  ConnectToDeviceAsync()
    {
       
    }


}