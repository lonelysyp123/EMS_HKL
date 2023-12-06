using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class BatteryStrategyModel : ObservableObject, IComparer, IEquatable<BatteryStrategyModel>
    {
        public BatteryStrategyModel()
        {
            _id = 0;
            _setValue = 0;
            _batteryStrategy = BatteryStrategyEnum.Standby;
            _startTime = TimeSpan.Zero;
        }

        public BatteryStrategyModel(BatteryStrategyEnum batteryStrategy, int setValue)
        {
            _id = 0;
            SetValue = setValue;
            BatteryStrategy = batteryStrategy;
            StartTime = TimeSpan.Zero;
        }

        int IComparer.Compare(object left, object right)
        {
            BatteryStrategyModel leftModel = (BatteryStrategyModel)left;
            BatteryStrategyModel rightModel = (BatteryStrategyModel)right;
            if (leftModel.StartTime > rightModel.StartTime)
                return 1;
            if (leftModel.StartTime < rightModel.StartTime)
                return -1;
            else
                return 0;
        }

        private double _setValue;
        public double SetValue
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
        public bool Equals(BatteryStrategyModel other)
        {
            if (this.SetValue == other.SetValue && this._startTime == other._startTime && this.BatteryStrategy == other.BatteryStrategy) return true;
            return false;
        }

        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public BessCommand Command { get => new BessCommand(this.SetValue, this.BatteryStrategy); }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                SetProperty(ref _startTime, value);
            }
        }
    }

    public class BessCommand
    {
        public BessCommand()
        {
            _value = 0;
            _batteryStrategy = BatteryStrategyEnum.Standby;
        }

        public BessCommand(double value, BatteryStrategyEnum batteryStrategy)
        {
            _value = value;
            _batteryStrategy = batteryStrategy;
        }

        private double _value;

        public double Value
        {
            get => _value;
        }

        private BatteryStrategyEnum _batteryStrategy;
        public BatteryStrategyEnum BatteryStrategy
        {
            get => _batteryStrategy;
        }
    }

    public enum BatteryStrategyEnum
    {
        [Description("待机")]
        Standby = 0,
        [Description("恒电流充电")]
        ConstantCurrentCharge = 1,
        [Description("恒电流放电")]
        ConstantCurrentDischarge = 2,
        [Description("恒功率充电")]
        ConstantPowerCharge = 3,
        [Description("恒功率放电")]
        ConstantPowerDischarge = 4,
       
    }
}
