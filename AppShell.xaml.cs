using BluetoothApp.Pages;
namespace BluetoothApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("BluetoothDevicePage", typeof(BluetoothDevicePage));
        }
    }
}
