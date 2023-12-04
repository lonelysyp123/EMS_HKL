using CommunityToolkit.Mvvm.Input;
using EMS.Api;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.MyControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
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

        public RelayCommand ReadClusterVolThreshInfoCommand { get; set; }
        public RelayCommand SyncClusterVolThreshInfoCommand { get; set; }
        public RelayCommand ReadSingleVolThreshInfoCommand { get; set; }
        public RelayCommand SyncSingleVolThreshInfoCommand { get; set; }
        public RelayCommand ReadTempThreshInfoCommand { get; set; }
        public RelayCommand SyncTempThreshInfoCommand { get; set; }
        public RelayCommand ReadCurrThreshInfoCommand { get; set; }
        public RelayCommand SyncCurrThreshInfoCommand { get; set; }
        public RelayCommand ReadSingleVolDiffAndSOCThreshInfoCommand { get; set; }
        public RelayCommand SyncSingleVolDiffAndSOCThreshInfoCommand { get; set; }

        public RelayCommand ReadIsoRThreshInfoCommand { get; set; }
        public RelayCommand SyncIsrRThreshInfoCommand { get; set; }
        private ModbusClient ModbusClient;
        public ParameterSettingViewModel(ModbusClient client)
        {
            ModbusClient = client;
            ReadClusterVolThreshInfoCommand = new RelayCommand(ReadClusterVolThreshInfo);
            SyncClusterVolThreshInfoCommand = new RelayCommand(SyncClusterVolThreshInfo);
            ReadSingleVolThreshInfoCommand = new RelayCommand(ReadSingleVolThreshInfo);
            SyncSingleVolThreshInfoCommand = new RelayCommand(SyncSingleVolThreshInfo);
            ReadTempThreshInfoCommand = new RelayCommand(ReadTempThreshInfo);
            SyncTempThreshInfoCommand = new RelayCommand(SyncTempThreshInfo);
            ReadCurrThreshInfoCommand = new RelayCommand(ReadCurrThreshInfo);
            SyncCurrThreshInfoCommand = new RelayCommand(SyncCurrThreshInfo);
            ReadSingleVolDiffAndSOCThreshInfoCommand = new RelayCommand(ReadSingleVolDiffAndSOCThreshInfo);
            SyncSingleVolDiffAndSOCThreshInfoCommand = new RelayCommand(SyncSingleVolDiffAndSOCThreshInfo);

            ReadIsoRThreshInfoCommand = new RelayCommand(ReadIsoRThreshInfo);
            SyncIsrRThreshInfoCommand = new RelayCommand(SyncIsoRThreshInfo);



        }

        private void SyncIsoRThreshInfo()
        {
            ModbusClient.WriteFunc(40233, (ushort)IsoRLowLimitLv1);
        }

        private void ReadIsoRThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40233, 1);
            IsoRLowLimitLv1 = BitConverter.ToUInt16(data, 0);




        }



        private void SyncSingleVolDiffAndSOCThreshInfo()
        {
            ModbusClient.WriteFunc(40227, (ushort)(SingleVolDiffLv1 * 1000));
            ModbusClient.WriteFunc(40228, (ushort)(SingleVolDiffLv2 * 1000));
            ModbusClient.WriteFunc(40229, (ushort)(SingleVolDiffLv3 * 1000));
            ModbusClient.WriteFunc(40230, (ushort)(SOCLowLimitLv1 * 10));
            ModbusClient.WriteFunc(40231, (ushort)(SOCLowLimitLv2 * 10));
            ModbusClient.WriteFunc(40232, (ushort)(SOCLowLimitLv3 * 10));
        }

        private void ReadSingleVolDiffAndSOCThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40227, 6);
            SingleVolDiffLv1 = BitConverter.ToUInt16(data, 0) * 0.001;
            SingleVolDiffLv2 = BitConverter.ToUInt16(data, 2) * 0.001;
            SingleVolDiffLv3 = BitConverter.ToUInt16(data, 4) * 0.001;

            SOCLowLimitLv1 = BitConverter.ToUInt16(data, 6) * 0.1;
            SOCLowLimitLv2 = BitConverter.ToUInt16(data, 8) * 0.1;
            SOCLowLimitLv3 = BitConverter.ToUInt16(data, 10) * 0.1;


        }

        private void ReadCurrThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40221, 6);
            CurCharLv1 = BitConverter.ToUInt16(data, 0) * 0.1;
            CurCharLv2 = BitConverter.ToUInt16(data, 2) * 0.1;
            CurCharLv3 = BitConverter.ToUInt16(data, 4) * 0.1;
            CurDischarLv1 = BitConverter.ToUInt16(data, 6) * 0.1;
            CurDischarLv2 = BitConverter.ToUInt16(data, 8) * 0.1;
            CurDischarLv3 = BitConverter.ToUInt16(data, 10) * 0.1;
        }


        private void SyncCurrThreshInfo()
        {
            ModbusClient.WriteFunc(40221, (ushort)(CurCharLv1 * 10));
            ModbusClient.WriteFunc(40222, (ushort)(CurCharLv2 * 10));
            ModbusClient.WriteFunc(40223, (ushort)(CurCharLv3 * 10));
            ModbusClient.WriteFunc(40224, (ushort)(CurDischarLv1 * 10));
            ModbusClient.WriteFunc(40225, (ushort)(CurDischarLv2 * 10));
            ModbusClient.WriteFunc(40226, (ushort)(CurDischarLv3 * 10));
        }

        private void ReadTempThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40212, 9);
            TempCharUpLimitLv1 = (BitConverter.ToUInt16(data, 0) - 2731) * 0.1;
            TempCharUpLimitLv2 = (BitConverter.ToUInt16(data, 2) - 2731) * 0.1;
            TempCharUpLimitLv3 = (BitConverter.ToUInt16(data, 4) - 2731) * 0.1;
            TempCharLowLimitLv1 = (BitConverter.ToUInt16(data, 6) - 2731) * 0.1;
            TempCharLowLimitLv2 = (BitConverter.ToUInt16(data, 8) - 2731) * 0.1;
            TempCharLowLimitLv3 = (BitConverter.ToUInt16(data, 10) - 2731) * 0.1;
            TempDischarUpLimitLv1 = (BitConverter.ToUInt16(data, 12) - 2731) * 0.1;
            TempDischarUpLimitLv2 = (BitConverter.ToUInt16(data, 14) - 2731) * 0.1;
            TempDischarUpLimitLv3 = (BitConverter.ToUInt16(data, 16) - 2731) * 0.1;


        }

        private void SyncTempThreshInfo()
        {

            ModbusClient.WriteFunc(40212, (ushort)(TempCharUpLimitLv1 * 10 + 2731));
            ModbusClient.WriteFunc(40213, (ushort)(TempCharUpLimitLv2 * 10 + 2731));
            ModbusClient.WriteFunc(40214, (ushort)(TempCharUpLimitLv3 * 10 + 2731));
            ModbusClient.WriteFunc(40215, (ushort)(TempCharLowLimitLv1 * 10 + 2731));
            ModbusClient.WriteFunc(40216, (ushort)(TempCharLowLimitLv2 * 10 + 2731));
            ModbusClient.WriteFunc(40217, (ushort)(TempCharLowLimitLv3 * 10 + 2731));
            ModbusClient.WriteFunc(40218, (ushort)(TempDischarUpLimitLv1 * 10 + 2731));
            ModbusClient.WriteFunc(40219, (ushort)(TempDischarUpLimitLv2 * 10 + 2731));
            ModbusClient.WriteFunc(40220, (ushort)(TempDischarUpLimitLv3 * 10 + 2731));

        }

        private void ReadSingleVolThreshInfo()
        {
            byte[] data = ModbusClient.ReadFunc(40206, 6);
            SingleVolUpLimitLv1 = BitConverter.ToInt16(data, 0) * 0.001;
            SingleVolUpLimitLv2 = BitConverter.ToInt16(data, 2) * 0.001;
            SingleVolUpLimitLv3 = BitConverter.ToInt16(data, 4) * 0.001;
            SingleVolLowLimitLv1 = BitConverter.ToUInt16(data, 6) * 0.001;
            SingleVolLowLimitLv2 = BitConverter.ToUInt16(data, 8) * 0.001;
            SingleVolLowLimitLv3 = BitConverter.ToUInt16(data, 10) * 0.001;
        }

        private void SyncSingleVolThreshInfo()
        {
            ModbusClient.WriteFunc(40206, (ushort)(SingleVolUpLimitLv1 * 1000));
            ModbusClient.WriteFunc(40207, (ushort)(SingleVolUpLimitLv2 * 1000));
            ModbusClient.WriteFunc(40208, (ushort)(SingleVolUpLimitLv3 * 1000));
            ModbusClient.WriteFunc(40209, (ushort)(SingleVolLowLimitLv1 * 1000));
            ModbusClient.WriteFunc(40210, (ushort)(SingleVolLowLimitLv2 * 1000));
            ModbusClient.WriteFunc(40211, (ushort)(SingleVolLowLimitLv3 * 1000));
        }

        private void ReadClusterVolThreshInfo()
        {

            byte[] data = ModbusClient.ReadFunc(40200, 6);
            ClusterVolUpLimitLv1 = BitConverter.ToInt16(data, 0) * 0.1;
            ClusterVolUpLimitLv2 = BitConverter.ToInt16(data, 2) * 0.1;
            ClusterVolUpLimitLv3 = BitConverter.ToInt16(data, 4) * 0.1;
            ClusterVolLowLimitLv1 = BitConverter.ToUInt16(data, 6) * 0.1;
            ClusterVolLowLimitLv2 = BitConverter.ToUInt16(data, 8) * 0.1;
            ClusterVolLowLimitLv3 = BitConverter.ToUInt16(data, 10) * 0.1;

        }
        
        private void SyncClusterVolThreshInfo()
        {
            


            ModbusClient.WriteFunc(40200, (ushort)(ClusterVolUpLimitLv1 * 10));
            ModbusClient.WriteFunc(40201, (ushort)(ClusterVolUpLimitLv2 * 10));
            ModbusClient.WriteFunc(40202, (ushort)(ClusterVolUpLimitLv3 * 10));
            ModbusClient.WriteFunc(40203, (ushort)(ClusterVolLowLimitLv1 * 10));
            ModbusClient.WriteFunc(40204, (ushort)(ClusterVolLowLimitLv2 * 10));
            ModbusClient.WriteFunc(40205, (ushort)(ClusterVolLowLimitLv3 * 10));
        }


    }
}
