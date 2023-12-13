using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.View;
using System.IO;
using System.Windows.Media.Imaging;
using EMS.Model;
using EMS.Api;

namespace EMS.ViewModel
{
    public class PCSMainViewModel : ObservableObject
    {
        /// <summary>
        /// 连接IP
        /// </summary>
        private string _iP;
        public string IP
        {
            get => _iP;
            set
            {
                SetProperty(ref _iP, value);
            }
        }

        /// <summary>
        /// 采集状态图片
        /// </summary>
        private static BitmapImage DataAcuisitionOn = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/play.png"));
        private static BitmapImage DataAcuisitionOff = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/pause.png"));

        public BitmapImage DataAcquisitionImageSource
        {
            get
            {
                if (IsRead) return DataAcuisitionOn;
                else return DataAcuisitionOff;
            }
        }


        /// <summary>
        /// 连接状态图片
        /// </summary>
        private static BitmapImage Connected = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnConnect.png"));
        private static BitmapImage Unconnected = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffConnect.png"));
        public BitmapImage ConnectImageSource
        {
            get
            {
                if (IsConnected) return Connected;
                else return Unconnected;
            }
        }

        public bool IsConnected { get { return pcsModel.IsConnected; } }

        public bool IsRead { get { return pcsModel.IsRead; } }

        /// <summary>
        /// 主界面PCS连接状态颜色显示
        /// </summary>
        private SolidColorBrush _mainWindowPCSConnectColor;
        public SolidColorBrush MainWindowPCSConnectColor
        {
            get => _mainWindowPCSConnectColor;
            set
            {
                SetProperty(ref _mainWindowPCSConnectColor, value);
            }
        }

        /// <summary>
        /// 主界面PCS连接状态显示
        /// </summary>
        private string _mainWindowPCSConnectState;
        public string MainWindowPCSConnectState
        {
            get => _mainWindowPCSConnectState;
            set
            {
                SetProperty(ref _mainWindowPCSConnectState, value);
            }
        }

        public DCStatusViewModel dcStatusViewModel;
        public PCSMonitorViewModel pcsMonitorViewModel;

        public PCSModel pcsModel;

        public Thread thread;

        private int DaqTimeSpan = 1;

        public RelayCommand ConnectMSLCommand { get; set; }
        public RelayCommand SyncBUSVolInfoCommand { get; set; }
        //public RelayCommand SyncTimeInfoCommand { get; set; }
        //public RelayCommand SyncCMTimeOutCommand { get; set; }
        public RelayCommand ReadBUSVolInfoCommand { get; set; }
        //public RelayCommand ReadCMTimeOutCommand { get; set; }
        //public RelayCommand ReadTimeInfoCommand { get; set; }
        public RelayCommand SyncDCBranchInfoCommand { get; set; }
        public RelayCommand ReadDCBranchInfoCommand { get; set; }
        public RelayCommand ModeSetCommand { get; set; }
        public RelayCommand ManCharCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand StartDaqCommand { get; set; }
        public RelayCommand StopDaqCommand { get; set; }
        public RelayCommand DisConnectCommand { get; set; }


        public PCSMainViewModel()
        {
            dcStatusViewModel = new DCStatusViewModel();
            pcsMonitorViewModel = new PCSMonitorViewModel();


            pcsModel = new PCSModel();
            EnergyManagementSystem.GlobalInstance.PcsManager.SetPCS(pcsModel);


            ConnectCommand = new RelayCommand(Connect);
            DisConnectCommand = new RelayCommand(DisConnect);
            StartDaqCommand = new RelayCommand(StartDataAcquisition);
            StopDaqCommand = new RelayCommand(StopDataAcquisition);

            pcsModel.MonitorModel.VisDCFault = Visibility.Hidden;
            pcsModel.MonitorModel.VisPDSFault = Visibility.Hidden;
            pcsModel.MonitorModel.VisDCAlarm = Visibility.Hidden;
            pcsModel.MonitorModel.VisPDSAlarm = Visibility.Hidden;

            MainWindowPCSConnectState = "未连接";
            MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);


