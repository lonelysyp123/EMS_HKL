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
        ///  如果下发指令和当前PCS正在执行的指令一直，可以避免重复下发。
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

        public static bool SetPCSHalt() { return true; }

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
            //Buffer,是个缓存，把所有指令封装成一个，下发的时候。
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
    }
}
