using EMS.ViewModel.NewEMSViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Service
{
    public class SmartElectricityMeterDataService
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

        public string ID;
        private Action<object, bool, bool> OnChangeState;
        private Action<object, object> OnChangeData;
        private SerialPort SerialPortInstance;
        private Configuaration Configuaration;
        private static object Locker;

        public SmartElectricityMeterDataService()
        {

        }

        public SmartElectricityMeterDataService(string id)
        {
            ID = id;
            Locker = new object();
            Configuaration = new Configuaration();
            StartDataService();
        }

        private void StartDataService()
        {
            Thread thread = new Thread(TryConnect);
            thread.IsBackground = true;
            thread.Start();
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
