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

        public Thread DataAcquisitionThread { get { return pcsModel.DataAcuisitionThread; } }

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

        public bool IsConnected { get { return IsConnected; } }

        public bool IsRead { get { return IsRead; } }

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

        public DCStatusViewModel DcStatusViewModelInstance;
        public PCSMonitorViewModel PcsMonitorViewModelInstance;

        public PCSModel pcsModel;

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
            DcStatusViewModelInstance = new DCStatusViewModel();
            PcsMonitorViewModelInstance = new PCSMonitorViewModel();


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
                if (IsConnected)
                {
                    MessageBox.Show("已连接");
                }
                else
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
                if (!IsConnected)
                {
                    MessageBox.Show("请连接");
                }
                else if (IsConnected && !IsRead)
                {
                    pcsModel.Disconnect();

                    MainWindowPCSConnectState = "未连接";
                    MainWindowPCSConnectColor = new SolidColorBrush(Colors.Red);
                }
                else if (IsConnected && IsRead)
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
            if (!IsConnected)
            {
                MessageBox.Show("请连接");
            }
            else
            {
                pcsModel.StartDataAcquisition();
            }
        }

        public void StopDataAcquisition()
        {
            if (IsRead)
            {
                pcsModel.StopDataAcquisition();
            }
            else
            {
                MessageBox.Show("请开始采集");
            }
        }




        private void SyncBUSVolInfo()
        {
            if (IsConnected)
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
                pcsModel.SyncBUSVolInfo();
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
                pcsModel.ReadBUSVolInfo();
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
                pcsModel.SyncCMTimeOut();
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
                pcsModel.ReadCMTimeOut();
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
                pcsModel.SyncDCBranchInfo();
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
                pcsModel.ReadDCBranchInfo();
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
                pcsModel.ModeSet();
                if (pcsModel.ParSettingModel.ModeSet1 == "设置电流调节")
                {
                    pcsModel.ParSettingModel.VisDCCur = Visibility.Visible;
                    pcsModel.ParSettingModel.VisDCPower = Visibility.Hidden;
                    pcsModel.ParSettingModel.VisDCChar = Visibility.Visible;
                }
                else if (pcsModel.ParSettingModel.ModeSet1 == "设置功率调节")
                {
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
            if (IsConnected)
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
                }
                pcsModel.ManChar();
            }
            else
            {
                MessageBox.Show("请连接");
            }
        }



    }
}
