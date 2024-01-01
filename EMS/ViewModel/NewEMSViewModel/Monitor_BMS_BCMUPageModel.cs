using CommunityToolkit.Mvvm.Input;
using EMS.Common;
using EMS.Model;
using EMS.View.NewEMSView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_BMS_BCMUPageModel : ViewModelBase
    {
        #region ObservableObject
        private SolidColorBrush isConnect;
        public SolidColorBrush IsConnect
        {
            get => isConnect;
            set
            {
                SetProperty(ref isConnect, value);
            }
        }

        private Visibility isVisible_SwitchOn;
        public Visibility IsVisible_SwitchOn
        {
            get => isVisible_SwitchOn;
            set
            {
                SetProperty(ref isVisible_SwitchOn, value);
            }
        }

        private Visibility isVisible_SwitchOff;
        public Visibility IsVisible_SwitchOff
        {
            get => isVisible_SwitchOff;
            set
            {
                SetProperty(ref isVisible_SwitchOff, value);
            }
        }

        private Visibility isVisible_UpArrow;
        public Visibility IsVisible_UpArrow
        {
            get => isVisible_UpArrow;
            set
            {
                SetProperty(ref isVisible_UpArrow, value);
            }
        }

        private Visibility isVisible_DownArrow;
        public Visibility IsVisible_DownArrow
        {
            get => isVisible_DownArrow;
            set
            {
                SetProperty(ref isVisible_DownArrow, value);
            }
        }

        private SolidColorBrush alarmcolor;
        public SolidColorBrush Alarmcolor
        {
            get => alarmcolor;
            set
            {
                SetProperty(ref alarmcolor, value);
            }
        }

        private string soc_BCMU;
        public string SOC_BCMU
        {
            get { return soc_BCMU; }
            set
            {
                SetProperty(ref soc_BCMU, value);
            }
        }

        private string clusterVoltage_BCMU;
        public string ClusterVoltage_BCMU
        {
            get { return clusterVoltage_BCMU; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU, value);
            }
        }

        private string presentCurrent_BCMU;
        public string PresentCurrent_BCMU
        {
            get { return presentCurrent_BCMU; }
            set
            {
                SetProperty(ref presentCurrent_BCMU, value);
            }
        }

        private string maxCellVoltage_BCMU;
        public string MaxCellVoltage_BCMU
        {
            get { return maxCellVoltage_BCMU; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU, value);
            }
        }

        private string minCellVoltage_BCMU;
        public string MinCellVoltage_BCMU
        {
            get { return minCellVoltage_BCMU; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU, value);
            }
        }

        private string maxTemperature_BCMU;
        public string MaxTemperature_BCMU
        {
            get { return maxTemperature_BCMU; }
            set
            {
                SetProperty(ref maxTemperature_BCMU, value);
            }
        }


        #endregion

        #region Command

        public RelayCommand ToMonitor_BMS_BCMUPageCommand { get;private set;}

        #endregion

        public Monitor_BMS_BCMUPageModel()
        {
            ToMonitor_BMS_BCMUPageCommand = new RelayCommand(ToMonitor_BMS_BCMUPage);
        }

        public void DataDistribution(BatteryTotalModel model, bool isconnected)
        {
            SOC_BCMU = model.TotalSOC.ToString();
            ClusterVoltage_BCMU = model.TotalVoltage.ToString();
            PresentCurrent_BCMU = model.TotalCurrent.ToString();
            MaxCellVoltage_BCMU = model.MaxVoltage.ToString();
            MinCellVoltage_BCMU = model.MinVoltage.ToString();
            MaxTemperature_BCMU = model.MaxTemperature.ToString();

            if (model.AlarmStateBCMU.Count > 0)
            {
                Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_T);
            }
            else
            {
                Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_F);
            }

            if (isconnected)
            {
                IsConnect = new SolidColorBrush(BMUColors.IsConnect_T);
            }
            else
            {
                IsConnect = new SolidColorBrush(BMUColors.IsConnect_F);
            }

            // 判断是否并离网
            //IsVisible_SwitchOff;
            //IsVisible_SwitchOn;

            // 判断充放电
            //IsVisible_DownArrow;
            //IsVisible_UpArrow;
        }

        private void ToMonitor_BMS_BCMUPage()
        {
            throw new NotImplementedException();
        }
    }
}
