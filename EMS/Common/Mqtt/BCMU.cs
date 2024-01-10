using EMS.Model;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;

namespace TNCN.EMS.Common.Mqtt
{
    public class BCMU
    {
        public int id { get; set; }
        public int state { get; set; }
        /// <summary>
        /// 电池簇总电压
        /// </summary>
        public double cl_group_volt { get; set; }
        /// <summary>
        /// 电池簇总电流
        /// </summary>
        public double cl_group_curr { get; set; }
        /// <summary>
        /// 电池簇总SOC
        /// </summary>
        public double cl_group_soc { get; set; }
        /// <summary>
        /// 电池簇总SOH
        /// </summary>
        public double cl_group_soh { get; set; }
        /// <summary>
        /// 电池簇平均温度
        /// </summary>
        public double cl_batt_avg_temp { get; set; }
        /// <summary>
        /// 电池簇单体最低电压
        /// </summary>
        public double cl_batt_min_volt { get; set; }
        /// <summary>
        /// 电池簇单体最高电压
        /// </summary>
        public double cl_batt_max_volt { get; set; }
        /// <summary>
        /// 电池簇单体最低温度
        /// </summary>
        public double cl_batt_min_temp { get; set; }
        /// <summary>
        /// 电池簇单体最高温度
        /// </summary>
        public double cl_batt_max_temp { get; set; }
        /// <summary>
        /// 电池簇单体最低电压编号
        /// </summary>
        public int cl_batt_min_volt_cell { get; set; }
        /// <summary>
        /// 电池簇单体最高电压编号
        /// </summary>
        public int cl_batt_max_volt_cell { get; set; }
        /// <summary>
        /// 电池簇单体最低温度编号
        /// </summary>
        public int cl_batt_min_temp_sensor { get; set; }
        /// <summary>
        /// 电池簇单体最高温度编号
        /// </summary>
        public int cl_batt_max_temp_sensor { get; set; }
        /// <summary>
        /// 电池最大充电功率
        /// </summary>
        public double cl_max_chg_power { get; set; }
        /// <summary>
        /// 电池最大放电功率
        /// </summary>
        public double cl_max_dischg_power { get; set; }
        /// <summary>
        /// 单次充电量
        /// </summary>
        public double cl_single_chg_capacity { get; set; }
        /// <summary>
        /// 单次放电量
        /// </summary>
        public double cl_dischg_cap { get; set; }
        /// <summary>
        /// 累计充电量
        /// </summary>
        public double cl_accum_charge { get; set; }
        /// <summary>
        /// 累计放电量
        /// </summary>
        public double cl_accum_dischg { get; set; }
        /// <summary>
        /// 剩余电量
        /// </summary>
        public double cl_remaining_cap { get; set; }
        /// <summary>
        /// 电池组平均电压
        /// </summary>
        public double cl_batt_avg_volt { get; set; }
        /// <summary>
        /// 绝缘电阻RP
        /// </summary>
        public double cl_pos_ins_res { get; set; }
        /// <summary>
        /// 绝缘电阻RN
        /// </summary>
        public double cl_neg_ins_res { get; set; }
        /// <summary>
        /// DC母线电压（电池堆电压）
        /// </summary>
        public double bms_pack_volt { get; set; }
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public long time { get; set; }

        public List<BMU> bmus { get; set; }
        public BCMU() {
            this.bmus = new List<BMU>();
            this.id = 1;
            this.state = 1;
            this.cl_batt_max_volt = 11.1;
            this.cl_group_curr = 12.1;
            this.cl_group_soc = 13.1;
            this.cl_group_soh = 14.1;
            this.cl_batt_avg_temp = 15.1;
            this.cl_batt_min_volt = 16.1;
            this.cl_batt_min_volt_cell = 11;
            this.cl_batt_max_volt = 1.1;
            this.cl_batt_max_volt_cell = 12;
            this.cl_batt_min_temp = 1.3;
            this.cl_batt_min_temp_sensor = 12;
            this.cl_batt_max_temp = 12;
            this.cl_batt_max_temp_sensor = 12;
            this.cl_max_chg_power = 12;
            this.cl_max_dischg_power = 12;
            this.cl_single_chg_capacity = 12;
            this.cl_dischg_cap = 12;
            this.cl_accum_charge = 12;
            this.cl_accum_dischg = 12;
            this.cl_remaining_cap = 12;
            this.cl_batt_avg_volt = 12;
            this.cl_pos_ins_res = 12;
            this.cl_neg_ins_res = 12;
            this.bms_pack_volt = 12;

            BMU bmu = new BMU();
            bmus.Add(bmu);
        }
        public BCMU(BatteryTotalModel batteryTotalModel) {
            this.bmus = new List<BMU>();
            if (batteryTotalModel.BCMUID == null)
            {
                this.id = 0;
            }
            else {
                this.id = int.Parse(batteryTotalModel.BCMUID);
            }
            
            Console.WriteLine(batteryTotalModel.BCMUID);
            this.state = batteryTotalModel.StateBCMU;
            this.cl_batt_max_volt = batteryTotalModel.TotalVoltage;
            this.cl_group_curr = batteryTotalModel.TotalCurrent;
            this.cl_group_soc = batteryTotalModel.TotalSOC;
            this.cl_group_soh = batteryTotalModel.TotalSOH;
            this.cl_batt_avg_temp = batteryTotalModel.AverageTemperature;
            this.cl_batt_min_volt = batteryTotalModel.MinVoltage;
            this.cl_batt_min_volt_cell = batteryTotalModel.MinVoltageIndex;
            this.cl_batt_max_volt = batteryTotalModel.MaxVoltage;
            this.cl_batt_max_volt_cell = batteryTotalModel.MaxVoltageIndex;
            this.cl_batt_min_temp = batteryTotalModel.MinTemperature;
            this.cl_batt_min_temp_sensor = batteryTotalModel.MinTemperatureIndex;
            this.cl_batt_max_temp = batteryTotalModel.MaxTemperature;
            this.cl_batt_max_temp_sensor = batteryTotalModel.MaxTemperatureIndex;
            this.cl_max_chg_power = batteryTotalModel.BatMaxChgPower;
            this.cl_max_dischg_power = batteryTotalModel.BatMaxDischgPower;
            this.cl_single_chg_capacity = batteryTotalModel.OneChgCoulomb;
            this.cl_dischg_cap = batteryTotalModel.OneDischgCoulomb;
            this.cl_accum_charge = batteryTotalModel.TotalChgCoulomb;
            this.cl_accum_dischg = batteryTotalModel.TotalDischgCoulomb;
            this.cl_remaining_cap = batteryTotalModel.RestCoulomb;
            this.cl_batt_avg_volt = batteryTotalModel.AvgVol;
            this.cl_pos_ins_res = batteryTotalModel.IResistanceRP;
            this.cl_neg_ins_res = batteryTotalModel.IResistanceRN;
            this.bms_pack_volt = batteryTotalModel.DCVoltage;
            this.time = DateTimeUtil.ConvertDateTimeToLong(batteryTotalModel.CurrentTime);

            foreach (var series in batteryTotalModel.Series)
            {
                BMU bmu = new BMU(series);
                bmus.Add(bmu);
            }

        }
    }
}
