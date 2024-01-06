using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string selectedBCMUID;
        public string SelectedBCMUID
        {
            get => selectedBCMUID;
            set
            {
                SetProperty(ref  selectedBCMUID, value);
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
        /// 放电温度下限
        /// </summary>
        private double _tempDischarLowLimitLv2;
        public double TempDischarLowLimitLv2
        {
            get => _tempDischarLowLimitLv2;
            set
            {
                SetProperty(ref _tempDischarLowLimitLv2, value);
            }
        }

        private double _tempDischarLowLimitLv1;
        public double TempDischarLowLimitLv1
        {
            get => _tempDischarLowLimitLv1;
            set
            {
                SetProperty(ref _tempDischarLowLimitLv1, value);
            }
        }

        private double _tempDischarLowLimitLv3;
        public double TempDischarLowLimitLv3
        {
            get => _tempDischarLowLimitLv3;
            set
            {
                SetProperty(ref _tempDischarLowLimitLv3, value);
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
        public RelayCommand SyncInfo1Command { get; set; }
        public RelayCommand SyncInfo13Command { get; set; }
        public RelayCommand SyncInfo2Command { get; set; }
        public RelayCommand SyncInfo3Command { get; set; }
        public RelayCommand SyncInfo4Command { get; set; }
        public RelayCommand SyncInfo5Command { get; set; }
        public RelayCommand SyncInfo6Command { get; set; }
        public RelayCommand SyncInfo7Command { get; set; }
        public RelayCommand SyncInfo8Command { get; set; }
        public RelayCommand SyncInfo9Command { get; set; }
        public RelayCommand SyncInfo10Command { get; set; }
        public RelayCommand SyncInfo11Command { get; set; }
        public RelayCommand SyncInfo12Command { get; set; }
        
        public RelayCommand ReadBUSVolInfoCommand { get; set; }
        public RelayCommand SyncBUSVolInfoCommand { get; set; }
        public RelayCommand ReadDCBranchInfoCommand { get; set; }
        public RelayCommand SyncDCBranchInfoCommand { get; set; }
        #endregion

        private BMSDataService bmsDataService;
        //private PCSDataService pcsDataService;

        public Strategy_ProtectSetterPageModel()
        {
            ReadDBInfoCommand = new RelayCommand(ReadDBInfo);
            ReadBCMUInfoCommand = new RelayCommand(ReadBCMUInfo);
            SyncInfo1Command = new RelayCommand(SyncInfo1);
            SyncInfo2Command = new RelayCommand(SyncInfo2);
            SyncInfo3Command = new RelayCommand(SyncInfo3);
            SyncInfo4Command = new RelayCommand(SyncInfo4);
            SyncInfo5Command = new RelayCommand(SyncInfo5);
            SyncInfo6Command = new RelayCommand(SyncInfo6);
            SyncInfo7Command = new RelayCommand(SyncInfo7);
            SyncInfo8Command = new RelayCommand(SyncInfo8);
            SyncInfo9Command = new RelayCommand(SyncInfo9);
            SyncInfo10Command = new RelayCommand(SyncInfo10);
            SyncInfo11Command = new RelayCommand(SyncInfo11);
            SyncInfo12Command = new RelayCommand(SyncInfo12);
            SyncInfo13Command = new RelayCommand(SyncInfo13);
            ReadBUSVolInfoCommand = new RelayCommand(ReadBUSVolInfo);
            SyncBUSVolInfoCommand = new RelayCommand(SyncBUSVolInfo);
            ReadDCBranchInfoCommand = new RelayCommand(ReadDCBranchInfo);
            SyncDCBranchInfoCommand = new RelayCommand(SyncDCBranchInfo);
        }

        private void ReadDBInfo()
        {
            //
        }

        private void ReadBCMUInfo()
        {
            //BCMUInfoModel model = bmsService.ReadBCMUInfo();
            //解析model
            byte[] data;
            data=BmsApi.GetBMSProtectSet(selectedBCMUID);
            ClusterVolUpLimitLv1 = BitConverter.ToUInt16(data, 0) * 0.1;
            ClusterVolUpLimitLv2 = BitConverter.ToUInt16(data, 2) * 0.1;
            ClusterVolUpLimitLv3 = BitConverter.ToUInt16(data, 4) * 0.1;
            ClusterVolLowLimitLv1 = BitConverter.ToUInt16(data, 6) * 0.1;
            ClusterVolLowLimitLv2 = BitConverter.ToUInt16(data, 8) * 0.1;
            ClusterVolLowLimitLv3 = BitConverter.ToUInt16(data, 10) * 0.1;
            SingleVolUpLimitLv1 = BitConverter.ToUInt16(data, 12) * 0.001;
            SingleVolUpLimitLv2 = BitConverter.ToUInt16(data, 14) * 0.001;
            SingleVolUpLimitLv3 = BitConverter.ToUInt16(data, 16) * 0.001;
            SingleVolLowLimitLv1 = BitConverter.ToUInt16(data, 18) * 0.001;
            SingleVolLowLimitLv2 = BitConverter.ToUInt16(data, 20) * 0.001;
            SingleVolLowLimitLv3 = BitConverter.ToUInt16(data, 22) * 0.001;
            TempCharUpLimitLv1 = (BitConverter.ToUInt16(data, 24) - 2731) * 0.1;
            TempCharUpLimitLv2 = (BitConverter.ToUInt16(data, 26) - 2731) * 0.1;
            TempCharUpLimitLv3 = (BitConverter.ToUInt16(data, 28) - 2731) * 0.1;
            TempCharLowLimitLv1 = (BitConverter.ToUInt16(data, 30) - 2731) * 0.1;
            TempCharLowLimitLv2 = (BitConverter.ToUInt16(data, 32) - 2731) * 0.1;
            TempCharLowLimitLv3 = (BitConverter.ToUInt16(data, 34) - 2731) * 0.1;
            TempDischarUpLimitLv1 = (BitConverter.ToUInt16(data, 36) - 2731) * 0.1;
            TempDischarUpLimitLv2 = (BitConverter.ToUInt16(data, 38) - 2731) * 0.1;
            TempDischarUpLimitLv3 = (BitConverter.ToUInt16(data, 40) - 2731) * 0.1;


            TempDischarLowLimitLv1 = (BitConverter.ToUInt16(data, 42) - 2731) * 0.1;
            TempDischarLowLimitLv2 = (BitConverter.ToUInt16(data, 44) - 2731) * 0.1;
            TempDischarLowLimitLv3 = (BitConverter.ToUInt16(data, 46) - 2731) * 0.1;
            CurCharLv1 = BitConverter.ToInt16(data, 48) * 0.1;
            CurCharLv2 = BitConverter.ToInt16(data, 50) * 0.1;
            CurCharLv3 = BitConverter.ToInt16(data, 52) * 0.1;
            CurDischarLv1 = BitConverter.ToInt16(data, 54) * 0.1;
            CurDischarLv2 = BitConverter.ToInt16(data, 56) * 0.1;
            CurDischarLv3 = BitConverter.ToInt16(data, 58) * 0.1;
            SingleVolDiffLv1 = BitConverter.ToUInt16(data, 60) * 0.001;
            SingleVolDiffLv2 = BitConverter.ToUInt16(data, 62) * 0.001;
            SingleVolDiffLv3 = BitConverter.ToUInt16(data, 64) * 0.001;
            SOCLowLimitLv1 = BitConverter.ToUInt16(data, 66) * 0.1;
            SOCLowLimitLv2 = BitConverter.ToUInt16(data, 68) * 0.1;
            SOCLowLimitLv3 = BitConverter.ToUInt16(data, 70) * 0.1;
            IsoRLowLimitLv1 = BitConverter.ToUInt16(data, 72);
        }

        private void SyncInfo1()
        {
           
        }

        private void SyncInfo2()
        {

        }

        private void SyncInfo3()
        {

        }

        private void SyncInfo4()
        {

        }

        private void SyncInfo5()
        {

        }

        private void SyncInfo6()
        {

        }

        private void SyncInfo7()
        {

        }

        private void SyncInfo8()
        {

        }

        private void SyncInfo9()
        {

        }

        private void SyncInfo10()
        {

        }

        private void SyncInfo11()
        {

        }

        private void SyncInfo12()
        {

        }

        private void SyncInfo13()
        {

        }

        
        private void ReadBUSVolInfo()
        {
            //PCSInfoModel model = pcsService.BUSVolInfo();
            //解析model
        }

        private void SyncBUSVolInfo()
        {
            //PCSInfoModel model = new BUSVolInfo();
            //model.xxxx = this.xxxx
            //pcsService.SyncBUSVolInfo(model);
        }

        private void ReadDCBranchInfo()
        {
            //PCSInfoModel model = pcsService.DCBranchInfo();
            //解析model
        }

        private void SyncDCBranchInfo()
        {
            //PCSInfoModel model = new DCBranchInfo();
            //model.xxxx = this.xxxx
            //pcsService.SyncDCBranchInfo(model);
        }

    }
}
