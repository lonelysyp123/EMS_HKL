using EMS.Model;
using EMS.ViewModel;
using log4net;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace EMS.Api
{
    public class PcsDcSideParams
    {
        public int dcsidemode;             //dc侧控制模式 0-恒流  1-恒功率
        public int dcsidelowbatvoltthreshold;//电池放电下限电压阈值
        public int dcsideendofdisvolt;     //放电终止电压
        public int dcsidecurrregparam;     //多支路电流调节参数
        public int topingchgvolt;          //电池均充电压
        public int endofchgcurr;           //电池充电截止电流
        public int maxchgcurr;             //最大充电电流
        public int mindsgcurr;             //最大放电电流
    }

    public static class PcsApi
    {
        /// <summary>
        /// 建议实现方案：
        /// 1、将采集的PCS数据放到一个线程安全固定长度的队列中。可用本项目的ConcurrentQueueLength
        /// 2、该方法是从队列中取出一个采集的对象数据
        /// 获取采集的PCS数据
        /// </summary>
        /// <returns></returns>
        public static PCSModel GetNextPCSData()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.GetCurrentData();
        }
            /// <summary>
            ///  对PCS发送控制指令，需要包含一定的验证，检查下发指令是否合理，否则报错，该API不能是阻塞函数，需要立刻返回。如遇到异常，需要抛出异常。
            ///  如果下发指令和当前PCS正在执行的指令一致，可以避免重复下发。
            /// </summary>
            /// <returns>指令下发是否成功</returns>
        public static void SendPcsCommand(BessCommand command)
        {
             EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.SendPcsCommand(command);
        }

        /// <summary>
        /// 获取PCS保护参数接口
        /// </summary>
        /// <returns></returns>
        public static PCSParSettingModel GetBUSParam()
        {
            //需实现获取BUS相关逻辑
            return null;
        }
        public static PCSParSettingModel SetBUSParam()
        {
            //需实现设置BUS相关逻辑
            return null;
        }
        public static PCSParSettingModel GetDCBranchParam()
        {
            //需实现获取DCBranch相关逻辑
            return null;
        }
        public static PCSParSettingModel SetDCBranchParam()
        {
            //需实现设置DCBranch相关逻辑
            return null;
        }

        /// <summary>
        ///  返回PCS当前是否处于正常运行状态，如果PCS处于故障状态，返回false，如果PCS没有故障但是有告警返回true
        /// </summary>
        /// <returns>指令下发是否成功</returns>
        public static bool IsPcsNormal() 
        {
            PCSModel pcsmodel = EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.GetCurrentData();
            if (GetPCSFault(pcsmodel).Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<string> GetPCSFaultInfo() 
        {
            PCSModel pcsmodel = EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.GetCurrentData();
            return GetPCSAllFault(pcsmodel);
        }

        /// <summary>
        /// 读取保护参数界面BUSVol参数
        /// </summary>
        /// <returns>返回读取的字节数组</returns>
        public static byte[] ReadPCSBUSVolPar()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.ReadBUSVolInfo();
        }

        /// <summary>
        /// 读取保护参数界面DCBranch1参数
        /// </summary>
        /// <returns>返回读取的字节数组</returns>
        public static byte[] ReadPCSDCBranch1Par()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.ReadDCBranchInfo();
        }

        public static  bool  SyncPCSBUSVolPar(double[] busvolvalues)
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.SyncBUSVolInfo(busvolvalues);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public static bool SyncPCSDCBranch1Par(double[] dcbranch1values)
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.SyncDCBranchInfo(dcbranch1values);
                return true;
            }
            catch(Exception ex)
            {
                return false; 
                throw ex;
            }
        }



        public static bool SetPCSHalt()
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.PCSClose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            } 
        }
        /// <summary>
        /// 获取监视器所有数据
        /// </summary>
        /// <returns></returns>
        //public static PCSMonitorModel PCSGetMonitorInfo()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.MonitorModel;
        //}

        public static bool SetPCSStart()
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.PCSOpen();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }
        /// <summary>
        /// 获取参数设置界面所有展现在界面上的数据
        /// </summary>
        /// <returns></returns>
        //    public static PCSParSettingModel PCSGetParSettingInfo()
        //        {
        //            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.ParSettingModel;
        //}

        /// <summary>
        /// 获取DC侧支路状态
        /// </summary>
        /// <returns></returns>
        //public static string PCSGetDCSideBranchState()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.MonitorModel.DcBranch1State1;
        //}

        /// <summary>
        /// 获取DC侧支路启停状态
        /// </summary>
        /// <returns></returns>
        //public static string PCSGetDCSideBranchOperationState()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.MonitorModel.DcBranch1State2;
        //}


        /// <summary>
        /// 获取指定pcs，指定的侧支路的功率
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId">侧支路的编号</param>
        /// <param name="dcPower">侧支路输出功率</param>
        /// <returns></returns>
        public static double PcsGetDcSidePower()
        {
            double at=0;
            return at;
            //return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.MonitorModel.DcBranch1DCPower;
        }

        public static bool SetPCSSystemClearFault()
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.PCSSystemClearFault();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }
        /// <summary>
        /// 获取侧支路电流
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcCurr">侧支路电流</param>
        /// <returns></returns>
        //public static double PcsGetDcSideCurrent()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.MonitorModel.DcBranch1DCCur;
        //}

        /// <summary>
        /// 获取侧支路电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcVolt"></param>
        /// <returns></returns>
        //public static double PcsGetDcSideVoltage()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService. DcBranch1DCVol;
        //}

        /// <summary>
        /// 获取总线侧电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="busVolt"></param>
        /// <returns></returns>
        //public static double PcsGetBusSideVoltage()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1BUSVol;
        //}

        /// <summary>
        /// 获取累计充电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accGhgEnergy">累计充电能量 kwh</param>
        /// <returns></returns>
        //public static uint PcsGetDcSideAccumulatedChargedEnergy()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1Char;
        //}

        /// <summary>
        ///  获取累计放电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accDsgEnergy">累计放电能量 kwh</param>
        /// <returns></returns>
        //public static uint PcsGetDcSideAccumulatedDischargedEnergy()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1DisChar;
        //}


        /// <summary>
        /// 获取DC故障信息
        /// </summary>
        /// <returns></returns>
        //public static ObservableCollection<string> PCSGetDCModuleFaultInfo()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.FaultInfoDC;
        //}

        /// <summary>
        /// 获取DC告警信息
        /// </summary>
        /// <returns></returns>
        //public static ObservableCollection<string> PCSGetDCModuleAlarmInfo()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AlarmInfoDC;
        //}

        /// <summary>
        /// 获取PDS故障信息
        /// </summary>
        /// <returns></returns>
        //public static ObservableCollection<string> PCSGetPDSFaultInfo()
        //{
        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.FaultInfoPDS;
        //}

        /// <summary>
        /// 获取PDS告警信息
        /// </summary>
        /// <returns></returns>
        //public static ObservableCollection<string> PCSGetPDSAlarmInfo()
        //{

        //    return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AlarmInfoPDS;
        //}


        /// <summary>
        /// 获取模组温度
        /// </summary>
        /// <returns></returns>
        public static double PCSGetModuleTemperature()
        {
            double at = 0;
            return at;
            //return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.ModuleTemperature;       
        }

        /// <summary>
        /// 获取环境温度
        /// </summary>
        /// <returns></returns>
        public static double PCSGetAmbientTemperature()
        {
            double at=0;
            return at;
            //return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AmbientTemperature;
        }

        private static List<string> GetPCSFault(PCSModel model)
        {
            int value1;
            int value2;
            int value3;
            int value4;
            List<string> INFO = new List<string>();
            value1 = model.AlarmStateFlagDC1;
            value2 = model.AlarmStateFlagDC2;
            value3 = model.AlarmStateFlagDC3;
            value4 = model.AlarmStateFlagPDS;
            //DC故障
            if ((value1 & 0x0001) != 0) { INFO.Add("直流高压侧过压"); } //53005 bit0
            if ((value1 & 0x0002) != 0) { INFO.Add("直流高压侧欠压"); }  //bit1`
            if ((value1 & 0x0004) != 0) { INFO.Add("直流低压侧过压"); }  //bit2
            if ((value1 & 0x0008) != 0) { INFO.Add("直流低压侧欠压"); }  //bit3
            if ((value1 & 0x0010) != 0) { INFO.Add("直流低压侧过流"); }  //bit4
            //if ((value1 & 0x0020) != 0) { INFO.Add("重启过多"); colorflag = true; } //bit5
            if ((value1 & 0x0040) != 0) { INFO.Add("重启过多"); } //bit6
            if ((value1 & 0x0080) != 0) { INFO.Add("直流低压侧继电器短路"); } //bit7
            //if ((value1 & 0x0100) != 0) { INFO.Add("光伏能量不足"); colorflag = true; } //bit8
            if ((value1 & 0x0200) != 0) { INFO.Add("电池电量不足"); } //bit9
            if ((value1 & 0x0800) != 0) { INFO.Add("直流高压侧开关断开"); } //bit11
            if ((value1 & 0x2000) != 0) { INFO.Add("机柜温度过高");} //bit13

            if ((value2 & 0x0001) != 0) { INFO.Add("模块电流不平衡"); } //53007 bit0
            if ((value2 & 0x0002) != 0) { INFO.Add("直流低压侧开关断开"); } //bit1
            if ((value2 & 0x0004) != 0) { INFO.Add("24V辅助电源故障"); } //bit2
            if ((value2 & 0x0008) != 0) { INFO.Add("紧急停机"); } //bit3
            //if ((value2 & 0x0010) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit4
            //if ((value2 & 0x0020) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit5
            if ((value2 & 0x0040) != 0) { INFO.Add("模块温度过温"); } //bit6
            if ((value2 & 0x0080) != 0) { INFO.Add("风扇故障"); } //bit7
            if ((value2 & 0x0100) != 0) { INFO.Add("直流低压侧继电器开路"); } //bit8
            if ((value2 & 0x0400) != 0) { INFO.Add("保险故障"); } //bit10
            if ((value2 & 0x0800) != 0) { INFO.Add("DSP初始化故障"); } //bit11
            if ((value2 & 0x1000) != 0) { INFO.Add("直流低压侧软启动失败"); } //bit12
            if ((value2 & 0x2000) != 0) { INFO.Add("CANA通讯故障"); } //bit13
            if ((value2 & 0x4000) != 0) { INFO.Add("直流高压侧继电器开路"); } //bit14
            if ((value2 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); } //bit15

            if ((value3 & 0x0001) != 0) { INFO.Add("DSP版本故障"); } //53008 bit0
            if ((value3 & 0x0002) != 0) { INFO.Add("CPLD版本故障"); } //bit1
            if ((value3 & 0x0004) != 0) { INFO.Add("参数不匹配"); } //bit2
            if ((value3 & 0x0008) != 0) { INFO.Add("硬件版本故障"); } //bit3
            if ((value3 & 0x0010) != 0) { INFO.Add("485通讯故障"); } //bit4
            if ((value3 & 0x0020) != 0) { INFO.Add("CANB通讯故障"); } //bit5
            if ((value3 & 0x0040) != 0) { INFO.Add("模块重号故障"); } //bit6
            //if ((value3 & 0x0080) != 0) { INFO.Add("风扇故障"); colorflag = true; } //bit7
            if ((value3 & 0x0100) != 0) { INFO.Add("15V辅助电源故障"); } //bit8
            if ((value3 & 0x0200) != 0) { INFO.Add("直流高压侧继电器短路"); } //bit9
            if ((value3 & 0x0400) != 0) { INFO.Add("BMS电压异常"); } //bit10
            if ((value3 & 0x0800) != 0) { INFO.Add("BMS电流异常"); } //bit11
            if ((value3 & 0x1000) != 0) { INFO.Add("BMS温度异常"); } //bit12
            if ((value3 & 0x2000) != 0) { INFO.Add("BMS关机异常"); } //bit13
            if ((value3 & 0x4000) != 0) { INFO.Add("绝缘检测异常"); } //bit14
            //if ((value3 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); colorflag = true; } //bit15
            
            //PDS故障
            if ((value4 & 0x0001) != 0) { INFO.Add("软件版本故障"); } //53009 bit0
            if ((value4 & 0x0002) != 0) { INFO.Add("DSP初始化故障"); } //bit1
            if ((value4 & 0x0004) != 0) { INFO.Add("BMS故障"); } //bit2
            if ((value4 & 0x0008) != 0) { INFO.Add("紧急停机"); } //bit3

            ////DC告警
            //if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高"); } //bit10  AAAA
            //if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); } //bit12  AAAAA
            //if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); } //bit14  AAAAAA
            //if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); } //bit15  AAAAAA

            //if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); } //bit9   AAAAAA

            ////PDS告警
            //if ((value4 & 0x0010) != 0) { INFO.Add("防雷器告警"); } //bit4   AAAAAAAAA
            return INFO;
        }

        private static List<string> GetPCSAllFault(PCSModel model)
        {
            int value1;
            int value2;
            int value3;
            int value4;
            List<string> INFO = new List<string>();
            value1 = model.AlarmStateFlagDC1;
            value2 = model.AlarmStateFlagDC2;
            value3 = model.AlarmStateFlagDC3;
            value4 = model.AlarmStateFlagPDS;
            //DC故障
            if ((value1 & 0x0001) != 0) { INFO.Add("直流高压侧过压"); } //53005 bit0
            if ((value1 & 0x0002) != 0) { INFO.Add("直流高压侧欠压"); }  //bit1`
            if ((value1 & 0x0004) != 0) { INFO.Add("直流低压侧过压"); }  //bit2
            if ((value1 & 0x0008) != 0) { INFO.Add("直流低压侧欠压"); }  //bit3
            if ((value1 & 0x0010) != 0) { INFO.Add("直流低压侧过流"); }  //bit4
            //if ((value1 & 0x0020) != 0) { INFO.Add("重启过多"); colorflag = true; } //bit5
            if ((value1 & 0x0040) != 0) { INFO.Add("重启过多"); } //bit6
            if ((value1 & 0x0080) != 0) { INFO.Add("直流低压侧继电器短路"); } //bit7
            //if ((value1 & 0x0100) != 0) { INFO.Add("光伏能量不足"); colorflag = true; } //bit8
            if ((value1 & 0x0200) != 0) { INFO.Add("电池电量不足"); } //bit9
            if ((value1 & 0x0800) != 0) { INFO.Add("直流高压侧开关断开"); } //bit11
            if ((value1 & 0x2000) != 0) { INFO.Add("机柜温度过高"); } //bit13

            if ((value2 & 0x0001) != 0) { INFO.Add("模块电流不平衡"); } //53007 bit0
            if ((value2 & 0x0002) != 0) { INFO.Add("直流低压侧开关断开"); } //bit1
            if ((value2 & 0x0004) != 0) { INFO.Add("24V辅助电源故障"); } //bit2
            if ((value2 & 0x0008) != 0) { INFO.Add("紧急停机"); } //bit3
            //if ((value2 & 0x0010) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit4
            //if ((value2 & 0x0020) != 0) { INFO.Add("环温探头故障"); colorflag = true; } //bit5
            if ((value2 & 0x0040) != 0) { INFO.Add("模块温度过温"); } //bit6
            if ((value2 & 0x0080) != 0) { INFO.Add("风扇故障"); } //bit7
            if ((value2 & 0x0100) != 0) { INFO.Add("直流低压侧继电器开路"); } //bit8
            if ((value2 & 0x0400) != 0) { INFO.Add("保险故障"); } //bit10
            if ((value2 & 0x0800) != 0) { INFO.Add("DSP初始化故障"); } //bit11
            if ((value2 & 0x1000) != 0) { INFO.Add("直流低压侧软启动失败"); } //bit12
            if ((value2 & 0x2000) != 0) { INFO.Add("CANA通讯故障"); } //bit13
            if ((value2 & 0x4000) != 0) { INFO.Add("直流高压侧继电器开路"); } //bit14
            if ((value2 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); } //bit15

            if ((value3 & 0x0001) != 0) { INFO.Add("DSP版本故障"); } //53008 bit0
            if ((value3 & 0x0002) != 0) { INFO.Add("CPLD版本故障"); } //bit1
            if ((value3 & 0x0004) != 0) { INFO.Add("参数不匹配"); } //bit2
            if ((value3 & 0x0008) != 0) { INFO.Add("硬件版本故障"); } //bit3
            if ((value3 & 0x0010) != 0) { INFO.Add("485通讯故障"); } //bit4
            if ((value3 & 0x0020) != 0) { INFO.Add("CANB通讯故障"); } //bit5
            if ((value3 & 0x0040) != 0) { INFO.Add("模块重号故障"); } //bit6
            //if ((value3 & 0x0080) != 0) { INFO.Add("风扇故障"); colorflag = true; } //bit7
            if ((value3 & 0x0100) != 0) { INFO.Add("15V辅助电源故障"); } //bit8
            if ((value3 & 0x0200) != 0) { INFO.Add("直流高压侧继电器短路"); } //bit9
            if ((value3 & 0x0400) != 0) { INFO.Add("BMS电压异常"); } //bit10
            if ((value3 & 0x0800) != 0) { INFO.Add("BMS电流异常"); } //bit11
            if ((value3 & 0x1000) != 0) { INFO.Add("BMS温度异常"); } //bit12
            if ((value3 & 0x2000) != 0) { INFO.Add("BMS关机异常"); } //bit13
            if ((value3 & 0x4000) != 0) { INFO.Add("绝缘检测异常"); } //bit14
                                                                //if ((value3 & 0x8000) != 0) { INFO.Add("直流高压侧软启动失败"); colorflag = true; } //bit15

            //PDS故障
            if ((value4 & 0x0001) != 0) { INFO.Add("软件版本故障"); } //53009 bit0
            if ((value4 & 0x0002) != 0) { INFO.Add("DSP初始化故障"); } //bit1
            if ((value4 & 0x0004) != 0) { INFO.Add("BMS故障"); } //bit2
            if ((value4 & 0x0008) != 0) { INFO.Add("紧急停机"); } //bit3

            //DC告警
            if ((value1 & 0x0400) != 0) { INFO.Add("环境温度过高"); } //bit10  AAAA
            if ((value1 & 0x1000) != 0) { INFO.Add("U2通信异常1"); } //bit12  AAAAA
            if ((value1 & 0x4000) != 0) { INFO.Add("柜温探头故障"); } //bit14  AAAAAA
            if ((value1 & 0x8000) != 0) { INFO.Add("环温探头故障"); } //bit15  AAAAAA

            if ((value2 & 0x0200) != 0) { INFO.Add("校准参数异常"); } //bit9   AAAAAA

            //PDS告警
            if ((value4 & 0x0010) != 0) { INFO.Add("防雷器告警"); } //bit4   AAAAAAAAA
            return INFO;
        }

        //public static async Task<bool> PCSConnect()
        //{
        //    try
        //    {
        //        await EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.ConnectAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        throw (ex);
        //    }
        //}

        //public static bool PCSDisConnect()
        //{
        //    //Buffer,是个缓存，把所有指令封装成一个，下发的时候调用其中的指令。
        //    try
        //    {
        //        EnergyManagementSystem.GlobalInstance.PcsManager.PCSDataService.Disconnect();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        throw (ex);
        //    }
        //}


    }
}
