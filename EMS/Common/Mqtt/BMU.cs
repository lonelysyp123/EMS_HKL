using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Mqtt
{
    public class BMU
    {
        public string id { get; set; }
        public int alarm_state { get; set; }
        public int faulty_state { get; set; }

        public List<BatteryCell> battery_cells { get; set; }

        public BMU(BatterySeriesModel batterySeriesModel) {
            this.battery_cells = new List<BatteryCell>();
            this.id = batterySeriesModel.BMUID;
            this.alarm_state = batterySeriesModel.AlarmStateFlagBMU;
            this.faulty_state = batterySeriesModel.FaultyStateFlagBMU;
            foreach (var battery in batterySeriesModel.Batteries)
            {
                BatteryCell batteryCell = new BatteryCell(battery);
                battery_cells.Add(batteryCell);
            }
            
        }

    }
}
