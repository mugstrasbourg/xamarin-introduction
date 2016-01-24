using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXamarin.Business
{
    public interface IGpsProvider
    {
        bool GeolocationAvailable { get; }
        GeoCoordinate GetCurrentPosition { get; }
        Task RefreshPositionAsync();
    }

    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
