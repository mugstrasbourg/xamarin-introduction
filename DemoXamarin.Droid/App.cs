using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DemoXamarin.Business.ViewModels;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Ioc;
using DemoXamarin.Business;

namespace DemoXamarin.Droid
{
    public static class App
    {
        public static Context Context { get; set; }

        private static ViewModelLocatorBase _locator;
        public static ViewModelLocatorBase Locator
        {
            get
            {
                if(_locator == null)
                {
                    SimpleIoc.Default.Register<IGpsProvider, GpsProvider>();

                    _locator = new ViewModelLocatorBase();
                }

                return _locator;
            }
        }


    }
}