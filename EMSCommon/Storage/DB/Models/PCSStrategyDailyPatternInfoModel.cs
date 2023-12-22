using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSCommon.Storage.DB.Models
{
    public class PCSStrategyDailyPatternInfoModel
    {
        [Key]
        public int ID { get; set; }
     
        public int Series {  get; set; }
       
        public int Number { get; set; }
        public string StrategyName {  get; set; }
        public double Value {  get; set; }
        public string StartTime { get; set; }
    }
}
