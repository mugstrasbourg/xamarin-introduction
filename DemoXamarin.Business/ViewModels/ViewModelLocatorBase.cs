using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXamarin.Business.ViewModels
{
    public class ViewModelLocatorBase
    {
        protected bool IsInDesign => ViewModelBase.IsInDesignModeStatic && !UseDesignTimeData;

        private bool UseDesignTimeData
        {
            get
            {
                return false;
            }
        }

        public ViewModelLocatorBase()
        {
            // Declare dependancies
        }
    }
}
