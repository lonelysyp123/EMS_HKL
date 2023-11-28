using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

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
        public static bool PcsSetAction(int pcsId,int actionId, byte[] actionParam)
        {
            ///1、获取到pcs管理对象，pcs管理对象应该是单例的
            ///2、管理对象查询对应pcsid的实例对象
            ///3、如果实例pcs对像所处状态可以下发动作指令,执行动作，并且返回动作执行结果，否则返回动作执行失败

            return true;
        }
        /// <summary>
        /// 设置对应的Pcs到当前时间
        /// </summary>
        /// <param name="pcsid"></param>
        /// <returns></returns>
        public static bool PcsSetDataTime2Now(int pcsId)
        {
            return true;
        }

        /// <summary>
        /// 设置通信超时时间
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="devType">1-bms 2-pcs  3-remote tcp</param>
        /// <param name="timeoutMs"></param>
        /// <returns></returns>
        public static bool PcsSetCommunicateTimeout(int pcsId,int devType,int timeoutMs)
        {
            return true;

        }

        /// <summary>
        /// 设置PCS Bus 电压输出高低阈值
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="hthreshold">bus 侧上限电压阈值</param>
        /// <param name="lthreshold">bus 侧下限限电压阈值</param>
        /// <returns></returns>
        public static bool PcsSetBusThreshHold(int pcsId,float hthreshold,float lthreshold)
        {
            return true;
        }

        /// <summary>
        /// 设置PCS BUS 实时输出电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="busVolt"></param>
        /// <returns></returns>
        public static bool PcsSetBusVolt(int pcsId,float busVolt)
        {
            return true;
        }

        /// <summary>
        /// 设置dc side工作模式
        /// </summary>
        /// <param name="pcsId">pcs id</param>
        /// <param name="dcSideId">dc 侧支路 id</param>
        /// <param name="dcMode">dc 侧支路工作模式，1-恒流模式，2-恒功率模式</param>
        /// <returns></returns>
        public static bool PcsSetDcSideMode(int pcsId,int dcSideId,int dcMode)
        {
            return true;
        }

        //public static bool PcsSetDcSideCCParams(int pcsId,int dcSideId,float curr,)
        //{
        //    return true;
        //}

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
        /// 获取DC模组故障信息
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="DcFaultMessage"></param>
        /// <returns></returns>
        public static bool PcsGetDcMessage(int pcsId,out DcFaultMessage dcfm)
        {
            dcfm = null;
            return true;
        }

        /// <summary>
        /// 获取DC模组运行状态
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dconstate"></param>
        /// <returns>1-成功返回 0-返回失败</returns>
        public static bool PcsGetDcModuleRuningState(int pcsId,out DcRuningState dcrunstate)
        {
            dcrunstate = null;
            return true;
        }

        /// <summary>
        /// 获取DC模组在线状态
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dconlinestate"></param>
        /// <returns></returns>
        public static bool PcsGetDcModuleOnlineState(int pcsId, out DcRuningState dconlinestate)
        {
            dconlinestate = null;
            return true;
        }

        /// <summary>
        /// 获取指定pcs，指定的侧支路的功率
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId">侧支路的编号</param>
        /// <param name="dcPower">侧支路输出功率</param>
        /// <returns></returns>
        public static bool PcsGetDcSidePower(int pcsId,int dcSideId,out int dcPower)
        {
            dcPower = 0;
            return true;
        }

        /// <summary>
        /// 获取侧支路电流
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcCurr">侧支路电流</param>
        /// <returns></returns>
        public static bool PcsGetDcSideVolt(int pcsId, int dcSideId, out int dcCurr)
        {
            dcCurr = 0;
            return true;
        }

        /// <summary>
        /// 获取侧支路电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="dcVolt"></param>
        /// <returns></returns>
        public static bool PcsGetDcSideCurr(int pcsId, int dcSideId, out int dcVolt)
        {
            dcVolt = 0;
            return true;
        }
        /// <summary>
        /// 获取总线侧电压
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="busVolt"></param>
        /// <returns></returns>
        public static bool PcsGetBusSideVolt(int pcsId, int dcSideId, out int busVolt)
        {
            busVolt = 0;
            return true;
        }

        /// <summary>
        /// 获取累计充电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accGhgEnergy">累计充电能量 kwh</param>
        /// <returns></returns>
        public static bool PcsGetDcSideAccChgEnergy(int pcsId,int dcSideId,out int accGhgEnergy)
        {
            accGhgEnergy = 0;
            return true;
        }

        /// <summary>
        ///  获取累计放电容量
        /// </summary>
        /// <param name="pcsId"></param>
        /// <param name="dcSideId"></param>
        /// <param name="accDsgEnergy">累计放电能量 kwh</param>
        /// <returns></returns>
        public static bool PcsGetDcSideAccDsgEnergy(int pcsId, int dcSideId, out int accDsgEnergy)
        {
            accDsgEnergy = 0;
            return true;
        }




    }
}
