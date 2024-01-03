using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Strategy_SetterPageModel : ViewModelBase
    {
        /*当前执行模式*/
        private string totalStrategyState;
        public string TotalStrategyState
        {
            get { return totalStrategyState; }
            set
            {
                SetProperty(ref totalStrategyState, value);
            }
        }

        #region Command
        public RelayCommand Strategy_SetterPageCommand { get; set; }


        #endregion

        #region AutoRunMode
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
        private double lowerProportionDemand;
        public double LowerProportionDemand
        {
            get { return lowerProportionDemand; }
            set
            {
                SetProperty(ref lowerProportionDemand, value);
            }
        }

        /*逆功率保护限制*/
        private double protectionLimit;
        public double ProtectionLimit
        {
            get { return protectionLimit; }
            set
            {
                SetProperty(ref protectionLimit, value);
            }
        }

        /*逆功率保护动作限制*/
        private double protectionActionLimit;
        public double ProtectionActionLimit
        {
            get { return protectionActionLimit; }
            set
            {
                SetProperty(ref protectionActionLimit, value);
            }
        }

        /*下设比例*/
        private double lowerProportion_InversePower;
        public double LowerProportion_InversePower
        {
            get { return lowerProportion_InversePower; }
            set
            {
                SetProperty(ref lowerProportion_InversePower, value);
            }
        }
        #endregion

        public Strategy_SetterPageModel()
        {

        }

        
    }
}
