using EMS.Common;
using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_BMSPageModel : ViewModelBase
    {
        #region ObservableObject
        private string soc_BCMU1;
        public string SOC_BCMU1
        {
            get { return soc_BCMU1; }
            set
            {
                SetProperty(ref soc_BCMU1, value);
            }
        }

        private string clusterVoltage_BCMU1;
        public string ClusterVoltage_BCMU1
        {
            get { return clusterVoltage_BCMU1; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU1, value);
            }
        }

        private string presentCurrent_BCMU1;
        public string PresentCurrent_BCMU1
        {
            get { return presentCurrent_BCMU1; }
            set
            {
                SetProperty(ref presentCurrent_BCMU1, value);
            }
        }

        private string maxCellVoltage_BCMU1;
        public string MaxCellVoltage_BCMU1
        {
            get { return maxCellVoltage_BCMU1; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU1, value);
            }
        }

        private string minCellVoltage_BCMU1;
        public string MinCellVoltage_BCMU1
        {
            get { return minCellVoltage_BCMU1; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU1, value);
            }
        }

        private string maxTemperature_BCMU1;
        public string MaxTemperature_BCMU1
        {
            get { return maxTemperature_BCMU1; }
            set
            {
                SetProperty(ref maxTemperature_BCMU1, value);
            }
        }

        private string soc_BCMU2;
        public string SOC_BCMU2
        {
            get { return soc_BCMU2; }
            set
            {
                SetProperty(ref soc_BCMU2, value);
            }
        }

        private string clusterVoltage_BCMU2;
        public string ClusterVoltage_BCMU2
        {
            get { return clusterVoltage_BCMU2; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU2, value);
            }
        }

        private string presentCurrent_BCMU2;
        public string PresentCurrent_BCMU2
        {
            get { return presentCurrent_BCMU2; }
            set
            {
                SetProperty(ref presentCurrent_BCMU2, value);
            }
        }

        private string maxCellVoltage_BCMU2;
        public string MaxCellVoltage_BCMU2
        {
            get { return maxCellVoltage_BCMU2; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU2, value);
            }
        }

        private string minCellVoltage_BCMU2;
        public string MinCellVoltage_BCMU2
        {
            get { return minCellVoltage_BCMU2; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU2, value);
            }
        }

        private string maxTemperature_BCMU2;
        public string MaxTemperature_BCMU2
        {
            get { return maxTemperature_BCMU2; }
            set
            {
                SetProperty(ref maxTemperature_BCMU2, value);
            }
        }

        private string soc_BCMU3;
        public string SOC_BCMU3
        {
            get { return soc_BCMU3; }
            set
            {
                SetProperty(ref soc_BCMU3, value);
            }
        }

        private string clusterVoltage_BCMU3;
        public string ClusterVoltage_BCMU3
        {
            get { return clusterVoltage_BCMU3; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU3, value);
            }
        }

        private string presentCurrent_BCMU3;
        public string PresentCurrent_BCMU3
        {
            get { return presentCurrent_BCMU3; }
            set
            {
                SetProperty(ref presentCurrent_BCMU3, value);
            }
        }

        private string maxCellVoltage_BCMU3;
        public string MaxCellVoltage_BCMU3
        {
            get { return maxCellVoltage_BCMU3; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU3, value);
            }
        }

        private string minCellVoltage_BCMU3;
        public string MinCellVoltage_BCMU3
        {
            get { return minCellVoltage_BCMU3; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU3, value);
            }
        }

        private string maxTemperature_BCMU3;
        public string MaxTemperature_BCMU3
        {
            get { return maxTemperature_BCMU3; }
            set
            {
                SetProperty(ref maxTemperature_BCMU3, value);
            }
        }

        private string soc_BCMU4;
        public string SOC_BCMU4
        {
            get { return soc_BCMU4; }
            set
            {
                SetProperty(ref soc_BCMU4, value);
            }
        }

        private string clusterVoltage_BCMU4;
        public string ClusterVoltage_BCMU4
        {
            get { return clusterVoltage_BCMU4; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU4, value);
            }
        }

        private string presentCurrent_BCMU4;
        public string PresentCurrent_BCMU4
        {
            get { return presentCurrent_BCMU4; }
            set
            {
                SetProperty(ref presentCurrent_BCMU4, value);
            }
        }

        private string maxCellVoltage_BCMU4;
        public string MaxCellVoltage_BCMU4
        {
            get { return maxCellVoltage_BCMU4; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU4, value);
            }
        }

        private string minCellVoltage_BCMU4;
        public string MinCellVoltage_BCMU4
        {
            get { return minCellVoltage_BCMU4; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU4, value);
            }
        }

        private string maxTemperature_BCMU4;
        public string MaxTemperature_BCMU4
        {
            get { return maxTemperature_BCMU4; }
            set
            {
                SetProperty(ref maxTemperature_BCMU4, value);
            }
        }

        private string soc_BCMU5;
        public string SOC_BCMU5
        {
            get { return soc_BCMU5; }
            set
            {
                SetProperty(ref soc_BCMU5, value);
            }
        }

        private string clusterVoltage_BCMU5;
        public string ClusterVoltage_BCMU5
        {
            get { return clusterVoltage_BCMU5; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU5, value);
            }
        }

        private string presentCurrent_BCMU5;
        public string PresentCurrent_BCMU5
        {
            get { return presentCurrent_BCMU5; }
            set
            {
                SetProperty(ref presentCurrent_BCMU5, value);
            }
        }

        private string maxCellVoltage_BCMU5;
        public string MaxCellVoltage_BCMU5
        {
            get { return maxCellVoltage_BCMU5; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU5, value);
            }
        }

        private string minCellVoltage_BCMU5;
        public string MinCellVoltage_BCMU5
        {
            get { return minCellVoltage_BCMU5; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU5, value);
            }
        }

        private string maxTemperature_BCMU5;
        public string MaxTemperature_BCMU5
        {
            get { return maxTemperature_BCMU5; }
            set
            {
                SetProperty(ref maxTemperature_BCMU5, value);
            }
        }

        private string soc_BCMU6;
        public string SOC_BCMU6
        {
            get { return soc_BCMU6; }
            set
            {
                SetProperty(ref soc_BCMU6, value);
            }
        }

        private string clusterVoltage_BCMU6;
        public string ClusterVoltage_BCMU6
        {
            get { return clusterVoltage_BCMU6; }
            set
            {
                SetProperty(ref clusterVoltage_BCMU6, value);
            }
        }

        private string presentCurrent_BCMU6;
        public string PresentCurrent_BCMU6
        {
            get { return presentCurrent_BCMU6; }
            set
            {
                SetProperty(ref presentCurrent_BCMU6, value);
            }
        }

        private string maxCellVoltage_BCMU6;
        public string MaxCellVoltage_BCMU6
        {
            get { return maxCellVoltage_BCMU6; }
            set
            {
                SetProperty(ref maxCellVoltage_BCMU6, value);
            }
        }

        private string minCellVoltage_BCMU6;
        public string MinCellVoltage_BCMU6
        {
            get { return minCellVoltage_BCMU6; }
            set
            {
                SetProperty(ref minCellVoltage_BCMU6, value);
            }
        }

        private string maxTemperature_BCMU6;
        public string MaxTemperature_BCMU6
        {
            get { return maxTemperature_BCMU6; }
            set
            {
                SetProperty(ref maxTemperature_BCMU6, value);
            }
        }

        #endregion

        public Monitor_BMS_BCMUPageModel[] bmuViewModels;

        public Monitor_BMSPageModel()
        {
            bmuViewModels = new Monitor_BMS_BCMUPageModel[6];
            for (int i = 0; i < bmuViewModels.Length; i++)
            {
                bmuViewModels[i] = new Monitor_BMS_BCMUPageModel();
            }
        }

        public void ServiceDataCallBack(BatteryTotalModel model, bool IsConnected)
        {
            int index = -1;
            if (model.BCMUID == "BCMU1") index = 1;
            else if (model.BCMUID == "BCMU2") index = 2;
            else if (model.BCMUID == "BCMU3") index = 3;
            else if (model.BCMUID == "BCMU4") index = 4;
            else if (model.BCMUID == "BCMU5") index = 5;
            else if (model.BCMUID == "BCMU6") index = 6;

            this.GetType().GetProperty("SOC_BCMU" + index).SetValue(this, model.TotalSOC.ToString(), null);
            this.GetType().GetProperty("ClusterVoltage_BCMU" + index).SetValue(this, model.TotalVoltage.ToString(), null);
            this.GetType().GetProperty("PresentCurrent_BCMU" + index).SetValue(this, model.TotalCurrent.ToString(), null);
            this.GetType().GetProperty("MaxCellVoltage_BCMU" + index).SetValue(this, model.MaxVoltage.ToString(), null);
            this.GetType().GetProperty("MinCellVoltage_BCMU" + index).SetValue(this, model.MinVoltage.ToString(), null);
            this.GetType().GetProperty("MaxTemperature_BCMU" + index).SetValue(this, model.MaxTemperature.ToString(), null);

            if (model.AlarmStateBCMU.Count > 0)
            {
                bmuViewModels[index - 1].Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_T);
            }
            else
            {
                bmuViewModels[index - 1].Alarmcolor = new SolidColorBrush(BMUColors.Alarmcolor_F);
            }
            
            if(IsConnected)
            {
                bmuViewModels[index - 1].IsConnect = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            else
            {
                bmuViewModels[index - 1].IsConnect = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            }
            
            // 判断是否并离网
            //bmuViewModels[index - 1].IsVisible_SwitchOff;
            //bmuViewModels[index - 1].IsVisible_SwitchOn;

            // 判断充放电
            //bmuViewModels[index - 1].IsVisible_DownArrow;
            //bmuViewModels[index - 1].IsVisible_UpArrow;
        }
    }
}
