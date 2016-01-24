using DemoXamarin.Business.Models;
using GalaSoft.MvvmLight;
using Smartbourg.DataAccessLayer.Models.Parkings;
using Smartbourg.DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoXamarin.Business.ViewModels
{
    public class ParkingViewModel : ViewModelBase
    {
        private ObservableCollection<GeolocalizableModel> _parkings;
        private IParkingService _parkingService;
        private IGpsProvider _gpsProvider;

        public ObservableCollection<GeolocalizableModel> Parkings
        {
            get
            {
                if(this._parkings == null)
                {
                    this._parkings = new ObservableCollection<GeolocalizableModel>();
                }

                return this._parkings;
            }
            set
            {
                this.Set(ref this._parkings, value);
            }
        }

        public ParkingViewModel(IParkingService parkingService,
            IGpsProvider gpsProvider)
        {
            this._parkingService = parkingService;
            this._gpsProvider = gpsProvider;
        }

        public async Task ReloadDataAsync()
        {
            GeoCoordinate coordinates = null;
            List<GeolocalizableModel> result = new List<GeolocalizableModel>();
            await this._gpsProvider.RefreshPositionAsync();

            if(this._gpsProvider.GeolocationAvailable)
            {
                coordinates = this._gpsProvider.GetCurrentPosition;
            }

            foreach (Parking parking in await this._parkingService.RetrieveParkings())
            {
                result.Add(new GeolocalizableModel()
                {
                    Name = parking.Name,
                    Latitude = parking.Latitude,
                    Longitude = parking.Longitude,
                    Distance = coordinates != null ? Distance(coordinates.Latitude, coordinates.Longitude, parking.Latitude, parking.Longitude) : 0d
                });
            }

            if(coordinates == null)
            {
                this.Parkings = new ObservableCollection<GeolocalizableModel>(result.OrderBy(x => x.Name));
            }
            else
            {
                this.Parkings = new ObservableCollection<GeolocalizableModel>(result.OrderBy(x => x.Distance));
            }
        }

        public static double Distance(double Lat1,
                 double Long1, double Lat2, double Long2)
        {
            /*
                The Haversine formula according to Dr. Math.
                http://mathforum.org/library/drmath/view/51879.html

                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c

                Where
                    * dlon is the change in longitude
                    * dlat is the change in latitude
                    * c is the great circle distance in Radians.
                    * R is the radius of a spherical Earth.
                    * The locations of the two points in 
                        spherical coordinates (longitude and 
                        latitude) are lon1,lat1 and lon2, lat2.
            */
            double dDistance = Double.MinValue;
            double dLat1InRad = Lat1 * (Math.PI / 180.0);
            double dLong1InRad = Long1 * (Math.PI / 180.0);
            double dLat2InRad = Lat2 * (Math.PI / 180.0);
            double dLong2InRad = Long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Asin(Math.Sqrt(a));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

            return dDistance;
        }
    }
}
