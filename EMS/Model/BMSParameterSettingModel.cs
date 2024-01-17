using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class BMSParameterSettingModel
    {
        /// <summary>
        ///组端电压上限
        /// </summary>
        public double ClusterVolUpLimitLv1 { get; set; }
        public double ClusterVolUpLimitLv2 {  get; set; }
        public double ClusterVolUpLimitLv3 { get; set; }

        public BMSParameterSettingModel() { }
        /// <summary>
        /// 组端电压下限
        /// </summary>
        public double ClusterVolLowLimitLv1 { get; set; }
        public double ClusterVolLowLimitLv2 { get; set; }
        public double ClusterVolLowLimitLv3 { get; set; }

        /// <summary>
        /// 单体电压上限
        /// </summary>
        public double SingleVolUpLimitLv1 { get; set; }
        public double SingleVolUpLimitLv2 { get;set; }
        public double SingleVolUpLimitLv3 { get; set; }

        /// <summary>
        /// 单体电压下限
        /// </summary>
        public double SingleVolLowLimitLv1 { get; set; }
        public double SingleVolLowLimitLv2 { get; set; }
        public double SingleVolLowLimitLv3 { get; set; }

        /// <summary>
        /// 充电温度上限
        /// </summary>
        public double TempCharUpLimitLv1 { get; set; }
        public double TempCharUpLimitLv2 { get; set; }
        public double TempCharUpLimitLv3 { get; set; }

        /// <summary>
        /// 充电温度下限
        /// </summary>
        public double TempCharLowLimitLv1 { get; set; }
        public double TempCharLowLimitLv2 { get; set; }
        public double TempCharLowLimitLv3 { get; set; }

        /// <summary>
        /// 放电温度上限
        /// </summary>
        public double TempDischarUpLimitLv1 { get; set; }
        public double TempDischarUpLimitLv2 { get; set; }
        public double TempDischarUpLimitLv3 { get; set; }

        /// <summary>
        /// 充电电流
        /// </summary>
        public double CurCharLv1 { get; set; }
        public double CurCharLv2 { get; set; }
        public double CurCharLv3 { get; set; }

        /// <summary>
        /// 放电电流
        /// </summary>
        public double CurDischarLv1 { get; set; }
        public double CurDischarLv2 { get; set; }
        public double CurDischarLv3 { get; set; }

        /// <summary>
        /// 单体压差
        /// </summary>
        public double SingleVolDiffLv1 { get; set; }
        public double SingleVolDiffLv2 { get; set; }
        public double SingleVolDiffLv3 { get; set; }

        /// <summary>
        /// SOC下限
        /// </summary>
        public double SOCLowLimitLv1 { get; set; }
        public double SOCLowLimitLv2 { get; set; }
        public double SOCLowLimitLv3 { get; set; }

        /// <summary>
        /// 绝缘电阻下限
        /// </summary>
        public double IsoRLowLimitLv1 { get; set; }
    }
}
