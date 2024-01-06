using CommunityToolkit.Mvvm.Input;
using EMS.Common.Modbus.ModbusTCP;
using EMS.Service;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EMS.Model
{
    public class DevControlViewModel : ViewModelBase
    {


        private int _address1;
        /// <summary>
        /// IP地址1：192
        /// </summary>
        public int Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                SetProperty(ref _address1, value);
            }
        }

        private int _address2;
        /// <summary>
        /// IP地址2：168
        /// </summary>
        public int Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                SetProperty(ref _address2, value);
            }
        }

        private int _address3;
        /// <summary>
        /// IP地址3：1
        /// </summary>
        public int Address3
        {
            get
            {
                return _address3;
            }
            set
            {
                SetProperty(ref _address3, value);
            }
        }

        private int _address4;
        /// <summary>
        /// IP地址4：100
        /// </summary>
        public int Address4
        {
            get
            {
                return _address4;
            }
            set
            {
                SetProperty(ref _address4, value);
            }
        }


        private int _mask1;
        /// <summary>
        /// 掩码1：255
        /// </summary>
        public int Mask1
        {
            get
            {
                return _mask1;
            }
            set
            {
                SetProperty(ref _mask1, value);
            }
        }



        private int _mask2;
        /// <summary>
        /// 掩码2：255
        /// </summary>
        public int Mask2
        {
            get
            {
                return _mask2;
            }
            set
            {
                SetProperty(ref _mask2, value);
            }
        }


        private int _mask3;
        /// <summary>
        /// 掩码3：255
        /// </summary>
        public int Mask3
        {
            get
            {
                return _mask3;
            }
            set
            {
                SetProperty(ref _mask3, value);
            }
        }

        private int _mask4;
        /// <summary>
        /// 掩码4：0
        /// </summary>
        public int Mask4
        {
            get
            {
                return _mask4;
            }
            set
            {
                SetProperty(ref _mask4, value);
            }
        }

        private int _gateway1;
        /// <summary>
        /// 网关1：192
        /// </summary>
        public int Gateway1
        {
            get
            {
                return _gateway1;
            }
            set
            {
                SetProperty(ref _gateway1, value);
            }
        }

        private int _gateway2;
        /// <summary>
        /// 网关2：168
        /// </summary>
        public int Gateway2
        {
            get
            {
                return _gateway2;
            }
            set
            {
                SetProperty(ref _gateway2, value);
            }
        }

        private int _gateway3;
        /// <summary>
        /// 网关3：0
        /// </summary>
        public int Gateway3
        {
            get
            {
                return _gateway3;
            }
            set
            {
                SetProperty(ref _gateway3, value);
            }
        }


        private int _gateway4;
        /// <summary>
        /// 网关4：1
        /// </summary>
        public int Gateway4
        {
            get
            {
                return _gateway4;
            }
            set
            {
                SetProperty(ref _gateway4, value);
            }
        }

        /// <summary>
        /// BMU集合
        /// </summary>
        private List<string> _bMUid;
        public List<string> BMUID
        {
            get { return _bMUid; }
            set { SetProperty(ref _bMUid, value); }
        }

        private List<string> _channels;
        /// <summary>
        /// 充电通道集合
        /// </summary>
        public List<string> Channels
        {
            get
            {
                return _channels;
            }
            set
            {
                SetProperty(ref _channels, value);
            }
        }

        private string _selectedChannel;
        /// <summary>
        /// BMU的通道
        /// </summary>
        public string SelectedChannel
        {
            get
            {
                return _selectedChannel;
            }
            set
            {
                SetProperty(ref _selectedChannel, value);
            }
        }

        /// <summary>
        /// 被选择的BMU
        /// </summary>
        private string _selectedBMU;
        public string SelectedBMU
        {
            get
            {
                return _selectedBMU;
            }
            set
            {
                SetProperty(ref _selectedBMU, value);
            }
        }

        /// <summary>
        /// 数据采集模式
        /// </summary>
        private string _selectedDataCollectionMode;
        public string SelectedDataCollectionMode
        {
            get => _selectedDataCollectionMode;
            set
            {
                SetProperty(ref _selectedDataCollectionMode, value);
            }
        }

        private List<string> _dataCollectionMode;
        public List<string> DataCollectionMode
        {
            get => _dataCollectionMode;
            set
            {
                SetProperty(ref _dataCollectionMode, value);
            }
        }

        /// <summary>
        /// 均衡模式选择
        /// </summary>
        private string _selectedBalanceMode;
        public string SelectedBalanceMode
        {
            get => _selectedBalanceMode;
            set
            {
                SetProperty(ref _selectedBalanceMode, value);
            }
        }

        private List<string> _balanceMode;
        public List<string> BalanceMode
        {
            get => _balanceMode;
            set
            {
                SetProperty(ref _balanceMode, value);
            }
        }

        private string _bCMUSName;
        public string BCMUSName
        {
            get => _bCMUSName;
            set
            {
                SetProperty(ref _bCMUSName, value);
            }
        }

        private string _bCMUName;
        public string BCMUName
        {
            get => _bCMUName;
            set
            {
                SetProperty(ref _bCMUName, value);
            }
        }

        public RelayCommand SelectDataCollectionModeCommand { get; set; }
        public RelayCommand ReadNetInfoCommand { get; set; }
        public RelayCommand SyncNetInfoCommand { get; set; }
        public RelayCommand OpenChargeChannelCommand { get; set; }
        public RelayCommand CloseChargeChannelCommand { get; set; }
        public RelayCommand SelectBalancedModeCommand { get; set; }
        public RelayCommand InNetCommand { get; set; }
        public RelayCommand FwUpdateCommand { get; set; }
        public RelayCommand ReadBCMUIDINFOCommand { get; set; }
        public RelayCommand SyncBCMUIDINFOCommand { get; set; }

        private BMSDataService DevService;
        public DevControlViewModel(BMSDataService service)
        {
            ReadNetInfoCommand = new RelayCommand(ReadNetInfo);
            SyncNetInfoCommand = new RelayCommand(SyncNetInfo);
            OpenChargeChannelCommand = new RelayCommand(OpenChargeChannel);
            CloseChargeChannelCommand = new RelayCommand(CloseChargeChannel);
            SelectBalancedModeCommand = new RelayCommand(SelectBalancedMode);
            FwUpdateCommand = new RelayCommand(FwUpdate);
            InNetCommand = new RelayCommand(InNet);
           
            SelectDataCollectionModeCommand = new RelayCommand(SelectDataCollectionMode);   
            DevService = service;
        }

        public void InitBCMUInfo(int channelCounr, int bmuCount)
        {
            Channels = new List<string>();
            for (int i = 1; i <= channelCounr; i++)
            {
                Channels.Add(i.ToString());
            }

            BMUID = new List<string>();
            for (int i = 1;i <= bmuCount; i++)
            {
                BMUID.Add(i.ToString());
            }

            BalanceMode = new List<string>
            {
                "远程模式",
                "自动模式"
            };

            DataCollectionMode = new List<string>
            {
                "正常模式",
                "仿真模式"
            };
        }

        private void InNet()
        {
            DevService.InNet();
        }

        private void SelectBalancedMode()
        {
            DevService.SelectBalancedMode(SelectedBalanceMode);
        }

        private void CloseChargeChannel()
        {
            DevService.CloseChargeChannel(SelectedChannel, SelectedBMU);
        }

        private void OpenChargeChannel()
        {
            DevService.OpenChargeChannel(SelectedChannel, SelectedBMU);
        }

        private void ReadNetInfo()
        {
            int[] data = DevService.ReadNetInfo();
            Address1 = data[0] & 0xFF;//192
            Address2 = (data[0] & 0xFF00) >> 8; //168
            Address3 = data[1] & 0xFF; //0
            Address4 = (data[1] & 0xFF00) >> 8; //102
            Mask1 = data[2] & 0xFF; //255
            Mask2 = (data[2] & 0xFF00) >> 8;//255
            Mask3 = (data[3] & 0xFF);//255
            Mask4 = (data[3] & 0xFF00) >> 8;//0
            Gateway1 = data[4] & 0xFF;//192
            Gateway2 = (data[4] & 0xFF00) >> 8;//168
            Gateway3 = data[5] & 0xFF;//1
            Gateway4 = (data[5] & 0xFF00) >> 8;//1
        }

        private void SyncNetInfo()
        {
            DevService.SyncNetInfo(this);
        }
        private void FwUpdate()
        {
            DevService.FWUpdate();
        }

        private void SelectDataCollectionMode()
        {
            DevService.SelectDataCollectionMode(SelectedDataCollectionMode);
        }

       

      

    }
}
