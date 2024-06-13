using BluetoothApp.Services;
using BluetoothApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Dependency(typeof(BluetoothConnector))]
namespace BluetoothApp.Services
{
    internal class BluetoothConnector : IBluetoothConnector
    {
        public void Connect(string deviceName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetConnectedDevices()
        {
            throw new NotImplementedException();
        }
    }
}
