using EMS.Model;
using EMS.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    public class DcAlarmMessage
    {

    }

    public class DcFaultMessage
    {

    }

    public class DcRuningState
    {

    }

    public static class PcsApi
    {
        /// <summary>
        /// 设置对应 pcs对像的动作，动作包括1、开机，2、停机
        /// </summary>
        /// <param name="pcsid">pcs id编号</param>
        /// <param name="actionid">动作编号，pcs对象类定义</param>
        /// <param name="actionParam">动作编号的对应参数，侧支路编号1开始</param>
        /// <returns></returns>
        public static bool PcsSetAction(int pcsId, int actionId, byte[] actionParam)
        {
            ///1、获取到pcs管理对象，pcs管理对象应该是单例的
            ///2、管理对象查询对应pcsid的实例对象
            ///3、如果实例pcs对像所处状态可以下发动作指令,执行动作，并且返回动作执行结果，否则返回动作执行失败
            //var log = LogManager.GetLogger(typeof(PcsApi));
            //log.Debug("log Pcs test");
            return true;
        }

        /// <summary>
        ///  对PCS发送控制指令，需要包含一定的验证，检查下发指令是否合理，否则报错，该API不能是阻塞函数，需要立刻返回。如遇到异常，需要抛出异常。
        ///  如果下发指令和当前PCS正在执行的指令一致，可以避免重复下发。
        /// </summary>
        /// <returns>指令下发是否成功</returns>
        public static bool SendPcsCommand(BessCommand command)
        {

            return true;
        }

        /// <summary>
        ///  对负载放电为正，对电池充电为负
        /// </summary>
        /// <returns>当前PCS的直流总功率</returns>
        public static double GetPcsPower() { return 0; }

        public static List<string> GetPCSFaultInfo() { return null; }

        /// <summary>
        /// 设置pcs 控制参数
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcscp">
        /// </param>
        /// <returns>配置返回结果，成功or失败</returns>
        public static bool PcsSetDcSideControlParams(int pcsId, int dcSideId, PcsDcSideParams dcscp)
        {
            return true;
        }




        /// <summary>
        /// 获取监视器所有数据
        /// </summary>
        /// <returns></returns>
        public static PCSMonitorModel PCSGetMonitorInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel;
        }

        /// <summary>
        /// 获取参数设置界面所有展现在界面上的数据
        /// </summary>
        /// <returns></returns>
        public static PCSParSettingModel PCSGetParSettingInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.ParSettingModel;
        }





        /// <summary>
        /// 获取DC侧支路状态
        /// </summary>
        /// <returns></returns>
        public static string PCSGetDCSideBranchState()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1State1;
        }

        /// <summary>
        /// 获取DC侧支路启停状态
        /// </summary>
        /// <returns></returns>
        public static string PCSGetDCSideBranchOperState()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1State2;
        }


        /// <summary>
        /// 获取指定pcs，指定的侧支路的功率
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId">侧支路的编号</param>
        /// <param name="dcPower">侧支路输出功率</param>
        /// <returns></returns>
        public static double  PcsGetDcSidePower()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1DCPower;
        }

        /// <summary>
        /// 获取侧支路电流
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcCurr">侧支路电流</param>
        /// <returns></returns>
        public static double PcsGetDcSideCurr()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1DCCur;
        }

        /// <summary>
        /// 获取侧支路电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcVolt"></param>
        /// <returns></returns>
        public static double PcsGetDcSideVolt()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1DCVol;
        }

        /// <summary>
        /// 获取总线侧电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="busVolt"></param>
        /// <returns></returns>
        public static double PcsGetBusSideVolt()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1BUSVol;
        }

        /// <summary>
        /// 获取累计充电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accGhgEnergy">累计充电能量 kwh</param>
        /// <returns></returns>
        public static uint PcsGetDcSideAccChgEnergy()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1Char;
        }

        /// <summary>
        ///  获取累计放电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accDsgEnergy">累计放电能量 kwh</param>
        /// <returns></returns>
        public static uint PcsGetDcSideAccDsgEnergy()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.DcBranch1DisChar;
        }


        /// <summary>
        /// 获取DC故障信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> PCSGetDCModuleFaultInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.FaultInfoDC;
        }

        /// <summary>
        /// 获取DC告警信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> PCSGetDCModuleAlarmInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AlarmInfoDC;
        }

        /// <summary>
        /// 获取PDS故障信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> PCSGetPDSFaultInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.FaultInfoPDS;
        }

        /// <summary>
        /// 获取PDS告警信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> PCSGetPDSAlarmInfo()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AlarmInfoPDS;
        }


        /// <summary>
        /// 获取模组温度
        /// </summary>
        /// <returns></returns>
        public static double PCSGetModuleTemper()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.ModuleTemperature;       
        }

        /// <summary>
        /// 获取环境温度
        /// </summary>
        /// <returns></returns>
        public static double PCSGetAmbientTemper()
        {
            return EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.AmbientTemperature;
        }

        /// <summary>
        /// 获取PCS控制状态
        /// </summary>
        /// <returns></returns>
        //public static string PCSGetPCSControlState()
        //{

        //    if (EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.PCSStateColorManControl.Color.Equals((Color)ColorConverter.ConvertFromString("#98FB98")))
        //    {
        //        return "本地手动控制状态";
        //    }
        //    else if (EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.PCSStateColorAutoControl.Color.Equals((Color)ColorConverter.ConvertFromString("#98FB98")))
        //    {
        //        return "本地自动控制状态";
        //    }
        //    else if (EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.MonitorModel.PCSStateColorRemoteControl.Color.Equals((Color)ColorConverter.ConvertFromString("#98FB98")))
        //    {
        //        return "远程控制状态";
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        /// <summary>
        /// 设置PCS充放电
        /// </summary>
        /// <param name="model">充放电模式,要string格式，带双引号</param>
        /// <param name="setvalue">充放电值</param>
        /// <returns></returns>
        public static bool PCSManChar(string model, double setvalue)
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.SetManChar(model, setvalue);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
                return false;
            }
        }

        public static bool PCSConnect()
        {
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSMainViewModel.Connect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        public static bool PCSDisConnect()
        {
            //Buffer,是个缓存，把所有指令封装成一个，下发的时候调用其中的指令。
            try
            {
                EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }


        //public static bool SetPCSHalt()
        //{

        //}


    }
}
