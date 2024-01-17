using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class SmartMeterModel
    {
        public double Voltage_A { get; set; }
        public double Voltage_B { get; set; }
        public double Voltage_C { get; set; }
        public double Current_A {  get; set; }
        public double Current_B {  get; set; }
        public double Current_C {  get; set; }
        public double ActivePower_A {  get; set; }
        public double ActivePower_B {  get; set; }
        public double ActivePower_C {  get; set; }
        public double ActivePower_Total {  get; set; }
        public double ReactivePower_A { get; set; }
        public double ReactivePower_B { get; set; }
        public double ReactivePower_C { get; set; }
        public double ReactivePower_Total { get; set; }
        public string SmartMeterNumber {  get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
