using EMS.Common;
using EMS.Model;
using EMS.ViewModel;
using Modbus.Device;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace EMS.Service
{
    public class SmartMeterDataService
    {
        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            private set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnChangeState(_isConnected, _isDaqData);
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
                    OnChangeState(_isConnected, _isDaqData);
                }
            }
        }

        private Action<bool, bool> OnChangeState;
        private SerialPort SerialPortInstance;
        public Configuaration Configuaration;

        public SmartMeterDataService()
        {
            smartMeterModels = new BlockingCollection<SmartMeterModel>(new ConcurrentQueue<SmartMeterModel>(),300);
        }

        public void RegisterState(Action<bool, bool> action)
        {
            OnChangeState = action;
        }

        public void SetCommunicationConfig(Configuaration configuaration)
        {
            Configuaration = configuaration;
        }

        public bool Connect()
        {
            if (!IsConnected)
            {
                if (Open())
                {
                    IsConnected = true;
                    return true;
                }

                // 采集状态
                if (IsDaqData)
                {
                    StartDaqData();
                }
                return true;
            }
            return false;
        }

        public bool Disconnect()
        {
            if (IsConnected)
            {
                if (IsDaqData)
                {
                    StopDaqData();
                }

                if (SerialPortInstance != null)
                {
                    if (SerialPortInstance.IsOpen)
                    {
                        SerialPortInstance.Close();
                    }
                }
                IsConnected = false;
            }
            return true;
        }

        public void StopDaqData()
        {
            IsDaqData = false;
        }

        public void StartDaqData()
        {
            if (IsConnected)
            {
                if (!IsDaqData)
                {
                    IsDaqData = true;
                    Thread th = new Thread(DaqDataTh);
                    th.IsBackground = true;
                    th.Start();
                }
            }
        }

        private bool Open()
        {
            try
            {
                SerialPortInstance = new SerialPort();
                SerialPortInstance.PortName = Configuaration.SelectedCommPort;
                SerialPortInstance.BaudRate = Configuaration.SelectedBaudRate;
                SerialPortInstance.DataBits = Configuaration.SelectedDataBits;
                SerialPortInstance.StopBits = Configuaration.SelectedStopBits;
                SerialPortInstance.Parity = Configuaration.SelectedParity;
                SerialPortInstance.ReadTimeout = 100;
                SerialPortInstance.Open();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetDaqTimeSpan(int value)
        {
            DaqTimeSpan = value;
        }

        private int DaqTimeSpan = 1;
        public BlockingCollection<SmartMeterModel> smartMeterModels;
        // CS校验不计算0xFE, 0xFE, 0xFE, 0xFE
        byte[] Request_GetVoltage_A = new byte[] { 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x11, 0x04, 0x02, 0x01, 0x01, 0x00, 0xA4, 0x16 };
        byte[] Response_GetVoltage_A = new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x91, 0x08, 0x33, 0x33, 0x33, 0x33, 0x00, 0x00, 0x00, 0x00, 0x44, 0x16 };
        byte[] Request_GetVoltage_B = new byte[0];
        byte[] Response_GetVoltage_B = new byte[0];
        private void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 500);
                    // 采集数据
                    SmartMeterModel model = new SmartMeterModel();
                    var Data_Voltage_A = ReadDataForCmd(Request_GetVoltage_A, Response_GetVoltage_A.Length);
                    if(DataDecode(Data_Voltage_A, Response_GetVoltage_A, out int Voltage_A))
                    {
                        model.Voltage_A = Voltage_A;
                    }

                    var Data_Voltage_B = ReadDataForCmd(Request_GetVoltage_B, Response_GetVoltage_B.Length);
                    if (DataDecode(Data_Voltage_A, Response_GetVoltage_A, out int Voltage_B))
                    {
                        model.Voltage_B = Voltage_B;
                    }
                    smartMeterModels.TryAdd(model);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private byte[] ReadDataForCmd(byte[] Request, int num)
        {
            try
            {
                var readBytes = new byte[num];
                SerialPortInstance.Write(Request, 0, Request.Length);
                SerialPortInstance.Read(readBytes, 0, num);
                return readBytes;
            }
            catch (InvalidOperationException)
            {
                //串口断开，尝试重连
                if (!SerialPortInstance.IsOpen && !IsCommunicationProtectState)
                {
                    if (CommunicationCheck())
                    {
                        var readBytes = new byte[num];
                        SerialPortInstance.Write(Request, 0, Request.Length);
                        SerialPortInstance.Read(readBytes, 0, num);
                        return readBytes;
                    }
                }
            }
            return new byte[num];
        }

        private static int reconnectInterval = 150; // ms
        private static int maxReconnectTimes = 3;
        private int reconnectCount = 0;
        private bool IsCommunicationProtectState = false;
        private Thread CommunicationProtectTr;
        private bool CommunicationCheck()
        {
            while (true)
            {
                try
                {
                    SerialPortInstance.Open();
                    reconnectCount = 0;
                    return true;
                }
                catch (Exception)
                {
                    reconnectCount++;
                    if (reconnectCount > maxReconnectTimes)
                    {
                        IsCommunicationProtectState = true;
                        IsConnected = false;
                        CommunicationProtectTr.Start();
                        return false;
                    }
                }
            }
        }

        private static int reconnectIntervalLong = 60 * 1000 * 5; // ms
        private void CommunicationProtect()
        {
            while (!IsConnected)
            {
                Thread.Sleep(reconnectIntervalLong);
                try
                {
                    SerialPortInstance.Open();
                    IsConnected = true;
                    LogUtils.Debug("电表保护机制重连成功，设备上线");
                }
                catch (Exception)
                {
                    LogUtils.Debug("电表保护机制重连失败，继续尝试重新链接");
                    continue;
                }
            }
        }

        private bool DataDecode(byte[] data, byte[] model, out int value)
        {
            if (CheckData(model, data))
            {
                byte[] bf = new byte[] { data[data.Length - 6], data[data.Length - 5], data[data.Length - 4], data[data.Length-3] };
                value =  BitConverter.ToInt32(bf, 0);
                return true;
            }
            value = 0;
            return false;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="standardVBytes">标准模板</param>
        /// <param name="readBytes">读取到的数据</param>
        /// <returns>校验结果</returns>
        private bool CheckData(byte[] standardBytes, byte[] readBytes)
        {
            if (standardBytes.Length == readBytes.Length)
            {
                for (int i = 0; i < standardBytes.Length - 4; i++)
                {
                    if (standardBytes[i] != readBytes[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
