using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class SmartMeterDBModel
    {
        public int Id { get; set; }
        public string SelectedCommPort { get; set; }
        public int SelectedBaudRate { get; set; }
        public int SelectedParity { get; set; }
        public int SelectedStopBits { get; set; }
        public int SelectedDataBits { get; set; }
        public int AcquisitionCycle { get; set; }

        public SmartMeterDBModel() { }
    }
}
