﻿using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace BluetoothApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private const int REQUEST_PERMISSION_CODE = 1001;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            initPermission();
        }

        private void initPermission()
        {
            List<String> mPermissionList = new List<string>();
            // When the Android version is 12 or greater, apply for new Bluetooth permissions
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            {
                mPermissionList.Add(Manifest.Permission.BluetoothScan);
                mPermissionList.Add(Manifest.Permission.BluetoothAdvertise);
                mPermissionList.Add(Manifest.Permission.BluetoothConnect);
                //Request for location permissions based on your actual needs
                //mPermissionList.add(Manifest.permission.ACCESS_COARSE_LOCATION);
                //mPermissionList.add(Manifest.permission.ACCESS_FINE_LOCATION);
            }


            else
            {
                mPermissionList.Add(Manifest.Permission.AccessCoarseLocation);
                mPermissionList.Add(Manifest.Permission.AccessFineLocation);
            }

            ActivityCompat.RequestPermissions(this, mPermissionList.ToArray(), REQUEST_PERMISSION_CODE);
        }
    }
}
