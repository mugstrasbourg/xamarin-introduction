using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smartbourg.DataAccessLayer.Models.Parkings
{
    [DataContract]
    internal class ParkingGeometryJson
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "ln")]
        public string Name { get; set; }

        [DataMember(Name = "go")]
        public ParkingGeolocationJson Localisation { get; set; }
    }
}
