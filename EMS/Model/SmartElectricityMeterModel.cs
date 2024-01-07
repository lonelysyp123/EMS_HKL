using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class SmartElectricityMeterModel
    {
        public double DcVoltage { get; set; }
        public double DcCurrent { get; set; }
        public double PowerValue { get; set; }
        public double PositivePrimarySideElectricalEnergy { get; set; }
        public double ReversePrimarySideElectricalEnergy { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Power { get; set; }
        public double TotalPositiveActivePowerEnergy { get; set; }
    }
}
