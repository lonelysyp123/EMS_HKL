using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using EMS.Common.Modbus.ModbusTCP;
using System.Windows.Media.Imaging;
using OxyPlot;

namespace EMS.Model
{
    public class PCSModel : ViewModelBase
    {
        /// <summary>
        /// 采集状态图片
        /// </summary>
        private BitmapImage _daqImageSource;
        public BitmapImage DaqImageSource
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
        private BitmapImage _connectImageSource;
        public BitmapImage ConnectImageSource
        {
            get => _connectImageSource;
            set
            {
                SetProperty(ref _connectImageSource, value);
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        private bool _isConnected = false;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    ConnectImageChange(value);
                }
            }
        }

        /// <summary>
        /// 采集状态
        /// </summary>
        private bool _isRead;

        public bool IsRead
        {
            get => _isRead;
            set
            {
                if (_isRead != value)
                {
                    _isRead = value;
                    DaqImageChange(value);
                }
            }
        }

        private ModbusClient _modbusClient;
        public ModbusClient ModbusClient { get { return _modbusClient; } }

        public PCSMonitorModel MonitorModel { get; set; }
        public PCSParSettingModel ParSettingModel { get; set; }

        /// <summary>
        /// 连接图标更改
        /// </summary>
        /// <param name="isconnect"></param>
        public void ConnectImageChange(bool isconnect)
        {
            if (isconnect) 
            {
                ConnectImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OnConnect.png"));
            }
            else
            {
                ConnectImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/OffConnect.png"));
            }
        }

        /// <summary>
        /// 采集图标更改
        /// </summary>
        /// <param name="isread"></param>
        public void DaqImageChange(bool isread)
        {
            if (isread)
            {
                DaqImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/play.png"));
            }
            else
            {
                DaqImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/pause.png"));
            }
        }

        public void Connect(string ip, int port)
        {
            try
            {
                _modbusClient = new ModbusClient(ip, port);
                _modbusClient.Connect();
            }
            catch (Exception ex)
            {
                IsConnected = false;
                throw ex;
            }
            IsConnected = true;
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
            _modbusClient.WriteFunc(PcsId, valueAddress, modeValue);
        }
        /// <summary>
        /// PCS系统开机
        /// </summary>
        public void PCSOpen()
        {
            try
            {
                _modbusClient.WriteFunc(PcsId, (ushort)PcsCommandAdressEnum.PCSSystemOpen, 1);
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
                _modbusClient.WriteFunc(PcsId, (ushort)PcsCommandAdressEnum.PCSSystemClose, 1);
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
                _modbusClient.WriteFunc(PcsId, (ushort)PcsCommandAdressEnum.PCSSystemClearFault, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte PcsId = 0;
        
        public PCSModel()
        {
            MonitorModel = new PCSMonitorModel();

            ParSettingModel = new PCSParSettingModel();
        }
    }
    public enum PcsCommandAdressEnum
    {
        PCSSystemOpen = 53900,
        PCSSystemClose = 53901,
        PCSSystemClearFault = 53903,
        CharModeSet = 53650,
        CurrentValueSet = 53651,
        PowerValueSet = 53652,
    }
}