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

        /// <summary>
        ///  得到用户预设的日内储能充放电曲线
        /// </summary>
        /// <returns>每个时间节点的充放电出力</returns>
        public static List<BatteryStrategyModel> GetDailyPattern()
        {
            
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

            EnergyManagementSystem.GlobalInstance.Controller.SetMode(automationMode, maxDemandpowermode, reversePowermode, dailyPatternMode);
        }

        public static List<bool> GetMode()
        {
            return EnergyManagementSystem.GlobalInstance.Controller.GetMode();
        }

        public static void SetMaxDemandThreshold(double maxdemandpower,double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.SetMaxDemandThreshold(maxdemandpower, descendrate);
        }

        public static void GetMaxDemandThreshhold(out double maxdemandpower, out double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.GetMaxDemandThreshhold(out maxdemandpower, out descendrate);
        }


        /// <summary>
        /// ReversePower的参数的get和set
        /// </summary>
        /// <param name="threshold"></param>
        /// <param name="lowestthreshhold"></param>
        /// <param name="desecndrate"></param>
        public static void SetReversePowerThreshold(double threshold,double lowestthreshhold,double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.SetReversePowerThreshold(threshold, lowestthreshhold, descendrate);
        }
        public static void GetReversePowerThreshold(out double threshold, out double lowestthreshold,out double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.GetReversePowerThreshold(out threshold, out lowestthreshold, out descendrate);
        }
    }

}
