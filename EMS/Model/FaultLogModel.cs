using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class FaultLogModel
    {
        public string FaultNumber { get; set; }
        public string FaultDevice { get; set; }
        public string FaultId { get; set; }
        public string FaultModule { get; set; }
        public string FaultName { get; set; }
        public string FaultGrade { get; set; }
        public string FaultTime { get; set; }

        public FaultLogModel() { }
    }
}
