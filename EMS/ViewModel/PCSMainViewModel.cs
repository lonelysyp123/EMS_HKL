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
        private ImageSource  _daqImageSource;

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
        public PCSFaultViewModel pCSFaultViewModel;
        public PCSMonitorViewModel pCSMonitorViewModel;
        public PCSParSettingViewModel pCSParSettingViewModel;

        public ModbusClient modbusClient;

        public bool isRead = false;

        public Thread thread;



        private int DaqTimeSpan = 1;
        public RelayCommand ConnectCommand { get; set; }

        public RelayCommand StartDaqCommand { get; set; }
        public RelayCommand StopDaqCommand { get; set; }
        public RelayCommand DisConnectCommand { get; set; }


        public PCSMainViewModel()
        {
            dCStatusViewModel = new DCStatusViewModel();
            pCSFaultViewModel = new PCSFaultViewModel();
            pCSMonitorViewModel = new PCSMonitorViewModel();
            pCSParSettingViewModel = new PCSParSettingViewModel();

            ConnectCommand = new RelayCommand(Connect);
            DisConnectCommand = new RelayCommand(DisConnect);
            StartDaqCommand = new RelayCommand(StartDaq);
            StopDaqCommand = new RelayCommand(StopDaq);

            pCSMonitorViewModel.VisDCAlarm = Visibility.Hidden;
            pCSMonitorViewModel.VisPDSAlarm = Visibility.Hidden;

            MainWindowPCSConnectState = "未连接";
            MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);

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
                if (pCSParSettingViewModel.IsConnected == true)
                {
                    MessageBox.Show("已连接");
                }
                if (pCSParSettingViewModel.IsConnected == false)
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

                        pCSParSettingViewModel.IsConnected = true;
                        pCSParSettingViewModel.modbusClient = modbusClient;

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
                if (pCSParSettingViewModel.IsConnected == false)
                {
                    MessageBox.Show("请连接");
                }
                if (pCSParSettingViewModel.IsConnected == true& isRead == false)
                {
                    modbusClient.Disconnect(); 
                    pCSParSettingViewModel.IsConnected = false;

                    BitmapImage bi;
                    DirectoryInfo directory = new DirectoryInfo("./Resource/Image");
                    FileInfo[] files = directory.GetFiles("OffConnect.png");
                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(files[0].FullName, UriKind.Absolute);
                    bi.EndInit();
                    ConnectImageSource = bi;
                }
                else if (pCSParSettingViewModel.IsConnected == true & isRead == true)
                {
                    MessageBox.Show("请停止采集");
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public void DisConnect()
        {
            try
            {
                if (pCSParSettingViewModel.IsConnected == false)
                {
                    MessageBox.Show("请连接");
                }
                if (pCSParSettingViewModel.IsConnected == true& isRead == false)
                {
                    modbusClient.Disconnect(); 
                    pCSParSettingViewModel.IsConnected = false;
                }
                else if (pCSParSettingViewModel.IsConnected == true & isRead == true)
                {
                    MessageBox.Show("请停止采集");
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public void StartDaq()
        {
            if (pCSParSettingViewModel.IsConnected==false)
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

                    pCSMonitorViewModel.GetDCBranchINFO();

                    byte[] Temp = modbusClient.ReadFunc(53221, 3);
                    pCSMonitorViewModel.ModuleTemperature = Math.Round(BitConverter.ToUInt16(Temp, 0) * 0.1 - 20, 2);
                    pCSMonitorViewModel.AmbientTemperature = Math.Round(BitConverter.ToUInt16(Temp, 4) * 0.1 - 20, 2);

                    byte[] DCBranch1INFO = modbusClient.ReadFunc(53250, 10);
                    pCSMonitorViewModel.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 0) * 0.1 - 1500, 2);
                    pCSMonitorViewModel.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 2) * 0.1, 2);
                    pCSMonitorViewModel.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 4) * 0.1 - 2000, 2);
                    pCSMonitorViewModel.DcBranch1CharHigh = BitConverter.ToUInt16(DCBranch1INFO, 6);
                    pCSMonitorViewModel.DcBranch1CharLow = BitConverter.ToUInt16(DCBranch1INFO, 8);
                    pCSMonitorViewModel.DcBranch1DisCharHigh = BitConverter.ToUInt16(DCBranch1INFO, 10);
                    pCSMonitorViewModel.DcBranch1DisCharLow = BitConverter.ToUInt16(DCBranch1INFO, 12);
                    pCSMonitorViewModel.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(DCBranch1INFO, 18) * 0.1, 2);

                    pCSMonitorViewModel.EnergyCal();

                    bool AlarmColorFlagDC = pCSMonitorViewModel.GetActiveDCState();
                    bool AlarmColorFlagPDS = pCSMonitorViewModel.GetActivePDSState();

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (AlarmColorFlagDC == true)
                        {
                            pCSMonitorViewModel.VisDCAlarm = Visibility.Visible;
                            pCSMonitorViewModel.AlarmColorDC = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pCSMonitorViewModel.VisDCAlarm = Visibility.Hidden;
                        }

                        if (AlarmColorFlagPDS == true)
                        {
                            pCSMonitorViewModel.VisPDSAlarm = Visibility.Visible;
                            pCSMonitorViewModel.AlarmColorPDS = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE0000"));
                        }
                        else
                        {
                            pCSMonitorViewModel.VisPDSAlarm = Visibility.Hidden;
                        }
                        pCSMonitorViewModel.GetActivePCSControlState();
                        pCSMonitorViewModel.GetActivePCSState();
                        dCStatusViewModel.DaqDCModuleStatus();
                        pCSMonitorViewModel.GetActiveDCState();
                        pCSMonitorViewModel.GetActivePDSState();
                    });
                    Thread.Sleep(DaqTimeSpan * 1000);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
    }
}
