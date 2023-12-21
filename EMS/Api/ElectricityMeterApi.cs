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

        public static double GetActivePowerTotal() 
        {
            return -1;
        }

        public static double GetReactivePowerTotal()
        {
            return -1;
        }
    }
}
