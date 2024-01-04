using EMS.Common;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using Modbus.Device;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.Windows.Markup;

namespace EMS.Service
{
    public class BMSDataService
    {
        private bool _isConnected;
        public bool IsConnected
        { 
            get=>_isConnected; 
            private set
            {
                if(_isConnected != value)
                {
                    _isConnected = value;
                    OnChangeState(this, _isConnected, _isDaqData);
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
                    OnChangeState(this, _isConnected, _isDaqData);
                }
            }
        }

        public string ID { get; private set; }
        private string IP;
        private int Port;
        private TcpClient _client;
        private ModbusMaster _master;
        private Action<object, bool, bool> OnChangeState;
        private Action<object, BatteryTotalModel> OnChangeData;

        public BMSDataService()
        {
            CommunicationProtectTr = new Thread(CommunicationProtect);
            CommunicationProtectTr.IsBackground = true;
        }

        public void RegisterState(Action<object, bool, bool> action)
        {
            OnChangeState = action;
        }

        public void RegisterState(Action<object, BatteryTotalModel> action)
        {
            OnChangeData = action;
        }

        public void SetCommunicationConfig(string ip, string port, string id)
        {
            IP = ip;
            int.TryParse(port, out Port);
            ID = id;
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
            catch(TimeoutException)
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
                    if(IsDaqData)
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

        private int DaqTimeSpan = 1;
        public void SetDaqTimeSpan(int value)
        {
            DaqTimeSpan = value;
        }

        public BlockingCollection<BatteryTotalModel> batteryTotalModels;
        private void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000);

                    byte[] BCMUData = new byte[90];
                    Array.Copy(ReadFunc(11000, 45), 0, BCMUData, 0, 90);
                    byte[] BMUIDData = new byte[48];
                    Array.Copy(ReadFunc(11045, 24), 0, BMUIDData, 0, 48);
                    byte[] BMUData = new byte[744];
                    Array.Copy(ReadFunc(10000, 120), 0, BMUData, 0, 240);
                    Array.Copy(ReadFunc(10120, 120), 0, BMUData, 240, 240);
                    Array.Copy(ReadFunc(10240, 120), 0, BMUData, 480, 240);
                    Array.Copy(ReadFunc(10360, 12), 0, BMUData, 720, 24);
                    batteryTotalModels.TryAdd(DataDecode(BCMUData, BMUIDData, BMUData));
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

