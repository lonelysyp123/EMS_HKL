using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Mqtt
{
    internal class Pcs
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
        /// 直流累计充电电量
        /// </summary>
        public double dcdc_accum_disch { get; set; }
        public double dcdc_accum_chrg { get; set; }
        public double dcdc_br1_bus_volt { get; set; }
        public double dcdc_br1_ctrl { get; set; }
        public double dcdc_br1_curr_set { get; set; }
        public double dcdc_br1_pwr_set { get; set; }
        public double bat_ll_volt { get; set; }
        public double dterm_volt { get; set; }
        public double bms_max_chg_curr { get; set; }
        public double bms_max_dischg_curr { get; set; }
        public double up_volt_limit { get; set; }
        public double low_volt_limit { get; set; }
        public double high_volt_set { get; set; }
        public double low_volt_set { get; set; }


        public long time { get; set; }
    }
}
