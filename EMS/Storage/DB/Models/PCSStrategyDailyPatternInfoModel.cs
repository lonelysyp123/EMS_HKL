using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class PCSStrategyDailyPatternInfoModel
    {
        [Key]
        public int ID { get; set; }
        public string StrategyName {  get; set; }
        public string Value {  get; set; }
        public DateTime StartTime { get; set; }
    }
}
