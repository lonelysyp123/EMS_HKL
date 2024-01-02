using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_SmartMeterPageModel : ViewModelBase
    {
        private string voltage_A;
        public string Voltage_A
        {
            get { return voltage_A; }
            set
            {
                SetProperty(ref voltage_A, value);
            }
        }

        public Monitor_SmartMeterPageModel()
        {

        }

        public void DataRefresh(SmartMeterModel model)
        {
            // 解析这个model，来填充viewmodel
            Voltage_A = model.Voltage_A.ToString();

        }
    }
}
