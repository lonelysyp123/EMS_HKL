using EMS.Common.Modbus.ModbusTCP;
using EMS.ViewModel;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace EMS.Model
{
    public class PCSModel : ViewModelBase
    {
        private int DataAcquireTimeSpan = 1;
        private DcStatusModel _dcStatusModel;

        
        private bool _isConnected;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected
        {  
            get =>_isConnected;
            private set
            {
                _isConnected = value;
                if (_isConnected)
                {
                    ConnectImageSource= new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnConnect.png"));
                }
                else
                {
                    ConnectImageSource= new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffConnect.png"));
                }
            }
        }

        /// <summary>
        /// 连接状态图片
        /// </summary>
        private BitmapImage _connectImageSource;
        public BitmapImage ConnectImageSource
        {
            get
            {
                return _connectImageSource;
            }
            set
            {
                SetProperty(ref _connectImageSource, value);
            }
        }

        private bool _isRead;
        /// <summary>
        /// 采集状态
        /// </summary>
        public bool IsRead 
        { 
            get =>_isRead;
            private set
            {
                if (_isRead!=value)
                {
                    _isRead = value;
                    if (_isRead)
                    {
                        DataAcquisitionImageSource= new BitmapImage(new Uri("pack://application:,,,/Resource/Image/play.png"));
                    }
                    else
                    {
                        DataAcquisitionImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/pause.png"));
                    }
                }
            }
        }

        /// <summary>
        /// 采集状态图片
        /// </summary>
        private BitmapImage _dataAcquisitionImageSource;
        public BitmapImage DataAcquisitionImageSource
        {
            get
            {
                return _dataAcquisitionImageSource;
            }
            set
            {
                SetProperty(ref _dataAcquisitionImageSource, value);
            }
        }



        private Thread _dataAcquisitionThread;
        public Thread DataAcuisitionThread { get { return _dataAcquisitionThread; } }

        
        private ModbusClient _modbusClient;
        public ModbusClient ModbusClient { get { return _modbusClient; } }


        public PCSMonitorModel MonitorModel { get; set; }
        public PCSParSettingModel ParSettingModel { get; set; }

        private ILog Logger;

        public void Connect(string ip, int port)
        {
            try
            {
                _modbusClient = new ModbusClient(ip, port);
                _modbusClient.Connect();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                Logger.Info(ex.ToString());
                throw ex;
            }
            
        }

        public void Disconnect()
        {
            if (!_isConnected) return;
            _modbusClient.Disconnect();
            IsConnected = false;
        }

        public void SendPcsCommand(BessCommand command)
        {
            PcsCommandAdressEnum modeAddress = PcsCommandAdressEnum.CharModeSet;
            int modeValue = 0;
            PcsCommandAdressEnum valueAddress = PcsCommandAdressEnum.PowerValueSet;
            int controlValue = 0;

            switch (command.BatteryStrategy)
            {
                case BatteryStrategyEnum.Standby:
                    modeValue = 1;
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    controlValue = 0;
                    break;
                case BatteryStrategyEnum.ConstantCurrentCharge:
                    modeValue = 0;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10));
                    valueAddress = PcsCommandAdressEnum.CurrentValueSet;
                    break;
                case BatteryStrategyEnum.ConstantCurrentDischarge: //需要添加负值
                    modeValue = 0;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10) * -1);
                    valueAddress = PcsCommandAdressEnum.CurrentValueSet;
                    break;
                case BatteryStrategyEnum.ConstantPowerCharge:
                    modeValue = 1;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10));
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    break;
                case BatteryStrategyEnum.ConstantPowerDischarge:  //需要添加负值
                    modeValue = 1;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10) * -1);
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    break;
            }

            _modbusClient.WriteFunc(PcsId, modeAddress, modeValue);
            _modbusClient.WriteFunc(PcsId, valueAddress, controlValue);
        }

        public void StartDataAcquisition()
        {
            _dataAcquisitionThread = new Thread(ReadInfo);
            _dataAcquisitionThread.IsBackground = true;
            IsRead = true;
            _dataAcquisitionThread.Start();
        }
        public void StopDataAcquisition()
        {
            if (IsRead)
            {
                IsRead = false;
            }
        }

        public void SyncBUSVolInfo()
        {
            ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.HigherVolThreshold, (ushort)(ParSettingModel.BUSUpperLimitVolThresh * 10));
            ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.LowerVolThreshold, (ushort)(ParSettingModel.BUSLowerLimitVolThresh * 10));
            ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.HigherVolSetting, (ushort)(ParSettingModel.BUSHVolSetting * 10));
            ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.LowerVolSetting, (ushort)(ParSettingModel.BUSLVolSetting * 10));
        }

        public void ReadBUSVolInfo()
        {
            if (IsConnected)
            {
                byte[] data = ModbusClient.ReadFunc(53640, 4);
                ParSettingModel.BUSUpperLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 0) * 0.1, 2);
                ParSettingModel.BUSLowerLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 2) * 0.1, 2);
                ParSettingModel.BUSHVolSetting = Math.Round(BitConverter.ToInt16(data, 4) * 0.1, 2);
                ParSettingModel.BUSLVolSetting = Math.Round(BitConverter.ToInt16(data, 6) * 0.1, 2);
            }
        }

        public void ReadCMTimeOut()
        {
            if (IsConnected)
            {
                byte[] data = ModbusClient.ReadFunc(56006, 3);
                ParSettingModel.BMSCMInterruptionTimeOut = BitConverter.ToUInt16(data, 0);
                ParSettingModel.Remote485CMInterruptonTimeOut = BitConverter.ToUInt16(data, 2);
                ParSettingModel.RemoteTCPCMInterruptionTimeOut = BitConverter.ToUInt16(data, 4);
            }
        }

        //public void SyncCMTimeOut()
        //{
        //    if (IsConnected)
        //    {
        //        ModbusClient.WriteFunc(PcsId, 56006, (ushort)(ParSettingModel.BMSCMInterruptionTimeOut));
        //        ModbusClient.WriteFunc(PcsId, 56007, (ushort)(ParSettingModel.Remote485CMInterruptonTimeOut));
        //        ModbusClient.WriteFunc(PcsId, 56008, (ushort)(ParSettingModel.RemoteTCPCMInterruptionTimeOut));
        //    }
        //}

        public void SyncDCBranchInfo()
        {
            if (IsConnected)
            {
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.BatteryLowerVolThreshold, (ushort)(ParSettingModel.BTLLimitVol * 10));
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.EndOfDischargeVol, (ushort)(ParSettingModel.DischargeSTVol * 10));
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.MutiStrCurRegulationPar, (ushort)ParSettingModel.MultiBranchCurRegPar);
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.BatteryToppingCharVol, (ushort)(ParSettingModel.BatAveChVol * 10));
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.EndOfCharCur, (ushort)(ParSettingModel.ChCutCurrent * 10));
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.MaxCharCur, (ushort)(ParSettingModel.MaxChCurrent * 10));
                ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.MaxDischarCur, (ushort)(ParSettingModel.MaxDisChCurrent * 10));
            }
        }

        public void ReadDCBranchInfo()
        {
            if (IsConnected)
            {
                byte[] data11 = ModbusClient.ReadFunc(53651, 3);
                ParSettingModel.DCCurrentSet = Math.Round(BitConverter.ToInt16(data11, 0) * 0.1, 2);
                ParSettingModel.DCPowerSet = Math.Round(BitConverter.ToInt16(data11, 2) * 0.1, 2);
                ParSettingModel.BTLLimitVol = Math.Round(BitConverter.ToInt16(data11, 4) * 0.1, 2);

                byte[] data12 = ModbusClient.ReadFunc(53655, 1);
                ParSettingModel.DischargeSTVol = Math.Round(BitConverter.ToInt16(data12, 0) * 0.1, 2);

                byte[] data13 = ModbusClient.ReadFunc(53658, 1);
                ParSettingModel.MultiBranchCurRegPar = BitConverter.ToInt16(data13, 0);

                byte[] data14 = ModbusClient.ReadFunc(53660, 1);
                ParSettingModel.BatAveChVol = Math.Round(BitConverter.ToInt16(data14, 0) * 0.1, 2);

                byte[] data15 = ModbusClient.ReadFunc(53662, 3);
                ParSettingModel.ChCutCurrent = Math.Round(BitConverter.ToInt16(data15, 0) * 0.1, 2);
                ParSettingModel.MaxChCurrent = Math.Round(BitConverter.ToInt16(data15, 2) * 0.1, 2);
                ParSettingModel.MaxDisChCurrent = Math.Round(BitConverter.ToInt16(data15, 4) * 0.1, 2);
            }
        }

        public void ModeSet()
        {
            if (IsConnected)
            {
                if (ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.CharModeSet, 0);
                }
                else if (ParSettingModel.ModeSet1 == "设置功率调节")
                {
                    ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.CharModeSet, 1);
                }
            }
        }

        public void ManChar()
        {
            if (IsConnected)
            {
                if (ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.CurrentValueSet, (ushort)(ParSettingModel.DCCurrentSet * 10));
                }
                else
                {
                    ModbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.PowerValueSet, (ushort)ParSettingModel.DCPowerSet * 10);
                }
            }
        }

        public void ReadInfo()
        {
            while (true)
            {
                if (!IsRead)
                {
                    break;
                }
                try
                {
                    byte[] dcState = ModbusClient.ReadFunc(53026, 7);
                    _dcStatusModel.ModuleOnLineFlag = BitConverter.ToUInt16(dcState, 0);
                    _dcStatusModel.ModuleRunFlag = BitConverter.ToUInt16(dcState, 4);
                    _dcStatusModel.ModuleAlarmFlag = BitConverter.ToUInt16(dcState, 8);
                    _dcStatusModel.ModuleFaultFlag = BitConverter.ToUInt16(dcState, 12);
                    //_dcStatusModel.ModuleAlarmFlag= dcState[0];
                    //_dcStatusModel.ModuleRunFlag = dcState[2];
                    //_dcStatusModel.ModuleAlarmFlag = dcState[4];
                    //_dcStatusModel.ModuleFaultFlag = dcState[6];

                    byte[] pcsData = ModbusClient.ReadFunc(53005, 10);
                    MonitorModel.AlarmStateFlagDC1 = BitConverter.ToUInt16(pcsData, 0);
                    MonitorModel.AlarmStateFlagDC2 = BitConverter.ToUInt16(pcsData, 4);
                    MonitorModel.AlarmStateFlagDC3 = BitConverter.ToUInt16(pcsData, 6);
                    MonitorModel.AlarmStateFlagPDS = BitConverter.ToUInt16(pcsData, 8);
                    MonitorModel.ControlStateFlagPCS = BitConverter.ToUInt16(pcsData, 10);
                    MonitorModel.StateFlagPCS = BitConverter.ToUInt16(pcsData, 12);
                    MonitorModel.DcBranch1StateFlag1 = BitConverter.ToUInt16(pcsData, 16);
                    MonitorModel.DcBranch1StateFlag2 = BitConverter.ToUInt16(pcsData, 18);

                    GetDCBranchINFO();

                    byte[] Temp = ModbusClient.ReadFunc(53221, 3);
                    MonitorModel.ModuleTemperature = Math.Round(BitConverter.ToUInt16(Temp, 0) * 0.1 - 20, 2);
                    MonitorModel.AmbientTemperature = Math.Round(BitConverter.ToUInt16(Temp, 4) * 0.1 - 20, 2);

                    byte[] DCBranch1INFO = ModbusClient.ReadFunc(53250, 10);
                    MonitorModel.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 0) * 0.1 - 1500, 2);
                    MonitorModel.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 2) * 0.1, 2);
                    MonitorModel.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 4) * 0.1 - 2000, 2);
                    MonitorModel.DcBranch1CharHigh = BitConverter.ToUInt16(DCBranch1INFO, 6);
                    MonitorModel.DcBranch1CharLow = BitConverter.ToUInt16(DCBranch1INFO, 8);
                    MonitorModel.DcBranch1DisCharHigh = BitConverter.ToUInt16(DCBranch1INFO, 10);
                    MonitorModel.DcBranch1DisCharLow = BitConverter.ToUInt16(DCBranch1INFO, 12);
                    MonitorModel.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 18) * 0.1, 2);

                    byte[] SerialNumber = ModbusClient.ReadFunc(53579, 15);
                    MonitorModel.MonitorSoftCode = BitConverter.ToUInt16(SerialNumber, 22);
                    MonitorModel.DcSoftCode=BitConverter.ToUInt16(SerialNumber, 26);
                    MonitorModel.U2SoftCode=BitConverter.ToUInt16(SerialNumber, 28);

                    EnergyCal();

                    bool FaultColorFlagDC = GetDCFault();
                    bool FaultColorFlagPDS = GetPDSFault();
                    bool AlarmColorFlagDC = GetDCAlarm();
                    bool AlarmColorFlagPDS = GetPDSAlarm();

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (AlarmColorFlagDC == true)
                        {
                            MonitorModel.VisDCAlarm = Visibility.Visible;
                            MonitorModel.AlarmColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            MonitorModel.VisDCAlarm = Visibility.Hidden;
                        }

                        if (AlarmColorFlagPDS == true)
                        {
                            MonitorModel.VisPDSAlarm = Visibility.Visible;
                            MonitorModel.AlarmColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            MonitorModel.VisPDSAlarm = Visibility.Hidden;
                        }
                        
                        if (FaultColorFlagDC == true)
                        {
                            MonitorModel.VisDCFault = Visibility.Visible;
                            MonitorModel.FaultColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            MonitorModel.VisDCFault = Visibility.Hidden;
                        }

                        if (FaultColorFlagPDS == true)
                        {
                            MonitorModel.VisPDSFault = Visibility.Visible;
                            MonitorModel.FaultColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            MonitorModel.VisPDSFault = Visibility.Hidden;
                        }
                        GetActivePCSControlState();
                        GetActivePCSState();
                        DataAcquisitionDcModuleStatus();
                    });

                    Thread.Sleep(DataAcquireTimeSpan * 1000);
                }
                catch (Exception ex)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        IsConnected = false;
                        IsRead = false;
                    });
                    Logger.Error(ex.ToString());
                }
            }
        }

        public void DataAcquisitionDcModuleStatus()
        {

            int onlinevalue;
            int runvalue;
            int alarmvalue;
            int faultvalue;
            onlinevalue = _dcStatusModel.ModuleOnLineFlag;
            runvalue = _dcStatusModel.ModuleRunFlag;
            alarmvalue = _dcStatusModel.ModuleAlarmFlag;
            faultvalue = _dcStatusModel.ModuleFaultFlag;

            //DC模组1状态
            if ((onlinevalue & 0x0001) != 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                MonitorModel.Module1Status1 = "在线";
                MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0001) != 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0 && (onlinevalue & 0x0001) == 0)
            {
                MonitorModel.Module1Status1 = "运行";
                MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                MonitorModel.Module1Status1 = "告警";
                MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0)
            {
                MonitorModel.Module1Status1 = "故障";
                MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                MonitorModel.Module1Status1 = "离线";
                MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组2状态
            if ((onlinevalue & 0x0002) != 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                MonitorModel.Module1Status2 = "在线";
                MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0002) != 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0 && (onlinevalue & 0x0002) == 0)
            {
                MonitorModel.Module1Status2 = "运行";
                MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                MonitorModel.Module1Status2 = "告警";
                MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0)
            {
                MonitorModel.Module1Status2 = "故障";
                MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                MonitorModel.Module1Status2 = "离线";
                MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组3状态
            if ((onlinevalue & 0x0004) != 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                MonitorModel.Module1Status3 = "在线";
                MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0004) != 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0 && (onlinevalue & 0x0004) == 0)
            {
                MonitorModel.Module1Status3 = "运行";
                MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                MonitorModel.Module1Status3 = "告警";
                MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0)
            {
                MonitorModel.Module1Status3 = "故障";
                MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                MonitorModel.Module1Status3 = "离线";
                MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组4状态
            if ((onlinevalue & 0x0008) != 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                MonitorModel.Module1Status4 = "在线";
                MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0008) != 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0 && (onlinevalue & 0x0008) == 0)
            {
                MonitorModel.Module1Status4 = "运行";
                MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                MonitorModel.Module1Status4 = "告警";
                MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0)
            {
                MonitorModel.Module1Status4 = "故障";
                MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                MonitorModel.Module1Status4 = "离线";
                MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组5状态
            if ((onlinevalue & 0x0010) != 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                MonitorModel.Module1Status5 = "在线";
                MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0010) != 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0 && (onlinevalue & 0x0010) == 0)
            {
                MonitorModel.Module1Status5 = "运行";
                MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                MonitorModel.Module1Status5 = "告警";
                MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0)
            {
                MonitorModel.Module1Status5 = "故障";
                MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                MonitorModel.Module1Status5 = "离线";
                MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组6状态
            if ((onlinevalue & 0x0020) != 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                MonitorModel.Module1Status6 = "在线";
                MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0020) != 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0 && (onlinevalue & 0x0020) == 0)
            {
                MonitorModel.Module1Status6 = "运行";
                MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                MonitorModel.Module1Status6 = "告警";
                MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0)
            {
                MonitorModel.Module1Status6 = "故障";
                MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                MonitorModel.Module1Status6 = "离线";
                MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组7状态
            if ((onlinevalue & 0x0040) != 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                MonitorModel.Module1Status7 = "在线";
                MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0040) != 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0 && (onlinevalue & 0x0040) == 0)
            {
                MonitorModel.Module1Status7 = "运行";
                MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                MonitorModel.Module1Status7 = "告警";
                MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0)
            {
                MonitorModel.Module1Status7 = "故障";
                MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0040) == 0)
            {
                MonitorModel.Module1Status7 = "离线";
                MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组8状态
            if ((onlinevalue & 0x0080) != 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                MonitorModel.Module1Status8 = "在线";
                MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0080) != 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0 && (onlinevalue & 0x0080) == 0)
            {
                MonitorModel.Module1Status8 = "运行";
                MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                MonitorModel.Module1Status8 = "告警";
                MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0)
            {
                MonitorModel.Module1Status8 = "故障";
                MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                MonitorModel.Module1Status8 = "离线";
                MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组9状态
            if ((onlinevalue & 0x0100) != 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                MonitorModel.Module1Status9 = "在线";
                MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0100) != 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0 && (onlinevalue & 0x0100) == 0)
            {
                MonitorModel.Module1Status9 = "运行";
                MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                MonitorModel.Module1Status9 = "告警";
                MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0)
            {
                MonitorModel.Module1Status9 = "故障";
                MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                MonitorModel.Module1Status9 = "离线";
                MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            // DC模组10状态
            if ((onlinevalue & 0x0200) != 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                MonitorModel.Module1Status10 = "在线";
                MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0200) != 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0 && (onlinevalue & 0x0200) == 0)
            {
                MonitorModel.Module1Status10 = "运行";
                MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                MonitorModel.Module1Status10 = "告警";
                MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0)
            {
                MonitorModel.Module1Status10 = "故障";
                MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                MonitorModel.Module1Status10 = "离线";
                MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }
        }

        public void GetActivePCSControlState()
        {
            int value;
            value = MonitorModel.ControlStateFlagPCS;
            if ((value & 0x0100) != 0)
            {
                MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0200) != 0)
            {
                MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetActivePCSState()
        {
            int value;
            value = MonitorModel.StateFlagPCS;
            if ((value & 0x0200) != 0)
            {
                MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x1000) != 0)
            {
                MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetDCBranchINFO()
        {
            int value1;
            int value2;

            value1 = MonitorModel.DcBranch1StateFlag1;
            value2 = MonitorModel.DcBranch1StateFlag2;
            if ((value1 & 0x0001) != 0)
            {
                MonitorModel.DcBranch1State1 = "电池充满";
            }
            else if ((value1 & 0x0002) != 0)
            {
                MonitorModel.DcBranch1State1 = "电池放空";
            }
            else if ((value1 & 0x0004) != 0)
            {
                MonitorModel.DcBranch1State1 = "充电";
            }
            else if ((value1 & 0x0008) != 0)
            {
                MonitorModel.DcBranch1State1 = "放电";
            }
            else if ((value1 & 0x0040) != 0)
            {
                MonitorModel.DcBranch1State1 = "电池恒压均充";
            }


            if ((value2 & 0x0001) != 0)
            {
                MonitorModel.DcBranch1State2 = "启动";
            }
            else if ((value2 & 0x0001) == 0)
            {
                MonitorModel.DcBranch1State2 = "停止";
            }
        }

        /// <summary>
        /// 电量计算
        /// </summary>
        public void EnergyCal()
        {
            uint value1;
            uint value2;
            uint value3;
            uint value4;
            value1 = MonitorModel.DcBranch1CharHigh;
            value2 = MonitorModel.DcBranch1CharLow;
            value3 = MonitorModel.DcBranch1DisCharHigh;
            value4 = MonitorModel.DcBranch1DisCharLow;
            MonitorModel.DcBranch1Char = value1 << 16 | value2;
            MonitorModel.DcBranch1DisChar = value3 << 16 | value4;
        }

        public bool GetDCFault()
        {
            int value1;
            int value2;
            int value3;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = MonitorModel.AlarmStateFlagDC1;
            value2 = MonitorModel.AlarmStateFlagDC2;
            value3 = MonitorModel.AlarmStateFlagDC3;
            if ((value1 & 0x0001) != 0) { INFO.Add("直流高压侧过压"); colorflag = true; } //53005 bit0
            if ((value1 & 0x0002) != 0) { INFO.Add("直流高压侧欠压"); colorflag = true; }  //bit1`
            if ((value1 & 0x0004) != 0) { INFO.Add("直流低压侧过压"); colorflag = true; }  //bit2
            if ((value1 & 0x0008) != 0) { INFO.Add("直流低压侧欠压"); colorflag = true; }  //bit3
            if ((value1 & 0x0010) != 0) { INFO.Add("直流低压侧过流"); colorflag = true; }  //bit4
            //if ((value1 & 0x0020) != 0) { INFO.Add("重启过多"); colorflag = true; } //bit5
            if ((value1 & 0x0040) != 0) { INFO.Add("重启过多"); colorflag = true; } //bit6
            if ((value1 & 0x0080) != 0) { INFO.Add("直流低压侧继电器短路"); colorflag = true; } //bit7
            if ((value1 & 0x0100) != 0) { INFO.Add("光伏能量不足"); colorflag = true; } //bit8
            if ((value1 & 0x0200) != 0) { INFO.Add("电池电量不足"); colorflag = true; } //bit9
            if ((value1 & 0x0800) != 0) { INFO.Add("直流高压侧开关断开"); colorflag = true; } //bit11
            if ((value1 & 0x2000) != 0) { INFO.Add("机柜温度过高"); colorflag = true; } //bit13


            if ((value2 & 0x0001) != 0) { INFO.Add("模块电流不平衡"); colorflag = true; } //53007 bit0
            if ((value2 & 0x0002) != 0) { INFO.Add("直流低压侧开关断开"); colorflag = true; } //bit1
            if ((value2 & 0x0004) != 0) { INFO.Add("24V辅助电源故障"); colorflag = true; } //bit2
            if ((value2 & 0x0008) != 0) { INFO.Add("紧急停机"); colorflag = true; } //bit3
            //if ((value2 & 0x0010) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit4
            //if ((value2 & 0x0020) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit5
            if ((value2 & 0x0040) != 0) { INFO.Add("模块温度过温"); colorflag = true; } //bit6
            if ((value2 & 0x0080) != 0) { INFO.Add("风扇故障"); colorflag = true; } //bit7
            if ((value2 & 0x0100) != 0) { INFO.Add("直流低压侧继电器开路"); colorflag = true; } //bit8
            if ((value2 & 0x0400) != 0) { INFO.Add("保险故障"); colorflag = true; } //bit10
            if ((value2 & 0x0800) != 0) { INFO.Add("DSP初始化故障"); colorflag = true; } //bit11
            if ((value2 & 0x1000) != 0) { INFO.Add("直流低压侧软启动失败"); colorflag = true; } //bit12
            if ((value2 & 0x2000) != 0) { INFO.Add("CANA通讯故障"); colorflag = true; } //bit13
            if ((value2 & 0x4000) != 0) { INFO.Add("直流高压侧继电器开路"); colorflag = true; } //bit14
            if ((value2 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); colorflag = true; } //bit15

            if ((value3 & 0x0001) != 0) { INFO.Add("DSP版本故障"); colorflag = true; } //53008 bit0
            if ((value3 & 0x0002) != 0) { INFO.Add("CPLD版本故障"); colorflag = true; } //bit1
            if ((value3 & 0x0004) != 0) { INFO.Add("参数不匹配"); colorflag = true; } //bit2
            if ((value3 & 0x0008) != 0) { INFO.Add("硬件版本故障"); colorflag = true; } //bit3
            if ((value3 & 0x0010) != 0) { INFO.Add("485通讯故障"); colorflag = true; } //bit4
            if ((value3 & 0x0020) != 0) { INFO.Add("CANB通讯故障"); colorflag = true; } //bit5
            if ((value3 & 0x0040) != 0) { INFO.Add("模块重号故障"); colorflag = true; } //bit6
            //if ((value3 & 0x0080) != 0) { INFO.Add("风扇故障"); colorflag = true; } //bit7
            if ((value3 & 0x0100) != 0) { INFO.Add("15V辅助电源故障"); colorflag = true; } //bit8
            if ((value3 & 0x0200) != 0) { INFO.Add("直流高压侧继电器短路"); colorflag = true; } //bit9
            if ((value3 & 0x0400) != 0) { INFO.Add("BMS电压异常"); colorflag = true; } //bit10
            if ((value3 & 0x0800) != 0) { INFO.Add("BMS电流异常"); colorflag = true; } //bit11
            if ((value3 & 0x1000) != 0) { INFO.Add("BMS温度异常"); colorflag = true; } //bit12
            if ((value3 & 0x2000) != 0) { INFO.Add("BMS关机异常"); colorflag = true; } //bit13
            if ((value3 & 0x4000) != 0) { INFO.Add("绝缘检测异常"); colorflag = true; } //bit14
            //if ((value3 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); colorflag = true; } //bit15
            MonitorModel.FaultInfoDC = INFO;

            return colorflag;
        }



        public bool GetPDSFault()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = MonitorModel.AlarmStateFlagPDS;
            if ((value & 0x0001) != 0) { INFO.Add("软件版本故障"); colorflag = true; } //53009 bit0
            if ((value & 0x0002) != 0) { INFO.Add("DSP初始化故障"); colorflag = true; } //bit1
            if ((value & 0x0004) != 0) { INFO.Add("BMS故障"); colorflag = true; } //bit2
            if ((value & 0x0008) != 0) { INFO.Add("紧急停机"); colorflag = true; } //bit3

            MonitorModel.FaultInfoPDS = INFO;
            return colorflag;
        }

        public bool GetDCAlarm()
        {
            int value1;
            int value2;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = MonitorModel.AlarmStateFlagDC1;
            value2 = MonitorModel.AlarmStateFlagDC2;

            if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高"); colorflag = true; } //bit10  AAAA
            if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); colorflag = true; } //bit12  AAAAA
            if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); colorflag = true; } //bit14  AAAAAA
            if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit15  AAAAAA

            if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); colorflag = true; } //bit9   AAAAAA
            MonitorModel.AlarmInfoDC = INFO;
            return colorflag;
        }

        public bool GetPDSAlarm()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = MonitorModel.AlarmStateFlagPDS;

            if ((value & 0x0010) != 0) { INFO.Add("防雷器告警"); colorflag = true; } //bit4   AAAAAAAAA
            MonitorModel.AlarmInfoPDS = INFO;
            return colorflag;
        }
        /// <summary>
        /// PCS系统开机
        /// </summary>
        public void PCSOpen()
        {
            try
            {
                _modbusClient.WriteFunc(PcsId,PcsCommandAdressEnum.PCSSystemOpen, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PCS系统关机
        /// </summary>
        public void PCSClose()
        {
            try
            {
                _modbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.PCSSystemClose, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PCS系统清除故障
        /// </summary>
        public void PCSSystemClearFault()
        {
            try
            {
                _modbusClient.WriteFunc(PcsId, PcsCommandAdressEnum.PCSSystemClearFault, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte PcsId = 1;

        public PCSModel()
        {
            MonitorModel = new PCSMonitorModel();

            ParSettingModel = new PCSParSettingModel();

            _dcStatusModel=new DcStatusModel();

            Logger=LogManager.GetLogger(typeof(PCSModel));
        }
    }
    public enum PcsCommandAdressEnum
    {
        [Description("系统开机")]
        PCSSystemOpen = 53900,
        [Description("系统关机")]
        PCSSystemClose = 53901,
        [Description("系统清除故障")]
        PCSSystemClearFault = 53903,
        [Description("直流控制模式")]
        CharModeSet = 53650,
        [Description("直流电流设置")]
        CurrentValueSet = 53651,
        [Description("直流功率设置")]
        PowerValueSet = 53652,
        [Description("BUS上限电压")]
        HigherVolThreshold = 53640,
        [Description("BUS下限电压")]
        LowerVolThreshold = 53641,
        [Description("BUS高压设置")]
        HigherVolSetting = 53642,
        [Description("BUS低压设置")]
        LowerVolSetting = 53643,
        [Description("DC侧支路1：电池下限电压")]
        BatteryLowerVolThreshold = 53653,
        [Description("DC侧支路1：放电终止电压")]
        EndOfDischargeVol = 53655,
        [Description("DC侧支路1：多支路电流调节参数")]
        MutiStrCurRegulationPar = 53658,
        [Description("DC侧支路1：电池均充电压")]
        BatteryToppingCharVol = 53660,
        [Description("DC侧支路1：充电截止电流")]
        EndOfCharCur = 53662,
        [Description("DC侧支路1：最大充电电流")]
        MaxCharCur = 53663,
        [Description("DC侧支路1：最大放电电流")]
        MaxDischarCur = 53664,
    }
}