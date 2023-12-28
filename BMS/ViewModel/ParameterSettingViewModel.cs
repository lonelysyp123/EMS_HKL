using CommunityToolkit.Mvvm.Input;
using BMS.Common.Modbus.ModbusTCP;
using BMS.Storage.DB.DBManage;
using BMS.Storage.DB.Models;
using BMS.Model;
using BMS.MyControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Service;

namespace BMS.ViewModel
{
    public class ParameterSettingViewModel : ViewModelBase
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
      
        public RelayCommand ReadDBInfoCommand { get; set; }
        public RelayCommand SyncInfoCommand { get; set; }
        public RelayCommand ReadBCMUInfoCommand { get; set; }
        private BMSDataService DevService;
        private string BCMUid;
        public ParameterSettingViewModel(BMSDataService service,string bcmuid)
        {
            DevService = service;
            BCMUid = bcmuid;
           
            ReadDBInfoCommand = new RelayCommand(ReadDBInfo);
            SyncInfoCommand = new RelayCommand(SyncInfo);
            ReadBCMUInfoCommand = new RelayCommand(ReadBCMUInfo);
        }

        private void ReadBCMUInfo()
        {
            byte[] data = DevService.ReadBCMUInfo();
            ClusterVolUpLimitLv1 = BitConverter.ToUInt16(data, 0)*0.1;
            ClusterVolUpLimitLv2 = BitConverter.ToUInt16(data, 2)*0.1;
            ClusterVolUpLimitLv3 = BitConverter.ToUInt16(data, 4)*0.1;
            ClusterVolLowLimitLv1 = BitConverter.ToUInt16(data, 6)*0.1;
            ClusterVolLowLimitLv2 = BitConverter.ToUInt16(data, 8)*0.1;
            ClusterVolLowLimitLv3 = BitConverter.ToUInt16(data, 10)*0.1;
            SingleVolUpLimitLv1 = BitConverter.ToUInt16(data,12 ) * 0.001;
            SingleVolUpLimitLv2 = BitConverter.ToUInt16(data, 14)*0.001;
            SingleVolUpLimitLv3 =BitConverter.ToUInt16(data, 16)*0.001; 
            SingleVolLowLimitLv1 = BitConverter.ToUInt16(data, 18)*0.001;
            SingleVolLowLimitLv2 = BitConverter.ToUInt16(data, 20)*0.001;
            SingleVolLowLimitLv3 = BitConverter.ToUInt16(data, 22)*0.001;
            TempCharUpLimitLv1 = (BitConverter.ToUInt16(data,24 )-2731)*0.1;
            TempCharUpLimitLv2 = (BitConverter.ToUInt16(data,26 )-2731)*0.1;
            TempCharUpLimitLv3 = (BitConverter.ToUInt16(data,28 )-2731)*0.1;
            TempCharLowLimitLv1 = (BitConverter.ToUInt16(data,30 )-2731)*0.1;
            TempCharLowLimitLv2 = (BitConverter.ToUInt16(data, 32)-2731)*0.1;
            TempCharLowLimitLv3 = (BitConverter.ToUInt16(data,34 )-2731)*0.1;
            TempDischarUpLimitLv1 = (BitConverter.ToUInt16(data, 36)-2731)*0.1;
            TempDischarUpLimitLv2 = (BitConverter.ToUInt16(data, 38)-2731)*0.1;
            TempDischarUpLimitLv3 = (BitConverter.ToUInt16(data, 40)-2731)*0.1;
            CurCharLv1 = BitConverter.ToUInt16(data, 42) * 0.1;
            CurCharLv2 = BitConverter.ToUInt16(data, 44)*0.1;
            CurCharLv3 = BitConverter.ToUInt16(data, 46)*0.1;
            CurDischarLv1 = BitConverter.ToUInt16(data,48 )*0.1;
            CurDischarLv2 = BitConverter.ToUInt16(data, 50)*0.1;
            CurDischarLv3 = BitConverter.ToUInt16(data, 52)*0.1;
            SingleVolDiffLv1 = BitConverter.ToUInt16(data, 54) * 0.001;
            SingleVolDiffLv2 = BitConverter.ToUInt16(data, 56)* 0.001;
            SingleVolDiffLv3 = BitConverter.ToUInt16(data, 58)* 0.001;
            SOCLowLimitLv1 = BitConverter.ToUInt16(data, 60) * 0.1;
            SOCLowLimitLv2 = BitConverter.ToUInt16(data, 62) * 0.1;
            SOCLowLimitLv3 = BitConverter.ToUInt16(data, 64) * 0.1;
            IsoRLowLimitLv1 = BitConverter.ToUInt16(data, 66);

        }

