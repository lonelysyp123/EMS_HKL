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

namespace EMS.Model
{
    public class PCSModel:ViewModelBase
    {
        private bool _isConnected = false;

        public bool IsConnected { get { return _isConnected; } }

        private ModbusClient _modbusClient;
        public ModbusClient ModbusClient { get { return _modbusClient; } }
        public PCSMonitorModel MonitorModel { get; set; }
        public PCSParSettingModel ParSettingModel { get; set; }

        public void Connect(string ip, int port)
        {
            try { 
            _modbusClient = new ModbusClient(ip, port);
            _modbusClient.Connect(); }
            catch (Exception ex)
            {
                _isConnected = false;
                throw ex;
            }
            _isConnected = true;
        }

        public void Disconnect()
        {
            if (!_isConnected) return;
            _modbusClient.Disconnect();
            _isConnected = false;
        }
        public PCSModel()
        {
            MonitorModel = new PCSMonitorModel();

            ParSettingModel = new PCSParSettingModel();
        }
    }

}
