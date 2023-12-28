using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Concurrent;

namespace PCSTester.ViewModel
{
    
    public class PCSMonitorViewModel:ObservableObject
    {
        /// <summary>
        /// DC模块异常1 DC模块异常有三个地址
        /// </summary>
        private int _alarmStateFlagDC1;

        public int AlarmStateFlagDC1
        {
            get => _alarmStateFlagDC1;
            set
            {
                SetProperty(ref _alarmStateFlagDC1, value);
            }
        }


        /// <summary>
        /// DC模块异常2
        /// </summary>
        private int _alarmStateFlagDC2;

        public int AlarmStateFlagDC2
        {
            get => _alarmStateFlagDC2;
            set
            {
                SetProperty(ref _alarmStateFlagDC2, value);
            }
        }


        /// <summary>
        /// DC模块异常3
        /// </summary>
        private int _alarmStateFlagDC3;

        public int AlarmStateFlagDC3
        {
            get => _alarmStateFlagDC3;
            set
            {
                SetProperty(ref _alarmStateFlagDC3, value);
            }
        }


        /// <summary>
        /// PDS异常信息
        /// </summary>
        private int _alarmStateFlagPDS;

        public int AlarmStateFlagPDS
        {
            get => _alarmStateFlagPDS;
            set
            {
                SetProperty(ref _alarmStateFlagPDS, value);
            }
        }


        /// <summary>
        /// PCS控制状态读取
        /// </summary>
        private int _controlStateFlagPCS;

        public int ControlStateFlagPCS
        {
            get => _controlStateFlagPCS;
            set
            {
                SetProperty(ref _controlStateFlagPCS, value);
            }
        }


        /// <summary>
        /// PCS状态读取
        /// </summary>
        private int _stateFlagPCS;

        public int StateFlagPCS
        {
            get => _stateFlagPCS;
            set
            {
                SetProperty(ref _stateFlagPCS, value);
            }
        }


        /// <summary>
        /// DC侧支路1状态读取1 
        /// </summary>
        private int _dcBranch1StateFlag1;

        public int DcBranch1StateFlag1
        {
            get => _dcBranch1StateFlag1;
            set
            {
                SetProperty(ref _dcBranch1StateFlag1, value);
            }
        }


        /// <summary>
        /// DC侧支路1状态读取2  启停状态
        /// </summary>
        private int _dcBranch1StateFlag2;

        public int DcBranch1StateFlag2
        {
            get => _dcBranch1StateFlag2;
            set
            {
                SetProperty(ref _dcBranch1StateFlag2, value);
            }
        }


        /// <summary>
        /// DC侧支路1：直流累计充电电量高两字节
        /// </summary>
        private ushort _dcBranch1CharHigh;

        public ushort DcBranch1CharHigh
        {
            get => _dcBranch1CharHigh;
            set
            {
                SetProperty(ref _dcBranch1CharHigh, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计充电电量低两字节
        /// </summary>
        private ushort _dcBranch1CharLow;

        public ushort DcBranch1CharLow
        {
            get => _dcBranch1CharLow;
            set
            {
                SetProperty(ref _dcBranch1CharLow, value);
            }
        }



        /// <summary>
        /// DC侧支路1：直流累计放电电量高两字节
        /// </summary>
        private ushort _dcBranch1DisCharHigh;

        public ushort DcBranch1DisCharHigh
        {
            get => _dcBranch1DisCharHigh;
            set
            {
                SetProperty(ref _dcBranch1DisCharHigh, value);
            }
        }

        /// <summary>
        /// DC侧支路1：直流累计放电电量低两字节
        /// </summary>
        private ushort _dcBranch1DisCharLow;

        public ushort DcBranch1DisCharLow
        {
            get => _dcBranch1DisCharLow;
            set
            {
                SetProperty(ref _dcBranch1DisCharLow, value);
            }
        }
    }
}
