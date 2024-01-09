using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class SmartElectricityMeter
    {
        /// <summary>
        /// 直流电压
        /// </summary>
        //public double DcVoltage { get; set; }
        /// <summary>
        /// 直流电流
        /// </summary>
        //public double DcCurrent { get; set; }
        /// <summary>
        /// 功率值
        /// </summary>
        //public double PowerValue { get; set; }
        /// <summary>
        /// 总正向一次侧电能
        /// </summary>
        public double TotalForwardPrimaryEnergy { get; set; }
        /// <summary>
        /// 总反向一次侧电能
        /// </summary>
        public double TotalReversePrimaryEnergy { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public double Voltage { get; set; }
        /// <summary>
        /// 电流
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// 功率
        /// </summary>
        public double Power { get; set; }
        /// <summary>
        /// 总正向有功电能
        /// </summary>
        public double TotalActiveEnergy { get; set; }
        /// <summary>
        /// 总尖正向有功电能
        /// </summary>
        public double TotalSpikesActiveEnergy { get; set; }
        /// <summary>
        /// 总峰正向有功电能
        /// </summary>
        public double TotalPeakActiveEnergy { get; set; }
        /// <summary>
        /// 总平正向有功电能
        /// </summary>
        public double TotalFlatActiveEnergy { get; set; }
        /// <summary>
        /// 总谷正向有功电能
        /// </summary>
        public double TotalValleyActiveEnergy { get; set; }
        /// <summary>
        /// 当前月总正向有功电能
        /// </summary>
        public double CurrMonthTotalActiveEnergy { get; set; }
        /// <summary>
        /// 当前月尖正向有功电能
        /// </summary>
        public double CurrMonthSpikesActiveEnergy { get; set; }
        /// <summary>
        /// 当前月峰正向有功电能
        /// </summary>
        public double CurrMonthPeakActiveEnergy { get; set; }
        /// <summary>
        /// 当前月平正向有功电能
        /// </summary>
        public double CurrMonthFlatRateActiveEnergy { get; set; }
        /// <summary>
        /// 当前月谷正向有功电能
        /// </summary>
        public double CurrMonthValleyActiveEnergy { get; set; }
        /// <summary>
        /// 总反向有功电能
        /// </summary>
        public double TotalReverseActiveEnergy { get; set; }
        /// <summary>
        /// 总尖反向有功电能
        /// </summary>
        public double TotalSpikesReverseActiveEnergy { get; set; }
        /// <summary>
        /// 总峰反向有功电能
        /// </summary>
        public double TotalPeakReverseActiveEnergy { get; set; }
        /// <summary>
        /// 总平反向有功电能
        /// </summary>
        public double TotalFlatReverseActiveEnergy { get; set; }
        /// <summary>
        /// 总谷反向有功电能
        /// </summary>
        public double TotalValleyReverseActiveEnergy { get; set; }
        /// <summary>
        /// 当前月总反向有功电能
        /// </summary>
        public double CurrMonthTotalReverseActiveEnergy { get; set; }
        /// <summary>
        /// 当前月尖反向有功电能
        /// </summary>
        public double CurrMonthSpikesReverseActiveEnergy { get; set; }
        /// <summary>
        /// 当前月峰反向有功电能
        /// </summary>
        public double CurrMonthPeakReverseActiveEnergy { get; set; }
        /// <summary>
        /// 当前月平反向有功电能
        /// </summary>
        public double CurrMonthFlatReverseActiveEnergy { get; set; }
        /// <summary>
        /// 当前月谷反向有功电能
        /// </summary>
        public double CurrMonthValleyReverseActiveEnergy { get; set; }
    }
}
