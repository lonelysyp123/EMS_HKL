using EMS.Common;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.ViewModel;
using EMS.ViewModel.NewEMSViewModel;
using log4net.Repository.Hierarchy;
using Modbus.Device;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EMS.Service
{
    public class SmartMeterDataService : DataServiceBase<SmartMeterModel>
    {
        private SerialPort SerialPortInstance;
        private Configuaration Configuaration;

        public SmartMeterDataService(string id)
            :base(id)
        {
            DevType = "SM";
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

        // CS校验不计算0xFE, 0xFE, 0xFE, 0xFE
        byte[] Request_GetVoltage_A = new byte[] { 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x11, 0x04, 0x02, 0x01, 0x01, 0x00, 0xA4, 0x16 };
        byte[] Response_GetVoltage_A = new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x91, 0x08, 0x33, 0x33, 0x33, 0x33, 0x00, 0x00, 0x00, 0x00, 0x44, 0x16 };
        byte[] Request_GetVoltage_B = new byte[0];
        byte[] Response_GetVoltage_B = new byte[0];
        protected override void DaqDataTh()
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

                    CurrentModel = model;
                    OnChangeData(this, CurrentModel.Clone());
                    Models.TryAdd(CurrentModel.Clone() as SmartMeterModel);
                    if (IsSaveDaq)
                    {
                        SaveData(CurrentModel);
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void SaveData(SmartMeterModel model)
        {
            // 电表存储相关操作
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
                    LogUtils.Debug("保护机制重连成功，"+ DevType + " ID:" + ID + "上线");
                }
                catch (Exception)
                {
                    LogUtils.Debug("保护机制重连失败, "+ DevType + " ID:" + ID + "下线");
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
