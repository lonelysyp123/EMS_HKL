using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.Storage.DB.DBManage.EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
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
        /// 方案序号（共5套）
        /// </summary>
        private Dictionary<int, List<PCSSettingModel>> _strategySeries;
        public Dictionary<int, List<PCSSettingModel>> StrategySeries
        {
            get => _strategySeries;
            set
            {
                SetProperty(ref _strategySeries, value);
            }
        }
        /// <summary>
        /// 选择方案的Combobox
        /// </summary>
        private List<string> _strategySeriesNumber;
        public List<string> StrategySeriesNumber
        {
            get => _strategySeriesNumber;
            set
            {
                SetProperty(ref _strategySeriesNumber, value);
            }
        }
       /// <summary>
       /// 被选中的方案
       /// </summary>
        private string _selectedStrategySeries;
        public string SelectedStrategySeries
        {
            get => _selectedStrategySeries;
            set
            {
                //SetProperty(ref _selectedStrategySeries, value);

                if (SetProperty(ref _selectedStrategySeries, value))
                {
                    GetSeriesInfo();
                }
            } 
        }
        /// <summary>
        /// 发送策略键是否有效
        /// </summary>
        private bool _isStrateySendBtnEnabled;
        public bool IsStrateySendBtnEnabled
        {
            get => _isStrateySendBtnEnabled;
            set
            {
                SetProperty(ref _isStrateySendBtnEnabled, value);
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
        /// <summary>
        /// 手动自动运行字符串
        /// </summary>
        private string _totalStrategyState;
        public string TotalStrategyState
        {
            get => _totalStrategyState;
            set
            {
                SetProperty(ref _totalStrategyState, value);
            }
        }

        /// <summary>
        /// 是否启用自动策略标志位
        /// </summary>
        private bool _isAutoStrategyBtnEnabled;
        public bool IsAutoStrategyBtnEnabled
        {
            get => _isAutoStrategyBtnEnabled;
            set
            {
                SetProperty(ref _isAutoStrategyBtnEnabled, value);
                
            }
        }

        /// <summary>
        /// 手动发送按钮是否启用
        /// </summary>
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
        /// 策略模式Combobox，5种模式
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
        /// <summary>
        /// 被选中的设置的模式，5种模式之一
        /// </summary>
        private string _selectedStrategyMode;
        public string SelectedStrategyMode
        {
            get => _selectedStrategyMode;
            set
            {
                if(SetProperty(ref _selectedStrategyMode, value))
                {
                    ChangeUnit();
                }
            }
        }
        /// <summary>
        /// 参数单位
        /// </summary>
        private string _paraUnits;
        public string ParaUnits
        {
            get => _paraUnits;
            set
            {
                SetProperty(ref _paraUnits, value);
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
       /// <summary>
       /// 被选中的策略
       /// </summary>
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
        public RelayCommand BatteryStrategySendCommand { get; private set; }
        public RelayCommand SwitchAutoManualCommand {  get; private set; }
        public RelayCommand MaxDemandSendCommand { get; private set; }
        public RelayCommand ReversePowerSendCommand { get; private set; }
        
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
            IsStrateySendBtnEnabled = false;
            TotalStrategyState = "手动运行";
            BatteryStrategyAddRowCommand = new RelayCommand(BatteryStrategyAddRow);
            BatteryStrategyRemoveRowCommand = new RelayCommand(BatteryStrategyRemoveRow);
            SwitchAutoManualCommand = new RelayCommand(SwitchAutoManual);
            MaxDemandSendCommand = new RelayCommand(SendMaxDemandPara);
            ReversePowerSendCommand = new RelayCommand(SendReversePowerPara);
            BatteryStrategySendCommand = new RelayCommand(SendDailyPatern);
            StrategyApi.SetMode(IsAutoStrategy, IsDailyPatternBtnOpen, 
                IsMaxDemandControlBtnOpen, IsReversePowerBtnOpen);//获取现在的策略控制状态
            BatteryStrategyArray = new List<BatteryStrategyModel>();          
            StrategyModeSet = new List<string>()
            {
                "待机",
                "恒电流充电",
                "恒电流放电",
                "恒功率充电",
                "恒功率放电"
            };

            StrategySeriesNumber = new List<string> { "1", "2", "3", "4", "5"};
            
            StrategySeries = new Dictionary<int, List<PCSSettingModel>>();
            //SelectedStrategySeries = "1";
            GetStrategySeries();
           
        }

        /// <summary>
        /// 下发DailyPattern
        /// </summary>
        private void SendDailyPatern()
        {
            int.TryParse(SelectedStrategySeries, out int number);
            BatteryStrategyArray.Clear();
            PCSStrategyDailyPatternInfoManage manage = new PCSStrategyDailyPatternInfoManage();
            if (TotalStrategies.Count >= 2)
            {
                manage.Delete(number);
                foreach (var strategy in TotalStrategies)
                {
                    int strategymode = 0;

                    //List<PCSStrategyDailyPatternInfoModel> b = manage.Find(number);

                    switch (strategy.StrategyMode)
                    {
                        case "待机":
                            {
                                strategymode = 0;
                            }
                            break;

                        case "恒电流充电":
                            {
                                strategymode = 1;
                            }
                            break;

                        case "恒电流放电":
                            {
                                strategymode = 2;
                            }
                            break;

                        case "恒功率充电":
                            {
                                strategymode = 3;
                            }
                            break;

                        case "恒功率放电":
                            {
                                strategymode = 4;
                            }
                            break;
                    }
                    TimeSpan timeSpan = TimeSpan.Parse(strategy.StrategyStartTime);

                    PCSStrategyDailyPatternInfoModel storageModel = new PCSStrategyDailyPatternInfoModel()
                    {

                        Series = number,
                        Number = strategy.StrategyNumber,
                        Value = strategy.StrategyValue,
                        StrategyName = strategy.StrategyMode,
                        StartTime = strategy.StrategyStartTime
                    };

                    manage.Insert(storageModel);

                    BatteryStrategyModel strategymodel = new BatteryStrategyModel()
                    {
                        StartTime = timeSpan,
                        SetValue = strategy.StrategyValue,
                        BatteryStrategy = (BatteryStrategyEnum)strategymode
                    };
                    BatteryStrategyArray.Add(strategymodel);
                    int.TryParse(SelectedStrategySeries, out int a);
                    StrategySeries[a] = TotalStrategies.ToList();

                }
                // BatteryStrategyArray.Reverse();
                StrategyApi.SetDailyPattern(BatteryStrategyArray);
                EnergyManagementSystem.GlobalInstance.RestartOperationThread();

            }
            else
            {
                MessageBox.Show("请重新输入");
            }

        }
        /// <summary>
        /// 下发reversepower
        /// </summary>
        private void SendReversePowerPara()
        {
                StrategyApi.SetReversePowerThreshold(ReversePowerThreshold, 
                ReversePowerLowestThreshold, ReversePowerDescendRate);
        }
        /// <summary>
        /// 下发maxdemand
        /// </summary>
        private void SendMaxDemandPara()
        {
            StrategyApi.SetMaxDemandThreshold(DemandControlCapacity, 
            MaxDemandDescendRate);
        }
        /// <summary>
        /// 切换自动手动模式
        /// </summary>
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
                IsStrateySendBtnEnabled = false;
            }
            else
            {
                IsAutoStrategy = true;
                TotalStrategyState = "自动运行";
                IsAutoStrategyBtnEnabled = true;
                IsManualSendBtnEnabled = false;
                IsStrateySendBtnEnabled = true;
            }
        }

        private void ChangeUnit()
        {
            if (SelectedStrategyMode == "待机")
            {
                ParaUnits = "";
            }else if(SelectedStrategyMode == "恒电流充电" || SelectedStrategyMode == "恒电流放电")
            {
                ParaUnits = "A";
            }else if(SelectedStrategyMode == "恒功率充电"||SelectedStrategyMode== "恒功率放电")
            {
                ParaUnits = "kW";
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        private void SortStrategy()
        {
            var total = TotalStrategies.OrderBy(TotalStrategies => TotalStrategies.StrategyStartTime).ToList();
            TotalStrategies.Clear();
            for (int i = 0; i < total.Count; i++)
            {
                total[i].StrategyNumber = i + 1;
            }
            foreach (var strategy in total)
            {
                TotalStrategies.Add(strategy);
            }
        }

        /// <summary>
        /// 删除dailypattern行
        /// </summary>
        private void BatteryStrategyRemoveRow()
        {
            TotalStrategies.Remove(SelectedStrategy);
            SortStrategy();
            //updateBatteryStrategyArray();
        }
        /// <summary>
        /// 添加行
        /// </summary>
        private void BatteryStrategyAddRow()
        { 
            PCSSettingModel newStrategy = new PCSSettingModel();
            newStrategy.StrategyStartTime = StrategyStartTimeSet;
            newStrategy.StrategyMode = SelectedStrategyMode;
            newStrategy.StrategyValue = StrategyValueSet;
           bool sameTimeCheck= TotalStrategies.ToList().Any(Item => Item.StrategyStartTime == StrategyStartTimeSet);
            bool stringValidityCheck = CheckTimeStringValidity(newStrategy.StrategyStartTime);
            bool otherCheck = true;
            
            if (SelectedStrategyMode == "待机")
            {
                newStrategy.StrategyValue = 0;
                StrategyValueSet = 0;
            }else if (SelectedStrategyMode !="待机"&& StrategyValueSet == 0)
            {
                otherCheck = false;
            }
            if((!sameTimeCheck)&& stringValidityCheck&otherCheck) 
            {
                TotalStrategies.Add(newStrategy);
                SortStrategy();
            }
            else
            {
                MessageBox.Show("请重新输入");
            }
        }
        /// <summary>
        /// 获取当前选中的方案
        /// </summary>
        private void GetSeriesInfo()
        {
            TotalStrategies.Clear();
            int.TryParse(SelectedStrategySeries, out int number);
                foreach (var strategy in StrategySeries[number])
                {
                    TotalStrategies.Add(strategy);
                }
                  
        }
        /// <summary>
        /// 从数据库获取所有方案
        /// </summary>
        private void GetStrategySeries()
        {
            
            TotalStrategies.Clear();          
            PCSStrategyDailyPatternInfoManage manage = new PCSStrategyDailyPatternInfoManage();
            for(int i = 1;i <= 5; i++)
            {
                var entities = manage.Find(i);
                if (entities.Count!=0)
                {
                    foreach (var entity in entities)
                    {
                        PCSSettingModel strategy = new PCSSettingModel();
                        strategy.StrategyNumber = entity.Number;
                        strategy.StrategyValue = entity.Value;
                        strategy.StrategyStartTime = entity.StartTime;
                        strategy.StrategyMode = entity.StrategyName;
                        TotalStrategies.Add(strategy);
                    }
                    StrategySeries.Add(i, TotalStrategies.ToList());
                    TotalStrategies.Clear();
                }
                else
                {
                    StrategySeries.Add(i, new List<PCSSettingModel>());
                }

            }
 
        }
        /// <summary>
        /// 检查方案中时间是否重复
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private bool CheckTimeStringValidity(string timeString)
        {
            string timestring = timeString;
            string[] timepart = timestring.Split(':');
            if(timepart.Length ==3) 
            {
                int.TryParse(timepart[0], out int hourstring);
                int.TryParse(timepart[1],out int minutestring);
                int.TryParse(timepart[2], out int secondstring);
                if(hourstring >=0 && minutestring >= 0 && secondstring >= 0 && hourstring < 24 && minutestring < 60 && secondstring < 60)
                {
                    return true;
                }else 
                { 
                    return false; 
                }
            }
            else
            {
                return false;
            }
        }
      
    }
}
