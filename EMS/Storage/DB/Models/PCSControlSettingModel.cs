using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMS.Storage.DB.Models
{
    public class PCSControlSettingModel
    {
        [Key]
        public int id {  get; set; }
        public double BusVolUpSet { get; set; }
        public double BusVolLowSet { get; set; }
        public double BusHighVolSet { get; set; }
        public double BusLowVolSet { get; set; }
        public string DCControlModeSet { get; set; }
        public double DCCurrentSet { get; set; }
        public double DCPowerSet { get; set; }
        public double DCBatVolLowSet { get; set; }
        public double DCFinalDisVolSet { get; set; }
        public int DCCurrentAdjustSet { get; set; }
        public double DCAvgBatCharVolSet { get; set; }
        public double DCFinalCharCurSet { get; set; }
        public double DCMaxCharCurSet { get; set; }
        public double DCMaxDischarCurSet { get; set; }



    }
}
