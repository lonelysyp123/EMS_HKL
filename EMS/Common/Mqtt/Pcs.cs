using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;

namespace TNCN.EMS.Common.Mqtt
{
    public class Pcs
    {
        /// <summary>
        /// pcs序号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 环境温度
        /// </summary>
        public double dcdc_amb_temp { get; set; }
        /// <summary>
        /// 模块温度
        /// </summary>
        public double dcdc_mod_temp { get; set; }
        /// <summary>
        /// 直流功率
        /// </summary>
        public double dcdc_br1_pwr { get; set; }
        /// <summary>
        /// 直流电压
        /// </summary>
        public double dcdc_br1_volt { get; set; }
        /// <summary>
        /// 直流电流
        /// </summary>
        public double dcdc_br1_curr { get; set; }
        /// <summary>
        /// 直流累计放电电量
        /// </summary>
        public double dcdc_accum_disch { get; set; }
        /// <summary>
        /// 直流累计充电电量
        /// </summary>
        public double dcdc_accum_chrg { get; set; }
        /// <summary>
        /// BUS侧电压
        /// </summary>
        public double dcdc_br1_bus_volt { get; set; }
        /// <summary>
        /// 直流控制模式
        /// </summary>
        public double dcdc_br1_ctrl { get; set; }
        /// <summary>
        /// 直流电流设置
        /// </summary>
        public double dcdc_br1_curr_set { get; set; }
        /// <summary>
        /// 直流功率设置
        /// </summary>
        public double dcdc_br1_pwr_set { get; set; }
        /// <summary>
        /// 电池下限电压
        /// </summary>
        public double bat_ll_volt { get; set; }
        /// <summary>
        /// 放电终止电压
        /// </summary>
        public double dterm_volt { get; set; }
        /// <summary>
        /// 最大充电电流
        /// </summary>
        public double bms_max_chg_curr { get; set; }
        /// <summary>
        /// 最大放电电流
        /// </summary>
        public double bms_max_dischg_curr { get; set; }
        /// <summary>
        /// BUS测：上限电压
        /// </summary>
        public double up_volt_limit { get; set; }
        /// <summary>
        /// BUS测：下限电压
        /// </summary>
        public double low_volt_limit { get; set; }
        /// <summary>
        /// BUS测：高压设置
        /// </summary>
        public double high_volt_set { get; set; }
        /// <summary>
        /// BUS测：低压设置
        /// </summary>
        public double low_volt_set { get; set; }
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public long time { get; set; }
        public Pcs(PCSModel pcsModel) {
            this.id = 1;
            this.dcdc_amb_temp = pcsModel.AmbientTemperature;
            this.dcdc_mod_temp = pcsModel.ModuleTemperature;
            this.dcdc_br1_pwr = pcsModel.DcBranch1DCPower;
            this.dcdc_br1_volt = pcsModel.DcBranch1DCVol;
            this.dcdc_br1_curr = pcsModel.DcBranch1DCCur;
            this.dcdc_accum_disch = pcsModel.DcBranch1DisCharLow;
            this.dcdc_accum_chrg = pcsModel.DcBranch1CharLow;
            this.dcdc_br1_bus_volt = pcsModel.DcBranch1BUSVol;
            this.dcdc_br1_ctrl = pcsModel.ControlStateFlagPCS;
        }
    }
}
