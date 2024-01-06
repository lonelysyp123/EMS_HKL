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
        public double ClusterVolUpLimitLv1 { get; set; }
        public double ClusterVolUpLimitLv2 { get; set; }
        public double ClusterVolUpLimitLv3 { get; set; }
        public double ClusterVolLowLimitLv1 { get; set; }
        public double ClusterVolLowLimitLv2 { get; set; }
        public double ClusterVolLowLimitLv3 { get; set; }
        public double SingleVolUpLimitLv1 { get; set; }
        public double SingleVolUpLimitLv2 { get; set; }
        public double SingleVolUpLimitLv3 { get; set; }
        public double SingleVolLowLimitLv1 { get; set; }
        public double SingleVolLowLimitLv2 { get; set; }
        public double SingleVolLowLimitLv3 { get; set; }
        public double TempCharUpLimitLv1 { get; set; }
        public double TempCharUpLimitLv2 { get; set; }
        public double TempCharUpLimitLv3 { get; set; }
        public double TempCharLowLimitLv1 { get; set; }
        public double TempCharLowLimitLv2 { get; set; }
        public double TempCharLowLimitLv3 { get; set; }
        public double TempDischarUpLimitLv1 { get; set; }
        public double TempDischarUpLimitLv2 { get; set; }
        public double TempDischarUpLimitLv3 { get; set; }
        public double CurCharLv1 { get; set; }
        public double CurCharLv2 { get; set; }
        public double CurCharLv3 { get; set; }
        public double CurDischarLv1 { get; set; }
        public double CurDischarLv2 { get; set; }
        public double CurDischarLv3 { get; set; }
        public double SingleVolDiffLv1 { get; set; }
        public double SingleVolDiffLv2 { get; set; }
        public double SingleVolDiffLv3 { get; set; }
        public double SOCLowLimitLv1 { get; set; }
        public double SOCLowLimitLv2 { get; set; }
        public double SOCLowLimitLv3 { get; set; }
        public double IsoRLowLimitLv1 { get; set; }

        public BMSParameterSettingModel() { }
    }
}
