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
    public class PCSMainViewModel:ObservableObject
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

        /// <summary>
        /// 采集状态图片
        /// </summary>
        private ImageSource _daqImageSource;

        public ImageSource DaqImageSource
        {
            get => _daqImageSource;
            set
            {
                SetProperty(ref _daqImageSource, value);
            }
        }

        /// <summary>
        /// 连接状态图片
        /// </summary>
        private ImageSource _connectImageSource;

        public ImageSource ConnectImageSource
        {
            get => _connectImageSource;
            set
            {
                SetProperty(ref _connectImageSource, value);
            }
        }




        public DCStatusViewModel dCStatusViewModel;
        public PCSMonitorViewModel pCSMonitorViewModel;


        public PCSModel pCSModel;


        public ModbusClient modbusClient;

        public bool isRead = false;
        public bool IsConnected = false;
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
            dCStatusViewModel = new DCStatusViewModel();
            pCSMonitorViewModel = new PCSMonitorViewModel();


            pCSModel = new PCSModel();
            EnergyManagementSystem.GlobalInstance.PcsManager.SetPCS(pCSModel);


            ConnectCommand = new RelayCommand(Connect);
            DisConnectCommand = new RelayCommand(DisConnect);
            StartDaqCommand = new RelayCommand(StartDaq);
            StopDaqCommand = new RelayCommand(StopDaq);

            pCSModel.MonitorModel.VisDCFault = Visibility.Hidden;
            pCSModel.MonitorModel.VisPDSFault = Visibility.Hidden;
            pCSModel.MonitorModel.VisDCAlarm = Visibility.Hidden;
            pCSModel.MonitorModel.VisPDSAlarm = Visibility.Hidden;

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

            pCSModel.ParSettingModel.VisDCPower = Visibility.Hidden;
            pCSModel.ParSettingModel.VisDCCur = Visibility.Hidden;
            pCSModel.ParSettingModel.VisDCChar = Visibility.Hidden;

            //BitmapImage bi;
            //DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
            //FileInfo[] files = directory.GetFiles("pause.png");
            //bi = new BitmapImage();
            //bi.BeginInit();
            //bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            //bi.EndInit();
            //DaqImageSource = bi;
        }




        public void Connect()
        {
            try
            {
                if (IsConnected == true)
                {
                    MessageBox.Show("已连接");
                }
                if (IsConnected == false)
                {
                    PCSConView view = new PCSConView();
                    if (view.ShowDialog() == true)
                    {
                        IP = view.PCSIPText.AddressText;
                        int port = Convert.ToInt32(view.PCSTCPPort.Text);
                        modbusClient = new ModbusClient(IP, port);
                        modbusClient.Connect();

                        MainWindowPCSConnectState = "已连接";
                        MainWindowPCSConnectColor = new SolidColorBrush(Colors.Green);

                        IsConnected = true;

                        BitmapImage bi;
                        DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                        FileInfo[] files = directory.GetFiles("OnConnect.png");
                        bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                        bi.EndInit();
                        ConnectImageSource = bi;
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
                if (IsConnected == false)
                {
                    MessageBox.Show("请连接");
                }
                if (IsConnected == true & isRead == false)
                {
                    modbusClient.Disconnect();
                    IsConnected = false;
                    MainWindowPCSConnectState = "未连接";
                    MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);

                    BitmapImage bi;
                    DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                    FileInfo[] files = directory.GetFiles("OffConnect.png");
                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                    bi.EndInit();
                    ConnectImageSource = bi;
                }
                else if (IsConnected == true & isRead == true)
                {
                    MessageBox.Show("请停止采集");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void StartDaq()
        {
            if (IsConnected == false)
            {
                MessageBox.Show("请连接");
            }
            else
            {
                thread = new Thread(ReadINFO);
                thread.IsBackground = true;

                isRead = true;
                thread.Start();

                BitmapImage bi;
                DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                FileInfo[] files = directory.GetFiles("play.png");
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                bi.EndInit();
                DaqImageSource = bi;
            }
        }

        public void StopDaq()
        {
            isRead = false;

            BitmapImage bi;
            DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
            FileInfo[] files = directory.GetFiles("pause.png");
            bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
            bi.EndInit();
            DaqImageSource = bi;
        }

        public void ReadINFO()
        {
            while (true)
            {
                if (!isRead)
                {
                    break;
                }
                try
                {
                    byte[] DCstate = modbusClient.ReadFunc(53026, 7);
                    dCStatusViewModel.ModuleOnLineFlag = BitConverter.ToUInt16(DCstate, 0);
                    dCStatusViewModel.ModuleRunFlag = BitConverter.ToUInt16(DCstate, 4);
                    dCStatusViewModel.ModuleAlarmFlag = BitConverter.ToUInt16(DCstate, 8);
                    dCStatusViewModel.ModuleFaultFlag = BitConverter.ToUInt16(DCstate, 12);

                    byte[] PCSData = modbusClient.ReadFunc(53005, 10);
                    pCSMonitorViewModel.AlarmStateFlagDC1 = BitConverter.ToUInt16(PCSData, 0);
                    pCSMonitorViewModel.AlarmStateFlagDC2 = BitConverter.ToUInt16(PCSData, 4);
                    pCSMonitorViewModel.AlarmStateFlagDC3 = BitConverter.ToUInt16(PCSData, 6);
                    pCSMonitorViewModel.AlarmStateFlagPDS = BitConverter.ToUInt16(PCSData, 8);
                    pCSMonitorViewModel.ControlStateFlagPCS = BitConverter.ToUInt16(PCSData, 10);
                    pCSMonitorViewModel.StateFlagPCS = BitConverter.ToUInt16(PCSData, 12);
                    pCSMonitorViewModel.DcBranch1StateFlag1 = BitConverter.ToUInt16(PCSData, 16);
                    pCSMonitorViewModel.DcBranch1StateFlag2 = BitConverter.ToUInt16(PCSData, 18);

                    GetDCBranchINFO();

                    byte[] Temp = modbusClient.ReadFunc(53221, 3);
                    pCSModel.MonitorModel.ModuleTemperature = Math.Round(BitConverter.ToUInt16(Temp, 0) * 0.1 - 20, 2);
                    pCSModel.MonitorModel.AmbientTemperature = Math.Round(BitConverter.ToUInt16(Temp, 4) * 0.1 - 20, 2);

                    byte[] DCBranch1INFO = modbusClient.ReadFunc(53250, 10);
                    pCSModel.MonitorModel.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 0) * 0.1 - 1500, 2);
                    pCSModel.MonitorModel.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 2) * 0.1, 2);
                    pCSModel.MonitorModel.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 4) * 0.1 - 2000, 2);
                    pCSMonitorViewModel.DcBranch1CharHigh = BitConverter.ToUInt16(DCBranch1INFO, 6);
                    pCSMonitorViewModel.DcBranch1CharLow = BitConverter.ToUInt16(DCBranch1INFO, 8);
                    pCSMonitorViewModel.DcBranch1DisCharHigh = BitConverter.ToUInt16(DCBranch1INFO, 10);
                    pCSMonitorViewModel.DcBranch1DisCharLow = BitConverter.ToUInt16(DCBranch1INFO, 12);
                    pCSModel.MonitorModel.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 18) * 0.1, 2);

                    EnergyCal();

                    bool FaultColorFlagDC = GetDCFault();
                    bool FaultColorFlagPDS = GetPDSFault();
                    bool AlarmColorFlagDC = GetDCAlarm();
                    bool AlarmColorFlagPDS = GetPDSAlarm();

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (AlarmColorFlagDC == true)
                        {
                            pCSModel.MonitorModel.VisDCAlarm = Visibility.Visible;
                            pCSModel.MonitorModel.AlarmColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            pCSModel.MonitorModel.VisDCAlarm = Visibility.Hidden;
                        }

                        if (AlarmColorFlagPDS == true)
                        {
                            pCSModel.MonitorModel.VisPDSAlarm = Visibility.Visible;
                            pCSModel.MonitorModel.AlarmColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
                        }
                        else
                        {
                            pCSModel.MonitorModel.VisPDSAlarm = Visibility.Hidden;
                        }



                        if (FaultColorFlagDC == true)
                        {
                            pCSModel.MonitorModel.VisDCFault = Visibility.Visible;
                            pCSModel.MonitorModel.FaultColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pCSModel.MonitorModel.VisDCFault = Visibility.Hidden;
                        }

                        if (FaultColorFlagPDS == true)
                        {
                            pCSModel.MonitorModel.VisPDSFault = Visibility.Visible;
                            pCSModel.MonitorModel.FaultColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pCSModel.MonitorModel.VisPDSFault = Visibility.Hidden;
                        }
                        GetActivePCSControlState();
                        GetActivePCSState();
                        DaqDCModuleStatus();
                    });
                    
                    Thread.Sleep(DaqTimeSpan * 1000);
                }
                catch
                {

                }
                
            }
        }

        public void GetActivePCSControlState()
        {
            int value;
            value = pCSMonitorViewModel.ControlStateFlagPCS;
            if ((value & 0x0100) != 0)
            {
                pCSModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0200) != 0)
            {
                pCSModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                pCSModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                pCSModel.MonitorModel.PCSStateColorAutoControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorManControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PCSStateColorRemoteControl = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetActivePCSState()
        {
            int value;
            value = pCSMonitorViewModel.StateFlagPCS;
            if ((value & 0x0200) != 0)
            {
                pCSModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x0400) != 0)
            {
                pCSModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else if ((value & 0x1000) != 0)
            {
                pCSModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98"));
            }
            else
            {
                pCSModel.MonitorModel.AlarmStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.FaultStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
                pCSModel.MonitorModel.PowerOnInitStateColorPCS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
            }
        }

        public void GetDCBranchINFO()
        {
            int value1;
            int value2;

            value1 = pCSMonitorViewModel.DcBranch1StateFlag1;
            value2 = pCSMonitorViewModel.DcBranch1StateFlag2;
            if ((value1 & 0x0001) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State1 = "电池充满";
            }
            else if ((value1 & 0x0002) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State1 = "电池放空";
            }
            else if ((value1 & 0x0004) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State1 = "充电";
            }
            else if ((value1 & 0x0008) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State1 = "放电";
            }
            else if ((value1 & 0x0040) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State1 = "电池恒压均充";
            }


            if ((value2 & 0x0001) != 0)
            {
                pCSModel.MonitorModel.DcBranch1State2 = "启动";
            }
            else if ((value2 & 0x0001) == 0)
            {
                pCSModel.MonitorModel.DcBranch1State2 = "停止";
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
            value1 = pCSMonitorViewModel.DcBranch1CharHigh;
            value2 = pCSMonitorViewModel.DcBranch1CharLow;
            value3 = pCSMonitorViewModel.DcBranch1DisCharHigh;
            value4 = pCSMonitorViewModel.DcBranch1DisCharLow;
            pCSModel.MonitorModel.DcBranch1Char = value1 << 16 | value2;
            pCSModel.MonitorModel.DcBranch1DisChar = value3 << 16 | value4;
        }

        public bool GetDCFault()
        {
            int value1;
            int value2;
            int value3;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = pCSMonitorViewModel.AlarmStateFlagDC1;
            value2 = pCSMonitorViewModel.AlarmStateFlagDC2;
            value3 = pCSMonitorViewModel.AlarmStateFlagDC3;
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
            pCSModel.MonitorModel.FaultInfoDC = INFO;

            return colorflag;
        }



        public bool GetPDSFault()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = pCSMonitorViewModel.AlarmStateFlagPDS;
            if ((value & 0x0001) != 0) { INFO.Add("软件版本故障"); colorflag = true; } //53009 bit0
            if ((value & 0x0002) != 0) { INFO.Add("DSP初始化故障"); colorflag = true; } //bit1
            if ((value & 0x0004) != 0) { INFO.Add("BMS故障"); colorflag = true; } //bit2
            if ((value & 0x0008) != 0) { INFO.Add("紧急停机"); colorflag = true; } //bit3
            
            pCSModel.MonitorModel.FaultInfoPDS = INFO;
            return colorflag;
        }

        public bool GetDCAlarm()
        {
            int value1;
            int value2;
            bool colorflag = false;

            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value1 = pCSMonitorViewModel.AlarmStateFlagDC1;
            value2 = pCSMonitorViewModel.AlarmStateFlagDC2;

            if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高"); colorflag = true; } //bit10  AAAA
            if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); colorflag = true; } //bit12  AAAAA
            if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); colorflag = true; } //bit14  AAAAAA
            if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit15  AAAAAA

            if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); colorflag = true; } //bit9   AAAAAA
            pCSModel.MonitorModel.AlarmInfoDC= INFO;
            return colorflag;
        }

        public bool GetPDSAlarm()
        {
            int value;
            bool colorflag = false;
            ObservableCollection<string> INFO = new ObservableCollection<string>();
            value = pCSMonitorViewModel.AlarmStateFlagPDS;
            
            if ((value & 0x0010) != 0) { INFO.Add("防雷器告警"); colorflag = true; } //bit4   AAAAAAAAA
            pCSModel.MonitorModel.AlarmInfoPDS = INFO;
            return colorflag;
        }



        private void SyncBUSVolInfo()
        {
            if (IsConnected)
            {
                if (pCSModel.ParSettingModel.BUSUpperLimitVolThresh < 100 || pCSModel.ParSettingModel.BUSUpperLimitVolThresh > 900)
                {
                    MessageBox.Show("上限电压：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSUpperLimitVolThresh.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSUpperLimitVolThresh.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("上限电压：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.BUSLowerLimitVolThresh < 100 || pCSModel.ParSettingModel.BUSLowerLimitVolThresh > 900)
                {
                    MessageBox.Show("下限电压：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSLowerLimitVolThresh.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSLowerLimitVolThresh.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("下限电压：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.BUSHVolSetting < 100 || pCSModel.ParSettingModel.BUSHVolSetting > 900)
                {
                    MessageBox.Show("高压设置：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSHVolSetting.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSHVolSetting.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("高压设置：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.BUSLVolSetting < 100 || pCSModel.ParSettingModel.BUSLVolSetting > 900)
                {
                    MessageBox.Show("低压设置：请输入100-900的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSLVolSetting.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BUSLVolSetting.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("低压设置：请输入一位小数");
                    return;
                }
                modbusClient.WriteFunc(1, 53640, (ushort)(pCSModel.ParSettingModel.BUSUpperLimitVolThresh * 10));
                modbusClient.WriteFunc(1, 53641, (ushort)(pCSModel.ParSettingModel.BUSLowerLimitVolThresh * 10));
                modbusClient.WriteFunc(1, 53642, (ushort)(pCSModel.ParSettingModel.BUSHVolSetting * 10));
                modbusClient.WriteFunc(1, 53643, (ushort)(pCSModel.ParSettingModel.BUSLVolSetting * 10));
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
            if (IsConnected)
            {
                byte[] data = modbusClient.ReadFunc(53640, 4);
                pCSModel.ParSettingModel.BUSUpperLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 0) * 0.1, 2);
                pCSModel.ParSettingModel.BUSLowerLimitVolThresh = Math.Round(BitConverter.ToInt16(data, 2) * 0.1, 2);
                pCSModel.ParSettingModel.BUSHVolSetting = Math.Round(BitConverter.ToInt16(data, 4) * 0.1, 2);
                pCSModel.ParSettingModel.BUSLVolSetting = Math.Round(BitConverter.ToInt16(data, 6) * 0.1, 2);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }

        private void SyncCMTimeOut()
        {
            if (IsConnected)
            {
                if (pCSModel.ParSettingModel.BMSCMInterruptionTimeOut < 1 || pCSModel.ParSettingModel.BMSCMInterruptionTimeOut > 600)
                {
                    MessageBox.Show("BMS通信超时设置：请输入1-600的整数");
                    return;
                }
                if (pCSModel.ParSettingModel.Remote485CMInterruptonTimeOut < 1 || pCSModel.ParSettingModel.Remote485CMInterruptonTimeOut > 600)
                {
                    MessageBox.Show("远程485通信超时设置：请输入1-600的整数");
                    return;
                }
                if (pCSModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut < 1 || pCSModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut > 600)
                {
                    MessageBox.Show("远程TCP通信超时设置：请输入1-600的整数");
                    return;
                }
                modbusClient.WriteFunc(1, 56006, (ushort)(pCSModel.ParSettingModel.BMSCMInterruptionTimeOut));
                modbusClient.WriteFunc(1, 56007, (ushort)(pCSModel.ParSettingModel.Remote485CMInterruptonTimeOut));
                modbusClient.WriteFunc(1, 56008, (ushort)(pCSModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut));
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void ReadCMTimeOut()
        {
            if (IsConnected)
            {
                byte[] data = modbusClient.ReadFunc(56006, 3);
                pCSModel.ParSettingModel.BMSCMInterruptionTimeOut = BitConverter.ToUInt16(data, 0);
                pCSModel.ParSettingModel.Remote485CMInterruptonTimeOut = BitConverter.ToUInt16(data, 2);
                pCSModel.ParSettingModel.RemoteTCPCMInterruptionTimeOut = BitConverter.ToUInt16(data, 4);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void SyncDCBranchInfo()
        {

            if (IsConnected)
            {

                if (pCSModel.ParSettingModel.BTLLimitVol > 800 || pCSModel.ParSettingModel.BTLLimitVol < 30)
                {
                    MessageBox.Show("电池下限电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BTLLimitVol.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BTLLimitVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("电池下限电压：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.DischargeSTVol > 900 || pCSModel.ParSettingModel.DischargeSTVol < 30)
                {
                    MessageBox.Show("放电终止电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DischargeSTVol.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DischargeSTVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("放电终止电压：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.MultiBranchCurRegPar > 50 || pCSModel.ParSettingModel.MultiBranchCurRegPar < -50)
                {
                    MessageBox.Show("多支路电流调节参数：请输入-50到50的数");
                    return;
                }

                if (pCSModel.ParSettingModel.BatAveChVol > 800 || pCSModel.ParSettingModel.BatAveChVol < 30)
                {
                    MessageBox.Show("电池均充电压：请输入30到800的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BatAveChVol.ToString(), @"^\d+\.\d$")&!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.BatAveChVol.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("电池均充电压：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.ChCutCurrent > 250 || pCSModel.ParSettingModel.ChCutCurrent < 0)
                {
                    MessageBox.Show("充电截止电流：请输入0到250的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.ChCutCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.ChCutCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("充电截止电流：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.MaxChCurrent > 1500 || pCSModel.ParSettingModel.MaxChCurrent < 0)
                {
                    MessageBox.Show("最大充电电流：请输入0到1500的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.MaxChCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.MaxChCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("最大充电电流：请输入一位小数");
                    return;
                }

                if (pCSModel.ParSettingModel.MaxDisChCurrent > 1500 || pCSModel.ParSettingModel.MaxDisChCurrent < 0)
                {
                    MessageBox.Show("最大放电电流：请输入0到1500的数");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.MaxDisChCurrent.ToString(), @"^\d+\.\d$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.MaxDisChCurrent.ToString(), @"^\d+$"))
                {
                    MessageBox.Show("最大放电电流：请输入一位小数");
                    return;
                }



                modbusClient.WriteFunc(1, 53653, (ushort)(pCSModel.ParSettingModel.BTLLimitVol * 10));
                modbusClient.WriteFunc(1, 53655, (ushort)(pCSModel.ParSettingModel.DischargeSTVol * 10));
                modbusClient.WriteFunc(1, 53658, (ushort)pCSModel.ParSettingModel.MultiBranchCurRegPar);
                modbusClient.WriteFunc(1, 53660, (ushort)(pCSModel.ParSettingModel.BatAveChVol * 10));
                modbusClient.WriteFunc(1, 53662, (ushort)(pCSModel.ParSettingModel.ChCutCurrent * 10));
                modbusClient.WriteFunc(1, 53663, (ushort)(pCSModel.ParSettingModel.MaxChCurrent * 10));
                modbusClient.WriteFunc(1, 53664, (ushort)(pCSModel.ParSettingModel.MaxDisChCurrent * 10));


            }
            else
            {
                MessageBox.Show("请连接");
            }
        }


        private void ReadDCBranchInfo()
        {
            if (IsConnected)
            {
                byte[] data11 = modbusClient.ReadFunc(53651, 3);
                pCSModel.ParSettingModel.DCCurrentSet = Math.Round(BitConverter.ToInt16(data11, 0) * 0.1, 2);
                pCSModel.ParSettingModel.DCPowerSet = Math.Round(BitConverter.ToInt16(data11, 2) * 0.1, 2);
                pCSModel.ParSettingModel.BTLLimitVol = Math.Round(BitConverter.ToInt16(data11, 4) * 0.1, 2);

                byte[] data12 = modbusClient.ReadFunc(53655, 1);
                pCSModel.ParSettingModel.DischargeSTVol = Math.Round(BitConverter.ToInt16(data12, 0) * 0.1, 2);

                byte[] data13 = modbusClient.ReadFunc(53658, 1);
                pCSModel.ParSettingModel.MultiBranchCurRegPar = BitConverter.ToInt16(data13, 0);

                byte[] data14 = modbusClient.ReadFunc(53660, 1);
                pCSModel.ParSettingModel.BatAveChVol = Math.Round(BitConverter.ToInt16(data14, 0) * 0.1, 2);

                byte[] data15 = modbusClient.ReadFunc(53662, 3);
                pCSModel.ParSettingModel.ChCutCurrent = Math.Round(BitConverter.ToInt16(data15, 0) * 0.1, 2);
                pCSModel.ParSettingModel.MaxChCurrent = Math.Round(BitConverter.ToInt16(data15, 2) * 0.1, 2);
                pCSModel.ParSettingModel.MaxDisChCurrent = Math.Round(BitConverter.ToInt16(data15, 4) * 0.1, 2);
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }

        private void ModeSet()
        {
            if (IsConnected)
            {
                var item = PcsApi.PCSGetMonitorInfo();
                if (pCSModel.ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    modbusClient.WriteFunc(1, 53650, 0);
                    pCSModel.ParSettingModel.VisDCCur = Visibility.Visible;
                    pCSModel.ParSettingModel.VisDCPower = Visibility.Hidden;
                    pCSModel.ParSettingModel.VisDCChar = Visibility.Visible;
                }
                else if (pCSModel.ParSettingModel.ModeSet1 == "设置功率调节")
                {
                    modbusClient.WriteFunc(1, 53650, 1);
                    pCSModel.ParSettingModel.VisDCPower = Visibility.Visible;
                    pCSModel.ParSettingModel.VisDCCur = Visibility.Hidden;
                    pCSModel.ParSettingModel.VisDCChar = Visibility.Visible;
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
            if (IsConnected)
            {
                if (pCSModel.ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    if (pCSModel.ParSettingModel.DCCurrentSet > 1500 || pCSModel.ParSettingModel.DCCurrentSet < -1500)
                    {
                        MessageBox.Show("直流电流设置：请输入-1500到1500的数");
                        return;
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCCurrentSet.ToString(), @"^\d+\.\d$") == false & System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCCurrentSet.ToString(), @"^\-\d+\.\d$") == false
                        & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCCurrentSet.ToString(), @"^\d+$")& !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCCurrentSet.ToString(), @"^\-\d+$"))
                    {
                        MessageBox.Show("直流电流设置：请输入一位小数");
                        return;
                    }
                    modbusClient.WriteFunc(1, 53651, (ushort)(pCSModel.ParSettingModel.DCCurrentSet * 10));
                }
                else
                {
                    if (pCSModel.ParSettingModel.DCPowerSet > 1000 || pCSModel.ParSettingModel.DCPowerSet < -1000)
                    {
                        MessageBox.Show("直流功率设置：请输入-1000到1000的数");
                        return;
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCPowerSet.ToString(), @"^\d+\.\d$") == false & System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCPowerSet.ToString(), @"^\-\d+\.\d$") == false
                        & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCPowerSet.ToString(), @"^\d+$") & !System.Text.RegularExpressions.Regex.IsMatch(pCSModel.ParSettingModel.DCPowerSet.ToString(), @"^\-\d+$"))
                    {
                        MessageBox.Show("直流功率设置：请输入一位小数");
                        return;
                    }
                    modbusClient.WriteFunc(1, 53652, (ushort)(pCSModel.ParSettingModel.DCPowerSet * 10));
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
            onlinevalue = dCStatusViewModel.ModuleOnLineFlag;
            runvalue = dCStatusViewModel.ModuleRunFlag;
            alarmvalue = dCStatusViewModel.ModuleAlarmFlag;
            faultvalue = dCStatusViewModel.ModuleFaultFlag;

            //DC模组1状态
            if ((onlinevalue & 0x0001) != 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pCSModel.MonitorModel.Module1Status1 = "在线";
                pCSModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0001) != 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0 && (onlinevalue & 0x0001) == 0)
            {
                pCSModel.MonitorModel.Module1Status1 = "运行";
                pCSModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pCSModel.MonitorModel.Module1Status1 = "告警";
                pCSModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0001) != 0 && (onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0)
            {
                pCSModel.MonitorModel.Module1Status1 = "故障";
                pCSModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0001) == 0 && (runvalue & 0x0001) == 0 && (alarmvalue & 0x0001) == 0 && (faultvalue & 0x0001) == 0)
            {
                pCSModel.MonitorModel.Module1Status1 = "离线";
                pCSModel.MonitorModel.Module1StatusColor1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组2状态
            if ((onlinevalue & 0x0002) != 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pCSModel.MonitorModel.Module1Status2 = "在线";
                pCSModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0002) != 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0 && (onlinevalue & 0x0002) == 0)
            {
                pCSModel.MonitorModel.Module1Status2 = "运行";
                pCSModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pCSModel.MonitorModel.Module1Status2 = "告警";
                pCSModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0002) != 0 && (onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0)
            {
                pCSModel.MonitorModel.Module1Status2 = "故障";
                pCSModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0002) == 0 && (runvalue & 0x0002) == 0 && (alarmvalue & 0x0002) == 0 && (faultvalue & 0x0002) == 0)
            {
                pCSModel.MonitorModel.Module1Status2 = "离线";
                pCSModel.MonitorModel.Module1StatusColor2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组3状态
            if ((onlinevalue & 0x0004) != 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pCSModel.MonitorModel.Module1Status3 = "在线";
                pCSModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0004) != 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0 && (onlinevalue & 0x0004) == 0)
            {
                pCSModel.MonitorModel.Module1Status3 = "运行";
                pCSModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pCSModel.MonitorModel.Module1Status3 = "告警";
                pCSModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0004) != 0 && (onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0)
            {
                pCSModel.MonitorModel.Module1Status3 = "故障";
                pCSModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0004) == 0 && (runvalue & 0x0004) == 0 && (alarmvalue & 0x0004) == 0 && (faultvalue & 0x0004) == 0)
            {
                pCSModel.MonitorModel.Module1Status3 = "离线";
                pCSModel.MonitorModel.Module1StatusColor3 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组4状态
            if ((onlinevalue & 0x0008) != 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pCSModel.MonitorModel.Module1Status4 = "在线";
                pCSModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0008) != 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0 && (onlinevalue & 0x0008) == 0)
            {
                pCSModel.MonitorModel.Module1Status4 = "运行";
                pCSModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pCSModel.MonitorModel.Module1Status4 = "告警";
                pCSModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0008) != 0 && (onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0)
            {
                pCSModel.MonitorModel.Module1Status4 = "故障";
                pCSModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0008) == 0 && (runvalue & 0x0008) == 0 && (alarmvalue & 0x0008) == 0 && (faultvalue & 0x0008) == 0)
            {
                pCSModel.MonitorModel.Module1Status4 = "离线";
                pCSModel.MonitorModel.Module1StatusColor4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组5状态
            if ((onlinevalue & 0x0010) != 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pCSModel.MonitorModel.Module1Status5 = "在线";
                pCSModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0010) != 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0 && (onlinevalue & 0x0010) == 0)
            {
                pCSModel.MonitorModel.Module1Status5 = "运行";
                pCSModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pCSModel.MonitorModel.Module1Status5 = "告警";
                pCSModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0010) != 0 && (onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0)
            {
                pCSModel.MonitorModel.Module1Status5 = "故障";
                pCSModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0010) == 0 && (runvalue & 0x0010) == 0 && (alarmvalue & 0x0010) == 0 && (faultvalue & 0x0010) == 0)
            {
                pCSModel.MonitorModel.Module1Status5 = "离线";
                pCSModel.MonitorModel.Module1StatusColor5 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组6状态
            if ((onlinevalue & 0x0020) != 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pCSModel.MonitorModel.Module1Status6 = "在线";
                pCSModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0020) != 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0 && (onlinevalue & 0x0020) == 0)
            {
                pCSModel.MonitorModel.Module1Status6 = "运行";
                pCSModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pCSModel.MonitorModel.Module1Status6 = "告警";
                pCSModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0020) != 0 && (onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0)
            {
                pCSModel.MonitorModel.Module1Status6 = "故障";
                pCSModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0020) == 0 && (runvalue & 0x0020) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0020) == 0)
            {
                pCSModel.MonitorModel.Module1Status6 = "离线";
                pCSModel.MonitorModel.Module1StatusColor6 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组7状态
            if ((onlinevalue & 0x0040) != 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                pCSModel.MonitorModel.Module1Status7 = "在线";
                pCSModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0040) != 0 && (alarmvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0 && (onlinevalue & 0x0040) == 0)
            {
                pCSModel.MonitorModel.Module1Status7 = "运行";
                pCSModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (faultvalue & 0x0040) == 0)
            {
                pCSModel.MonitorModel.Module1Status7 = "告警";
                pCSModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0040) != 0 && (onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0040) == 0)
            {
                pCSModel.MonitorModel.Module1Status7 = "故障";
                pCSModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0040) == 0 && (runvalue & 0x0040) == 0 && (alarmvalue & 0x0020) == 0 && (faultvalue & 0x0040) == 0)
            {
                pCSModel.MonitorModel.Module1Status7 = "离线";
                pCSModel.MonitorModel.Module1StatusColor7 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组8状态
            if ((onlinevalue & 0x0080) != 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pCSModel.MonitorModel.Module1Status8 = "在线";
                pCSModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0080) != 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0 && (onlinevalue & 0x0080) == 0)
            {
                pCSModel.MonitorModel.Module1Status8 = "运行";
                pCSModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pCSModel.MonitorModel.Module1Status8 = "告警";
                pCSModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0080) != 0 && (onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0)
            {
                pCSModel.MonitorModel.Module1Status8 = "故障";
                pCSModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0080) == 0 && (runvalue & 0x0080) == 0 && (alarmvalue & 0x0080) == 0 && (faultvalue & 0x0080) == 0)
            {
                pCSModel.MonitorModel.Module1Status8 = "离线";
                pCSModel.MonitorModel.Module1StatusColor8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            //DC模组9状态
            if ((onlinevalue & 0x0100) != 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pCSModel.MonitorModel.Module1Status9 = "在线";
                pCSModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0100) != 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0 && (onlinevalue & 0x0100) == 0)
            {
                pCSModel.MonitorModel.Module1Status9 = "运行";
                pCSModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pCSModel.MonitorModel.Module1Status9 = "告警";
                pCSModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0100) != 0 && (onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0)
            {
                pCSModel.MonitorModel.Module1Status9 = "故障";
                pCSModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0100) == 0 && (runvalue & 0x0100) == 0 && (alarmvalue & 0x0100) == 0 && (faultvalue & 0x0100) == 0)
            {
                pCSModel.MonitorModel.Module1Status9 = "离线";
                pCSModel.MonitorModel.Module1StatusColor9 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }

            // DC模组10状态
            if ((onlinevalue & 0x0200) != 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pCSModel.MonitorModel.Module1Status10 = "在线";
                pCSModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BFFF"));
            }
            else if ((runvalue & 0x0200) != 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0 && (onlinevalue & 0x0200) == 0)
            {
                pCSModel.MonitorModel.Module1Status10 = "运行";
                pCSModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            }
            else if ((alarmvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pCSModel.MonitorModel.Module1Status10 = "告警";
                pCSModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else if ((faultvalue & 0x0200) != 0 && (onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0)
            {
                pCSModel.MonitorModel.Module1Status10 = "故障";
                pCSModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else if ((onlinevalue & 0x0200) == 0 && (runvalue & 0x0200) == 0 && (alarmvalue & 0x0200) == 0 && (faultvalue & 0x0200) == 0)
            {
                pCSModel.MonitorModel.Module1Status10 = "离线";
                pCSModel.MonitorModel.Module1StatusColor10 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A"));
            }
        }
    }
}
