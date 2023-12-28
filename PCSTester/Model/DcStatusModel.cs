using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSTester.Model
{
    public class DcStatusModel : ObservableObject
    {
        /// <summary>
        /// 模块在线地址状态读取
        /// </summary>
        private int _moduleOnLineFlag;

        public int ModuleOnLineFlag
        {
            get => _moduleOnLineFlag;
            set
            {
                SetProperty(ref _moduleOnLineFlag, value);
            }
        }

        /// <summary>
        /// 模块运行地址状态读取
        /// </summary>
        private int _moduleRunFlag;

        public int ModuleRunFlag
        {
            get => _moduleRunFlag;
            set
            {
                SetProperty(ref _moduleRunFlag, value);
            }
        }

        /// <summary>
        /// 模块告警地址状态读取
        /// </summary>
        private int _moduleAlarmFlag;

        public int ModuleAlarmFlag
        {
            get => _moduleAlarmFlag;
            set
            {
                SetProperty(ref _moduleAlarmFlag, value);
            }
        }

        /// <summary>
        /// 模块故障地址状态读取
        /// </summary>
        private int _moduleFaultFlag;

        public int ModuleFaultFlag
        {
            get => _moduleFaultFlag;
            set
            {
                SetProperty(ref _moduleFaultFlag, value);
            }
        }
    }
}
