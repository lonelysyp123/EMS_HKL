using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;

namespace TNCN.EMS.Common.Mqtt
{
    internal class SmartMeter
    {
        /// <summary>
        /// 智能电表id
        /// </summary>
        public int id;
        /// <summary>
        /// 电表编号
        /// </summary>
        public string sn;
        /// <summary>
        /// Utc时间戳
        /// </summary>
        public long ps_Utc;
        /// <summary>
        /// A相电压
        /// </summary>
        public double A_curr;
        /// <summary>
        /// B相电压
        /// </summary>
        public double B_curr;
        /// <summary>
        /// C相电压
        /// </summary>
        public double C_curr;
        /// <summary>
        /// A相电流
        /// </summary>
        public double A_volt;
        /// <summary>
        /// B相电流
        /// </summary>
        public double B_volt;
        /// <summary>
        /// C相电流
        /// </summary>
        public double C_volt;
        /// <summary>
        /// 总功率
        /// </summary>
        public double ps_Pt;
        /// <summary>
        /// A相功率因数
        /// </summary>
        public double A_pf;
        /// <summary>
        /// B相功率因数
        /// </summary>
        public double B_pf;
        /// <summary>
        /// C相功率因数
        /// </summary>
        public double C_pf;
        public double ps_Ft;
        /// <summary>
        /// A相有功功率
        /// </summary>
        public double A_Fa;
        /// <summary>
        /// B相有功功率
        /// </summary>
        public double B_Fb;
        /// <summary>
        /// C相有功功率
        /// </summary>
        public double C_Fc;
        /// <summary>
        /// 正向有功尖峰电量
        /// </summary>
        public double ps_zxyg1;
        /// <summary>
        /// 正向有功尖峰电量
        /// </summary>
        public double ps_zxyg2;
        /// <summary>
        /// 正向有功尖峰电量
        /// </summary>
        public double ps_zxyg3;
        /// <summary>
        /// 正向有功谷峰电量
        /// </summary>
        public double ps_zxyg4;
        /// <summary>
        /// 反向有功尖峰电量
        /// </summary>
        public double ps_fxyg1;
        /// <summary>
        /// 反向有功高峰电量
        /// </summary>
        public double ps_fxyg2;
        /// <summary>
        /// 反向有功平峰电量
        /// </summary>
        public double ps_fxyg3;
        /// <summary>
        /// 反向有功谷峰电量
        /// </summary>
        public double ps_fxyg4;
        /// <summary>
        /// 变化
        /// </summary>
        public double ratio;
        /// <summary>
        /// 采集时间
        /// </summary>
        public long time;

        public SmartMeter(SmartMeterModel smartMeterModel) {
            this.id = 1;
            this.sn = smartMeterModel.SmartMeterNumber;
            this.time = DateTimeUtil.ConvertDateTimeToLong(DateTime.Now);
            this.A_curr = smartMeterModel.Electric_A;
            this.B_curr = smartMeterModel.Electric_B;
            this.C_curr = smartMeterModel.Electric_C;
            this.A_volt = smartMeterModel.Voltage_A;
            this.B_volt = smartMeterModel.Voltage_B;
            this.C_volt = smartMeterModel.Voltage_C;
        }
    }
}
