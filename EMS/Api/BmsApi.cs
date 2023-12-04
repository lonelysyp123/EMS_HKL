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
        public static List<BatteryTotalBase> GetBMSTotalInfo()
        {
            return EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;
        }
        public static BatteryTotalBase GetBMSTotalInfo(string bcmuid)
        {
            List<BatteryTotalBase>totallist= EnergyManagementSystem.GlobalInstance.BmsManager.BmsTotalList;
            foreach(var total in totallist)
            {
                if(total.BCMUID == bcmuid)
                {
                    return total;
                  
                }
              
            }
            return null;
        }
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


        

    }
}