        private void SyncInfo()
        {
            AlarmParameterSettingInfoManage  manage = new AlarmParameterSettingInfoManage();
            AlarmParameterSettingModel model = new AlarmParameterSettingModel()
            {
                BCMUID = BCMUid,
                DiffVol1 = SingleVolDiffLv1,
                DiffVol2 = SingleVolDiffLv2,
                DiffVol3 = SingleVolDiffLv3,
                SOCLow1 = SOCLowLimitLv1,
                SOCLow2 = SOCLowLimitLv2,
                SOCLow3 = SOCLowLimitLv3,
                CurChar1 = CurCharLv1,
                CurChar2 = CurCharLv2,
                CurChar3 = CurCharLv3,
                CurDischar1 = CurDischarLv1,
                CurDischar2 = CurDischarLv2,
                CurDischar3 = CurDischarLv3,
                TempCharUp1 = TempCharUpLimitLv1,
                TempCharUp2 = TempCharUpLimitLv2,
                TempCharUp3 = TempCharUpLimitLv3,
                TempCharLow1 = TempCharLowLimitLv1,
                TempCharLow2 = TempCharLowLimitLv2,
                TempCharLow3 = TempCharLowLimitLv3,
                TempDischarUp1 = TempDischarUpLimitLv1,
                TempDischarUp2 = TempDischarUpLimitLv2,
                TempDischarUp3 = TempDischarUpLimitLv3,
                SingleVolUp1 = SingleVolUpLimitLv1,
                SingleVolUp2 = SingleVolUpLimitLv2,
                SingleVolUp3 = SingleVolUpLimitLv3,
                SingleVolLow1 = SingleVolLowLimitLv1,
                SingleVolLow2 = SingleVolLowLimitLv2,
                SingleVolLow3 = SingleVolLowLimitLv3,
                ClusterVolLow1 = ClusterVolLowLimitLv1,
                ClusterVolLow2 = ClusterVolLowLimitLv2,
                ClusterVolLow3 = ClusterVolLowLimitLv3,
                ClusterVolUp1 = ClusterVolUpLimitLv1,
                ClusterVolUp2 = ClusterVolUpLimitLv2,
                ClusterVolUp3 = ClusterVolUpLimitLv3,
                IsoRLow = IsoRLowLimitLv1
            };
            var result = manage.Find(BCMUid);
            if ( result.Count!=0)
            {
                manage.Update(model);

            }
            else
            {
                manage.Insert(model);
            }

            ushort[] values =
            {
                (ushort)(ClusterVolUpLimitLv1*10),
                (ushort)(ClusterVolUpLimitLv2*10),
                (ushort)(ClusterVolUpLimitLv3*10),
                (ushort)(ClusterVolLowLimitLv1*10),
                (ushort)(ClusterVolLowLimitLv2*10),
                (ushort)(ClusterVolLowLimitLv3 *10),
                (ushort)(SingleVolUpLimitLv1*1000),
                (ushort)(SingleVolUpLimitLv2*1000),
                (ushort)(SingleVolUpLimitLv3*1000),
                (ushort)(SingleVolLowLimitLv1*1000),
                (ushort)(SingleVolLowLimitLv2*1000),
                (ushort)(SingleVolLowLimitLv3*1000),
                (ushort)(TempCharUpLimitLv1*10+2731),
                (ushort)(TempCharUpLimitLv2*10+2731),
                (ushort)(TempCharUpLimitLv3*10+2731),
                (ushort)(TempCharLowLimitLv1*10+2731),
                (ushort)(TempCharLowLimitLv2*10+2731),
                (ushort)(TempCharLowLimitLv3*10+2731),
                (ushort)(TempDischarUpLimitLv1*10+2731),
                (ushort)(TempDischarUpLimitLv2*10+2731),
                (ushort)(TempDischarUpLimitLv3 *10+2731),
                (ushort)(CurCharLv1*10),
                (ushort)(CurCharLv2*10),
                (ushort)(CurCharLv3*10),
                (ushort)(CurDischarLv1 *10),
                (ushort)(CurDischarLv2*10),
                (ushort)(CurDischarLv3 *10),
                (ushort)(SingleVolDiffLv1*1000),
                (ushort)(SingleVolDiffLv2*1000),
                (ushort)(SingleVolDiffLv3 *1000),
                (ushort)(SOCLowLimitLv1 *10),
                (ushort)(SOCLowLimitLv2*10),
                (ushort)(SOCLowLimitLv3*10),
                (ushort)IsoRLowLimitLv1
            };
            DevService.SyncBCMUInfo(values);
        }

