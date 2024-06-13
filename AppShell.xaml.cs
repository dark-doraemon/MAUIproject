using BluetoothApp.Pages;
namespace BluetoothApp
{
    public partial class AppShell : Shell
    {

        public static Dictionary<string, Tab> _createdTabs = new Dictionary<string, Tab>();
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("BluetoothDevicePage", typeof(BluetoothDevicePage));
        }

        public void AddNewTab(Tab newTab,string name)
        {

            if (_createdTabs.ContainsKey(name))
            {
                this.CurrentItem = _createdTabs[name];
            }

            else
            {
                this.mainTabBar.Items.Add(newTab);
                _createdTabs.Add(name, newTab);
                this.CurrentItem = newTab;
            }
        }

        
    }
}
