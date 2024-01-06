using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Service;
using EMS.Service.impl;
using EMS.Storage.DB.Models;
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
            InitBMS();
            InitPcs();
        }

        private void InitPcs() 
        {
            List<PcsModel> pcsModels = SystemSettingService.GetPcsList();
            if (pcsModels != null && pcsModels.Count > 0) 
            {
                PcsModel pcsModel = pcsModels.Find(item => item.Id == 1);
                if (pcsModel != null) 
                { 
                    Ip_PCS = pcsModel.Ip;
                    Port_PCS = pcsModel.Port;
                    AcquisitionCycle_PCS = pcsModel.AcquisitionCycle;
                }
            }
        }

        private void InitBMS() 
        {
            List<BcmuModel> bcmuModels = SystemSettingService.GetBcmuList();
            if (bcmuModels != null && bcmuModels.Count > 0)
            {
                BcmuModel bcmuModel1 = bcmuModels.Find(item => item.Id == 1);
                if (bcmuModel1 != null)
                {
                    Ip_BCMU1 = bcmuModel1.Ip;
                    Port_BCMU1 = bcmuModel1.Port;
                    AcquisitionCycle_BCMU1 = bcmuModel1.AcquisitionCycle;
                }

                BcmuModel bcmuModel2 = bcmuModels.Find(item => item.Id == 2);
                if (bcmuModel2 != null)
                {
                    Ip_BCMU2 = bcmuModel2.Ip;
                    Port_BCMU2 = bcmuModel2.Port;
                    AcquisitionCycle_BCMU2 = bcmuModel2.AcquisitionCycle;
                }

                BcmuModel bcmuModel3 = bcmuModels.Find(item => item.Id == 3);
                if (bcmuModel3 != null)
                {
                    Ip_BCMU3 = bcmuModel3.Ip;
                    Port_BCMU3 = bcmuModel3.Port;
                    AcquisitionCycle_BCMU3 = bcmuModel3.AcquisitionCycle;
                }

                BcmuModel bcmuModel4 = bcmuModels.Find(item => item.Id == 4);
                if (bcmuModel4 != null)
                {
                    Ip_BCMU4 = bcmuModel4.Ip;
                    Port_BCMU4 = bcmuModel4.Port;
                    AcquisitionCycle_BCMU4 = bcmuModel4.AcquisitionCycle;
                }

                BcmuModel bcmuModel5 = bcmuModels.Find(item => item.Id == 5);
                if (bcmuModel5 != null)
                {
                    Ip_BCMU5 = bcmuModel5.Ip;
                    Port_BCMU5 = bcmuModel5.Port;
                    AcquisitionCycle_BCMU5 = bcmuModel5.AcquisitionCycle;
                }

                BcmuModel bcmuModel6 = bcmuModels.Find(item => item.Id == 6);
                if (bcmuModel6 != null)
                {
                    Ip_BCMU6 = bcmuModel6.Ip;
                    Port_BCMU6 = bcmuModel6.Port;
                    AcquisitionCycle_BCMU6 = bcmuModel6.AcquisitionCycle;
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
        public List<SerialPortSettingsModel> CommPorts { get; set; }
        public string SelectedCommPort { get; set; }
        public List<SerialPortSettingsModel> BaudRates { get; set; }
        public int SelectedBaudRate { get; set; }
        public List<SerialPortSettingsModel> Parities { get; set; }
        public Parity SelectedParity { get; set; }
        public List<SerialPortSettingsModel> StopBitsList { get; set; }
        public StopBits SelectedStopBits { get; set; }
        public int[] DataBits { get; set; }
        public int SelectedDataBits { get; set; }
        public int AcquisitionCycle_Ammeter {  get; set; }

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
