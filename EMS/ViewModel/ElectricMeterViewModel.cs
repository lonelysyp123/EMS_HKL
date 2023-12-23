using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EMS.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class ElectricMeterViewModel : ViewModelBase
    {
        public RelayCommand OpenSerialPortCommand { get; set; }
        public RelayCommand CloseSerialPortCommand { get; set; }
        public RelayCommand StartDaqDataCommand { get; set; }
        public RelayCommand StopDaqDataCommand { get; set; }

        public ElectricityMeterModel electricityMeterModel;
        private ILog Logger;

        public ElectricMeterViewModel()
        {
            electricityMeterModel = new ElectricityMeterModel();

            OpenSerialPortCommand = new RelayCommand(OpenSerialPort);
            CloseSerialPortCommand = new RelayCommand(CloseSerialPort);
            StartDaqDataCommand = new RelayCommand(StartDaqData);
            StopDaqDataCommand = new RelayCommand(StopDaqData);

            Logger = LogManager.GetLogger(typeof(ElectricityMeterModel));
        }

        private void StopDaqData()
        {
            electricityMeterModel.StartDaqTh();
        }

        private void StartDaqData()
        {
            electricityMeterModel.StopDaqTh();
        }

        private void CloseSerialPort()
        {
            if (electricityMeterModel.Close())
            {
                Logger.Info("Close Serial Port("+ electricityMeterModel.Configuaration.SelectedCommPort + ")");
            }
            else
            {
                Logger.Info("Close Serial Port(" + electricityMeterModel.Configuaration.SelectedCommPort + ") Failed");
            }
        }

        private void OpenSerialPort()
        {
            if (electricityMeterModel.Open())
            {
                Logger.Info("Open Serial Port(" + electricityMeterModel.Configuaration.SelectedCommPort + ")");
            }
            else
            {
                Logger.Info("Open Serial Port(" + electricityMeterModel.Configuaration.SelectedCommPort + ") Failed");
            }
        }

        public ThreePhaseValue GetThreePhaseVoltage()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = electricityMeterModel.Voltage_A;
            item.PhaseB = electricityMeterModel.Voltage_B;
            item.PhaseC = electricityMeterModel.Voltage_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseElectric()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = electricityMeterModel.Electric_A;
            item.PhaseB = electricityMeterModel.Electric_B;
            item.PhaseC = electricityMeterModel.Electric_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseActivePower()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = electricityMeterModel.ActivePower_A;
            item.PhaseB = electricityMeterModel.ActivePower_B;
            item.PhaseC = electricityMeterModel.ActivePower_C;
            return item;
        }

        public ThreePhaseValue GetThreePhaseReactivePower()
        {
            var item = new ThreePhaseValue();
            item.PhaseA = electricityMeterModel.ReactivePower_A;
            item.PhaseB = electricityMeterModel.ReactivePower_B;
            item.PhaseC = electricityMeterModel.ReactivePower_C;
            return item;
        }

        public double GetRealPowerTotal()
        {
            return electricityMeterModel.ActivePower_Total;
        }

        public double GetReactivePowerTotal()
        {
            return electricityMeterModel.ReactivePower_Total;
        }
    }
}
