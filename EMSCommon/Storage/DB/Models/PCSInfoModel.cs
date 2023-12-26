using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSCommon.Storage.DB.Models
{
    public class PCSInfoModel
    {
        public int ID { get; set; }
        public double DCPower { get; set; }
        public double DCVol { get; set; }
        public double DCCurrent {get; set; }
        public double TotalCharCap { get; set; }
        public double BusVol {  get; set; }
        public double ModuleTemp {  get; set; }
        public double EnvTemp {  get; set; }
        public string PCSState { get; set; }
        public string SideState {  get; set; }
        public DateTime HappenTime { get; set; }
    }
}
