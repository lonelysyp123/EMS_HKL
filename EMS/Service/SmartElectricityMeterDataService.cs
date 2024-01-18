using EMS.Storage.DB.DBManage;
using EMS.ViewModel.NewEMSViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using EMS.Common;
using EMS.Model;
using EMS.ViewModel;
using log4net.Repository.Hierarchy;
using Modbus.Device;
using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using OxyPlot;
using System.Windows.Markup;
using EMS.Storage.DB.Models;

namespace EMS.Service
     
{
    public class SmartElectricityMeterDataService : DataServiceBase<SmartElectricityMeterModel>
    {
        private SerialPort SerialPortInstance;
        private Configuaration Configuaration;
        private ModbusMaster _master;

        public SmartElectricityMeterDataService(string id)
            : base(id)
        {
            DevType = "SEM";
            Configuaration = new Configuaration();
        }

        protected override void TryConnect()
        {
            while (!IsConnected)
            {
                // 从数据库中获取链接信息
                SmartElectricityMeterManage semConfigInfo = new SmartElectricityMeterManage();
                var items = semConfigInfo.Get();
                if (items != null && items.Count > 0)
                {
                    var item = items.Find(x => x.Id.ToString() == ID);
                    if (item != null)
                    {
                        Configuaration.SelectedCommPort = item.SelectedCommPort;
                        Configuaration.SelectedBaudRate = item.SelectedBaudRate;
                        Configuaration.SelectedParity = (Parity)item.SelectedParity;
                        Configuaration.SelectedStopBits = (StopBits)item.SelectedStopBits;
                        Configuaration.SelectedDataBits = item.SelectedDataBits;
                        Configuaration.AcquisitionCycle_Ammeter = item.AcquisitionCycle;
                    }
                }

                if (Open())
                {
                    _master = ModbusIpMaster.CreateIp(SerialPortInstance);
                    IsConnected = true;
                }
                else
                {
                    continue;
                }

                Thread.Sleep(1000);
            }

            // 连接成功后开始采集数据
            StartDaqData();
            StartSaveData();
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

        protected override void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000 + 100);

