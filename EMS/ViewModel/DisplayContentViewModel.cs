using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.View;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using EMS.Common;
using EMS.Api;
using EMS.Service;

namespace EMS.ViewModel
{
    public class DisplayContentViewModel : ViewModelBase
    {
        private ObservableCollection<BatteryTotalViewModel> _batteryTotalViewModelList;
        public ObservableCollection<BatteryTotalViewModel> BatteryTotalViewModelList
        {
            get => _batteryTotalViewModelList;
            set
            {
                if(SetProperty(ref _batteryTotalViewModelList, value))
                {
                    EnergyManagementSystem.GlobalInstance.BmsManager.SetBMSList(_batteryTotalViewModelList.ToList());
                }
            }
        }

        private BatteryTotalViewModel _selectedBatteryTotalViewModel;
        public BatteryTotalViewModel SelectedBatteryTotalViewModel
        {
            get => _selectedBatteryTotalViewModel;
            set
            {
                SetProperty(ref _selectedBatteryTotalViewModel, value);
            }
        }

        public RelayCommand AddDevCommand { get; set; }
        public RelayCommand DeleteDevCommand { get; set; }
        public RelayCommand DelAllDevCommand { get; set; }
        public RelayCommand ModifyPCSTCPCommand { get; set; }
        public RelayCommand ConnectDevCommand { get; set; }
        public RelayCommand DisconnectDevCommand { get; set; }

        public int DaqTimeSpan = 1;
        public bool IsStartSaveData = false;
        public ModbusClient PCSClient;
        public DisplayContentViewModel()
        {
            AddDevCommand = new RelayCommand(AddDev);
            DeleteDevCommand = new RelayCommand(DeleteDev);
            DelAllDevCommand = new RelayCommand(DelAllDev);
            ConnectDevCommand = new RelayCommand(ConnectDev);
            DisconnectDevCommand = new RelayCommand(DisconnectDev);

            // 初始化设备列表
            BatteryTotalViewModelList = new ObservableCollection<BatteryTotalViewModel>();
            DevConnectInfoManage manage = new DevConnectInfoManage();
            var entites = manage.Get();
            if (entites != null)
            {
                foreach (var entity in entites)
                {
                    BatteryTotalViewModelList.Add(new BatteryTotalViewModel(entity.IP, entity.Port) { TotalID = entity.BCMUID });
                }
            }
        }

        private void DisconnectDev()
        {
            SelectedBatteryTotalViewModel.DisconnectDev();
        }

        private void ConnectDev()
        {
            SelectedBatteryTotalViewModel.ConnectDev();
        }

        private void DeleteDev()
        {
            if (SelectedBatteryTotalViewModel != null)
            {
                if (!SelectedBatteryTotalViewModel.IsConnected)
                {
                    DevConnectInfoManage manage = new DevConnectInfoManage();
                    manage.Delete(new DevConnectInfoModel() { IP = SelectedBatteryTotalViewModel.IP, Port = SelectedBatteryTotalViewModel.Port, BCMUID = SelectedBatteryTotalViewModel.TotalID });
                    BatteryTotalViewModelList.Remove(SelectedBatteryTotalViewModel);
                }
                else
                {
                    MessageBox.Show("请先断开设备连接");
                }
            }
        }

        private void DelAllDev()
        {
            var items = BatteryTotalViewModelList.Where(p => p.IsConnected);
            if (items.Count() > 0)
            {
                MessageBox.Show("请先断开设备连接");
            }
            else
            {
                BatteryTotalViewModelList.Clear();
                DevConnectInfoManage manage = new DevConnectInfoManage();
                manage.DeleteAll();
            }
        }

        private void AddDev()
        {
            AddDevView view = new AddDevView();
            view.PCSRaB.IsEnabled = false;
            if (view.ShowDialog() == true)
            {
                //! 判断该IP是否存在
                var objs = BatteryTotalViewModelList.Where(dev => dev.IP == view.IPText.AddressText);
                var objs2 = BatteryTotalViewModelList.Where(dev => dev.TotalID.Contains(view.BCMUID.Text));
                if (objs.Count() == 0 && objs2.Count() == 0)
                {
                    // add Modbus TCP Dev
                    BatteryTotalViewModel vm = new BatteryTotalViewModel(view.IPText.AddressText, view.TCPPort.Text);
                    vm.TotalID = "BCMU(" + view.BCMUID.Text + ")";
                    BatteryTotalViewModelList.Add(vm);

                    //! 配置文件中新增IP
                    DevConnectInfoModel entity = new DevConnectInfoModel() { BCMUID = vm.TotalID, IP = vm.IP, Port = vm.Port };
                    DevConnectInfoManage manage = new DevConnectInfoManage();
                    manage.Insert(entity);
                }
                else
                {
                    MessageBox.Show("重复");
                    LogUtils.Warn("重复");
                }
            }
        }

        public bool IsStartDaqData { get;private set; }
        /// <summary>
        /// 展示实时数据
        /// </summary>
        public void DisplayRealTimeData()
        {
            IsStartDaqData = true;
            for (int i = 0; i < BatteryTotalViewModelList.Count; i++)
            {
                BatteryTotalViewModelList[i].StartDaqData();
            }
        }

        /// <summary>
        /// 停止展示实时数据
        /// </summary>
        public void StopDisplayRealTimeData()
        {
            IsStartDaqData = false;
            for (int i = 0; i < BatteryTotalViewModelList.Count; i++)
            {
                BatteryTotalViewModelList[i].StopDaqData();
            }
        }

        internal void StartSaveData()
        {
            IsStartSaveData = true;
            for (int i = 0; i < BatteryTotalViewModelList.Count; i++)
            {
                BatteryTotalViewModelList[i].StartSaveData();
            }
        }

        internal void StopSaveData()
        {
            IsStartSaveData = false;
            for (int i = 0; i < BatteryTotalViewModelList.Count; i++)
            {
                BatteryTotalViewModelList[i].StopSaveData();
            }
        }
    }
}
