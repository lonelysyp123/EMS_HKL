using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{
    public static class ElectricityMeterApi
    {
        public static ThreePhaseValue GetVoltage()
        {
            return new ThreePhaseValue();
        }

        public static ThreePhaseValue GetElectric()
        {
            return new ThreePhaseValue();
        }

        public static ThreePhaseValue GetActivePower()
        {
            return new ThreePhaseValue();
        }

        public static ThreePhaseValue GetReactivePower()
        {
            return new ThreePhaseValue();
        }

        /// <summary>
        ///  得到智能电表的注入有功功率，需要是三相总功率，找丁冠文讨论转换事宜
        /// </summary>
        /// <returns>当前AC交流侧电表的三相总功率</returns>
        public static double GetRealPowerTotal() 
        {
            return -1;
        }

        public static double GetReactivePowerTotal()
        {
            return -1;
        }
    }
}
