using CommunityToolkit.Mvvm.Input;
using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Mqtt;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Strategy_ProtectSetterPageModel:ViewModelBase
    {
        //BCMU簇选择
        private List<string> _bcmuId;
        public List<string> BcmuId
        {
            get => _bcmuId;
            set 
            {
                SetProperty(ref _bcmuId, value);
            }
        }

        #region ProtectPara
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

        /// <summary>
        /// BUS侧上限电压
        /// </summary>
        private double _bUSHigherVolThresh;
        public double BUSUpperLimitVolThresh
        {
            get => _bUSHigherVolThresh;
            set
            {
                SetProperty(ref _bUSHigherVolThresh, value);
            }
        }

        /// <summary>
        /// BUS侧下限电压
        /// </summary>
        private double _bUSLowerLimitVolThresh;
        public double BUSLowerLimitVolThresh
        {
            get => _bUSLowerLimitVolThresh;
            set
            {
                SetProperty(ref _bUSLowerLimitVolThresh, value);
            }
        }

        /// <summary>
        /// BUS侧高压设置
        /// </summary>
        private double _bUSHVolSetting;
        public double BUSHVolSetting
        {
            get => _bUSHVolSetting;
            set
            {
                SetProperty(ref _bUSHVolSetting, value);
            }
        }

        /// <summary>
        /// BUS侧低压设置
        /// </summary>
        private double _bUSLVolSetiing;
        public double BUSLVolSetting
        {
            get => _bUSLVolSetiing;
            set
            {
                SetProperty(ref _bUSLVolSetiing, value);
            }
        }

        /// <summary>
        /// 电池下限电压
        /// </summary>
        private double _bTLLimitVol;
        public double BTLLimitVol
        {
            get => _bTLLimitVol;
            set
            {
                SetProperty(ref _bTLLimitVol, value);
            }
        }

        /// <summary>
        /// 放电终止电压
        /// </summary>
        private double _dischargeSTVol;
        public double DischargeSTVol
        {
            get => _dischargeSTVol;
            set
            {
                SetProperty(ref _dischargeSTVol, value);
            }
        }

        /// <summary>
        /// 多支路电流调节参数
        /// </summary>
        private int _multiBranchCurRegPar;
        public int MultiBranchCurRegPar
        {
            get => _multiBranchCurRegPar;
            set
            {
                SetProperty(ref _multiBranchCurRegPar, value);
            }
        }

        /// <summary>
        /// 电池均充电压
        /// </summary>
        private double _batAveChVol;
        public double BatAveChVol
        {
            get => _batAveChVol;
            set
            {
                SetProperty(ref _batAveChVol, value);
            }
        }

        /// <summary>
        /// 充电截止电流
        /// </summary>
        private double _chCutCurrent;
        public double ChCutCurrent
        {
            get => _chCutCurrent;
            set
            {
                SetProperty(ref _chCutCurrent, value);
            }
        }

        /// <summary>
        /// 最大充电电流
        /// </summary>
        private double _maxChCurrent;
        public double MaxChCurrent
        {
            get => _maxChCurrent;
            set
            {
                SetProperty(ref _maxChCurrent, value);
            }
        }

        /// <summary>
        /// 最大放电电流
        /// </summary>
        private double _maxDisChCurrent;
        public double MaxDisChCurrent
        {
            get => _maxDisChCurrent;
            set
            {
                SetProperty(ref _maxDisChCurrent, value);
            }
        }

        #endregion

        #region Command
        public RelayCommand ReadDBInfoCommand { get; set; }
        public RelayCommand ReadBCMUInfoCommand { get; set; }
        public RelayCommand SyncInfoCommand { get; set; }
        public RelayCommand ReadBUSVolInfoCommand { get; set; }
        public RelayCommand SyncBUSVolInfoCommand { get; set; }
        public RelayCommand ReadDCBranchInfoCommand { get; set; }
        public RelayCommand SyncDCBranchInfoCommand { get; set; }
        #endregion

        private BMSDataService bmsDataService;
        private PCSDataService pcsDataService;

        public Strategy_ProtectSetterPageModel(BMSDataService bmsDataService, PCSDataService pcsDataService)
        {
            BcmuId = new List<string> { "BCMUID(1)", "BCMUID(2)", "BCMUID(3)", "BCMUID(4)", "BCMUID(5)", "BCMUID(6)" };


            ReadDBInfoCommand = new RelayCommand(ReadDBInfo);
            ReadBCMUInfoCommand = new RelayCommand(ReadBCMUInfo);
            SyncInfoCommand = new RelayCommand(SyncInfo);


            ReadBUSVolInfoCommand = new RelayCommand(ReadBUSVolInfo);
            SyncBUSVolInfoCommand = new RelayCommand(SyncBUSVolInfo);
            ReadDCBranchInfoCommand = new RelayCommand(ReadDCBranchInfo);
            SyncDCBranchInfoCommand = new RelayCommand(SyncDCBranchInfo);

            this.bmsDataService = bmsDataService;
            this.pcsDataService = pcsDataService;
        }

        private void ReadDBInfo()
        {
            //
        }

        private void ReadBCMUInfo()
        {
            BMSParameterSettingModel model = new BMSParameterSettingModel();
            this.ClusterVolUpLimitLv1 = model.ClusterVolUpLimitLv1;
            this.ClusterVolUpLimitLv2 = model.ClusterVolUpLimitLv2;
            this.ClusterVolUpLimitLv3 = model.ClusterVolUpLimitLv3;
            this.ClusterVolLowLimitLv1 = model.ClusterVolLowLimitLv1;
            this.ClusterVolLowLimitLv2 = model.ClusterVolLowLimitLv2;
            this.ClusterVolLowLimitLv3 = model.ClusterVolLowLimitLv3;
            this.SingleVolUpLimitLv1 = model.SingleVolUpLimitLv1;
            this.SingleVolUpLimitLv2 = model.SingleVolUpLimitLv2;
            this.SingleVolUpLimitLv3 = model.SingleVolUpLimitLv3;
            this.SingleVolLowLimitLv1 = model.SingleVolLowLimitLv1;
            this.SingleVolLowLimitLv2 = model.SingleVolLowLimitLv2;
            this.SingleVolLowLimitLv3 = model.SingleVolLowLimitLv3;
            this.TempCharUpLimitLv1 = model.TempCharUpLimitLv1;
            this.TempCharUpLimitLv2 = model.TempCharUpLimitLv2;
            this.TempCharUpLimitLv3 = model.TempCharUpLimitLv3;
            this.TempCharLowLimitLv1 = model.TempCharLowLimitLv1;
            this.TempCharLowLimitLv2 = model.TempCharLowLimitLv2;
            this.TempCharLowLimitLv3 = model.TempCharLowLimitLv3;
            this.TempDischarUpLimitLv1 = model.TempDischarUpLimitLv1;
            this.TempDischarUpLimitLv2 = model.TempDischarUpLimitLv2;
            this.TempDischarUpLimitLv3 = model.TempDischarUpLimitLv3;
            this.CurCharLv1 = model.CurCharLv1;
            this.CurCharLv2 = model.CurCharLv2;
            this.CurCharLv3 = model.CurCharLv3;
            this.CurDischarLv1 = model.CurDischarLv1;
            this.CurDischarLv2 = model.CurDischarLv2;
            this.CurDischarLv3 = model.CurDischarLv3;
            this.SingleVolDiffLv1 = model.SingleVolDiffLv1;
            this.SingleVolDiffLv2 = model.SingleVolDiffLv2;
            this.SingleVolDiffLv3 = model.SingleVolDiffLv3;
            this.SOCLowLimitLv1 = model.SOCLowLimitLv1;
            this.SOCLowLimitLv2 = model.SOCLowLimitLv2;
            this.SOCLowLimitLv3 = model.SOCLowLimitLv3;
            this.IsoRLowLimitLv1 = model.IsoRLowLimitLv1;
            //解析model
        }

        private void SyncInfo()
        {
            BMSParameterSettingModel model = new BMSParameterSettingModel();
            model.ClusterVolUpLimitLv1 = this.ClusterVolUpLimitLv1;
            model.ClusterVolUpLimitLv2 = this.ClusterVolUpLimitLv2;
            model.ClusterVolUpLimitLv3 = this.ClusterVolUpLimitLv3;
            model.ClusterVolLowLimitLv1 = this.ClusterVolLowLimitLv1;
            model.ClusterVolLowLimitLv2 = this.ClusterVolLowLimitLv2;
            model.ClusterVolLowLimitLv3 = this.ClusterVolLowLimitLv3;
            model.SingleVolUpLimitLv1 = this.SingleVolUpLimitLv1;
            model.SingleVolUpLimitLv2 = this.SingleVolUpLimitLv2;
            model.SingleVolUpLimitLv3 = this.SingleVolUpLimitLv3;
            model.SingleVolLowLimitLv1 = this.SingleVolLowLimitLv1;
            model.SingleVolLowLimitLv2 = this.SingleVolLowLimitLv2;
            model.SingleVolLowLimitLv3 = this.SingleVolLowLimitLv3;
            model.TempCharUpLimitLv1 = this.TempCharUpLimitLv1;
            model.TempCharUpLimitLv2 = this.TempCharUpLimitLv2;
            model.TempCharUpLimitLv3 = this.TempCharUpLimitLv3;
            model.TempCharLowLimitLv1 = this.TempCharLowLimitLv1;
            model.TempCharLowLimitLv2 = this.TempCharLowLimitLv2;
            model.TempCharLowLimitLv3 = this.TempCharLowLimitLv3;
            model.TempDischarUpLimitLv1 = this.TempDischarUpLimitLv1;
            model.TempDischarUpLimitLv2 = this.TempDischarUpLimitLv2;
            model.TempDischarUpLimitLv3 = this.TempDischarUpLimitLv3;
            model.CurCharLv1 = this.CurCharLv1;
            model.CurCharLv2 = this.CurCharLv2;
            model.CurCharLv3 = this.CurCharLv3;
            model.CurDischarLv1 = this.CurDischarLv1;
            model.CurDischarLv2 = this.CurDischarLv2;
            model.CurDischarLv3 = this.CurDischarLv3;
            model.SingleVolDiffLv1 = this.SingleVolDiffLv1;
            model.SingleVolDiffLv2 = this.SingleVolDiffLv2;
            model.SingleVolDiffLv3 = this.SingleVolDiffLv3;
            model.SOCLowLimitLv1 = this.SOCLowLimitLv1;
            model.SOCLowLimitLv2 = this.SOCLowLimitLv2;
            model.SOCLowLimitLv3 = this.SOCLowLimitLv3;
            model.IsoRLowLimitLv1 = this.IsoRLowLimitLv1;

            //bmsDataService.SyncInfo(model);
        }

        private void ReadBUSVolInfo()
        {
            PCSParSettingModel model = new PCSParSettingModel();
            //解析model
            this.BUSUpperLimitVolThresh = model.BUSUpperLimitVolThresh;
            this.BUSLowerLimitVolThresh = model.BUSLowerLimitVolThresh;
            this.BUSHVolSetting = model.BUSHVolSetting;
            this.BUSLVolSetting = model.BUSLVolSetting;

        }

        private void SyncBUSVolInfo()
        {
            PCSParSettingModel model = new PCSParSettingModel();
            model.BUSUpperLimitVolThresh = this.BUSUpperLimitVolThresh;
            model.BUSLowerLimitVolThresh = this.BUSLowerLimitVolThresh;
            model.BUSHVolSetting = this.BUSHVolSetting;
            model.BUSLVolSetting = this.BUSLVolSetting;
            //pcsService.SyncBUSVolInfo(model);
        }

        private void ReadDCBranchInfo()
        {
            //PCSInfoModel model = pcsService.DCBranchInfo();
            //解析model
            PCSParSettingModel model = new PCSParSettingModel();
            this.BTLLimitVol = model.BTLLimitVol;
            this.DischargeSTVol = model.DischargeSTVol;
            this.MultiBranchCurRegPar = model.MultiBranchCurRegPar;
            this.BatAveChVol = model.BatAveChVol;
            this.ChCutCurrent = model.ChCutCurrent;
            this.MaxChCurrent = model.MaxChCurrent;
            this.MaxDisChCurrent = model.MaxDisChCurrent;
        }

        private void SyncDCBranchInfo()
        {
            //PCSInfoModel model = new DCBranchInfo();
            //model.xxxx = this.xxxx
            //pcsService.SyncDCBranchInfo(model);
            PCSParSettingModel model = new PCSParSettingModel();
            model.BTLLimitVol = this.BTLLimitVol;
            model.DischargeSTVol = this.DischargeSTVol;
            model.MultiBranchCurRegPar = this.MultiBranchCurRegPar;
            model.BatAveChVol = this.BatAveChVol;
            model.ChCutCurrent = this.ChCutCurrent;
            model.MaxChCurrent = this.MaxChCurrent;
            model.MaxDisChCurrent = this.MaxDisChCurrent;

        }

    }
}
