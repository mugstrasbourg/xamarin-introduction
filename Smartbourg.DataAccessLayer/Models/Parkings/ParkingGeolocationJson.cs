using System.Runtime.Serialization;

namespace Smartbourg.DataAccessLayer.Models.Parkings
{
    [DataContract]
    internal class ParkingGeolocationJson
    {
        [DataMember(Name = "x")]
        public double Longitude { get; set; }

        [DataMember(Name = "y")]
        public double Latitude { get; set; }
    }
}