using CommunityToolkit.Mvvm.Input;
using EMS.Common;
using EMS.Model;
using EMS.MyControl;
using EMS.View.NewEMSView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_BMS_BCMUPageModel : ViewModelBase
    {
        #region ObservableObject
        private SolidColorBrush isConnect;
        public SolidColorBrush IsConnect
        {
            get => isConnect;
            set
            {
                SetProperty(ref isConnect, value);
            }
        }

        private Visibility isVisible_SwitchOn;
        public Visibility IsVisible_SwitchOn
        {
            get => isVisible_SwitchOn;
            set
            {
                SetProperty(ref isVisible_SwitchOn, value);
            }
        }

        private Visibility isVisible_SwitchOff;
        public Visibility IsVisible_SwitchOff
        {
            get => isVisible_SwitchOff;
            set
            {
                SetProperty(ref isVisible_SwitchOff, value);
            }
        }

        private Visibility isVisible_UpArrow;
        public Visibility IsVisible_UpArrow
        {
            get => isVisible_UpArrow;
            set
            {
                SetProperty(ref isVisible_UpArrow, value);
            }
        }

        private Visibility isVisible_DownArrow;
        public Visibility IsVisible_DownArrow
        {
            get => isVisible_DownArrow;
            set
            {
                SetProperty(ref isVisible_DownArrow, value);
            }
        }

        private SolidColorBrush alarmcolor;
        public SolidColorBrush Alarmcolor
        {
            get => alarmcolor;
            set
            {
                SetProperty(ref alarmcolor, value);
            }
        }

        private string remainingSOC;
        public string RemainingSOC
        {
            get { return remainingSOC; }
            set
            {
                SetProperty(ref remainingSOC, value);
            }
        }

        private string clusterVoltage;
        public string ClusterVoltage
        {
            get { return clusterVoltage; }
            set
            {
                SetProperty(ref clusterVoltage, value);
            }
        }

        private string presentCurrent;
        public string PresentCurrent
        {
            get { return presentCurrent; }
            set
            {
                SetProperty(ref presentCurrent, value);
            }
        }

        private string maxCellVoltage;
        public string MaxCellVoltage
        {
            get { return maxCellVoltage; }
            set
            {
                SetProperty(ref maxCellVoltage, value);
            }
        }

        private string minCellVoltage;
        public string MinCellVoltage
        {
            get { return minCellVoltage; }
            set
            {
                SetProperty(ref minCellVoltage, value);
            }
        }

        private string maxTemperature;
        public string MaxTemperature
        {
            get { return maxTemperature; }
            set
            {
                SetProperty(ref maxTemperature, value);
            }
        }

        private bool isOffGrid;
        public bool IsOffGrid
        {
            get { return isOffGrid; }
            set
            {
                SetProperty(ref isOffGrid, value);
            }
        }

        private bool isConnectedGrid;
        public bool IsConnectedGrid
        {
            get { return isConnectedGrid; }
            set
            {
                SetProperty(ref isConnectedGrid, value);
            }
        }

        private SolidColorBrush offGrid;
        public SolidColorBrush OffGrid
        {
            get { return offGrid; }
            set
            {
                SetProperty(ref offGrid, value);
            }
        }

        private SolidColorBrush connectedGrid;
        public SolidColorBrush ConnectedGrid
        {
            get { return connectedGrid; }
            set
            {
                SetProperty(ref connectedGrid, value);
            }
        }

        private SolidColorBrush battery_Stand;
        public SolidColorBrush Battery_Stand
        {
            get { return battery_Stand; }
            set
            {
                SetProperty(ref battery_Stand, value);
            }
        }

        private SolidColorBrush battery_Charging;
        public SolidColorBrush Battery_Charging
        {
            get { return battery_Charging; }
            set
            {
                SetProperty(ref battery_Charging, value);
            }
        }

        private SolidColorBrush battery_Discharge;
        public SolidColorBrush Battery_Discharge
        {
            get { return battery_Discharge; }
            set
            {
                SetProperty(ref battery_Discharge, value);
            }
        }

        private SolidColorBrush battery_OffGrid;
        public SolidColorBrush Battery_OffGrid
        {
            get { return battery_OffGrid; }
            set
            {
                SetProperty(ref battery_OffGrid, value);
            }
        }

        private string maxAmpPower;
        public string MaxAmpPower
        {
            get { return maxAmpPower; }
            set
            {
                SetProperty(ref maxAmpPower, value);
            }
        }

        private string cumulativeDischarge;
        public string CumulativeDischarge
        {
            get { return cumulativeDischarge; }
            set
            {
                SetProperty(ref cumulativeDischarge, value);
            }
        }

        private string singleDischarge;
        public string SingleDischarge
        {
            get { return singleDischarge; }
            set
            {
                SetProperty(ref singleDischarge, value);
            }
        }

        private string maxChargingPower;
        public string MaxChargingPower
        {
            get { return maxChargingPower; }
            set
            {
                SetProperty(ref maxChargingPower, value);
            }
        }

        private string cumulativeCharge;
        public string CumulativeCharge
        {
            get { return cumulativeCharge; }
            set
            {
                SetProperty(ref cumulativeCharge, value);
            }
        }

        private string singleCharge;
        public string SingleCharge
        {
            get { return singleCharge; }
            set
            {
                SetProperty(ref singleCharge, value);
            }
        }

        private string temperature1;
        public string Temperature1
        {
            get { return temperature1; }
            set
            {
                SetProperty(ref temperature1, value);
            }
        }

        private string temperature2;
        public string Temperature2
        {
            get { return temperature2; }
            set
            {
                SetProperty(ref temperature2, value);
            }
        }

        private string temperature3;
        public string Temperature3
        {
            get { return temperature3; }
            set
            {
                SetProperty(ref temperature3, value);
            }
        }

        private string temperature4;
        public string Temperature4
        {
            get { return temperature4; }
            set
            {
                SetProperty(ref temperature4, value);
            }
        }

        private AlarmtLevels wran_BCMU_1;
        public AlarmtLevels Wran_BCMU_1
        {
            get { return wran_BCMU_1; }
            set
            {
                SetProperty(ref wran_BCMU_1, value);
            }
        }

        private AlarmtLevels fault_BCMU_1;
        public AlarmtLevels Fault_BCMU_1
        {
            get { return fault_BCMU_1; }
            set
            {
                SetProperty(ref fault_BCMU_1, value);
            }
        }

        private string[] cluster = new string[] {"A", "B", "C"};
        public string[] Cluster
        {
            get { return cluster; }
            set
            {
                SetProperty(ref cluster, value);
            }
        }

        private string selectedCluster;
        public string SelectedCluster
        {
            get { return selectedCluster; }
            set
            {
                SetProperty(ref selectedCluster, value);
            }
        }

        private string accChargingCapacity;
        public string AccChargingCapacity
        {
            get { return accChargingCapacity; }
            set
            {
                SetProperty(ref accChargingCapacity, value);
            }
        }

        private string channel;
        public string Channel
        {
            get { return channel; }
            set
            {
                SetProperty(ref channel, value);
            }
        }

        private AlarmtLevels wran_BMU_1;
        public AlarmtLevels Wran_BMU_1
        {
            get { return wran_BMU_1; }
            set
            {
                SetProperty(ref wran_BMU_1, value);
            }
        }

        private AlarmtLevels fault_BMU_1;
        public AlarmtLevels Fault_BMU_1
        {
            get { return fault_BMU_1; }
            set
            {
                SetProperty(ref fault_BMU_1, value);
            }
        }
        #endregion

        #region Command

        public RelayCommand ToMonitor_BMS_BCMUPageCommand { get;private set;}

        #endregion

        public Monitor_BMS_BCMUPageModel()
        {
            ToMonitor_BMS_BCMUPageCommand = new RelayCommand(ToMonitor_BMS_BCMUPage);
        }

        public void DataDistribution(BatteryTotalModel model, bool isconnected)
        {
            RemainingSOC = model.TotalSOC.ToString();
            ClusterVoltage = model.TotalVoltage.ToString();
            PresentCurrent = model.TotalCurrent.ToString();
            MaxCellVoltage = model.MaxVoltage.ToString();
            MinCellVoltage = model.MinVoltage.ToString();
            MaxTemperature = model.MaxTemperature.ToString();

            if (model.AlarmStateBCMU.Count > 0)
            {
                Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_T);
            }
            else
            {
                Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_F);
            }

            if (isconnected)
            {
                IsConnect = new SolidColorBrush(BMUColors.IsConnect_T);
            }
            else
            {
                IsConnect = new SolidColorBrush(BMUColors.IsConnect_F);
            }

            // 断路器？
            //IsVisible_SwitchOff;
            //IsVisible_SwitchOn;

            // 充放电状态
            //IsVisible_DownArrow;
            //IsVisible_UpArrow;

            // 并网离网状态
            //IsOffGrid;
            //IsConnectedGrid;
            //OffGrid;
            //ConnectedGrid;

            // 电池簇状态
            //Battery_Stand;
            //Battery_Charging;
            //Battery_Discharge;
            //Battery_OffGrid;

            // 充放电信息
            //MaxAmpPower;
            //CumulativeDischarge;
            //SingleDischarge;
            //MaxChargingPower;
            //CumulativeCharge;
            //SingleCharge;

            Temperature1 = model.VolContainerTemperature1.ToString();
            Temperature2 = model.VolContainerTemperature2.ToString();
            Temperature3 = model.VolContainerTemperature3.ToString();
            Temperature4 = model.VolContainerTemperature4.ToString();

            TemperatureBCMU(model);
            BMUInfo(model);
        }

        private void ToMonitor_BMS_BCMUPage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// BCMU故障告警信息解读
        /// </summary>
        /// <param name="obj">入参</param>
        private void TemperatureBCMU(object obj)
        {
            //Wran_BCMU_1;
            //Fault_BCMU_1;
        }

        /// <summary>
        /// BMU信息解读
        /// </summary>
        /// <param name="obj">入参</param>
        private void BMUInfo(object obj)
        {
            if (SelectedCluster == Cluster[0])
            {
                // 第一串电池信息解读
                //AccChargingCapacity;
                //Channel;
                //Wran_BMU_1;
                //Fault_BMU_1;
            }
            else if (SelectedCluster == Cluster[1])
            {
                // 第二串电池信息解读
            }
            else if (SelectedCluster == Cluster[2])
            {
                // 第三串电池信息解读
            }
        }
    }
}
