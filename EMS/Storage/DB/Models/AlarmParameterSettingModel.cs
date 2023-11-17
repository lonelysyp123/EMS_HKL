using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MQTTnet.Packets;
using System.Security.Policy;

namespace EMS.Storage.DB.Models
{
    public class AlarmParameterSettingModel
    {
        [Key]
        public int id { get; set; }
        public int BCMUID { get; set; }
        public double ClusterVolUp1 {  get; set; }
        public double ClusterVolUp2 { get; set; }
        public double ClusterVolUp3 { get; set; }
        public double ClusterVolLow1 {  get; set; }
        public double ClusterVolLow2 { get;set; }
        public double ClusterVolLow3 { get;set; }
        public double SingleVolUp1 { get; set; }
        public double SingleVolUp2 { get; set; }
        public double SingleVolUp3 { get; set; }
        public double SingleVolLow1 { get;set; }
        public double SingleVolLow2 { get; set; }
        public double SingleVolLow3 { get; set ; }
        public double TempCharUp1 {  get; set; }
        public double TempCharUp2 { get;set ; }
        public double TempCharUp3 { get; set; }
        public double TempCharLow1 { get; set; }
        public double TempCharLow2 { get; set; }
        public double TempCharLow3 { get; set; }
        public double TempDischarUp1 { get; set; }
        public double TempDischarUp2 { get; set; }
        public double TempDischarUp3 { get; set; }
        public double CurChar1 {  get; set; }
        public double CurChar2 {  get; set; }
        public double CurChar3 {  get; set; }
        public double CurDischar1 {  get; set; }
        public double CurDischar2 { get; set; }
        public double CurDischar3 { get;set; }
        public double DiffVol1 {  get; set; }
        public double DiffVol2 { get; set;}
        public double DiffVol3 { get; set;}
        public double SOCLow1 {  get; set; }
        public double SOCLow2 { get; set;}
        public double SOCLow3 { get; set;}
        public double IsoRLow {  get; set;} 
        public DateTime HappenTime { get; set; }




    }
}
