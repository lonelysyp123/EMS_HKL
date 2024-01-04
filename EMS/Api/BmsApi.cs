﻿using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{

    public static class BmsApi
    {
        public static BatteryTotalModel GetNextBMSData(string bcmuid)
        {
            var item = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList().Find(x => x.TotalID == bcmuid);
            return item.GetNextBMSDataForMqtt();
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
        public static void Connect2DcBus(string bcmuid) { }

        public static BatteryTotalModel[] GetNextBMSData()
        {
            DateTime dateTime = DateTime.Now;
            List<BatteryTotalViewModel> viewmodels = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList();
            List<BatteryTotalModel> models = new List<BatteryTotalModel>();
            for (int i = 0; i < viewmodels.Count; i++)
            {
                var item = viewmodels[i].GetNextBMSDataForMqtt();
                if (item != null)
                {
                    item.CurrentTime = dateTime;
                    models.Add(item);
                }
            }

            return models.ToArray();
        }

        /// <summary>
        /// 得到BMS信息
        /// </summary>
        /// <returns></returns>
        public static List<BatteryTotalViewModel> GetBMSTotalInfo()
        {
            return EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList();
        }

        public static BatteryTotalViewModel GetBMSTotalInfo(string bcmuid)
        {// 这个函数如果经常被调用，可以考虑重构成Dictionary
            List<BatteryTotalViewModel> totallist= EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList();
            
            if(totallist != null)
            {
                foreach (var total in totallist)
                {
                    if (total.TotalID == bcmuid)
                    {
                        return total;

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
            List<BatteryTotalViewModel> totallist = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList();//获取所有电池数据
            List<string>totalalarminfo = new List<string>();
            foreach(var total in totallist)
            {
                List<string>bcmualarm = new List<string>();
                List<string>bcmufault = new List<string>();
               
                bcmualarm = total.AlarmStateBCMU.ToList();
                bcmufault = total.FaultyStateBCMU.ToList();
                totalalarminfo.AddRange(bcmualarm);
                totalalarminfo.AddRange((bcmufault));//BCMU数据得到
                for (int i = 0;i<total.batterySeriesViewModelList.Count;i++)
                {
                    List<string> bmufault = new List<string>();
                    bmufault = total.batterySeriesViewModelList[i].FaultyStateBMU.ToList();
                    totalalarminfo.AddRange(bmufault);//BMU数据得到
                }
                
            }
            List<string>newlist = new List<string>();
            
            totalalarminfo=totalalarminfo.Distinct().ToList();
            return totalalarminfo;
        }
        /// <summary>
        /// 所有SOC、最大最小平均SOC
        /// </summary>
        /// <returns></returns>
        public static List<double> GetTotalSOC()
        {
            List<BatteryTotalViewModel> batteryTotalBases = new List<BatteryTotalViewModel>();
            batteryTotalBases = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList.ToList();
            List<double> SOCTotalList = new List<double>();
            foreach (var total in batteryTotalBases)
            {
                SOCTotalList.Add(total.TotalSOC);
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
    }
}
