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
        #region 负荷跟踪表
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

        #region 电量计量表
        /// <summary>
        /// 一次侧电能
        /// </summary>
        private double totalForwardPrimaryEnergy;
        public double TotalForwardPrimaryEnergy
        {
            get { return totalForwardPrimaryEnergy; }
            set
            {
                SetProperty(ref totalForwardPrimaryEnergy, value);
            }
        }
        private double totalReversePrimaryEnergy;
        public double TotalReversePrimaryEnergy
        {
            get { return totalReversePrimaryEnergy; }
            set
            {
                SetProperty(ref totalReversePrimaryEnergy, value);
            }
        }
        /// <summary>
        /// 电压、电流、功率
        /// </summary>
        private float voltage;
        public float Voltage
        {
            get { return voltage; }
            set
            {
                SetProperty(ref voltage, value);
            }
        }
        private float current;
        public float Current
        {
            get { return current; }
            set
            {
                SetProperty(ref current, value);
            }
        }
        private float power;
        public float Power
        {
            get { return power; }
            set
            {
                SetProperty(ref power, value);
            }
        }
        /// <summary>
        /// 总正向有功电能
        /// </summary>
        private double totalActiveEnergy;
        public double TotalActiveEnergy
        {
            get { return totalActiveEnergy; }
            set
            {
                SetProperty(ref totalActiveEnergy, value);
            }
        }
        private double totalSpikesActiveEnergy;
        public double TotalSpikesActiveEnergy
        {
            get { return totalSpikesActiveEnergy; }
            set
            {
                SetProperty(ref totalSpikesActiveEnergy, value);
            }
        }
        private double totalPeakActiveEnergy;
        public double TotalPeakActiveEnergy
        {
            get { return totalPeakActiveEnergy; }
            set
            {
                SetProperty(ref totalPeakActiveEnergy, value);
            }
        }
        private double totalFlatActiveEnergy;
        public double TotalFlatActiveEnergy
        {
            get { return totalFlatActiveEnergy; }
            set
            {
                SetProperty(ref totalFlatActiveEnergy, value);
            }
        }
        private double totalValleyActiveEnergy;
        public double TotalValleyActiveEnergy
        {
            get { return totalValleyActiveEnergy; }
            set
            {
                SetProperty(ref totalValleyActiveEnergy, value);
            }
        }
        /// <summary>
        /// 总反向有功电能
        /// </summary>
        private double totalReverseActiveEnergy;
        public double TotalReverseActiveEnergy
        {
            get { return totalReverseActiveEnergy; }
            set
            {
                SetProperty(ref totalReverseActiveEnergy, value);
            }
        }
        private double totalSpikesReverseActiveEnergy;
        public double TotalSpikesReverseActiveEnergy
        {
            get { return totalSpikesReverseActiveEnergy; }
            set
            {
                SetProperty(ref totalSpikesReverseActiveEnergy, value);
            }
        }
        private double totalPeakReverseActiveEnergy;
        public double TotalPeakReverseActiveEnergy
        {
            get { return totalPeakReverseActiveEnergy; }
            set
            {
                SetProperty(ref totalPeakReverseActiveEnergy, value);
            }
        }
        private double totalFlatReverseActiveEnergy;
        public double TotalFlatReverseActiveEnergy
        {
            get { return totalFlatReverseActiveEnergy; }
            set
            {
                SetProperty(ref totalFlatReverseActiveEnergy, value);
            }
        }
        private double totalValleyReverseActiveEnergy;
        public double TotalValleyReverseActiveEnergy
        {
            get { return totalValleyReverseActiveEnergy; }
            set
            {
                SetProperty(ref totalValleyReverseActiveEnergy, value);
            }
        }
        /// <summary>
        /// 月总正向有功电能
        /// </summary>
        private double currMonthTotalActiveEnergy;
        public double CurrMonthTotalActiveEnergy
        {
            get { return currMonthTotalActiveEnergy; }
            set
            {
                SetProperty(ref currMonthTotalActiveEnergy, value);
            }
        }
        private double currMonthSpikesActiveEnergy;
        public double CurrMonthSpikesActiveEnergy
        {
            get { return currMonthSpikesActiveEnergy; }
            set
            {
                SetProperty(ref currMonthSpikesActiveEnergy, value);
            }
        }
        private double currMonthPeakActiveEnergy;
        public double CurrMonthPeakActiveEnergy
        {
            get { return currMonthPeakActiveEnergy; }
            set
            {
                SetProperty(ref currMonthPeakActiveEnergy, value);
            }
        }
        private double currMonthFlatRateActiveEnergy;
        public double CurrMonthFlatRateActiveEnergy
        {
            get { return currMonthFlatRateActiveEnergy; }
            set
            {
                SetProperty(ref currMonthFlatRateActiveEnergy, value);
            }
        }
        private double currMonthValleyActiveEnergy;
        public double CurrMonthValleyActiveEnergy
        {
            get { return currMonthValleyActiveEnergy; }
            set
            {
                SetProperty(ref currMonthValleyActiveEnergy, value);
            }
        }
        /// <summary>
        /// 月总反向有功电能
        /// </summary>
        private double currMonthTotalReverseActiveEnergy;
        public double CurrMonthTotalReverseActiveEnergy
        {
            get { return currMonthTotalReverseActiveEnergy; }
            set
            {
                SetProperty(ref currMonthTotalReverseActiveEnergy, value);
            }
        }
        private double currMonthSpikesReverseActiveEnergy;
        public double CurrMonthSpikesReverseActiveEnergy
        {
            get { return currMonthSpikesReverseActiveEnergy; }
            set
            {
                SetProperty(ref currMonthSpikesReverseActiveEnergy, value);
            }
        }
        private double currMonthPeakReverseActiveEnergy;
        public double CurrMonthPeakReverseActiveEnergy
        {
            get { return currMonthPeakReverseActiveEnergy; }
            set
            {
                SetProperty(ref currMonthPeakReverseActiveEnergy, value);
            }
        }
        private double currMonthFlatReverseActiveEnergy;
        public double CurrMonthFlatReverseActiveEnergy
        {
            get { return currMonthFlatReverseActiveEnergy; }
            set
            {
                SetProperty(ref currMonthFlatReverseActiveEnergy, value);
            }
        }
        private double currMonthValleyReverseActiveEnergy;
        public double CurrMonthValleyReverseActiveEnergy
        {
            get { return currMonthValleyReverseActiveEnergy; }
            set
            {
                SetProperty(ref currMonthValleyReverseActiveEnergy, value);
            }
        }
        #endregion

        public Monitor_SmartMeterPageModel()
        {

        }

        #region 电表数据刷新方法
        public void DataRefresh(SmartElectricityMeterModel semodel)
        {
            /// <summary>
            /// 电量计量表解析
            /// <summary>
            //一次侧总有功电能
            this.TotalForwardPrimaryEnergy = semodel.TotalForwardPrimaryEnergy;
            this.TotalReversePrimaryEnergy = semodel.TotalReversePrimaryEnergy;
            //电压电流功率
            this.Voltage = semodel.Voltage;
            this.Current = semodel.Current;
            this.power = semodel.Power;
            //总正向有功电能
            this.totalActiveEnergy = semodel.TotalActiveEnergy;
            this.TotalSpikesActiveEnergy = semodel.TotalSpikesActiveEnergy;
            this.TotalPeakActiveEnergy = semodel.TotalPeakActiveEnergy;
            this.TotalFlatActiveEnergy = semodel.TotalFlatActiveEnergy;
            this.TotalValleyActiveEnergy = semodel.TotalValleyActiveEnergy;
            //总反向有功电能
            this.TotalReverseActiveEnergy = semodel.TotalReverseActiveEnergy;
            this.TotalSpikesReverseActiveEnergy = semodel.TotalSpikesReverseActiveEnergy;
            this.TotalPeakReverseActiveEnergy = semodel.TotalPeakReverseActiveEnergy;
            this.TotalFlatReverseActiveEnergy = semodel.TotalFlatReverseActiveEnergy;
            this.TotalValleyReverseActiveEnergy = semodel.TotalValleyReverseActiveEnergy;
            //月总正向有功电能
            this.CurrMonthTotalActiveEnergy = semodel.CurrMonthTotalActiveEnergy;
            this.CurrMonthSpikesActiveEnergy = semodel.CurrMonthSpikesActiveEnergy;
            this.CurrMonthPeakActiveEnergy = semodel.CurrMonthPeakActiveEnergy;
            this.CurrMonthFlatRateActiveEnergy = semodel.CurrMonthFlatRateActiveEnergy;
            this.CurrMonthValleyActiveEnergy = semodel.CurrMonthValleyActiveEnergy;
            //月总反向有功电能
            this.CurrMonthTotalReverseActiveEnergy = semodel.CurrMonthTotalReverseActiveEnergy;
            this.CurrMonthSpikesReverseActiveEnergy = semodel.CurrMonthSpikesReverseActiveEnergy;
            this.CurrMonthPeakReverseActiveEnergy = semodel.CurrMonthPeakReverseActiveEnergy;
            this.CurrMonthFlatReverseActiveEnergy = semodel.CurrMonthFlatReverseActiveEnergy;
            this.CurrMonthValleyReverseActiveEnergy = semodel.CurrMonthValleyReverseActiveEnergy;
        }

        public void DataRefresh(SmartMeterModel model)
        {
            /// <summary>
            ///负荷跟踪表解析
            /// <summary>
            //A,B,C三相电压
            this.Voltage_A = model.Voltage_A;
            this.Voltage_B = model.Voltage_B;
            this.Voltage_C = model.Voltage_C;
            //A,B,C三相电流
            this.Current_A = model.Electric_A;
            this.Current_B = model.Electric_B;
            this.Current_C = model.Electric_C;
            //A,B,C三相和总有功功率
            this.ActivePower_A = model.ActivePower_A;
            this.ActivePower_B = model.ActivePower_B;
            this.ActivePower_C = model.ActivePower_C;
            this.ActivePower_Total = model.ActivePower_Total;
            //A,B,C三相和总无功功率
            this.WattlessPower_A = model.ReactivePower_A;
            this.WattlessPower_B = model.ReactivePower_B;
            this.WattlessPower_C = model.ReactivePower_C;
            this.WattlessPower_Total = model.ReactivePower_Total;
        }
        #endregion
    }
}
