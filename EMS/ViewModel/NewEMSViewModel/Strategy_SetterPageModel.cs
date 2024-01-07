using CommunityToolkit.Mvvm.Input;
using EMS.Api;
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
        private List<string> strategyModeSet;
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
        public RelayCommand StrategyControlStartStopCommand { get; set; }
        public RelayCommand DemandControlStartStopCommand { get; set; }
        public RelayCommand InversePowerProtectionStartStopCommand { get; set; }
        /// <summary>
        /// 重置
        /// </summary>
        private string isEnabled_ManualReset;
        public string IsEnabled_ManualReset
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
        private string isEnabled_ManualApply;
        public string IsEnabled_ManualApply
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
        private string isEnabled_AutoReset;
        public string IsEnabled_AutoReset
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
        private string isAutoStrategyBtnEnabled;
        public string IsAutoStrategyBtnEnabled
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
            if (TotalStrategyState == "手动运行")
            {
                StrategyModeSet = new List<string> { "待机", "恒电流充电", "恒电流放电", "恒功率充电", "恒功率放电" };
                StrategyControlStartStopCommand = new RelayCommand(StrategyControlStartStop);
                DemandControlStartStopCommand = new RelayCommand(DemandControlStartStop);
                InversePowerProtectionStartStopCommand = new RelayCommand(InversePowerProtectionStartStop);
                CommandManualReset = new RelayCommand(ManualReset);
                CommandManualApply = new RelayCommand(ManualApply);
            }
            else if (TotalStrategyState == "自动运行")
            {
                CommandAutoReset = new RelayCommand(AutoReset);
                ReversePowerSendCommand = new RelayCommand(ReversePowerSend);
            }
            //else
            //{
            //    throw new NotImplementedException();
            //}
        }
        private void SwitchAutoManual()
        {
            //模式切换
            TotalStrategyState = TotalStrategyState == "手动运行" ? "自动运行" : "手动运行";
        }
        /// <summary>
        /// 充放电策略控制启停，需量控制启停，逆功率保护启停
        /// </summary>
        private void StrategyControlStartStop()
        {

        }
        private void DemandControlStartStop()
        {

        }
        private void InversePowerProtectionStartStop()
        {

        }
        /// <summary>
        /// 手动运行的重置应用
        /// </summary>
        private void ManualReset()
        {
            if (SelectedManualStrategyMode == "待机")
            {
                this.StrategyManualValueSet = 0;//
            }
            if (SelectedManualStrategyMode == "恒电流充电")
            {
                this.StrategyManualValueSet = 1;
            }
            if (SelectedManualStrategyMode == "恒电流放电")
            {
                this.StrategyManualValueSet = 2;
            }
            if (SelectedManualStrategyMode == "恒功率充电")
            {
                this.StrategyManualValueSet = 3;
            }
            if (SelectedManualStrategyMode == "恒功率放电")
            {
                this.StrategyManualValueSet = 4;
            }
        }
        private void ManualApply()
        {
            if (SelectedManualStrategyMode == "待机")
            {
                //= this.StrategyManualValueSet;
            }
            if (SelectedManualStrategyMode == "恒电流充电")
            {
                //= this.StrategyManualValueSet;
            }
            if (SelectedManualStrategyMode == "恒电流放电")
            {
                //= this.StrategyManualValueSet;
            }
            if (SelectedManualStrategyMode == "恒功率充电")
            {
                //= this.StrategyManualValueSet;
            }
            if (SelectedManualStrategyMode == "恒功率放电")
            {
                //= this.StrategyManualValueSet;
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
    }
}
