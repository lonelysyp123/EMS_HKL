using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Service;
using EMS.Service.impl;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class System_DevSetterPageModel:ViewModelBase
    {
        #region ObservableObject
        private string _ip_BCMU1;
		/// <summary>
		/// BCMU1-IP
		/// </summary>
		public string Ip_BCMU1
        {
			get => _ip_BCMU1;
			set
			{
				SetProperty(ref _ip_BCMU1, value);
			}
		}

        private string _ip_BCMU2;
        /// <summary>
        /// BCMU2-IP
        /// </summary>
        public string Ip_BCMU2
        {
            get => _ip_BCMU2;
            set
            {
                SetProperty(ref _ip_BCMU2, value);
            }
        }

        private string _ip_BCMU3;
        /// <summary>
        /// BCMU3-IP
        /// </summary>
        public string Ip_BCMU3
        {
            get => _ip_BCMU3;
            set
            {
                SetProperty(ref _ip_BCMU3, value);
            }
        }

        private string _ip_BCMU4;
        /// <summary>
        /// BCMU4-IP
        /// </summary>
        public string Ip_BCMU4
        {
            get => _ip_BCMU4;
            set
            {
                SetProperty(ref _ip_BCMU4, value);
            }
        }

        private string _ip_BCMU5;
        /// <summary>
        /// BCMU5-IP
        /// </summary>
        public string Ip_BCMU5
        {
            get => _ip_BCMU5;
            set
            {
                SetProperty(ref _ip_BCMU5, value);
            }
        }

        private string _ip_BCMU6;
        /// <summary>
        /// BCMU6-IP
        /// </summary>
        public string Ip_BCMU6
        {
            get => _ip_BCMU6;
            set
            {
                SetProperty(ref _ip_BCMU6, value);
            }
        }

        private int _port_BCMU1;
        /// <summary>
        /// BCMU1-端口
        /// </summary>
        public int Port_BCMU1
        {
            get => _port_BCMU1;
            set
            {
                SetProperty(ref _port_BCMU1, value);
            }
        }

        private int _port_BCMU2;
        /// <summary>
        /// BCMU2-端口
        /// </summary>
        public int Port_BCMU2
        {
            get => _port_BCMU2;
            set
            {
                SetProperty(ref _port_BCMU2, value);
            }
        }

        private int _port_BCMU3;
        /// <summary>
        /// BCMU3-端口
        /// </summary>
        public int Port_BCMU3
        {
            get => _port_BCMU3;
            set
            {
                SetProperty(ref _port_BCMU3, value);
            }
        }

        private int _port_BCMU4;
        /// <summary>
        /// BCMU4-端口
        /// </summary>
        public int Port_BCMU4
        {
            get => _port_BCMU4;
            set
            {
                SetProperty(ref _port_BCMU4, value);
            }
        }

        private int _port_BCMU5;
        /// <summary>
        /// BCMU5-端口
        /// </summary>
        public int Port_BCMU5
        {
            get => _port_BCMU5;
            set
            {
                SetProperty(ref _port_BCMU5, value);
            }
        }

        private int _port_BCMU6;
        /// <summary>
        /// BCMU6-端口
        /// </summary>
        public int Port_BCMU6
        {
            get => _port_BCMU6;
            set
            {
                SetProperty(ref _port_BCMU6, value);
            }
        }

        private int _acquisitionCycle_BCMU1;
        /// <summary>
        /// BCMU1-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU1
        {
            get => _acquisitionCycle_BCMU1;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU1, value);
            }
        }

        private int _acquisitionCycle_BCMU2;
        /// <summary>
        /// BCMU2-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU2
        {
            get => _acquisitionCycle_BCMU2;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU2, value);
            }
        }

        private int _acquisitionCycle_BCMU3;
        /// <summary>
        /// BCMU3-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU3
        {
            get => _acquisitionCycle_BCMU3;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU3, value);
            }
        }

        private int _acquisitionCycle_BCMU4;
        /// <summary>
        /// BCMU4-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU4
        {
            get => _acquisitionCycle_BCMU4;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU4, value);
            }
        }

        private int _acquisitionCycle_BCMU5;
        /// <summary>
        /// BCMU5-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU5
        {
            get => _acquisitionCycle_BCMU5;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU5, value);
            }
        }

        private int _acquisitionCycle_BCMU6;
        /// <summary>
        /// BCMU6-采集周期
        /// </summary>
        public int AcquisitionCycle_BCMU6
        {
            get => _acquisitionCycle_BCMU6;
            set
            {
                SetProperty(ref _acquisitionCycle_BCMU6, value);
            }
        }

        private string _ip_PCS;
        /// <summary>
        /// PCS-IP
        /// </summary>
        public string Ip_PCS
        {
            get => _ip_PCS;
            set
            {
                SetProperty(ref _ip_PCS, value);
            }
        }

        private int _port_PCS;
        /// <summary>
        /// PCS端口
        /// </summary>
        public int Port_PCS
        {
            get => _port_PCS;
            set
            {
                SetProperty(ref _port_PCS, value);
            }
        }

        private int _acquisitionCycle_PCS;
        /// <summary>
        /// PCS采集周期
        /// </summary>
        public int AcquisitionCycle_PCS
        {
            get => _acquisitionCycle_PCS;
            set
            {
                SetProperty(ref _acquisitionCycle_PCS, value);
            }
        }

        public Configuaration Configuaration { get; set; }
        #endregion

        #region Command
        public RelayCommand BMSConfigCommand { get; private set; }
        public RelayCommand PCSConfigCommand { get; private set; }
        public RelayCommand SmartMeterConfigCommand { get; private set; }
        public RelayCommand TimeCollatingCommand { get; private set; }
        public RelayCommand DevDataPointConfigCommand { get; private set; }

        #endregion

        public SystemSettingService SystemSettingService { get; set; }

        public System_DevSetterPageModel()
        {
            BMSConfigCommand = new RelayCommand(BMSConfig);
            PCSConfigCommand = new RelayCommand(PCSConfig);
            SmartMeterConfigCommand = new RelayCommand(SmartMeterConfig);
            TimeCollatingCommand = new RelayCommand(TimeCollating);
            DevDataPointConfigCommand = new RelayCommand(DevDataPointConfig);
            SystemSettingService = new SystemSettingService();
            var items = SystemSettingService.GetBcmu();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id.ToString() == "1")
                {
                    _ip_BCMU1 = items[i].Ip;
                    _port_BCMU1 = items[i].Port;
                    _acquisitionCycle_BCMU1 = items[i].AcquisitionCycle;
                }
                else if (items[i].Id.ToString() == "2")
                {
                    _ip_BCMU2 = items[i].Ip;
                    _port_BCMU2 = items[i].Port;
                    _acquisitionCycle_BCMU2 = items[i].AcquisitionCycle;
                }
                else if (items[i].Id.ToString() == "3")
                {
                    _ip_BCMU3 = items[i].Ip;
                    _port_BCMU3 = items[i].Port;
                    _acquisitionCycle_BCMU3 = items[i].AcquisitionCycle;
                }
                else if (items[i].Id.ToString() == "4")
                {
                    _ip_BCMU4 = items[i].Ip;
                    _port_BCMU4 = items[i].Port;
                    _acquisitionCycle_BCMU4 = items[i].AcquisitionCycle;
                }
                else if (items[i].Id.ToString() == "5")
                {
                    _ip_BCMU5 = items[i].Ip;
                    _port_BCMU5 = items[i].Port;
                    _acquisitionCycle_BCMU5 = items[i].AcquisitionCycle;
                }
                else if (items[i].Id.ToString() == "6")
                {
                    _ip_BCMU6 = items[i].Ip;
                    _port_BCMU6 = items[i].Port;
                    _acquisitionCycle_BCMU6 = items[i].AcquisitionCycle;
                }
            }
        }

        private void BMSConfig()
        {
            SystemSettingService.AddBcmu(1, _ip_BCMU1, _port_BCMU1, _acquisitionCycle_BCMU1);
            SystemSettingService.AddBcmu(2, _ip_BCMU2, _port_BCMU2, _acquisitionCycle_BCMU2);
            SystemSettingService.AddBcmu(3, _ip_BCMU3, _port_BCMU3, _acquisitionCycle_BCMU3);
            SystemSettingService.AddBcmu(4, _ip_BCMU4, _port_BCMU4, _acquisitionCycle_BCMU4);
            SystemSettingService.AddBcmu(5, _ip_BCMU5, _port_BCMU5, _acquisitionCycle_BCMU5);
            SystemSettingService.AddBcmu(6, _ip_BCMU6, _port_BCMU6, _acquisitionCycle_BCMU6);
        }

        private void PCSConfig()
        {
            SystemSettingService.AddPcs(1, _ip_PCS, _port_PCS, _acquisitionCycle_PCS);
        }

        private void SmartMeterConfig()
        {

        }

        private void TimeCollating()
        {
             
        }

        private void DevDataPointConfig()
        {

        }
    }

    public class Configuaration
    {
        public List<SerialPortSettingsModel> CommPorts { get; private set; }
        public string SelectedCommPort { get; set; }
        public List<SerialPortSettingsModel> BaudRates { get; private set; }
        public int SelectedBaudRate { get; set; }
        public List<SerialPortSettingsModel> Parities { get; private set; }
        public Parity SelectedParity { get; set; }
        public List<SerialPortSettingsModel> StopBitsList { get; private set; }
        public StopBits SelectedStopBits { get; set; }
        public int[] DataBits { get; private set; }
        public int SelectedDataBits { get; set; }
        public int AcquisitionCycle_Ammeter {  get; private set; }

        public Configuaration()
        {
            CommPorts = SerialPortSettingsModel.Instance.getCommPorts();
            BaudRates = SerialPortSettingsModel.Instance.getBaudRates();
            Parities = SerialPortSettingsModel.Instance.getParities();
            StopBitsList = SerialPortSettingsModel.Instance.getStopBits();
            DataBits = new int[] { 4, 5, 6, 7, 8 };
        }
    }
}
