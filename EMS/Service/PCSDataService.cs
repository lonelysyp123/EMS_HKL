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
using log4net.Repository.Hierarchy;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using TNCN.EMS.Common.Mqtt;

namespace EMS.Service
{
    public class PCSDataService : DataServiceBase<PCSModel>
    {
        private string IP = "0.0.0.0";
        private int Port = 0;
        private TcpClient _client;
        private ModbusMaster _master;
        private byte PCSId = 1;

        public PCSDataService(string ID)
            :base(ID)
        {
            DevType = "PCS";
        }

        protected override void TryConnect()
        {
            while (!IsConnected)
            {
                try
                {
                    // 从数据库中获取链接信息
                    PcsManage pcsConfigInfo = new PcsManage();
                    var items = pcsConfigInfo.Get();
                    if (items != null && items.Count > 0)
                    {
                        var item = items.Find(x => x.Id.ToString() == ID);
                        if (item != null)
                        {
                            IP = item.Ip == null ? "" : item.Ip;
                            Port = item.Port;
                            DaqTimeSpan = item.AcquisitionCycle;
                        }
                    }

                    // 创建一个线程去链接设备，直到设备链接成功，退出线程，并开始采集
                    _client = new TcpClient();
                    _client.Connect(IPAddress.Parse(IP), Port);
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    //LogUtils.Warn(DevType + " ID:" + ID + " 连接失败", ex);
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }

            // 连接成功后开始采集数据
            StartDaqData();
            StartSaveData();
        }

        protected override void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000 + 100);

