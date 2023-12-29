using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EMS.Common;
using System.Collections.Concurrent;
using EMS.Model;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace EMS.Service
{
    public class PCSDataService
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

        public static byte PcsId = 0;


        private PCSModel pcsModel;

        public PCSDataService()
        {
            CommunicationProtectTr = new Thread(CommunicationProtect);
            CommunicationProtectTr.IsBackground = true;
        }

        public void RegisterState(Action<bool, bool> action)
        {
            OnChangeState = action;
        }

        public void SetCommunicationConfig(string ip, string port, PCSModel obj)
        {
            IP = ip;
            int.TryParse(port, out Port);
            pcsModel = obj;
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
                    StartDaqData();
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
                    IsDaqData = false;
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
                IsDaqData = true;
                Thread th = new Thread(DaqDataTh);
                th.IsBackground = true;
                th.Start();
            }
        }

        private int DaqTimeSpan = 1;

        private void DaqDataTh()
        {
            while (IsConnected)
            {
                if (!IsDaqData)
                {
                    break;
                }
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000);

                    byte[] dcState = ReadFunc(53026, 7);
                    byte[] pcsData = ReadFunc(53005, 10);
                    byte[] Temp = ReadFunc(53221, 3);
                    byte[] DCBranch1INFO = ReadFunc(53250, 10);
                    byte[] SerialNumber = ReadFunc(53579, 15);
                    pcsModel = DataDecode(dcState, pcsData, Temp, DCBranch1INFO, SerialNumber);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }


        private PCSModel DataDecode(byte[] dcstate, byte[] pcsdata, byte[]temp, byte[] dcbranch1info, byte[] serialnumber)
        {
            PCSModel item = new PCSModel();
            if (dcstate !=null)
            {
                item.dcStatusModel.ModuleOnLineFlag = BitConverter.ToUInt16(dcstate, 0);
                item.dcStatusModel.ModuleRunFlag = BitConverter.ToUInt16(dcstate, 4);
                item.dcStatusModel.ModuleAlarmFlag = BitConverter.ToUInt16(dcstate, 8);
                item.dcStatusModel.ModuleFaultFlag = BitConverter.ToUInt16(dcstate, 12);
            }
            if (pcsdata != null)
            {
                item.MonitorModel.AlarmStateFlagDC1 = BitConverter.ToUInt16(pcsdata, 0);
                item.MonitorModel.AlarmStateFlagDC2 = BitConverter.ToUInt16(pcsdata, 4);
                item.MonitorModel.AlarmStateFlagDC3 = BitConverter.ToUInt16(pcsdata, 6);
                item.MonitorModel.AlarmStateFlagPDS = BitConverter.ToUInt16(pcsdata, 8);
                item.MonitorModel.ControlStateFlagPCS = BitConverter.ToUInt16(pcsdata, 10);
                item.MonitorModel.StateFlagPCS = BitConverter.ToUInt16(pcsdata, 12);
                item.MonitorModel.DcBranch1StateFlag1 = BitConverter.ToUInt16(pcsdata, 16);
                item.MonitorModel.DcBranch1StateFlag2 = BitConverter.ToUInt16(pcsdata, 18);
            }
            if (temp != null)
            {
                item.MonitorModel.ModuleTemperature = Math.Round(BitConverter.ToUInt16(temp, 0) * 0.1 - 20, 2);
                item.MonitorModel.AmbientTemperature = Math.Round(BitConverter.ToUInt16(temp, 4) * 0.1 - 20, 2);
            }
            if (dcbranch1info!=null)
            {
                item.MonitorModel.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(dcbranch1info, 0) * 0.1 - 1500, 2);
                item.MonitorModel.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(dcbranch1info, 2) * 0.1, 2);
                item.MonitorModel.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(dcbranch1info, 4) * 0.1 - 2000, 2);
                item.MonitorModel.DcBranch1CharHigh = BitConverter.ToUInt16(dcbranch1info, 6);
                item.MonitorModel.DcBranch1CharLow = BitConverter.ToUInt16(dcbranch1info, 8);
                item.MonitorModel.DcBranch1DisCharHigh = BitConverter.ToUInt16(dcbranch1info, 10);
                item.MonitorModel.DcBranch1DisCharLow = BitConverter.ToUInt16(dcbranch1info, 12);
                item.MonitorModel.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(dcbranch1info, 18) * 0.1, 2);
            }
            if (serialnumber!=null)
            {
                item.MonitorModel.SNAdress = new ushort[11];
                for (int i = 0; i < 11; i++)
                {
                    item.MonitorModel.SNAdress[i] = BitConverter.ToUInt16(serialnumber, 2 * i);
                }
                item.MonitorModel.MonitorSoftCode = BitConverter.ToUInt16(serialnumber, 22);
                item.MonitorModel.DcSoftCode = BitConverter.ToUInt16(serialnumber, 26);
                item.MonitorModel.U2SoftCode = BitConverter.ToUInt16(serialnumber, 28);
            }
            return item;
        }

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

        private static int reconnectIntervalLong = 60 * 1000 * 5; // ms
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

        /// <summary>
        /// 通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        public bool WriteFunc(byte slave, ushort address, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(slave, address, value);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex.ToString());
                return false;
            }
        }

        public bool WriteFunc(byte slave, PcsCommandAdressEnum address, int value)
        {
            return WriteFunc(slave, (ushort)address, (ushort)value);
        }

        public void SyncBUSVolInfo(ushort[] busvolvalues)
        {
            WriteFunc(PcsId, PcsCommandAdressEnum.HigherVolThreshold, busvolvalues[1] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.LowerVolThreshold, busvolvalues[2] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.HigherVolSetting, busvolvalues[3] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.LowerVolSetting, busvolvalues[4] * 10);
        }

        public byte[] ReadBUSVolInfo()
        {
            return ReadFunc(53640, 4);
        }

        //public byte[] ReadCMTimeOut()
        //{
        //    return ReadFunc(56006, 3);
        //}

        //public void SyncCMTimeOut()
        //{
        //    //WriteFunc(PcsId, 56006, (ushort)(ParSettingModel.BMSCMInterruptionTimeOut));
        //    //WriteFunc(PcsId, 56007, (ushort)(ParSettingModel.Remote485CMInterruptonTimeOut));
        //    WriteFunc(PcsId, PcsCommandAdressEnum.RemoteTCPCMInterruptionTimeOut, (ushort)(ParSettingModel.RemoteTCPCMInterruptionTimeOut));
        //}

        /// <summary>
        /// 同步DC侧分支值
        /// </summary>
        /// <param name="dcbranch1values">DC侧支路分支值数组</param>
        public void SyncDCBranchInfo(ushort[] dcbranch1values)
        {
            WriteFunc(PcsId, PcsCommandAdressEnum.BatteryLowerVolThreshold, dcbranch1values[1] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.EndOfDischargeVol, dcbranch1values[2] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.MutiStrCurRegulationPar, dcbranch1values[3]);
            WriteFunc(PcsId, PcsCommandAdressEnum.BatteryToppingCharVol, dcbranch1values[4] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.EndOfCharCur, dcbranch1values[5] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.MaxCharCur, dcbranch1values[6] * 10);
            WriteFunc(PcsId, PcsCommandAdressEnum.MaxDischarCur, dcbranch1values[7] * 10);
        }

        public byte[] ReadDCBranchInfo()
        {
             return ReadFunc(53651, 14);
        }

        /// <summary>
        /// 充放电模式设置
        /// </summary>
        /// <param name="pcsmancharmodelset">设置模式</param>
        public void ModeSet(string pcsmancharmodelset)
        {
            if (pcsmancharmodelset == "设置电流调节")
            {
                WriteFunc(PcsId, PcsCommandAdressEnum.CharModeSet, 0);
            }
            else if (pcsmancharmodelset == "设置功率调节")
            {
                WriteFunc(PcsId, PcsCommandAdressEnum.CharModeSet, 1);
            }
        }

        /// <summary>
        /// PCS手动充放电
        /// </summary>
        /// <param name="pcsmancharset">设置值</param>
        /// <param name="pcsmancharmodelset">设置模式</param>
        public void ManChar(double pcsmancharset,string pcsmancharmodelset)
        {
            if (pcsmancharmodelset == "设置电流调节")
            {
                WriteFunc(PcsId, PcsCommandAdressEnum.CurrentValueSet, (ushort)(pcsmancharset * 10));
            }
            else
            {
                WriteFunc(PcsId, PcsCommandAdressEnum.PowerValueSet, (ushort)(pcsmancharset * 10));
            }
        }
    }
    public enum PcsCommandAdressEnum
    {
        [Description("系统开机")]
        PCSSystemOpen = 53900,
        [Description("系统关机")]
        PCSSystemClose = 53901,
        [Description("系统清除故障")]
        PCSSystemClearFault = 53903,
        [Description("直流控制模式")]
        CharModeSet = 53650,
        [Description("直流电流设置")]
        CurrentValueSet = 53651,
        [Description("直流功率设置")]
        PowerValueSet = 53652,
        [Description("BUS上限电压")]
        HigherVolThreshold = 53640,
        [Description("BUS下限电压")]
        LowerVolThreshold = 53641,
        [Description("BUS高压设置")]
        HigherVolSetting = 53642,
        [Description("BUS低压设置")]
        LowerVolSetting = 53643,
        [Description("DC侧支路1：电池下限电压")]
        BatteryLowerVolThreshold = 53653,
        [Description("DC侧支路1：放电终止电压")]
        EndOfDischargeVol = 53655,
        [Description("DC侧支路1：多支路电流调节参数")]
        MutiStrCurRegulationPar = 53658,
        [Description("DC侧支路1：电池均充电压")]
        BatteryToppingCharVol = 53660,
        [Description("DC侧支路1：充电截止电流")]
        EndOfCharCur = 53662,
        [Description("DC侧支路1：最大充电电流")]
        MaxCharCur = 53663,
        [Description("DC侧支路1：最大放电电流")]
        MaxDischarCur = 53664,
        [Description("远程TCP通讯超时设置")]
        RemoteTCPCMInterruptionTimeOut = 56008
    }
}
