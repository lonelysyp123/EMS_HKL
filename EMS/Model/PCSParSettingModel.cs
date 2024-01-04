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
        
        private ushort _bMSCMInterruptionTimeOut;
        /// <summary>
        /// BMS通讯中断超时
        /// </summary>
        public ushort BMSCMInterruptionTimeOut
        {
            get => _bMSCMInterruptionTimeOut;
            set
            {
                SetProperty(ref _bMSCMInterruptionTimeOut, value);
            }
        }

        
        private ushort _remote485CMInterruptionTimeOut;
        /// <summary>
        /// 远程485通信中断超时
        /// </summary>
        public ushort Remote485CMInterruptonTimeOut
        {
            get => _remote485CMInterruptionTimeOut;
            set
            {
                SetProperty(ref _remote485CMInterruptionTimeOut, value);
            }
        }

        
        private ushort _remoteTCPCMInterruptionTimeOut;
        /// <summary>
        /// 远程TCP通信中断超时
        /// </summary>
        public ushort RemoteTCPCMInterruptionTimeOut
        {
            get => _remoteTCPCMInterruptionTimeOut;
            set
            {
                SetProperty(ref _remoteTCPCMInterruptionTimeOut, value);
            }
        }



        
        private double _bUSHigherVolThresh;
        /// <summary>
        /// BUS侧上限电压
        /// </summary>
        public double BUSUpperLimitVolThresh
        {
            get => _bUSHigherVolThresh;
            set
            {
                SetProperty(ref _bUSHigherVolThresh, value);
            }
        }

        
        private double _bUSLowerLimitVolThresh;
        /// <summary>
        /// BUS侧下限电压
        /// </summary>
        public double BUSLowerLimitVolThresh
        {
            get => _bUSLowerLimitVolThresh;
            set
            {
                SetProperty(ref _bUSLowerLimitVolThresh, value);
            }
        }

        
        private double _bUSHVolSetting;
        /// <summary>
        /// BUS侧高压设置
        /// </summary>
        public double BUSHVolSetting
        {
            get => _bUSHVolSetting;
            set
            {
                SetProperty(ref _bUSHVolSetting, value);
            }
        }

        
        private double _bUSLVolSetiing;
        /// <summary>
        /// BUS侧低压设置
        /// </summary>
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

        
        private double _bTLLimitVol;
        /// <summary>
        /// 电池下限电压
        /// </summary>
        public double BTLLimitVol
        {
            get => _bTLLimitVol;
            set
            {
                SetProperty(ref _bTLLimitVol, value);
            }
        }

        
        private double _dischargeSTVol;
        /// <summary>
        /// 放电终止电压
        /// </summary>
        public double DischargeSTVol
        {
            get => _dischargeSTVol;
            set
            {
                SetProperty(ref _dischargeSTVol, value);
            }
        }

        
        private int _multiBranchCurRegPar;
        /// <summary>
        /// 多支路电流调节参数
        /// </summary>
        public int MultiBranchCurRegPar
        {
            get => _multiBranchCurRegPar;
            set
            {
                SetProperty(ref _multiBranchCurRegPar, value);
            }
        }

        
        private double _batAveChVol;
        /// <summary>
        /// 电池均充电压
        /// </summary>
        public double BatAveChVol
        {
            get => _batAveChVol;
            set
            {
                SetProperty(ref _batAveChVol, value);
            }
        }

        
        private double _chCutCurrent;
        /// <summary>
        /// 充电截止电流
        /// </summary>
        public double ChCutCurrent
        {
            get => _chCutCurrent;
            set
            {
                SetProperty(ref _chCutCurrent, value);
            }
        }

        
        private double _maxChCurrent;
        /// <summary>
        /// 最大充电电流
        /// </summary>
        public double MaxChCurrent
        {
            get => _maxChCurrent;
            set
            {
                SetProperty(ref _maxChCurrent, value);
            }
        }

        
        private double _maxDisChCurrent;
        /// <summary>
        /// 最大放电电流
        /// </summary>
        public double MaxDisChCurrent
        {
            get => _maxDisChCurrent;
            set
            {
                SetProperty(ref _maxDisChCurrent, value);
            }
        }

        
        private double _dCCurrentSet;
        /// <summary>
        /// 直流电流设置
        /// </summary>
        public double DCCurrentSet
        {
            get => _dCCurrentSet;
            set
            {
                SetProperty(ref _dCCurrentSet, value);
            }
        }

        
        private double _dCPowerSet;
        /// <summary>
        /// 直流功率设置
        /// </summary>
        public double DCPowerSet
        {
            get => _dCPowerSet;
            set
            {
                SetProperty(ref _dCPowerSet, value);
            }
        }

        
        private Visibility _visDCPower;
        /// <summary>
        /// 功率调节可见度
        /// </summary>
        public Visibility VisDCPower
        {
            get => _visDCPower;
            set
            {
                SetProperty(ref _visDCPower, value);
            }
        }

        
        private Visibility _visDCCur;
        /// <summary>
        /// 电流调节可见度
        /// </summary>
        public Visibility VisDCCur
        {
            get => _visDCCur;
            set
            {
                SetProperty(ref _visDCCur, value);
            }
        }

        
        private Visibility _visDCChar;
        /// <summary>
        /// 充放电按钮可见度
        /// </summary>
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
