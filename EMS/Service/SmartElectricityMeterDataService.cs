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

namespace EMS.Service
{
    public class SmartElectricityMeter: DataServiceBase<SmartElectricityMeterModel>
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
                SmartMeterManage smConfigInfo = new SmartMeterManage();
                var items = smConfigInfo.Get();
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

        public enum SmartEleMeterCommandAddressEnum
        {
            [Description("总正向一次侧电能")]
            SEMTotalForwardPrimaryEnergy = 12,
            [Description("总反向一次侧电能")]
            SEMTotalReversePrimaryEnergy = 14,
            [Description("电压")]
            SEMVoltage = 50,
            [Description("电流")]
            SEMCurrent = 52,
            [Description("功率")]
            SEMPower = 54,
            [Description("总正向有功电能")]
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
