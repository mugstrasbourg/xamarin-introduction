using DemoXamarin.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DemoXamarin.Dos
{
    public class GpsProvider : IGpsProvider
    {
        public bool GeolocationAvailable
        {
            get
            {
                return this._status != null && this._status == GeolocationAccessStatus.Allowed;
            }
        }

        private GeoCoordinate _currentPosition;
        public GeoCoordinate GetCurrentPosition
        {
            get
            {
                if (this._geolocator == null)
                {
                    return null;
                }
                return _currentPosition;
            }
            private set { _currentPosition = value; }
        }

        private GeolocationAccessStatus? _status = null;
        private Geolocator _geolocator;

        public async Task RefreshPositionAsync()
        {
            this._status = await Geolocator.RequestAccessAsync();
            this._geolocator = new Geolocator();
            Geoposition position = await this._geolocator.GetGeopositionAsync();

            this.GetCurrentPosition = new GeoCoordinate()
            {
                Latitude = position.Coordinate.Latitude,
                Longitude = position.Coordinate.Longitude
            };
        }
    }
}
