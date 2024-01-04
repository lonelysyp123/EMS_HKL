using EMS.Common;
using EMS.Model;
using Modbus.Device;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Service
{
    public class ModbusTCPDataService<TModel> : IDataService<TModel> where TModel : class, new()
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

        private string IP;
        private int Port;
        private TcpClient _client;
        private ModbusMaster _master;
        private Action<bool, bool> OnChangeState;

        public ModbusTCPDataService()
        {
            Models = new BlockingCollection<TModel>(new ConcurrentQueue<TModel>(), 300);
        }

        /// <summary>
        /// 注册链接状态，采集状态的回调函数
        /// </summary>
        /// <param name="action">回调函数</param>
        public void RegisterState(Action<bool, bool> action)
        {
            OnChangeState = action;
        }

        public void SetCommunicationConfig(string ip, string port)
        {
            IP = ip;
            int.TryParse(port, out Port);
        }

        public bool Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    _client = new TcpClient();
                    _client.Connect(IPAddress.Parse(IP), Port);
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;

                    // 采集状态
                    if (IsDaqData)
                    {
                        StartDaqData();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
            }
            return false;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                if (!IsConnected)
                {
                    _client = new TcpClient();
                    await _client.ConnectAsync(IPAddress.Parse(IP), Port);
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;

                    // 采集状态
                    if (IsDaqData)
                    {
                        StartDaqData();
                    }

                    return true;
                }
            }
            catch (SocketException)
            {
                LogUtils.Error("未应答");
            }
            catch (TimeoutException)
            {
                LogUtils.Error("TCP链接超时");
            }
            return false;
        }

        public bool Disconnect()
        {
            try
            {
                if (IsConnected)
                {
                    if (IsDaqData)
                    {
                        StopDaqData();
                    }
                    _master.Transport.Dispose();
                    _client.Close();
                    _client.Dispose();
                    IsConnected = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
            }
            return false;
        }

        public TModel GetNextData()
        {
            if(Models.TryTake(out TModel model))
            {
                return model;
            }
            return null;
        }

        public void SetDaqTimeSpan(int interval)
        {
            DaqTimeSpan = interval;
        }

        private int DaqTimeSpan = 1;
        private BlockingCollection<TModel> Models;
        private static int reconnectIntervalLong = 60 * 1000 * 5; // ms
        private static int reconnectInterval = 150; // ms
        private static int maxReconnectTimes = 3;
        private int reconnectCount = 0;
        private bool IsCommunicationProtectState = false;
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

        public void StopDaqData()
        {
            IsDaqData = false;
        }

        public byte[] SpecificPointsRead(ushort address, ushort length)
        {
            return ReadFunc(address, length);
        }

        public bool SpecificPointsWrite(ushort address, ushort[] value)
        {
            bool res = false;
            if (value != null && value.Length > 0)
            {
                if (value.Length > 1)
                {
                    res = WriteFunc(address, value);
                }
                else
                {
                    res = WriteFunc(address, value[0]);
                }
            }
            return res;
        }

        private void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 500);

                    byte[] BCMUData = new byte[90];
                    Array.Copy(ReadFunc(11000, 45), 0, BCMUData, 0, 90);
                    byte[] BMUIDData = new byte[48];
                    Array.Copy(ReadFunc(11045, 24), 0, BMUIDData, 0, 48);
                    byte[] BMUData = new byte[744];
                    Array.Copy(ReadFunc(10000, 120), 0, BMUData, 0, 240);
                    Array.Copy(ReadFunc(10120, 120), 0, BMUData, 240, 240);
                    Array.Copy(ReadFunc(10240, 120), 0, BMUData, 480, 240);
                    Array.Copy(ReadFunc(10360, 12), 0, BMUData, 720, 24);
                    Models.TryAdd(DataDecode(BCMUData, BMUIDData, BMUData));
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 通用读取函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="num">读取位数</param>
        /// <returns>读取值</returns>
        private byte[] ReadFunc(ushort address, ushort num)
        {
            try
            {
                ushort[] holding_register = _master.ReadHoldingRegisters(0, address, num);
                byte[] ret = new byte[holding_register.Length * 2];
                for (int i = 0; i < holding_register.Length; i++)
                {
                    var objs = BitConverter.GetBytes(holding_register[i]);
                    ret[i * 2] = objs[0];
                    ret[i * 2 + 1] = objs[1];
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtils.Warn("读取数据失败", ex);
                if (!_client.Connected && !IsCommunicationProtectState)
                {
                    if (CommunicationCheck())
                    {
                        return ReadFunc(address, num);
                    }
                }

                return new byte[num * 2];
            }
        }

        /// <summary>
        /// 通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        private bool WriteFunc(ushort address, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(0, address, value);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 通用批量写入函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values">写入值</param>
        public bool WriteFunc(ushort address, ushort[] values)
        {
            try
            {
                _master.WriteMultipleRegisters(0, address, values);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
                return false;
            }
        }

        private TModel DataDecode(byte[] BCMU, byte[] BMUID, byte[] BMU)
        {
            TModel item = new TModel();
            if (BCMU != null)
            {
                //DataDecode_BCMU(BCMU, ref item);
            }
            if (BMU != null && BMUID != null)
            {
                //DataDecode_BMU(BMU, BMUID, ref item);
            }
            return item;
        }

        private void CommunicationProtect()
        {
            while (!IsConnected)
            {
                Thread.Sleep(reconnectIntervalLong);
                if (_client.ConnectAsync(IPAddress.Parse(IP), Port).Wait(reconnectInterval))
                {
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;
                    LogUtils.Debug("保护机制重连成功，设备上线");
                }
                else
                {
                    LogUtils.Debug("保护机制重连失败");
                }
            }
        }

        private bool CommunicationCheck()
        {
            while (true)
            {
                if (_client.ConnectAsync(IPAddress.Parse(IP), Port).Wait(reconnectInterval))
                {
                    _master = ModbusIpMaster.CreateIp(_client);
                    reconnectCount = 0;
                    return true;
                }
                else
                {
                    reconnectCount++;
                    if (reconnectCount > maxReconnectTimes)
                    {
                        IsCommunicationProtectState = true;
                        IsConnected = false;
                        Thread tr = new Thread(CommunicationProtect);
                        tr.IsBackground = true;
                        tr.Start();
                        return false;
                    }
                }
            }
        }

        private void DataDecode_BMU(byte[] obj1, byte[] obj2, ref BatteryTotalModel total)
        {
            for (int i = 0; i < total.Series.Count; i++)
            {
                total.Series[i].AlarmStateFlagBMU = BitConverter.ToUInt16(obj1, (336 + i) * 2);
                total.Series[i].FaultyStateFlagBMU = BitConverter.ToUInt16(obj1, (339 + i) * 2);
                total.Series[i].ChargeChannelState = BitConverter.ToUInt16(obj1, (342 + i) * 2);
                total.Series[i].ChargeCapacitySum = BitConverter.ToUInt16(obj1, (345 + i) * 2) * 0.01;
                total.Series[i].MinVoltage = BitConverter.ToInt16(obj1, (348 + i * 8) * 2) * 0.001;
                total.Series[i].MaxVoltage = BitConverter.ToInt16(obj1, (349 + i * 8) * 2) * 0.001;
                total.Series[i].MinVoltageIndex = BitConverter.ToUInt16(obj1, (350 + i * 8) * 2);
                total.Series[i].MaxVoltageIndex = BitConverter.ToUInt16(obj1, (351 + i * 8) * 2);
                total.Series[i].MinTemperature = (BitConverter.ToInt16(obj1, (352 + i * 8) * 2) - 2731) * 0.1;
                total.Series[i].MaxTemperature = (BitConverter.ToInt16(obj1, (353 + i * 8) * 2) - 2731) * 0.1;
                total.Series[i].MinTemperatureIndex = BitConverter.ToUInt16(obj1, (354 + i * 8) * 2);
                total.Series[i].MaxTemperatureIndex = BitConverter.ToUInt16(obj1, (355 + i * 8) * 2);
                total.Series[i].ChargeChannelStateNumber = GetSetBitPositions(total.Series[i].ChargeChannelState).ToString();

                // BMUID
                byte[] BMUIDArray = new byte[16];
                Array.Copy(obj2, 16 * i, BMUIDArray, 0, 16);
                int ID1 = BitConverter.ToInt16(BMUIDArray, 0);
                StringBuilder BMUNameBuilder = new StringBuilder();
                for (int k = 0; k < 16; k++)
                {
                    char BMUIDChar = Convert.ToChar(BMUIDArray[k]);
                    BMUNameBuilder.Append(BMUIDChar);
                }
                total.Series[i].BMUID = BMUNameBuilder.ToString();

                for (int j = 0; j < total.Series[i].Batteries.Count; j++)
                {
                    total.Series[i].Batteries[j].Voltage = BitConverter.ToInt16(obj1, (j + i * 16) * 2) * 0.001;
                    total.Series[i].Batteries[j].Temperature1 = (BitConverter.ToInt16(obj1, (48 + j * 2 + i * 32) * 2) - 2731) * 0.1;
                    total.Series[i].Batteries[j].Temperature2 = (BitConverter.ToInt16(obj1, (48 + j * 2 + 1 + i * 32) * 2) - 2731) * 0.1;
                    total.Series[i].Batteries[j].SOC = BitConverter.ToUInt16(obj1, (144 + j + i * 16) * 2) * 0.1;
                    total.Series[i].Batteries[j].SOH = BitConverter.ToUInt16(obj1, (192 + j + i * 16) * 2);
                    total.Series[i].Batteries[j].Resistance = BitConverter.ToUInt16(obj1, (240 + j + i * 16) * 2);
                    total.Series[i].Batteries[j].Capacity = BitConverter.ToUInt16(obj1, (288 + j + i * 16) * 2) * 0.1;
                    total.Series[i].Batteries[j].BatteryNumber = j + 1;
                }
            }
        }

        private void DataDecode_BCMU(byte[] obj, ref BatteryTotalModel total)
        {
            total.TotalVoltage = BitConverter.ToInt16(obj, 0) * 0.1;
            total.TotalCurrent = (BitConverter.ToInt16(obj, 2) * 0.1);
            total.TotalSOC = BitConverter.ToUInt16(obj, 4) * 0.1;
            total.TotalSOH = BitConverter.ToUInt16(obj, 6) * 0.1;
            total.AverageTemperature = (BitConverter.ToInt16(obj, 8) - 2731) * 0.1;
            total.MinVoltage = BitConverter.ToInt16(obj, 10) * 0.001;
            total.MaxVoltage = BitConverter.ToInt16(obj, 12) * 0.001;
            total.MinVoltageIndex = BitConverter.ToUInt16(obj, 14);
            total.MaxVoltageIndex = BitConverter.ToUInt16(obj, 16);
            total.MinTemperature = (BitConverter.ToInt16(obj, 18) - 2731) * 0.1;
            total.MaxTemperature = (BitConverter.ToInt16(obj, 20) - 2731) * 0.1;
            total.MinTemperatureIndex = BitConverter.ToUInt16(obj, 22);
            total.MaxTemperatureIndex = BitConverter.ToUInt16(obj, 24);
            total.BatteryCycles = BitConverter.ToInt16(obj, 26);
            total.HWVersionBCMU = BitConverter.ToInt16(obj, 28);
            total.VersionSWBCMU = BitConverter.ToInt16(obj, 34);
            total.BatteryCount = BitConverter.ToUInt16(obj, 38);
            total.StateBCMU = BitConverter.ToInt16(obj, 48);
            total.IResistanceRP = BitConverter.ToInt16(obj, 50);
            total.IResistanceRN = BitConverter.ToInt16(obj, 52);
            total.DCVoltage = BitConverter.ToInt16(obj, 54);
            total.VolContainerTemperature1 = (BitConverter.ToUInt16(obj, 56) - 2731) * 0.1;
            total.VolContainerTemperature2 = (BitConverter.ToUInt16(obj, 58) - 2731) * 0.1;
            total.VolContainerTemperature3 = (BitConverter.ToUInt16(obj, 60) - 2731) * 0.1;
            total.VolContainerTemperature4 = (BitConverter.ToUInt16(obj, 62) - 2731) * 0.1;
            total.AlarmStateBCMUFlag1 = BitConverter.ToUInt16(obj, 64);
            total.AlarmStateBCMUFlag2 = BitConverter.ToUInt16(obj, 66);
            total.AlarmStateBCMUFlag3 = BitConverter.ToUInt16(obj, 68);
            total.FaultStateBCMUFlag1 = BitConverter.ToUInt16(obj, 70);
            total.BatMaxChgPower = BitConverter.ToUInt16(obj, 72) * 0.01;
            total.BatMaxDischgPower = BitConverter.ToUInt16(obj, 74) * 0.01;
            total.OneChgCoulomb = BitConverter.ToUInt16(obj, 76) * 0.01;
            total.OneDischgCoulomb = BitConverter.ToUInt16(obj, 78) * 0.01;
            total.TotalChgCoulomb = BitConverter.ToUInt16(obj, 80) * 0.01;
            total.TotalDischgCoulomb = BitConverter.ToUInt16(obj, 82) * 0.01;
            total.RestCoulomb = BitConverter.ToUInt16(obj, 84) * 0.01;
            total.MaxVolDiff = BitConverter.ToUInt16(obj, 86) * 0.01;
            total.AvgVol = BitConverter.ToUInt16(obj, 88) * 0.01;
        }

        private int GetSetBitPositions(UInt16 num)
        {
            int[] setBits = new int[16];
            int bitPosition = 0;
            while (num > 0)
            {
                if ((num & 1) == 1)
                {
                    setBits[bitPosition] = 1;
                }
                num >>= 1;
                bitPosition++;
            }
            return bitPosition;
        }


    }
}
