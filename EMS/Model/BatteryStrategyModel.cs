using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class BatteryStrategyModel : ObservableObject
    {
        private int _id;
        public int ID 
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private int _setValue;
        public int SetValue
        {
            get => _setValue;
            set
            {
                SetProperty(ref _setValue, value);
            }
        }

        private BatteryStrategyEnum _batteryStrategy;
        public BatteryStrategyEnum BatteryStrategy
        {
            get => _batteryStrategy;
            set
            {
                SetProperty(ref _batteryStrategy, value);
            }
        }

        private string _startTime = "00:00:00";
        public string StartTime 
        {
            get => _startTime;
            set
            {
                SetProperty(ref _startTime, value);
            }
        }

        private string _endTime = "00:00:00";
        public string EndTime 
        {
            get => _endTime; 
            set
            {
                SetProperty(ref _endTime, value);
            }
        }

        public BatteryStrategyModel()
        {

        }
    }

    public enum BatteryStrategyEnum
    {
        [Description("恒电流充电")]
        ConstantCurrentCharge = 0,
        [Description("恒电流放电")]
        ConstantCurrentDischarge = 1,
        [Description("恒功率充电")]
        ConstantPowerCharge = 2,
        [Description("恒功率放电")]
        ConstantPowerDischarge = 3,
    }
}
