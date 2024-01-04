using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_SmartMeterPageModel:ViewModelBase
    {
        #region SmartMeterParameters
        /*ABC三相电压*/
        private string voltage_A;
        public string Voltage_A
        {
            get { return voltage_A; }
            set
            {
                SetProperty(ref voltage_A, value);
            }
        }

        private string voltage_B;
        public string Voltage_B
        {
            get { return voltage_B; }
            set
            {
                SetProperty(ref voltage_B, value);
            }
        }

        private string voltage_C;
        public string Voltage_C
        {
            get { return voltage_C; }
            set
            {
                SetProperty(ref voltage_C, value);
            }
        }

        /*ABC三相电流*/
        private string current_A;
        public string Current_A
        {
            get { return current_A; }
            set
            {
                SetProperty(ref current_A, value);
            }
        }

        private string current_B;
        public string Current_B
        {
            get { return current_B; }
            set
            {
                SetProperty(ref current_B, value);
            }
        }

        private string current_C;
        public string Current_C
        {
            get { return current_C; }
            set
            {
                SetProperty(ref current_C, value);
            }
        }

        /*ABC三相有功功率和总功率*/
        private string activePower_A;
        public string ActivePower_A
        {
            get { return activePower_A; }
            set
            {
                SetProperty(ref activePower_A, value);
            }
        }

        private string activePower_B;
        public string ActivePower_B
        {
            get { return activePower_B; }
            set
            {
                SetProperty(ref activePower_B, value);
            }
        }

        private string activePower_C;
        public string ActivePower_C
        {
            get { return activePower_C; }
            set
            {
                SetProperty(ref activePower_C, value);
            }
        }

        private string activePower_Total;
        public string ActivePower_Total
        {
            get { return activePower_Total; }
            set
            {
                SetProperty(ref activePower_Total, value);
            }
        }

        /*ABC三相无功功率和总功率*/
        private string wattlessPower_A;
        public string WattlessPower_A
        {
            get { return wattlessPower_A; }
            set
            {
                SetProperty(ref wattlessPower_A, value);
            }
        }

        private string wattlessPower_B;
        public string WattlessPower_B
        {
            get { return wattlessPower_B; }
            set
            {
                SetProperty(ref wattlessPower_B, value);
            }
        }

        private string wattlessPower_C;
        public string WattlessPower_C
        {
            get { return wattlessPower_C; }
            set
            {
                SetProperty(ref wattlessPower_C, value);
            }
        }

        private string wattlessPower_Total;
        public string WattlessPower_Total
        {
            get { return wattlessPower_Total; }
            set
            {
                SetProperty(ref wattlessPower_Total, value);
            }
        }
        #endregion

        public Monitor_SmartMeterPageModel()
        {

        }

        public void DataRefresh(SmartMeterModel model)
        {
            Voltage_A = model.Voltage_A.ToString();
            Voltage_B = model.Voltage_B.ToString();
            Voltage_C = model.Voltage_C.ToString();
            Current_A = model.Electric_A.ToString();
            Current_B = model.Electric_B.ToString();
            Current_C = model.Electric_C.ToString();
            ActivePower_A = model.ActivePower_A.ToString();
            ActivePower_B = model.ActivePower_B.ToString();
            ActivePower_C = model.ActivePower_C.ToString();
            ActivePower_Total = model.ActivePower_Total.ToString();
            WattlessPower_A = model.ReactivePower_A.ToString();
            WattlessPower_B = model.ReactivePower_B.ToString();
            WattlessPower_C = model.ReactivePower_C.ToString();
            WattlessPower_Total = model.ReactivePower_Total.ToString();
        }
    }
}
