using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EMS.Common;
using EMS.Model;
using EMS.Service;
using EMS.ViewModel.NewEMSViewModel;
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

        private double _voltage_A;
        public double Voltage_A
        {
            get => _voltage_A;
            set
            {
                SetProperty(ref _voltage_A, value);
            }
        }

        private double _voltage_B;
        public double Voltage_B
        {
            get => _voltage_B;
            set
            {
                SetProperty(ref _voltage_B, value);
            }
        }

        private double _voltage_C;
        public double Voltage_C
        {
            get => _voltage_C;
            set
            {
                SetProperty(ref _voltage_C, value);
            }
        }

        private double _electric_A;
        public double Electric_A
        {
            get => _electric_A;
            set
            {
                SetProperty(ref _electric_A, value);
            }
        }

        private double _electric_B;
        public double Electric_B
        {
            get => _electric_B;
            set
            {
                SetProperty(ref _electric_B, value);
            }
        }

        private double _electric_C;
        public double Electric_C
        {
            get => _electric_C;
            set
            {
                SetProperty(ref _electric_C, value);
            }
        }

        private double _activePower_A;
        public double ActivePower_A
        {
            get => _activePower_A;
            set
            {
                SetProperty(ref _activePower_A, value);
            }
        }

        private double _activePower_B;
        public double ActivePower_B
        {
            get => _activePower_B;
            set
            {
                SetProperty(ref _activePower_B, value);
            }
        }

        private double _activePower_C;
        public double ActivePower_C
        {
            get => _activePower_C;
            set
            {
                SetProperty(ref _activePower_C, value);
            }
        }

        private double _activePower_Total;
        public double ActivePower_Total
        {
            get => _activePower_Total;
            set
            {
                SetProperty(ref _activePower_Total, value);
            }
        }

        private double _reactivePower_A;
        public double ReactivePower_A
        {
            get => _reactivePower_A;
            set
            {
                SetProperty(ref _reactivePower_A, value);
            }
        }

        private double _reactivePower_B;
        public double ReactivePower_B
        {
            get => _reactivePower_B;
            set
            {
                SetProperty(ref _reactivePower_B, value);
            }
        }

        private double _reactivePower_C;
        public double ReactivePower_C
        {
            get => _reactivePower_C;
            set
            {
                SetProperty(ref _reactivePower_C, value);
            }
        }

        private double _reactivePower_Total;
        public double ReactivePower_Total
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
            //service = new SmartMeterDataService();
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
            this.Electric_A = currentSmartMeterModel.Current_A;
            this.Electric_B = currentSmartMeterModel.Current_B;
            this.Electric_C = currentSmartMeterModel.Current_C;
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
            item.PhaseA = CurrentSmartMeterModel.Current_A;
            item.PhaseB = CurrentSmartMeterModel.Current_B;
            item.PhaseC = CurrentSmartMeterModel.Current_C;
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
}
