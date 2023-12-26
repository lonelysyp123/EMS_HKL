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
        public double voltage { get; set; }
        public double temperature1 { get; set; }
        public double temperature2 { get; set; }
        public double soc { get; set; }
        public int resistance { get; set; }
        public int soh { get; set; }
        public double capacity { get; set; }
    }
}
