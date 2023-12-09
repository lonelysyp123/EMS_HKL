using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Api
{

    public class BmsApi
    {
        /// <summary>
        /// 得到BMS信息
        /// </summary>
        /// <returns></returns>
        public static List<BatteryTotalBase> GetBMSTotalInfo()//得到所有BMS信息
        {
            return EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;
        }
        public static BatteryTotalBase GetBMSTotalInfo(string bcmuid)//得到单簇信息
        {// 这个函数如果经常被调用，可以考虑重构成Dictionary
            List<BatteryTotalBase>totallist= EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;
            
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
            List<BatteryTotalBase> totallist = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;//获取所有电池数据
            List<string>totalalarminfo = new List<string>();
            foreach(var total in totallist)
            {
                List<string>bcmualarm = new List<string>();
                List<string>bcmufault = new List<string>();
               
                bcmualarm = total.AlarmStateBCMU.ToList();
                bcmufault = total.FaultyStateBCMU.ToList();
                totalalarminfo.AddRange(bcmualarm);
                totalalarminfo.AddRange((bcmufault));//BCMU数据得到
                for (int i = 0;i<total.Series.Count;i++)
                {
                    List<string> bmufault = new List<string>();
                    bmufault = total.Series[i].FaultyStateBMU.ToList();
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
            List<BatteryTotalBase> batteryTotalBases = new List<BatteryTotalBase>();
            batteryTotalBases = EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;
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
