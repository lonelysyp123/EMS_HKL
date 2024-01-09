﻿using ControlzEx.Standard;
using EMS.Common;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
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
using System.Windows.Media.Animation;

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
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
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
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        private bool _isSaveDaq;
        public bool IsSaveDaq
        {
            get => _isSaveDaq;
            private set
            {
                if (_isSaveDaq != value)
                {
                    _isSaveDaq = value;
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        public string ID { get; private set; }
        private string IP = "0.0.0.0";
        private int Port = 0;
        private TcpClient _client;
        private ModbusMaster _master;
        private Action<object, bool, bool, bool> OnChangeState;
        private Action<object, object> OnChangeData;

        //public BMSDataService()
        //{
        //    Locker = new object();
        //    StartDataService();
        //}

        public BMSDataService(string id)
        {
            ID = id;
            //Locker = new object();
            StartDataService();
        }

        private void StartDataService()
        {
            Thread thread = new Thread(TryConnect);
            thread.IsBackground = true;
            thread.Start();
        }

        private void TryConnect()
        {
            while (!IsConnected)
            {
                try
                {
                    // 从数据库中获取链接信息
                    BcmuManage bmsConfigInfo = new BcmuManage();
                    var items = bmsConfigInfo.Get();
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

                    _client = new TcpClient();
                    _client.Connect(IPAddress.Parse(IP), Port);
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;
                }
                catch(Exception ex)
                {
                    LogUtils.Warn("BMS id:" + ID + " 连接失败", ex);
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

        public void RegisterState(Action<object, bool, bool, bool> action)
        {
            OnChangeState = action;
        }

        public void RegisterState(Action<object, object> action)
        {
            OnChangeData = action;
        }

        private void StartDaqData()
        {
            if (IsConnected)
            {
                if (!IsDaqData)
                {
                    IsDaqData = true;
                    Thread th = new Thread(DaqDataTh);
                    th.IsBackground = true;
                    th.Start();
                    LogUtils.Debug("BMS id:" + ID + " 开始采集数据");
                }
            }
        }

        public void StartSaveData()
        {
            IsSaveDaq = true;
            LogUtils.Debug("BMS id:" + ID + " 开始保存数据");
        }

        public void StopSaveData()
        {
            IsSaveDaq = false;
            LogUtils.Debug("BMS id:" + ID + " 停止保存数据");
        }

        private int DaqTimeSpan = 0;
        //public BlockingCollection<BatteryTotalModel> BatteryTotalModels;
        //private static object Locker;
        public BatteryTotalModel CurrentBatteryTotalModel;
        private void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000 + 100);

                    //var dataRefreshedFlag = ReadFunc(470, 1);
                    //if (BitConverter.ToInt16(dataRefreshedFlag, 0) == 0x55aa || BitConverter.ToInt16(dataRefreshedFlag, 0) == 0x0000)
                    //{
                    //    WriteFunc(551, 0x55AA);
                    //    Thread.Sleep(100);
                    //    WriteFunc(551, 0x0000);
                    //    Thread.Sleep(100);
                    //}
                    //else
                    //{
                    //    WriteFunc(551, 0x0000);
                    //    Thread.Sleep(100);
                    //}

                    byte[] BCMUData = new byte[48];
                    Array.Copy(ReadFunc(361, 24), 0, BCMUData, 0, 48);
                    //Array.Copy(ReadFunc(405, 1), 0, BCMUData, 48, 2);
                    byte[] BMUIDData = { 0 };
                    byte[] BMUData = new byte[720];
                    Array.Copy(ReadFunc(1, 120), 0, BMUData, 0, 240);
                    Array.Copy(ReadFunc(121, 120), 0, BMUData, 240, 240);
                    Array.Copy(ReadFunc(241, 120), 0, BMUData, 480, 240);


                    ///BMUState, 0:451   2:452, 4:455 6:456 8:457 10:458 12:459 14:460 16:461 18:462 20:463 22:464 24:465 26:466
                    byte[] BMUStateData = new byte[28];
                    Array.Copy(ReadFunc(451, 2), 0, BMUStateData, 0, 4);
                    Array.Copy(ReadFunc(455, 12), 0, BMUStateData, 4, 24);

                    //BCMUState  0:450  2:453 4：454    6：467 8：468 10：469
                    byte[] BCMUStateData = new byte[12];
                    Array.Copy(ReadFunc(450, 1), 0, BCMUStateData, 0, 2);
                    Array.Copy(ReadFunc(453, 2), 0, BCMUStateData, 2, 4);
                    Array.Copy(ReadFunc(467, 3), 0, BCMUStateData, 6, 6);

                    //batteryTotalModels.Enqueue(DataDecode(BCMUData, BCMUStateData, BMUIDData, BMUData, BMUStateData));
                    //lock (Locker)
                    //{
                        CurrentBatteryTotalModel = DataDecode(BCMUData, BCMUStateData, BMUIDData, BMUData, BMUStateData);
                        OnChangeData(this, CurrentBatteryTotalModel.Clone());
                        if (IsSaveDaq)
                        {
                            SaveData(CurrentBatteryTotalModel);
                        }
                    //}
                }
                catch (Exception ex)
                {
                    LogUtils.Error("BMS id:" + ID, ex);
                    break;
                }
            }
        }

        private void SaveData(BatteryTotalModel total)
        {
            DateTime date = DateTime.Now;
            TotalBatteryInfoModel TotalModel = new TotalBatteryInfoModel();
            TotalModel.HappenTime = date;
            TotalModel.BCMUID = ID;
            TotalModel.Voltage = total.TotalVoltage;
            TotalModel.Current = total.TotalCurrent;
            TotalModel.SOC = total.TotalSOC;
            TotalModel.SOH = total.TotalSOH;
            TotalModel.AverageTemperature = total.AverageTemperature;
            TotalModel.MinVoltage = total.MinVoltage;
            TotalModel.MinVoltageIndex = total.MinVoltageIndex;
            TotalModel.MaxVoltage = total.MaxVoltage;
            TotalModel.MaxVoltageIndex = total.MaxVoltageIndex;
            TotalModel.MinTemperature = total.MinTemperature;
            TotalModel.MinTemperatureIndex = total.MinTemperatureIndex;
            TotalModel.MaxTemperature = total.MaxTemperature;
            TotalModel.MaxTemperatureIndex = total.MaxTemperatureIndex;
            TotalModel.BCMUState = total.StateBCMU;
            TotalModel.FaultState1 = total.FaultStateBCMUTotalFlag;
            TotalModel.FaultState2 = total.FaultStateBCMUFlag1;
            TotalModel.FaultState3 = total.FaultStateBCMUFlag2;
            TotalModel.AlarmState1 = total.AlarmStateBCMUFlag1;
            TotalModel.AlarmState2 = total.AlarmStateBCMUFlag2;
            TotalModel.AlarmState3 = total.AlarmStateBCMUFlag3;
            TotalModel.BalanceChannel = total.BalanceChannel;
            TotalBatteryInfoManage TotalManage = new TotalBatteryInfoManage();
            TotalManage.Insert(TotalModel);

            for (int i = 0; i < total.Series.Count; i++)
            {
                SeriesBatteryInfoModel SeriesModel = new SeriesBatteryInfoModel();
                SeriesModel.BCMUID = ID;
                SeriesModel.BMUID = total.Series[i].BMUID;
                SeriesModel.MinVoltage = total.Series[i].MinVoltage;
                SeriesModel.MinVoltageIndex = total.Series[i].MinVoltageIndex;
                SeriesModel.MaxVoltage = total.Series[i].MaxVoltage;
                SeriesModel.MaxVoltageIndex = total.Series[i].MaxVoltageIndex;
                SeriesModel.MinTemperature = total.Series[i].MinTemperature;
                SeriesModel.MinTemperatureIndex = total.Series[i].MinTemperatureIndex;
                SeriesModel.MaxTemperature = total.Series[i].MaxTemperature;
                SeriesModel.MaxTemperatureIndex = total.Series[i].MaxTemperatureIndex;
                SeriesModel.VolFaultState = total.Series[i].VolFaultInfo;
                SeriesModel.Temp1FaultState = total.Series[i].TempFaultInfo1;
                SeriesModel.Temp2FaultState = total.Series[i].TempFaultInfo2;
                SeriesModel.BalanceFaultState = total.Series[i].BalanceFaultFaultInfo;
                
                SeriesModel.ChargeCapacitySum = total.Series[i].ChargeCapacitySum;
                SeriesModel.HappenTime = date;
                for (int j = 0; j < total.Series[i].Batteries.Count; j++)
                {
                    typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Voltage);
                    typeof(SeriesBatteryInfoModel).GetProperty("Capacity" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Capacity);
                    typeof(SeriesBatteryInfoModel).GetProperty("SOC" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].SOC);
                    typeof(SeriesBatteryInfoModel).GetProperty("Resistance" + j).SetValue(SeriesModel, total.Series[i].Batteries[j].Resistance);
                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2)).SetValue(SeriesModel, total.Series[i].Batteries[j].Temperature1);
                    typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (j * 2 + 1)).SetValue(SeriesModel, total.Series[i].Batteries[j].Temperature2);
                }
                SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
                SeriesManage.Insert(SeriesModel);
            }
        }

        public BatteryTotalModel GetCurrentData()
        {
            BatteryTotalModel item = new BatteryTotalModel();
            if (CurrentBatteryTotalModel != null)
            {
                //lock (Locker)
                //{
                    item = CurrentBatteryTotalModel.Clone() as BatteryTotalModel;
                //}
            }
            return item;
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
                LogUtils.Warn("BMS id:"+ID+"读取数据失败", ex);
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
                        Thread CommunicationProtectTr = new Thread(CommunicationProtect);
                        CommunicationProtectTr.IsBackground = true;
                        CommunicationProtectTr.Start();
                        return false;
                    }
                }
            }
        }

        private static int reconnectIntervalLong =1 * 1000 * 5; // ms
        private void CommunicationProtect()
        {
            while (!IsConnected)
            {
                Thread.Sleep(reconnectIntervalLong);
                if (_client.ConnectAsync(IPAddress.Parse(IP), Port).Wait(reconnectInterval))
                {
                    _master = ModbusIpMaster.CreateIp(_client);
                    IsConnected = true;
                    StartDaqData();
                    LogUtils.Debug("保护机制重连成功，设备上线");
                }
                else
                {
                    LogUtils.Debug("保护机制重连失败");
                }
            }
        }

        private BatteryTotalModel DataDecode(byte[] BCMU, byte[] BCMUState, byte[] BMUID, byte[] BMU, byte[] BMUState)
        {
            BatteryTotalModel item = new BatteryTotalModel();
            if (BCMU != null && BCMUState != null)
            {
                DataDecode_BCMU(BCMU, BCMUState, ref item);
            }
            if (BMU != null && BMUID != null && BMUState != null)
            {
                DataDecode_BMU(BMU, BMUID, BMUState, ref item);
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

        private void DataDecode_BMU(byte[] obj1, byte[] obj2, byte[] obj3, ref BatteryTotalModel total)
        {
            for (int i = 0; i < total.Series.Count; i++)
            {                 
                total.Series[i].ChargeChannelState = 0;
                total.Series[i].ChargeCapacitySum = 0;
                total.Series[i].MinVoltage = BitConverter.ToInt16(obj1, (337 + i * 4) * 2) * 0.001; ;
                total.Series[i].MaxVoltage = BitConverter.ToInt16(obj1, (338 + i * 4) * 2) * 0.001;
                var volindex = BitConverter.ToInt16(obj1, ((339 + i * 4)) * 2);
                total.Series[i].MaxVoltageIndex = (volindex >> 8) & 0xFF;
                total.Series[i].MinVoltageIndex = volindex & 0xFF;
                total.Series[i].MinTemperature = (BitConverter.ToInt16(obj1, (349 + i * 4) * 2) - 2731) * 0.1;
                total.Series[i].MaxTemperature = (BitConverter.ToInt16(obj1, (350 + i * 4) * 2) - 2731) * 0.1;
                var tempindex = BitConverter.ToInt16(obj1, (351 + i * 4) * 2);
                total.Series[i].MaxTemperatureIndex = (tempindex >> 8) & 0xFF;
                total.Series[i].MinTemperatureIndex = (tempindex) & 0xFF;
                //BMU电梯故障信息
                total.Series[i].VolFaultInfo = BitConverter.ToUInt16(obj3, (2 + i * 4) * 2);
                total.Series[i].TempFaultInfo1 = BitConverter.ToUInt16(obj3, (3 + i * 4) * 2);
                total.Series[i].TempFaultInfo2 = BitConverter.ToUInt16(obj3, (4 + i * 4) * 2);
                total.Series[i].BalanceFaultFaultInfo = BitConverter.ToUInt16(obj3, (5 + 4 * i) * 2);

                // BMUID
                //byte[] BMUIDArray = new byte[16];
                //Array.Copy(obj2, 16 * i, BMUIDArray, 0, 16);
                //int ID1 = BitConverter.ToInt16(BMUIDArray, 0);
                //StringBuilder BMUNameBuilder = new StringBuilder();
                //for (int k = 0; k < 16; k++)
                //{
                //    char BMUIDChar = Convert.ToChar(BMUIDArray[k]);
                //    BMUNameBuilder.Append(BMUIDChar);
                //}

                total.Series[i].BMUID = (i + 1).ToString();

                for (int j = 0; j < total.Series[i].Batteries.Count; j++)
                {
                    total.Series[i].Batteries[j].Voltage = BitConverter.ToInt16(obj1, (j + i * 16) * 2) * 0.001;
                    total.Series[i].Batteries[j].Temperature1 = (BitConverter.ToInt16(obj1, (48 + j + i * 32) * 2) - 2731) * 0.1;
                    total.Series[i].Batteries[j].Temperature2 = (BitConverter.ToInt16(obj1, (64 + j + i * 32) * 2) - 2731) * 0.1;
                    total.Series[i].Batteries[j].SOC = BitConverter.ToInt16(obj1, (144 + i * 16 + j) * 2) * 0.01;
                    total.Series[i].Batteries[j].SOH = BitConverter.ToInt16(obj1, (192 + i * 16 + j) * 2) * 0.1;
                    total.Series[i].Batteries[j].Resistance = BitConverter.ToInt16(obj1, (240 + i * 16 + j) * 2);
                    total.Series[i].Batteries[j].Capacity = BitConverter.ToInt16(obj1, (288 + i * 16 + j) * 2);
                    total.Series[i].Batteries[j].BatteryNumber = j + 1;
                }
            }
        }

        private void DataDecode_BCMU(byte[] obj, byte[] obj2, ref BatteryTotalModel total)
        {
            total.BCMUID = ID;
            total.TotalVoltage = BitConverter.ToInt16(obj, 0) * 0.1;
            total.TotalCurrent = BitConverter.ToInt16(obj, 2) * 0.1;
            total.TotalSOC = BitConverter.ToInt16(obj, 4) * 0.1;
            total.TotalSOH = BitConverter.ToInt16(obj, 6) * 0.1;
            total.AvgVol = BitConverter.ToInt16(obj, 8) *0.001;
            total.MinVoltage = BitConverter.ToInt16(obj, 10) * 0.001;
            total.MaxVoltage = BitConverter.ToInt16(obj, 12) * 0.001;
            var volIndex = BitConverter.ToInt16(obj, 14);
            total.MaxVoltageIndex = (volIndex >> 8) & 0xFF;
            total.MinVoltageIndex = (volIndex) & 0xFF;
            total.AverageTemperature = (BitConverter.ToInt16(obj, 16) - 2731) * 0.1;
            total.MinTemperature = (BitConverter.ToInt16(obj, 18) - 2731) * 0.1;
            total.MaxTemperature = (BitConverter.ToInt16(obj, 20) - 2731) * 0.1;
            var tempIndex = BitConverter.ToInt16(obj, 22);
            total.MaxTemperatureIndex = (tempIndex >> 8) & 0xFF;
            total.MinTemperatureIndex = (tempIndex) & 0xFF;
            //total.MinTemperatureIndex = BitConverter.ToInt16(obj, 22);
            //total.MaxTemperatureIndex = BitConverter.ToInt16(obj, 23);
            total.VolContainerTemperature1 = (BitConverter.ToInt16(obj, 24) - 2731) * 0.1;
            total.VolContainerTemperature2 = (BitConverter.ToInt16(obj, 26) - 2731) * 0.1;
            total.VolContainerTemperature3 = (BitConverter.ToInt16(obj, 28) - 2731) * 0.1;
            total.VolContainerTemperature4 = (BitConverter.ToInt16(obj, 30) - 2731) * 0.1;
            total.StateBCMU = BitConverter.ToInt16(obj, 32);
            total.DCVoltage = BitConverter.ToInt16(obj, 34);
            total.IResistanceRP = BitConverter.ToInt16(obj, 36);
            total.IResistanceRN = BitConverter.ToInt16(obj, 38);
            total.NomCapacity = BitConverter.ToInt16(obj, 40);
            total.NomVoltage = BitConverter.ToInt16(obj, 42);
            total.BatteryCount = BitConverter.ToInt16(obj, 44);
            total.BatteryCycles = BitConverter.ToInt16(obj, 46);
            total.BalanceChannel = 0;
            total.FaultStateBCMUTotalFlag = BitConverter.ToInt16(obj2, 0);
            total.FaultStateBCMUFlag1 = BitConverter.ToInt16(obj2, 2);
            total.FaultStateBCMUFlag2 = BitConverter.ToInt16(obj2, 4);
            total.AlarmStateBCMUFlag1 = BitConverter.ToUInt16(obj2, 6);
            total.AlarmStateBCMUFlag2 = BitConverter.ToUInt16(obj2, 8);
            total.AlarmStateBCMUFlag3 = BitConverter.ToUInt16(obj2, 10);


            total.HWVersionBCMU = 0;
            total.VersionSWBCMU = 0;
            //total.AlarmStateBCMUFlag1 =0;
            //total.AlarmStateBCMUFlag2 = 0;
            //total.AlarmStateBCMUFlag3 = 0;

            total.BatMaxChgPower = 0;
            total.BatMaxDischgPower = 0;
            total.OneChgCoulomb = 0;
            total.OneDischgCoulomb = 0;
            total.TotalChgCoulomb = 0;
            total.TotalDischgCoulomb = 0;
            total.RestCoulomb = 0;
            total.MaxVolDiff = 0;

        }

        public void StopDaqData()
        {
            IsDaqData = false;
            LogUtils.Debug("BMS id:" + ID + " 停止采集数据");
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
                LogUtils.Warn("BMS id:" + ID + "写入数据失败", ex);
                if (!_client.Connected && !IsCommunicationProtectState)
                {
                    if (CommunicationCheck())
                    {
                        WriteFunc(address, value);
                    }
                }
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

     

        public void OnGrid()
        {
            WriteFunc(545, 0x0055);
            WriteFunc(545, 0);
        }

        public void OffGrid()
        {
            WriteFunc(546, 0x00AA);
            WriteFunc(546, 0);
        }

        public byte[] GetProetctSet()
        {
            return ReadFunc(560, 37);
        }
        public void SetClusterState(ushort value)
        {
            WriteFunc(547, value);
            WriteFunc(547, 0);
        }

        public void ResetFault()
        {
            WriteFunc(550, 0x0055);
            WriteFunc(550, 0);
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

        public void SyncBCMUInfo1(ushort[] values)
        {

            WriteFunc(500, values);
        }
        public void SyncBCMUInfo2(ushort[] values)
        {
            WriteFunc(503, values);
        }
        public void SyncBCMUInfo3(ushort[] values)
        {
            WriteFunc(506, values);
        }
        public void SyncBCMUInfo4(ushort[] values)
        {
            WriteFunc(509, values);
        }
        public void SyncBCMUInfo5(ushort[] values)
        {
            WriteFunc(512, values);
        }
        public void SyncBCMUInfo6(ushort[] values)
        {
            WriteFunc(515, values);
        }
        public void SyncBCMUInfo7(ushort[] values)
        {
            WriteFunc(518, values);
        }
        public void SyncBCMUInfo8(ushort[] values)
        {
            WriteFunc(521, values);

        }
        public void SyncBCMUInfo9(ushort[] values)
        {
            WriteFunc(524, values);
        }
        public void SyncBCMUInfo10(ushort[] values)
        {
            WriteFunc(527, values);
        }
        public void SyncBCMUInfo11(ushort[] values)
        {
            WriteFunc(530, values);
        }
        public void SyncBCMUInfo12(ushort[] values)
        {
            WriteFunc(533, values);
        }
        public void SyncBCMUInfo13(ushort[] values)
        {
            WriteFunc(536, values);
        }
    }
}
