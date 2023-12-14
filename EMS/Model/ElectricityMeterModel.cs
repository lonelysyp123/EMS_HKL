using CommunityToolkit.Mvvm.ComponentModel;
using EMS.Api;
using log4net;
using log4net.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace EMS.Model
{
    public class ElectricityMeterModel : ObservableObject
    {
        private int _voltage_A;
        public int Voltage_A 
        {
            get => _voltage_A; 
            set 
            {
                SetProperty(ref _voltage_A, value);
            } 
        }

        private int _voltage_B;
        public int Voltage_B 
        { 
            get => _voltage_B;
            set
            {
                SetProperty(ref _voltage_B, value);
            }
        }

        private int _voltage_C;
        public int Voltage_C 
        { 
            get => _voltage_C; 
            set
            {
                SetProperty(ref _voltage_C, value);
            }
        }

        private int _electric_A;
        public int Electric_A 
        { 
            get => _electric_A; 
            set
            {
                SetProperty(ref _electric_A, value);
            }
        }

        private int _electric_B;
        public int Electric_B 
        { 
            get => _electric_B;
            set
            {
                SetProperty(ref _electric_B, value);
            } 
        }

        private int _electric_C;
        public int Electric_C 
        { 
            get => _electric_C;
            set
            {
                SetProperty(ref _electric_C, value);
            } 
        }

        private int _activePower_A;
        public int ActivePower_A 
        { 
            get => _activePower_A;
            set
            {
                SetProperty(ref _activePower_A, value);
            }
        }

        private int _activePower_B;
        public int ActivePower_B 
        { 
            get => _activePower_B;
            set
            {
                SetProperty(ref _activePower_B, value);
            } 
        }

        private int _activePower_C;
        public int ActivePower_C 
        { 
            get => _activePower_C;
            set
            {
                SetProperty(ref _activePower_C, value);
            }
        }

        private int _activePower_Total;
        public int ActivePower_Total 
        { 
            get => _activePower_Total; 
            set
            {
                SetProperty(ref _activePower_Total, value);
            }
        }

        private int _reactivePower_A;
        public int ReactivePower_A 
        { 
            get => _reactivePower_A; 
            set
            {
                SetProperty(ref _reactivePower_A, value);
            }
        }

        private int _reactivePower_B;
        public int ReactivePower_B 
        { 
            get => _reactivePower_B; 
            set
            {
                SetProperty(ref _reactivePower_B, value);
            }
        }

        private int _reactivePower_C;
        public int ReactivePower_C 
        { 
            get => _reactivePower_C; 
            set
            {
                SetProperty(ref _reactivePower_C, value);
            }
        }

        private int _reactivePower_Total;
        public int ReactivePower_Total 
        { 
            get => _reactivePower_Total; 
            set
            {
                SetProperty(ref _reactivePower_Total, value);
            }
        }

        public Configuaration Configuaration { get; set; }
        private ILog Logger;

        public ElectricityMeterModel()
        {
            Configuaration = new Configuaration();
            Logger = LogManager.GetLogger(typeof(ElectricityMeterModel));
        }

        private SerialPort SerialPortInstance;
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Open()
        {
            try
            {
                SerialPortInstance = new SerialPort();
                SerialPortInstance.PortName = Configuaration.SelectedCommPort;
                SerialPortInstance.BaudRate = Configuaration.SelectedBaudRate;
                SerialPortInstance.DataBits = Configuaration.SelectedDataBits;
                SerialPortInstance.StopBits = Configuaration.SelectedStopBits;
                SerialPortInstance.Parity = Configuaration.SelectedParity;
                SerialPortInstance.ReadTimeout = 500;
                SerialPortInstance.Open();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Close()
        {
            try
            {
                if (SerialPortInstance != null)
                {
                    if (SerialPortInstance.IsOpen)
                    {
                        StopDaqTh();
                        SerialPortInstance.Close();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsStartDaqData = false;
        /// <summary>
        /// 启动采集线程
        /// </summary>
        public void StartDaqTh()
        {
            if (SerialPortInstance != null && SerialPortInstance.IsOpen)
            {
                if (!IsStartDaqData)
                {
                    Thread thread = new Thread(DaqTh);
                    thread.IsBackground = true;
                    IsStartDaqData = true;
                    thread.Start();
                }
            }
        }

        /// <summary>
        /// 采集线程
        /// </summary>
        private void DaqTh()
        {
            try
            {
                while (IsStartDaqData)
                {
                    Thread.Sleep(500);
                    // 采集数据
                    Voltage_A = ReadDataForCmd(SerialPortInstance, Request_GetVoltage_A, Response_GetVoltage_A);

                    // 储存数据
                    if (IsStartSaveData)
                    {
                        TemporaryData.Enqueue(GetItself());
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录报错
                Logger.Error(ex.ToString());
                Close();
            }
        }

        private ElectricityMeterModel GetItself()
        {
            return new ElectricityMeterModel()
            {
                Voltage_A = this.Voltage_A,
                Voltage_B = this.Voltage_B,
                Voltage_C = this.Voltage_C,
                Electric_A = this.Electric_A,
                Electric_B = this.Electric_B,
                Electric_C = this.Electric_C,
                ActivePower_A = this.ActivePower_A,
                ActivePower_B = this.ActivePower_B,
                ActivePower_C = this.ActivePower_C,
                ActivePower_Total = this.ActivePower_Total,
                ReactivePower_A = this.ReactivePower_A,
                ReactivePower_B = this.ReactivePower_B,
                ReactivePower_C = this.ReactivePower_C,
                ReactivePower_Total = this.ReactivePower_Total,
            };
        }

        // CS校验不计算0xFE, 0xFE, 0xFE, 0xFE
        byte[] Request_GetVoltage_A = new byte[] { 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x11, 0x04, 0x02, 0x01, 0x01, 0x00, 0xA4, 0x16 };
        byte[] Response_GetVoltage_A = new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0x68, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x68, 0x91, 0x08, 0x33, 0x33, 0x33, 0x33, 0x00, 0x00, 0x00, 0x00, 0x44, 0x16 };
        byte[] Request_GetVoltage_B = new byte[0];
        byte[] Response_GetVoltage_B = new byte[0];

        /// <summary>
        /// 读取A相电压
        /// </summary>
        /// <param name="serialPort">串口实例</param>
        /// <returns>电压值</returns>
        private int ReadVoltage_A(SerialPort serialPort)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {

                    serialPort.Write(Request_GetVoltage_A, 0, Request_GetVoltage_A.Length);

                    // 读取返回值
                    int count = 0;
                    int readLength = 16;
                    byte[] readByte = new byte[readLength];
                    while (true)
                    {
                        count++;
                        if (serialPort.BytesToRead >= readLength)
                        {
                            serialPort.Read(readByte, 0, readByte.Length);
                        }
                        else
                        {
                            if (count > 5)
                            {
                                break;
                            }
                            else
                            {
                                Thread.Sleep(100);
                                continue;
                            }
                        }
                    }

                    // 验证数据
                    if (CheckData(Response_GetVoltage_A, readByte))
                    {
                        // 解析数据
                        byte[] bf = new byte[] { readByte[18], readByte[19], readByte[20], readByte[21] };
                        return BitConverter.ToInt32(bf, 0);
                    }
                    else
                    {
                        // 日志记录
                        Logger.Warn("Data Validation Failed");
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 用指定的命令去读取数据
        /// </summary>
        /// <param name="serialPort">串口实例</param>
        /// <param name="Request">请求数据包</param>
        /// <param name="Response">响应数据包模板</param>
        /// <returns>读数</returns>
        private int ReadDataForCmd(SerialPort serialPort, byte[] Request, byte[] Response)
        {
            try
            {
                if (serialPort != null)
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Write(Request, 0, Request.Length);
                        // 读取返回值
                        int count = 0;
                        byte[] readByte = new byte[Response.Length];
                        while (true)
                        {
                            count++;
                            if (serialPort.BytesToRead >= Response.Length)
                            {
                                serialPort.Read(readByte, 0, readByte.Length);
                            }
                            else
                            {
                                if (count > 5)
                                {
                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(100);
                                    continue;
                                }
                            }
                        }
                        // 验证数据
                        if (CheckData(Response, readByte))
                        {
                            // 解析数据
                            byte[] bf = new byte[] { readByte[18], readByte[19], readByte[20], readByte[21] };
                            return BitConverter.ToInt32(bf, 0);
                        }
                        else
                        {
                            // 日志记录
                            Logger.Warn("Data Validation Failed");
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// <summary>
        /// 停止采集线程
        /// </summary>
        public void StopDaqTh()
        {
            if (SerialPortInstance != null && SerialPortInstance.IsOpen)
            {
                if (IsStartDaqData)
                {
                    IsStartDaqData = false;
                }
            }
        }

        private bool IsStartSaveData = false;
        private int MedianRange = 10;
        private ConcurrentQueue<ElectricityMeterModel> HistoryData;
        private ConcurrentQueue<ElectricityMeterModel> TemporaryData;
        /// <summary>
        /// 开始数据存储
        /// </summary>
        public void StartSaveDataTh()
        {
            HistoryData = new ConcurrentQueue<ElectricityMeterModel>();
            TemporaryData = new ConcurrentQueue<ElectricityMeterModel>();
            Thread thread = new Thread(SaveDataTh);
            thread.IsBackground = true;
            IsStartSaveData = true;
            thread.Start();
        }

        private void SaveDataTh()
        {
            while (IsStartSaveData)
            {
                if (TemporaryData.Count > MedianRange)
                {
                    ElectricityMeterModel[] models = new ElectricityMeterModel[MedianRange];
                    for (int i = 0; i < MedianRange; i++)
                    {
                        TemporaryData.TryDequeue(out ElectricityMeterModel item);
                        models[i] = item;
                    }

                    
                }
            }
        }

        public void StopSaveDataTh()
        {
            IsStartSaveData = false;
        }
    }

    public class Configuaration
    {
        public List<SerialPortSettingsModel> CommPorts { get; private set; }
        public string SelectedCommPort { get; set; }
        public List<SerialPortSettingsModel> BaudRates { get; private set; }
        public int SelectedBaudRate { get; set; }
        public List<SerialPortSettingsModel> Parities { get; private set; }
        public Parity SelectedParity { get; set; }
        public List<SerialPortSettingsModel> StopBitsList { get; private set; }
        public StopBits SelectedStopBits { get; set; }
        public int[] DataBits { get; private set; }
        public int SelectedDataBits { get; set; }

        public Configuaration()
        {
            CommPorts = SerialPortSettingsModel.Instance.getCommPorts();
            BaudRates = SerialPortSettingsModel.Instance.getBaudRates();
            Parities = SerialPortSettingsModel.Instance.getParities();
            StopBitsList = SerialPortSettingsModel.Instance.getStopBits();
            DataBits = new int[]{ 4,5,6,7,8 };
        }
    }
}