            SyncBUSVolInfoCommand = new RelayCommand(SyncBUSVolInfo);
            //SyncCMTimeOutCommand = new RelayCommand(SyncCMTimeOut);
            ReadBUSVolInfoCommand = new RelayCommand(ReadBUSVolInfo);
            //ReadCMTimeOutCommand = new RelayCommand(ReadCMTimeOut);
            SyncDCBranchInfoCommand = new RelayCommand(SyncDCBranchInfo);
            ReadDCBranchInfoCommand = new RelayCommand(ReadDCBranchInfo);
            ModeSetCommand = new RelayCommand(ModeSet);
            ManCharCommand = new RelayCommand(ManChar);

            pcsModel.ParSettingModel.VisDCPower = Visibility.Hidden;
            pcsModel.ParSettingModel.VisDCCur = Visibility.Hidden;
            pcsModel.ParSettingModel.VisDCChar = Visibility.Hidden;
        }




        public void Connect()
        {
            try
            {
                if (pcsModel.IsConnected == true)
                {
                    MessageBox.Show("已连接");
                }
                if (pcsModel.IsConnected == false)
                {
                    PCSConView view = new PCSConView();
                    if (view.ShowDialog() == true)
                    {
                        IP = view.PCSIPText.AddressText;
                        int port = Convert.ToInt32(view.PCSTCPPort.Text);
                        pcsModel.Connect(IP, port);

                        MainWindowPCSConnectState = "已连接";
                        MainWindowPCSConnectColor = new SolidColorBrush(Colors.Green);
                    }
                }
            }
            catch (Exception)
            {
                MainWindowPCSConnectState = "未连接";
                MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);
                MessageBox.Show("请输入正确的IP地址");
            }
        }

        public void DisConnect()
        {
            try
            {
                if (pcsModel.IsConnected == false)
                {
                    MessageBox.Show("请连接");
                }
                if (pcsModel.IsConnected == true & pcsModel.IsRead == false)
                {
                    pcsModel.Disconnect();

                    MainWindowPCSConnectState = "未连接";
                    MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);
                }
                else if (pcsModel.IsConnected == true & pcsModel.IsRead == true)
                {
                    MessageBox.Show("请停止采集");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void StartDataAcquisition()
        {
            if (pcsModel.IsConnected == false)
            {
                MessageBox.Show("请连接");
            }
            else
            {
                thread = new Thread(ReadINFO);
                thread.IsBackground = true;

                pcsModel.IsRead = true;
                thread.Start();
            }
        }

        public void StopDataAcquisition()
        {
            if (pcsModel.IsRead == true)
            {
                pcsModel.IsRead = false;
            }
            else
            {
                MessageBox.Show("请开始采集");
            }
        }

        public void ReadINFO()
        {
            while (true)
            {
                if (!pcsModel.IsRead)
                {
                    break;
                }
                try
                {
                    byte[] DCstate = pcsModel.ModbusClient.ReadFunc(53026, 7);
                    dcStatusViewModel.ModuleOnLineFlag = BitConverter.ToUInt16(DCstate, 0);
                    dcStatusViewModel.ModuleRunFlag = BitConverter.ToUInt16(DCstate, 4);
                    dcStatusViewModel.ModuleAlarmFlag = BitConverter.ToUInt16(DCstate, 8);
                    dcStatusViewModel.ModuleFaultFlag = BitConverter.ToUInt16(DCstate, 12);

                    byte[] PCSData = pcsModel.ModbusClient.ReadFunc(53005, 10);
                    pcsMonitorViewModel.AlarmStateFlagDC1 = BitConverter.ToUInt16(PCSData, 0);
                    pcsMonitorViewModel.AlarmStateFlagDC2 = BitConverter.ToUInt16(PCSData, 4);
                    pcsMonitorViewModel.AlarmStateFlagDC3 = BitConverter.ToUInt16(PCSData, 6);
                    pcsMonitorViewModel.AlarmStateFlagPDS = BitConverter.ToUInt16(PCSData, 8);
                    pcsMonitorViewModel.ControlStateFlagPCS = BitConverter.ToUInt16(PCSData, 10);
                    pcsMonitorViewModel.StateFlagPCS = BitConverter.ToUInt16(PCSData, 12);
                    pcsMonitorViewModel.DcBranch1StateFlag1 = BitConverter.ToUInt16(PCSData, 16);
                    pcsMonitorViewModel.DcBranch1StateFlag2 = BitConverter.ToUInt16(PCSData, 18);

                    GetDCBranchINFO();

                    byte[] Temp = pcsModel.ModbusClient.ReadFunc(53221, 3);
                    pcsModel.MonitorModel.ModuleTemperature = Math.Round(BitConverter.ToUInt16(Temp, 0) * 0.1 - 20, 2);
                    pcsModel.MonitorModel.AmbientTemperature = Math.Round(BitConverter.ToUInt16(Temp, 4) * 0.1 - 20, 2);

                    byte[] DCBranch1INFO = pcsModel.ModbusClient.ReadFunc(53250, 10);
                    pcsModel.MonitorModel.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 0) * 0.1 - 1500, 2);
                    pcsModel.MonitorModel.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 2) * 0.1, 2);
                    pcsModel.MonitorModel.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 4) * 0.1 - 2000, 2);
                    pcsMonitorViewModel.DcBranch1CharHigh = BitConverter.ToUInt16(DCBranch1INFO, 6);
                    pcsMonitorViewModel.DcBranch1CharLow = BitConverter.ToUInt16(DCBranch1INFO, 8);
                    pcsMonitorViewModel.DcBranch1DisCharHigh = BitConverter.ToUInt16(DCBranch1INFO, 10);
                    pcsMonitorViewModel.DcBranch1DisCharLow = BitConverter.ToUInt16(DCBranch1INFO, 12);
                    pcsModel.MonitorModel.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 18) * 0.1, 2);

                    EnergyCal();

                    bool FaultColorFlagDC = GetDCFault();
                    bool FaultColorFlagPDS = GetPDSFault();
                    bool AlarmColorFlagDC = GetDCAlarm();
                    bool AlarmColorFlagPDS = GetPDSAlarm();

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (AlarmColorFlagDC == true)
                        {
                            pcsModel.MonitorModel.VisDCAlarm = Visibility.Visible;
                            pcsModel.MonitorModel.AlarmColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            pcsModel.MonitorModel.VisDCAlarm = Visibility.Hidden;
                        }

                        if (AlarmColorFlagPDS == true)
                        {
                            pcsModel.MonitorModel.VisPDSAlarm = Visibility.Visible;
                            pcsModel.MonitorModel.AlarmColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            pcsModel.MonitorModel.VisPDSAlarm = Visibility.Hidden;
                        }



                        if (FaultColorFlagDC == true)
                        {
                            pcsModel.MonitorModel.VisDCFault = Visibility.Visible;
                            pcsModel.MonitorModel.FaultColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pcsModel.MonitorModel.VisDCFault = Visibility.Hidden;
                        }

                        if (FaultColorFlagPDS == true)
                        {
                            pcsModel.MonitorModel.VisPDSFault = Visibility.Visible;
                            pcsModel.MonitorModel.FaultColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pcsModel.MonitorModel.VisPDSFault = Visibility.Hidden;
                        }
                        GetActivePCSControlState();
                        GetActivePCSState();
                        DaqDCModuleStatus();
                    });

                    Thread.Sleep(DaqTimeSpan * 1000);
                }
                catch (Exception ex)
                {

                }

            }
        }

        public void GetActivePCSControlState()
        {
            int value;
            value = pcsMonitorViewModel.ControlStateFlagPCS;
            if ((value & 0x0100) != 0)
            {
                pcsModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0200) != 0)
            {
                pcsModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                pcsModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                pcsModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetActivePCSState()
        {
            int value;
            value = pcsMonitorViewModel.StateFlagPCS;
            if ((value & 0x0200) != 0)
            {
                pcsModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                pcsModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x1000) != 0)
            {
                pcsModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                pcsModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pcsModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetDCBranchINFO()
        {
            int value1;
            int value2;

            value1 = pcsMonitorViewModel.DcBranch1StateFlag1;
            value2 = pcsMonitorViewModel.DcBranch1StateFlag2;
            if ((value1 & 0x0001) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State1 = "电池充满";
            }
            else if ((value1 & 0x0002) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State1 = "电池放空";
            }
            else if ((value1 & 0x0004) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State1 = "充电";
            }
            else if ((value1 & 0x0008) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State1 = "放电";
            }
            else if ((value1 & 0x0040) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State1 = "电池恒压均充";
            }


            if ((value2 & 0x0001) != 0)
            {
                pcsModel.MonitorModel.DcBranch1State2 = "启动";
            }
            else if ((value2 & 0x0001) == 0)
            {
                pcsModel.MonitorModel.DcBranch1State2 = "停止";
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
            value1 = pcsMonitorViewModel.DcBranch1CharHigh;
            value2 = pcsMonitorViewModel.DcBranch1CharLow;
            value3 = pcsMonitorViewModel.DcBranch1DisCharHigh;
            value4 = pcsMonitorViewModel.DcBranch1DisCharLow;
            pcsModel.MonitorModel.DcBranch1Char = value1 << 16 | value2;
            pcsModel.MonitorModel.DcBranch1DisChar = value3 << 16 | value4;
        }

        public bool GetDCFault()
        {
            int value1;
            int value2;
            int value3;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = pcsMonitorViewModel.AlarmStateFlagDC1;
            value2 = pcsMonitorViewModel.AlarmStateFlagDC2;
            value3 = pcsMonitorViewModel.AlarmStateFlagDC3;
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
            pcsModel.MonitorModel.FaultInfoDC = INFO;

            return colorflag;
        }



        public bool GetPDSFault()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = pcsMonitorViewModel.AlarmStateFlagPDS;
            if ((value & 0x0001) != 0) { INFO.Add("软件版本故障"); colorflag = true; } //53009 bit0
            if ((value & 0x0002) != 0) { INFO.Add("DSP初始化故障"); colorflag = true; } //bit1
            if ((value & 0x0004) != 0) { INFO.Add("BMS故障"); colorflag = true; } //bit2
            if ((value & 0x0008) != 0) { INFO.Add("紧急停机"); colorflag = true; } //bit3

            pcsModel.MonitorModel.FaultInfoPDS = INFO;
            return colorflag;
        }

        public bool GetDCAlarm()
        {
            int value1;
            int value2;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = pcsMonitorViewModel.AlarmStateFlagDC1;
            value2 = pcsMonitorViewModel.AlarmStateFlagDC2;

            if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高"); colorflag = true; } //bit10  AAAA
            if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); colorflag = true; } //bit12  AAAAA
            if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); colorflag = true; } //bit14  AAAAAA
            if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit15  AAAAAA

            if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); colorflag = true; } //bit9   AAAAAA
            pcsModel.MonitorModel.AlarmInfoDC = INFO;
            return colorflag;
        }

        public bool GetPDSAlarm()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = pcsMonitorViewModel.AlarmStateFlagPDS;

            if ((value & 0x0010) != 0) { INFO.Add("防雷器告警"); colorflag = true; } //bit4   AAAAAAAAA
            pcsModel.MonitorModel.AlarmInfoPDS = INFO;
            return colorflag;
        }



        private void SyncBUSVolInfo()
        {
            if (pcsModel.IsConnected)
            {
                if (pcsModel.ParSettingModel.BUSUpperLimitVolThresh < 100 || pcsModel.ParSettingModel.BUSUpperLimitVolThresh > 900)
                {
                    MessageBox.Show("上限电压：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSUpperLimitVolThresh.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSUpperLimitVolThresh.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("上限电压：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.BUSLowerLimitVolThresh < 100 || pcsModel.ParSettingModel.BUSLowerLimitVolThresh > 900)
                {
                    MessageBox.Show("下限电压：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSLowerLimitVolThresh.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSLowerLimitVolThresh.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("下限电压：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.BUSHVolSetting < 100 || pcsModel.ParSettingModel.BUSHVolSetting > 900)
                {
                    MessageBox.Show("高压设置：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSHVolSetting.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSHVolSetting.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("高压设置：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.BUSLVolSetting < 100 || pcsModel.ParSettingModel.BUSLVolSetting > 900)
                {
                    MessageBox.Show("低压设置：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSLVolSetting.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BUSLVolSetting.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("低压设置：请输入一位小数");
                    return;
                }
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53640, (ushort)(pcsModel.ParSettingModel.BUSUpperLimitVolThresh * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53641, (ushort)(pcsModel.ParSettingModel.BUSLowerLimitVolThresh * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53642, (ushort)(pcsModel.ParSettingModel.BUSHVolSetting * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53643, (ushort)(pcsModel.ParSettingModel.BUSLVolSetting * 10));
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        /// <summary>
        /// 使用四舍五入避免出现多位小数的情况。
        /// </summary>
        private void ReadBUSVolInfo()
        {
            if (pcsModel.IsConnected)
            {
                byte[] data = pcsModel.ModbusClient.ReadFunc(53640, 4);
                pcsModel.ParSettingModel.BUSUpperLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 0) * 0.1, 2);
                pcsModel.ParSettingModel.BUSLowerLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 2) * 0.1, 2);
                pcsModel.ParSettingModel.BUSHVolSetting = Math.Round(BitConverter.ToInt16(data, 4) * 0.1, 2);
                pcsModel.ParSettingModel.BUSLVolSetting = Math.Round(BitConverter.ToInt16(data, 6) * 0.1, 2);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }

        private void SyncCMTimeOut()
        {
            if (pcsModel.IsConnected)
            {
                if (pcsModel.ParSettingModel.BMSCMInterruptionTimeOut < 1 || pcsModel.ParSettingModel.BMSCMInterruptionTimeOut > 600)
                {
                    MessageBox.Show("BMS通信超时设置：请输入1-600的整数");
                    return;
                }
                if (pcsModel.ParSettingModel.Remote485CMInterruptonTimeOut < 1 || pcsModel.ParSettingModel.Remote485CMInterruptonTimeOut > 600)
                {
                    MessageBox.Show("远程485通信超时设置：请输入1-600的整数");
                    return;
                }
                if (pcsModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut < 1 || pcsModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut > 600)
                {
                    MessageBox.Show("远程TCP通信超时设置：请输入1-600的整数");
                    return;
                }
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 56006, (ushort)(pcsModel.ParSettingModel.BMSCMInterruptionTimeOut));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 56007, (ushort)(pcsModel.ParSettingModel.Remote485CMInterruptonTimeOut));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 56008, (ushort)(pcsModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut));
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void ReadCMTimeOut()
        {
            if (pcsModel.IsConnected)
            {
                byte[] data = pcsModel.ModbusClient.ReadFunc(56006, 3);
                pcsModel.ParSettingModel.BMSCMInterruptionTimeOut = BitConverter.ToUInt16(data, 0);
                pcsModel.ParSettingModel.Remote485CMInterruptonTimeOut = BitConverter.ToUInt16(data, 2);
                pcsModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut = BitConverter.ToUInt16(data, 4);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void SyncDCBranchInfo()
        {

            if (pcsModel.IsConnected)
            {

                if (pcsModel.ParSettingModel.BTLLimitVol > 800 || pcsModel.ParSettingModel.BTLLimitVol < 30)
                {
                    MessageBox.Show("电池下限电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BTLLimitVol.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BTLLimitVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("电池下限电压：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.DischargeSTVol > 900 || pcsModel.ParSettingModel.DischargeSTVol < 30)
                {
                    MessageBox.Show("放电终止电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DischargeSTVol.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DischargeSTVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("放电终止电压：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.MultiBranchCurRegPar > 50 || pcsModel.ParSettingModel.MultiBranchCurRegPar < -50)
                {
                    MessageBox.Show("多支路电流调节参数：请输入-50到50的数");
                    return;
                }

                if (pcsModel.ParSettingModel.BatAveChVol > 800 || pcsModel.ParSettingModel.BatAveChVol < 30)
                {
                    MessageBox.Show("电池均充电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BatAveChVol.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.BatAveChVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("电池均充电压：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.ChCutCurrent > 250 || pcsModel.ParSettingModel.ChCutCurrent < 0)
                {
                    MessageBox.Show("充电截止电流：请输入0到250的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.ChCutCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.ChCutCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("充电截止电流：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.MaxChCurrent > 1500 || pcsModel.ParSettingModel.MaxChCurrent < 0)
                {
                    MessageBox.Show("最大充电电流：请输入0到1500的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.MaxChCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.MaxChCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("最大充电电流：请输入一位小数");
                    return;
                }

                if (pcsModel.ParSettingModel.MaxDisChCurrent > 1500 || pcsModel.ParSettingModel.MaxDisChCurrent < 0)
                {
                    MessageBox.Show("最大放电电流：请输入0到1500的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.MaxDisChCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.MaxDisChCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("最大放电电流：请输入一位小数");
                    return;
                }



                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53653, (ushort)(pcsModel.ParSettingModel.BTLLimitVol * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53655, (ushort)(pcsModel.ParSettingModel.DischargeSTVol * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53658, (ushort)pcsModel.ParSettingModel.MultiBranchCurRegPar);
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53660, (ushort)(pcsModel.ParSettingModel.BatAveChVol * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53662, (ushort)(pcsModel.ParSettingModel.ChCutCurrent * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53663, (ushort)(pcsModel.ParSettingModel.MaxChCurrent * 10));
                pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53664, (ushort)(pcsModel.ParSettingModel.MaxDisChCurrent * 10));


            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void ReadDCBranchInfo()
        {
            if (pcsModel.IsConnected)
            {
                byte[] data11 = pcsModel.ModbusClient.ReadFunc(53651, 3);
                pcsModel.ParSettingModel.DCCurrentSet = Math.Round(BitConverter.ToInt16(data11, 0) * 0.1, 2);
                pcsModel.ParSettingModel.DCPowerSet = Math.Round(BitConverter.ToInt16(data11, 2) * 0.1, 2);
                pcsModel.ParSettingModel.BTLLimitVol = Math.Round(BitConverter.ToInt16(data11, 4) * 0.1, 2);

                byte[] data12 = pcsModel.ModbusClient.ReadFunc(53655, 1);
                pcsModel.ParSettingModel.DischargeSTVol = Math.Round(BitConverter.ToInt16(data12, 0) * 0.1, 2);

                byte[] data13 = pcsModel.ModbusClient.ReadFunc(53658, 1);
                pcsModel.ParSettingModel.MultiBranchCurRegPar = BitConverter.ToInt16(data13, 0);

                byte[] data14 = pcsModel.ModbusClient.ReadFunc(53660, 1);
                pcsModel.ParSettingModel.BatAveChVol = Math.Round(BitConverter.ToInt16(data14, 0) * 0.1, 2);

                byte[] data15 = pcsModel.ModbusClient.ReadFunc(53662, 3);
                pcsModel.ParSettingModel.ChCutCurrent = Math.Round(BitConverter.ToInt16(data15, 0) * 0.1, 2);
                pcsModel.ParSettingModel.MaxChCurrent = Math.Round(BitConverter.ToInt16(data15, 2) * 0.1, 2);
                pcsModel.ParSettingModel.MaxDisChCurrent = Math.Round(BitConverter.ToInt16(data15, 4) * 0.1, 2);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }

        private void ModeSet()
        {
            if (pcsModel.IsConnected)
            {

                if (pcsModel.ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53650, 0);
                    pcsModel.ParSettingModel.VisDCCur = Visibility.Visible;
                    pcsModel.ParSettingModel.VisDCPower = Visibility.Hidden;
                    pcsModel.ParSettingModel.VisDCChar = Visibility.Visible;
                }
                else if (pcsModel.ParSettingModel.ModeSet1 == "设置功率调节")
                {
                    pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53650, 1);
                    pcsModel.ParSettingModel.VisDCPower = Visibility.Visible;
                    pcsModel.ParSettingModel.VisDCCur = Visibility.Hidden;
                    pcsModel.ParSettingModel.VisDCChar = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("请选择模式");
                }
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }

        private void ManChar()
        {
            if (pcsModel.IsConnected)
            {
                if (pcsModel.ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    if (pcsModel.ParSettingModel.DCCurrentSet > 1500 || pcsModel.ParSettingModel.DCCurrentSet < -1500)
                    {
                        MessageBox.Show("直流电流设置：请输入-1500到1500的数");
                        return;
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCCurrentSet.ToString(), @"^\d+\.\d$") == false & System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCCurrentSet.ToString(), @"^\-\d+\.\d$") == false
                        & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCCurrentSet.ToString(), @"^\d+$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCCurrentSet.ToString(), @"^\-\d+$"))
                    {
                        MessageBox.Show("直流电流设置：请输入一位小数");
                        return;
                    }
                    pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53651, (ushort)(pcsModel.ParSettingModel.DCCurrentSet * 10));
                }
                else
                {
                    if (pcsModel.ParSettingModel.DCPowerSet > 1000 || pcsModel.ParSettingModel.DCPowerSet < -1000)
                    {
                        MessageBox.Show("直流功率设置：请输入-1000到1000的数");
                        return;
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCPowerSet.ToString(), @"^\d+\.\d$") == false & System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCPowerSet.ToString(), @"^\-\d+\.\d$") == false
                        & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCPowerSet.ToString(), @"^\d+$") & !System.Text.RegularExpressions.Regex.IsMatch(pcsModel.ParSettingModel.DCPowerSet.ToString(), @"^\-\d+$"))
                    {
                        MessageBox.Show("直流功率设置：请输入一位小数");
                        return;
                    }
                    pcsModel.ModbusClient.WriteFunc(PCSModel.PcsId, 53652, (ushort)(pcsModel.ParSettingModel.DCPowerSet * 10));
                }
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        public void DaqDCModuleStatus()
        {

            int onlinevalue;
            int runvalue;
            int alarmvalue;
            int faultvalue;
            onlinevalue = dcStatusViewModel.ModuleOnLineFlag;
            runvalue = dcStatusViewModel.ModuleRunFlag;
            alarmvalue = dcStatusViewModel.ModuleAlarmFlag;
            faultvalue = dcStatusViewModel.ModuleFaultFlag;

            //DC模组1状态
            if ((onlinevalue & 0x0001) != 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pcsModel.MonitorModel.Module1Status1 = "在线";
                pcsModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0001) != 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0 && (onlinevalue & 0x0001) == 0)
            {
                pcsModel.MonitorModel.Module1Status1 = "运行";
                pcsModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pcsModel.MonitorModel.Module1Status1 = "告警";
                pcsModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0)
            {
                pcsModel.MonitorModel.Module1Status1 = "故障";
                pcsModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pcsModel.MonitorModel.Module1Status1 = "离线";
                pcsModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组2状态
            if ((onlinevalue & 0x0002) != 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pcsModel.MonitorModel.Module1Status2 = "在线";
                pcsModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0002) != 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0 && (onlinevalue & 0x0002) == 0)
            {
                pcsModel.MonitorModel.Module1Status2 = "运行";
                pcsModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pcsModel.MonitorModel.Module1Status2 = "告警";
                pcsModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0)
            {
                pcsModel.MonitorModel.Module1Status2 = "故障";
                pcsModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pcsModel.MonitorModel.Module1Status2 = "离线";
                pcsModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组3状态
            if ((onlinevalue & 0x0004) != 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pcsModel.MonitorModel.Module1Status3 = "在线";
                pcsModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0004) != 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0 && (onlinevalue & 0x0004) == 0)
            {
                pcsModel.MonitorModel.Module1Status3 = "运行";
                pcsModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pcsModel.MonitorModel.Module1Status3 = "告警";
                pcsModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0)
            {
                pcsModel.MonitorModel.Module1Status3 = "故障";
                pcsModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pcsModel.MonitorModel.Module1Status3 = "离线";
                pcsModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组4状态
            if ((onlinevalue & 0x0008) != 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pcsModel.MonitorModel.Module1Status4 = "在线";
                pcsModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0008) != 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0 && (onlinevalue & 0x0008) == 0)
            {
                pcsModel.MonitorModel.Module1Status4 = "运行";
                pcsModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pcsModel.MonitorModel.Module1Status4 = "告警";
                pcsModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0)
            {
                pcsModel.MonitorModel.Module1Status4 = "故障";
                pcsModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pcsModel.MonitorModel.Module1Status4 = "离线";
                pcsModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组5状态
            if ((onlinevalue & 0x0010) != 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pcsModel.MonitorModel.Module1Status5 = "在线";
                pcsModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0010) != 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0 && (onlinevalue & 0x0010) == 0)
            {
                pcsModel.MonitorModel.Module1Status5 = "运行";
                pcsModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pcsModel.MonitorModel.Module1Status5 = "告警";
                pcsModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0)
            {
                pcsModel.MonitorModel.Module1Status5 = "故障";
                pcsModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pcsModel.MonitorModel.Module1Status5 = "离线";
                pcsModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组6状态
            if ((onlinevalue & 0x0020) != 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pcsModel.MonitorModel.Module1Status6 = "在线";
                pcsModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0020) != 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0 && (onlinevalue & 0x0020) == 0)
            {
                pcsModel.MonitorModel.Module1Status6 = "运行";
                pcsModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pcsModel.MonitorModel.Module1Status6 = "告警";
                pcsModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0)
            {
                pcsModel.MonitorModel.Module1Status6 = "故障";
                pcsModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pcsModel.MonitorModel.Module1Status6 = "离线";
                pcsModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组7状态
            if ((onlinevalue & 0x0040) != 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                pcsModel.MonitorModel.Module1Status7 = "在线";
                pcsModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0040) != 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0 && (onlinevalue & 0x0040) == 0)
            {
                pcsModel.MonitorModel.Module1Status7 = "运行";
                pcsModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                pcsModel.MonitorModel.Module1Status7 = "告警";
                pcsModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0)
            {
                pcsModel.MonitorModel.Module1Status7 = "故障";
                pcsModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0040) == 0)
            {
                pcsModel.MonitorModel.Module1Status7 = "离线";
                pcsModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组8状态
            if ((onlinevalue & 0x0080) != 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pcsModel.MonitorModel.Module1Status8 = "在线";
                pcsModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0080) != 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0 && (onlinevalue & 0x0080) == 0)
            {
                pcsModel.MonitorModel.Module1Status8 = "运行";
                pcsModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pcsModel.MonitorModel.Module1Status8 = "告警";
                pcsModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0)
            {
                pcsModel.MonitorModel.Module1Status8 = "故障";
                pcsModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pcsModel.MonitorModel.Module1Status8 = "离线";
                pcsModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组9状态
            if ((onlinevalue & 0x0100) != 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pcsModel.MonitorModel.Module1Status9 = "在线";
                pcsModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0100) != 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0 && (onlinevalue & 0x0100) == 0)
            {
                pcsModel.MonitorModel.Module1Status9 = "运行";
                pcsModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pcsModel.MonitorModel.Module1Status9 = "告警";
                pcsModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0)
            {
                pcsModel.MonitorModel.Module1Status9 = "故障";
                pcsModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pcsModel.MonitorModel.Module1Status9 = "离线";
                pcsModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            // DC模组10状态
            if ((onlinevalue & 0x0200) != 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pcsModel.MonitorModel.Module1Status10 = "在线";
                pcsModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0200) != 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0 && (onlinevalue & 0x0200) == 0)
            {
                pcsModel.MonitorModel.Module1Status10 = "运行";
                pcsModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pcsModel.MonitorModel.Module1Status10 = "告警";
                pcsModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0)
            {
                pcsModel.MonitorModel.Module1Status10 = "故障";
                pcsModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pcsModel.MonitorModel.Module1Status10 = "离线";
                pcsModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }
        }
    }
}
