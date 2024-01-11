using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{
    public static class SmartElectricityMeterApi
    {
        public static SmartElectricityMeterModel GetNextSmartElectricityMeterData()
        {
            return null;
        }

        public static byte[] ReadTotalPrimaryEnergyInfo()
        {
            return EnergyManagementSystem.GlobalInstance.SemManager.SEMDataService.ReadTotalPrimaryEnergyInfo();
        }

        public static byte[] ReadDCBaseInfo()
        {
            return EnergyManagementSystem.GlobalInstance.SemManager.SEMDataService.ReadDCBaseInfo();
        }

        public static byte[] ReadActiveEnergyInfo()
        {
            return EnergyManagementSystem.GlobalInstance.SemManager.SEMDataService.ReadActiveEnergyInfo();
        }

        public static byte[] ReadReverseActiveEnergyInfo()
        {
            return EnergyManagementSystem.GlobalInstance.SemManager.SEMDataService.ReadReverseActiveEnergyInfo();
        }
    }
}
