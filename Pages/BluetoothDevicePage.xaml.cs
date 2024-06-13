using BluetoothApp.Services;
using Plugin.BLE.Abstractions.Exceptions;
using System.Text;

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


    public BluetoothDevicePage()
    {
        InitializeComponent();
        ConnectToDeviceAsync();
    }

    public async Task ConnectToDeviceAsync()
    {
        this.actIsBusy.IsRunning = true;
        try
        {
            this.btn_connect.IsEnabled = false;
            await BluetoothService.Adapter.ConnectToDeviceAsync(BluetoothService.mainDevice);
            DisplayAlert("Message", "Kết nối thành công", "OK");
            this.btn_connect.IsVisible = false;
            this.btn_disconnect.IsVisible = true;
            this.btn_disconnect.IsEnabled = true;
        }
        catch (DeviceConnectionException e)
        {
            DisplayAlert("Message", "Kết nối thất bại", "OK");
        }
        this.actIsBusy.IsRunning = false;

        ReadValueAsync();
    }

    private async void OnDisconnect(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            this.actIsBusy.IsRunning = true;
            this.btn_disconnect.IsEnabled = false;
            await BluetoothService.Adapter.DisconnectDeviceAsync(BluetoothService.mainDevice);
            DisplayAlert("Message", "Disconnect thành công", "OK");

            check = true;
        }
        catch
        {
            check = false;
            DisplayAlert("Message", "Disconnect thất bại", "OK");
        }

        if (check == true)
        {
            this.btn_disconnect.IsVisible = false;
            this.btn_connect.IsVisible = true;
            this.btn_connect.IsEnabled = true;
        }
        this.actIsBusy.IsRunning = false;
    }

    private async void OnConnect(object sender, EventArgs e)
    {
        await ConnectToDeviceAsync();
    }

    private async void ReadValueAsync()
    {
        try
        {
            var services = await BluetoothService.mainDevice.GetServicesAsync();

            var characteristic = await services[2].GetCharacteristicsAsync();


            while (true)
            {
                var bytes = await characteristic[0].ReadAsync();

                string stringValue = Encoding.UTF8.GetString(bytes.data);

                this.lbl_value.Text = stringValue;
            }
        }
        catch
        {

        }


    }
}