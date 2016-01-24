using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DemoXamarin.Business.ViewModels;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Helpers;
using DemoXamarin.Business.Models;

namespace DemoXamarin.Droid
{
    [Activity(Label = "DemoXamarin.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected ParkingViewModel Vm => App.Locator.ParkingViewModel;
        protected ListView ParkingListView => FindViewById<ListView>(Resource.Id.parkingListView);

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            App.Context = this;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            await this.Vm.ReloadDataAsync();

            this.ParkingListView.Adapter = this.Vm.Parkings.GetAdapter(GetParkingView);
        }

        private View GetParkingView(int index, GeolocalizableModel model, View convertView)
        {
            View view = convertView ?? LayoutInflater.Inflate(Resource.Layout.RowItemTemplate, null);

            TextView textView = view.FindViewById<TextView>(Resource.Id.parkingName);
            textView.Text = model.Name;

            return view;
        }
    }
}

