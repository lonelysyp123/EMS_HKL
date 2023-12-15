using EMS.MyControl;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel
{
    public class BatterySeriesViewModel : ViewModelBase
    {
        #region DependencyProperty
        private string _bMUID;
        public string BMUID
        {
            get => _bMUID;
            set
            {
                SetProperty(ref _bMUID, value);
            }
        }

        private ObservableCollection<string> _alarmState;
        public ObservableCollection<string> AlarmStateBMU
        {

            get => _alarmState;
            set
            {
                SetProperty(ref _alarmState, value);
            }
        }

        private SolidColorBrush _alarmColorBMU;
        public SolidColorBrush AlarmColorBMU
        {
            get => _alarmColorBMU;
            set
            {
                SetProperty(ref _alarmColorBMU, value);
            }
        }

        private ObservableCollection<string> _faultyStateBMU;
        public ObservableCollection<string> FaultyStateBMU
        {
            get => _faultyStateBMU;
            set
            {
                SetProperty(ref _faultyStateBMU, value);
            }
        }

        private SolidColorBrush _faultyColorBMU;
        public SolidColorBrush FaultyColorBMU
        {
            get => _faultyColorBMU;
            set
            {
                SetProperty(ref _faultyColorBMU, value);
            }
        }

        private string _chargeChannelStateNumber;
        public string ChargeChannelStateNumber
        {
            get => _chargeChannelStateNumber;
            set
            {
                SetProperty(ref _chargeChannelStateNumber, value);
            }
        }

        private double _chargeCapacitySum;
        public double ChargeCapacitySum
        {

            get => _chargeCapacitySum;
            set
            {
                SetProperty(ref _chargeCapacitySum, value);
            }
        }
        #endregion

        private double _minVoltage;
        public double MinVoltage { get; set; }

        private int _minVoltageIndex;
        public int MinVoltageIndex 
        { 
            get => _minVoltageIndex;
            set
            {
                if (_minVoltageIndex != value)
                {
                    _minVoltageIndex = value;
                    BatteryViewModelList[value - 1].MarkMinVoltage();
                }
            }
        }

        private double _maxVoltage;
        public double MaxVoltage { get; set; }

        private int _maxVoltageIndex;
        public int MaxVoltageIndex 
        { 
            get=> _maxVoltageIndex; 
            set
            {
                if (_maxVoltageIndex != value)
                {
                    _maxVoltageIndex = value;
                    BatteryViewModelList[value - 1].MarkMaxVoltage();
                }
            }
        }

        private double _minTemperature;
        public double MinTemperature { get; set; }

        private int _minTemperatureIndex;
        public int MinTemperatureIndex 
        { 
            get=> _minTemperatureIndex; 
            set
            {
                if (_minTemperatureIndex != value)
                {
                    _minTemperatureIndex = value;
                    BatteryViewModelList[value - 1].MarkMinTemperature();
                }
            }
        }

        private double _maxTemperature;
        public double MaxTemperature { get; set; }

        private int _maxTemperatureIndex;
        public int MaxTemperatureIndex 
        { 
            get=> _maxTemperatureIndex; 
            set
            {
                if (_maxTemperatureIndex != value)
                {
                    _maxTemperatureIndex = value;
                    BatteryViewModelList[value - 1].MarkMaxTemperature();
                }
            }
        }

        private ushort _chargeChannelState;
        public ushort ChargeChannelState { get; set; }

        public List<BatteryViewModel> BatteryViewModelList { get; private set; }

        public BatterySeriesViewModel(int count)
        {
            BatteryViewModelList = new List<BatteryViewModel>();
            for (int i = 0; i < count; i++)
            {
                BatteryViewModelList.Add(new BatteryViewModel());
            }
        }

        public void GetActiveFaultyBMU(int flag)
        {
            int Value = flag;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            bool colorflag = false;
            if ((Value & 0x0001) != 0) { INFO.Add("电压传感器异常"); colorflag = true; } //bit0
            if ((Value & 0x0002) != 0) { INFO.Add("温度传感器异常"); colorflag = true; }  //bit1
            if ((Value & 0x0004) != 0) { INFO.Add("内部通讯故障"); colorflag = true; }  //bit2
            if ((Value & 0x0008) != 0) { INFO.Add("输入过压故障"); colorflag = true; }  //bit3
            if ((Value & 0x0010) != 0) { INFO.Add("输入反接故障"); colorflag = true; }  //bit4
            if ((Value & 0x0020) != 0) { INFO.Add("继电器故障"); colorflag = true; } //bit5
            if ((Value & 0x0040) != 0) { INFO.Add("电池损坏故障"); colorflag = true; } //bit6
            if ((Value & 0x0080) != 0) { INFO.Add("关机电路异常"); colorflag = true; } //bit7
            if ((Value & 0x0100) != 0) { INFO.Add("BMIC异常"); colorflag = true; } //bit8
            if ((Value & 0x0200) != 0) { INFO.Add("内部总线异常"); colorflag = true; } //bit9
            if ((Value & 0x0400) != 0) { INFO.Add("开机自检异常"); colorflag = true; } //bit10
            FaultyStateBMU = INFO;

            if (colorflag) { FaultyColorBMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000")); }
            else { FaultyColorBMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1")); }
        }

        public void GetActiveAlarmBMU(int flag)
        {
            int Value = flag;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            bool colorflag = false;
            if ((Value & 0x0001) != 0) { INFO.Add("单体低压告警"); colorflag = true; } //bit0
            if ((Value & 0x0002) != 0) { INFO.Add("单体高压告警"); colorflag = true; }  //bit1
            if ((Value & 0x0004) != 0) { INFO.Add("放电低压告警"); colorflag = true; }  //bit2
            if ((Value & 0x0008) != 0) { INFO.Add("充电高压告警"); colorflag = true; }  //bit3
            if ((Value & 0x0010) != 0) { INFO.Add("充电低温告警"); colorflag = true; }  //bit4
            if ((Value & 0x0020) != 0) { INFO.Add("充电高温告警"); colorflag = true; } //bit5
            if ((Value & 0x0040) != 0) { INFO.Add("放电低温告警"); colorflag = true; } //bit6
            if ((Value & 0x0080) != 0) { INFO.Add("放电高温告警"); colorflag = true; } //bit7
            if ((Value & 0x0100) != 0) { INFO.Add("充电过流告警"); colorflag = true; } //bit8
            if ((Value & 0x0200) != 0) { INFO.Add("放电过流告警"); colorflag = true; } //bit9
            if ((Value & 0x0400) != 0) { INFO.Add("模块低压告警"); colorflag = true; } //bit10
            if ((Value & 0x0800) != 0) { INFO.Add("模块高压告警"); colorflag = true; }//bit11
            AlarmStateBMU = INFO;
        }
    }
}
