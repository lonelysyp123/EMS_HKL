using EMS.Model;
using EMS.Service;
using EMS.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TNCN.EMS.Common.Mqtt;

namespace EMS.Api
{

    public static class BmsApi
    {
        public static BatteryTotalModel GetNextBMSData(string bcmuid)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == bcmuid);
            return item.GetCurrentData();
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
        /// <returns></returns> <comment>返回的（int,bool）为告警等级以及是否含有故障
        public static (int,bool) GetTotalAlarmInfo()
        {
            List<BMSDataService> services = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices;//获取所有电池数据
            List<int>alarmLevel = new List<int>();
            List<bool>faultState = new List<bool>();
            int alarmTotalLevel = 0;
            bool faultTotalState = false;
            Dictionary<int,List<int>>TotalFaultInfo = new Dictionary<int, List<int>> ();
           
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].IsConnected)
                {
                    var item = services[i].GetCurrentData();
                    int alarmflag1 = item.AlarmStateBCMUFlag1;
                    int alarmFlag2 = item.AlarmStateBCMUFlag2;
                    int alarmFlag3 = item.AlarmStateBCMUFlag3;
                    int faultFlag = item.FaultStateBCMUTotalFlag;
                    if ((faultFlag & 1) == 1)
                    {
                        faultState.Add(true);
                    }
                    else
                    {
                        faultState.Add(false);
                    }

                    int alarmLevel1 = 0;
                    if (((alarmflag1 >> 8) & 0xFF) != 0)
                    {
                        alarmLevel1 = 3;
                    }
                    else
                    {
                        alarmLevel1 = 0;
                    }

                    int alarmLevel2LowerByte = 0; // alarmLevel2的寄存器包含两个Byte，处理方式不同，所以此处分开标识
                    if ((alarmFlag2 & 0xFF) != 0)
                    {
                        alarmLevel2LowerByte = 2;

                    }
                    else
                    {
                        alarmLevel2LowerByte = 0;
                    }
                    int alarmflag2HigherByte = (alarmFlag2 >> 8) & 0xFF;
                    List<int> alarmlevel22List = new List<int>();
                    int alarmLevel22 = 0;
                    for (int j = 8; j < 16; j += 2)
                    {
                        int twoBitValue = (alarmflag2HigherByte >> j) & 0x3;
                        alarmlevel22List.Add(twoBitValue);
                    }
                    alarmLevel22 = alarmlevel22List.Max();
                    int alarmLevel2 = Math.Max(alarmLevel2LowerByte, alarmLevel22);
                    int alarmLevel3 = 0;
                    List<int> alarmlevel3List = new List<int>();
                    for (int j = 0; j < 16; j += 2)
                    {
                        int twoBitValue = (alarmFlag3 >> j) & 0x3;
                        alarmlevel3List.Add(twoBitValue);
                    }
                    alarmLevel3 = alarmlevel3List.Max();
                    alarmLevel2 = Math.Max(alarmLevel2, alarmLevel1);
                    alarmLevel3 = Math.Max(alarmLevel3, alarmLevel2);
                    alarmLevel.Add(alarmLevel3);
                }
            }
              
                
            alarmTotalLevel = alarmLevel.Max();
            if (faultState.Contains(true))
            {
                faultTotalState = true;

            }
            else
            {
                faultTotalState =false;
            }
            return (alarmTotalLevel, faultTotalState);

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

        public static void SendBCMUBalanceMode(string index, ushort values)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == index);
           item.SendBalanceMode(values);
        }

        public static void SendBalanceChannel(string index, ushort values)
        {
            var item = EnergyManagementSystem.GlobalInstance.BMSManager.BMSDataServices.ToList().Find(x => x.ID == index);

            item.SendBalanceChannel(values);

        }
    }
}
