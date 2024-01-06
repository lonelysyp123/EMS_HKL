using CommunityToolkit.Mvvm.ComponentModel;
using EMS.Model;
using EMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TNCN.EMS.Api;
using System.Diagnostics;
using EMS.Service;

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
        /// 启停状态
        /// </summary>
        private string startStopState;
        public string StartStopState
        {
            get { return startStopState; }
            set
            {
                SetProperty(ref startStopState, value);
            }
        }

        /// <summary>
        /// 储存装机功率
        /// </summary>
        private double installedPower;
        public double InstalledPower
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
        private double energyStorageCapacity;
        public double EnergyStorageCapacity
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
        private double totalSOC;
        public double TotalSOC
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
        private double totalSOH;
        public double TotalSOH
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
        private double chargingCapacity;
        public double ChargingCapacity
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
        private double dischargeCapacity;
        public double DischargeCapacity
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
        private double dcBranch1Char;
        public double DcBranch1Char
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
        private double dcBranch1DisChar;
        public double DcBranch1DisChar
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
        private double currentPower;
        public double CurrentPower
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
        private double stationPower;
        public double StationPower
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
        private double dcBranch1DCVol;
        public double DcBranch1DCVol
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
        private double dcBranch1DCCur;
        public double DcBranch1DCCur
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
        private double dcBranch1DCPower;
        public double DcBranch1DCPower
        {
            get { return dcBranch1DCPower; }
            set
            {
                SetProperty(ref dcBranch1DCPower, value);
            }
        }

        #endregion


        private SmartMeterDataService smartMeterDataService;
        public HomePageModel()
        {

        }

        /// <summary>
        /// BMS数据展示
        /// </summary>
        /// <param name="model"></param>
        public void DataDisPlayBMS(BatteryTotalModel bmsmodel)
        {
            //StateFill_Normal：正常
            //StateFill_Offline：离线
            //StateFill_Warn：预警
            //StateFill_MinorFaults：轻故障
            //StateFill_HeavyFaults：重故障
            //StateFill_CrisisFaults：危机故障
            //StateFill_BMSRun：BMS运行
            //EnergyStorageCapacity：储能容量
            //TotalSOC：总SOC
            //TotalSOH：总SOH
            //StartStopState：启停状态
            //FaultState：故障状态
        }

        /// <summary>
        /// PCS数据展示
        /// </summary>
        /// <param name="bmsmodel"></param>
        /// <param name="pcsmodel"></param>
        /// <param name="smartmetermodel"></param>
        public void DataDisPlayPCS(PCSMonitorModel pcsmodel)
        {
            DcBranch1DCVol = pcsmodel.DcBranch1DCVol;
            DcBranch1DCVol = pcsmodel.DcBranch1DCVol;
            DcBranch1DCCur = pcsmodel.DcBranch1DCCur;
            DcBranch1DCPower = pcsmodel.DcBranch1DCPower;
            DcBranch1Char = pcsmodel.DcBranch1Char;
            DcBranch1DisChar = pcsmodel.DcBranch1DisChar;
            //StateFill_PCSRun：PCS运行
            //InstalledPower：储存装机功率
            //ChargingCapacity：今日充电量
            //DischargeCapacity：今日放电量
            //CurrentPower：当前功率
            //StationPower：电站功率
        }

        /// <summary>
        /// 电表数据展示
        /// </summary>
        /// <param name="smartmetermodel"></param>
        public void DataDisPlaySmartMeter(SmartMeterDataService smartMeterDataService)
        {
            if (smartMeterDataService.IsConnected)
            {
                StateFill_AmmeterRun = new SolidColorBrush(LightColors.Open_Green);
            }
            else
            {
                StateFill_AmmeterRun = new SolidColorBrush(LightColors.Close);
            }
        }

        /// <summary>
        /// 云端数据展示
        /// </summary>
        public void DataDisPlayCloud()
        {

            if (MqttClientApi.IsConnected())
               
            {
                StateFill_CloudTelecom = new SolidColorBrush(LightColors.Open_Green);
            } 
            else
            {
                StateFill_CloudTelecom = new SolidColorBrush(LightColors.Close);

            }
        }
    }
}

