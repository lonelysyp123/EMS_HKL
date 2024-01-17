using EMS.Common.Modbus.ModbusTCP;
using EMS.ViewModel;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace EMS.Model
{
    public class PCSModel : ViewModelBase
    {
        /// <summary>
        /// 模块在线地址状态读取
        /// </summary>
        public int ModuleOnLineFlag { get; set; }
        /// <summary>
        /// 模块运行地址状态读取
        /// </summary>
        public int ModuleRunFlag { get; set; }
        /// <summary>
        /// 模块告警地址状态读取
        /// </summary>
        public int ModuleAlarmFlag { get; set; }
        /// <summary>
        /// 模块故障地址状态读取
        /// </summary>
        public int ModuleFaultFlag { get; set; }


        /// <summary>
        /// DC模块异常读取1 DC模块异常有三个地址
        /// </summary>
        public int AlarmStateFlagDC1 { get; set; }
        /// <summary>
        /// DC模块异常读取2
        /// </summary>
        public int AlarmStateFlagDC2 { get; set; }
        /// <summary>
        /// DC模块异常读取3
        /// </summary>
        public int AlarmStateFlagDC3 { get; set; }
        ///<summary>
        /// PDS异常信息读取
        /// </summary>
        public int AlarmStateFlagPDS { get; set; }
        /// <summary>
        /// PCS控制状态读取
        /// </summary>
        public int ControlStateFlagPCS { get; set; }
        /// <summary>
        /// PCS状态读取
        /// </summary>
        public int StateFlagPCS {  get; set; }
        /// <summary>
        /// DC侧支路1状态读取1 
        /// </summary>
        public int DcBranch1StateFlag1 {  get; set; }
        /// <summary>
        /// DC侧支路1状态读取2  启停状态
        /// </summary>
        public int DcBranch1StateFlag2 {  get; set; }

        /// <summary>
        /// 模块温度  精度0.1 偏移值-20
        /// </summary>
        public double ModuleTemperature { get; set; }
        /// <summary>
        /// 环境温度  精度0.1 偏移值-20
        /// </summary>
        public double AmbientTemperature { get; set; }


        /// <summary>
        /// DC侧支路1：直流功率
        /// </summary>
        public double DcBranch1DCPower { get; set; }
        /// <summary>
        /// DC侧支路1：直流电压
        /// </summary>
        public double DcBranch1DCVol {  get; set; }
        /// <summary>
        /// DC侧支路1：直流电流
        /// </summary>
        public double DcBranch1DCCur { get; set; }
        /// <summary>
        /// DC侧支路1：直流累计充电电量高两字节
        /// </summary>
        public ushort DcBranch1CharHigh { get; set; }
        /// <summary>
        /// DC侧支路1：直流累计充电电量低两字节
        /// </summary>
        public ushort DcBranch1CharLow {  get; set; }
        /// <summary>
        /// DC侧支路1：直流累计放电电量高两字节
        /// </summary>
        public ushort DcBranch1DisCharHigh {  get; set; }
        /// <summary>
        /// DC侧支路1：直流累计放电电量低两字节
        /// </summary>
        public ushort DcBranch1DisCharLow {  get; set; }
        /// <summary>
        /// DC侧支路1：BUS侧电压
        /// </summary>
        public double DcBranch1BUSVol {  get; set; }


        /// <summary>
        /// 机柜序列号地址读取
        /// </summary>
        public ushort[] SNAdress {  get; set; }
        /// <summary>
        /// 监控软件代码
        /// </summary>
        public ushort MonitorSoftCode { get; set; }
        /// <summary>
        /// DC软件代码
        /// </summary>
        public ushort DcSoftCode { get;set; }
        /// <summary>
        /// U2软件代码
        /// </summary>
        public ushort U2SoftCode {  get; set; }

        public DateTime CurrentTime;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}