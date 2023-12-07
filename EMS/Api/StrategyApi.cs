using EMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EMS.Api
{
    public class StrategyApi
    {

        
        /// <summary>
        /// 日出力曲线的get和set
        /// </summary>
        /// <param name="dailyPattern"></param>
        public static void SetDailyPattern(List<BatteryStrategyModel> dailyPattern)
        {
            EnergyManagementSystem.GlobalInstance.Controller.DailyPattern = dailyPattern;
        }
        public static List<BatteryStrategyModel> GetDailyPattern()
        {
            EnergyManagementSystem.GlobalInstance.Controller.DailyPattern = new List<BatteryStrategyModel>();
            if (EnergyManagementSystem.GlobalInstance.Controller.DailyPattern.Count !=0)
            {
                return EnergyManagementSystem.GlobalInstance.Controller.DailyPattern;
            }
            else
            {
               
                EnergyManagementSystem.GlobalInstance.Controller.DailyPattern = null;
                    return EnergyManagementSystem.GlobalInstance.Controller.DailyPattern;
            }
            // 输出需要排序，用户输入时需要检查设定值上下限   
        }

        /// <summary>
        /// 策略模式的get与set
        /// </summary>
        /// <param name="automationMode"></param>
        /// <param name="maxDemandpowermode"></param>
        /// <param name="reversePowermode"></param>
        /// <param name="dailyPatternMode"></param>
        public static void SetMode(bool automationMode,bool maxDemandpowermode,bool reversePowermode, bool dailyPatternMode)
        {
            
            EnergyManagementSystem.GlobalInstance.Controller._hasMaxDemandControlEnabled = maxDemandpowermode;
            EnergyManagementSystem.GlobalInstance.Controller._isAutomaticMode = automationMode;
            EnergyManagementSystem.GlobalInstance.Controller._hasReversePowerflowProtectionEnabled = reversePowermode;
            EnergyManagementSystem.GlobalInstance.Controller._hasDailyPatternEnabled = dailyPatternMode;
        }

        public static List<bool> GetMode()
        {
            List<bool> result = new List<bool>()
            {
                EnergyManagementSystem.GlobalInstance.Controller._isAutomaticMode,
                EnergyManagementSystem.GlobalInstance.Controller._hasDailyPatternEnabled,
                EnergyManagementSystem.GlobalInstance.Controller._hasMaxDemandControlEnabled,
                EnergyManagementSystem.GlobalInstance.Controller._hasReversePowerflowProtectionEnabled
            } ;
            return result ;
        }

        public static void SetMaxDemandThreshold(double maxdemandpower,double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller._maxDemandPower = maxdemandpower;
            EnergyManagementSystem.GlobalInstance.Controller._maxDemandPowerDescendRate = descendrate;
        }

        public static void GetMaxDemandThreshhold(out double maxdemandpower,out double descendrate)
        {
            descendrate = EnergyManagementSystem.GlobalInstance.Controller._maxDemandPowerDescendRate;
            maxdemandpower = EnergyManagementSystem.GlobalInstance.Controller._maxDemandPower;
        }
        /// <summary>
        /// ReversePower的参数的get和set
        /// </summary>
        /// <param name="threshold"></param>
        /// <param name="lowestthreshhold"></param>
        /// <param name="desecndrate"></param>
        public static void SetReversePowerThreshold(double threshold,double lowestthreshhold,double descendrate)
        {
            var instance = EnergyManagementSystem.GlobalInstance.Controller;
            instance._reversePowerThreshold = threshold;
            instance._reversePowerLowestThreshold = lowestthreshhold;
            instance._reversePowerDescendRate= descendrate;
        }
        public static void GetReversePowerThreshold(out double threshold, out double lowestthreshold,out double descendrate)
        {
            var instance = EnergyManagementSystem.GlobalInstance.Controller;
            threshold = instance._reversePowerThreshold;
            lowestthreshold = instance._reversePowerLowestThreshold;
            descendrate= instance._reversePowerDescendRate;
        }
    }

}
