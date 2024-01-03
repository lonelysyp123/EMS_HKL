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
        public RelayCommand Strategy_SetterPageCommand { get; private set; }
        #endregion

        #region AutoRunMode
        /*最大SOC*/
        private string maxSOC;
        public string MaxSOC
        {
            get { return maxSOC; }
            set
            {
                SetProperty(ref maxSOC, value);
            }
        }

        /*最小SOC*/
        private string minSOC;
        public string MinSOC
        {
            get { return minSOC; }
            set
            {
                SetProperty(ref minSOC, value);
            }
        }

        /*最大允许充电功率*/
        private string maxAllowChargingPower;
        public string MaxAllowChargingPower
        {
            get { return maxAllowChargingPower; }
            set
            {
                SetProperty(ref maxAllowChargingPower, value);
            }
        }

        /*最大允许放电功率*/
        private string maxAllowDishargingPower;
        public string MaxAllowDishargingPower
        {
            get { return maxAllowDishargingPower; }
            set
            {
                SetProperty(ref maxAllowDishargingPower, value);
            }
        }

        /*储能充电效率*/
        private string energyChargingEfficiency;
        public string EnergyChargingEfficiency
        {
            get { return energyChargingEfficiency; }
            set
            {
                SetProperty(ref energyChargingEfficiency, value);
            }
        }

        /*储能放电效率*/
        private string energyDishargingEfficiency;
        public string EnergyDishargingEfficiency
        {
            get { return energyDishargingEfficiency; }
            set
            {
                SetProperty(ref energyDishargingEfficiency, value);
            }
        }

        /*储能初始能量*/
        private string initialEnergyStorage;
        public string InitialEnergyStorage
        {
            get { return initialEnergyStorage; }
            set
            {
                SetProperty(ref initialEnergyStorage, value);
            }
        }

        /*储能可用容量*/
        private string availableEnergyStorageCapacity;
        public string AvailableEnergyStorageCapacity
        {
            get { return availableEnergyStorageCapacity; }
            set
            {
                SetProperty(ref availableEnergyStorageCapacity, value);
            }
        }

        /*需量控制容量*/
        private string demandControlCapacity;
        public string DemandControlCapacity
        {
            get { return demandControlCapacity; }
            set
            {
                SetProperty(ref demandControlCapacity, value);
            }
        }

        /*下设比例*/
        private string lowerProportionDemand;
        public string LowerProportionDemand
        {
            get { return lowerProportionDemand; }
            set
            {
                SetProperty(ref lowerProportionDemand, value);
            }
        }

        /*逆功率保护限制*/
        private string protectionLimit;
        public string ProtectionLimit
        {
            get { return protectionLimit; }
            set
            {
                SetProperty(ref protectionLimit, value);
            }
        }

        /*逆功率保护动作限制*/
        private string protectionActionLimit;
        public string ProtectionActionLimit
        {
            get { return protectionActionLimit; }
            set
            {
                SetProperty(ref protectionActionLimit, value);
            }
        }

        /*下设比例*/
        private string lowerProportion_InversePower;
        public string LowerProportion_InversePower
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

        public void DataDistribution()
        {

        }
    }
}
