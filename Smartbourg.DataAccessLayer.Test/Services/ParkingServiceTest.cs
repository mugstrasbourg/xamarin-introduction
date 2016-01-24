using Smartbourg.DataAccessLayer.Models.Parkings;
using Smartbourg.DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartbourg.DataAccessLayer.Test.Services
{
    public class ParkingServiceTest
    {
        [Fact]
        public async Task RetrieveParkings()
        {
            List<Parking> parkings = await new ParkingService().RetrieveParkings();

            Assert.True(parkings.Count > 0, "It should have at least one parking");
            foreach (Parking parking in parkings)
            {
                Assert.True(!string.IsNullOrEmpty(parking.Name), string.Format("Parking name is null for ID {0}", parking.Id));
            }
        }
    }
}
