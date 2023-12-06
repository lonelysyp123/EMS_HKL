using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EMS.Model;
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

        public ElectricMeterViewModel()
        {
            electricityMeterModel = new ElectricityMeterModel();

            OpenSerialPortCommand = new RelayCommand(OpenSerialPort);
            CloseSerialPortCommand = new RelayCommand(CloseSerialPort);
            StartDaqDataCommand = new RelayCommand(StartDaqData);
            StopDaqDataCommand = new RelayCommand(StopDaqData);
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
                Debug.WriteLine("关闭串口");
            }
            else
            {
                Debug.WriteLine("关闭串口失败");
            }
        }

        private void OpenSerialPort()
        {
            if (electricityMeterModel.Open())
            {
                Debug.WriteLine("打开串口");
            }
            else
            {
                Debug.WriteLine("打开串口失败");
            }
        }
    }
}
