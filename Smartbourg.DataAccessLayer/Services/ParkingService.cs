using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartbourg.DataAccessLayer.Models;
using Smartbourg.DataAccessLayer.Models.Parkings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Smartbourg.DataAccessLayer.Services
{
    public class ParkingService : IParkingService
    {
        private readonly string ParkingStatusPath = "http://strasmap.eu/remote.amf.json/Parking.status";
        private readonly string ParkingGeometryPath = "http://strasmap.eu/remote.amf.json/Parking.geometry";

        public async Task<List<Parking>> RetrieveParkings()
        {
            List<Parking> parkings = new List<Parking>();
            List<ParkingStatusJson> parkingsStatus;
            List<ParkingGeometryJson> parkingsGeometry;

            using (StreamReader reader = new StreamReader(await GetResponseStreamAsync(this.ParkingStatusPath)))
            {
                JObject json = JObject.Parse(reader.ReadToEnd());
                parkingsStatus = JsonConvert.DeserializeObject<List<ParkingStatusJson>>(json["s"].ToString());
            }

            using (StreamReader reader = new StreamReader(await GetResponseStreamAsync(this.ParkingGeometryPath)))
            {
                JObject json = JObject.Parse(reader.ReadToEnd());
                parkingsGeometry = JsonConvert.DeserializeObject<List<ParkingGeometryJson>>(json["s"].ToString());
            }

            foreach (ParkingStatusJson parking in parkingsStatus)
            {
                ParkingGeometryJson geometry = parkingsGeometry.Where(x => x.Id == parking.Id).FirstOrDefault();
                int totalPlaces = 0;
                int freePlaces = 0;

                if(geometry == null)
                {
                    continue;
                }

                if(!(int.TryParse(parking.TotalPlaces, out totalPlaces) && int.TryParse(parking.FreePlaces, out freePlaces)))
                {
                    continue;
                }
                
                parkings.Add(new Parking(parking.Status)
                {
                    Id = parking.Id,
                    Latitude = geometry.Localisation.Latitude,
                    Longitude = geometry.Localisation.Longitude,
                    Name = geometry.Name,
                    TotalPlaces = totalPlaces,
                    FreePlaces = freePlaces,
                });
            }
            
            return parkings;
        }


        private static async Task<Stream> GetResponseStreamAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            return response.GetResponseStream();
        }
    }
}
