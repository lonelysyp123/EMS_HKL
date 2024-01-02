using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_PCSPageViewModel:ViewModelBase
    {
        #region ObservableObject
        /// <summary>
        /// DC侧支路1状态
        /// </summary>
        private string _dc_RunningState;

        public string DC_RunningState
        {
            get => _dc_RunningState;
            set
            {
                SetProperty(ref _dc_RunningState, value);
            }
        }

        /// <summary>
        /// DC侧支路1启停状态
        /// </summary>
        private SolidColorBrush _dc_StartOrStopState;

        public SolidColorBrush DC_StartOrStopState
        {
            get => _dc_StartOrStopState;
            set
            {
                SetProperty(ref _dc_StartOrStopState, value);
            }
        }

        /// <summary>
        /// DC侧支路1直流功率
        /// </summary>
        private double _dc_power;

        public double DC_power
        {
            get => _dc_power;
            set
            {
                SetProperty(ref _dc_power, value);
            }
        }

        /// <summary>
        /// DC侧支路1直流电压
        /// </summary>
        private double _dc_Voltage;

        public double DC_Voltage
        {
            get => _dc_Voltage;
            set
            {
                SetProperty(ref _dc_Voltage, value);
            }
        }

        /// <summary>
        /// DC侧支路1直流电流
        /// </summary>
        private double _dc_Current;

        public double DC_Current
        {
            get => _dc_Current;
            set
            {
                SetProperty(ref _dc_Current, value);
            }
        }

        /// <summary>
        /// DC侧支路1累计充电电量
        /// </summary>
        private uint _dc_CumulativeChargePower;

        public uint DC_CumulativeChargePower
        {
            get => _dc_CumulativeChargePower;
            set
            {
                SetProperty(ref _dc_CumulativeChargePower, value);
            }
        }

        /// <summary>
        /// DC侧支路1累计放电电量
        /// </summary>
        private uint _dc_CumulativeDischargePower;

        public uint DC_CumulativeDischargePower
        {
            get => _dc_CumulativeDischargePower;
            set
            {
                SetProperty(ref _dc_CumulativeDischargePower, value);
            }
        }

        /// <summary>
        /// DC侧支路1BUS侧电压
        /// </summary>
        private double _dc_BUSVoltage;

        public double DC_BUSVoltage
        {
            get => _dc_BUSVoltage;
            set
            {
                SetProperty(ref _dc_BUSVoltage, value);
            }
        }

        /// <summary>
        /// 模块温度
        /// </summary>
        private double _moduleTemp;

        public double ModuleTemp
        {
            get => _moduleTemp;
            set
            {
                SetProperty(ref _moduleTemp, value);
            }
        }

        /// <summary>
        /// 环境温度
        /// </summary>
        private double _environmentTemp;

        public double EnvironmentTemp
        {
            get => _environmentTemp;
            set
            {
                SetProperty(ref _environmentTemp, value);
            }
        }

        /// <summary>
        /// PCS控制状态-本地手动控制状态
        /// </summary>
        private SolidColorBrush _isManualControl;

        public SolidColorBrush IsManualControl
        {
            get => _isManualControl;
            set
            {
                SetProperty(ref _isManualControl, value);
            }
        }

        /// <summary>
        /// PCS控制状态-本地自动控制状态
        /// </summary>
        private SolidColorBrush _isAutomation;

        public SolidColorBrush IsAutomation
        {
            get => _isAutomation;
            set
            {
                SetProperty(ref _isAutomation, value);
            }
        }

        /// <summary>
        /// PCS控制状态-远程控制状态
        /// </summary>
        private SolidColorBrush _isRemoteControl;

        public SolidColorBrush IsRemoteControl
        {
            get => _isRemoteControl;
            set
            {
                SetProperty(ref _isRemoteControl, value);
            }
        }

        /// <summary>
        /// PCS状态-告警状态
        /// </summary>
        private SolidColorBrush _isAlarmStatus;

        public SolidColorBrush IsAlarmStatus
        {
            get => _isAlarmStatus;
            set
            {
                SetProperty(ref _isAlarmStatus, value);
            }
        }

        /// <summary>
        /// PCS状态-故障状态
        /// </summary>
        private SolidColorBrush _isFaultStatus;

        public SolidColorBrush IsFaultStatus
        {
            get => _isFaultStatus;
            set
            {
                SetProperty(ref _isFaultStatus, value);
            }
        }

        /// <summary>
        /// PCS状态-上电初始化状态
        /// </summary>
        private SolidColorBrush _isInitStatus;

        public SolidColorBrush IsInitStatus
        {
            get => _isInitStatus;
            set
            {
                SetProperty(ref _isInitStatus, value);
            }
        }












        #endregion

        public ObservableCollection<Item> Items { get; set; }

        private PCSDataService pcsservice;
        public Monitor_PCSPageViewModel()
        {
            Items = new ObservableCollection<Item> { };
            pcsservice = new PCSDataService();
        }

        //    private void RefreshDataTh()
        //    {
        //        while (IsDaqData)
        //        {
        //            if (pcsservice.pcsModels.TryTake(out PCSModel CurrentPCSModel))
        //            {
        //                var model = (PCSModel)CurrentPCSModel.Clone();
        //                SmartMeterModelList.Add(model);
        //                // 把数据分发给需要显示的内容
        //                App.Current.Dispatcher.Invoke(() =>
        //                {
        //                    RefreshData(CurrentSmartMeterModel);
        //                });

        //                if (IsRecordData)
        //                {
        //                    SaveData(CurrentSmartMeterModel);
        //                }
        //            }
        //            else
        //            {
        //                Thread.Sleep(500);
        //            }
        //        }
        //    }
        //}
    }
    public class Item
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