                    byte[] oneSidedata = ReadFunc(12, 2);
                    byte[] dcBasedata = ReadFunc(50, 3);
                    byte[] forwardEnergydata = ReadFunc(2000, 10);
                    byte[] reverseEnergydata = ReadFunc(2140, 10);
                    CurrentModel = DataDecode(oneSidedata, dcBasedata, forwardEnergydata, reverseEnergydata);
                    OnChangeData(this, CurrentModel.Clone());
                    Models.Add(CurrentModel.Clone() as SmartElectricityMeterModel);
                    if (IsSaveDaq)
                    {
                        SaveModels.TryAdd(CurrentModel.Clone() as SmartElectricityMeterModel);
                    }
                }
                catch (Exception ex)
                {
                    LogUtils.Error(DevType + " ID:" + ID, ex);
                    break;
                }
            }
        }

        private void SaveData(SmartElectricityMeterModel[] models)
        {
            List<SmartElectricityMeterInfoModel> smartelectricitymeterInfoModels = new List<SmartElectricityMeterInfoModel>();
            for (int l = 0; l < models.Length; l++)
            {
                var model = models[l];
                //计量电表存储相关操作
                SmartElectricityMeterInfoModel smartelectricitymeterInfoModel = new SmartElectricityMeterInfoModel();
                smartelectricitymeterInfoModel.ID = int.Parse(ID);
                smartelectricitymeterInfoModel.TotalForwardPrimaryEnergy = model.TotalForwardPrimaryEnergy;
                smartelectricitymeterInfoModel.TotalReversePrimaryEnergy = model.TotalReversePrimaryEnergy;
                smartelectricitymeterInfoModel.Voltage = model.Voltage;
                smartelectricitymeterInfoModel.Current = model.Current;
                smartelectricitymeterInfoModel.Power = model.Power;
                smartelectricitymeterInfoModel.TotalActiveEnergy = model.TotalActiveEnergy;
                smartelectricitymeterInfoModel.TotalSpikesActiveEnergy = model.TotalSpikesActiveEnergy;
                smartelectricitymeterInfoModel.TotalPeakActiveEnergy = model.TotalPeakActiveEnergy;
                smartelectricitymeterInfoModel.TotalFlatActiveEnergy = model.TotalFlatActiveEnergy;
                smartelectricitymeterInfoModel.TotalValleyActiveEnergy = model.TotalValleyActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthTotalActiveEnergy = model.CurrMonthTotalActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthSpikesActiveEnergy = model.CurrMonthSpikesActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthPeakActiveEnergy = model.CurrMonthPeakActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthFlatRateActiveEnergy = model.CurrMonthFlatRateActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthValleyActiveEnergy = model.CurrMonthValleyActiveEnergy;
                smartelectricitymeterInfoModel.TotalReverseActiveEnergy = model.TotalReverseActiveEnergy;
                smartelectricitymeterInfoModel.TotalSpikesReverseActiveEnergy = model.TotalSpikesReverseActiveEnergy;
                smartelectricitymeterInfoModel.TotalPeakReverseActiveEnergy = model.TotalPeakReverseActiveEnergy;
                smartelectricitymeterInfoModel.TotalFlatReverseActiveEnergy = model.TotalFlatReverseActiveEnergy;
                smartelectricitymeterInfoModel.TotalValleyReverseActiveEnergy = model.TotalValleyReverseActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthTotalReverseActiveEnergy = model.CurrMonthTotalReverseActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthSpikesReverseActiveEnergy = model.CurrMonthSpikesReverseActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthPeakReverseActiveEnergy = model.CurrMonthPeakReverseActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthFlatReverseActiveEnergy = model.CurrMonthFlatReverseActiveEnergy;
                smartelectricitymeterInfoModel.CurrMonthValleyReverseActiveEnergy = model.CurrMonthValleyReverseActiveEnergy;
            }
            SmartElectricityMeterInfoManage smartelectricitymeterInfoManage = new SmartElectricityMeterInfoManage();
            smartelectricitymeterInfoManage.Insert(smartelectricitymeterInfoModels.ToArray());
        }

        //数据解析
        private SmartElectricityMeterModel DataDecode(byte[] oneSidedata, byte[] dcBasedata, byte[] forwardEnergydata, byte[] reverseEnergydata)
        {
            SmartElectricityMeterModel item = new SmartElectricityMeterModel();
            if (oneSidedata != null)
            {
                item.TotalForwardPrimaryEnergy = BitConverter.ToInt16(oneSidedata, 0);
                item.TotalReversePrimaryEnergy = BitConverter.ToInt16(oneSidedata, 2);
            }
            if (dcBasedata != null)
            {
                item.Voltage = BitConverter.ToInt16(dcBasedata, 0);
                item.Current = BitConverter.ToInt16(dcBasedata, 2);
                item.Power = BitConverter.ToInt16(dcBasedata, 4);
            }
            if (forwardEnergydata != null)
            {
                item.TotalActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 0);
                item.TotalSpikesActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 2);
                item.TotalPeakActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 4);
                item.TotalFlatActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 6);
                item.TotalValleyActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 8);
                item.CurrMonthTotalActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 10);
                item.CurrMonthSpikesActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 12);
                item.CurrMonthPeakActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 14);
                item.CurrMonthFlatRateActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 16);
                item.CurrMonthValleyActiveEnergy = BitConverter.ToInt16(forwardEnergydata, 18);
            }
            if (reverseEnergydata != null)
            {
                item.TotalReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 0);
                item.TotalSpikesReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 2);
                item.TotalPeakReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 4);
                item.TotalFlatReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 6);
                item.TotalValleyReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 8);
                item.CurrMonthTotalReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 10);
                item.CurrMonthSpikesReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 12);
                item.CurrMonthPeakReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 14);
                item.CurrMonthFlatReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 16);
                item.CurrMonthValleyReverseActiveEnergy = BitConverter.ToInt16(reverseEnergydata, 18);
            }
            return item;
        }
        /// <summary>
        /// 通用读取函数
        /// </summary>
        /// <param name="address"></param>
        /// <param name="num"></param>
        /// <returns></returns>
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
                LogUtils.Warn(DevType + " ID:" + ID + "读取数据失败", ex);
                if (IsConnected && !IsCommunicationProtectState)
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
                try
                {
                    SerialPortInstance.Open();
                    return true;
                }
                catch (Exception)
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
                try
                {
                    SerialPortInstance.Open();
                    IsConnected = true;
                    LogUtils.Debug("保护机制重连成功，" + DevType + " ID:" + ID + "上线");
                }
                catch (Exception)
                {
                    LogUtils.Debug("保护机制重连失败, " + DevType + " ID:" + ID + "下线");
                }
            }
        }

        //读取总的一次侧电量
        public byte[] ReadTotalPrimaryEnergyInfo()
        {
            return ReadFunc(12, 2);
        }
        //读取电压电流功率
        public byte[] ReadDCBaseInfo()
        {
            return ReadFunc(50, 3);
        }
        //读取所有正向电量信息
        public byte[] ReadActiveEnergyInfo()
        {
            return ReadFunc(2000, 10);
        }
        //读取所有反向电量信息
        public byte[] ReadReverseActiveEnergyInfo()
        {
            return ReadFunc(2140, 10);
        }

        /// <summary>
        /// 方便查看无实际作用
        /// </summary>
        public enum SmartEleMeterCommandAddressEnum
        {
            [Description("总正向一次侧电能")]
            SEMTotalForwardPrimaryEnergy = 12,//读4个字节，高字节在前，低字节在后
            [Description("总反向一次侧电能")]
            SEMTotalReversePrimaryEnergy = 14,
            [Description("电压")]//float
            SEMVoltage = 50,
            [Description("电流")]//float
            SEMCurrent = 52,
            [Description("功率")]//float
            SEMPower = 54,
            [Description("总正向有功电能")]//读4个字节，高字节在前，低字节在后
            SEMTotal = 2000,
            [Description("总尖正向有功电能")]
            SEMTotalSpikes = 2002,
            [Description("总峰正向有功电能")]
            SEMTotalPeak = 2004,
            [Description("总平正向有功电能")]
            SEMTotalFlat = 2006,
            [Description("总谷正向有功电能")]
            SEMTotalValley = 2008,
            [Description("当前月总正向有功电能")]
            SEMCurrMonthTotal = 2010,
            [Description("当前月尖正向有功电能")]
            SEMCurrMonthSpikes = 2012,
            [Description("当前月峰正向有功电能")]
            SEMCurrMonthPeak = 2014,
            [Description("当前月平正向有功电能")]
            SEMCurrMonthFlatRate = 2016,
            [Description("当前月谷正向有功电能")]
            SEMCurrMonthValley = 2018,
            [Description("总反向有功电能")]
            SEMTotalReverse = 2140,
            [Description("总尖反向有功电能")]
            SEMTotalSpikesReverse = 2142,
            [Description("总峰反向有功电能")]
            SEMTotalPeakReverse = 2144,
            [Description("总平反向有功电能")]
            SEMTotalFlatReverse = 2146,
            [Description("总谷反向有功电能")]
            SEMTotalValleyReverse = 2148,
            [Description("当前月总反向有功电能")]
            SEMCurrMonthTotalReverse = 2150,
            [Description("当前月尖反向有功电能")]
            SEMCurrMonthSpikesReverse = 2152,
            [Description("当前月峰反向有功电能")]
            SEMCurrMonthPeakReverse = 2154,
            [Description("当前月平反向有功电能")]
            SEMCurrMonthFlatReverse = 2156,
            [Description("当前月谷反向有功电能")]
            SEMCurrMonthValleyReverse = 2158,
        }
    }
}
