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
        private double voltage_A;
        public double Voltage_A
        {
            get { return voltage_A; }
            set
            {
                SetProperty(ref voltage_A, value);
            }
        }

        private double voltage_B;
        public double Voltage_B
        {
            get { return voltage_B; }
            set
            {
                SetProperty(ref voltage_B, value);
            }
        }

        private double voltage_C;
        public double Voltage_C
        {
            get { return voltage_C; }
            set
            {
                SetProperty(ref voltage_C, value);
            }
        }

        /*ABC三相电流*/
        private double current_A;
        public double Current_A
        {
            get { return current_A; }
            set
            {
                SetProperty(ref current_A, value);
            }
        }

        private double current_B;
        public double Current_B
        {
            get { return current_B; }
            set
            {
                SetProperty(ref current_B, value);
            }
        }

        private double current_C;
        public double Current_C
        {
            get { return current_C; }
            set
            {
                SetProperty(ref current_C, value);
            }
        }

        /*ABC三相有功功率和总功率*/
        private double activePower_A;
        public double ActivePower_A
        {
            get { return activePower_A; }
            set
            {
                SetProperty(ref activePower_A, value);
            }
        }

        private double activePower_B;
        public double ActivePower_B
        {
            get { return activePower_B; }
            set
            {
                SetProperty(ref activePower_B, value);
            }
        }

        private double activePower_C;
        public double ActivePower_C
        {
            get { return activePower_C; }
            set
            {
                SetProperty(ref activePower_C, value);
            }
        }

        private double activePower_Total;
        public double ActivePower_Total
        {
            get { return activePower_Total; }
            set
            {
                SetProperty(ref activePower_Total, value);
            }
        }

        /*ABC三相无功功率和总功率*/
        private double wattlessPower_A;
        public double WattlessPower_A
        {
            get { return wattlessPower_A; }
            set
            {
                SetProperty(ref wattlessPower_A, value);
            }
        }

        private double wattlessPower_B;
        public double WattlessPower_B
        {
            get { return wattlessPower_B; }
            set
            {
                SetProperty(ref wattlessPower_B, value);
            }
        }

        private double wattlessPower_C;
        public double WattlessPower_C
        {
            get { return wattlessPower_C; }
            set
            {
                SetProperty(ref wattlessPower_C, value);
            }
        }

        private double wattlessPower_Total;
        public double WattlessPower_Total
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
            this.Voltage_A = model.Voltage_A;
            this.Voltage_B = model.Voltage_B;
            this.Voltage_C = model.Voltage_C;
            this.Current_A = model.Electric_A;
            this.Current_B = model.Electric_B;
            this.Current_C = model.Electric_C;
            this.ActivePower_A = model.ActivePower_A;
            this.ActivePower_B = model.ActivePower_B;
            this.ActivePower_C = model.ActivePower_C;
            this.ActivePower_Total = model.ActivePower_Total;
            this.WattlessPower_A = model.ReactivePower_A;
            this.WattlessPower_B = model.ReactivePower_B;
            this.WattlessPower_C = model.ReactivePower_C;
            this.WattlessPower_Total = model.ReactivePower_Total;
        }
    }
}
