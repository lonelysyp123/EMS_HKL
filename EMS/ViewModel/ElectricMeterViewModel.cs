using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EMS.Common;
using EMS.Model;
using EMS.Service;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Media.Imaging;

namespace EMS.ViewModel
{
    public class ElectricMeterViewModel : ViewModelBase
    {
        #region ObservableObject

        private int _voltage_A;
        public int Voltage_A
        {
            get => _voltage_A;
            set
            {
                SetProperty(ref _voltage_A, value);
            }
        }

        private int _voltage_B;
        public int Voltage_B
        {
            get => _voltage_B;
            set
            {
                SetProperty(ref _voltage_B, value);
            }
        }

        private int _voltage_C;
        public int Voltage_C
        {
            get => _voltage_C;
            set
            {
                SetProperty(ref _voltage_C, value);
            }
        }

        private int _electric_A;
        public int Electric_A
        {
            get => _electric_A;
            set
            {
                SetProperty(ref _electric_A, value);
            }
        }

        private int _electric_B;
        public int Electric_B
        {
            get => _electric_B;
            set
            {
                SetProperty(ref _electric_B, value);
            }
        }

        private int _electric_C;
        public int Electric_C
        {
            get => _electric_C;
            set
            {
                SetProperty(ref _electric_C, value);
            }
        }

        private int _activePower_A;
        public int ActivePower_A
        {
            get => _activePower_A;
            set
            {
                SetProperty(ref _activePower_A, value);
            }
        }

        private int _activePower_B;
        public int ActivePower_B
        {
            get => _activePower_B;
            set
            {
                SetProperty(ref _activePower_B, value);
            }
        }

        private int _activePower_C;
        public int ActivePower_C
        {
            get => _activePower_C;
            set
            {
                SetProperty(ref _activePower_C, value);
            }
        }

        private int _activePower_Total;
        public int ActivePower_Total
        {
            get => _activePower_Total;
            set
            {
                SetProperty(ref _activePower_Total, value);
            }
        }

        private int _reactivePower_A;
        public int ReactivePower_A
        {
            get => _reactivePower_A;
            set
            {
                SetProperty(ref _reactivePower_A, value);
            }
        }

        private int _reactivePower_B;
        public int ReactivePower_B
        {
            get => _reactivePower_B;
            set
            {
                SetProperty(ref _reactivePower_B, value);
            }
        }

        private int _reactivePower_C;
        public int ReactivePower_C
        {
            get => _reactivePower_C;
            set
            {
                SetProperty(ref _reactivePower_C, value);
            }
        }

        private int _reactivePower_Total;
        public int ReactivePower_Total
        {
            get => _reactivePower_Total;
            set
            {
                SetProperty(ref _reactivePower_Total, value);
            }
        }

        public Configuaration Configuaration { get; set; }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            private set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                }
            }
        }

        private bool _isDaqData;
        public bool IsDaqData
        {
            get => _isDaqData;
            private set
            {
                if (_isDaqData != value)
                {
                    _isDaqData = value;
                }
            }
        }

        private bool _isRecordData = false;
        public bool IsRecordData
        {
            get => _isRecordData;
            private set
            {
                if (_isRecordData != value)
                {
                    _isRecordData = value;
                }
            }
        }

        #endregion

        public SmartMeterModel CurrentSmartMeterModel;
        private BlockingCollection<SmartMeterModel> SmartMeterModelList;
        private SmartMeterDataService service;

        public ElectricMeterViewModel()
        {
            service = new SmartMeterDataService();
            //service.RegisterState(ServiceStateCallBack);
        }

        private void ServiceStateCallBack(bool isConnected, bool isDaqData)
        {
            App.Current.Dispatcher.Invoke(() => {
                IsConnected = isConnected;
                IsDaqData = isDaqData;
            });
        }

        [RelayCommand]
        private void StopDaqData()
        {
            service.StopDaqData();
        }

        [RelayCommand]
        private void StartDaqData()
        {
            service.StartDaqData();
            Thread thread = new Thread(RefreshDataTh);
            thread.IsBackground = true;
            thread.Start();
        }

        private void RefreshDataTh()
        {
            while (IsDaqData)
            {
                //if (service.smartMeterModels.TryTake(out SmartMeterModel CurrentSmartMeterModel))
                //{
                //    var model = (SmartMeterModel)CurrentSmartMeterModel.Clone();
                //    SmartMeterModelList.Add(model);
                //    // 把数据分发给需要显示的内容
                //    App.Current.Dispatcher.Invoke(() =>
                //    {
                //        RefreshData(CurrentSmartMeterModel);
                //    });

                //    if (IsRecordData)
                //    {
                //        SaveData(CurrentSmartMeterModel);
                //    }
                //}
                //else
                //{
                //    Thread.Sleep(500);
                //}
            }
        }

        private void SaveData(SmartMeterModel currentSmartMeterModel)
        {
            // TODO
        }

        private void RefreshData(SmartMeterModel currentSmartMeterModel)
        {
            this.Voltage_A = currentSmartMeterModel.Voltage_A;
            this.Voltage_B = currentSmartMeterModel.Voltage_B;
            this.Voltage_C = currentSmartMeterModel.Voltage_A;
            this.Electric_A = currentSmartMeterModel.Electric_A;
            this.Electric_B = currentSmartMeterModel.Electric_B;
            this.Electric_C = currentSmartMeterModel.Electric_C;
            this.ActivePower_A = currentSmartMeterModel.ActivePower_A;
            this.ActivePower_B = currentSmartMeterModel.ActivePower_B;
            this.ActivePower_C = currentSmartMeterModel.ActivePower_C;
            this.ActivePower_Total = currentSmartMeterModel.ActivePower_Total;
            this.ReactivePower_A = currentSmartMeterModel.ReactivePower_A;
            this.ReactivePower_B = currentSmartMeterModel.ReactivePower_B;
            this.ReactivePower_C = currentSmartMeterModel.ReactivePower_C;
            this.ReactivePower_Total = currentSmartMeterModel.ReactivePower_Total;
        }

        [RelayCommand]
        public void CloseSerialPort()
        {
            //service.Disconnect();
        }

        [RelayCommand]
        private void OpenSerialPort()
        {
            //service.SetCommunicationConfig(Configuaration);
            //service.Connect();
        }

        public ThreePhaseValue GetThreePhaseVoltage()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = CurrentSmartMeterModel.Voltage_A;
            item.PhaseB = CurrentSmartMeterModel.Voltage_B;
            item.PhaseC = CurrentSmartMeterModel.Voltage_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseElectric()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = CurrentSmartMeterModel.Electric_A;
            item.PhaseB = CurrentSmartMeterModel.Electric_B;
            item.PhaseC = CurrentSmartMeterModel.Electric_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseActivePower()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = CurrentSmartMeterModel.ActivePower_A;
            item.PhaseB = CurrentSmartMeterModel.ActivePower_B;
            item.PhaseC = CurrentSmartMeterModel.ActivePower_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseReactivePower()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = CurrentSmartMeterModel.ReactivePower_A;
            item.PhaseB = CurrentSmartMeterModel.ReactivePower_B;
            item.PhaseC = CurrentSmartMeterModel.ReactivePower_C;
            return item;
        }

        public double GetRealPowerTotal()
        {
            return CurrentSmartMeterModel.ActivePower_Total;
        }

        public double GetReactivePowerTotal()
        {
            return CurrentSmartMeterModel.ReactivePower_Total;
        }
    }

    public struct ThreePhaseValue
    {
        public double PhaseA;
        public double PhaseB;
        public double PhaseC;
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
