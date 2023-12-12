using EMS.Model;
using EMS.ViewModel;
using log4net;
using OxyPlot;
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

    public static class PcsApi
    {
        /// <summary>
        ///  对PCS发送控制指令，需要包含一定的验证，检查下发指令是否合理，否则报错，该API不能是阻塞函数，需要立刻返回。如遇到异常，需要抛出异常。
        ///  如果下发指令和当前PCS正在执行的指令一直，可以避免重复下发。
        /// </summary>
        /// <returns>指令下发是否成功</returns>
        public static void SendPcsCommand(BessCommand command)
        {
             EnergyManagementSystem.GlobalInstance.PcsManager.PCSModel.SendPcsCommand(command);
        }

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



        public static bool SetPCSHalt()
        {
            return true;
        }
    }
}
