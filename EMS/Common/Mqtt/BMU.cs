using EMS.Model;
using EMS.MyControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Mqtt
{
    public class BMU
    {
        public int id { get; set; }
        public int alarm_state { get; set; }
        public int faulty_state { get; set; }

        public List<BatteryCell> battery_cells { get; set; }
        public BMU()
        {
            this.battery_cells = new List<BatteryCell>();
            this.id = 1;
            this.alarm_state = 1;
            this.faulty_state = 1;

            BatteryCell batteryCell = new BatteryCell();
            battery_cells.Add(batteryCell);
        }

        public BMU(BatterySeriesModel batterySeriesModel) {
            this.battery_cells = new List<BatteryCell>();
            if (batterySeriesModel.BMUID == null)
            {
                this.id = 0;
            }
            else {
                this.id = int.Parse(batterySeriesModel.BMUID);
            }
            
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
