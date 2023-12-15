using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Service;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EMS.ViewModel
{
    public class BatteryTotalViewModel : ViewModelBase
    {
        #region DependencyProperty

        private BitmapSource _devImage;
        public BitmapSource DevImage
        {
            get => _devImage;
            set
            {
                SetProperty(ref _devImage, value);
            }
        }

        private string _totalID;
        public string TotalID
        {
            get => _totalID;
            set
            {
                SetProperty(ref _totalID, value);
            }
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set
            {
                SetProperty(ref _ip, value);
            }
        }

        private string _port;
        public string Port
        {
            get => _port;
            set
            {
                SetProperty(ref _port, value);
            }
        }

        private BitmapSource _connectImage;
        public BitmapSource ConnectImage
        {

            get => _connectImage;
            set
            {
                SetProperty(ref _connectImage, value);
            }
        }

        private BitmapSource _daqDataImage;
        public BitmapSource DaqDataImage
        {

            get => _daqDataImage;
            set
            {
                SetProperty(ref _daqDataImage, value);
            }
        }

        private BitmapSource _recordDataImage;
        public BitmapSource RecordDataImage
        {

            get => _recordDataImage;
            set
            {
                SetProperty(ref _recordDataImage, value);
            }
        }

        // 下面都是需要实时更新的数据
        private double _totalVoltage;
        public double TotalVoltage
        {

            get => _totalVoltage;
            set
            {
                SetProperty(ref _totalVoltage, value);
            }
        }

        private double _totalCurrent;
        public double TotalCurrent
        {

            get => _totalCurrent;
            set
            {
                SetProperty(ref _totalCurrent, value);
            }
        }

        private double _totalSOC;
        public double TotalSOC
        {

            get => _totalSOC;
            set
            {
                SetProperty(ref _totalSOC, value);
            }
        }

        private double _totalSOH;
        public double TotalSOH
        {

            get => _totalSOH;
            set
            {
                SetProperty(ref _totalSOH, value);
            }
        }

        private double _averageTemperature;
        public double AverageTemperature
        {

            get => _averageTemperature;
            set
            {
                SetProperty(ref _averageTemperature, value);
            }
        }

        private double _minVoltage;
        public double MinVoltage
        {

            get => _minVoltage;
            set
            {
                SetProperty(ref _minVoltage, value);
            }
        }

        private int _minVoltageIndex;
        public int MinVoltageIndex
        {

            get => _minVoltageIndex;
            set
            {
                SetProperty(ref _minVoltageIndex, value);
            }
        }

        private double _maxVoltage;
        public double MaxVoltage
        {

            get => _maxVoltage;
            set
            {
                SetProperty(ref _maxVoltage, value);
            }
        }

        private int _maxVoltageIndex;
        public int MaxVoltageIndex
        {

            get => _maxVoltageIndex;
            set
            {
                SetProperty(ref _maxVoltageIndex, value);
            }
        }

        private double _minTemperature;
        public double MinTemperature
        {

            get => _minTemperature;
            set
            {
                SetProperty(ref _minTemperature, value);
            }
        }

        private int _minTemperatureIndex;
        public int MinTemperatureIndex
        {

            get => _minTemperatureIndex;
            set
            {
                SetProperty(ref _minTemperatureIndex, value);
            }
        }

        private double _maxTemperature;
        public double MaxTemperature
        {

            get => _maxTemperature;
            set
            {
                SetProperty(ref _maxTemperature, value);
            }
        }

        private int _maxTemperatureIndex;
        public int MaxTemperatureIndex
        {

            get => _maxTemperatureIndex;
            set
            {
                SetProperty(ref _maxTemperatureIndex, value);
            }
        }

        private SolidColorBrush _alarmColorBCMU;
        public SolidColorBrush AlarmColorBCMU
        {
            get => _alarmColorBCMU;
            set
            {
                SetProperty(ref _alarmColorBCMU, value);
            }
        }

        private ObservableCollection<string> _alarmStateBCMU;
        public ObservableCollection<string> AlarmStateBCMU
        {
            get => _alarmStateBCMU;
            set
            {
                SetProperty(ref _alarmStateBCMU, value);
            }
        }

        private SolidColorBrush _faultyColorBCMU;
        public SolidColorBrush FaultyColorBCMU
        {
            get => _faultyColorBCMU;
            set
            {
                SetProperty(ref _faultyColorBCMU, value);
            }
        }

        private ObservableCollection<string> _faultyStateBCMU;
        public ObservableCollection<string> FaultyStateBCMU
        {
            get => _faultyStateBCMU;
            set
            {
                SetProperty(ref _faultyStateBCMU, value);
            }
        }

        private int _dcVoltage;
        public int DCVoltage
        {
            get => _dcVoltage;
            set
            {
                SetProperty(ref _dcVoltage, value);
            }
        }

        private double _batMaxChgPower;
        public double BatMaxChgPower
        {
            get => _batMaxChgPower;
            set
            {
                SetProperty(ref _batMaxChgPower, value);
            }
        }

        private double _batMaxDischgPower;
        public double BatMaxDischgPower
        {
            get => _batMaxDischgPower;
            set
            {
                SetProperty(ref _batMaxDischgPower, value);
            }
        }

        private double _oneChgCoulomb;
        public double OneChgCoulomb
        {
            get => _oneChgCoulomb;
            set
            {
                SetProperty(ref _oneChgCoulomb, value);
            }
        }

        private double _oneDischgCoulomb;
        public double OneDischgCoulomb
        {
            get => _oneDischgCoulomb;
            set
            {
                SetProperty(ref _oneDischgCoulomb, value);
            }
        }

        private double _totalChgCoulomb;
        public double TotalChgCoulomb
        {
            get => _totalChgCoulomb;
            set
            {
                SetProperty(ref _totalChgCoulomb, value);
            }
        }

        private double _totalDischgCoulomb;
        public double TotalDischgCoulomb
        {
            get => _totalDischgCoulomb;
            set
            {
                SetProperty(ref _totalDischgCoulomb, value);
            }
        }

        private double _restCoulomb;
        public double RestCoulomb
        {
            get => _restCoulomb;
            set
            {
                SetProperty(ref _restCoulomb, value);
            }
        }

        private double _maxVolDiff;
        public double MaxVolDiff
        {
            get => _maxVolDiff;
            set
            {
                SetProperty(ref _maxVolDiff, value);
            }
        }

        private double _avgVol;
        public double AvgVol
        {
            get => _avgVol;
            set
            {
                SetProperty(ref _avgVol, value);
            }
        }

        private double _volContainerTemperature1;
        public double VolContainerTemperature1
        {
            get => _volContainerTemperature1;
            set
            {
                SetProperty(ref _volContainerTemperature1, value);
            }
        }

        private double _volContainerTemperature2;
        public double VolContainerTemperature2
        {
            get => _volContainerTemperature2;
            set
            {
                SetProperty(ref _volContainerTemperature2, value);
            }
        }

        private double _volContainerTemperature3;
        public double VolContainerTemperature3
        {
            get => _volContainerTemperature3;
            set
            {
                SetProperty(ref _volContainerTemperature3, value);
            }
        }

        private double _volContainerTemperature4;
        public double VolContainerTemperature4
        {
            get => _volContainerTemperature4;
            set
            {
                SetProperty(ref _volContainerTemperature4, value);
            }
        }

        private int _iResistanceRP;
        public int IResistanceRP
        {
            get => _iResistanceRP;
            set
            {
                SetProperty(ref _iResistanceRP, value);
            }
        }

        private int _iResistanceRN;
        public int IResistanceRN
        {
            get => _iResistanceRN;
            set
            {
                SetProperty(ref _iResistanceRN, value);
            }
        }

        private SolidColorBrush _chargeStateBCMU;
        public SolidColorBrush ChargeStateBCMU
        {
            get => _chargeStateBCMU;
            set
            {
                SetProperty(ref _chargeStateBCMU, value);
            }
        }

        private SolidColorBrush _disChargeStateBCMU;
        public SolidColorBrush DisChargeStateBCMU
        {
            get => _disChargeStateBCMU;
            set
            {
                SetProperty(ref _disChargeStateBCMU, value);
            }
        }

        private SolidColorBrush _standStateBCMU;
        public SolidColorBrush StandStateBCMU
        {
            get => _standStateBCMU;
            set
            {
                SetProperty(ref _standStateBCMU, value);
            }
        }

        private SolidColorBrush _offNetSateBCMU;
        public SolidColorBrush OffNetStateBCMU
        {
            get => _offNetSateBCMU;
            set
            {
                SetProperty(ref _offNetSateBCMU, value);
            }
        }

        private int _versionSWBCMU;
        public int VersionSWBCMU
        {
            get => _versionSWBCMU;
            set
            {
                SetProperty(ref _versionSWBCMU, value);
            }
        }

        private ushort _batteryCount;
        public ushort BatteryCount
        {
            get => _batteryCount;
            set
            {
                SetProperty(ref _batteryCount, value);
            }
        }

        private Visibility _devControlVisibility;
        public Visibility DevControlVisibility
        {
            get=> _devControlVisibility;
            set
            {
                SetProperty(ref _devControlVisibility, value);
            }
        }

        #endregion

        #region Command

        public RelayCommand ConnectDevCommand { get; set; }
        public RelayCommand DisconnectDevCommand { get; set; }

        #endregion

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            private set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    if (_isConnected)
                    {
                        ConnectImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnConnect.png"));
                        DevControlVisibility = Visibility.Visible;
                    }
                    else
                    {
                        ConnectImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffConnect.png"));
                        DevControlVisibility = Visibility.Hidden;
                    }
                }
            }
        }

        private bool _isDaqData;
        public bool IsDaqData
        {
            get => _isDaqData;
            private set
            {
                if(_isDaqData != value)
                {
                    _isDaqData = value;
                    if (_isDaqData)
                    {
                        DaqDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnDaq.png"));
                    }
                    else
                    {
                        DaqDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffDaq.png"));
                    }
                }
            }
        }

        private bool _isRecordData = false;
        public bool IsRecordData
        {
            get=>_isRecordData;
            set
            {
                if(_isRecordData != value)
                {
                    _isRecordData = value;
                    if (_isRecordData)
                    {
                        RecordDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnRecord.png"));
                    }
                    else
                    {
                        RecordDataImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffRecord.png"));
                    }
                }
            }
        }

        public List<BatterySeriesViewModel> batterySeriesViewModelList { get; private set; }
        public DevControlViewModel devControlViewModel;
        public ParameterSettingViewModel parameterSettingViewModel;
        private ConcurrentQueue<BatteryTotalModel> TotalList;
        private BMSDataService service;

        public BatteryTotalViewModel(string ip, string port)
        {
            ConnectDevCommand = new RelayCommand(ConnectDev);
            DisconnectDevCommand = new RelayCommand(DisconnectDev);

            IP = ip;
            Port = port;
            TotalList = new ConcurrentQueue<BatteryTotalModel>();
            devControlViewModel = new DevControlViewModel(service.Client);
            batterySeriesViewModelList = new List<BatterySeriesViewModel>();
            for (int i = 0; i < 3; i++)
            {
                batterySeriesViewModelList.Add(new BatterySeriesViewModel(14));
            }
        }

        private void ServiceStateCallBack(bool isConnected, bool isDaqData)
        {
            IsConnected = isConnected;
            IsDaqData = isDaqData;
        }

        private void DisconnectDev()
        {
            service.Disconnect();
        }

        private void ConnectDev()
        {
            service = new BMSDataService();
            service.RegisterState(ServiceStateCallBack);
            service.SetCommunicationConfig(IP, Port, TotalList);
            service.Connect();
        }

        public void StartDaqData()
        {
            service.StartDaqData();
            Thread thread = new Thread(RefreshDataTh);
            thread.IsBackground = true;
            thread.Start();
        }

        private void RefreshDataTh()
        {
            while(IsDaqData)
            {
                if(TotalList.TryDequeue(out BatteryTotalModel result))
                {
                    // 把数据分发给需要显示的内容
                    RefreshData(result);

                    if (IsRecordData)
                    {
                        SaveData(result);
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        private void RefreshData(BatteryTotalModel model)
        {
            this.TotalVoltage = model.TotalVoltage;
            this.TotalCurrent = model.TotalCurrent;
            this.TotalSOC = model.TotalSOC;
            this.TotalSOH = model.TotalSOH;
            this.AverageTemperature = model.AverageTemperature;
            this.MinVoltage = model.MinVoltage;
            this.MaxVoltage = model.MaxVoltage;
            this.MinVoltageIndex = model.MinVoltageIndex;
            this.MaxVoltageIndex = model.MaxVoltageIndex;
            this.MinTemperature = model.MinTemperature;
            this.MaxTemperature = model.MaxTemperature;
            this.MinTemperatureIndex = model.MinTemperatureIndex;
            this.MaxTemperatureIndex = model.MaxTemperatureIndex;
            this.VersionSWBCMU = model.VersionSWBCMU;
            this.BatteryCount = model.BatteryCount;
            this.IResistanceRP = model.IResistanceRP;
            this.IResistanceRN = model.IResistanceRN;
            this.DCVoltage = model.DCVoltage;
            this.VolContainerTemperature1 = model.VolContainerTemperature1;
            this.VolContainerTemperature2 = model.VolContainerTemperature2;
            this.VolContainerTemperature3 = model.VolContainerTemperature3;
            this.VolContainerTemperature4 = model.VolContainerTemperature4;
            this.BatMaxChgPower = model.BatMaxChgPower;
            this.BatMaxDischgPower = model.BatMaxDischgPower;
            this.OneChgCoulomb = model.OneChgCoulomb;
            this.OneDischgCoulomb = model.OneDischgCoulomb;
            this.TotalChgCoulomb = model.TotalChgCoulomb;
            this.TotalDischgCoulomb = model.TotalDischgCoulomb;
            this.RestCoulomb = model.RestCoulomb;
            this.MaxVolDiff = model.MaxVolDiff;
            this.AvgVol = model.AvgVol;
            StateBCMUChange(model.StateBCMU);
            GetActiveFaultyBCMU(model.FaultyStateBCMUFlag);
            GetBCMUAlarm(model.AlarmStateBCMUFlag1, model.AlarmStateBCMUFlag2, model.AlarmStateBCMUFlag3);


            for (int i = 0; i < batterySeriesViewModelList.Count; i++)
            {
                BatterySeriesViewModel series = batterySeriesViewModelList[i];
                series.ChargeChannelState = model.Series[i].ChargeChannelState;
                series.ChargeCapacitySum = model.Series[i].ChargeCapacitySum;
                series.MinVoltage = model.Series[i].MinVoltage;
                series.MaxVoltage = model.Series[i].MaxVoltage;
                series.MinVoltageIndex = model.Series[i].MinVoltageIndex;
                series.MaxVoltageIndex = model.Series[i].MaxVoltageIndex;
                series.MinTemperature = model.Series[i].MinTemperature;
                series.MaxTemperature = model.Series[i].MaxTemperature;
                series.MinTemperatureIndex = model.Series[i].MinTemperatureIndex;
                series.MaxTemperatureIndex = model.Series[i].MaxTemperatureIndex;
                series.ChargeChannelStateNumber = model.Series[i].ChargeChannelStateNumber;
                series.GetActiveFaultyBMU(model.Series[i].FaultyStateFlagBMU);
                series.GetActiveAlarmBMU(model.Series[i].AlarmStateFlagBMU);
                series.BMUID = model.Series[i].BMUID;

                for (int j = 0; j < series.BatteryViewModelList.Count; j++)
                {
                    BatteryViewModel battery = series.BatteryViewModelList[j];
                    battery.Voltage = model.Series[i].Batteries[j].Voltage;
                    battery.Temperature1 = model.Series[i].Batteries[j].Temperature1;
                    battery.Temperature2 = model.Series[i].Batteries[j].Temperature2;
                    battery.SOC = model.Series[i].Batteries[j].SOC;
                    battery.SOH = model.Series[i].Batteries[j].SOH;
                    battery.Resistance = model.Series[i].Batteries[j].Resistance;
                    battery.Capacity = model.Series[i].Batteries[j].Capacity;
                    battery.BatteryNumber = j + 1;
                }
            }
        }

        private void StateBCMUChange(int state)
        {
            if (state == 1)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
            else if (state == 2)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
            else if (state == 3)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
            else if (state == 4)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
            }
        }

        private void GetBCMUAlarm(int value1, int value2, int value3)
        {
            int j1 = 0;
            int j2 = 0;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            Dictionary<int, int> result2 = new Dictionary<int, int>();
            Dictionary<int, int> result3 = new Dictionary<int, int>();
            int colorvalue1 = 0;
            if ((value1 & 0x0001) != 0) { INFO.Add("高压箱高温异常"); colorvalue1 = 3; }       //bit0
            if ((value1 & 0x0002) != 0) { INFO.Add("充电过流异常"); colorvalue1 = 3; }  //bit1
            if ((value1 & 0x0004) != 0) { INFO.Add("放电过流异常"); colorvalue1 = 3; }  //bit2
            if ((value1 & 0x0008) != 0) { INFO.Add("绝缘Rp异常低"); colorvalue1 = 3; }  //bit3
            if ((value1 & 0x0010) != 0) { INFO.Add("绝缘Rn异常低"); colorvalue1 = 3; }  //bit4
            if ((value1 & 0x0020) != 0) { INFO.Add("绝缘HV与PE短路异常"); colorvalue1 = 3; }  //bit5
            if ((value1 & 0x0040) != 0) { INFO.Add("绝缘BAT负与PE短路异常"); colorvalue1 = 3; }  //bit6
            if ((value1 & 0x0060) != 0) { INFO.Add("并网压差大异常"); colorvalue1 = 3; }
            if ((value1 & 0x0080) != 0) { INFO.Add("高压箱NTC连接2级保护"); colorvalue1 = 2; }

            for (int i = 0; i < 16; i += 2)
            {
                int twoBitValue = (value2 >> i) & 0x3;
                j1 = i / 2;
                if (twoBitValue != 0)
                {

                    result2.Add(j1, twoBitValue);
                }
            }
            for (int i = 0; i < 16; i += 2)
            {
                int twoBitValue = (value3 >> i) & 0x3;
                j2 = i / 2;
                if (twoBitValue != 0)
                {

                    result3.Add(j2, twoBitValue);
                }


            }
            int colorvalue2 = 0;
            int colorvalue3 = 0;
            int colorvalueper = 0;

            if (result2.Count != 0 && result3.Count != 0)
            {
                colorvalue2 = result2.Max(pair => pair.Value);
                colorvalue3 = result3.Max(pair => pair.Value);
                colorvalueper = Math.Max(colorvalue2, colorvalue3);
            }
            else if (result2.Count == 0 && result3.Count != 0)
            {
                colorvalueper = result3.Max(pair => pair.Value);
            }
            else if (result3.Count == 0 && result2.Count != 0)
            {
                colorvalueper = result2.Max(pair => pair.Value);
            }

            int colorvalue = Math.Max(colorvalue1, colorvalueper);
            foreach (var item in result2)
            {

                switch (item.Key)
                {
                    case 0:
                        {
                            if (item.Value == 1) INFO.Add("单体电池低压1级");
                            if (item.Value == 2) INFO.Add("单体电池低压2级");
                            if (item.Value == 3) INFO.Add("单体电池低压3级");
                        }
                        break;
                    case 1:
                        {
                            if (item.Value == 1) INFO.Add("单体电池高压1级");
                            if (item.Value == 2) INFO.Add("单体电池高压2级");
                            if (item.Value == 3) INFO.Add("单体电池高压3级");
                        }
                        break;
                    case 2:
                        {
                            if (item.Value == 1) INFO.Add("电池组低压1级");
                            if (item.Value == 2) INFO.Add("电池组低压2级");
                            if (item.Value == 3) INFO.Add("电池组低压3级");
                        }
                        break;
                    case 3:
                        {
                            if (item.Value == 1) INFO.Add("电池组高压1级");
                            if (item.Value == 2) INFO.Add("电池组高压2级");
                            if (item.Value == 3) INFO.Add("电池组高压3级");
                        }
                        break;
                    case 4:
                        {
                            if (item.Value == 1) INFO.Add("充电低温1级");
                            if (item.Value == 2) INFO.Add("充电低温2级");
                            if (item.Value == 3) INFO.Add("充电低温3级");
                        }
                        break;
                    case 5:
                        {
                            if (item.Value == 1) INFO.Add("充电高温1级");
                            if (item.Value == 2) INFO.Add("充电高温2级");
                            if (item.Value == 3) INFO.Add("充电高温3级");
                        }
                        break;
                    case 6:
                        {
                            if (item.Value == 1) INFO.Add("放电低温1级");
                            if (item.Value == 2) INFO.Add("放电低温2级");
                            if (item.Value == 3) INFO.Add("放电低温3级");
                        }
                        break;
                    case 7:
                        {
                            if (item.Value == 1) INFO.Add("放电高温1级");
                            if (item.Value == 2) INFO.Add("放电高温2级");
                            if (item.Value == 3) INFO.Add("放电高温3级");
                        }
                        break;

                }
            }

            foreach (var item in result3)
            {
                switch (item.Key)
                {
                    case 0:
                        {
                            if (item.Value == 1) INFO.Add("电池组充电过流1级");
                            if (item.Value == 2) INFO.Add("电池组充电过流2级");
                            if (item.Value == 3) INFO.Add("电池组充电过流3级");
                        }
                        break;
                    case 1:
                        {
                            if (item.Value == 1) INFO.Add("电池组放电过流1级");
                            if (item.Value == 2) INFO.Add("电池组放电过流2级");
                            if (item.Value == 3) INFO.Add("电池组放电过流3级");
                        }
                        break;
                    case 2:
                        {
                            if (item.Value == 1) INFO.Add("单体压差1级");
                            if (item.Value == 2) INFO.Add("单体压差2级");
                            if (item.Value == 3) INFO.Add("单体压差3级");
                        }
                        break;
                    case 3:
                        {
                            if (item.Value == 1) INFO.Add("低SOC1级");
                            if (item.Value == 2) INFO.Add("低SOC2级");
                            if (item.Value == 3) INFO.Add("低SOC3级");
                        }
                        break;
                }

            }
            AlarmStateBCMU = INFO;
            List<string> list = new List<string>();

            if (colorvalue == 1)
            {
                AlarmColorBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA07A"));
            }
            else if (colorvalue == 2)
            {
                AlarmColorBCMU = new SolidColorBrush(Colors.Orange);
            }
            else if (colorvalue == 3)
            {
                AlarmColorBCMU = new SolidColorBrush(Colors.Red);
            }
            else
            {
                AlarmColorBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
        }

        public void GetActiveFaultyBCMU(int flag)
        {
            bool colorflag = false;
            if ((flag & 0x0001) != 0) { FaultyStateBCMU.Add("主接触开关异常"); colorflag = true; } //bit0
            if ((flag & 0x0002) != 0) { FaultyStateBCMU.Add("预放继电器开关异常"); colorflag = true; }  //bit1
            if ((flag & 0x0004) != 0) { FaultyStateBCMU.Add("断路器继电器开关异常"); colorflag = true; }  //bit2
            if ((flag & 0x0008) != 0) { FaultyStateBCMU.Add("CAN通讯异常"); colorflag = true; }  //bit3
            if ((flag & 0x0010) != 0) { FaultyStateBCMU.Add("485硬件异常"); colorflag = true; }  //bit4
            if ((flag & 0x0020) != 0) { FaultyStateBCMU.Add("以太网phy异常"); colorflag = true; } //bit5
            if ((flag & 0x0040) != 0) { FaultyStateBCMU.Add("以太网通讯测试异常"); colorflag = true; } //bit6
            if ((flag & 0x0080) != 0) { FaultyStateBCMU.Add("霍尔ADC I2C通讯异常"); colorflag = true; } //bit7
            if ((flag & 0x0100) != 0) { FaultyStateBCMU.Add("霍尔电流检测异常"); colorflag = true; } //bit8
            if ((flag & 0x0200) != 0) { FaultyStateBCMU.Add("分流器电流检测异常"); colorflag = true; } //bit9
            if ((flag & 0x0400) != 0) { FaultyStateBCMU.Add("绝缘检测ADC I2C通讯异常"); colorflag = true; } //bit10
            if ((flag & 0x0800) != 0) { FaultyStateBCMU.Add("高压DC电压检测ADC I2C通讯异常"); colorflag = true; }//bit11
            if ((flag & 0x1000) != 0) { FaultyStateBCMU.Add("高压箱NTC连接异常"); colorflag = true; } //bit12

            if (colorflag == true)
            {
                FaultyColorBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
            }
            else
            {
                FaultyColorBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
        }

        private void SaveData(BatteryTotalModel total)
        {
            DateTime date = DateTime.Now;
            TotalBatteryInfoModel TotalModel = new TotalBatteryInfoModel();
            TotalModel.BCMUID = TotalID;
            TotalModel.Voltage = total.TotalVoltage;
            TotalModel.Current = total.TotalCurrent;
            TotalModel.SOC = total.TotalSOC;
            TotalModel.SOH = total.TotalSOH;
            TotalModel.AverageTemperature = total.AverageTemperature;
            TotalModel.MinVoltage = total.MinVoltage;
            TotalModel.MinVoltageIndex = total.MinVoltageIndex;
            TotalModel.MaxVoltage = total.MaxVoltage;
            TotalModel.MaxVoltageIndex = total.MaxVoltageIndex;
            TotalModel.MinTemperature = total.MinTemperature;
            TotalModel.MinTemperatureIndex = total.MinTemperatureIndex;
            TotalModel.MaxTemperature = total.MaxTemperature;
            TotalModel.MaxTemperatureIndex = total.MaxTemperatureIndex;
            TotalModel.HappenTime = date;
            TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
            TotalManage.Insert(TotalModel);

            for (int i = 0; i < total.Series.Count; i++)
            {
                SeriesBatteryInfoModel SeriesModel = new SeriesBatteryInfoModel();
                SeriesModel.BCMUID = TotalID;
                SeriesModel.BMUID = total.Series[i].BMUID;
                SeriesModel.MinVoltage = total.Series[i].MinVoltage;
                SeriesModel.MinVoltageIndex = total.Series[i].MinVoltageIndex;
                SeriesModel.MaxVoltage = total.Series[i].MaxVoltage;
                SeriesModel.MaxVoltageIndex = total.Series[i].MaxVoltageIndex;
                SeriesModel.MinTemperature = total.Series[i].MinTemperature;
                SeriesModel.MinTemperatureIndex = total.Series[i].MinTemperatureIndex;
                SeriesModel.MaxTemperature = total.Series[i].MaxTemperature;
                SeriesModel.MaxTemperatureIndex = total.Series[i].MaxTemperatureIndex;
                SeriesModel.AlarmState = total.Series[i].AlarmStateFlagBMU.ToString();
                SeriesModel.FaultState = total.Series[i].FaultyStateFlagBMU.ToString();
                SeriesModel.ChargeChannelState = total.Series[i].ChargeChannelState.ToString();
                SeriesModel.ChargeCapacitySum = total.Series[i].ChargeCapacitySum;
                SeriesModel.HappenTime = date;
                for (int j = 0; j < total.Series[i].Batteries.Count; j++)
                {
                    typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Voltage);
                    typeof(SeriesBatteryInfoModel).GetProperty("Capacity" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Capacity);
                    typeof(SeriesBatteryInfoModel).GetProperty("SOC" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].SOC);
                    typeof(SeriesBatteryInfoModel).GetProperty("Resistance" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Resistance);
                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2)).SetValue(SeriesModel, total.Series[i].Batteries[j].Temperature1);
                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2 + 1)).SetValue(SeriesModel, total.Series[i].Batteries[j].Temperature2);
                }
                SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                SeriesManage.Insert(SeriesModel);
            }
        }

        public void StopDaqData()
        {
            service.StopDaqData();
        }

        public void StartSaveData()
        {
            IsRecordData = true;
        }

        public void StopSaveData()
        {
            IsRecordData = false;
        }
    }
}
