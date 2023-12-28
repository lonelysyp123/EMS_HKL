using PCSTester.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using PCSTester.Common.Modbus.ModbusTCP;

namespace PCSTester.Model
{
    public class PCSModel : ViewModelBase
    {
        private bool _isConnected = false;
        public bool IsConnected { get { return _isConnected; } }

        private ModbusClient _modbusClient;
        public ModbusClient ModbusClient { get { return _modbusClient; } }

        public PCSMonitorModel MonitorModel { get; set; }
        public PCSParSettingModel ParSettingModel { get; set; }

        public void Connect(string ip, int port)
        {
            try
            {
                _modbusClient = new ModbusClient(ip, port);
                _modbusClient.Connect();
            }
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

        static byte PcsId = 0;

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