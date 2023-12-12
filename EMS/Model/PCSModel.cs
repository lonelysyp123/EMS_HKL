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

        /// <summary>
        /// PCS充放电，+ BUS to DC，- DC to BUS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="setvalue"></param>
        public void SetManChar(string model, double setvalue)
        {
            //注意：前置条件不该block的事就不能block
            try
            {
                if (model == "设置电流调节")
                {
                    _modbusClient.WriteFunc(PcsId, (ushort)PcsCommandAdressEnum.CharModeSet, 0);
                    _modbusClient.WriteFunc(1, 53651, (ushort)(setvalue * 10));
                }
                else
                {
                    _modbusClient.WriteFunc(1, 53650, 1);
                    _modbusClient.WriteFunc(1, 53652, (ushort)(setvalue * 10));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        

        static byte PcsId = 0;

        public enum PcsCommandAdressEnum
        {
            PCSSystemOpen=53900,
            PCSSystemClose=53901,
            PCSSystemClearFault=53903,
            CharModeSet=53650,
            CurrentValueSet=53651,
            PowerValueSet=53652,
        }
        public PCSModel()
        {
            MonitorModel = new PCSMonitorModel();

            ParSettingModel = new PCSParSettingModel();
        }
    }

}
