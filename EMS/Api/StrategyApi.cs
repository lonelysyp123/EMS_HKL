using EMS.Common.StrategyManage;
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
        /// 获取当前最高故障状态
        /// </summary>
        public static ContingencyStatusEnum GetFaultState()
        {
            return EnergyManagementSystem.GlobalInstance.Controller.ContingencyStatus;
        }

        /// <summary>
        /// 设定最大SOC，当输入值为1.0时对应SOC为100%
        /// </summary>
        public static void SetMaxSoc(double maxSoc) { EnergyManagementSystem.GlobalInstance.Controller.SetMaxSoc(maxSoc); }

        /// <summary>
        /// 得到最大SOC，当返回值为1.0时对应SOC为100%
        /// </summary>
        public static double GetMaxSoc() { return EnergyManagementSystem.GlobalInstance.Controller.MaxSoc; }

        /// <summary>
        /// 设定最小SOC，当输入值为1.0时对应SOC为100%
        /// </summary>
        public static void SetMinSoc(double minSoc) { EnergyManagementSystem.GlobalInstance.Controller.SetMinSoc(minSoc); }

        /// <summary>
        /// 得到最小SOC，当返回值为1.0时对应SOC为100%
        /// </summary>
        public static double GetMinSoc() { return EnergyManagementSystem.GlobalInstance.Controller.MinSoc; }

        /// <summary>
        /// 设定当前最大允许充电功率kW
        /// </summary>
        public static void SetMaxChargingPower(double maxChargingPower) { EnergyManagementSystem.GlobalInstance.Controller.SetMaxChargingPower(maxChargingPower); }

        /// <summary>
        ///得到当前最大允许充电功率kW
        /// </summary>
        public static double GetMaxChargingPower() { return EnergyManagementSystem.GlobalInstance.Controller.MaxChargingPower; }

        /// <summary>
        /// 设定当前最大允许放电功率kW
        /// </summary>
        public static void SetMaxDischargingPower(double maxDischargingPower) { EnergyManagementSystem.GlobalInstance.Controller.SetMaxChargingPower(maxDischargingPower); }

        /// <summary>
        ///得到当前最大允许放电功率kW
        /// </summary>
        public static double GetMaxDischargingPower() { return EnergyManagementSystem.GlobalInstance.Controller.MaxDischargingPower; }
        /// <summary>
        ///得到储能充电效率%
        /// </summary>
        public static double GetChargingEfficiency() { return EnergyManagementSystem.GlobalInstance.ChargingEfficiency; }
        /// <summary>
        ///得到储能放电效率%
        /// </summary>
        public static double GetDischargingEfficiency() { return EnergyManagementSystem.GlobalInstance.DischargingEfficiency; }
        /// <summary>
        ///得到储能初始能量kWh
        /// </summary>
        public static double GetInitialEnergy() { return EnergyManagementSystem.GlobalInstance.InitialEnergy; }
        /// <summary>
        ///得到储能可用容量kWh
        /// </summary>
        public static double GetEnergyCapacity() { return EnergyManagementSystem.GlobalInstance.EnergyCapacity; }

        public static void SetChargingEfficiency(double efficiency) { EnergyManagementSystem.GlobalInstance.SetChargingEfficiency(efficiency); }
        public static void SetDischargingEfficiency(double efficiency) { EnergyManagementSystem.GlobalInstance.SetDischargingEfficiency(efficiency); }
        public static void SetInitialEnergy(double energy) { EnergyManagementSystem.GlobalInstance.SetInitialEnergy(energy); }
        public static void SetEnergyCapacity(double capacity) { EnergyManagementSystem.GlobalInstance.SetEnergyCapacity(capacity); }

        /// <summary>
        /// 日出力曲线的set
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
            return EnergyManagementSystem.GlobalInstance.Controller.DailyPattern;
        }

        public static bool IsAutomaticMode { get { return EnergyManagementSystem.GlobalInstance.Controller.IsAutomaticMode; } }
        public static bool HasDailyPatternEnabled { get { return EnergyManagementSystem.GlobalInstance.Controller.HasDailyPatternEnabled; } }
        public static bool HasMaxDemandControlEnabled { get { return EnergyManagementSystem.GlobalInstance.Controller.HasMaxDemandControlEnabled; } }
        public static bool HasReversePowerflowProtectionEnabled { get { return EnergyManagementSystem.GlobalInstance.Controller.HasReversePowerflowProtectionEnabled; } }

        /// <summary>
        /// 策略模式的get与set
        /// </summary>
        /// <param name="automationMode"></param>
        /// <param name="maxDemandpowermode"></param>
        /// <param name="reversePowermode"></param>
        /// <param name="dailyPatternMode"></param>
        public static void SetMode(bool automationMode, bool maxDemandpowermode, bool reversePowermode, bool dailyPatternMode)
        {
            // TODO {温经毅} 咱们把这个API拆成四个分别配置参数吧，和它相关的上层调用和下层传达都拆成四个。
            EnergyManagementSystem.GlobalInstance.Controller.SetMode(automationMode, maxDemandpowermode, reversePowermode, dailyPatternMode);
        }

        public static void SetMaxDemandThreshold(double maxdemandpower, double descendrate)
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
        public static void SetReversePowerThreshold(double threshold, double lowestthreshhold, double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.SetReversePowerThreshold(threshold, lowestthreshhold, descendrate);
        }
        public static void GetReversePowerThreshold(out double threshold, out double lowestthreshold, out double descendrate)
        {
            EnergyManagementSystem.GlobalInstance.Controller.GetReversePowerThreshold(out threshold, out lowestthreshold, out descendrate);
        }

        public static BessCommand GetManualCommand() { return EnergyManagementSystem.GlobalInstance.Controller.ManualCommand; }
        
        public static void SetManualCommand(BessCommand command) { EnergyManagementSystem.GlobalInstance.Controller.SetManualCommand(command); }
    }

}
