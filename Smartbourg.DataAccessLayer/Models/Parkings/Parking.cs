using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbourg.DataAccessLayer.Models.Parkings
{
    public class Parking
    {

        public Parking(string status)
        {
            this.Status = SetStatus(status);
        }

        private ParkingStatus SetStatus(string status)
        {
            ParkingStatus parkingStatus = ParkingStatus.CLOSED;

            if (status == "status_1")
            {
                parkingStatus = ParkingStatus.OPEN;
            }
            else if (status == "status_2")
            {
                parkingStatus = ParkingStatus.FULL;
            }

            return parkingStatus;
        }

        public int FreePlaces { get; internal set; }
        public string Id { get; internal set; }
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
        public string Name { get; set; }
        public int TotalPlaces { get; internal set; }
        public ParkingStatus Status { get; internal set; }
    }

    public enum ParkingStatus
    {
        OPEN,
        FULL,
        NOT_AVAILABLE,
        CLOSED
    }
}
