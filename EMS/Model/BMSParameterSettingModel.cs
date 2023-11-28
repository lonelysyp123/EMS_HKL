using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class BMSParameterSettingModel:ObservableObject
    {
        /// <summary>
        ///组端电压上限
        /// </summary>
        private double _clusterVolUpLimitLv1;
        public double ClusterVolUpLimitLv1
        {
            get => _clusterVolUpLimitLv1;
            set
            {
                SetProperty(ref _clusterVolUpLimitLv1, value);
            }
        }

        private double _clusterVolUpLimitLv2;
        public double ClusterVolUpLimitLv2
        {
            get => _clusterVolUpLimitLv2;
            set
            {
                SetProperty(ref _clusterVolUpLimitLv2, value);
            }
        }

        private double _clusterVolUpLimitLv3;
        public double ClusterVolUpLimitLv3
        {
            get => _clusterVolUpLimitLv3;
            set
            {
                SetProperty(ref _clusterVolUpLimitLv3, value);
            }

        }

        /// <summary>
        /// 组端电压下限
        /// </summary>
        private double _clusterVolLowLimitLv1;
        public double ClusterVolLowLimitLv1
        {
            get => _clusterVolLowLimitLv1;
            set
            {
                SetProperty(ref _clusterVolLowLimitLv1, value);
            }
        }


        private double _clusterVolLowLimitLv2;
        public double ClusterVolLowLimitLv2
        {
            get => _clusterVolLowLimitLv2;
            set
            {
                SetProperty(ref _clusterVolLowLimitLv2, value);
            }
        }


        private double _clusterVolLowLimitLv3;
        public double ClusterVolLowLimitLv3
        {
            get => _clusterVolLowLimitLv3;
            set
            {
                SetProperty(ref _clusterVolLowLimitLv3, value);
            }
        }

        /// <summary>
        /// 单体电压上限
        /// </summary>
        private double _singleVolUpLimitLv1;
        public double SingleVolUpLimitLv1
        {
            get => _singleVolUpLimitLv1;
            set
            {
                SetProperty(ref _singleVolUpLimitLv1, value);
            }
        }


        private double _singleVolUpLimitLv2;
        public double SingleVolUpLimitLv2
        {
            get => _singleVolUpLimitLv2;
            set
            {
                SetProperty(ref _singleVolUpLimitLv2, value);
            }
        }

        private double _singleVolUpLimitLv3;
        public double SingleVolUpLimitLv3
        {
            get => _singleVolUpLimitLv3;
            set
            {
                SetProperty(ref _singleVolUpLimitLv3, value);
            }
        }
        /// <summary>
        /// 单体电压下限
        /// </summary>
        private double _singleVolLowLimitLv1;
        public double SingleVolLowLimitLv1
        {
            get => _singleVolLowLimitLv1;
            set
            {
                SetProperty(ref _singleVolLowLimitLv1, value);
            }
        }

        private double _singleVolLowLimitLv2;
        public double SingleVolLowLimitLv2
        {
            get => _singleVolLowLimitLv2;
            set
            {
                SetProperty(ref _singleVolLowLimitLv2, value);
            }
        }

        private double _singleVolLowLimitLv3;
        public double SingleVolLowLimitLv3
        {
            get => _singleVolLowLimitLv3;
            set
            {
                SetProperty(ref _singleVolLowLimitLv3, value);
            }
        }


        /// <summary>
        /// 充电温度上限
        /// </summary>
        private double _tempCharUpLimitLv1;
        public double TempCharUpLimitLv1
        {
            get => _tempCharUpLimitLv1;
            set
            {
                SetProperty(ref _tempCharUpLimitLv1, value);
            }
        }

        private double _tempCharUpLimitLv2;
        public double TempCharUpLimitLv2
        {
            get => _tempCharUpLimitLv2;
            set
            {
                SetProperty(ref _tempCharUpLimitLv2, value);
            }
        }

        private double _tempCharUpLimitLv3;
        public double TempCharUpLimitLv3
        {
            get => _tempCharUpLimitLv3;
            set
            {
                SetProperty(ref _tempCharUpLimitLv3, value);
            }
        }

        /// <summary>
        /// 充电温度下限
        /// </summary>
        private double _tempCharLowLimitLv1;
        public double TempCharLowLimitLv1
        {
            get => _tempCharLowLimitLv1;
            set
            {
                SetProperty(ref _tempCharLowLimitLv1, value);
            }
        }

        private double _tempCharLowLimitLv2;
        public double TempCharLowLimitLv2
        {
            get => _tempCharLowLimitLv2;
            set
            {
                SetProperty(ref _tempCharLowLimitLv2, value);
            }
        }

        private double _tempCharLowLimitLv3;
        public double TempCharLowLimitLv3
        {
            get => _tempCharLowLimitLv3;
            set
            {
                SetProperty(ref _tempCharLowLimitLv3, value);
            }
        }

        /// <summary>
        /// 放电温度上限
        /// </summary>
        private double _tempDischarUpLimitLv1;
        public double TempDischarUpLimitLv1
        {
            get => _tempDischarUpLimitLv1;
            set
            {
                SetProperty(ref _tempDischarUpLimitLv1, value);
            }
        }

        private double _tempDischarUpLimitLv2;
        public double TempDischarUpLimitLv2
        {
            get => _tempDischarUpLimitLv2;
            set
            {
                SetProperty(ref _tempDischarUpLimitLv2, value);
            }
        }

        private double _tempDischarUpLimitLv3;
        public double TempDischarUpLimitLv3
        {
            get => _tempDischarUpLimitLv3;
            set
            {
                SetProperty(ref _tempDischarUpLimitLv3, value);
            }
        }

        /// <summary>
        /// 充电电流
        /// </summary>
        private double _curCharLv1;
        public double CurCharLv1
        {
            get => _curCharLv1;
            set
            {
                SetProperty(ref _curCharLv1, value);
            }
        }

        private double _curCharLv2;
        public double CurCharLv2
        {
            get => _curCharLv2;
            set
            {
                SetProperty(ref _curCharLv2, value);
            }
        }

        private double _curCharLv3;
        public double CurCharLv3
        {
            get => _curCharLv3;
            set
            {
                SetProperty(ref _curCharLv3, value);
            }
        }

        /// <summary>
        /// 放电电流
        /// </summary>
        private double _curDischarLv1;
        public double CurDischarLv1
        {
            get => _curDischarLv1;
            set
            {
                SetProperty(ref _curDischarLv1, value);
            }
        }

        private double _curDischarLv2;
        public double CurDischarLv2
        {
            get => _curDischarLv2;
            set
            {
                SetProperty(ref _curDischarLv2, value);
            }
        }

        private double _curDischarLv3;
        public double CurDischarLv3
        {
            get => _curDischarLv3;
            set
            {
                SetProperty(ref _curDischarLv3, value);
            }
        }

        /// <summary>
        /// 单体压差
        /// </summary>
        private double _singleVolDiffLv1;
        public double SingleVolDiffLv1
        {
            get => _singleVolDiffLv1;
            set
            {
                SetProperty(ref _singleVolDiffLv1, value);
            }
        }

        private double _singleVolDiffLv2;
        public double SingleVolDiffLv2
        {
            get => _singleVolDiffLv2;
            set
            {
                SetProperty(ref _singleVolDiffLv2, value);
            }
        }

        private double _singleVolDiffLv3;
        public double SingleVolDiffLv3
        {
            get => _singleVolDiffLv3;
            set
            {
                SetProperty(ref _singleVolDiffLv3, value);
            }
        }

        /// <summary>
        /// SOC下限
        /// </summary>
        private double _sOCLowLimitLv1;
        public double SOCLowLimitLv1
        {
            get => _sOCLowLimitLv1;
            set
            {
                SetProperty(ref _sOCLowLimitLv1, value);
            }
        }

        private double _sOCLowLimitLv2;
        public double SOCLowLimitLv2
        {
            get => _sOCLowLimitLv2;
            set
            {
                SetProperty(ref _sOCLowLimitLv2, value);
            }
        }

        private double _sOCLowLimitLv3;
        public double SOCLowLimitLv3
        {
            get => _sOCLowLimitLv3;
            set
            {
                SetProperty(ref _sOCLowLimitLv3, value);
            }
        }

        /// <summary>
        /// 绝缘电阻下限
        /// </summary>
        private double _isoRLowLimitLv1;
        public double IsoRLowLimitLv1
        {
            get => _isoRLowLimitLv1;
            set
            {
                SetProperty(ref _isoRLowLimitLv1, value);
            }
        }
    }
}
