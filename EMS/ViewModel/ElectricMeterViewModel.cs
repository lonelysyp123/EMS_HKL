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
    }
}
