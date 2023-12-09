using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{
    public static class ElectricityMeterApi
    {
        public static int[] GetVoltage()
        {
            return new int[3];
        }

        public static int[] GetElectric()
        {
            return new int[3];
        }

        public static int[] GetActivePower()
        {
            return new int[3];
        }

        public static int[] GetReactivePower()
        {
            return new int[3];
        }

        public static int GetActivePowerTotal() 
        {
            return -1;
        }

        public static int GetReactivePowerTotal()
        {
            return -1;
        }
    }
}
