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
        /// <summary>
        /// DC模块故障信息
        /// </summary>
        private ObservableCollection<string> _faultInfoDC;

        public ObservableCollection<string> FaultInfoDC
        {
            get => _faultInfoDC;
            set
            {
                SetProperty(ref _faultInfoDC, value);
            }
        }

        /// <summary>
        /// DC模块故障颜色
        /// </summary>
        private SolidColorBrush _faultColorDC;

        public SolidColorBrush FaultColorDC
        {
            get => _faultColorDC;
            set
            {
                SetProperty(ref _faultColorDC, value);
            }
        }

        /// <summary>
        /// DC模块告警信息
        /// </summary>
        private ObservableCollection<string> _alarmInfoDC;

        public ObservableCollection<string> AlarmInfoDC
        {
            get => _alarmInfoDC;
            set
            {
                SetProperty(ref _alarmInfoDC, value);
            }
        }

        /// <summary>
        /// DC模块告警颜色
        /// </summary>
        private SolidColorBrush _alarmColorDC;

        public SolidColorBrush AlarmColorDC
        {
            get => _alarmColorDC;
            set
            {
                SetProperty(ref _alarmColorDC, value);
            }
        }





        /// <summary>
        /// PDS故障信息
        /// </summary>
        private ObservableCollection<string> _faultInfoPDS;

        public ObservableCollection<string> FaultInfoPDS
        {
            get => _faultInfoPDS;
            set
            {
                SetProperty(ref _faultInfoPDS, value);
            }
        }

        /// <summary>
        /// PDS故障颜色
        /// </summary>
        private SolidColorBrush _faultColorPDS;

        public SolidColorBrush FaultColorPDS
        {
            get => _faultColorPDS;
            set
            {
                SetProperty(ref _faultColorPDS, value);
            }
        }


        /// <summary>
        /// PDS告警信息
        /// </summary>
        private ObservableCollection<string> _alarmInfoPDS;

        public ObservableCollection<string> AlarmInfoPDS
        {
            get => _alarmInfoPDS;
            set
            {
                SetProperty(ref _alarmInfoPDS, value);
            }
        }

        /// <summary>
        /// PDS告警颜色
        /// </summary>
        private SolidColorBrush _alarmColorPDS;

        public SolidColorBrush AlarmColorPDS
        {
            get => _alarmColorPDS;
            set
            {
                SetProperty(ref _alarmColorPDS, value);
            }
        }




        /// <summary>
        /// PCS本地手动控制状态颜色
        /// </summary>
        private SolidColorBrush _pCSStateColorManControl;

        public SolidColorBrush PCSStateColorManControl
        {
            get => _pCSStateColorManControl;
            set
            {
                SetProperty(ref _pCSStateColorManControl, value);
            }
        }

        /// <summary>
        /// PCS本地自动控制状态颜色
        /// </summary>
        private SolidColorBrush _pCSStateColorAutoControl;

        public SolidColorBrush PCSStateColorAutoControl
        {
            get => _pCSStateColorAutoControl;
            set
            {
                SetProperty(ref _pCSStateColorAutoControl, value);
            }
        }

        /// <summary>
        /// PCS远程控制状态颜色
        /// </summary>
        private SolidColorBrush _pCSStateColorRemoteControl;

        public SolidColorBrush PCSStateColorRemoteControl
        {
            get => _pCSStateColorRemoteControl;
            set
            {
                SetProperty(ref _pCSStateColorRemoteControl, value);
            }
        }


        /// <summary>
        /// PCS告警状态颜色
        /// </summary>
        private SolidColorBrush _alarmStateColorPCS;

        public SolidColorBrush AlarmStateColorPCS
        {
            get => _alarmStateColorPCS;
            set
            {
                SetProperty(ref _alarmStateColorPCS, value);
            }
        }

        /// <summary>
        /// PCS故障状态颜色
        /// </summary>
        private SolidColorBrush _faultStateColorPCS;

        public SolidColorBrush FaultStateColorPCS
        {
            get => _faultStateColorPCS;
            set
            {
                SetProperty(ref _faultStateColorPCS, value);
            }
        }

        /// <summary>
        /// PCS上电初始化状态颜色
        /// </summary>
        private SolidColorBrush _powerOnInitStateColorPCS;

        public SolidColorBrush PowerOnInitStateColorPCS
        {
            get => _powerOnInitStateColorPCS;
            set
            {
                SetProperty(ref _powerOnInitStateColorPCS, value);
            }
        }


        /// <summary>
        /// 模块温度  精度0.1 偏移值-20
        /// </summary>
        private double _moduleTemperature;

        public double ModuleTemperature
        {
            get => _moduleTemperature;
            set
            {
                SetProperty(ref _moduleTemperature, value);
            }
        }

        /// <summary>
        /// 环境温度  精度0.1 偏移值-20
        /// </summary>
        private double _ambientTemperature;

        public double AmbientTemperature
        {
            get => _ambientTemperature;
            set
            {
                SetProperty(ref _ambientTemperature, value);
            }
        }

        /// <summary>
        /// DC侧支路1状态1
        /// </summary>
        private string _dcBranch1State1;

        public string DcBranch1State1
        {
            get => _dcBranch1State1;
            set
            {
                SetProperty(ref _dcBranch1State1, value);
            }
        }

        /// <summary>
        /// DC侧支路1状态2  启停状态
        /// </summary>
        private string _dcBranch1State2;

        public string DcBranch1State2
        {
            get => _dcBranch1State2;
            set
            {
                SetProperty(ref _dcBranch1State2, value);
            }
        }


        /// <summary>
        /// DC侧支路1：直流功率
        /// </summary>
        private double _dcBranch1DCPower;

        public double DcBranch1DCPower
        {
            get => _dcBranch1DCPower;
            set
            {
                SetProperty(ref _dcBranch1DCPower, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流电压
        /// </summary>
        private double _dcBranch1DCVol;

        public double DcBranch1DCVol
        {
            get => _dcBranch1DCVol;
            set
            {
                SetProperty(ref _dcBranch1DCVol, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流电流
        /// </summary>
        private double _dcBranch1DCCur;

        public double DcBranch1DCCur
        {
            get => _dcBranch1DCCur;
            set
            {
                SetProperty(ref _dcBranch1DCCur, value);
            }
        }

        /// <summary>
        /// DC侧支路1：BUS侧电压
        /// </summary>
        private double _dcBranch1BUSVol;

        public double DcBranch1BUSVol
        {
            get => _dcBranch1BUSVol;
            set
            {
                SetProperty(ref _dcBranch1BUSVol, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计充电电量
        /// </summary>
        private uint _dcBranch1Char;

        public uint DcBranch1Char
        {
            get => _dcBranch1Char;
            set
            {
                SetProperty(ref _dcBranch1Char, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计放电电量
        /// </summary>
        private uint _dcBranch1DisChar;

        public uint DcBranch1DisChar
        {
            get => _dcBranch1DisChar;
            set
            {
                SetProperty(ref _dcBranch1DisChar, value);
            }
        }

        /// <summary>
        /// 模组一状态
        /// </summary>
        private string _module1Status1;

        public string Module1Status1
        {
            get => _module1Status1;
            set
            {
                SetProperty(ref _module1Status1, value);
            }
        }

        /// <summary>
        /// 模组二状态
        /// </summary>
        private string _module1Status2;

        public string Module1Status2
        {
            get => _module1Status2;
            set
            {
                SetProperty(ref _module1Status2, value);
            }
        }

        /// <summary>
        /// 模组三状态
        /// </summary>
        private string _module1Status3;

        public string Module1Status3
        {
            get => _module1Status3;
            set
            {
                SetProperty(ref _module1Status3, value);
            }
        }

        /// <summary>
        /// 模组四状态
        /// </summary>
        private string _module1Status4;

        public string Module1Status4
        {
            get => _module1Status4;
            set
            {
                SetProperty(ref _module1Status4, value);
            }
        }

        /// <summary>
        /// 模组五状态
        /// </summary>
        private string _module1Status5;

        public string Module1Status5
        {
            get => _module1Status5;
            set
            {
                SetProperty(ref _module1Status5, value);
            }
        }

        /// <summary>
        /// 模组六状态
        /// </summary>
        private string _module1Status6;

        public string Module1Status6
        {
            get => _module1Status6;
            set
            {
                SetProperty(ref _module1Status6, value);
            }
        }

        /// <summary>
        /// 模组七状态
        /// </summary>
        private string _module1Status7;

        public string Module1Status7
        {
            get => _module1Status7;
            set
            {
                SetProperty(ref _module1Status7, value);
            }
        }

        /// <summary>
        /// 模组八状态
        /// </summary>
        private string _module1Status8;

        public string Module1Status8
        {
            get => _module1Status8;
            set
            {
                SetProperty(ref _module1Status8, value);
            }
        }

        /// <summary>
        /// 模组九状态
        /// </summary>
        private string _module1Status9;

        public string Module1Status9
        {
            get => _module1Status9;
            set
            {
                SetProperty(ref _module1Status9, value);
            }
        }

        /// <summary>
        /// 模组十状态
        /// </summary>
        private string _module1Status10;

        public string Module1Status10
        {
            get => _module1Status10;
            set
            {
                SetProperty(ref _module1Status10, value);
            }
        }

        /// <summary>
		/// 模组一状态颜色（蓝色=在线，绿色=运行，黄色=告警，红色=故障）
		/// </summary>
		private SolidColorBrush _module1StatusColor1;

        public SolidColorBrush Module1StatusColor1
        {
            get => _module1StatusColor1;
            set
            {
                SetProperty(ref _module1StatusColor1, value);
            }
        }

        /// <summary>
        /// 模组二状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor2;

        public SolidColorBrush Module1StatusColor2
        {
            get => _module1StatusColor2;
            set
            {
                SetProperty(ref _module1StatusColor2, value);
            }
        }

        /// <summary>
        /// 模组三状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor3;

        public SolidColorBrush Module1StatusColor3
        {
            get => _module1StatusColor3;
            set
            {
                SetProperty(ref _module1StatusColor3, value);
            }
        }

        /// <summary>
        /// 模组四状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor4;

        public SolidColorBrush Module1StatusColor4
        {
            get => _module1StatusColor4;
            set
            {
                SetProperty(ref _module1StatusColor4, value);
            }
        }

        /// <summary>
        /// 模组五状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor5;

        public SolidColorBrush Module1StatusColor5
        {
            get => _module1StatusColor5;
            set
            {
                SetProperty(ref _module1StatusColor5, value);
            }
        }

        /// <summary>
        /// 模组六状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor6;

        public SolidColorBrush Module1StatusColor6
        {
            get => _module1StatusColor6;
            set
            {
                SetProperty(ref _module1StatusColor6, value);
            }
        }

        /// <summary>
        /// 模组七状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor7;

        public SolidColorBrush Module1StatusColor7
        {
            get => _module1StatusColor7;
            set
            {
                SetProperty(ref _module1StatusColor7, value);
            }
        }

        /// <summary>
        /// 模组八状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor8;

        public SolidColorBrush Module1StatusColor8
        {
            get => _module1StatusColor8;
            set
            {
                SetProperty(ref _module1StatusColor8, value);
            }
        }

        /// <summary>
        /// 模组九状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor9;

        public SolidColorBrush Module1StatusColor9
        {
            get => _module1StatusColor9;
            set
            {
                SetProperty(ref _module1StatusColor9, value);
            }
        }

        /// <summary>
        /// 模组十状态颜色
        /// </summary>
        private SolidColorBrush _module1StatusColor10;

        public SolidColorBrush Module1StatusColor10
        {
            get => _module1StatusColor10;
            set
            {
                SetProperty(ref _module1StatusColor10, value);
            }
        }

        /// <summary>
        /// DC故障可见度
        /// </summary>
        private Visibility _visDCFault;

        public Visibility VisDCFault
        {
            get => _visDCFault;
            set
            {
                SetProperty(ref _visDCFault, value);
            }
        }

        /// <summary>
        /// PDS故障可见度
        /// </summary>
        private Visibility _visPDSFault;

        public Visibility VisPDSFault
        {
            get => _visPDSFault;
            set
            {
                SetProperty(ref _visPDSFault, value);
            }
        }

        /// <summary>
        /// DC告警可见度
        /// </summary>
        private Visibility _visDCAlarm;

        public Visibility VisDCAlarm
        {
            get => _visDCAlarm;
            set
            {
                SetProperty(ref _visDCAlarm, value);
            }
        }

        /// <summary>
        /// PDS告警可见度
        /// </summary>
        private Visibility _visPDSAlarm;

        public Visibility VisPDSAlarm
        {
            get => _visPDSAlarm;
            set
            {
                SetProperty(ref _visPDSAlarm, value);
            }
        }

        /// <summary>
        /// DC模块异常1 DC模块异常有三个地址
        /// </summary>
        private int _alarmStateFlagDC1;

        public int AlarmStateFlagDC1
        {
            get => _alarmStateFlagDC1;
            set
            {
                SetProperty(ref _alarmStateFlagDC1, value);
            }
        }


        /// <summary>
        /// DC模块异常2
        /// </summary>
        private int _alarmStateFlagDC2;

        public int AlarmStateFlagDC2
        {
            get => _alarmStateFlagDC2;
            set
            {
                SetProperty(ref _alarmStateFlagDC2, value);
            }
        }


        /// <summary>
        /// DC模块异常3
        /// </summary>
        private int _alarmStateFlagDC3;

        public int AlarmStateFlagDC3
        {
            get => _alarmStateFlagDC3;
            set
            {
                SetProperty(ref _alarmStateFlagDC3, value);
            }
        }


        /// <summary>
        /// PDS异常信息
        /// </summary>
        private int _alarmStateFlagPDS;

        public int AlarmStateFlagPDS
        {
            get => _alarmStateFlagPDS;
            set
            {
                SetProperty(ref _alarmStateFlagPDS, value);
            }
        }


        /// <summary>
        /// PCS控制状态读取
        /// </summary>
        private int _controlStateFlagPCS;

        public int ControlStateFlagPCS
        {
            get => _controlStateFlagPCS;
            set
            {
                SetProperty(ref _controlStateFlagPCS, value);
            }
        }


        /// <summary>
        /// PCS状态读取
        /// </summary>
        private int _stateFlagPCS;

        public int StateFlagPCS
        {
            get => _stateFlagPCS;
            set
            {
                SetProperty(ref _stateFlagPCS, value);
            }
        }


        /// <summary>
        /// DC侧支路1状态读取1 
        /// </summary>
        private int _dcBranch1StateFlag1;

        public int DcBranch1StateFlag1
        {
            get => _dcBranch1StateFlag1;
            set
            {
                SetProperty(ref _dcBranch1StateFlag1, value);
            }
        }


        /// <summary>
        /// DC侧支路1状态读取2  启停状态
        /// </summary>
        private int _dcBranch1StateFlag2;

        public int DcBranch1StateFlag2
        {
            get => _dcBranch1StateFlag2;
            set
            {
                SetProperty(ref _dcBranch1StateFlag2, value);
            }
        }


        /// <summary>
        /// DC侧支路1：直流累计充电电量高两字节
        /// </summary>
        private ushort _dcBranch1CharHigh;

        public ushort DcBranch1CharHigh
        {
            get => _dcBranch1CharHigh;
            set
            {
                SetProperty(ref _dcBranch1CharHigh, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计充电电量低两字节
        /// </summary>
        private ushort _dcBranch1CharLow;

        public ushort DcBranch1CharLow
        {
            get => _dcBranch1CharLow;
            set
            {
                SetProperty(ref _dcBranch1CharLow, value);
            }
        }



        /// <summary>
        /// DC侧支路1：直流累计放电电量高两字节
        /// </summary>
        private ushort _dcBranch1DisCharHigh;

        public ushort DcBranch1DisCharHigh
        {
            get => _dcBranch1DisCharHigh;
            set
            {
                SetProperty(ref _dcBranch1DisCharHigh, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计放电电量低两字节
        /// </summary>
        private ushort _dcBranch1DisCharLow;

        public ushort DcBranch1DisCharLow
        {
            get => _dcBranch1DisCharLow;
            set
            {
                SetProperty(ref _dcBranch1DisCharLow, value);
            }
        }

        /// <summary>
        /// 监控软件代码
        /// </summary>
        private ushort _monitorSoftCode;

        public ushort MonitorSoftCode
        {
            get => _monitorSoftCode;
            set
            {
                SetProperty(ref _monitorSoftCode, value);
            }
        }

        /// <summary>
        /// DC软件代码
        /// </summary>
        private ushort _dcSoftCode;

        public ushort DcSoftCode
        {
            get => _dcSoftCode;
            set
            {
                SetProperty(ref _dcSoftCode, value);
            }
        }

        /// <summary>
        /// U2软件代码
        /// </summary>
        private ushort _u2SoftCode;

        public ushort U2SoftCode
        {
            get => _u2SoftCode;
            set
            {
                SetProperty(ref _u2SoftCode, value);
            }
        }

        /// <summary>
        /// 机柜序列号
        /// </summary>
        private string _cabSerialNumber;

        public string CabSerialNumber
        {
            get => _cabSerialNumber;
            set
            {
                SetProperty(ref _cabSerialNumber, value);
            }
        }


        /// <summary>
        /// 机柜序列号地址
        /// </summary>
        private ushort[] _snAdress; 

        public ushort[] SNAdress
        {
            get => _snAdress;
            set
            {
                SetProperty(ref _snAdress, value);
            }
        }
    }
}
