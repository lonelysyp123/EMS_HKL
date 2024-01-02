using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace EMS.ViewModel.NewEMSViewModel
{
    class DataModel
    {
        public int id { get; set; }
        public string controlType { get; set; }
        public double value { get; set; }
        public string startTime { get; set; }
    }
    public class Strategy_AnalysisPageModel: ObservableObject
    {        
        public RelayCommand BatteryStrategyAddRowCommand { get; set; }
        public RelayCommand BatteryStrategySendCommand { get; set; }

        public Strategy_AnalysisPageModel()
        {
            InitStrategy_AnalysisPageModel();
            BatteryStrategyAddRowCommand = new RelayCommand(BatteryStrategyAddRow);
            BatteryStrategySendCommand = new RelayCommand(BatteryStrategySend);
        }
        #region ObservableObject
        private string _curStrategyDemo;
        public string CurStrategyDemo
        {
            get => _curStrategyDemo;
            set
            {
                SetProperty(ref _curStrategyDemo, value);
            }
        }

        private List<string> _strategySelect;
        public List<string> StrategySelect
        {
            get => _strategySelect;
            set
            { 
                SetProperty(ref _strategySelect, value); 
            }
        }
        private string _strategySelectedItem;
        public string StrategySelectedItem
        {
            get => _strategySelectedItem;
            set
            {
                SetProperty(ref _strategySelectedItem, value);
            }
        }

        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                SetProperty(ref _startTime, value);
            }
        }

        private List<string> _controlType;
        public List<string> ControlType
        {
            get => _controlType;
            set 
            { 
                SetProperty(ref _controlType, value); 
            }
        }
        private string _controlTypeSelectedItem;
        public string ControlTypeSelectedItem
        {
            get => _controlTypeSelectedItem;
            set
            {
                SetProperty(ref _controlTypeSelectedItem, value);
            }
        }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
            }
        }

        private DateTime _planTime;
        public DateTime PlanTime
        {
            get => _planTime;
            set
            {
                SetProperty(ref _planTime, value);
            }
        }

        #endregion

        private void BatteryStrategyAddRow()
        {
            int i = 1;
            var newItem = new DataModel()
            {
                id = i++,
                controlType = ControlTypeSelectedItem,
                value = Value,
                startTime = StartTime
            };
        }
        private void BatteryStrategySend() 
        {
            
        }
        private void InitStrategy_AnalysisPageModel() 
        {
            StrategySelect = new List<string>
            {
                "逆功率保护",
                "需量控制"
            };
            ControlType = new List<string>
            {
                "待机",
                "恒电流充电",
                "恒电流放电",
                "恒功率充电",
                "恒功率放电"
            };
        }
    }
}
