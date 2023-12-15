using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.Model
{
    public class BatteryModel
    {
        public double Voltage {  get; set; }
        public double Temperature1 {  get; set; }
        public double Temperature2 {  get; set; }
        public double SOC {  get; set; }
        public int Resistance {  get; set; }
        public int SOH {  get; set; }
        public double Capacity {  get; set; }
        public int BatteryNumber {  get; set; }

        public BatteryModel() { }
    }
}
