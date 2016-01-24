using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DemoXamarin.Business;
using Android.Locations;
using System.Threading.Tasks;

namespace DemoXamarin.Droid
{
    public class GpsProvider : IGpsProvider
    {
        public bool GeolocationAvailable
        {
            get
            {
                return this._locationManager != null
                    && this._locationManager.IsProviderEnabled(locationProvider);
            }
        }

        public GeoCoordinate GetCurrentPosition
        {
            get
            {
                if (this._location == null)
                {
                    return null;
                }

                return new GeoCoordinate()
                {
                    Longitude = this._location.Longitude,
                    Latitude = this._location.Latitude
                };
            }
        }

        public async Task RefreshPositionAsync()
        {
            await Task.Run(() =>
            {
                this._location = this._locationManager.GetLastKnownLocation(locationProvider);
            });
        }

        private const string locationProvider = LocationManager.GpsProvider;
        private LocationManager _locationManager;
        private Location _location;

        public GpsProvider()
        {
            this._locationManager = (LocationManager)App.Context.GetSystemService(Context.LocationService);
        }
    }
}