        private void ReadDBInfo()
        {
            AlarmParameterSettingInfoManage manage = new AlarmParameterSettingInfoManage();
            
            var entities = manage.Find(BCMUid);
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    IsoRLowLimitLv1 = entity.IsoRLow;
                    SingleVolDiffLv1 = entity.DiffVol1;
                    SingleVolDiffLv2 = entity.DiffVol2;
                    SingleVolDiffLv3 = entity.DiffVol3;
                    SOCLowLimitLv1 = entity.SOCLow1;
                    SOCLowLimitLv2 = entity.SOCLow2;
                    SOCLowLimitLv3 = entity.SOCLow3;
                    CurCharLv1 =entity.CurChar1;
                    CurCharLv2 = entity.CurChar2;
                    CurCharLv3 = entity.CurChar3;
                    CurDischarLv1 = entity.CurDischar1;
                    CurDischarLv2 = entity.CurDischar2;
                    CurDischarLv3 = entity.CurDischar3;
                    TempCharUpLimitLv1 = entity.TempCharUp1;
                    TempCharUpLimitLv2 = entity.TempCharUp2;
                    TempCharUpLimitLv3 = entity.TempCharUp3;
                    TempCharLowLimitLv1 = entity.TempCharLow1;
                    TempCharLowLimitLv2 = entity.TempCharLow2;
                    TempCharLowLimitLv3 = entity.TempCharLow3;
                    TempDischarUpLimitLv1 = entity.TempDischarUp1;
                    TempDischarUpLimitLv2 = entity.TempDischarUp2;
                    TempDischarUpLimitLv3 = entity.TempDischarUp3;
                    SingleVolUpLimitLv1 = entity.SingleVolUp1;
                    SingleVolUpLimitLv2 = entity.SingleVolUp2;
                    SingleVolUpLimitLv3=entity.SingleVolUp3;
                    SingleVolLowLimitLv1=entity.SingleVolLow1;
                    SingleVolLowLimitLv2=entity.SingleVolLow2;
                    SingleVolLowLimitLv3= entity.SingleVolLow3;
                    ClusterVolUpLimitLv1 = entity.ClusterVolUp1;
                    ClusterVolUpLimitLv2 = entity.ClusterVolUp2;
                    ClusterVolUpLimitLv3 = entity.ClusterVolUp3;
                    ClusterVolLowLimitLv1 = entity.ClusterVolLow1;
                    ClusterVolLowLimitLv2 = entity.ClusterVolLow2;
                    ClusterVolLowLimitLv3 = entity.ClusterVolLow3;
                }
            }
        }
    }
}
