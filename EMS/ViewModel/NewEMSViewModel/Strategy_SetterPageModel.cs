using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Model;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Strategy_SetterPageModel : ViewModelBase
    {
        /// <summary>
        /// 当前执行模式
        /// </summary>
        private string totalStrategyState;
        public string TotalStrategyState
        {
            get { return totalStrategyState; }
            set
            {
                SetProperty(ref totalStrategyState, value);
            }
        }

        /// <summary>
        /// 模式
        /// </summary>
        private List<string> strategyModeSet = new List<string> { "待机", "恒电流充电", "恒电流放电", "恒功率充电", "恒功率放电" };
        public List<string> StrategyModeSet
        {
            get { return strategyModeSet; }
            set
            {
                SetProperty(ref strategyModeSet, value);
            }
        }

        private string selectedManualStrategyMode;
        public string SelectedManualStrategyMode
        {
            get { return selectedManualStrategyMode; }
            set
            {
                SetProperty(ref selectedManualStrategyMode, value);
            }
        }

        /// <summary>
        /// 设置值
        /// </summary>       
        private double strategyManualValueSet;
        public double StrategyManualValueSet
        {
            get { return strategyManualValueSet; }
            set
            {
                SetProperty(ref strategyManualValueSet, value);
            }
        }
        #region Command   
        public RelayCommand SwitchAutoManualCommand { get; set; }
        /// <summary>
        /// 充放电策略控制
        /// </summary>
        private bool isDailyPatternBtnOpen;
        public bool IsDailyPatternBtnOpen
        {
            get { return isDailyPatternBtnOpen; }
            set
            {
                SetProperty(ref isDailyPatternBtnOpen, value);
            }
        }
        private bool isAutoStrategyControlBtnEnabled = true;
        public bool IsAutoStrategyControlBtnEnabled
        {
            get { return isAutoStrategyControlBtnEnabled; }
            set
            {
                SetProperty(ref isAutoStrategyControlBtnEnabled, value);
            }
        }
        public RelayCommand StrategyControlStartStopCommand { get; set; }
        /// <summary>
        /// 需量控制
        /// </summary>
        private bool isMaxDemandControlBtnOpen;
        public bool IsMaxDemandControlBtnOpen
        {
            get { return isMaxDemandControlBtnOpen; }
            set
            {
                SetProperty(ref isMaxDemandControlBtnOpen, value);
            }
        }
        private bool isAutoDemandControlBtnEnabled = true;
        public bool IsAutoDemandControlBtnEnabled
        {
            get { return isAutoDemandControlBtnEnabled; }
            set
            {
                SetProperty(ref isAutoDemandControlBtnEnabled, value);
            }
        }
        public RelayCommand DemandControlStartStopCommand { get; set; }
        /// <summary>
        /// 逆功率控制
        /// </summary>
        private bool isReversePowerBtnOpen;
        public bool IsReversePowerBtnOpen
        {
            get { return isReversePowerBtnOpen; }
            set
            {
                SetProperty(ref isReversePowerBtnOpen, value);
            }
        }
        private bool isAutoReversePowerBtnEnabled = true;
        public bool IsAutoReversePowerBtnEnabled
        {
            get { return isAutoReversePowerBtnEnabled; }
            set
            {
                SetProperty(ref isAutoReversePowerBtnEnabled, value);
            }
        }
        public RelayCommand InversePowerProtectionStartStopCommand { get; set; }
        /// <summary>
        /// 重置
        /// </summary>
        private bool isEnabled_ManualReset = true;
        public bool IsEnabled_ManualReset
        {
            get { return isEnabled_ManualReset; }
            set
            {
                SetProperty(ref isEnabled_ManualReset, value);
            }
        }
        public RelayCommand CommandManualReset { get; set; }
        /// <summary>
        /// 应用
        /// </summary>
        private bool isEnabled_ManualApply = true;
        public bool IsEnabled_ManualApply
        {
            get { return isEnabled_ManualApply; }
            set
            {
                SetProperty(ref isEnabled_ManualApply, value);
            }
        }
        public RelayCommand CommandManualApply { get; set; }
        /// <summary>
        /// 重置
        /// </summary>
        private bool isEnabled_AutoReset = true;
        public bool IsEnabled_AutoReset
        {
            get { return isEnabled_AutoReset; }
            set
            {
                SetProperty(ref isEnabled_AutoReset, value);
            }
        }
        public RelayCommand CommandAutoReset { get; set; }
        /// <summary>
        /// 应用
        /// </summary>
        private bool isAutoStrategyBtnEnabled = true;
        public bool IsAutoStrategyBtnEnabled
        {
            get { return isAutoStrategyBtnEnabled; }
            set
            {
                SetProperty(ref isAutoStrategyBtnEnabled, value);
            }
        }
        public RelayCommand ReversePowerSendCommand { get; set; }


        
        #endregion

        #region AutoRunModePara
        /*最大SOC*/
        private double maxSOC;
        public double MaxSOC
        {
            get { return maxSOC; }
            set
            {
                SetProperty(ref maxSOC, value);
            }
        }

        /*最小SOC*/
        private double minSOC;
        public double MinSOC
        {
            get { return minSOC; }
            set
            {
                SetProperty(ref minSOC, value);
            }
        }

        /*最大允许充电功率*/
        private double maxAllowChargingPower;
        public double MaxAllowChargingPower
        {
            get { return maxAllowChargingPower; }
            set
            {
                SetProperty(ref maxAllowChargingPower, value);
            }
        }

        /*最大允许放电功率*/
        private double maxAllowDishargingPower;
        public double MaxAllowDishargingPower
        {
            get { return maxAllowDishargingPower; }
            set
            {
                SetProperty(ref maxAllowDishargingPower, value);
            }
        }

        /*储能充电效率*/
        private double energyChargingEfficiency;
        public double EnergyChargingEfficiency
        {
            get { return energyChargingEfficiency; }
            set
            {
                SetProperty(ref energyChargingEfficiency, value);
            }
        }

        /*储能放电效率*/
        private double energyDishargingEfficiency;
        public double EnergyDishargingEfficiency
        {
            get { return energyDishargingEfficiency; }
            set
            {
                SetProperty(ref energyDishargingEfficiency, value);
            }
        }

        /*储能初始能量*/
        private double initialEnergyStorage;
        public double InitialEnergyStorage
        {
            get { return initialEnergyStorage; }
            set
            {
                SetProperty(ref initialEnergyStorage, value);
            }
        }

        /*储能可用容量*/
        private double availableEnergyStorageCapacity;
        public double AvailableEnergyStorageCapacity
        {
            get { return availableEnergyStorageCapacity; }
            set
            {
                SetProperty(ref availableEnergyStorageCapacity, value);
            }
        }

        /*需量控制容量*/
        private double demandControlCapacity;
        public double DemandControlCapacity
        {
            get { return demandControlCapacity; }
            set
            {
                SetProperty(ref demandControlCapacity, value);
            }
        }

        /*下设比例*/
        private double maxDemandDescendRate;
        public double MaxDemandDescendRate
        {
            get { return maxDemandDescendRate; }
            set
            {
                SetProperty(ref maxDemandDescendRate, value);
            }
        }

        /*逆功率保护限制*/
        private double reversePowerThreshold;
        public double ReversePowerThreshold
        {
            get { return reversePowerThreshold; }
            set
            {
                SetProperty(ref reversePowerThreshold, value);
            }
        }

        /*逆功率保护动作限值*/
        private double reversePowerLowestThreshold;
        public double ReversePowerLowestThreshold
        {
            get { return reversePowerLowestThreshold; }
            set
            {
                SetProperty(ref reversePowerLowestThreshold, value);
            }
        }

        /*下设比例*/
        private double reversePowerDescendRate;
        public double ReversePowerDescendRate
        {
            get { return reversePowerDescendRate; }
            set
            {
                SetProperty(ref reversePowerDescendRate, value);
            }
        }
        #endregion

        public Strategy_SetterPageModel()
        {
            SwitchAutoManualCommand = new RelayCommand(SwitchAutoManual, () => true);
            StrategyControlStartStopCommand = new RelayCommand(StrategyControlStartStop);
            DemandControlStartStopCommand = new RelayCommand(DemandControlStartStop);
            InversePowerProtectionStartStopCommand = new RelayCommand(InversePowerProtectionStartStop);
            CommandManualReset = new RelayCommand(ManualReset);
            CommandManualApply = new RelayCommand(ManualApply);
            CommandAutoReset = new RelayCommand(AutoReset);
            ReversePowerSendCommand = new RelayCommand(ReversePowerSend);
            
        }

        

        private void SwitchAutoManual()
        {
            //模式切换
            TotalStrategyState = TotalStrategyState == "手动运行" ? "自动运行" : "手动运行";
            bool automationMode = TotalStrategyState == "手动运行" ? true : false;
            StrategyApi.SetMode(automationMode, this.IsDailyPatternBtnOpen, this.IsMaxDemandControlBtnOpen, this.IsReversePowerBtnOpen);
        }
        /// <summary>
        /// 充放电策略控制启停，需量控制启停，逆功率保护启停
        /// </summary>
        private void StrategyControlStartStop()
        {
            
            //充放电策略控制启停
            bool automationMode = TotalStrategyState == "手动运行" ? true : false;
            StrategyApi.SetMode(automationMode, this.IsDailyPatternBtnOpen, this.IsMaxDemandControlBtnOpen, this.IsReversePowerBtnOpen);
        }
        private void DemandControlStartStop()
        {
            //需量控制启停
            bool automationMode = TotalStrategyState == "手动运行" ? true : false;
            StrategyApi.SetMode(automationMode, this.IsDailyPatternBtnOpen, this.IsMaxDemandControlBtnOpen, this.IsReversePowerBtnOpen);
        }
        private void InversePowerProtectionStartStop()
        {
            //
            bool automationMode = TotalStrategyState == "手动运行" ? true : false;
            StrategyApi.SetMode(automationMode, this.IsDailyPatternBtnOpen, this.IsMaxDemandControlBtnOpen, this.IsReversePowerBtnOpen);
        }
        /// <summary>
        /// 手动运行的重置
        /// </summary>
        private void ManualReset()
        {
            if (SelectedManualStrategyMode == "待机")
            {
                BessCommand currentCommand = StrategyApi.GetManualCommand();
                if (currentCommand.BatteryStrategy == (BatteryStrategyEnum)SelectedManualMode.Standby)
                {
                    this.StrategyManualValueSet = currentCommand.Value;
                }
            }
            if (SelectedManualStrategyMode == "恒电流充电")
            {
                BessCommand currentCommand = StrategyApi.GetManualCommand();
                if (currentCommand.BatteryStrategy == (BatteryStrategyEnum)SelectedManualMode.ConstantCurrentCharge)
                {
                    this.StrategyManualValueSet = currentCommand.Value;
                }
            }
            if (SelectedManualStrategyMode == "恒电流放电")
            {
                BessCommand currentCommand = StrategyApi.GetManualCommand();
                if (currentCommand.BatteryStrategy == (BatteryStrategyEnum)SelectedManualMode.ConstantCurrentDischarge)
                {
                    this.StrategyManualValueSet = currentCommand.Value;
                }
            }
            if (SelectedManualStrategyMode == "恒功率充电")
            {
                BessCommand currentCommand = StrategyApi.GetManualCommand();
                if (currentCommand.BatteryStrategy == (BatteryStrategyEnum)SelectedManualMode.ConstantPowerCharge)
                {
                    this.StrategyManualValueSet = currentCommand.Value;
                }
            }
            if (SelectedManualStrategyMode == "恒功率放电")
            {
                BessCommand currentCommand = StrategyApi.GetManualCommand();
                if (currentCommand.BatteryStrategy == (BatteryStrategyEnum)SelectedManualMode.ConstantPowerDischarge)
                {
                    this.StrategyManualValueSet = currentCommand.Value;
                }
            }
        }
        /// <summary>
        /// 手动运行的应用
        /// </summary>
        private void ManualApply()
        {
            if (SelectedManualStrategyMode == "待机")
            {
                var manualCommand = new BessCommand(this.StrategyManualValueSet, (BatteryStrategyEnum)SelectedManualMode.Standby);
                //StrategyApi.SetManualCommand(manualCommand);
                PcsApi.SendPcsCommand(manualCommand);
            }
            if (SelectedManualStrategyMode == "恒电流充电")
            {
                var manualCommand = new BessCommand(this.StrategyManualValueSet, (BatteryStrategyEnum)SelectedManualMode.ConstantCurrentCharge);
                //StrategyApi.SetManualCommand(manualCommand);
                PcsApi.SendPcsCommand(manualCommand);
            }
            if (SelectedManualStrategyMode == "恒电流放电")
            {
                var manualCommand = new BessCommand(this.StrategyManualValueSet, (BatteryStrategyEnum)SelectedManualMode.ConstantCurrentDischarge);
                //StrategyApi.SetManualCommand(manualCommand);
                PcsApi.SendPcsCommand(manualCommand);
            }
            if (SelectedManualStrategyMode == "恒功率充电")
            {
                var manualCommand = new BessCommand(this.StrategyManualValueSet, (BatteryStrategyEnum)SelectedManualMode.ConstantPowerCharge);
                //StrategyApi.SetManualCommand(manualCommand);
                PcsApi.SendPcsCommand(manualCommand);
            }
            if (SelectedManualStrategyMode == "恒功率放电")
            {
                var manualCommand = new BessCommand(this.StrategyManualValueSet, (BatteryStrategyEnum)SelectedManualMode.ConstantPowerDischarge);
                //StrategyApi.SetManualCommand(manualCommand);
                PcsApi.SendPcsCommand(manualCommand);
            }
        }
        /// <summary>
        /// 自动运行的重置应用
        /// </summary>
        private void AutoReset()
        {
            this.maxSOC = StrategyApi.GetMaxSoc();
            this.minSOC = StrategyApi.GetMinSoc();
            this.MaxAllowChargingPower = StrategyApi.GetMaxChargingPower();
            this.MaxAllowDishargingPower = StrategyApi.GetMaxDischargingPower();
            this.EnergyChargingEfficiency = StrategyApi.GetChargingEfficiency();
            this.EnergyDishargingEfficiency = StrategyApi.GetDischargingEfficiency();
            this.InitialEnergyStorage = StrategyApi.GetInitialEnergy();
            this.AvailableEnergyStorageCapacity = StrategyApi.GetEnergyCapacity();
            FetchAndGetMaxDemandThreshhold();
            FetchAndGetReversePowerThresholds();
        }
        private void ReversePowerSend()
        {
            StrategyApi.SetMaxSoc(this.maxSOC);
            StrategyApi.SetMinSoc(this.minSOC);
            StrategyApi.SetMaxChargingPower(this.MaxAllowChargingPower);
            StrategyApi.SetMaxDischargingPower(this.MaxAllowDishargingPower);
            StrategyApi.SetChargingEfficiency(this.EnergyChargingEfficiency);
            StrategyApi.SetDischargingEfficiency(this.EnergyDishargingEfficiency);
            StrategyApi.SetInitialEnergy(this.InitialEnergyStorage);
            StrategyApi.SetEnergyCapacity(this.AvailableEnergyStorageCapacity);
            StrategyApi.SetMaxDemandThreshold(this.demandControlCapacity, this.MaxDemandDescendRate);
            StrategyApi.SetReversePowerThreshold(this.ReversePowerThreshold, this.ReversePowerLowestThreshold, this.reversePowerDescendRate);
        }
        /// <summary>
        /// 需量控制参数解析
        /// </summary>
        private void FetchAndGetMaxDemandThreshhold()
        {
            double maxdemandpower, descendrate;

            StrategyApi.GetMaxDemandThreshhold(out maxdemandpower, out descendrate);
            this.DemandControlCapacity = maxdemandpower;
            this.MaxDemandDescendRate = descendrate;
        }

        /// <summary>
        /// 逆功率保护参数解析
        /// </summary>
        private void FetchAndGetReversePowerThresholds()
        {
            double threshold, lowestthreshhold, descendrate;

            StrategyApi.GetReversePowerThreshold(out threshold, out lowestthreshhold, out descendrate);
            this.ReversePowerThreshold = threshold;
            this.ReversePowerLowestThreshold = lowestthreshhold;
            this.reversePowerDescendRate = descendrate;
        }
        public enum SelectedManualMode
        {
            Standby = 0,
            ConstantCurrentCharge = 1,
            ConstantCurrentDischarge = 2,
            ConstantPowerCharge = 3,
            ConstantPowerDischarge = 4,
        }



    }
}
