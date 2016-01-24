using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Smartbourg.DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXamarin.Business.ViewModels
{
    public class ViewModelLocatorBase
    {
        private bool UseDesignTimeData
        {
            get
            {
                return false;
            }
        }
        protected bool IsInDesign => ViewModelBase.IsInDesignModeStatic && !UseDesignTimeData;

        public ParkingViewModel ParkingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParkingViewModel>();
            }
        }

        public ViewModelLocatorBase()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if(!IsInDesign)
            {
                SimpleIoc.Default.Register<IParkingService, ParkingService>();
            }

            SimpleIoc.Default.Register<ParkingViewModel>();
        }
    }
}