                    byte[] dcState = ReadFunc(53026, 7);
                    byte[] pcsData = ReadFunc(53005, 10);
                    byte[] Temp = ReadFunc(53221, 3);
                    byte[] DCBranch1INFO = ReadFunc(53250, 10);
                    byte[] SerialNumber = ReadFunc(53579, 15);
                    CurrentModel = DataDecode(dcState, pcsData, Temp, DCBranch1INFO, SerialNumber);
                    OnChangeData(this, CurrentModel.Clone());
                    Models.TryAdd(CurrentModel.Clone() as PCSModel);
                    if (IsSaveDaq)
                    {
                        SaveData(CurrentModel);
                    }
                }
                catch (Exception ex)
                {
                    //LogUtils.Error(DevType + " ID:" + ID, ex);
                    break;
                }
            }
        }

        private void SaveData(PCSModel model)
        {
            PCSInfoModel pcsInfoModel = new PCSInfoModel();
            pcsInfoModel.ID = int.Parse(ID);
            pcsInfoModel.DCPower = model.DcBranch1DCPower;
            pcsInfoModel.DCVol = model.DcBranch1DCVol;
            pcsInfoModel.DCCurrent = model.DcBranch1DCCur;
            //pcsInfoModel.TotalCharCap = model.;
            pcsInfoModel.BusVol = model.DcBranch1BUSVol;
            pcsInfoModel.ModuleTemp = model.ModuleTemperature;
            pcsInfoModel.EnvTemp = model.AmbientTemperature;
            //pcsInfoModel.PCSState = ;
            //pcsInfoModel.SideState = ;
            pcsInfoModel.HappenTime = DateTime.Now;
            PCSInfoManage pcsInfoManage = new PCSInfoManage();
            pcsInfoManage.Insert(pcsInfoModel);
        }

        private PCSModel DataDecode(byte[] dcstate, byte[] pcsdata, byte[] temp, byte[] dcbranch1info, byte[] serialnumber)
        {
            PCSModel item = new PCSModel();
            if (dcstate != null)
            {
                item.ModuleOnLineFlag = BitConverter.ToUInt16(dcstate, 0);
                item.ModuleRunFlag = BitConverter.ToUInt16(dcstate, 4);
                item.ModuleAlarmFlag = BitConverter.ToUInt16(dcstate, 8);
                item.ModuleFaultFlag = BitConverter.ToUInt16(dcstate, 12);
            }
            if (pcsdata != null)
            {
                item.AlarmStateFlagDC1 = BitConverter.ToUInt16(pcsdata, 0);
                item.AlarmStateFlagDC2 = BitConverter.ToUInt16(pcsdata, 4);
                item.AlarmStateFlagDC3 = BitConverter.ToUInt16(pcsdata, 6);
                item.AlarmStateFlagPDS = BitConverter.ToUInt16(pcsdata, 8);
                item.ControlStateFlagPCS = BitConverter.ToUInt16(pcsdata, 10);
                item.StateFlagPCS = BitConverter.ToUInt16(pcsdata, 12);
                item.DcBranch1StateFlag1 = BitConverter.ToUInt16(pcsdata, 16);
                item.DcBranch1StateFlag2 = BitConverter.ToUInt16(pcsdata, 18);
            }
            if (temp != null)
            {
                item.ModuleTemperature = Math.Round(BitConverter.ToUInt16(temp, 0) * 0.1 - 20, 2);
                item.AmbientTemperature = Math.Round(BitConverter.ToUInt16(temp, 4) * 0.1 - 20, 2);
            }
            if (dcbranch1info != null)
            {
                item.DcBranch1DCPower = Math.Round(BitConverter.ToUInt16(dcbranch1info, 0) * 0.1 - 1500, 2);
                item.DcBranch1DCVol = Math.Round(BitConverter.ToUInt16(dcbranch1info, 2) * 0.1, 2);
                item.DcBranch1DCCur = Math.Round(BitConverter.ToUInt16(dcbranch1info, 4) * 0.1 - 2000, 2);
                item.DcBranch1CharHigh = BitConverter.ToUInt16(dcbranch1info, 6);
                item.DcBranch1CharLow = BitConverter.ToUInt16(dcbranch1info, 8);
                item.DcBranch1DisCharHigh = BitConverter.ToUInt16(dcbranch1info, 10);
                item.DcBranch1DisCharLow = BitConverter.ToUInt16(dcbranch1info, 12);
                item.DcBranch1BUSVol = Math.Round(BitConverter.ToUInt16(dcbranch1info, 18) * 0.1, 2);
            }
            if (serialnumber != null)
            {
                item.SNAdress = new ushort[11];
                for (int i = 0; i < 11; i++)
                {
                    item.SNAdress[i] = BitConverter.ToUInt16(serialnumber, 2 * i);
                }
                item.MonitorSoftCode = BitConverter.ToUInt16(serialnumber, 22);
                item.DcSoftCode = BitConverter.ToUInt16(serialnumber, 26);
                item.U2SoftCode = BitConverter.ToUInt16(serialnumber, 28);
            }
            return item;
        }

        private byte[] ReadFunc(ushort address, ushort num)
        {
            try
            {
                ushort[] holding_register = _master.ReadHoldingRegisters(1, address, num);
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
                //LogUtils.Warn(DevType + " ID:" + ID + "读取数据失败", ex);
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

        private bool CommunicationCheck()
        {
            int count = 0;
            while (true)
            {
                if (_client.ConnectAsync(IPAddress.Parse(IP), Port).Wait(ReconnectInterval))
                {
                    _master = ModbusIpMaster.CreateIp(_client);
                    return true;
                }
                else
                {
                    count++;
                    if (count > MaxReconnectTimes)
                    {
                        IsCommunicationProtectState = true;
                        IsConnected = false;
                        Thread CommunicationProtectTr = new Thread(CommunicationProtect);
                        CommunicationProtectTr.IsBackground = true;
                        CommunicationProtectTr.Start();
                        return false;
                    }
                }
            }
        }

        private void CommunicationProtect()
        {
            while (!IsConnected)
            {
                Thread.Sleep(ReconnectIntervalLong);
                if (_client.ConnectAsync(IPAddress.Parse(IP), Port).Wait(ReconnectInterval))
                {
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;
                    //LogUtils.Debug("保护机制重连成功，PCS ID:" + ID + "上线");
                }
                else
                {
                    //LogUtils.Debug("保护机制重连失败, PCS ID:" + ID + "下线");
                }
            }
        }

        /// <summary>
        /// 通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        public void WriteFunc(byte slave, ushort address, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(slave, address, value);
            }
            catch (Exception ex)
            {
                //LogUtils.Warn("PCS ID:" + ID + "写入数据失败", ex);
                if (!_client.Connected && !IsCommunicationProtectState)
                {
                    if (CommunicationCheck())
                    {
                        WriteFunc(slave, address, value);
                    }
                }
            }
        }

        /// <summary>
        /// 同步BUS侧电压阈值
        /// </summary>
        /// <param name="busvolvalues">BUS侧电压阈值数组</param>
        public void SyncBUSVolInfo(double[] busvolvalues)
        {
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.HigherVolThreshold, (ushort)(busvolvalues[0] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.LowerVolThreshold, (ushort)(busvolvalues[1] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.HigherVolSetting, (ushort)(busvolvalues[2] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.LowerVolSetting, (ushort)(busvolvalues[3] * 10));
        }

        public byte[] ReadBUSVolInfo()
        {
            return ReadFunc(53640, 4);
        }

        /// <summary>
        /// 同步DC侧分支值
        /// </summary>
        /// <param name="dcbranch1values">DC侧支路分支值数组</param>
        public void SyncDCBranchInfo(double[] dcbranch1values)
        {
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.BatteryLowerVolThreshold, (ushort)(dcbranch1values[0] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.EndOfDischargeVol, (ushort)(dcbranch1values[1] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.MutiStrCurRegulationPar, (ushort)(dcbranch1values[2]));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.BatteryToppingCharVol, (ushort)(dcbranch1values[3] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.EndOfCharCur, (ushort)(dcbranch1values[4] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.MaxCharCur, (ushort)(dcbranch1values[5] * 10));
            WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.MaxDischarCur, (ushort)(dcbranch1values[6] * 10));
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
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.CharModeSet, 0);
            }
            else if (pcsmancharmodelset == "设置功率调节")
            {
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.CharModeSet, 1);
            }
        }

        /// <summary>
        /// PCS手动充放电
        /// </summary>
        /// <param name="pcsmancharset">设置值</param>
        /// <param name="pcsmancharmodelset">设置模式</param>
        public void ManChar(string pcsmancharmodelset, double pcsmancharset)
        {
            if (pcsmancharmodelset == "设置电流调节")
            {
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.CurrentValueSet, (ushort)(pcsmancharset * 10));
            }
            else
            {
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.PowerValueSet, (ushort)(pcsmancharset * 10));
            }
        }


        /// <summary>
        /// PCS系统开机
        /// </summary>
        public void PCSOpen()
        {
            try
            {
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.PCSSystemOpen, 1);
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
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.PCSSystemClose, 1);
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
                WriteFunc(PCSId, (ushort)PcsCommandAdressEnum.PCSSystemClearFault, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendPcsCommand(BessCommand command)
        {
            PcsCommandAdressEnum modeAddress = PcsCommandAdressEnum.CharModeSet;
            int modeValue = 0;
            PcsCommandAdressEnum valueAddress = PcsCommandAdressEnum.PowerValueSet;
            int controlValue = 0;

            switch (command.BatteryStrategy)
            {
                case BatteryStrategyEnum.Standby:
                    modeValue = 1;
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    controlValue = 0;
                    break;
                case BatteryStrategyEnum.ConstantCurrentCharge:
                    modeValue = 0;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10));
                    valueAddress = PcsCommandAdressEnum.CurrentValueSet;
                    break;
                case BatteryStrategyEnum.ConstantCurrentDischarge: //需要添加负值
                    modeValue = 0;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10) * -1);
                    valueAddress = PcsCommandAdressEnum.CurrentValueSet;
                    break;
                case BatteryStrategyEnum.ConstantPowerCharge:
                    modeValue = 1;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10));
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    break;
                case BatteryStrategyEnum.ConstantPowerDischarge:  //需要添加负值
                    modeValue = 1;
                    controlValue = Convert.ToInt32(Math.Abs(command.Value * 10) * -1);
                    valueAddress = PcsCommandAdressEnum.PowerValueSet;
                    break;
            }
            WriteFunc(PCSId, (ushort)modeAddress, (ushort)modeValue);
            WriteFunc(PCSId, (ushort)valueAddress, (ushort)controlValue);
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
