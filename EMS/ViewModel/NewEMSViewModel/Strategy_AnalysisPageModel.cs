﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Model;
using EMS.Storage.DB.DBManage.EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;


namespace EMS.ViewModel.NewEMSViewModel
{
    public class Strategy_AnalysisPageModel: ObservableObject
    {
        #region Strategy_AnalysisPageModel Params
        private string _strategyStartTimeSet;
        public string StrategyStartTimeSet
        {
            get => _strategyStartTimeSet;
            set
            {
                SetProperty(ref _strategyStartTimeSet, value);
            }
        }
        private string _currentStragy;
        public string CurrentStragy
        {
            get => _currentStragy;
            set
            {
                SetProperty(ref _currentStragy, value);
            }
        }
        private double _strategyValueSet;
        public double StrategyValueSet
        {
            get => _strategyValueSet;
            set
            {
                SetProperty(ref _strategyValueSet, value);
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
        /// 界面显示的list
        /// </summary>
        private ObservableCollection<StrategyModel> _totalStrategies = new ObservableCollection<StrategyModel>();
        public ObservableCollection<StrategyModel> TotalStrategies
        {
            get => _totalStrategies;
            set
            {
                SetProperty(ref _totalStrategies, value);
            }
        }
        /// <summary>
        /// 需要传给EMSController的策略list
        /// </summary>
        /// <summary>
        /// 被选中的策略
        /// </summary>
        private StrategyModel _selectedStrategy;
        public StrategyModel SelectedStrategy
        {
            get => _selectedStrategy;
            set
            {
                SetProperty(ref _selectedStrategy, value);
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
        /// 被选中的设置的模式，5种模式之一
        /// </summary>
        private string _selectedStrategyMode;
        public string SelectedStrategyMode
        {
            get => _selectedStrategyMode;
            set
            {
                if (SetProperty(ref _selectedStrategyMode, value))
                {
                    ChangeUnit();
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
        /// 方案序号（共5套）
        /// </summary>
        private Dictionary<int, List<StrategyModel>> _strategySeries;
        public Dictionary<int, List<StrategyModel>> StrategySeries
        {
            get => _strategySeries;
            set
            {
                SetProperty(ref _strategySeries, value);
            }
        }
        #endregion

        public RelayCommand BatteryStrategyAddRowCommand { get; set; }
        public RelayCommand BatteryStrategySendCommand { get; set; }
        public RelayCommand BatteryStrategyRemoveRowCommand { get; set; }
        public Strategy_AnalysisPageModel()
        {                
            IsAutoStrategyBtnEnabled = true;
            StrategyStartTimeSet = "00:00:00";
            BatteryStrategyAddRowCommand = new RelayCommand(BatteryStrategyAddRow);//添加
            BatteryStrategySendCommand = new RelayCommand(SendDailyPatern);//应用
            BatteryStrategyRemoveRowCommand = new RelayCommand(BatteryStrategyRemoveRow);//删除
            BatteryStrategyArray = new List<BatteryStrategyModel>();
            StrategyModeSet = new List<string>()
            {
                "待机",
                "恒电流充电",
                "恒电流放电",
                "恒功率充电",
                "恒功率放电"
            };
            StrategySeriesNumber = new List<string> { "1", "2", "3", "4", "5" };
            StrategySeries = new Dictionary<int, List<StrategyModel>>();
            GetStrategySeries();
        }       
        /// <summary>
        /// 添加行
        /// </summary>
        private void BatteryStrategyAddRow()
        {
            StrategyModel newStrategy = new StrategyModel();
            
            newStrategy.StrategyStartTime = StrategyStartTimeSet;
            newStrategy.StrategyMode = SelectedStrategyMode;
            newStrategy.StrategyValue = StrategyValueSet;
            bool sameTimeCheck = TotalStrategies.ToList().Any(Item => Item.StrategyStartTime == StrategyStartTimeSet);
            bool stringValidityCheck = CheckTimeStringValidity(newStrategy.StrategyStartTime);
            switch (SelectedStrategyMode)
            {
                case "恒电流放电":
                    if (StrategyValueSet>1500)
                    {
                        MessageBox.Show("设置电流值不能超过1500");
                        return;
                    }
                    if (StrategyValueSet == 0 )
                    {
                        MessageBox.Show("设置电流值不能等于0");
                        return;
                    }
                    newStrategy.StrategyValue = -StrategyValueSet;
                    break;
                case "恒电流充电":
                    if (StrategyValueSet > 1500)
                    {
                        MessageBox.Show("设置电流值不能超过1500");
                        return;
                    }
                    if (StrategyValueSet == 0 )
                    {
                        MessageBox.Show("设置电流值不能等于0");
                        return;
                    }
                    break;
                case "恒功率放电":
                    if (StrategyValueSet > 1000)
                    {
                        MessageBox.Show("设置功率值不能超过1000");
                        return;
                    }
                    if (StrategyValueSet == 0 )
                    {
                        MessageBox.Show("设置功率值不能等于0");
                        return;
                    }
                    newStrategy.StrategyValue = -StrategyValueSet;
                    break;
                case "恒功率充电":
                    if (StrategyValueSet > 1000)
                    {
                        MessageBox.Show("设置功率值不能超过1000");
                        return;
                    }
                    if (StrategyValueSet == 0 )
                    {
                        MessageBox.Show("设置功率值不能等于0");
                        return;
                    }
                    break;
                case "待机":
                    newStrategy.StrategyValue = 0;
                    StrategyValueSet = 0;
                    break;
                default:
                    MessageBox.Show("请选择控制类型");
                    return;
            }
            if ((!sameTimeCheck) && stringValidityCheck )
            {                 
                TotalStrategies.Add(newStrategy);                
                SortStrategy();
            }
            else
            {
                MessageBox.Show("设置时间冲突");
                return;
            }
        }
        /// <summary>
        /// 下发DailyPattern
        /// </summary>
        private void SendDailyPatern()
        {
            int.TryParse(SelectedStrategySeries, out int number);            
            BatteryStrategyArray.Clear();
            PCSStrategyDailyPatternInfoManage manage = new PCSStrategyDailyPatternInfoManage();
            if (TotalStrategies.Count != 0)
            {                
                manage.Delete(number);
                foreach (var strategy in TotalStrategies)
                {
                    int strategymode = 0;

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
                CurrentStragy = SelectedStrategySeries; //当前执行策略  
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
        /// 删除dailypattern行
        /// </summary>
        private void BatteryStrategyRemoveRow()
        {
            TotalStrategies.Remove(SelectedStrategy);
            SortStrategy1();
        }
        private void SortStrategy1()
        {
            if (TotalStrategies.Count > 1)
            {
                TotalStrategies.RemoveAt(TotalStrategies.Count - 1);
            }
            //TotalStrategies.RemoveAt(TotalStrategies.Count-1);
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
            StrategyModel model = new StrategyModel();
            model.StrategyNumber = total.Count + 1;
            model.StrategyValue = total.ToArray()[0].StrategyValue;
            model.StrategyMode = total.ToArray()[0].StrategyMode;
            model.StrategyStartTime = total.ToArray()[0].StrategyStartTime;
            TotalStrategies.Add(model);
        }
        /// <summary>
        /// 排序
        /// </summary>
        private void SortStrategy()
        {
            if (TotalStrategies.Count > 1)
            {
                TotalStrategies.RemoveAt(TotalStrategies.Count - 2);
            }
            //TotalStrategies.RemoveAt(TotalStrategies.Count-1);
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
            StrategyModel model = new StrategyModel();
            model.StrategyNumber = total.Count+1;
            model.StrategyValue = total.ToArray()[0].StrategyValue;
            model.StrategyMode = total.ToArray()[0].StrategyMode;
            model.StrategyStartTime = total.ToArray()[0].StrategyStartTime;            
            TotalStrategies.Add(model);
        }
        private void ChangeUnit()
        {
            if (SelectedStrategyMode == "待机")
            {
                ParaUnits = "";
            }
            else if (SelectedStrategyMode == "恒电流充电" || SelectedStrategyMode == "恒电流放电")
            {
                ParaUnits = "A";
            }
            else if (SelectedStrategyMode == "恒功率充电" || SelectedStrategyMode == "恒功率放电")
            {
                ParaUnits = "kW";
            }
        }
        /// <summary>
        /// 从数据库获取所有方案
        /// </summary>
        private void GetStrategySeries()
        {

            TotalStrategies.Clear();
            PCSStrategyDailyPatternInfoManage manage = new PCSStrategyDailyPatternInfoManage();
            for (int i = 1; i <= 5; i++)
            {
                var entities = manage.Find(i);
                if (entities.Count != 0)
                {
                    foreach (var entity in entities)
                    {
                        StrategyModel strategy = new StrategyModel();
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
                    StrategySeries.Add(i, new List<StrategyModel>());
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
            if (timepart.Length == 3)
            {
                int.TryParse(timepart[0], out int hourstring);
                int.TryParse(timepart[1], out int minutestring);
                int.TryParse(timepart[2], out int secondstring);
                if (hourstring >= 0 && minutestring >= 0 && secondstring >= 0 && hourstring < 24 && minutestring < 60 && secondstring < 60)
                {
                    return true;
                }
                else
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
