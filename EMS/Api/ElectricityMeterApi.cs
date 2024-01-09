using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{
    public static class ElectricityMeterApi
    {
        /// <summary>
        /// 建议实现方案：
        /// 1、将采集的智能电表数据放到一个线程安全固定长度的队列中。可用本项目的ConcurrentQueueLength
        /// 2、该方法是从队列中取出一个采集的对象数据
        /// 获取采集的智能电表数据
        /// </summary>
        /// <returns></returns>
        public static SmartMeterModel GetNextElectricityMeterData()
        {
            return null;
        }

        public static ThreePhaseValue GetVoltage(int index = 0)
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetThreePhaseVoltage();
        }

        public static ThreePhaseValue GetElectric(int index = 0)
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetThreePhaseElectric();
        }

        public static ThreePhaseValue GetActivePower(int index = 0)
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetThreePhaseActivePower();
        }

        public static ThreePhaseValue GetReactivePower(int index = 0)
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetThreePhaseReactivePower();
        }

        /// <summary>
        ///  得到智能电表的注入有功功率，需要是三相总功率，找丁冠文讨论转换事宜
        /// </summary>
        /// <returns>当前AC交流侧电表的三相总功率</returns>
        public static double GetRealPowerTotal(int index = 0) 
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetRealPowerTotal();
        }

        public static double GetReactivePowerTotal(int index = 0)
        {
            return EnergyManagementSystem.GlobalInstance.SmartMeterManager.SmartMeters[index].GetReactivePowerTotal();
        }
    }
}
