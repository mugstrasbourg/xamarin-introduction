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

        public ParkingViewModel(IParkingService parkingService)
        {
            this._parkingService = parkingService;
        }

        public async Task ReloadDataAsync()
        {
            foreach (Parking parking in (await this._parkingService.RetrieveParkings()).OrderBy(x => x.Name))
            {
                this.Parkings.Add(new GeolocalizableModel()
                {
                    Name = parking.Name,
                    Latitude = parking.Latitude,
                    Longitude = parking.Longitude
                });
            }
        }
    }
}