        private static int reconnectInterval = 150; // ms
        private static int maxReconnectTimes = 3;
        private int reconnectCount = 0;
        private bool IsCommunicationProtectState = false;
        private Thread CommunicationProtectTr;
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
                        CommunicationProtectTr.Start();
                        return false;
                    }
                }
            }
        }

        private static int reconnectIntervalLong =60 * 1000 * 5; // ms
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

        private BatteryTotalModel DataDecode(byte[] BCMU, byte[] BMUID, byte[] BMU)
        {
            BatteryTotalModel item = new BatteryTotalModel();
            if (BCMU != null)
            {
                DataDecode_BCMU(BCMU, ref item);
            }
            if (BMU != null && BMUID != null)
            {
                DataDecode_BMU(BMU, BMUID, ref item);
            }
            return item;
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

        public void StopDaqData()
        {
            IsDaqData = false;
        }

        public int[] ReadNetInfo()
        {
            byte[] data = ReadFunc(40301, 6);
            int ipaddr1 = BitConverter.ToUInt16(data, 0);
            int ipaddr2 = BitConverter.ToUInt16(data, 2);
            int ma1 = BitConverter.ToUInt16(data, 4);
            int ma2 = BitConverter.ToUInt16(data, 6);
            int gw1 = BitConverter.ToUInt16(data, 8);
            int gw2 = BitConverter.ToUInt16(data, 10);
            return new int[] {ipaddr1, ipaddr2, ma1, ma2, gw1, gw2};
        }

        public void SyncNetInfo(DevControlViewModel viewmodel)
        {
            int ipaddr1 = (viewmodel.Address2 << 8) | viewmodel.Address1;
            int ipaddr2 = (viewmodel.Address4 << 8) | viewmodel.Address3;
            int ma1 = (viewmodel.Mask2 << 8) | viewmodel.Mask1;
            int ma2 = (viewmodel.Mask4 << 8) | viewmodel.Mask3;
            int gw1 = (viewmodel.Gateway2 << 8) | viewmodel.Gateway1;
            int gw2 = (viewmodel.Gateway4 << 8) | viewmodel.Gateway3;
            WriteFunc(40301, (ushort)ipaddr1);
            WriteFunc(40302, (ushort)ipaddr2);
            WriteFunc(40303, (ushort)ma1);
            WriteFunc(40304, (ushort)ma2);
            WriteFunc(40305, (ushort)gw1);
            WriteFunc(40306, (ushort)gw2);
        }

        /// <summary>
        /// 通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        private void WriteFunc(ushort address, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(0, address, value);
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
                throw ex;
            }
        }

        private Int32[] OpenObjs = { 0xAB00, 0xAC00, 0xAD00 };
        public void OpenChargeChannel(string SelectedChannel, string SelectedBMU)
        {
            if (int.TryParse(SelectedChannel, out int openchannel))
            {
                if (int.TryParse(SelectedBMU, out int index))
                {
                    if (index - 1 < 3)
                    {
                        UInt16 data = (UInt16)(OpenObjs[index - 1] + openchannel);
                        WriteFunc(40100, (ushort)data);
                        return;
                    }
                }
            }
            MessageBox.Show("失败");
        }

        private Int32[] CloseObjs = { 0xBB11, 0xBC11, 0xBD11 };
        public void CloseChargeChannel(string SelectedChannel, string SelectedBMU)
        {
            if(int.TryParse(SelectedChannel, out int closechannel))
            {
                if (int.TryParse(SelectedBMU, out int index))
                {
                    if (index - 1 < 3)
                    {
                        UInt16 data = (UInt16)(CloseObjs[index-1]);
                        WriteFunc(40101, (ushort)data);
                        return;
                    }
                }
            }
            MessageBox.Show("失败");
        }

        public void SelectBalancedMode(string SelectedBalanceMode)
        {
            if (SelectedBalanceMode == "自动模式")
            {
                WriteFunc(40102, 0xBC11);
            }
            else if (SelectedBalanceMode == "远程模式")
            {
                WriteFunc(40102, 0xBC22);
            }
            else
            {
                MessageBox.Show("请选择模式");
            }
        }

        public void FWUpdate()
        {
            WriteFunc(40104, 0xBBAA);
        }

        public void InNet()
        {
            WriteFunc(40103, 0xBB11);
        }

        public string[] ReadBCMUIDINFO()
        {
            byte[] data = ReadFunc(40307, 16);
            StringBuilder BCMUNameBuilder = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                char BCMUNameChar = Convert.ToChar(data[i]);
                BCMUNameBuilder.Append(BCMUNameChar);
            }

            string str1 = BCMUNameBuilder.ToString().TrimStart('0');
            StringBuilder BCMUSNameBuilder = new StringBuilder();
            for (int i = 16; i < 32; i++)
            {
                char BCMUSNameChar = Convert.ToChar(data[i]);
                BCMUSNameBuilder.Append(BCMUSNameChar);

            }
            string str2 = BCMUSNameBuilder.ToString().TrimStart('0');
            return new string[] { str1, str2 };
        }

        public void SyncBCMUIDINFO(DevControlViewModel viewmodel)
        {
            int indexSN = 0; //BCMU序列号数据序号
            int indexN = 0;//BCMU别名序号
            string BCMUFullSName = "";//补足16位的BCMU序列号
            string BCMUFullName = "";//补足16位的BCMU别名
            if (viewmodel.BCMUSName.Length < 16 || viewmodel.BCMUName.Length < 16)
            {
                BCMUFullSName = viewmodel.BCMUSName.PadLeft(16, '0');
                BCMUFullName = viewmodel.BCMUName.PadLeft(16, '0');
            }
            else
            {
                BCMUFullSName = viewmodel.BCMUSName;
                BCMUFullName = viewmodel.BCMUName;
            }
            //写BCMU序列号
            for (int i = 0; i < BCMUFullSName.Length; i++)
            {
                int asciiCode = (int)BCMUFullSName[i];
                int asciiCode2;
                if (i % 2 == 0)
                {
                    asciiCode2 = (BCMUFullSName[i + 1]) << 8;
                    int nameof = asciiCode | asciiCode2;
                    WriteFunc((ushort)(40315 + indexSN), (ushort)nameof);
                    indexSN++;
                }
            }
            //写BCMU别名
            for (int i = 0; i < BCMUFullName.Length; i++)
            {
                int asciiCode = (int)BCMUFullName[i];
                if (i % 2 == 0)
                {
                    int asciiCode2 = (BCMUFullName[i + 1]) << 8;
                    int nameof = asciiCode | asciiCode2;
                    WriteFunc((ushort)(40307 + indexN), (ushort)nameof);
                    indexN++;
                }
            }
        }

        public void SelectDataCollectionMode(string SelectedDataCollectionMode)
        {
            if (SelectedDataCollectionMode == "正常模式")
            {
                //ModbusClient.WriteFunc(40105, 0xAAAA);
            }
            else if (SelectedDataCollectionMode == "仿真模式")
            {
                WriteFunc(40105, 0xAA55);
            }
            else
            {
                MessageBox.Show("请选择正确模式");
            }
        }

        public byte[] ReadBCMUInfo()
        {
            return ReadFunc(40200, 34);
        }

        public void SyncBCMUInfo(ushort[] values)
        {
            WriteFunc(40200, values);
        }

        /// <summary>
        /// 通用批量写入函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values">写入值</param>
        public void WriteFunc(ushort address, ushort[] values)
        {
            try
            {
                _master.WriteMultipleRegisters(0, address, values);
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
                throw ex;
            }
        }
    }
}
