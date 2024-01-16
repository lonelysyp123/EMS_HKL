using CommunityToolkit.Mvvm.ComponentModel;
using EMS.Api;
using EMS.Common;
using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TNCN.EMS.Api;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class HomePageModel : ViewModelBase
    {
        #region Property
        /// <summary>
        /// 运行状态——正常
        /// </summary>
        private SolidColorBrush stateFill_Normal;
        public SolidColorBrush StateFill_Normal
        {
            get { return stateFill_Normal; }
            set
            {
                SetProperty(ref stateFill_Normal, value);
            }
        }

        /// <summary>
        /// 运行状态——离线
        /// </summary>
        private SolidColorBrush stateFill_Offline;
        public SolidColorBrush StateFill_Offline
        {
            get { return stateFill_Offline; }
            set
            {
                SetProperty(ref stateFill_Offline, value);
            }
        }

        /// <summary>
        /// 运行状态——预警
        /// </summary>
        private SolidColorBrush stateFill_Warn;
        public SolidColorBrush StateFill_Warn
        {
            get { return stateFill_Warn; }
            set
            {
                SetProperty(ref stateFill_Warn, value);
            }
        }

        /// <summary>
        /// 运行状态——轻故障
        /// </summary>
        private SolidColorBrush stateFill_MinorFaults;
        public SolidColorBrush StateFill_MinorFaults
        {
            get { return stateFill_MinorFaults; }
            set
            {
                SetProperty(ref stateFill_MinorFaults, value);
            }
        }

        /// <summary>
        /// 运行状态——重故障
        /// </summary>
        private SolidColorBrush stateFill_HeavyFaults;
        public SolidColorBrush StateFill_HeavyFaults
        {
            get { return stateFill_HeavyFaults; }
            set
            {
                SetProperty(ref stateFill_HeavyFaults, value);
            }
        }

        /// <summary>
        /// 运行状态——危机故障
        /// </summary>
        private SolidColorBrush stateFill_CrisisFaults;
        public SolidColorBrush StateFill_CrisisFaults
        {
            get { return stateFill_CrisisFaults; }
            set
            {
                SetProperty(ref stateFill_CrisisFaults, value);
            }
        }

        /// <summary>
        /// 运行状态——云端通信
        /// </summary>
        private SolidColorBrush stateFill_CloudTelecom;
        public SolidColorBrush StateFill_CloudTelecom
        {
            get { return stateFill_CloudTelecom; }
            set
            {
                SetProperty(ref stateFill_CloudTelecom, value);
            }
        }

        /// <summary>
        /// 运行状态——BMS运行
        /// </summary>
        private SolidColorBrush stateFill_BMSRun;
        public SolidColorBrush StateFill_BMSRun
        {
            get { return stateFill_BMSRun; }
            set
            {
                SetProperty(ref stateFill_BMSRun, value);
            }
        }

        /// <summary>
        /// 运行状态——PCS运行
        /// </summary>
        private SolidColorBrush stateFill_PCSRun;
        public SolidColorBrush StateFill_PCSRun
        {
            get { return stateFill_PCSRun; }
            set
            {
                SetProperty(ref stateFill_PCSRun, value);
            }
        }

        /// <summary>
        /// 运行状态——电表运行
        /// </summary>
        private SolidColorBrush stateFill_AmmeterRun;
        public SolidColorBrush StateFill_AmmeterRun
        {
            get { return stateFill_AmmeterRun; }
            set
            {
                SetProperty(ref stateFill_AmmeterRun, value);
            }
        }

        /// <summary>
        /// 启停状态
        /// </summary>
        private SolidColorBrush startStopState;
        public SolidColorBrush StartStopState
        {
            get { return startStopState; }
            set
            {
                SetProperty(ref startStopState, value);
            }
        }

        /// <summary>
        /// 故障状态
        /// </summary>
        private SolidColorBrush faultState;
        public SolidColorBrush FaultState
        {
            get { return faultState; }
            set
            {
                SetProperty(ref faultState, value);
            }
        }

        /// <summary>
        /// 储存装机功率
        /// </summary>
        private string installedPower;
        public string InstalledPower
        {
            get { return installedPower; }
            set
            {
                SetProperty(ref installedPower, value);
            }
        }

        /// <summary>
        /// 储能容量
        /// </summary>
        private string energyStorageCapacity;
        public string EnergyStorageCapacity
        {
            get { return energyStorageCapacity; }
            set
            {
                SetProperty(ref energyStorageCapacity, value);
            }
        }

        /// <summary>
        /// 总SOC
        /// </summary>
        private string totalSOC;
        public string TotalSOC
        {
            get { return totalSOC; }
            set
            {
                SetProperty(ref totalSOC, value);
            }
        }

        /// <summary>
        /// 总SOH
        /// </summary>
        private string totalSOH;
        public string TotalSOH
        {
            get { return totalSOH; }
            set
            {
                SetProperty(ref totalSOH, value);
            }
        }

        /// <summary>
        /// 今日充电量
        /// </summary>
        private string chargingCapacity;
        public string ChargingCapacity
        {
            get { return chargingCapacity; }
            set
            {
                SetProperty(ref chargingCapacity, value);
            }
        }

        /// <summary>
        /// 今日放电量
        /// </summary>
        private string dischargeCapacity;
        public string DischargeCapacity
        {
            get { return dischargeCapacity; }
            set
            {
                SetProperty(ref dischargeCapacity, value);
            }
        }

        /// <summary>
        /// 累计充电量
        /// </summary>
        private string dcBranch1Char;
        public string DcBranch1Char
        {
            get { return dcBranch1Char; }
            set
            {
                SetProperty(ref dcBranch1Char, value);
            }
        }

        /// <summary>
        /// 累计放电量
        /// </summary>
        private string dcBranch1DisChar;
        public string DcBranch1DisChar
        {
            get { return dcBranch1DisChar; }
            set
            {
                SetProperty(ref dcBranch1DisChar, value);
            }
        }

        /// <summary>
        /// 当前功率
        /// </summary>
        private string currentPower;
        public string CurrentPower
        {
            get { return currentPower; }
            set
            {
                SetProperty(ref currentPower, value);
            }
        }

        /// <summary>
        /// 电站功率
        /// </summary>
        private string stationPower;
        public string StationPower
        {
            get { return stationPower; }
            set
            {
                SetProperty(ref stationPower, value);
            }
        }

        /// <summary>
        /// 直流电压
        /// </summary>
        private string dcBranch1DCVol;
        public string DcBranch1DCVol
        {
            get { return dcBranch1DCVol; }
            set
            {
                SetProperty(ref dcBranch1DCVol, value);
            }
        }

        /// <summary>
        /// 直流电流
        /// </summary>
        private string dcBranch1DCCur;
        public string DcBranch1DCCur
        {
            get { return dcBranch1DCCur; }
            set
            {
                SetProperty(ref dcBranch1DCCur, value);
            }
        }

        /// <summary>
        /// 直流功率
        /// </summary>
        private string dcBranch1DCPower;
        public string DcBranch1DCPower
        {
            get { return dcBranch1DCPower; }
            set
            {
                SetProperty(ref dcBranch1DCPower, value);
            }
        }

        #endregion

        #region Command
     


        #endregion

        public HomePageModel()
        {
            InstalledPower = 400.ToString();
            EnergyStorageCapacity = 4.ToString();
        }

        public void BMSDataRefreshFromAPI()
        {
            DataDisPlayBMS(BmsApi.GetNextBMSData().ToList());
            StateDisPlayBMS(BmsApi.GetDevServices().ToList());
        }

     

        /// <summary>
        /// BMS数据展示
        /// </summary>
        /// <param name="model"></param>
        public void DataDisPlayBMS(List<BatteryTotalModel> models)
        {
            List<double> SingleSOC = new List<double>();
            List<double> SingleSOH = new List<double>();
            foreach (BatteryTotalModel model in models)
            {
                SingleSOC.Add(model.TotalSOC);
                SingleSOH.Add(model.TotalSOH);
            }
            TotalSOC = SingleSOC.Average().ToString();
            TotalSOH = SingleSOH.Average().ToString();
        }

        public void StateDisPlayBMS(List<BMSDataService> bMSDataService)
        {
            bool bmsstateflag = true;
            foreach (BMSDataService service in bMSDataService)
            {
                if (service.IsConnected == false)
                {
                    bmsstateflag = false;
                }

            }
            if (!bmsstateflag)
            {
                StateFill_BMSRun = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
            }
            else
            {
                StateFill_BMSRun = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
            }
        }

        /// <summary>
        /// PCS数据展示
        /// </summary>
        /// <param name="model"></param>
        public void DataDisPlayPCS(PCSModel model)
        {
            DcBranch1DCVol = model.DcBranch1DCVol.ToString();
            DcBranch1DCCur = model.DcBranch1DCCur.ToString();
            DcBranch1DCPower = model.DcBranch1DCPower.ToString();
            //FaultState = model.
        }

        /// <summary>
        /// PCS状态展示
        /// </summary>
        /// <param name="bMSDataService"></param>
        public void StateDisPlayPCS(bool isconnected)
        {
            if (isconnected)
            {
                StateFill_PCSRun = new SolidColorBrush(BCMUColors.IsConnect_T);
                StartStopState = new SolidColorBrush(BCMUColors.IsConnect_T);
            }
            else
            {
                StateFill_PCSRun = new SolidColorBrush(BCMUColors.IsConnect_F);
                StartStopState = new SolidColorBrush(BCMUColors.IsConnect_F);
            }
        }

        /// <summary>
        /// 电表数据展示
        /// </summary>
        /// <param name="model"></param>
        public void DataDisPlaySM(bool isconnected)
        {
            if (isconnected)
            {
                StateFill_AmmeterRun = new SolidColorBrush(BCMUColors.IsConnect_T);
            }
            else
            {
                StateFill_AmmeterRun = new SolidColorBrush(BCMUColors.IsConnect_F);
            }

        }

        public void StateDisPlayFault()
        {
            // get fault state from strategy api
            StrategyApi.GetFaultState();
            switch (StrategyApi.GetFaultState())
            {
                case Common.StrategyManage.ContingencyStatusEnum.Normal:
                    StateFill_Warn = new SolidColorBrush(LightColors.Close);
                    StateFill_MinorFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_HeavyFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_CrisisFaults = new SolidColorBrush(LightColors.Close);
                    break;
                case Common.StrategyManage.ContingencyStatusEnum.Level1:
                    StateFill_Warn = new SolidColorBrush(LightColors.Close);
                    StateFill_MinorFaults = new SolidColorBrush(LightColors.Open_Red);
                    StateFill_HeavyFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_CrisisFaults = new SolidColorBrush(LightColors.Close);
                    break;
                case Common.StrategyManage.ContingencyStatusEnum.Level2:
                    StateFill_Warn = new SolidColorBrush(LightColors.Close);
                    StateFill_MinorFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_HeavyFaults = new SolidColorBrush(LightColors.Open_Red);
                    StateFill_CrisisFaults = new SolidColorBrush(LightColors.Close);
                    break;
                case Common.StrategyManage.ContingencyStatusEnum.Level3:
                    StateFill_Warn = new SolidColorBrush(LightColors.Close);
                    StateFill_MinorFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_HeavyFaults = new SolidColorBrush(LightColors.Close);
                    StateFill_CrisisFaults = new SolidColorBrush(LightColors.Open_Red);
                    break;
            }
        }

        public void StateDisPlayCloud()
        {
            // get mqtt connected state from mqtt api
            if (MqttClientApi.IsConnected())
            {
                StateFill_CloudTelecom = new SolidColorBrush(BCMUColors.IsConnect_T);
            }
            else
            {
                StateFill_CloudTelecom = new SolidColorBrush(BCMUColors.IsConnect_F);
            }
        }

        public void DataRefresh_SEM(SmartElectricityMeterModel model)
        {
            ChargingCapacity = model.CurrMonthTotalActiveEnergy.ToString();
            DischargeCapacity = model.CurrMonthTotalReverseActiveEnergy.ToString();
            DcBranch1Char = model.TotalActiveEnergy.ToString();
            DcBranch1DisChar = model.TotalReverseActiveEnergy.ToString();
            CurrentPower = model.PowerValue.ToString();
            StationPower = model.PowerValue.ToString();
        }

        //运行状态
        //StateFill_Normal
        //StateFill_Offline

        //状态
        //StartStopState
        //FaultState
    }
}
