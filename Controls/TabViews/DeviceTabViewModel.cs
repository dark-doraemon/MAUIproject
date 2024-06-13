using BluetoothApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using UraniumUI;
using UraniumUI.Material.Controls;

namespace BluetoothApp.Controls.TabViews
{
    class DeviceTabViewModel : UraniumBindableObject
    {
        public ObservableCollection<CustomBluetoothDevice> DeviceTabs { get; set; } = new ObservableCollection<CustomBluetoothDevice>
        {
            new CustomBluetoothDevice
            {
                Id = "12312",
                Name = "ESP32 1",
            },
            new CustomBluetoothDevice
            {
                Id = "33312",
                Name = "ESP32 2",
            }
        };

        private object currentTab;

        public object CurrentTab { get => currentTab; set => SetProperty(ref currentTab, value); }

        public ICommand CreateNewTabCommand { get; set; }

        public ICommand RemoveTabCommand { get; set; }

        public DeviceTabViewModel()
        {
            CurrentTab = DeviceTabs.First();
            CreateNewTabCommand = new Command(CreateNewTab);
            RemoveTabCommand = new Command(RemoveTab);
        }


        private void CreateNewTab()
        {
            var newDevice = new CustomBluetoothDevice
            {
                Id = "12312",
                Name = "ESP32 1",
            };
            DeviceTabs.Add(newDevice);
            CurrentTab = newDevice;
        }

        private void RemoveTab(object obj)
        {
            if (obj is CustomBluetoothDevice tabItem)
            {
                DeviceTabs.Remove(tabItem);
            }
        }
    }
}
