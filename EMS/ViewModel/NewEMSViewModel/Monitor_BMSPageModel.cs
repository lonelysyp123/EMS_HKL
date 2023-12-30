using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_BMSPageModel
    {
        private BMSDataService[] DevServices;
        public Monitor_BMSPageModel(BMSDataService[] services)
        {
            DevServices = services;
            for (int i = 0; i < DevServices.Length; i++)
            {

            }
        }


    }
}
