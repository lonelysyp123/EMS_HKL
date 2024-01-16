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
        public int Voltage_A { get; set; }
        public int Voltage_B { get; set; }
        public int Voltage_C { get; set; }
        public int Current_A {  get; set; }
        public int Current_B {  get; set; }
        public int Current_C {  get; set; }
        public int ActivePower_A {  get; set; }
        public int ActivePower_B {  get; set; }
        public int ActivePower_C {  get; set; }
        public int ActivePower_Total {  get; set; }
        public int ReactivePower_A { get; set; }
        public int ReactivePower_B { get; set; }
        public int ReactivePower_C { get; set; }
        public int ReactivePower_Total { get; set; }
        public string SmartMeterNumber {  get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
