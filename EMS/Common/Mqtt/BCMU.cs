using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Mqtt
{
    public class BCMU
    {
        public int id { get; set; }
        public int state { get; set; }
        public double cl_group_volt { get; set; }
        public double cl_group_curr { get; set; }
        public double cl_group_soc { get; set; }
        public double cl_group_soh { get; set; }
        public double cl_batt_avg_temp { get; set; }
        public double cl_batt_min_volt { get; set; }
        public double cl_batt_max_volt { get; set; }
        public double cl_batt_min_temp { get; set; }
        public double cl_batt_max_temp { get; set; }
        public int cl_batt_min_volt_cell { get; set; }
        public int cl_batt_max_volt_cell { get; set; }
        public int cl_batt_min_temp_sensor { get; set; }
        public int cl_batt_max_temp_sensor { get; set; }
        public double cl_max_chg_power { get; set; }
        public double cl_max_dischg_power { get; set; }
        public double cl_single_chg_capacity { get; set; }
        public double cl_dischg_cap { get; set; }
        public double cl_accum_charge { get; set; }
        public double cl_accum_dischg { get; set; }
        public double cl_remaining_cap { get; set; }
        public double cl_batt_avg_volt { get; set; }
        public double cl_pos_ins_res { get; set; }
        public double cl_neg_ins_res { get; set; }
        public double bms_pack_volt { get; set; }

        public List<BMU> bmus { get; set; }
    }
}
