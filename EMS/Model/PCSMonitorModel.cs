using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using EMS.ViewModel;

namespace EMS.Model
{
    public class PCSMonitorModel:ViewModelBase
    {
        #region DependencyProperty
        
        private ObservableCollection<string> _faultInfoDC;
        /// <summary>
        /// DC模块故障信息
        /// </summary>
        public ObservableCollection<string> FaultInfoDC
        {
            get => _faultInfoDC;
            set
            {
                SetProperty(ref _faultInfoDC, value);
            }
        }

        
        private SolidColorBrush _faultColorDC;
        /// <summary>
        /// DC模块故障颜色
        /// </summary>
        public SolidColorBrush FaultColorDC
        {
            get => _faultColorDC;
            set
            {
                SetProperty(ref _faultColorDC, value);
            }
        }

        
        private ObservableCollection<string> _alarmInfoDC;
        /// <summary>
        /// DC模块告警信息
        /// </summary>
        public ObservableCollection<string> AlarmInfoDC
        {
            get => _alarmInfoDC;
            set
            {
                SetProperty(ref _alarmInfoDC, value);
            }
        }

        
        private SolidColorBrush _alarmColorDC;
        /// <summary>
        /// DC模块告警颜色
        /// </summary>
        public SolidColorBrush AlarmColorDC
        {
            get => _alarmColorDC;
            set
            {
                SetProperty(ref _alarmColorDC, value);
            }
        }





        
        private ObservableCollection<string> _faultInfoPDS;
        /// <summary>
        /// PDS故障信息
        /// </summary>
        public ObservableCollection<string> FaultInfoPDS
        {
            get => _faultInfoPDS;
            set
            {
                SetProperty(ref _faultInfoPDS, value);
            }
        }

        
        private SolidColorBrush _faultColorPDS;
        /// <summary>
        /// PDS故障颜色
        /// </summary>
        public SolidColorBrush FaultColorPDS
        {
            get => _faultColorPDS;
            set
            {
                SetProperty(ref _faultColorPDS, value);
            }
        }


        
        private ObservableCollection<string> _alarmInfoPDS;
        /// <summary>
        /// PDS告警信息
        /// </summary>
        public ObservableCollection<string> AlarmInfoPDS
        {
            get => _alarmInfoPDS;
            set
            {
                SetProperty(ref _alarmInfoPDS, value);
            }
        }

        
        private SolidColorBrush _alarmColorPDS;
        /// <summary>
        /// PDS告警颜色
        /// </summary>
        public SolidColorBrush AlarmColorPDS
        {
            get => _alarmColorPDS;
            set
            {
                SetProperty(ref _alarmColorPDS, value);
            }
        }




        
        private SolidColorBrush _pCSStateColorManControl;
        /// <summary>
        /// PCS本地手动控制状态颜色
        /// </summary>
        public SolidColorBrush PCSStateColorManControl
        {
            get => _pCSStateColorManControl;
            set
            {
                SetProperty(ref _pCSStateColorManControl, value);
            }
        }

        
        private SolidColorBrush _pCSStateColorAutoControl;
        /// <summary>
        /// PCS本地自动控制状态颜色
        /// </summary>
        public SolidColorBrush PCSStateColorAutoControl
        {
            get => _pCSStateColorAutoControl;
            set
            {
                SetProperty(ref _pCSStateColorAutoControl, value);
            }
        }

        
        private SolidColorBrush _pCSStateColorRemoteControl;
        /// <summary>
        /// PCS远程控制状态颜色
        /// </summary>
        public SolidColorBrush PCSStateColorRemoteControl
        {
            get => _pCSStateColorRemoteControl;
            set
            {
                SetProperty(ref _pCSStateColorRemoteControl, value);
            }
        }


        
        private SolidColorBrush _alarmStateColorPCS;
        /// <summary>
        /// PCS告警状态颜色
        /// </summary>
        public SolidColorBrush AlarmStateColorPCS
        {
            get => _alarmStateColorPCS;
            set
            {
                SetProperty(ref _alarmStateColorPCS, value);
            }
        }

        
        private SolidColorBrush _faultStateColorPCS;
        /// <summary>
        /// PCS故障状态颜色
        /// </summary>
        public SolidColorBrush FaultStateColorPCS
        {
            get => _faultStateColorPCS;
            set
            {
                SetProperty(ref _faultStateColorPCS, value);
            }
        }

        
        private SolidColorBrush _powerOnInitStateColorPCS;
        /// <summary>
        /// PCS上电初始化状态颜色
        /// </summary>
        public SolidColorBrush PowerOnInitStateColorPCS
        {
            get => _powerOnInitStateColorPCS;
            set
            {
                SetProperty(ref _powerOnInitStateColorPCS, value);
            }
        }


        
        private double _moduleTemperature;
        /// <summary>
        /// 模块温度  精度0.1 偏移值-20
        /// </summary>
        public double ModuleTemperature
        {
            get => _moduleTemperature;
            set
            {
                SetProperty(ref _moduleTemperature, value);
            }
        }

        
        private double _ambientTemperature;
        /// <summary>
        /// 环境温度  精度0.1 偏移值-20
        /// </summary>
        public double AmbientTemperature
        {
            get => _ambientTemperature;
            set
            {
                SetProperty(ref _ambientTemperature, value);
            }
        }

        
        private string _dcBranch1State1;
        /// <summary>
        /// DC侧支路1状态1
        /// </summary>
        public string DcBranch1State1
        {
            get => _dcBranch1State1;
            set
            {
                SetProperty(ref _dcBranch1State1, value);
            }
        }

        
        private string _dcBranch1State2;
        /// <summary>
        /// DC侧支路1状态2  启停状态
        /// </summary>
        public string DcBranch1State2
        {
            get => _dcBranch1State2;
            set
            {
                SetProperty(ref _dcBranch1State2, value);
            }
        }


        
        private double _dcBranch1DCPower;
        /// <summary>
        /// DC侧支路1：直流功率
        /// </summary>
        public double DcBranch1DCPower
        {
            get => _dcBranch1DCPower;
            set
            {
                SetProperty(ref _dcBranch1DCPower, value);
            }
        }

        
        private double _dcBranch1DCVol;
        /// <summary>
        /// DC侧支路1：直流电压
        /// </summary>
        public double DcBranch1DCVol
        {
            get => _dcBranch1DCVol;
            set
            {
                SetProperty(ref _dcBranch1DCVol, value);
            }
        }

        
        private double _dcBranch1DCCur;
        /// <summary>
        /// DC侧支路1：直流电流
        /// </summary>
        public double DcBranch1DCCur
        {
            get => _dcBranch1DCCur;
            set
            {
                SetProperty(ref _dcBranch1DCCur, value);
            }
        }

        
        private double _dcBranch1BUSVol;
        /// <summary>
        /// DC侧支路1：BUS侧电压
        /// </summary>
        public double DcBranch1BUSVol
        {
            get => _dcBranch1BUSVol;
            set
            {
                SetProperty(ref _dcBranch1BUSVol, value);
            }
        }

        
        private uint _dcBranch1Char;
        /// <summary>
        /// DC侧支路1：直流累计充电电量
        /// </summary>
        public uint DcBranch1Char
        {
            get => _dcBranch1Char;
            set
            {
                SetProperty(ref _dcBranch1Char, value);
            }
        }

        
        private uint _dcBranch1DisChar;
        /// <summary>
        /// DC侧支路1：直流累计放电电量
        /// </summary>
        public uint DcBranch1DisChar
        {
            get => _dcBranch1DisChar;
            set
            {
                SetProperty(ref _dcBranch1DisChar, value);
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

        
        private Visibility _visDCFault;
        /// <summary>
        /// DC故障可见度
        /// </summary>
        public Visibility VisDCFault
        {
            get => _visDCFault;
            set
            {
                SetProperty(ref _visDCFault, value);
            }
        }

        
        private Visibility _visPDSFault;
        /// <summary>
        /// PDS故障可见度
        /// </summary>
        public Visibility VisPDSFault
        {
            get => _visPDSFault;
            set
            {
                SetProperty(ref _visPDSFault, value);
            }
        }

        
        private Visibility _visDCAlarm;
        /// <summary>
        /// DC告警可见度
        /// </summary>
        public Visibility VisDCAlarm
        {
            get => _visDCAlarm;
            set
            {
                SetProperty(ref _visDCAlarm, value);
            }
        }

        
        private Visibility _visPDSAlarm;
        /// <summary>
        /// PDS告警可见度
        /// </summary>
        public Visibility VisPDSAlarm
        {
            get => _visPDSAlarm;
            set
            {
                SetProperty(ref _visPDSAlarm, value);
            }
        }

        
        private int _alarmStateFlagDC1;
        /// <summary>
        /// DC模块异常1 DC模块异常有三个地址
        /// </summary>
        public int AlarmStateFlagDC1
        {
            get => _alarmStateFlagDC1;
            set
            {
                SetProperty(ref _alarmStateFlagDC1, value);
            }
        }


        
        private int _alarmStateFlagDC2;
        /// <summary>
        /// DC模块异常2
        /// </summary>
        public int AlarmStateFlagDC2
        {
            get => _alarmStateFlagDC2;
            set
            {
                SetProperty(ref _alarmStateFlagDC2, value);
            }
        }


        
        private int _alarmStateFlagDC3;
        /// <summary>
        /// DC模块异常3
        /// </summary>
        public int AlarmStateFlagDC3
        {
            get => _alarmStateFlagDC3;
            set
            {
                SetProperty(ref _alarmStateFlagDC3, value);
            }
        }


        
        private int _alarmStateFlagPDS;
        ///<summary>
        /// PDS异常信息
        /// </summary>
        public int AlarmStateFlagPDS
        {
            get => _alarmStateFlagPDS;
            set
            {
                SetProperty(ref _alarmStateFlagPDS, value);
            }
        }


        
        private int _controlStateFlagPCS;
        /// <summary>
        /// PCS控制状态读取
        /// </summary>
        public int ControlStateFlagPCS
        {
            get => _controlStateFlagPCS;
            set
            {
                SetProperty(ref _controlStateFlagPCS, value);
            }
        }


        
        private int _stateFlagPCS;
        /// <summary>
        /// PCS状态读取
        /// </summary>
        public int StateFlagPCS
        {
            get => _stateFlagPCS;
            set
            {
                SetProperty(ref _stateFlagPCS, value);
            }
        }


        
        private int _dcBranch1StateFlag1;
        /// <summary>
        /// DC侧支路1状态读取1 
        /// </summary>
        public int DcBranch1StateFlag1
        {
            get => _dcBranch1StateFlag1;
            set
            {
                SetProperty(ref _dcBranch1StateFlag1, value);
            }
        }


        
        private int _dcBranch1StateFlag2;
        /// <summary>
        /// DC侧支路1状态读取2  启停状态
        /// </summary>
        public int DcBranch1StateFlag2
        {
            get => _dcBranch1StateFlag2;
            set
            {
                SetProperty(ref _dcBranch1StateFlag2, value);
            }
        }


        
        private ushort _dcBranch1CharHigh;
        /// <summary>
        /// DC侧支路1：直流累计充电电量高两字节
        /// </summary>
        public ushort DcBranch1CharHigh
        {
            get => _dcBranch1CharHigh;
            set
            {
                SetProperty(ref _dcBranch1CharHigh, value);
            }
        }

        
        private ushort _dcBranch1CharLow;
        /// <summary>
        /// DC侧支路1：直流累计充电电量低两字节
        /// </summary>
        public ushort DcBranch1CharLow
        {
            get => _dcBranch1CharLow;
            set
            {
                SetProperty(ref _dcBranch1CharLow, value);
            }
        }



        
        private ushort _dcBranch1DisCharHigh;
        /// <summary>
        /// DC侧支路1：直流累计放电电量高两字节
        /// </summary>
        public ushort DcBranch1DisCharHigh
        {
            get => _dcBranch1DisCharHigh;
            set
            {
                SetProperty(ref _dcBranch1DisCharHigh, value);
            }
        }

        
        private ushort _dcBranch1DisCharLow;
        /// <summary>
        /// DC侧支路1：直流累计放电电量低两字节
        /// </summary>
        public ushort DcBranch1DisCharLow
        {
            get => _dcBranch1DisCharLow;
            set
            {
                SetProperty(ref _dcBranch1DisCharLow, value);
            }
        }

        
        private ushort _monitorSoftCode;
        /// <summary>
        /// 监控软件代码
        /// </summary>
        public ushort MonitorSoftCode
        {
            get => _monitorSoftCode;
            set
            {
                SetProperty(ref _monitorSoftCode, value);
            }
        }

        
        private ushort _dcSoftCode;
        /// <summary>
        /// DC软件代码
        /// </summary>
        public ushort DcSoftCode
        {
            get => _dcSoftCode;
            set
            {
                SetProperty(ref _dcSoftCode, value);
            }
        }

        
        private ushort _u2SoftCode;
        /// <summary>
        /// U2软件代码
        /// </summary>
        public ushort U2SoftCode
        {
            get => _u2SoftCode;
            set
            {
                SetProperty(ref _u2SoftCode, value);
            }
        }

        
        private string _cabSerialNumber;
        /// <summary>
        /// 机柜序列号
        /// </summary>
        public string CabSerialNumber
        {
            get => _cabSerialNumber;
            set
            {
                SetProperty(ref _cabSerialNumber, value);
            }
        }


        
        private ushort[] _snAdress;
        /// <summary>
        /// 机柜序列号地址
        /// </summary>
        public ushort[] SNAdress
        {
            get => _snAdress;
            set
            {
                SetProperty(ref _snAdress, value);
            }
        }

        private int _moduleOnLineFlag;
        /// <summary>
        /// 模块在线地址状态读取
        /// </summary>
        public int ModuleOnLineFlag
        {
            get => _moduleOnLineFlag;
            set
            {
                SetProperty(ref _moduleOnLineFlag, value);
            }
        }


        private int _moduleRunFlag;
        /// <summary>
        /// 模块运行地址状态读取
        /// </summary>
        public int ModuleRunFlag
        {
            get => _moduleRunFlag;
            set
            {
                SetProperty(ref _moduleRunFlag, value);
            }
        }


        private int _moduleAlarmFlag;
        /// <summary>
        /// 模块告警地址状态读取
        /// </summary>
        public int ModuleAlarmFlag
        {
            get => _moduleAlarmFlag;
            set
            {
                SetProperty(ref _moduleAlarmFlag, value);
            }
        }


        private int _moduleFaultFlag;
        /// <summary>
        /// 模块故障地址状态读取
        /// </summary>
        public int ModuleFaultFlag
        {
            get => _moduleFaultFlag;
            set
            {
                SetProperty(ref _moduleFaultFlag, value);
            }
        }
    }
}
#endregion
