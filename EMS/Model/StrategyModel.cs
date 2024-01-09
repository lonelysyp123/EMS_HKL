using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class StrategyModel : ObservableObject
    {
        private int _strategyNumber;
        public int StrategyNumber

        {
            get => _strategyNumber;
            set
            {
                SetProperty(ref _strategyNumber, value);
            }
        }
        private string _strategyMode;
        public string StrategyMode

        {
            get => _strategyMode;
            set
            {
                SetProperty(ref _strategyMode, value);
            }
        }

        private double _strategyValue;
        public double StrategyValue
        {
            get => _strategyValue;
            set
            {
                SetProperty(ref _strategyValue, value);
            }
        }

        private string _strategyStartTime;
        public string StrategyStartTime
        {
            get => _strategyStartTime;
            set
            {
                SetProperty(ref _strategyStartTime, value);
            }
        }

    }
}
