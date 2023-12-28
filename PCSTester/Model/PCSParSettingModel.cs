using CommunityToolkit.Mvvm.Input;
using PCSTester.Common.Modbus.ModbusTCP;
using PCSTester.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PCSTester.Model
{
    public class PCSParSettingModel : ViewModelBase
    {
        /// <summary>
        /// BMS通讯中断超时
        /// </summary>
        private ushort _bMSCMInterruptionTimeOut;

        public ushort BMSCMInterruptionTimeOut
        {
            get => _bMSCMInterruptionTimeOut;
            set
            {
                SetProperty(ref _bMSCMInterruptionTimeOut, value);
            }
        }

        /// <summary>
        /// 远程485通信中断超时
        /// </summary>
        private ushort _remote485CMInterruptionTimeOut;

        public ushort Remote485CMInterruptonTimeOut
        {
            get => _remote485CMInterruptionTimeOut;
            set
            {
                SetProperty(ref _remote485CMInterruptionTimeOut, value);
            }
        }

        /// <summary>
        /// 远程TCP通信中断超时
        /// </summary>
        private ushort _remoteTCPCMInterruptionTimeOut;

        public ushort RemoteTCPCMInterruptionTimeOut
        {
            get => _remoteTCPCMInterruptionTimeOut;
            set
            {
                SetProperty(ref _remoteTCPCMInterruptionTimeOut, value);
            }
        }



        /// <summary>
        /// BUS侧上限电压
        /// </summary>
        private double _bUSHigherVolThresh;

        public double BUSUpperLimitVolThresh
        {
            get => _bUSHigherVolThresh;
            set
            {
                SetProperty(ref _bUSHigherVolThresh, value);
            }
        }

        /// <summary>
        /// BUS侧下限电压
        /// </summary>
        private double _bUSLowerLimitVolThresh;

        public double BUSLowerLimitVolThresh
        {
            get => _bUSLowerLimitVolThresh;
            set
            {
                SetProperty(ref _bUSLowerLimitVolThresh, value);
            }
        }

        /// <summary>
        /// BUS侧高压设置
        /// </summary>
        private double _bUSHVolSetting;

        public double BUSHVolSetting
        {
            get => _bUSHVolSetting;
            set
            {
                SetProperty(ref _bUSHVolSetting, value);
            }
        }

        /// <summary>
        /// BUS侧低压设置
        /// </summary>
        private double _bUSLVolSetiing;

        public double BUSLVolSetting
        {
            get => _bUSLVolSetiing;
            set
            {
                SetProperty(ref _bUSLVolSetiing, value);
            }
        }

        /// <summary>
        /// 模式设置
        /// </summary>
        public string ModeSet1 { get; set; }

        /// <summary>
        /// 电池下限电压
        /// </summary>
        private double _bTLLimitVol;

        public double BTLLimitVol
        {
            get => _bTLLimitVol;
            set
            {
                SetProperty(ref _bTLLimitVol, value);
            }
        }

        /// <summary>
        /// 放电终止电压
        /// </summary>
        private double _dischargeSTVol;

        public double DischargeSTVol
        {
            get => _dischargeSTVol;
            set
            {
                SetProperty(ref _dischargeSTVol, value);
            }
        }

        /// <summary>
        /// 多支路电流调节参数
        /// </summary>
        private int _multiBranchCurRegPar;

        public int MultiBranchCurRegPar
        {
            get => _multiBranchCurRegPar;
            set
            {
                SetProperty(ref _multiBranchCurRegPar, value);
            }
        }

        /// <summary>
        /// 电池均充电压
        /// </summary>
        private double _batAveChVol;

        public double BatAveChVol
        {
            get => _batAveChVol;
            set
            {
                SetProperty(ref _batAveChVol, value);
            }
        }

        /// <summary>
        /// 充电截止电流
        /// </summary>
        private double _chCutCurrent;

        public double ChCutCurrent
        {
            get => _chCutCurrent;
            set
            {
                SetProperty(ref _chCutCurrent, value);
            }
        }

        /// <summary>
        /// 最大充电电流
        /// </summary>
        private double _maxChCurrent;

        public double MaxChCurrent
        {
            get => _maxChCurrent;
            set
            {
                SetProperty(ref _maxChCurrent, value);
            }
        }

        /// <summary>
        /// 最大放电电流
        /// </summary>
        private double _maxDisChCurrent;

        public double MaxDisChCurrent
        {
            get => _maxDisChCurrent;
            set
            {
                SetProperty(ref _maxDisChCurrent, value);
            }
        }

        /// <summary>
        /// 直流电流设置
        /// </summary>
        private double _dCCurrentSet;

        public double DCCurrentSet
        {
            get => _dCCurrentSet;
            set
            {
                SetProperty(ref _dCCurrentSet, value);
            }
        }

        /// <summary>
        /// 直流功率设置
        /// </summary>
        private double _dCPowerSet;

        public double DCPowerSet
        {
            get => _dCPowerSet;
            set
            {
                SetProperty(ref _dCPowerSet, value);
            }
        }

        /// <summary>
        /// 功率调节可见度
        /// </summary>
        private Visibility _visDCPower;

        public Visibility VisDCPower
        {
            get => _visDCPower;
            set
            {
                SetProperty(ref _visDCPower, value);
            }
        }

        /// <summary>
        /// 电流调节可见度
        /// </summary>
        private Visibility _visDCCur;

        public Visibility VisDCCur
        {
            get => _visDCCur;
            set
            {
                SetProperty(ref _visDCCur, value);
            }
        }

        /// <summary>
        /// 充放电按钮可见度
        /// </summary>
        private Visibility _visDCChar;

        public Visibility VisDCChar
        {
            get => _visDCChar;
            set
            {
                SetProperty(ref _visDCChar, value);
            }
        }
    }
}
