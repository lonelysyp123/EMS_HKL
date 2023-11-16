using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMS.Storage.DB.Models
{
    public class AlarmandFaultInfoModel
    {
        [Key]
        public int number { get; set; }
        public string Device {get; set;}
        public int id { get; set; }
        public string Module {get; set; }
        public string Type{ get; set;}
        public int level { get; set; }
        public DateTime HappenTime { get; set; }
        

        

    }
}
