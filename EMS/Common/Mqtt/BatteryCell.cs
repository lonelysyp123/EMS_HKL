using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Mqtt
{
    public class BatteryCell
    {
        public int id { get; set; }
        public double cl_batt_volt { get; set; }
        public double cl_batt_temp { get; set; }
        public double temperature2 { get; set; }
        public double cl_batt_soc { get; set; }
        public int resistance { get; set; }
        public int cl_batt_soh { get; set; }
        public double capacity { get; set; }
        public BatteryCell() {
            this.id = 1;
            this.cl_batt_volt = 1;
            this.cl_batt_temp = 1;
            this.temperature2 = 1;
            this.cl_batt_soc = 1;
            this.resistance = 1;
            this.cl_batt_soh = 1;
            this.capacity = 1;
        }
        public BatteryCell(BatteryModel batteryModel) {
            //this.id = batteryModel.BatteryNumber;
            //this.cl_batt_volt = batteryModel.Voltage;
            //this.cl_batt_temp = batteryModel.Temperature1;
            //this.temperature2 = batteryModel.Temperature2;
            //this.cl_batt_soc = batteryModel.SOC;
            //this.resistance = batteryModel.Resistance;
            //this.cl_batt_soh = batteryModel.SOH;
            //this.capacity = batteryModel.Capacity;
            
        }
    }
}
