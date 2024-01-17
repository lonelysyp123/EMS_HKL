using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EMS.Storage.DB.Models
{
    public class ElectricMeterInfoModel
    {
        [Key]
        public int id { get; set; }
        public double Voltage_A { get; set; }
        public double Voltage_B { get; set; }
        public double Voltage_C { get; set; }
        public double Current_A { get; set; }
        public double Current_B { get; set; }
        public double Current_C { get; set; }
        public double ActivePowerA { get; set; }
        public double ActivePowerB { get; set; }
        public double ActivePowerC { get; set; }
        public double ActivePower_Total { get; set; }
        public double ReactivePowerA { get; set; }
        public double ReactivePowerB { get; set; }
        public double ReactivePowerC { get; set; }
        public double ReactivePower_Total { get; set; }
        public DateTime HappenTime { get; set; }

    }
}
