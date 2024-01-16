using CommunityToolkit.Mvvm.Input;
using ControlzEx.Standard;
using EMS.Api;
using EMS.Common;
using EMS.Model;
using EMS.MyControl;
using EMS.Service;
using EMS.View.NewEMSView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using TNCN.EMS.Common.Mqtt;
using static EMS.MyControl.BCMUConnectGraph;

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


        #region BCMUINFO
        private string avgClusterVol;

        public string AvgClusterVol
        {
            get => avgClusterVol;
            set
            {
                SetProperty(ref avgClusterVol, value);
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

        private string maxCellVoltageIndex;
        public string MaxCellVoltageIndex
        {
            get { return maxCellVoltageIndex; }
            set
            {
                SetProperty(ref maxCellVoltageIndex, value);
            }
        }


        private string minCellVoltageIndex;
        public string MinCellVoltageIndex
        {
            get { return minCellVoltageIndex; }
            set
            {
                SetProperty(ref minCellVoltageIndex, value);
            }
        }


        private string avgClusterTemp;

        public string AvgClusterTemp
        {
            get => avgClusterTemp;
            set
            {
                SetProperty(ref avgClusterTemp, value);
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

        private string maxTemperatureIndex;
        public string MaxTemperatureIndex
        {
            get { return maxTemperatureIndex; }
            set
            {
                SetProperty(ref maxTemperatureIndex, value);
            }
        }

        
        private string minTemperature;
        public string MinTemperature
        {
            get { return minTemperature; }
            set
            {
                SetProperty(ref minTemperature, value);
            }
        }

        private string minTemperatureIndex;
        public string MinTemperatureIndex
        {
            get { return minTemperatureIndex; }
            set
            {
                SetProperty(ref minTemperatureIndex, value);
            }
        }


        private string chargeChannelStateNumber;

        public string ChargeChannelStateNumber
        {
            get => chargeChannelStateNumber;
            set
            {
                SetProperty(ref chargeChannelStateNumber, value);
            }
        }



        /// <summary>
        /// 额定容量
        /// </summary>
        private string ratedCapacity;

        public string RatedCapacity
        {
            get => ratedCapacity;
            set
            {
                SetProperty(ref ratedCapacity, value);
            }
        }
        /// <summary>
        /// 额定电压
        /// </summary>
        private string ratedVoltage;

        public string RatedVoltage
        {
            get => ratedVoltage;
            set
            {
                SetProperty(ref ratedVoltage, value);
            }
        }
        /// <summary>
        /// 额定电池串数量
        /// </summary>
        private string ratedBatteryNumber;

        public string RatedBatteryNumber
        {
            get => ratedBatteryNumber;
            set
            {
                SetProperty(ref ratedBatteryNumber, value);
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

        private SolidColorBrush standStateBCMU;
        public SolidColorBrush StandStateBCMU
        {
            get { return standStateBCMU; }
            set
            {
                SetProperty(ref standStateBCMU, value);
            }
        }

        private SolidColorBrush chargeStateBCMU;
        public SolidColorBrush ChargeStateBCMU
        {
            get { return chargeStateBCMU; }
            set
            {
                SetProperty(ref chargeStateBCMU, value);
            }
        }

        private SolidColorBrush disChargeStateBCMU;
        public SolidColorBrush DisChargeStateBCMU
        {
            get { return disChargeStateBCMU; }
            set
            {
                SetProperty(ref disChargeStateBCMU, value);
            }
        }

        private SolidColorBrush offNetStateBCMU;
        public SolidColorBrush OffNetStateBCMU
        {
            get { return offNetStateBCMU; }
            set
            {
                SetProperty(ref offNetStateBCMU, value);
            }
        }

        private string batMaxDischgPower;
        public string BatMaxDischgPower
        {
            get { return batMaxDischgPower; }
            set
            {
                SetProperty(ref batMaxDischgPower, value);
            }
        }

        private string totalDischgCoulomb;
        public string TotalDischgCoulomb
        {
            get { return totalDischgCoulomb; }
            set
            {
                SetProperty(ref totalDischgCoulomb, value);
            }
        }

        private string oneDischgCoulomb;
        public string OneDischgCoulomb
        {
            get { return oneDischgCoulomb; }
            set
            {
                SetProperty(ref oneDischgCoulomb, value);
            }
        }

        private string batMaxChgPower;
        public string BatMaxChgPower
        {
            get { return batMaxChgPower; }
            set
            {
                SetProperty(ref batMaxChgPower, value);
            }
        }

        private string totalChgCoulomb;
        public string TotalChgCoulomb
        {
            get { return totalChgCoulomb; }
            set
            {
                SetProperty(ref totalChgCoulomb, value);
            }
        }

        private string oneChgCoulomb;
        public string OneChgCoulomb
        {
            get { return oneChgCoulomb; }
            set
            {
                SetProperty(ref oneChgCoulomb, value);
            }
        }
       
        private string highCotainerTemperature1;
        public string HighCotainerTemperature1
        {
            get { return highCotainerTemperature1; }
            set
            {
                SetProperty(ref highCotainerTemperature1, value);
            }
        }

        private string highCotainerTemperature2;
        public string HighCotainerTemperature2
        {
            get { return highCotainerTemperature2; }
            set
            {
                SetProperty(ref highCotainerTemperature2, value);
            }
        }

        private string highCotainerTemperature3;
        public string HighCotainerTemperature3
        {
            get { return highCotainerTemperature3; }
            set
            {
                SetProperty(ref highCotainerTemperature3, value);
            }
        }

        private string highCotainerTemperature4;
        public string HighCotainerTemperature4
        {
            get { return highCotainerTemperature4; }
            set
            {
                SetProperty(ref highCotainerTemperature4, value);
            }
        }
        #endregion

        #region BCMU报警
        /// <summary>
        /// 单体电压低报警
        /// </summary>
        private AlarmtLevels singleVolLowAlarm;
        public AlarmtLevels SingleVolLowAlarm
        {
            get { return singleVolLowAlarm; }
            set
            {
                SetProperty(ref singleVolLowAlarm, value);
            }
        }

        /// <summary>
        /// 簇电压低报警
        /// </summary>
        private AlarmtLevels clusterVolLowAlarm;
        public AlarmtLevels ClusterVolLowAlarm
        {
            get { return clusterVolLowAlarm; }
            set
            {
                SetProperty(ref clusterVolLowAlarm, value);
            }
        }

        /// <summary>
        /// 单体电压高报警
        /// </summary>
        private AlarmtLevels singleVolUpAlarm;

        public AlarmtLevels SingleVolUpAlarm
        {
            get => singleVolUpAlarm;
            set
            {
                SetProperty(ref singleVolUpAlarm, value);
            }
        }

       /// <summary>
       /// 簇电压高报警
       /// </summary>
        private AlarmtLevels clusterVolUpAlarm;

        public AlarmtLevels ClusterVolUpAlarm
        {
            get => clusterVolUpAlarm;
            set
            {
                SetProperty(ref clusterVolUpAlarm, value);
            }
        }

        /// <summary>
        /// 充电温度低报警
        /// </summary>
        private AlarmtLevels charTempLowAlarm;

        public AlarmtLevels CharTempLowAlarm
        {
            get => charTempLowAlarm;
            set
            {
                SetProperty(ref charTempLowAlarm, value);
            }
        }

        /// <summary>
        /// 充电温度高报警
        /// </summary>
        private AlarmtLevels charTempUpAlarm;

        public AlarmtLevels CharTempUpAlarm
        {
            get => charTempUpAlarm;
            set
            {
                SetProperty(ref charTempUpAlarm, value);
            }
        }

        /// <summary>
        /// 放电温度高报警
        /// </summary>
        private AlarmtLevels discharTempUpAlarm;

        public AlarmtLevels DischarTempUpAlarm
        {
            get => discharTempUpAlarm;
            set
            {
                SetProperty(ref discharTempUpAlarm, value);
            }
        }

        /// <summary>
        /// 放电温度低报警
        /// </summary>
        private AlarmtLevels discharTempLowAlarm;

        public AlarmtLevels DischarTempLowAlarm
        {
            get => discharTempLowAlarm;
            set
            {
                SetProperty(ref discharTempLowAlarm, value);
            }
        }

        /// <summary>
        /// 充电过流告警
        /// </summary>
        private AlarmtLevels charClusterOverCurAlarm;

        public AlarmtLevels CharClusterOverCurAlarm
        {
            get => charClusterOverCurAlarm;
            set
            {
                SetProperty(ref charClusterOverCurAlarm, value);
            }
        }

        /// <summary>
        /// 放电过流告警
        /// </summary>
        private AlarmtLevels discharClusterOverCurAlarm;

        public AlarmtLevels DischarClusterOverCurAlarm
        {
            get => discharClusterOverCurAlarm;
            set
            {
                SetProperty(ref discharClusterOverCurAlarm, value);
            }
        }

        /// <summary>
        /// 单体压差大告警
        /// </summary>
        private AlarmtLevels singleVolDiffAlarm;

        public AlarmtLevels SingleVolDiffAlarm 
        {
            get => singleVolDiffAlarm;
            set
            {
                SetProperty(ref singleVolDiffAlarm, value);
            }
        }



        /// <summary>
        /// 低SOC告警
        /// </summary>
        private AlarmtLevels socLowAlarm;

        public AlarmtLevels SOCLowAlarm
        {
            get => socLowAlarm;
            set
            {
                SetProperty(ref socLowAlarm, value);
            }
        }


       
      

        /// <summary>
        /// 高压箱高温告警
        /// </summary>
        private AlarmtLevels highConTempTotalAlarm;

        public AlarmtLevels HighConTempTotalAlarm
        {
            get => highConTempTotalAlarm;
            set
            {
                SetProperty(ref highConTempTotalAlarm, value);
            }
        }

      
        private AlarmtLevels highConTemp1Alarm;

        public AlarmtLevels HighConTemp1Alarm
        {
            get => highConTemp1Alarm;
            set
            {
                SetProperty(ref highConTemp1Alarm, value);
            }
        }

        private AlarmtLevels highConTemp2Alarm;

        public AlarmtLevels HighConTemp2Alarm
        {
            get => highConTemp2Alarm;
            set
            {
                SetProperty(ref highConTemp2Alarm, value);
            }
        }

        private AlarmtLevels highConTemp3Alarm;

        public AlarmtLevels HighConTemp3Alarm
        {
            get => highConTemp3Alarm;
            set
            {
                SetProperty(ref highConTemp3Alarm, value);
            }
        }

        private AlarmtLevels highConTemp4Alarm;

        public AlarmtLevels HighConTemp4Alarm
        {
            get => highConTemp4Alarm;
            set
            {
                SetProperty(ref highConTemp4Alarm, value);
            }
        }
        /// <summary>
        /// 绝缘告警总
        /// </summary>
        private AlarmtLevels isoRTotalAlarm;

        public AlarmtLevels IsoRTotalAlarm
        {
            get => isoRTotalAlarm;
            set
            {
                SetProperty(ref isoRTotalAlarm, value);
            }
        }

        /// <summary>
        /// 绝缘RP异常低
        /// </summary>
        private AlarmtLevels isoRPLowAlarm;

        public AlarmtLevels IsoRPLowAlarm
        {
            get => isoRPLowAlarm;
            set
            {
                SetProperty(ref isoRPLowAlarm, value);
            }
        }
        /// <summary>
        /// 绝缘RN异常低
        /// </summary>
        private AlarmtLevels isoRNLowAlarm;

        public AlarmtLevels IsoRNLowAlarm
        {
            get => isoRNLowAlarm;
            set
            {
                SetProperty(ref isoRNLowAlarm, value);
            }
        }
        /// <summary>
        /// 绝缘HV与PE短路告警
        /// </summary>
        private AlarmtLevels hvPEShortALarm;

        public AlarmtLevels HVPEShortAlarm
        {
            get => hvPEShortALarm;
            set
            {
                SetProperty(ref hvPEShortALarm, value);
            }
        }

        /// <summary>
        /// 绝缘BAT-与PE短路告警
        /// </summary>
        private AlarmtLevels bATPEAlarm;

        public AlarmtLevels BATPEAlarm
        {
            get => bATPEAlarm;
            set
            {
                SetProperty(ref bATPEAlarm, value);
            }
        }
        #endregion

        #region BCMUFault
        /// <summary>
        /// 高压箱NTC连接故障
        /// </summary>
        private bool highConNTCTotalFault;

        public bool HighConNTCTotalFault
        {
            get => highConNTCTotalFault;
            set
            {
                SetProperty(ref highConNTCTotalFault, value);
            }
        }

        private bool highConNTC1Fault;

        public bool HighConNTC1Fault
        {
            get => highConNTC1Fault;
            set
            {
                SetProperty(ref highConNTC1Fault, value);
            }
        }

        private bool highConNTC2Fault;

        public bool HighConNTC2Fault
        {
            get => highConNTC2Fault;
            set
            {
                SetProperty(ref highConNTC2Fault, value);
            }
        }


        private bool highConNTC3Fault;

        public bool HighConNTC3Fault
        {
            get => highConNTC3Fault;
            set
            {
                SetProperty(ref highConNTC3Fault, value);
            }
        }

        private bool highConNTC4Fault;

        public bool HighConNTC4Fault
        {
            get => highConNTC4Fault;
            set
            {
                SetProperty(ref highConNTC4Fault, value);
            }
        }


        private bool canComFault;

        public bool CANComFault
        {
            get => canComFault;
            set
            {
                SetProperty(ref canComFault, value);
            }
        }

       /// <summary>
       /// 主回路继电器异常
       /// </summary>
        private bool mainLoopRelayFault;

        public bool MainLoopRelayFault
        {
            get => mainLoopRelayFault;
            set
            {
                SetProperty(ref mainLoopRelayFault, value);
            }
        }

        /// <summary>
        /// 预充继电器异常
        /// </summary>
        private bool precharRelayFault;

        public bool PrecharRelayFault
        {
            get => precharRelayFault;
            set
            {
                SetProperty(ref precharRelayFault, value);
            }
        }

        /// <summary>
        /// 断路器异常
        /// </summary>
        private bool breakerFault;

        public bool BreakerFault
        {
            get => breakerFault;
            set
            {
                SetProperty(ref breakerFault, value);
            }
        }

        /// <summary>
        /// 霍尔ADC I2C通讯异常
        /// </summary>
        private bool hallADCI2CComFault;

        public bool HallADCI2CComFault
        {
            get => hallADCI2CComFault;
            set
            {
                SetProperty(ref hallADCI2CComFault, value);
            }
        }

        /// <summary>
        /// 霍尔电流检测异常
        /// </summary>
        private bool hallCurDetectFault;

        public bool HallCurDetectFault
        {
            get => hallCurDetectFault;
            set
            {
                SetProperty(ref hallCurDetectFault, value);
            }
        }

        /// <summary>
        /// 高压DC电压ADCI2C通讯异常
        /// </summary>
        private bool highVolDCADCI2CComFault;

        public bool HighVolDCADCI2CComFault
        {
            get => highVolDCADCI2CComFault;
            set
            {
                SetProperty(ref highVolDCADCI2CComFault, value);
            }
        }

        /// <summary>
        /// 高压DC电压检测异常
        /// </summary>
        private bool highVolDCDetectFault;

        public bool HighVolDCDetectFault
        {
            get => highVolDCDetectFault;
            set
            {
                SetProperty(ref highVolDCDetectFault, value);
            }
        }

        /// <summary>
        /// 绝缘检测ADC I2C通讯异常
        /// </summary>
        private bool isoRADCI2CComFault;

        public bool IsoRADCI2CComFault
        {
            get => isoRADCI2CComFault;
            set
            {
                SetProperty(ref isoRADCI2CComFault, value);
            }
        }

        /// <summary>
        /// 绝缘检测异常
        /// </summary>
        private bool isoRDetectFault;

        public bool IsoRDetectFault
        {
            get => isoRDetectFault;
            set
            {
                SetProperty(ref isoRDetectFault, value);
            }
        }

        /// <summary>
        /// BMU 总故障
        /// </summary>
        private bool bmu1TotalFault;

        public bool BMU1TotalFault
        {
            get => bmu1TotalFault;
            set
            {
                SetProperty(ref bmu1TotalFault, value);
            }
        }

        private bool bmu2TotalFault;

        public bool BMU2TotalFault
        {
            get => bmu2TotalFault;
            set
            {
                SetProperty(ref bmu2TotalFault, value);
            }
        }

        private bool bmu3TotalFault;

        public bool BMU3TotalFault
        {
            get => bmu3TotalFault;
            set
            {
                SetProperty(ref bmu3TotalFault, value);
            }
        }

        /// <summary>
        /// 网络掉线故障
        /// </summary>
        private bool bmu1ConnectLostFault;

        public bool BMU1ConnectLostFault
        {
            get => bmu1ConnectLostFault;
            set
            {
                SetProperty(ref bmu1ConnectLostFault, value);
            }
        }

        private bool bmu2ConnectLostFault;

        public bool BMU2ConnectLostFault
        {
            get => bmu2ConnectLostFault;
            set
            {
                SetProperty(ref bmu2ConnectLostFault, value);
            }
        }


        private bool bmu3ConnectLostFault;

        public bool BMU3ConnectLostFault
        {
            get => bmu3ConnectLostFault;
            set
            {
                SetProperty(ref bmu3ConnectLostFault, value);
            }
        }
        #endregion
        #region BMUFault

        private bool bmuTotalVolFault;

        public bool BMUTotalVolFault
        {
            get => bmuTotalVolFault;
            set
            {
                SetProperty(ref bmuTotalVolFault, value);
            }
        }

        private bool bmuTotalTempFault;

        public bool BMUTotalTempFault
        {
            get => bmuTotalTempFault;
            set
            {
                SetProperty(ref bmuTotalTempFault, value);
            }
        }

        private bool bmuTotalBalanceFault;

        public bool BMUTotalBalanceFault
        {
            get => bmuTotalBalanceFault;
            set
            {
                SetProperty(ref bmuTotalBalanceFault, value);
            }
        }



        #endregion

        #region BMU故障


        private bool bmuVolFault;

        public bool BMUVolFault
        {
            get => bmuVolFault;
            set
            {
                SetProperty(ref bmuVolFault, value);
            }
        }

        private bool bmuTempFault;

        public bool BMUTempFault
        {
            get => bmuTempFault;
            set
            {
                SetProperty(ref bmuTempFault, value);
            }
        }

        private bool bmuBalanceFault;

        public bool BMUBalanceFault
        {
            get => bmuBalanceFault;
            set
            {
                SetProperty(ref bmuBalanceFault, value);
            }
        }

        #endregion


        /// <summary>
        /// 均衡模式选择
        /// </summary>
        private string[] balanceMode =new string[] { "自动均衡模式", "手动均衡模式" };

        public string[] BalanceMode
        {
            get => balanceMode;
            set
            {
                SetProperty(ref balanceMode, value);
            }
        }

        /// <summary>
        ///均衡bmu选择
        /// </summary>
        private string[] balanceBMU = new string[] { "A","B","C"};
        public string[] BalanceBMU
        {
            get { return balanceBMU; }
            set { SetProperty(ref balanceBMU, value); }
        }

        /// <summary>
        /// 均衡通道选择
        /// </summary>
        private string[] balanceChannels = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
       
        public string[] BalanceChannels
        {
            get
            {
                return balanceChannels;
            }
            set
            {
                SetProperty(ref balanceChannels, value);
            }
        }
        /// <summary>
        /// 被选择均衡的BMU通道
        /// </summary>
        private string selectedBalanceChannel;
       
        public string SelectedBalanceChannel
        {
            get
            {
                return selectedBalanceChannel;
            }
            set
            {
                SetProperty(ref selectedBalanceChannel, value);
            }
        }

        /// <summary>
        /// 被选择的BMU
        /// </summary>
        private string selectedBalanceBMU;
        public string SelectedBalanceBMU
        {
            get
            {
                return selectedBalanceBMU;
            }
            set
            {
                SetProperty(ref selectedBalanceBMU, value);
            }
        }

        /// <summary>
        /// 被选择的均衡模式
        /// </summary>
        private string selectedBalanceMode;

        public string SelectedBalanceMode
        {
            get => selectedBalanceMode;
            set
            {
                SetProperty(ref selectedBalanceMode, value);
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

        private string selectedCluster = "A";
        public string SelectedCluster
        {
            get { return selectedCluster; }
            set
            {
                SetProperty(ref selectedCluster, value);
            }
        }

        private string chargeCapacitySum;
        public string ChargeCapacitySum
        {
            get { return chargeCapacitySum; }
            set
            {
                SetProperty(ref chargeCapacitySum, value);
            }
        }

     

        private int bcmuFaultStateFlag1;
        public int BCMUFaultStateFlag1
        {
            get { return bcmuFaultStateFlag1; }
            set
            {
                SetProperty(ref bcmuFaultStateFlag1, value);
            }
        }
        private int bcmuFaultStateFlag2;
        public int BCMUFaultStateFlag2
        {
            get { return bcmuFaultStateFlag2; }
            set
            {
                SetProperty(ref bcmuFaultStateFlag2, value);
            }
        }
        private int bcmuFaultStateFlag3;
        public int BCMUFaultStateFlag3
        {
            get { return bcmuFaultStateFlag3; }
            set
            {
                SetProperty(ref bcmuFaultStateFlag3, value);
            }
        }

        private int bcmuAlarmStateFlag1;
        public int BCMUAlarmStateFlag1
        {
            get { return bcmuAlarmStateFlag1; }
            set
            {
                SetProperty(ref bcmuAlarmStateFlag1, value);
            }
        }
        private int bcmuAlarmStateFlag2;
        public int BCMUAlarmStateFlag2
        {
            get { return bcmuAlarmStateFlag2; }
            set
            {
                SetProperty(ref bcmuAlarmStateFlag2, value);
            }
        }
        private int bcmuAlarmStateFlag3;
        public int BCMUAlarmStateFlag3
        {
            get { return bcmuAlarmStateFlag3; }
            set
            {
                SetProperty(ref bcmuAlarmStateFlag3, value);
            }
        }

        private int stateBCMUFlag;
        public int StateBCMUFlag
        {
            get { return stateBCMUFlag; }
            set
            {
                SetProperty(ref stateBCMUFlag, value);

            }
        }



        private FaultLevels bcmuTotalFault;

        public FaultLevels BCMUTotalFault
        {
            get => bcmuTotalFault;
            set
            {
                SetProperty(ref bcmuTotalFault, value);
            }
        }


        private bool isOnGrid;

        public bool IsOnGrid
        {
            get => isOnGrid;
            set
            {
                SetProperty(ref isOnGrid, value);
            }
        }


        private BCMUStatus currentOri;

        public BCMUStatus CurrentOri
        {
            get => currentOri;
            set
            {
                SetProperty(ref currentOri, value);
            }
        }




        public BatteryViewModel[] BatteryViewModelList;

        #endregion


        #region Command

        public RelayCommand ToMonitor_BMS_BCMUPageCommand { get;private set;}
        public RelayCommand Command_OffGrid { get; private set; }
        public RelayCommand Command_OnGrid { get; private set; }
        public RelayCommand Command_ResetFault { get; private set; }
        public RelayCommand Command_ChooseBalanceMode { get;private set; }
        public RelayCommand Command_OpenBalanceChannel {  get;private set; }
        public RelayCommand Command_CloseBalanceChannel { get;private set; }
        #endregion
        private string id { get; set; }
        public Monitor_BMS_BCMUPageModel(string id)
        {
            this.id =id;
            ToMonitor_BMS_BCMUPageCommand = new RelayCommand(ToMonitor_BMS_BCMUPage);
            Command_OffGrid = new RelayCommand(OffGridCommand);
            Command_OnGrid = new RelayCommand(OnGridCommand);
            Command_ResetFault = new RelayCommand(ResetFault);
            Command_ChooseBalanceMode = new RelayCommand(ChooseBalanceMode);
            Command_OpenBalanceChannel = new RelayCommand(OpenBalanceChannel);
            Command_CloseBalanceChannel = new RelayCommand(CloseBalanceChannel);
            BatteryViewModelList = new BatteryViewModel[14];
            for (int i = 0; i < BatteryViewModelList.Length; i++)
            {
                BatteryViewModelList[i] = new BatteryViewModel();
            }
        }

        private void CloseBalanceChannel()
        {
            BmsApi.SendBalanceChannel(id, 0);
        }

        private void OpenBalanceChannel()
        {
            int value=0;
            int.TryParse(SelectedBalanceChannel, out int channels);
            switch (SelectedBalanceBMU)
            {
                case "A":
                    {
                        value = channels; 
                    }
                     break;
                    case "B":
                    {
                        value = channels + 16;
                    }
                    break;
                case "C":
                    {
                        value = channels + 32;
                    }
                    break;

            }
            BmsApi.SendBalanceChannel(id,(ushort)value);
        }

        private void ChooseBalanceMode()
        {
            switch (SelectedBalanceMode)
            {
                case "自动均衡模式": 
                    {
                        BmsApi.SendBCMUBalanceMode(id, 0x005A);
                    }break;
                case "手动均衡模式":
                    {
                        BmsApi.SendBCMUBalanceMode(id, 0x00A5);
                    }break;

            }
        }

        private void ResetFault()
        {
            BmsApi.ResetBMSFault(id);
        }

        private void OnGridCommand()
        {
            BmsApi.Connect2DcBus(id);
        }

        private void OffGridCommand()
        {
            BmsApi.Disconnect2DcBus(id);
        }

        public void DataDistribution(BatteryTotalModel model)
        {
            try
            {
                RemainingSOC = model.TotalSOC.ToString();
                ClusterVoltage = model.TotalVoltage.ToString();
                PresentCurrent = model.TotalCurrent.ToString();
                AvgClusterVol = model.AvgVol.ToString();
                AvgClusterTemp = model.AverageTemperature.ToString();
                MaxCellVoltage = model.MaxVoltage.ToString();
                MinCellVoltage = model.MinVoltage.ToString();
                MaxTemperature = model.MaxTemperature.ToString();
                MinTemperature = model.MinTemperature.ToString();
                MaxCellVoltageIndex = model.MaxVoltageIndex.ToString();
                MinCellVoltageIndex = model.MinVoltageIndex.ToString();
                MaxTemperatureIndex = model.MaxTemperatureIndex.ToString();
                MinTemperatureIndex = model.MinTemperatureIndex.ToString();
                RatedBatteryNumber = model.BatteryCount.ToString();
                RatedCapacity = model.NomCapacity.ToString();
                RatedVoltage = model.NomVoltage.ToString();
                BCMUFaultStateFlag1 = model.FaultStateBCMUTotalFlag;
                BCMUFaultStateFlag2 = model.FaultStateBCMUFlag1;
                BCMUFaultStateFlag3 = model.FaultStateBCMUFlag2;
                BCMUAlarmStateFlag1 = model.AlarmStateBCMUFlag1;
                BCMUAlarmStateFlag2 = model.AlarmStateBCMUFlag2;
                BCMUAlarmStateFlag3 = model.AlarmStateBCMUFlag3;
                StateBCMUFlag = model.StateBCMU;
                HighCotainerTemperature1 = model.VolContainerTemperature1.ToString();
                HighCotainerTemperature2 = model.VolContainerTemperature2.ToString();
                HighCotainerTemperature3 = model.VolContainerTemperature3.ToString();
                HighCotainerTemperature4 = model.VolContainerTemperature4.ToString();
                ChargeChannelStateNumber = model.BalanceChannel.ToString();
                App.Current.Dispatcher.Invoke(() =>
                {
                    StateBCMUChange(StateBCMUFlag);
                    bool faultResult = AnalyseBCMUFault(BCMUFaultStateFlag1, BCMUFaultStateFlag2, BCMUFaultStateFlag3);
                    int alarmResult =AnalyseBCMUAlarm(BCMUAlarmStateFlag1, BCMUAlarmStateFlag2, BCMUAlarmStateFlag3);
                    BMUInfo(model);
                    if (faultResult)
                    {
                        BCMUTotalFault = FaultLevels.Error;
                    }
                    else
                    {
                        switch(alarmResult)
                        {
                            case 0:
                                {
                                    BCMUTotalFault = FaultLevels.NoAlarm;
                                }break;
                                case 1:
                                {
                                    BCMUTotalFault = FaultLevels.Info;
                                }break;
                                case 2:
                                {
                                    BCMUTotalFault = FaultLevels.Warning;
                                }break;
                                case 3:
                                {
                                    BCMUTotalFault = FaultLevels.Error;
                                }break;
                        }
                    }
                });
                

                
            }
            catch (Exception ex)
            {
                LogUtils.Error($"BCMU{id}",ex);
                throw ex;
            }
          
        }


        private void StateBCMUChange(int state)
        {
            if (state == 1)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                isOnGrid = true;
                CurrentOri = BCMUStatus.Charge;


            }
            else if (state == 2)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                IsOnGrid = true;
                CurrentOri = BCMUStatus.Discharge;
            }
            else if (state == 3)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                IsOnGrid= true;
                CurrentOri = BCMUStatus.Stand;
            }
            else if (state == 4)
            {
                ChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                DisChargeStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                StandStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D1D1"));
                OffNetStateBCMU = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33FF33"));
                IsOnGrid = false;
            }
        }
        public void StateDistribution(bool isconnected, bool isdaqdata,bool issavedata)
        {
            if (isconnected) 
            { 

            }

            if (isdaqdata)
            {

            }
        }


        private void ToMonitor_BMS_BCMUPage()
        {
            throw new NotImplementedException();
        }

      
       

        /// <summary>
        /// BMU信息解读
        /// </summary>
        /// <param name="obj">入参</param>
        private void BMUInfo(BatteryTotalModel model)
        {  
            int i = Array.IndexOf(Cluster, SelectedCluster);
            AnlyseBMUFault(model.Series[i].VolFaultInfo, model.Series[i].TempFaultInfo1, model.Series[i].TempFaultInfo2, model.Series[i].BalanceFaultFaultInfo);
            BatteryInfo(model.Series[i]);

        }

        private void BatteryInfo(BatterySeriesModel model)
        {
            for (int j = 0; j < BatteryViewModelList.Length; j++)
            {
                BatteryViewModelList[j].Voltage = model.Batteries[j].Voltage;
                BatteryViewModelList[j].Temperature1 = model.Batteries[j].Temperature1;
                BatteryViewModelList[j].Temperature2 = model.Batteries[j].Temperature2;
                BatteryViewModelList[j].SOC = model.Batteries[j].SOC;
                BatteryViewModelList[j].SOH = model.Batteries[j].SOH;
                BatteryViewModelList[j].Resistance = model.Batteries[j].Resistance;
                BatteryViewModelList[j].Capacity = model.Batteries[j].Capacity;
                BatteryViewModelList[j].BatteryNumber = j + 1;
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (j == (model.MaxVoltageIndex - 1))
                    {
                        BatteryViewModelList[j].VoltageColor = new SolidColorBrush(Colors.Red);
                    }
                    else if (j == (model.MinVoltageIndex - 1))
                    {
                        BatteryViewModelList[j].VoltageColor = new SolidColorBrush(Colors.LightBlue);
                    }
                    else
                    {
                        BatteryViewModelList[j].VoltageColor = new SolidColorBrush(Colors.Black);
                    }

                    if (j == (model.MaxTemperatureIndex - 1))
                    {
                        BatteryViewModelList[j].TemperatureColor = new SolidColorBrush(Colors.Red);
                    }
                    else if (j == (model.MinTemperatureIndex - 1))
                    {
                        BatteryViewModelList[j].TemperatureColor = new SolidColorBrush(Colors.LightBlue);
                    }
                    else
                    {
                        BatteryViewModelList[j].TemperatureColor = new SolidColorBrush(Colors.Black);
                    }
                });
                
            }
        }

        /// <summary>
        /// 分析BCMU故障
        /// </summary>
        /// <param name="flag1"></param>
        /// <param name="flag2"></param>
        /// <param name="flag3"></param>
        private bool AnalyseBCMUFault(int flag1, int flag2, int flag3)
        {
            bool faultresult = false;
            List<int> flag1bitposition = new List<int>();
            List<int> flag2bitposition = new List<int>();
            List<int> flag3bitposition = new List<int>();
            flag1bitposition = GetBitPosition(flag1);
            flag2bitposition = GetBitPosition(flag2);
            flag3bitposition = GetBitPosition(flag3);
            if (flag1bitposition.Count != 0 || flag2bitposition.Count != 0 || flag3bitposition.Count != 0)
            {
                faultresult = true;
            }
            else
            {
                faultresult = false;
            }
            foreach (var item in flag1bitposition)
            {
                switch (item)
                {

                    case 2:
                        {
                            BMU1TotalFault = true;
                        }
                        break;
                    case 3:
                        {
                            BMU2TotalFault = true;
                        }
                        break;
                    case 4:
                        {
                            BMU3TotalFault = true;
                        }
                        break;
                    case 8:
                        {
                            BMU1ConnectLostFault = true;
                        }
                        break;
                    case 9:
                        {
                            BMU2ConnectLostFault = true;
                        }
                        break;
                    case 10:
                        {
                            BMU3ConnectLostFault = true;
                        }
                        break;
                    default:
                        {
                            BMU1TotalFault = false;
                            BMU2TotalFault = false;
                            BMU3TotalFault = false;
                            BMU1ConnectLostFault = false;
                            BMU2ConnectLostFault = false;
                            BMU3ConnectLostFault = false;

                        }
                        break;
                }
            }

            foreach (var item in flag2bitposition)
            {
                switch (item)
                {
                    case 0:
                        {
                            MainLoopRelayFault = true;
                        }
                        break;
                    case 1:
                        {
                            PrecharRelayFault = true;
                        }
                        break;
                    case 2:
                        {
                            BreakerFault = true;
                        }
                        break;
                    case 8:
                        {
                            HallADCI2CComFault = true;
                        }
                        break;
                    case 9:
                        {
                            HallCurDetectFault = true;
                        }
                        break;
                    case 10:
                        {
                            HighVolDCADCI2CComFault = true;
                        }
                        break;
                    case 11:
                        {
                            HighVolDCDetectFault = true;
                        }
                        break;

                    case 14:
                        {
                            IsoRADCI2CComFault = true;
                        }
                        break;
                    case 15:
                        {
                            IsoRDetectFault = true;
                        }
                        break;
                    default:
                        {
                            MainLoopRelayFault = false;
                            PrecharRelayFault = false;
                            BreakerFault = false;
                            HallADCI2CComFault = false;
                            HallCurDetectFault = false;
                            HighVolDCADCI2CComFault = false;
                            HighVolDCDetectFault = false;
                            IsoRADCI2CComFault = false;
                            IsoRDetectFault = false;

                        }
                        break;
                }
            }
            foreach (var item in flag3bitposition)
            {
                switch (item)
                {

                    case 8:
                        {
                            HighConNTCTotalFault = true;
                            HighConNTC1Fault = true;
                        }
                        break;
                    case 9:
                        {
                            HighConNTCTotalFault = true;
                            HighConNTC2Fault = true;
                        }
                        break;
                    case 10:
                        {
                            HighConNTCTotalFault = true;
                            HighConNTC3Fault = true;
                        }
                        break;
                    case 11:
                        {
                            HighConNTCTotalFault = true;
                            HighConNTC4Fault = true;
                        }
                        break;
                    default:
                        {
                            HighConNTCTotalFault = false;
                            HighConNTC1Fault = false;
                            HighConNTC2Fault = false;
                            HighConNTC3Fault = false;
                            HighConNTC4Fault = false;

                        }
                        break;

                }
            }
            return faultresult;
        }

        /// <summary>
        /// 分析BCMU告警
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private int AnalyseBCMUAlarm(int value1, int value2, int value3)
        {
            int colorflag1;
            int colorflag2;
            int colorflag3;
            int colorflag4;
            int j1 = 0;
            int j2 = 0;
            if ((value1 & 0x0100) != 0) { IsoRPLowAlarm = AlarmtLevels.Error;} else { IsoRNLowAlarm = AlarmtLevels.NoAlarm; }      //bit0
            if ((value1 & 0x0200) != 0) { IsoRNLowAlarm = AlarmtLevels.Error;   } else { IsoRNLowAlarm = AlarmtLevels.NoAlarm; } //bit1
            if ((value1 & 0x0400) != 0) { HVPEShortAlarm = AlarmtLevels.Error; } else { HVPEShortAlarm = AlarmtLevels.NoAlarm;  }//bit2
            if ((value1 & 0x0800) != 0) { BATPEAlarm = AlarmtLevels.Error; } else { BATPEAlarm = AlarmtLevels.NoAlarm; }  //bit3
            if ((IsoRPLowAlarm == AlarmtLevels.Error) || (IsoRNLowAlarm == AlarmtLevels.Error) || (HVPEShortAlarm == AlarmtLevels.Error) || (BATPEAlarm == AlarmtLevels.Error))
            {
                IsoRTotalAlarm = AlarmtLevels.Error;
                colorflag1 = 3;
            }
            else
            {
                IsoRTotalAlarm = AlarmtLevels.NoAlarm;
                colorflag1 = 0;
            }

            List<int> result21 = new List<int>();
            result21 = GetBitPosition(value2);
            
            foreach (var item in result21)
            {
                switch (item)
                {
                    case 0:
                        {
                            HighConTemp1Alarm = AlarmtLevels.Warning;
                            HighConTempTotalAlarm = AlarmtLevels.Warning;
                            
                        }
                        break;
                    case 1:
                        {
                            HighConTemp2Alarm = AlarmtLevels.Warning;
                            HighConTempTotalAlarm = AlarmtLevels.Warning;
                            
                        }
                        break;
                    case 2:
                        {
                            HighConTemp3Alarm = AlarmtLevels.Warning;
                            HighConTempTotalAlarm = AlarmtLevels.Warning;
                            
                        }
                        break;
                    case 3:
                        {
                            HighConTemp4Alarm = AlarmtLevels.Warning;
                            HighConTempTotalAlarm = AlarmtLevels.Warning;
                            
                        }
                        break;
                    default:
                        {
                            HighConTemp1Alarm = AlarmtLevels.NoAlarm;
                            HighConTemp2Alarm = AlarmtLevels.NoAlarm;
                            HighConTemp3Alarm = AlarmtLevels.NoAlarm;
                            HighConTemp4Alarm = AlarmtLevels.NoAlarm;
                            HighConTempTotalAlarm = AlarmtLevels.NoAlarm;
                            
                        }
                        break;
                }
            }
            if(HighConTempTotalAlarm == AlarmtLevels.NoAlarm)
            {
                colorflag2 = 0;
            }
            else
            {
                colorflag2 = 2;
            }

            Dictionary<int, int> result2 = new Dictionary<int, int>();

            Dictionary<int, int> result3 = new Dictionary<int, int>();
            for (int i = 8; i < 16; i += 2)
            {
                int twoBitValue = (value2 >> i) & 0x3;
                j1 = i / 2;
                if (twoBitValue != 0)
                {

                    result2.Add(j1, twoBitValue);
                }
            }
            for (int i = 0; i < 16; i += 2)
            {
                int twoBitValue = (value3 >> i) & 0x3;
                j2 = i / 2;
                if (twoBitValue != 0)
                {

                    result3.Add(j2, twoBitValue);
                }
            }
            if(result2.Count!=0)
            {
                colorflag3 = result2.Max(pair => pair.Value);
            }
            else
            {
                colorflag3 = 0;
            }
           if(result3.Count!=0)
            {
                colorflag4 = result3.Max(pair => pair.Value);
            }
            else
            {
                colorflag4 = 0;
            }
           
            int colorflagresult = Math.Max(colorflag1,colorflag2);
            colorflagresult = Math.Max(colorflagresult,colorflag3);
            colorflagresult = Math.Max(colorflagresult,colorflag4);
            
            foreach (var item in result2)
            {

                switch (item.Key)
                {

                    case 4:
                        {
                            if (item.Value == 1) { SingleVolLowAlarm = AlarmtLevels.Info;  }
                            if (item.Value == 2) {SingleVolLowAlarm = AlarmtLevels.Warning;  }
                            if (item.Value == 3) { SingleVolLowAlarm = AlarmtLevels.Error;  }
                            }
                        break;
                    case 5:
                        {
                            if (item.Value == 1) {SingleVolUpAlarm = AlarmtLevels.Info;  }
                            if (item.Value == 2) {SingleVolUpAlarm = AlarmtLevels.Warning;  }
                            if (item.Value == 3) {SingleVolUpAlarm = AlarmtLevels.Error;  }
                        }
                        break;
                    case 6:
                        {
                            if (item.Value == 1){ ClusterVolLowAlarm = AlarmtLevels.Info;   }
                            if (item.Value == 2) { ClusterVolLowAlarm = AlarmtLevels.Warning; }
                                if (item.Value == 3){ ClusterVolLowAlarm = AlarmtLevels.Error;  }
                            }
                        break;
                    case 7:
                        {
                            if (item.Value == 1){ ClusterVolUpAlarm = AlarmtLevels.Info;  }
                            if (item.Value == 2){ ClusterVolUpAlarm = AlarmtLevels.Warning; }
                            if (item.Value == 3){ ClusterVolUpAlarm = AlarmtLevels.Error;  }
                        }
                        break;
                    default:
                        {
                            ClusterVolUpAlarm = AlarmtLevels.NoAlarm;
                            ClusterVolLowAlarm = AlarmtLevels.NoAlarm;
                            SingleVolUpAlarm = AlarmtLevels.NoAlarm;
                            SingleVolLowAlarm = AlarmtLevels.NoAlarm;

                        }
                        break;
                }
            }

            foreach (var item in result3)
            {
                switch (item.Key)
                {
                    case 0:
                        {
                            if (item.Value == 1) CharTempLowAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) CharTempLowAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) CharTempLowAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 1:
                        {
                            if (item.Value == 1) CharTempUpAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) CharTempUpAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) CharTempUpAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 2:
                        {
                            if (item.Value == 1) DischarTempLowAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) DischarTempLowAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) DischarTempLowAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 3:
                        {
                            if (item.Value == 1) DischarTempUpAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) DischarTempUpAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) DischarTempUpAlarm = AlarmtLevels.Error;
                        }
                        break;

                    case 4:
                        {
                            if (item.Value == 1) CharClusterOverCurAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) CharClusterOverCurAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) CharClusterOverCurAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 5:
                        {
                            if (item.Value == 1) DischarClusterOverCurAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) DischarClusterOverCurAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) DischarClusterOverCurAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 6:
                        {
                            if (item.Value == 1) SingleVolDiffAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) SingleVolDiffAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) SingleVolDiffAlarm = AlarmtLevels.Error;
                        }
                        break;
                    case 7:
                        {
                            if (item.Value == 1) SOCLowAlarm = AlarmtLevels.Info;
                            if (item.Value == 2) SOCLowAlarm = AlarmtLevels.Warning;
                            if (item.Value == 3) SOCLowAlarm = AlarmtLevels.Error;
                        }
                        break;
                    default:
                        {
                            SOCLowAlarm = AlarmtLevels.NoAlarm;
                            SingleVolDiffAlarm = AlarmtLevels.NoAlarm;
                            DischarClusterOverCurAlarm = AlarmtLevels.NoAlarm;
                            CharClusterOverCurAlarm = AlarmtLevels.NoAlarm;
                            DischarTempUpAlarm = AlarmtLevels.NoAlarm;
                            DischarTempLowAlarm = AlarmtLevels.NoAlarm;
                            CharTempUpAlarm = AlarmtLevels.NoAlarm;
                            CharTempLowAlarm = AlarmtLevels.NoAlarm;
                        }
                        break;
                }

            }
            return colorflagresult;

        }
        /// <summary>
        /// 分析BMU故障
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void AnlyseBMUFault(int volfaultflag,int temp1faultflag1,int temp2fault2flag2,int balancefaultflag)
        {
            List<int> volinfos = new List<int>();
            List<int> tempinfos1 = new List<int>();
            List<int>tempinfos2 = new List<int>();
            List<int>balanceinfos = new List<int>() ;
            volinfos = GetBitPosition(volfaultflag);
            tempinfos1 = GetBitPosition(temp1faultflag1);
            tempinfos2 = GetBitPosition(temp2fault2flag2);
            balanceinfos = GetBitPosition(balancefaultflag);
            if (volinfos.Count > 0) { BMUVolFault = true; } else { BMUVolFault = false; }
            if(tempinfos1.Count> 0|| tempinfos2.Count>0) { BMUTempFault = true; }else { BMUTempFault = false; }
            if (balanceinfos.Count > 0) {  BMUBalanceFault = true; } else { BMUBalanceFault = false; }
            //将电池的状态赋值未做
        }

        /// <summary>
        /// 获取第几位非0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<int> GetBitPosition(int value)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                int mask = 1 << i;
                if ((value & mask) != 0)
                {
                    result.Add(i);
                }
            }
            return result;
        }


    }
}
