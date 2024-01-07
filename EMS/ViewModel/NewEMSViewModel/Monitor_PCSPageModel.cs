using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_PCSPageModel:ViewModelBase
    {
        #region ObservableObject
        
        private string _dc_RunningState;
        /// <summary>
        /// DC侧支路1状态
        /// </summary>
        public string DC_RunningState
        {
            get => _dc_RunningState;
            set
            {
                SetProperty(ref _dc_RunningState, value);
            }
        }

        
        private SolidColorBrush _dc_StartOrStopState;
        /// <summary>
        /// DC侧支路1启停状态
        /// </summary>
        public SolidColorBrush DC_StartOrStopState
        {
            get => _dc_StartOrStopState;
            set
            {
                SetProperty(ref _dc_StartOrStopState, value);
            }
        }

        
        private double _dc_Power;
        /// <summary>
        /// DC侧支路1直流功率
        /// </summary>
        public double DC_Power
        {
            get => _dc_Power;
            set
            {
                SetProperty(ref _dc_Power, value);
            }
        }

        
        private double _dc_Voltage;
        /// <summary>
        /// DC侧支路1直流电压
        /// </summary>
        public double DC_Voltage
        {
            get => _dc_Voltage;
            set
            {
                SetProperty(ref _dc_Voltage, value);
            }
        }

        
        private double _dc_Current;
        /// <summary>
        /// DC侧支路1直流电流
        /// </summary>
        public double DC_Current
        {
            get => _dc_Current;
            set
            {
                SetProperty(ref _dc_Current, value);
            }
        }

        
        private uint _dc_CumulativeChargePower;
        /// <summary>
        /// DC侧支路1累计充电电量
        /// </summary>
        public uint DC_CumulativeChargePower
        {
            get => _dc_CumulativeChargePower;
            set
            {
                SetProperty(ref _dc_CumulativeChargePower, value);
            }
        }

        
        private uint _dc_CumulativeDischargePower;
        /// <summary>
        /// DC侧支路1累计放电电量
        /// </summary>
        public uint DC_CumulativeDischargePower
        {
            get => _dc_CumulativeDischargePower;
            set
            {
                SetProperty(ref _dc_CumulativeDischargePower, value);
            }
        }

        
        private double _dc_BUSVoltage;
        /// <summary>
        /// DC侧支路1BUS侧电压
        /// </summary>
        public double DC_BUSVoltage
        {
            get => _dc_BUSVoltage;
            set
            {
                SetProperty(ref _dc_BUSVoltage, value);
            }
        }

        
        private double _moduleTemp;
        /// <summary>
        /// 模块温度
        /// </summary>
        public double ModuleTemp
        {
            get => _moduleTemp;
            set
            {
                SetProperty(ref _moduleTemp, value);
            }
        }

        
        private double _environmentTemp;
        /// <summary>
        /// 环境温度
        /// </summary>
        public double EnvironmentTemp
        {
            get => _environmentTemp;
            set
            {
                SetProperty(ref _environmentTemp, value);
            }
        }

        
        private SolidColorBrush _isManualControl;
        /// <summary>
        /// PCS控制状态-本地手动控制状态
        /// </summary>
        public SolidColorBrush IsManualControl
        {
            get => _isManualControl;
            set
            {
                SetProperty(ref _isManualControl, value);
            }
        }

        
        private SolidColorBrush _isAutomation;
        /// <summary>
        /// PCS控制状态-本地自动控制状态
        /// </summary>
        public SolidColorBrush IsAutomation
        {
            get => _isAutomation;
            set
            {
                SetProperty(ref _isAutomation, value);
            }
        }

        
        private SolidColorBrush _isRemoteControl;
        /// <summary>
        /// PCS控制状态-远程控制状态
        /// </summary>
        public SolidColorBrush IsRemoteControl
        {
            get => _isRemoteControl;
            set
            {
                SetProperty(ref _isRemoteControl, value);
            }
        }

        
        private SolidColorBrush _isAlarmStatus;
        /// <summary>
        /// PCS状态-告警状态
        /// </summary>
        public SolidColorBrush IsAlarmStatus
        {
            get => _isAlarmStatus;
            set
            {
                SetProperty(ref _isAlarmStatus, value);
            }
        }

        
        private SolidColorBrush _isFaultStatus;
        /// <summary>
        /// PCS状态-故障状态
        /// </summary>
        public SolidColorBrush IsFaultStatus
        {
            get => _isFaultStatus;
            set
            {
                SetProperty(ref _isFaultStatus, value);
            }
        }

        
        private SolidColorBrush _isInitStatus;
        /// <summary>
        /// PCS状态-上电初始化状态
        /// </summary>
        public SolidColorBrush IsInitStatus
        {
            get => _isInitStatus;
            set
            {
                SetProperty(ref _isInitStatus, value);
            }
        }

        private string _module1Status1;
        /// <summary>
        /// 模组一状态
        /// </summary>
        public string Module1Status1
        {
            get => _module1Status1;
            set
            {
                SetProperty(ref _module1Status1, value);
            }
        }


        private string _module1Status2;
        /// <summary>
        /// 模组二状态
        /// </summary>
        public string Module1Status2
        {
            get => _module1Status2;
            set
            {
                SetProperty(ref _module1Status2, value);
            }
        }


        private string _module1Status3;
        /// <summary>
        /// 模组三状态
        /// </summary>
        public string Module1Status3
        {
            get => _module1Status3;
            set
            {
                SetProperty(ref _module1Status3, value);
            }
        }


        private string _module1Status4;
        /// <summary>
        /// 模组四状态
        /// </summary>
        public string Module1Status4
        {
            get => _module1Status4;
            set
            {
                SetProperty(ref _module1Status4, value);
            }
        }


        private string _module1Status5;
        /// <summary>
        /// 模组五状态
        /// </summary>
        public string Module1Status5
        {
            get => _module1Status5;
            set
            {
                SetProperty(ref _module1Status5, value);
            }
        }


        private string _module1Status6;
        /// <summary>
        /// 模组六状态
        /// </summary>
        public string Module1Status6
        {
            get => _module1Status6;
            set
            {
                SetProperty(ref _module1Status6, value);
            }
        }


        private string _module1Status7;
        /// <summary>
        /// 模组七状态
        /// </summary>
        public string Module1Status7
        {
            get => _module1Status7;
            set
            {
                SetProperty(ref _module1Status7, value);
            }
        }


        private string _module1Status8;
        /// <summary>
        /// 模组八状态
        /// </summary>
        public string Module1Status8
        {
            get => _module1Status8;
            set
            {
                SetProperty(ref _module1Status8, value);
            }
        }


        private string _module1Status9;
        /// <summary>
        /// 模组九状态
        /// </summary>
        public string Module1Status9
        {
            get => _module1Status9;
            set
            {
                SetProperty(ref _module1Status9, value);
            }
        }


        private string _module1Status10;
        /// <summary>
        /// 模组十状态
        /// </summary>
        public string Module1Status10
        {
            get => _module1Status10;
            set
            {
                SetProperty(ref _module1Status10, value);
            }
        }


        private SolidColorBrush _module1StatusColor1;
        /// <summary>
		/// 模组一状态颜色（蓝色=在线，绿色=运行，黄色=告警，红色=故障）
		/// </summary>
        public SolidColorBrush Module1StatusColor1
        {
            get => _module1StatusColor1;
            set
            {
                SetProperty(ref _module1StatusColor1, value);
            }
        }


        private SolidColorBrush _module1StatusColor2;
        /// <summary>
        /// 模组二状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor2
        {
            get => _module1StatusColor2;
            set
            {
                SetProperty(ref _module1StatusColor2, value);
            }
        }


        private SolidColorBrush _module1StatusColor3;
        /// <summary>
        /// 模组三状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor3
        {
            get => _module1StatusColor3;
            set
            {
                SetProperty(ref _module1StatusColor3, value);
            }
        }


        private SolidColorBrush _module1StatusColor4;
        /// <summary>
        /// 模组四状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor4
        {
            get => _module1StatusColor4;
            set
            {
                SetProperty(ref _module1StatusColor4, value);
            }
        }


        private SolidColorBrush _module1StatusColor5;
        /// <summary>
        /// 模组五状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor5
        {
            get => _module1StatusColor5;
            set
            {
                SetProperty(ref _module1StatusColor5, value);
            }
        }


        private SolidColorBrush _module1StatusColor6;
        /// <summary>
        /// 模组六状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor6
        {
            get => _module1StatusColor6;
            set
            {
                SetProperty(ref _module1StatusColor6, value);
            }
        }


        private SolidColorBrush _module1StatusColor7;
        /// <summary>
        /// 模组七状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor7
        {
            get => _module1StatusColor7;
            set
            {
                SetProperty(ref _module1StatusColor7, value);
            }
        }


        private SolidColorBrush _module1StatusColor8;
        /// <summary>
        /// 模组八状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor8
        {
            get => _module1StatusColor8;
            set
            {
                SetProperty(ref _module1StatusColor8, value);
            }
        }


        private SolidColorBrush _module1StatusColor9;
        /// <summary>
        /// 模组九状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor9
        {
            get => _module1StatusColor9;
            set
            {
                SetProperty(ref _module1StatusColor9, value);
            }
        }


        private SolidColorBrush _module1StatusColor10;
        /// <summary>
        /// 模组十状态颜色
        /// </summary>
        public SolidColorBrush Module1StatusColor10
        {
            get => _module1StatusColor10;
            set
            {
                SetProperty(ref _module1StatusColor10, value);
            }
        }

        
        private bool _dcWarn_1;
        /// <summary>
        /// 环境温度过高
        /// </summary>
        public bool DCWarn_1
        {
            get => _dcWarn_1;
            set
            {
                SetProperty(ref _dcWarn_1, value);
            }
        }

        private bool _dcWarn_2;
        /// <summary>
        /// U2通信异常
        /// </summary>
        public bool DCWarn_2
        {
            get => _dcWarn_2;
            set
            {
                SetProperty(ref _dcWarn_2, value);
            }
        }

        private bool _dcWarn_3;
        /// <summary>
        /// 柜温探头故障
        /// </summary>
        public bool DCWarn_3
        {
            get => _dcWarn_3;
            set
            {
                SetProperty(ref _dcWarn_3, value);
            }
        }

        private bool _dcWarn_4;
        /// <summary>
        /// 环温探头故障
        /// </summary>
        public bool DCWarn_4
        {
            get => _dcWarn_4;
            set
            {
                SetProperty(ref _dcWarn_4, value);
            }
        }

        private bool _dcWarn_5;
        /// <summary>
        /// 校准参数异常
        /// </summary>
        public bool DCWarn_5
        {
            get => _dcWarn_5;
            set
            {
                SetProperty(ref _dcWarn_5, value);
            }
        }

        private bool _dcFault_1;
        /// <summary>
        /// 直流高压侧过压
        /// </summary>
        public bool DCFault_1
        {
            get => _dcFault_1;
            set
            {
                SetProperty(ref _dcFault_1, value);
            }
        }

        private bool _dcFault_2;
        /// <summary>
        /// 直流高压侧欠压
        /// </summary>
        public bool DCFault_2
        {
            get => _dcFault_2;
            set
            {
                SetProperty(ref _dcFault_2, value);
            }
        }

        private bool _dcFault_3;
        /// <summary>
        /// 直流低压侧过压
        /// </summary>
        public bool DCFault_3
        {
            get => _dcFault_3;
            set
            {
                SetProperty(ref _dcFault_3, value);
            }
        }

        private bool _dcFault_4;
        /// <summary>
        /// 直流低压侧欠压
        /// </summary>
        public bool DCFault_4
        {
            get => _dcFault_4;
            set
            {
                SetProperty(ref _dcFault_4, value);
            }
        }

        private bool _dcFault_5;
        /// <summary>
        /// 直流低压侧过流
        /// </summary>
        public bool DCFault_5
        {
            get => _dcFault_5;
            set
            {
                SetProperty(ref _dcFault_5, value);
            }
        }

        private bool _dcFault_6;
        /// <summary>
        /// 重启过多
        /// </summary>
        public bool DCFault_6
        {
            get => _dcFault_6;
            set
            {
                SetProperty(ref _dcFault_6, value);
            }
        }

        private bool _dcFault_7;
        /// <summary>
        /// 直流低压侧继电器短路
        /// </summary>
        public bool DCFault_7
        {
            get => _dcFault_7;
            set
            {
                SetProperty(ref _dcFault_7, value);
            }
        }

        private bool _dcFault_8;
        /// <summary>
        /// 绝缘检测异常
        /// </summary>
        public bool DCFault_8
        {
            get => _dcFault_8;
            set
            {
                SetProperty(ref _dcFault_8, value);
            }
        }

        private bool _dcFault_9;
        /// <summary>
        /// 电池电量不足
        /// </summary>
        public bool DCFault_9
        {
            get => _dcFault_9;
            set
            {
                SetProperty(ref _dcFault_9, value);
            }
        }

        private bool _dcFault_10;
        /// <summary>
        /// BMS故障或直流高压侧开关断开
        /// </summary>
        public bool DCFault_10
        {
            get => _dcFault_10;
            set
            {
                SetProperty(ref _dcFault_10, value);
            }
        }

        private bool _dcFault_11;
        /// <summary>
        /// 机柜温度过高
        /// </summary>
        public bool DCFault_11
        {
            get => _dcFault_11;
            set
            {
                SetProperty(ref _dcFault_11, value);
            }
        }

        private bool _dcFault_12;
        /// <summary>
        /// 模块电流不平衡
        /// </summary>
        public bool DCFault_12
        {
            get => _dcFault_12;
            set
            {
                SetProperty(ref _dcFault_12, value);
            }
        }

        private bool _dcFault_13;
        /// <summary>
        /// 直流低压侧开关断开
        /// </summary>
        public bool DCFault_13
        {
            get => _dcFault_13;
            set
            {
                SetProperty(ref _dcFault_13, value);
            }
        }

        private bool _dcFault_14;
        /// <summary>
        /// 24V辅助电源故障
        /// </summary>
        public bool DCFault_14
        {
            get => _dcFault_14;
            set
            {
                SetProperty(ref _dcFault_14, value);
            }
        }

        private bool _dcFault_15;
        /// <summary>
        /// 紧急停机
        /// </summary>
        public bool DCFault_15
        {
            get => _dcFault_15;
            set
            {
                SetProperty(ref _dcFault_15, value);
            }
        }

        private bool _dcFault_16;
        /// <summary>
        /// 模块温度过温
        /// </summary>
        public bool DCFault_16
        {
            get => _dcFault_16;
            set
            {
                SetProperty(ref _dcFault_16, value);
            }
        }

        private bool _dcFault_17;
        /// <summary>
        /// 风扇故障
        /// </summary>
        public bool DCFault_17
        {
            get => _dcFault_17;
            set
            {
                SetProperty(ref _dcFault_17, value);
            }
        }

        private bool _dcFault_18;
        /// <summary>
        /// 直流低压侧继电器开路
        /// </summary>
        public bool DCFault_18
        {
            get => _dcFault_18;
            set
            {
                SetProperty(ref _dcFault_18, value);
            }
        }

        private bool _dcFault_19;
        /// <summary>
        /// 保险故障
        /// </summary>
        public bool DCFault_19
        {
            get => _dcFault_19;
            set
            {
                SetProperty(ref _dcFault_19, value);
            }
        }

        private bool _dcFault_20;
        /// <summary>
        /// DSP初始化故障
        /// </summary>
        public bool DCFault_20
        {
            get => _dcFault_20;
            set
            {
                SetProperty(ref _dcFault_20, value);
            }
        }

        private bool _dcFault_21;
        /// <summary>
        /// 直流低压侧软启动失败
        /// </summary>
        public bool DCFault_21
        {
            get => _dcFault_21;
            set
            {
                SetProperty(ref _dcFault_21, value);
            }
        }

        private bool _dcFault_22;
        /// <summary>
        /// CANA通讯故障
        /// </summary>
        public bool DCFault_22
        {
            get => _dcFault_22;
            set
            {
                SetProperty(ref _dcFault_22, value);
            }
        }

        private bool _dcFault_23;
        /// <summary>
        /// 直流高压侧继电器开路
        /// </summary>
        public bool DCFault_23
        {
            get => _dcFault_23;
            set
            {
                SetProperty(ref _dcFault_23, value);
            }
        }

        private bool _dcFault_24;
        /// <summary>
        /// 直流高压侧软启动失败
        /// </summary>
        public bool DCFault_24
        {
            get => _dcFault_24;
            set
            {
                SetProperty(ref _dcFault_24, value);
            }
        }

        private bool _dcFault_25;
        /// <summary>
        /// DSP版本故障
        /// </summary>
        public bool DCFault_25
        {
            get => _dcFault_25;
            set
            {
                SetProperty(ref _dcFault_25, value);
            }
        }

        private bool _dcFault_26;
        /// <summary>
        /// CPLD版本故障
        /// </summary>
        public bool DCFault_26
        {
            get => _dcFault_26;
            set
            {
                SetProperty(ref _dcFault_26, value);
            }
        }

        private bool _dcFault_27;
        /// <summary>
        /// 参数不匹配
        /// </summary>
        public bool DCFault_27
        {
            get => _dcFault_27;
            set
            {
                SetProperty(ref _dcFault_27, value);
            }
        }

        private bool _dcFault_28;
        /// <summary>
        /// 硬件版本故障
        /// </summary>
        public bool DCFault_28
        {
            get => _dcFault_28;
            set
            {
                SetProperty(ref _dcFault_28, value);
            }
        }

        private bool _dcFault_29;
        /// <summary>
        /// 485通讯故障
        /// </summary>
        public bool DCFault_29
        {
            get => _dcFault_29;
            set
            {
                SetProperty(ref _dcFault_29, value);
            }
        }

        private bool _dcFault_30;
        /// <summary>
        /// CANB通讯故障
        /// </summary>
        public bool DCFault_30
        {
            get => _dcFault_30;
            set
            {
                SetProperty(ref _dcFault_30, value);
            }
        }

        private bool _dcFault_31;
        /// <summary>
        /// 模块重号故障
        /// </summary>
        public bool DCFault_31
        {
            get => _dcFault_31;
            set
            {
                SetProperty(ref _dcFault_31, value);
            }
        }

        private bool _dcFault_32;
        /// <summary>
        /// 15V辅助电源故障
        /// </summary>
        public bool DCFault_32
        {
            get => _dcFault_32;
            set
            {
                SetProperty(ref _dcFault_32, value);
            }
        }

        private bool _dcFault_33;
        /// <summary>
        /// 直流高压侧继电器短路
        /// </summary>
        public bool DCFault_33
        {
            get => _dcFault_33;
            set
            {
                SetProperty(ref _dcFault_33, value);
            }
        }

        private bool _dcFault_34;
        /// <summary>
        /// BMS电压异常
        /// </summary>
        public bool DCFault_34
        {
            get => _dcFault_34;
            set
            {
                SetProperty(ref _dcFault_34, value);
            }
        }

        private bool _dcFault_35;
        /// <summary>
        /// BMS电流异常
        /// </summary>
        public bool DCFault_35
        {
            get => _dcFault_35;
            set
            {
                SetProperty(ref _dcFault_35, value);
            }
        }

        private bool _dcFault_36;
        /// <summary>
        /// BMS温度异常
        /// </summary>
        public bool DCFault_36
        {
            get => _dcFault_36;
            set
            {
                SetProperty(ref _dcFault_36, value);
            }
        }

        private bool _dcFault_37;
        /// <summary>
        /// BMS关机故障
        /// </summary>
        public bool DCFault_37
        {
            get => _dcFault_37;
            set
            {
                SetProperty(ref _dcFault_37, value);
            }
        }

        private bool _pdsWarn;
        /// <summary>
        /// 防雷器告警
        /// </summary>
        public bool PDSWarn
        {
            get => _pdsWarn;
            set
            {
                SetProperty(ref _pdsWarn, value);
            }
        }

        private bool _pdsFault_1;
        /// <summary>
        /// 软件版本故障
        /// </summary>
        public bool PDSFault_1
        {
            get => _pdsFault_1;
            set
            {
                SetProperty(ref _pdsFault_1, value);
            }
        }

        private bool _pdsFault_2;
        /// <summary>
        /// DSP初始化故障
        /// </summary>
        public bool PDSFault_2
        {
            get => _pdsFault_2;
            set
            {
                SetProperty(ref _pdsFault_2, value);
            }
        }

        private bool _pdsFault_3;
        /// <summary>
        /// BMS故障
        /// </summary>
        public bool PDSFault_3
        {
            get => _pdsFault_3;
            set
            {
                SetProperty(ref _pdsFault_3, value);
            }
        }

        private bool _pdsFault_4;
        /// <summary>
        /// 紧急停机
        /// </summary>
        public bool PDSFault_4
        {
            get => _pdsFault_4;
            set
            {
                SetProperty(ref _pdsFault_4, value);
            }
        }




        #endregion

        public ObservableCollection<Item> Items { get; set; }

        //private PCSDataService pcsservice;
        public Monitor_PCSPageModel()
        {
            Items = new ObservableCollection<Item> { };
            //pcsservice = new PCSDataService();
            //pcsservice.RegisterDataState(PCSDataDistribution);

        }

        public void PCSDataDistribution(PCSModel model)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ModuleTemp = model.ModuleTemperature;
                EnvironmentTemp = model.AmbientTemperature;
                DC_Power = model.DcBranch1DCPower;
                DC_Voltage = model.DcBranch1DCVol;
                DC_Current = model.DcBranch1DCCur;
                DC_BUSVoltage = model.DcBranch1BUSVol;


                GetDCBranchINFO(model);
                EnergyCal(model);
                DataAcquisitionDcModuleStatus(model);
                GetActivePCSControlState(model);
                GetActivePCSState(model);
                GetDCFault(model);
                GetPDSFault(model);
                GetDCAlarm(model);
                GetPDSAlarm(model);
            });
        }

        public void PCSStateDistribution(bool isconnected, bool isdaqdata, bool issavedata)
        {

        }

        private void GetDCBranchINFO(PCSModel model)
        {
            int value1;
            int value2;

            value1 = model.DcBranch1StateFlag1;
            value2 = model.DcBranch1StateFlag2;
            if ((value1 & 0x0001) != 0)
            {
                DC_RunningState = "电池充满";
            }
            else if ((value1 & 0x0002) != 0)
            {
                DC_RunningState = "电池放空";
            }
            else if ((value1 & 0x0004) != 0)
            {
                DC_RunningState = "充电";
            }
            else if ((value1 & 0x0008) != 0)
            {
                DC_RunningState = "放电";
            }
            else if ((value1 & 0x0040) != 0)
            {
                DC_RunningState = "电池恒压均充";
            }


            if ((value2 & 0x0001) != 0)
            {
                DC_StartOrStopState = new SolidColorBrush(Colors.LightGreen);
            }
            else if ((value2 & 0x0001) == 0)
            {
                DC_StartOrStopState = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void EnergyCal(PCSModel model)
        {
            uint value1;
            uint value2;
            uint value3;
            uint value4;
            value1 = model.DcBranch1CharHigh;
            value2 = model.DcBranch1CharLow;
            value3 = model.DcBranch1DisCharHigh;
            value4 = model.DcBranch1DisCharLow;
            DC_CumulativeChargePower = value1 << 16 | value2;
            DC_CumulativeDischargePower = value3 << 16 | value4;
        }

        public void DataAcquisitionDcModuleStatus(PCSModel model)
        {

            int onlinevalue;
            int runvalue;
            int alarmvalue;
            int faultvalue;
            onlinevalue = model.ModuleOnLineFlag;
            runvalue = model.ModuleRunFlag;
            alarmvalue = model.ModuleAlarmFlag;
            faultvalue = model.ModuleFaultFlag;

            //DC模组1状态
            if ((onlinevalue & 0x0001) != 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                Module1Status1 = "在线";
                Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0001) != 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0 && (onlinevalue & 0x0001) == 0)
            {
                Module1Status1 = "运行";
                Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                Module1Status1 = "告警";
                Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0)
            {
                Module1Status1 = "故障";
                Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                Module1Status1 = "离线";
                Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组2状态
            if ((onlinevalue & 0x0002) != 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                Module1Status2 = "在线";
                Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0002) != 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0 && (onlinevalue & 0x0002) == 0)
            {
                Module1Status2 = "运行";
                Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                Module1Status2 = "告警";
                Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0)
            {
                Module1Status2 = "故障";
                Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                Module1Status2 = "离线";
                Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组3状态
            if ((onlinevalue & 0x0004) != 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                Module1Status3 = "在线";
                Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0004) != 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0 && (onlinevalue & 0x0004) == 0)
            {
                Module1Status3 = "运行";
                Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                Module1Status3 = "告警";
                Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0)
            {
                Module1Status3 = "故障";
                Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                Module1Status3 = "离线";
                Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组4状态
            if ((onlinevalue & 0x0008) != 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                Module1Status4 = "在线";
                Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0008) != 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0 && (onlinevalue & 0x0008) == 0)
            {
                Module1Status4 = "运行";
                Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                Module1Status4 = "告警";
                Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0)
            {
                Module1Status4 = "故障";
                Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                Module1Status4 = "离线";
                Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组5状态
            if ((onlinevalue & 0x0010) != 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                Module1Status5 = "在线";
                Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0010) != 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0 && (onlinevalue & 0x0010) == 0)
            {
                Module1Status5 = "运行";
                Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                Module1Status5 = "告警";
                Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0)
            {
                Module1Status5 = "故障";
                Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                Module1Status5 = "离线";
                Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组6状态
            if ((onlinevalue & 0x0020) != 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                Module1Status6 = "在线";
                Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0020) != 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0 && (onlinevalue & 0x0020) == 0)
            {
                Module1Status6 = "运行";
                Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                Module1Status6 = "告警";
                Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0)
            {
                Module1Status6 = "故障";
                Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                Module1Status6 = "离线";
                Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组7状态
            if ((onlinevalue & 0x0040) != 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                Module1Status7 = "在线";
                Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0040) != 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0 && (onlinevalue & 0x0040) == 0)
            {
                Module1Status7 = "运行";
                Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                Module1Status7 = "告警";
                Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0)
            {
                Module1Status7 = "故障";
                Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0040) == 0)
            {
                Module1Status7 = "离线";
                Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组8状态
            if ((onlinevalue & 0x0080) != 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                Module1Status8 = "在线";
                Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0080) != 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0 && (onlinevalue & 0x0080) == 0)
            {
                Module1Status8 = "运行";
                Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                Module1Status8 = "告警";
                Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0)
            {
                Module1Status8 = "故障";
                Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                Module1Status8 = "离线";
                Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组9状态
            if ((onlinevalue & 0x0100) != 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                Module1Status9 = "在线";
                Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0100) != 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0 && (onlinevalue & 0x0100) == 0)
            {
                Module1Status9 = "运行";
                Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                Module1Status9 = "告警";
                Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0)
            {
                Module1Status9 = "故障";
                Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                Module1Status9 = "离线";
                Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            // DC模组10状态
            if ((onlinevalue & 0x0200) != 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                Module1Status10 = "在线";
                Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0200) != 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0 && (onlinevalue & 0x0200) == 0)
            {
                Module1Status10 = "运行";
                Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                Module1Status10 = "告警";
                Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0)
            {
                Module1Status10 = "故障";
                Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                Module1Status10 = "离线";
                Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }
        }

        public void GetActivePCSControlState(PCSModel model)
        {
            int value;
            value = model.ControlStateFlagPCS;
            if ((value & 0x0100) != 0)
            {
                IsRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsAutomation = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsManualControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0200) != 0)
            {
                IsManualControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsAutomation = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                IsAutomation = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsManualControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                IsAutomation = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsManualControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetActivePCSState(PCSModel model)
        {
            int value;
            value = model.StateFlagPCS;
            if ((value & 0x0200) != 0)
            {
                IsFaultStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsInitStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsAlarmStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                IsInitStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsAlarmStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsFaultStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x1000) != 0)
            {
                IsAlarmStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsFaultStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsInitStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                IsAlarmStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsFaultStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                IsInitStatus = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }


        public void GetDCFault(PCSModel model)
        {
            int value1;
            int value2;
            int value3;
            

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = model.AlarmStateFlagDC1;
            value2 = model.AlarmStateFlagDC2;
            value3 = model.AlarmStateFlagDC3;
            if ((value1 & 0x0001) != 0) { INFO.Add("直流高压侧过压"); DCFault_1 = true; } else { DCFault_1 = false; } //53005 bit0
            if ((value1 & 0x0002) != 0) { INFO.Add("直流高压侧欠压"); DCFault_2 = true; } else { DCFault_2 = false; } //bit1`
            if ((value1 & 0x0004) != 0) { INFO.Add("直流低压侧过压"); DCFault_3 = true; } else { DCFault_3 = false; } //bit2
            if ((value1 & 0x0008) != 0) { INFO.Add("直流低压侧欠压"); DCFault_4 = true; } else { DCFault_4 = false; } //bit3
            if ((value1 & 0x0010) != 0) { INFO.Add("直流低压侧过流"); DCFault_5 = true; } else { DCFault_5 = false; } //bit4
            //if ((value1 & 0x0020) != 0) { INFO.Add("重启过多"); colorflag = true; } //bit5
            if ((value1 & 0x0040) != 0) { INFO.Add("重启过多"); DCFault_6 = true; } else { DCFault_6 = false; }//bit6
            if ((value1 & 0x0080) != 0) { INFO.Add("直流低压侧继电器短路"); DCFault_7 = true; } else { DCFault_7 = false; }//bit7
            //if ((value1 & 0x0100) != 0) { INFO.Add("光伏能量不足"); colorflag = true; } //bit8
            if ((value1 & 0x0200) != 0) { INFO.Add("电池电量不足"); DCFault_9 = true; } else { DCFault_9 = false; }//bit9
            if ((value1 & 0x0800) != 0) { INFO.Add("直流高压侧开关断开"); DCFault_10 = true; } else { DCFault_10 = false; }//bit11
            if ((value1 & 0x2000) != 0) { INFO.Add("机柜温度过高"); DCFault_11 = true; } else { DCFault_11 = false; }//bit13


            if ((value2 & 0x0001) != 0) { INFO.Add("模块电流不平衡"); DCFault_12 = true; } else { DCFault_12 = false; }//53007 bit0
            if ((value2 & 0x0002) != 0) { INFO.Add("直流低压侧开关断开"); DCFault_13 = true; } else { DCFault_13 = false; }//bit1
            if ((value2 & 0x0004) != 0) { INFO.Add("24V辅助电源故障"); DCFault_14 = true; } else { DCFault_14 = false; }//bit2
            if ((value2 & 0x0008) != 0) { INFO.Add("紧急停机"); DCFault_15 = true; } else { DCFault_15 = false; }//bit3
            //if ((value2 & 0x0010) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit4
            //if ((value2 & 0x0020) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit5
            if ((value2 & 0x0040) != 0) { INFO.Add("模块温度过温"); DCFault_16 = true; } else { DCFault_16 = false; }//bit6
            if ((value2 & 0x0080) != 0) { INFO.Add("风扇故障"); DCFault_17 = true; } else { DCFault_17 = false; }//bit7
            if ((value2 & 0x0100) != 0) { INFO.Add("直流低压侧继电器开路"); DCFault_18 = true; } else { DCFault_18 = false; }//bit8
            if ((value2 & 0x0400) != 0) { INFO.Add("保险故障"); DCFault_19 = true; } else { DCFault_19 = false; }//bit10
            if ((value2 & 0x0800) != 0) { INFO.Add("DSP初始化故障"); DCFault_20 = true; } else { DCFault_20 = false; }//bit11
            if ((value2 & 0x1000) != 0) { INFO.Add("直流低压侧软启动失败"); DCFault_21 = true; } else { DCFault_21 = false; }//bit12
            if ((value2 & 0x2000) != 0) { INFO.Add("CANA通讯故障"); DCFault_22 = true; } else { DCFault_22 = false; }//bit13
            if ((value2 & 0x4000) != 0) { INFO.Add("直流高压侧继电器开路"); DCFault_23 = true; } else { DCFault_23 = false; }//bit14
            if ((value2 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); DCFault_24 = true; } else { DCFault_24 = false; }//bit15

            if ((value3 & 0x0001) != 0) { INFO.Add("DSP版本故障"); DCFault_25 = true; } else { DCFault_25 = false; }//53008 bit0
            if ((value3 & 0x0002) != 0) { INFO.Add("CPLD版本故障"); DCFault_26 = true; } else { DCFault_26 = false; }//bit1
            if ((value3 & 0x0004) != 0) { INFO.Add("参数不匹配"); DCFault_27 = true; } else { DCFault_27 = false; }//bit2
            if ((value3 & 0x0008) != 0) { INFO.Add("硬件版本故障"); DCFault_28 = true; } else { DCFault_28 = false; }//bit3
            if ((value3 & 0x0010) != 0) { INFO.Add("485通讯故障"); DCFault_29 = true; } else { DCFault_29 = false; }//bit4
            if ((value3 & 0x0020) != 0) { INFO.Add("CANB通讯故障"); DCFault_30 = true; } else { DCFault_30 = false; }//bit5
            if ((value3 & 0x0040) != 0) { INFO.Add("模块重号故障"); DCFault_31 = true; } else { DCFault_31 = false; }//bit6
            //if ((value3 & 0x0080) != 0) { INFO.Add("风扇故障"); colorflag = true; } //bit7
            if ((value3 & 0x0100) != 0) { INFO.Add("15V辅助电源故障"); DCFault_32 = true; } else { DCFault_32 = false; }//bit8
            if ((value3 & 0x0200) != 0) { INFO.Add("直流高压侧继电器短路"); DCFault_33 = true; } else { DCFault_33 = false; }//bit9
            if ((value3 & 0x0400) != 0) { INFO.Add("BMS电压异常"); DCFault_34 = true; } else { DCFault_34 = false; }//bit10
            if ((value3 & 0x0800) != 0) { INFO.Add("BMS电流异常"); DCFault_35 = true; } else { DCFault_35 = false; }//bit11
            if ((value3 & 0x1000) != 0) { INFO.Add("BMS温度异常"); DCFault_36 = true; } else { DCFault_36 = false; }//bit12
            if ((value3 & 0x2000) != 0) { INFO.Add("BMS关机异常"); DCFault_37 = true; } else { DCFault_37 = false; }//bit13
            if ((value3 & 0x4000) != 0) { INFO.Add("绝缘检测异常"); DCFault_8 = true; } else { DCFault_8 = false; }//bit14
            //if ((value3 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); colorflag = true; } //bit15
        }

        public void GetPDSFault(PCSModel model)
        {
            int value;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = model.AlarmStateFlagPDS;
            if ((value & 0x0001) != 0) { INFO.Add("软件版本故障"); PDSFault_1 = true; } else { PDSFault_1 = false; }//53009 bit0
            if ((value & 0x0002) != 0) { INFO.Add("DSP初始化故障"); PDSFault_2 = true; } else { PDSFault_2 = false; }//bit1
            if ((value & 0x0004) != 0) { INFO.Add("BMS故障"); PDSFault_3 = true; } else { PDSFault_3 = false; }//bit2
            if ((value & 0x0008) != 0) { INFO.Add("紧急停机"); PDSFault_4 = true; } else { PDSFault_4 = false; }//bit3
        }

        public void GetDCAlarm(PCSModel model)
        {
            int value1;
            int value2;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = model.AlarmStateFlagDC1;
            value2 = model.AlarmStateFlagDC2;

            if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高");  DCWarn_1= true; } else { DCWarn_1 = false; }//bit10  AAAA
            if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); DCWarn_2 = true; } else { DCWarn_2 = false; }//bit12  AAAAA
            if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); DCWarn_3 = true; } else { DCWarn_3 = false; }//bit14  AAAAAA
            if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); DCWarn_4 = true; } else { DCWarn_4 = false; }//bit15  AAAAAA

            if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); DCWarn_5 = true; } else { DCWarn_5 = false; }//bit9   AAAAAA
        }

        public void GetPDSAlarm(PCSModel model)
        {
            int value;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = model.AlarmStateFlagPDS;

            if ((value & 0x0010) != 0) { INFO.Add("防雷器告警"); PDSWarn = true; } else { PDSWarn = false; }//bit4   AAAAAAAAA
        }

    }
    public class Item
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
