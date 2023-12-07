using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace EMS.ViewModel
{
    public class PCSSettingViewModel:ObservableObject
    {
        /// <summary>
        /// 单条出力策略的model
        /// </summary>
        public class PCSSettingModel : ObservableObject
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

        
        /// <summary>
        /// 需要传给EMSController的策略list
        /// </summary>
        private List<BatteryStrategyModel> _batteryStrategyArray;
        public List<BatteryStrategyModel> BatteryStrategyArray
        {
            get => _batteryStrategyArray;
            set
            {
                SetProperty(ref _batteryStrategyArray, value);
            }
        }

        /// <summary>
        /// 手动自动切换开关
        /// </summary>
        private bool _isAutoStrategy;
        public bool IsAutoStrategy
        {
            get=> _isAutoStrategy;
            set
            {
                if(SetProperty(ref _isAutoStrategy, value))
                {
                    StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen,
                    IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);

                }
            }
        }

        private string _totalStrategyState;
        public string TotalStrategyState
        {
            get => _totalStrategyState;
            set
            {
                SetProperty(ref _totalStrategyState, value);
            }
        }

        private bool _isAutoStrategyBtnEnabled;
        public bool IsAutoStrategyBtnEnabled
        {
            get => _isAutoStrategyBtnEnabled;
            set
            {
                SetProperty(ref _isAutoStrategyBtnEnabled, value);
                
            }
        }


        private bool _isManualSendBtnEnabled;
        public bool IsManualSendBtnEnabled
        {
            get => _isManualSendBtnEnabled;
            set
            {
                SetProperty(ref _isManualSendBtnEnabled, value);
               
            }
        }
        /// <summary>
        /// Daillypattern open开关
        /// </summary>
        private bool _isDailyPatternBtnOpen;
        public bool IsDailyPatternBtnOpen
        {
            get => _isDailyPatternBtnOpen;
            set
            {
                if(SetProperty(ref _isDailyPatternBtnOpen, value))
                {
                    StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen, 
                        IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);
                }
            }
        }

        

        /// <summary>
        /// 需量控制开关
        /// </summary>
        private bool _isMaxDemandControlBtnOpen;
        public bool IsMaxDemandControlBtnOpen
        {
            get => _isMaxDemandControlBtnOpen;
            set
            {
                if(SetProperty(ref _isMaxDemandControlBtnOpen, value))
                {
                    StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen, 
                        IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);
                }
            }
        }

        private double _demandControlCapacity;
        public double DemandControlCapacity
        {
            get => _demandControlCapacity;
            set
            {
                SetProperty(ref _demandControlCapacity, value);
            }
        }

        private double _maxDemandDescendRate;
        public double MaxDemandDescendRate
        {
            get => _maxDemandDescendRate;
            set
            {
                SetProperty(ref _maxDemandDescendRate, value);
            }
        }

        /// <summary>
        /// 逆功率控制
        /// </summary>
        private bool _isReversePowerBtnOpen;
        public bool IsReversePowerBtnOpen
        {
            get => _isReversePowerBtnOpen;
            set
            {
               if( SetProperty(ref _isReversePowerBtnOpen, value))
                {
                    StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen,
                    IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);

                }
            }
        }


        private double _reversePowerThreshold;
        public double ReversePowerThreshold
        {
            get => _reversePowerThreshold;
            set
            {
                SetProperty(ref _reversePowerThreshold, value);
            }
        }

        private double _reversePowerLowestThreshold;
        public double ReversePowerLowestThreshold
        {
            get => _reversePowerLowestThreshold;
            set
            {
                SetProperty(ref _reversePowerLowestThreshold, value);
            }
        }

        private double _reversePowerDescendRate;
        public double ReversePowerDescendRate
        {
            get => _reversePowerDescendRate;
            set
            {
                SetProperty(ref _reversePowerDescendRate, value);
            }
        }

        /// <summary>
        /// 手动参数设置
        /// </summary>
        private string _selectedManualStrategyMode;
        public string SelectedManualStrategyMode
        {
            get => _selectedManualStrategyMode;
            set
            {
                SetProperty(ref _selectedManualStrategyMode, value);
            }
        }
        private double _strategyManualValueSet;
        public double StrategyManualValueSet
        {
            get => _strategyManualValueSet;
            set
            {
                SetProperty(ref _strategyManualValueSet, value);
            }
        }


        /// <summary>
        /// 界面显示的list
        /// </summary>
        public ObservableCollection<PCSSettingModel> _totalStrategies =new ObservableCollection<PCSSettingModel>();
        public ObservableCollection<PCSSettingModel> TotalStrategies
        {
            get=> _totalStrategies;
            set
            {
                SetProperty(ref _totalStrategies, value);
            }
        }
        /// <summary>
        /// 界面被选中的策略
        /// </summary>
        private BatteryStrategyModel _selectedBatteryStrategy;
        public BatteryStrategyModel SelectedBatteryStrategy
        {
            get => _selectedBatteryStrategy;
            set
            {
                SetProperty(ref _selectedBatteryStrategy, value);
            }
        }

        /// <summary>
        /// 策略设置
        /// </summary>
        private List<string> _strategyModeSet;
        public List<string> StrategyModeSet
        {
            get => _strategyModeSet;
            set
            {
                SetProperty(ref _strategyModeSet, value);
            }
        }

        private string _selectedStrategyMode;
        public string SelectedStrategyMode
        {
            get => _selectedStrategyMode;
            set
            {
                SetProperty(ref _selectedStrategyMode, value);
            }
        }


        private string _strategyStartTimeSet;
        public string StrategyStartTimeSet
        {
            get => _strategyStartTimeSet;
            set
            { 
                SetProperty(ref _strategyStartTimeSet, value); 
            }
        }
        private double _strategyValueSet;
        public double StrategyValueSet
        {
            get => _strategyValueSet;
            set
            {
                SetProperty(ref _strategyValueSet,value);
            }
        }
        private PCSSettingModel _selectedStrategy;
        public PCSSettingModel SelectedStrategy
        {
            get => _selectedStrategy;
            set
            {
                SetProperty(ref _selectedStrategy, value);
            }
        }

        public RelayCommand BatteryStrategyAddRowCommand { get; private set; }
        public RelayCommand BatteryStrategyRemoveRowCommand { get; private set; }
        public RelayCommand SwitchAutoManualCommand {  get; private set; }
        public RelayCommand MaxDemandSendCommand { get; private set; }
        public RelayCommand ReversePowerSendCommand { get; private set; }

        private int index = 1;
        //public PCSSettingModel NEWStrategy;
        public PCSSettingViewModel()
        {
           
            StrategyStartTimeSet = "00:00:00";
            IsDailyPatternBtnOpen = false;
            IsReversePowerBtnOpen = false;
            IsMaxDemandControlBtnOpen = false;
            IsAutoStrategy = false;
            IsAutoStrategyBtnEnabled = false;
            IsManualSendBtnEnabled = true;
            TotalStrategyState = "手动运行";
            BatteryStrategyAddRowCommand = new RelayCommand(BatteryStrategyAddRow);
            BatteryStrategyRemoveRowCommand = new RelayCommand(BatteryStrategyRemoveRow);
            SwitchAutoManualCommand = new RelayCommand(SwitchAutoManual);
            MaxDemandSendCommand = new RelayCommand(SendMaxDemandPara);
            ReversePowerSendCommand = new RelayCommand(SendReversePowerPara);
            StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen, IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);
        BatteryStrategyArray = new List<BatteryStrategyModel>();
            StrategyModeSet = new List<string>()
            {
                "待机",
                "恒电流充电",
                "恒电流放电",
                "恒功率充电",
                "恒功率放电"
            };
          
        }

        private void SendReversePowerPara()
        {
            StrategyApi.SetReversePowerThreshold(ReversePowerThreshold, 
                ReversePowerLowestThreshold, ReversePowerDescendRate);
        }

        private void SendMaxDemandPara()
        {
            StrategyApi.SetMaxDemandThreshold(DemandControlCapacity, 
                MaxDemandDescendRate);
        }

        private void SwitchAutoManual()
        {
            if(IsAutoStrategy)
            {
                IsAutoStrategy = false;
                TotalStrategyState = "手动运行";
                IsAutoStrategyBtnEnabled = false;
                IsManualSendBtnEnabled = true;
                IsDailyPatternBtnOpen = false;
                IsReversePowerBtnOpen = false;
                IsMaxDemandControlBtnOpen = false;

            }
            else
            {
                IsAutoStrategy = true;
                TotalStrategyState = "自动运行";
                IsAutoStrategyBtnEnabled = true;
                IsManualSendBtnEnabled = false;
               
            }
        }

        private void updateBatteryStrategyArray()
        {
            var total= TotalStrategies.OrderBy(TotalStrategies=>TotalStrategies.StrategyStartTime).ToList();
            for (int i = 0; i < total.Count; i++)
            {
                total[i].StrategyNumber = i+1;
            }
            TotalStrategies.Clear();
            BatteryStrategyArray.Clear();

            foreach (var strategy in total)
            {
                int mode = 0;
                TotalStrategies.Add(strategy);
               switch(strategy.StrategyMode)
                {
                    case "待机":
                    {
                            mode = 0;
                     }break;
                    case "恒电流充电":
                        {
                            mode = 1;
                        }
                        break;
                    case "恒电流放电":
                        {
                            mode = 2;
                        }
                        break;
                    case "恒功率充电":
                        {
                            mode = 3;
                        }
                        break;
                    case "恒功率放电":
                        {
                            mode = 4;
                        }
                        break;    
                }
                TimeSpan timeSpan = TimeSpan.Parse(strategy.StrategyStartTime);
                BatteryStrategyModel model = new BatteryStrategyModel()
                {
                    StartTime = timeSpan,
                    SetValue = strategy.StrategyValue,
                    BatteryStrategy = (BatteryStrategyEnum)mode
                };
                
                BatteryStrategyArray.Add(model);
            }
                BatteryStrategyArray = BatteryStrategyArray.OrderBy(batteryStrategy => batteryStrategy.StartTime).ToList();
            StrategyApi.SetDailyPattern(BatteryStrategyArray);
        }



        private void BatteryStrategyRemoveRow()
        {
            TotalStrategies.Remove(SelectedStrategy);
            updateBatteryStrategyArray();
        }

        private void BatteryStrategyAddRow()
        {
           if(SelectedStrategyMode!="待机"&&StrategyValueSet!=0) 
            {
                
                PCSSettingModel newStrategy = new PCSSettingModel
                {
                    StrategyStartTime = StrategyStartTimeSet,
                    StrategyMode = SelectedStrategyMode,
                    StrategyValue = StrategyValueSet
                };
                TotalStrategies.Add(newStrategy);
                updateBatteryStrategyArray();
            }
            else if(SelectedStrategyMode == "待机")
           {
                PCSSettingModel newStrategy = new PCSSettingModel
                {
                    StrategyStartTime = StrategyStartTimeSet,
                    StrategyMode = SelectedStrategyMode,
                    StrategyValue = 0
                };
                TotalStrategies.Add(newStrategy);
                updateBatteryStrategyArray();
            }
            else 
            {
                MessageBox.Show("请正确输入");
            }
            
        }

        
    }
}
