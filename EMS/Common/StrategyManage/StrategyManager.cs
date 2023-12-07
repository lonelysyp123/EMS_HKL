using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class StrategyManager
    {
        private static object syncRoot = new Object();

        private static StrategyManager instance;
        public static StrategyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StrategyManager();
                        }
                    }
                }
                return instance;
            }
        }

        private StrategyManager()
        {

        }

        /// <summary>
        ///  得到用户预设的日内储能充放电曲线
        /// </summary>
        /// <returns>每个时间节点的充放电出力</returns>
        //public List<BatteryStrategyModel> GetDailyPattern()
        //{
        //    return null;
        //    // 输出需要排序，用户输入时需要检查设定值上下限
        //    //return EnergyManagementSystem.GlobalInstance.Controller.DailyPattern;
        //}

        //public void SetDailyPattern(List<BatteryStrategyModel> dailyPattern) {
        //    EnergyManagementSystem.GlobalInstance.Controller.DailyPattern = dailyPattern;
        //}

        /// <summary>
        ///  得到手动模式下用户设定的控制指令
        /// </summary>
        /// <returns>手动模式下用户设定的控制指令</returns>
        public BessCommand GetManualCommand() { return new BessCommand(); }

        /// <summary>
        ///  对PCS发送控制指令，需要包含一定的验证，检查下发指令是否合理，否则报错，该API不能是阻塞函数，需要立刻返回。如遇到异常，需要抛出异常。
        ///  如果下发指令和当前PCS正在执行的指令一直，可以避免重复下发。
        /// </summary>
        /// <returns>指令下发是否成功</returns>
        public bool SendPcsCommand(BessCommand command)
        {
            return true;
        }

        /// <summary>
        ///  得到智能电表的注入有功功率，需要是三相总功率，找丁冠文讨论转换事宜
        /// </summary>
        /// <returns>当前AC交流侧电表的三相总功率</returns>
        public double GetACSmartMeterPower() { return 0; }


        public double GetReversePowerflowProtectionThreshold() { return 0; } // 逆功率保护用户设置的阈值

        /// <summary>
        ///  对负载放电为正，对电池充电为负
        /// </summary>
        /// <returns>当前PCS的直流总功率</returns>
        public double GetPcsPower() { return 0; }

        public double GetAutomaticControlTolerance() { return 0.1; } //自动模式下的公差

        public double GetChargingDiscount() { return 0.6; } //充电模式中的降功率因数

        public double GetDischargingDiscount() { return 0.6; } //放电模式中的降功率因数

        public int GetSystemSamplePeriod() { return 500; } //系统采样频率
        public List<string> GetBMSAlarmandFaultInfo() { return null ; }
        public List<string>GetPCSFaultInfo() { return null ; }
        public bool SetPCSHalt() { return true; }
        public List<string>GetSystemErrors() { return null ; }
        public double GetDemandControlCapacity() { return 4000; } //总变压器容量
    }
}
