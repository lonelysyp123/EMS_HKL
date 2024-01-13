using EMS.Model;
using EMS.Service;
using EMS.ViewModel;
using MQTTnet.Diagnostics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.Api
{

    public static class BmsApi
    {
        public static BatteryTotalModel GetNextBMSData(string bcmuid)
        {
            if (EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.Count > 0)
            {
                var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
                if (item != null)
                {
                    return item.GetCurrentData();
                }
            }
            return null;
        }

        

        /// <summary>
        /// 获取BMS保护参数接口
        /// </summary>
        /// <returns></returns>
        public static BMSParameterSettingModel GetBMSParam(string bcmuid)
        {
            return null;
        }

        /// <summary>
        /// 设置BMS保护参数接口
        /// </summary>
        /// <returns></returns>
        public static BMSParameterSettingModel SetBMSParam(string bcmuid)
        {
            return null;
        }

        /// <summary>
        /// 返回当前BMS系统的额定功率(最大充放电功率)，需要根据当前连接的电池簇动态调整
        /// </summary>
        /// <returns></returns>
        public static double GetNormalPowerCapacity() { return 0; }

        /// <summary>
        /// 将bcmuid对应的那簇电池簇并网到DC侧母线，需要做一定的安全性检查，比如电压差符合并网要求，电池状态不能是异常
        /// </summary>
        /// <returns></returns>
        public static void Connect2DcBus(string bcmuid) 
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
            item.OnGrid();
        }

        public static void Disconnect2DcBus(string bcmuid)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
            item.OffGrid();
        }

        public static void ResetBMSFault(string bcmuid)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
            item.ResetFault();
        }

        public static byte[] GetBMSProtectSet(string bcmuid)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
            byte[] data = item.GetProetctSet();
            return data;
        }

        public static BatteryTotalModel[] GetNextBMSData()
        {
            DateTime dateTime = DateTime.Now;
            List<BMSDataService> services = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices;
            List<BatteryTotalModel> models = new List<BatteryTotalModel>();
            for (int i = 0; i < services.Count; i++)
            {
                var item = services[i].GetCurrentData();
                if (item != null)
                {
                    item.CurrentTime = dateTime;
                    models.Add(item);
                }
            }
            return models.ToArray();
        }

        public static BatteryTotalModel GetBMSTotalInfo(string bcmuid)
        {// 这个函数如果经常被调用，可以考虑重构成Dictionary
            List<BMSDataService> services = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices;
            
            if(services != null && services.Count > 0)
            {
                foreach (var service in services)
                {
                    if (service.ID == bcmuid)
                    {
                        return service.GetCurrentData();

                    }

                }
            }
            return null;
        }

        /// <summary>
        /// 得到BMS所有告警故障信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTotalAlarmInfo()
        {
            List<BMSDataService> services = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices;//获取所有电池数据
            List<string>totalalarminfo = new List<string>();
            foreach(var service in services)
            {
                List<string>bcmualarm = new List<string>();
                List<string>bcmufault = new List<string>();

                bcmualarm = service.GetCurrentData().AlarmStateBCMU.ToList();
                bcmufault = service.GetCurrentData().FaultyStateBCMU.ToList();
                totalalarminfo.AddRange(bcmualarm);
                totalalarminfo.AddRange((bcmufault));//BCMU数据得到
                for (int i = 0;i< service.GetCurrentData().Series.Count;i++)
                {
                    List<string> bmufault = new List<string>();
                    bmufault = GetActiveFaultyBMU(service.GetCurrentData().Series[i].AlarmStateFlagBMU);
                    totalalarminfo.AddRange(bmufault);//BMU数据得到
                }
                
            }
            
            totalalarminfo=totalalarminfo.Distinct().ToList();
            return totalalarminfo;
        }

        private static List<string> GetActiveFaultyBMU(int flag)
        {
            int Value = flag;
            List<string> INFO = new List<string>();
            if ((Value & 0x0001) != 0) { INFO.Add("电压传感器异常");  } //bit0
            if ((Value & 0x0002) != 0) { INFO.Add("温度传感器异常");  }  //bit1
            if ((Value & 0x0004) != 0) { INFO.Add("内部通讯故障");  }  //bit2
            if ((Value & 0x0008) != 0) { INFO.Add("输入过压故障");  }  //bit3
            if ((Value & 0x0010) != 0) { INFO.Add("输入反接故障");  }  //bit4
            if ((Value & 0x0020) != 0) { INFO.Add("继电器故障");  } //bit5
            if ((Value & 0x0040) != 0) { INFO.Add("电池损坏故障");  } //bit6
            if ((Value & 0x0080) != 0) { INFO.Add("关机电路异常");  } //bit7
            if ((Value & 0x0100) != 0) { INFO.Add("BMIC异常");  } //bit8
            if ((Value & 0x0200) != 0) { INFO.Add("内部总线异常");  } //bit9
            if ((Value & 0x0400) != 0) { INFO.Add("开机自检异常");  } //bit10
            return INFO;
        }

        /// <summary>
        /// 所有SOC、最大最小平均SOC
        /// </summary>
        /// <returns></returns>
        public static List<double> GetTotalSOC()
        {
            List<BMSDataService> services = new List<BMSDataService>();
            services = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices;
            List<double> SOCTotalList = new List<double>();
            foreach (var total in services)
            {
                SOCTotalList.Add(total.GetCurrentData().TotalSOC);
            }
            return SOCTotalList;
        }

        public static double GetMinSOC()
        {
            List<double> sOCTotalList = GetTotalSOC();
            return sOCTotalList.Min();
        }

        public static double GetMaxSOC()
        {      
            List<double> sOCTotalList = GetTotalSOC();
            return sOCTotalList.Max();
        }

        public static double GetAvgSOC()
        {
            List<double> sOCTotalList = GetTotalSOC();
            return sOCTotalList.Average();
        }
        public static void SyncBCMUInfo1(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo1(values);
        }
        public static void SyncBCMUInfo2(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo2(values);
        }
        public static void SyncBCMUInfo3(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo3(values);
        }
        public static void SyncBCMUInfo4(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo4(values);
        }
        public static void SyncBCMUInfo5(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo5(values);
        }
        public static void SyncBCMUInfo6(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo6(values);
        }
        public static void SyncBCMUInfo7(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo7(values);
        }
        public static void SyncBCMUInfo8(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo8(values);
        }
        public static void SyncBCMUInfo9(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo9(values);
        }
        public static void SyncBCMUInfo10(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo10(values);
        }
        public static void SyncBCMUInfo11(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo11(values);
        }
        public static void SyncBCMUInfo12(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo12(values);
        }
        public static void SyncBCMUInfo13(int index, ushort[] values)
        {
            EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices[index].SyncBCMUInfo13(values);
        }
    }
}
