using System.Collections.Generic;
using System.Threading.Tasks;
using Smartbourg.DataAccessLayer.Models.Parkings;

namespace Smartbourg.DataAccessLayer.Services
{
    public interface IParkingService
    {
        Task<List<Parking>> RetrieveParkings();
    }
}