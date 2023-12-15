﻿using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Service
{
    public class BMSDataService
    {
        public string IP { get; private set; }
        public string Port { get; private set; }

        private bool _isConnected;
        public bool IsConnected 
        { 
            get=>_isConnected; 
            private set 
            {
                if(_isConnected != value)
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
                if( _isDaqData != value)
                {
                    _isDaqData = value;
                    OnChangeState(_isConnected, _isDaqData);
                }
            }
        }

        public ModbusClient Client { get; private set; }
        private Action<bool, bool> OnChangeState;

        public BMSDataService() { }

        public void SetCommunicationConfig(string ip, string port, ConcurrentQueue<BatteryTotalModel> obj)
        {
            IP = ip;
            Port = port;
            batteryTotalModels = obj;
        }

        public void RegisterState(Action<bool, bool> action)
        {
            OnChangeState = action;
        }

        public void Connect()
        {
            if (!IsConnected)
            {
                if (int.TryParse(Port, out int port))
                {
                    Client = new ModbusClient(IP, port);
                    Client.Connect();
                    IsConnected = true;
                }
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                if (Client != null)
                {
                    Client.Disconnect();
                    IsConnected = false;
                }
            }
        }

        public void StartDaqData()
        {
            if(batteryTotalModels == null)
            {
                batteryTotalModels = new ConcurrentQueue<BatteryTotalModel>();
            }
            IsDaqData = true;
            Thread th = new Thread(DaqDataTh);
            th.IsBackground = true;
            th.Start();
        }

        private int DaqTimeSpan = 1;
        public void SetDaqTimeSpan(int value)
        {
            DaqTimeSpan = value;
        }

        public ConcurrentQueue<BatteryTotalModel> batteryTotalModels;
        private void DaqDataTh()
        {
            while (IsConnected && IsDaqData)
            {
                try
                {
                    Thread.Sleep(DaqTimeSpan * 1000);

                    byte[] BCMUData = new byte[90];
                    Array.Copy(Client.ReadFunc(11000, 45), 0, BCMUData, 0, 90);
                    byte[] BMUIDData = new byte[48];
                    Array.Copy(Client.ReadFunc(11045, 24), 0, BMUIDData, 0, 48);
                    byte[] BMUData = new byte[744];
                    Array.Copy(Client.ReadFunc(10000, 120), 0, BMUData, 0, 240);
                    Array.Copy(Client.ReadFunc(10120, 120), 0, BMUData, 240, 240);
                    Array.Copy(Client.ReadFunc(10240, 120), 0, BMUData, 480, 240);
                    Array.Copy(Client.ReadFunc(10360, 12), 0, BMUData, 720, 24);
                    batteryTotalModels.Enqueue(DataDecode(BCMUData, BMUIDData, BMUData));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private BatteryTotalModel DataDecode(byte[] BCMU, byte[] BMUID, byte[] BMU)
        {
            BatteryTotalModel item = new BatteryTotalModel();
            DataDecode_BCMU(BCMU, ref item);
            DataDecode_BMU(BCMU, BMUID, ref item);
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
            total.FaultyStateBCMUFlag = BitConverter.ToUInt16(obj, 70);
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
    }
}