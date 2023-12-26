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
        public string ChargeChannelStateNumber { get; set; }
        public ushort ChargeChannelState { get; set; }
        public double ChargeCapacitySum { get; set; }
        public double MinVoltage { get; set; }
        public int MinVoltageIndex { get; set; }
        public double MaxVoltage { get; set; }
        public int MaxVoltageIndex { get; set; }
        public double MinTemperature { get; set; }
        public int MinTemperatureIndex { get; set; }
        public double MaxTemperature { get; set; }
        public int MaxTemperatureIndex { get; set; }

        public List<BatteryCell> battery_cells { get; set; }
    }
}
