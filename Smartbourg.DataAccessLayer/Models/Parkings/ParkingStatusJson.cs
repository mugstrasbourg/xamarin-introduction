using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smartbourg.DataAccessLayer.Models.Parkings
{
    [DataContract]
    internal class ParkingStatusJson
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "ds")]
        public string Status { get; set; }

        [DataMember(Name = "df")]
        public string FreePlaces { get; set; }

        [DataMember(Name = "dt")]
        public string TotalPlaces { get; set; }
    }
}
