using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothApp.Models
{
    class BlePermissions : Permissions.BasePlatformPermission
    {
        //public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
        //    [
        //        (Android.Manifest.Permission.Bluetooth, true),
        //        (Android.Manifest.Permission.BluetoothAdmin, true),
        //        (Android.Manifest.Permission.AccessFineLocation, true),
        //        (Android.Manifest.Permission.AccessCoarseLocation, true),
        //        //(Android.Manifest.Permission.AccessBackgroundLocation, true),
        //        (Android.Manifest.Permission.BluetoothScan, true),
        //        (Android.Manifest.Permission.BluetoothConnect, true),
        //    ];
    }
}
