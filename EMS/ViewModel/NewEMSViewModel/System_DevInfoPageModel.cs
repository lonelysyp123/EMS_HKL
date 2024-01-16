using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class System_DevInfoPageModel:ViewModelBase
    {
		#region ObservableObject
		private int _version_Software;
		/// <summary>
		/// EMS软件版本
		/// </summary>
		public int Version_Software
        {
			get => _version_Software;
			set
			{
				SetProperty(ref _version_Software, value);
			}
		}


		private string _cabSerialNumber;
		/// <summary>
		///	PCS机柜序列号
		/// </summary>
		public string CabSerialNumber
        {
			get => _cabSerialNumber;
			set
			{
				SetProperty(ref _cabSerialNumber, value);
			}
		}

		private ushort _monitorSoftCode;
        /// <summary>
        /// PCS监控软件代码
        /// </summary>
        public ushort MonitorSoftCode
        {
			get => _monitorSoftCode;
			set
			{
				SetProperty(ref _monitorSoftCode, value);
			}
		}

		private ushort _dcSoftCode;
        /// <summary>
        /// PCS-DC软件代码
        /// </summary>
        public ushort DcSoftCode
        {
			get => _dcSoftCode;
			set
			{
				SetProperty(ref _dcSoftCode, value);
			}
		}

		private ushort _u2SoftCode;
        /// <summary>
        /// U2软件代码
        /// </summary>
        public ushort U2SoftCode
        {
			get => _u2SoftCode;
			set
			{
				SetProperty(ref _u2SoftCode, value);
			}
		}

		private int _versionSWBCMU1;
        /// <summary>
        /// BCMU1软件版本号
        /// </summary>
        public int VersionSWBCMU1
        {
			get => _versionSWBCMU1;
			set
			{
				SetProperty(ref _versionSWBCMU1, value);
			}
		}

        private int _versionSWBCMU2;
        /// <summary>
        /// BCMU2软件版本号
        /// </summary>
        public int VersionSWBCMU2
        {
            get => _versionSWBCMU2;
            set
            {
                SetProperty(ref _versionSWBCMU2, value);
            }
        }

        private int _versionSWBCMU3;
        /// <summary>
        /// BCMU3软件版本号
        /// </summary>
        public int VersionSWBCMU3
        {
            get => _versionSWBCMU3;
            set
            {
                SetProperty(ref _versionSWBCMU3, value);
            }
        }

        private int _versionSWBCMU4;
        /// <summary>
        /// BCMU4软件版本号
        /// </summary>
        public int VersionSWBCMU4
        {
            get => _versionSWBCMU4;
            set
            {
                SetProperty(ref _versionSWBCMU4, value);
            }
        }

        private int _versionSWBCMU5;
        /// <summary>
        /// BCMU5软件版本号
        /// </summary>
        public int VersionSWBCMU5
        {
            get => _versionSWBCMU5;
            set
            {
                SetProperty(ref _versionSWBCMU5, value);
            }
        }

        private int _versionSWBCMU6;
        /// <summary>
        /// BCMU6软件版本号
        /// </summary>
        public int VersionSWBCMU6
        {
            get => _versionSWBCMU6;
            set
            {
                SetProperty(ref _versionSWBCMU6, value);
            }
        }

        private int _version_Hardware_BCUM1;
        /// <summary>
        /// BCMU1硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM1
        {
            get => _version_Hardware_BCUM1;
            set
            {
                SetProperty(ref _version_Hardware_BCUM1, value);
            }
        }

        private int _version_Hardware_BCUM2;
        /// <summary>
        /// BCMU2硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM2
        {
            get => _version_Hardware_BCUM2;
            set
            {
                SetProperty(ref _version_Hardware_BCUM2, value);
            }
        }

        private int _version_Hardware_BCUM3;
        /// <summary>
        /// BCMU3硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM3
        {
            get => _version_Hardware_BCUM3;
            set
            {
                SetProperty(ref _version_Hardware_BCUM3, value);
            }
        }

        private int _version_Hardware_BCUM4;
        /// <summary>
        /// BCMU4硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM4
        {
            get => _version_Hardware_BCUM4;
            set
            {
                SetProperty(ref _version_Hardware_BCUM4, value);
            }
        }

        private int _version_Hardware_BCUM5;
        /// <summary>
        /// BCMU5硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM5
        {
            get => _version_Hardware_BCUM5;
            set
            {
                SetProperty(ref _version_Hardware_BCUM5, value);
            }
        }

        private int _version_Hardware_BCUM6;
        /// <summary>
        /// BCMU6硬件版本号
        /// </summary>
        public int Version_Hardware_BCUM6
        {
            get => _version_Hardware_BCUM6;
            set
            {
                SetProperty(ref _version_Hardware_BCUM6, value);
            }
        }

        private string _meterNumber;
        /// <summary>
        /// 电表编号
        /// </summary>
        public string MeterNumber
        {
            get => _meterNumber;
            set
            {
                SetProperty(ref _meterNumber, value);
            }
        }

        #endregion

        public System_DevInfoPageModel(PCSModel pcsmodel,BatteryTotalModel batterytotalmodel, SmartMeterModel smartmetermodel)
        {
            DevInfoDataDistribution(pcsmodel, batterytotalmodel, smartmetermodel);
        }

        public void DevInfoDataDistribution(PCSModel pcsmodel, BatteryTotalModel batterytotalmodel,SmartMeterModel smartmetermodel)
        {
            Version_Software = 1;
            MonitorSoftCode=pcsmodel.MonitorSoftCode;
            DcSoftCode=pcsmodel.DcSoftCode;
            U2SoftCode=pcsmodel.U2SoftCode;
            VersionSWBCMU1 = batterytotalmodel.VersionSWBCMU;
            VersionSWBCMU2 = batterytotalmodel.VersionSWBCMU;
            VersionSWBCMU3 = batterytotalmodel.VersionSWBCMU;
            VersionSWBCMU4 = batterytotalmodel.VersionSWBCMU;
            VersionSWBCMU5 = batterytotalmodel.VersionSWBCMU;
            VersionSWBCMU6 = batterytotalmodel.VersionSWBCMU;
            Version_Hardware_BCUM1 = batterytotalmodel.HWVersionBCMU;
            Version_Hardware_BCUM2 = batterytotalmodel.HWVersionBCMU;
            Version_Hardware_BCUM3 = batterytotalmodel.HWVersionBCMU;
            Version_Hardware_BCUM4 = batterytotalmodel.HWVersionBCMU;
            Version_Hardware_BCUM5 = batterytotalmodel.HWVersionBCMU;
            Version_Hardware_BCUM6 = batterytotalmodel.HWVersionBCMU;
            MeterNumber = smartmetermodel.SmartMeterNumber;

            GetPCSSN(pcsmodel);
        }


        /// <summary>
        /// 获取机柜序列号
        /// </summary>
        public void GetPCSSN(PCSModel model)
        {
            ushort value;
            string serialnumber = "";
            for (int i = 0; i < 11; i++)
            {
                value = model.SNAdress[i];
                byte[] bytes = BitConverter.GetBytes(value);
                char asciichar1 = Convert.ToChar(bytes[0]);
                char asciichar2 = Convert.ToChar(bytes[1]);
                serialnumber = serialnumber.PadRight(1 + 2 * i, asciichar1);
                serialnumber = serialnumber.PadRight(2 + 2 * i, asciichar2);
            }
            CabSerialNumber = serialnumber;
        }


    }
}
