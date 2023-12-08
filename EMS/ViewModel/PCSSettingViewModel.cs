using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        private ObservableCollection<StateGridModel> _stateGridArray;
        public ObservableCollection<StateGridModel> StateGridArray
        {
            get => _stateGridArray;
            set
            {
                SetProperty(ref _stateGridArray, value);
            }
        }

        private StateGridModel _selectedStateGrid;
        public StateGridModel SelectedStateGrid
        {
            get => _selectedStateGrid;
            set
            {
                SetProperty(ref _selectedStateGrid, value);
            }
        }

        private ObservableCollection<BatteryStrategyModel> _batteryStrategyArray;
        public ObservableCollection<BatteryStrategyModel> BatteryStrategyArray
        {
            get => _batteryStrategyArray;
            set
            {
                SetProperty(ref _batteryStrategyArray, value);
            }
        }

        private BatteryStrategyModel _selectedBatteryStrategy;
        public BatteryStrategyModel SelectedBatteryStrategy
        {
            get => _selectedBatteryStrategy;
            set
            {
                SetProperty(ref _selectedBatteryStrategy, value);
            }
        }

        public RelayCommand StateGridAddRowCommand { get; private set; }
        public RelayCommand StateGridRemoveRowCommand { get; private set; }
        public RelayCommand StateGridExecuteStrategyCommand { get; private set; }
        public RelayCommand BatteryStrategyAddRowCommand { get; private set; }
        public RelayCommand BatteryStrategyRemoveRowCommand { get; private set; }
        public RelayCommand BatteryStrategyExecuteStrategyCommand { get; private set; }
        private int index = 1;
        //public PCSSettingModel NEWStrategy;
        public PCSSettingViewModel()
        {
            StateGridAddRowCommand = new RelayCommand(StateGridAddRow);
            StateGridRemoveRowCommand = new RelayCommand(StateGridRemoveRow);
            StateGridExecuteStrategyCommand = new RelayCommand(StateGridExecuteStrategy);
            BatteryStrategyAddRowCommand = new RelayCommand(BatteryStrategyAddRow);
            BatteryStrategyRemoveRowCommand = new RelayCommand(BatteryStrategyRemoveRow);
            BatteryStrategyExecuteStrategyCommand = new RelayCommand(BatteryStrategyExecuteStrategy);

            BatteryStrategyArray = new ObservableCollection<BatteryStrategyModel>();
            StateGridArray = new ObservableCollection<StateGridModel>();
        }

        private void BatteryStrategyExecuteStrategy()
        {
            // 执行策略 1. 存到数据库，2. 通知采集线程按照逻辑执行
        }

        private void BatteryStrategyRemoveRow()
        {
            BatteryStrategyArray.Remove(SelectedBatteryStrategy);
        }

        private void BatteryStrategyAddRow()
        {
            BatteryStrategyArray.Add(new BatteryStrategyModel() { ID = index });
            index++;
        }

        private void StateGridExecuteStrategy()
        {
            // 执行策略 1. 存到数据库，2. 通知采集线程按照逻辑执行
        }

        private void StateGridRemoveRow()
        {
            StateGridArray.Remove(SelectedStateGrid);
        }

        private void StateGridAddRow()
        {
            StateGridArray.Add(new StateGridModel() { ID = index });
            index++;
        }
    }
}
