using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.Model
{
    public class PCSParSettingModel : ViewModelBase
    {
        /// <summary>
        /// BMS通讯中断超时
        /// </summary>
        public ushort BMSCMInterruptionTimeOut { get; set; }

        /// <summary>
        /// 远程485通信中断超时
        /// </summary>
        public ushort Remote485CMInterruptonTimeOut { get; set; }

        /// <summary>
        /// 远程TCP通信中断超时
        /// </summary>
        public ushort RemoteTCPCMInterruptionTimeOut { get; set; }

        /// <summary>
        /// BUS侧上限电压
        /// </summary>
        public double BUSUpperLimitVolThresh { get; set; }

        /// <summary>
        /// BUS侧下限电压
        /// </summary>
        public double BUSLowerLimitVolThresh { get; set; }

        /// <summary>
        /// BUS侧高压设置
        /// </summary>
        public double BUSHVolSetting { get; set; }

        /// <summary>
        /// BUS侧低压设置
        /// </summary>
        public double BUSLVolSetting { get; set; }

        /// <summary>
        /// 模式设置
        /// </summary>
        public string ModeSet1 { get; set; }

        /// <summary>
        /// 电池下限电压
        /// </summary>
        public double BTLLimitVol { get; set; }

        /// <summary>
        /// 放电终止电压
        /// </summary>
        public double DischargeSTVol { get; set; }

        /// <summary>
        /// 多支路电流调节参数
        /// </summary>
        public int MultiBranchCurRegPar { get; set; }

        /// <summary>
        /// 电池均充电压
        /// </summary>
        public double BatAveChVol { get; set; }

        /// <summary>
        /// 充电截止电流
        /// </summary>
        public double ChCutCurrent { get; set; }

        /// <summary>
        /// 最大充电电流
        /// </summary>
        public double MaxChCurrent { get; set; }

        /// <summary>
        /// 最大放电电流
        /// </summary>
        public double MaxDisChCurrent { get; set; }

        /// <summary>
        /// 直流电流设置
        /// </summary>
        public double DCCurrentSet { get; set; }

        /// <summary>
        /// 直流功率设置
        /// </summary>
        public double DCPowerSet { get; set; }

        /// <summary>
        /// 功率调节可见度
        /// </summary>
        public Visibility VisDCPower { get; set; }

        /// <summary>
        /// 电流调节可见度
        /// </summary>
        public Visibility VisDCCur { get; set; }

        /// <summary>
        /// 充放电按钮可见度
        /// </summary>
        public Visibility VisDCChar { get; set; }
    }
}
