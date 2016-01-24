using DemoXamarin.Business;
using DemoXamarin.Business.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXamarin.Dos
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        public ViewModelLocator()
        {
            if (!this.IsInDesign)
            {
                SimpleIoc.Default.Register<IGpsProvider, GpsProvider>();
            }
        }
    }
}
