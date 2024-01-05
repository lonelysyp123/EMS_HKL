using EMS.Service;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.StrategyManage
{
    public class BMSManager
    {
        private List<BMSDataService> bmsDataServices;
        public List<BMSDataService> BMSDataServices { get; private set; } //封装，不能set

        public BMSManager()
        {
            BMSDataServices = new List<BMSDataService>();
        }

        public void AddBMSDev(BMSDataService service)
        {
            if(!bmsDataServices.Contains(service))
                bmsDataServices.Add(service);
        }

        public void RemoveBMSDev(BMSDataService service)
        {
            if (bmsDataServices.Contains(service))
                bmsDataServices.Remove(service);
        }
    }
}
