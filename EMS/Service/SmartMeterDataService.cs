using ControlzEx.Standard;
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
using System.Security;
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
        private int HeadIndex = 11;
        private int LengthIndex = 13;
        byte[] Request_GetVoltage_A =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x34, 0x34, 0x35, 0x00, 0x16 };
        byte[] Response_GetVoltage_A =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x06, 0x33, 0x34, 0x34, 0x35, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetVoltage_B =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x35, 0x34, 0x35, 0x00, 0x16 };
        byte[] Response_GetVoltage_B =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x06, 0x33, 0x35, 0x34, 0x35, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetVoltage_C =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x36, 0x34, 0x35, 0x00, 0x16 };
        byte[] Response_GetVoltage_C =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x06, 0x33, 0x36, 0x34, 0x35, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetCurrent_A =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x34, 0x35, 0x35, 0x00, 0x16 };
        byte[] Response_GetCurrent_A =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x34, 0x35, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetCurrent_B =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x35, 0x35, 0x35, 0x00, 0x16 };
        byte[] Response_GetCurrent_B =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x35, 0x35, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetCurrent_C =       
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x36, 0x35, 0x35, 0x00, 0x16 };
        byte[] Response_GetCurrent_C =      
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x36, 0x35, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetActivePower_A =   
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x34, 0x36, 0x35, 0x00, 0x16 };
        byte[] Response_GetActivePower_A =  
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x34, 0x36, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetActivePower_B = 
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x35, 0x36, 0x35, 0x00, 0x16 };
        byte[] Response_GetActivePower_B = 
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x35, 0x36, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetActivePower_C = 
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x36, 0x36, 0x35, 0x00, 0x16 };
        byte[] Response_GetActivePower_C = 
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x36, 0x36, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetActivePower_Total =
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x33, 0x36, 0x35, 0x00, 0x16 };
        byte[] Response_GetActivePower_Total =
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x33, 0x36, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetReactivePower_A =
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x34, 0x37, 0x35, 0x00, 0x16 };
        byte[] Response_GetReactivePower_A =
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x34, 0x37, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetReactivePower_B =
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x35, 0x37, 0x35, 0x00, 0x16 };
        byte[] Response_GetReactivePower_B =
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x35, 0x37, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetReactivePower_C =
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x36, 0x37, 0x35, 0x00, 0x16 };
        byte[] Response_GetReactivePower_C =
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x36, 0x37, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        byte[] Request_GetReactivePower_Total =
            new byte[] { 0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x11, 0x04, 0x33, 0x33, 0x37, 0x35, 0x00, 0x16 };
        byte[] Response_GetReactivePower_Total =
            new byte[] { 0xFE, 0xFE, 0xFE, 0xFE,
                         0x68, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0x68, 0x91, 0x07, 0x33, 0x33, 0x37, 0x35, 0x00, 0x00, 0x00, 0x00, 0x16 };

        protected override void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000 + 100);
                    // 采集数据
                    var Data_Voltage_A = ReadDataForCmd(Request_GetVoltage_A, Response_GetVoltage_A.Length);
                    if(DataDecode(Data_Voltage_A, Response_GetVoltage_A, out int Voltage_A))
                    {
                        CurrentModel.Voltage_A = Voltage_A * 0.1;
                    }

                    var Data_Voltage_B = ReadDataForCmd(Request_GetVoltage_B, Response_GetVoltage_B.Length);
                    if (DataDecode(Data_Voltage_B, Response_GetVoltage_B, out int Voltage_B))
                    {
                        CurrentModel.Voltage_B = Voltage_B * 0.1;
                    }

                    var Data_Voltage_C = ReadDataForCmd(Request_GetVoltage_C, Response_GetVoltage_C.Length);
                    if (DataDecode(Data_Voltage_C, Response_GetVoltage_C, out int Voltage_C))
                    {
                        CurrentModel.Voltage_C = Voltage_C * 0.1;
                    }

                    var Data_Current_A = ReadDataForCmd(Request_GetCurrent_A, Response_GetCurrent_A.Length);
                    if (DataDecode(Data_Current_A, Response_GetCurrent_A, out int Current_A))
                    {
                        CurrentModel.Current_A = Current_A * 0.001;
                    }

                    var Data_Current_B = ReadDataForCmd(Request_GetCurrent_B, Response_GetCurrent_B.Length);
                    if (DataDecode(Data_Current_B, Response_GetCurrent_B, out int Current_B))
                    {
                        CurrentModel.Current_B = Current_B * 0.001;
                    }

                    var Data_Current_C = ReadDataForCmd(Request_GetCurrent_C, Response_GetCurrent_C.Length);
                    if (DataDecode(Data_Current_C, Response_GetCurrent_C, out int Current_C))
                    {
                        CurrentModel.Current_C = Current_C * 0.001;
                    }

                    var Data_ActivePower_A = ReadDataForCmd(Request_GetActivePower_A, Response_GetActivePower_A.Length);
                    if (DataDecode(Data_ActivePower_A, Response_GetActivePower_A, out int ActivePower_A))
                    {
                        CurrentModel.ActivePower_A = ActivePower_A * 0.0001;
                    }

                    var Data_ActivePower_B = ReadDataForCmd(Request_GetActivePower_B, Response_GetActivePower_B.Length);
                    if (DataDecode(Data_ActivePower_B, Response_GetActivePower_B, out int ActivePower_B))
                    {
                        CurrentModel.ActivePower_B = ActivePower_B * 0.0001;
                    }

                    var Data_ActivePower_C = ReadDataForCmd(Request_GetActivePower_C, Response_GetActivePower_C.Length);
                    if (DataDecode(Data_ActivePower_C, Response_GetActivePower_C, out int ActivePower_C))
                    {
                        CurrentModel.ActivePower_C = ActivePower_C * 0.0001;
                    }

                    var Data_ActivePower_Total = ReadDataForCmd(Request_GetActivePower_Total, Response_GetActivePower_Total.Length);
                    if (DataDecode(Data_ActivePower_Total, Response_GetActivePower_Total, out int ActivePower_Total))
                    {
                        CurrentModel.ActivePower_Total = ActivePower_Total * 0.0001;
                    }

                    var Data_ReactivePower_A = ReadDataForCmd(Request_GetReactivePower_A, Response_GetReactivePower_A.Length);
                    if (DataDecode(Data_ReactivePower_A, Response_GetReactivePower_A, out int ReactivePower_A))
                    {
                        CurrentModel.ReactivePower_A = ReactivePower_A * 0.0001;
                    }

                    var Data_ReactivePower_B = ReadDataForCmd(Request_GetReactivePower_B, Response_GetReactivePower_B.Length);
                    if (DataDecode(Data_ReactivePower_B, Response_GetReactivePower_B, out int ReactivePower_B))
                    {
                        CurrentModel.ReactivePower_B = ReactivePower_B * 0.0001;
                    }

                    var Data_ReactivePower_C = ReadDataForCmd(Request_GetReactivePower_C, Response_GetReactivePower_C.Length);
                    if (DataDecode(Data_ReactivePower_C, Response_GetReactivePower_C, out int ReactivePower_C))
                    {
                        CurrentModel.ReactivePower_C = ReactivePower_C * 0.0001;
                    }

                    var Data_ReactivePower_Total = ReadDataForCmd(Request_GetReactivePower_Total, Response_GetReactivePower_Total.Length);
                    if (DataDecode(Data_ReactivePower_Total, Response_GetReactivePower_Total, out int ReactivePower_Total))
                    {
                        CurrentModel.ReactivePower_Total = ReactivePower_Total * 0.0001;
                    }
                    OnChangeData(this, CurrentModel.Clone());
                    Models.TryAdd(CurrentModel.Clone() as SmartMeterModel);
                    if (IsSaveDaq)
                    {
                        SaveModels.TryAdd(CurrentModel.Clone() as SmartMeterModel);
                    }
                }
                catch (Exception ex)
                {
                    LogUtils.Error(DevType + " ID:" + ID, ex);
                    break;
                }
            }
        }

        protected override void SaveData(SmartMeterModel[] models)
        {
            //List<SMInfoModel> InfoModels = new List<SMInfoModel>();
            //for (int l = 0; l < models.Length; l++)
            //{
            //    var model = models[l];
            //    SMInfoModel InfoModel = new SMInfoModel();

            //    InfoModels.Add(InfoModel);
            //}

            //SMInfoManage InfoManage = new SMInfoManage();
            //InfoManage.Insert(InfoModels.ToArray());
        }

        private byte[] ReadDataForCmd(byte[] Request, int num)
        {
            try
            {
                // 计算crc校验
                int sum = 0;
                for (int l = 0; l < Request.Length - 2; l++)
                {
                    sum = sum + Request[l];
                }
                Request[Request.Length - 2] = (byte)sum;

                var readBytes = new byte[num];
                SerialPortInstance.Write(Request, 0, Request.Length);
                SerialPortInstance.Read(readBytes, 0, num);
                return readBytes;
            }
            catch (Exception ex)
            {
                LogUtils.Warn(DevType + " ID:" + ID + "读取数据失败", ex);
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
                    StartDaqData();
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
            value = 0;
            if (CheckData(model, data))
            {
                string str = (data[data.Length - 3] - 0x33).ToString("X") + (data[data.Length - 4] - 0x33).ToString("X");
                if(int.TryParse(str, out value))
                {
                    return true;
                }
            }
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
            // 验证数据完整性
            int tail = readBytes[LengthIndex] - 2;
            if (standardBytes.Length == readBytes.Length)
            {
                for (int i = HeadIndex; i < standardBytes.Length - tail; i++)
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

            // crc校验
            int sum = 0;
            for (int l = 4; l < readBytes.Length - 2; l++)
            {
                sum = sum + readBytes[l];
            }
            if (sum != readBytes[readBytes.Length - 2])
            {
                return false;
            }

            return true;
        }
    }
}
