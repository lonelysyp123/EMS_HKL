using EMS.Api;
using EMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class AssistanceStrategyViewModel : ViewModelBase
    {

        public static ObservableCollection<string> INFOAS = new ObservableCollection<string>();

        //BMS数据处理方法，生成告警
        public ObservableCollection<string> RecheckStrategy()
        {
            ObservableCollection<double> singleVoltages1 = new ObservableCollection<double>();
            ObservableCollection<double> singleVoltages2 = new ObservableCollection<double>();
            ObservableCollection<double> singleVoltages3 = new ObservableCollection<double>();
            ObservableCollection<double> singleVoltages4 = new ObservableCollection<double>();
            ObservableCollection<double> singleVoltages5 = new ObservableCollection<double>();
            ObservableCollection<double> singleVoltages6 = new ObservableCollection<double>();
            string bcmuid1 = "BCMU(1)";
            string bcmuid2 = "BCMU(2)";
            string bcmuid3 = "BCMU(3)";
            string bcmuid4 = "BCMU(4)";
            string bcmuid5 = "BCMU(5)";
            string bcmuid6 = "BCMU(6)";
            
            BatteryTotalBase batteryTotalBases1 = BmsApi.GetBMSTotalInfo(bcmuid1);
            BatteryTotalBase batteryTotalBases2 = BmsApi.GetBMSTotalInfo(bcmuid2);
            BatteryTotalBase batteryTotalBases3 = BmsApi.GetBMSTotalInfo(bcmuid3);
            BatteryTotalBase batteryTotalBases4 = BmsApi.GetBMSTotalInfo(bcmuid4);
            BatteryTotalBase batteryTotalBases5 = BmsApi.GetBMSTotalInfo(bcmuid5);
            BatteryTotalBase batteryTotalBases6 = BmsApi.GetBMSTotalInfo(bcmuid6);

            AnalyzeBatteryCluster(batteryTotalBases1, singleVoltages1);//第一簇
            AnalyzeBatteryCluster(batteryTotalBases2, singleVoltages2);
            AnalyzeBatteryCluster(batteryTotalBases3, singleVoltages3);
            AnalyzeBatteryCluster(batteryTotalBases4, singleVoltages4);
            AnalyzeBatteryCluster(batteryTotalBases5, singleVoltages5);
            AnalyzeBatteryCluster(batteryTotalBases6, singleVoltages6);

            return INFOAS;
        }


        //每簇组端电压保护，单体电压保护，单体压差保护逻辑
        private void AnalyzeBatteryCluster(BatteryTotalBase batteryTotalBases, ObservableCollection<double> singleVoltages)
        {
            if (batteryTotalBases != null)
            {
                ClusterVolLevel(batteryTotalBases.TotalVoltage);//组端电压保护
                for (int i = 0; i < batteryTotalBases.SeriesCount; i++)
                {
                    for (int j = 0; j < batteryTotalBases.BatteriesCountInSeries; j++)
                    {
                        SingleVolLevel(batteryTotalBases.Series[i].Batteries[j].Voltage);//单体电压保护
                        double singlevolvalue = batteryTotalBases.Series[i].Batteries[j].Voltage;
                        singleVoltages.Add(singlevolvalue);
                        ProcessTemperature(batteryTotalBases, i, j);
                    }
                }
                double maxSingleVolValue = singleVoltages.Max();
                double minSingleVolValue = singleVoltages.Min();
                double voltagesDiff = maxSingleVolValue - minSingleVolValue;
                SingleVolDiffProtectLevel(voltagesDiff);//单体压差保护
                ProcessCurrentAndSOC(batteryTotalBases);
            }
        }

        //充放电温度告警逻辑
        private void ProcessTemperature(BatteryTotalBase batteryTotalBases, int seriesIndex, int batteryIndex)
        {
            switch (batteryTotalBases.StateBCMU)
            {
                case 1:
                    TempCharProtectLevel(batteryTotalBases.Series[seriesIndex].Batteries[batteryIndex].Temperature1);
                    TempCharProtectLevel(batteryTotalBases.Series[seriesIndex].Batteries[batteryIndex].Temperature2);
                    break;
                case 2:
                    TempDischarProtectLevel(batteryTotalBases.Series[seriesIndex].Batteries[batteryIndex].Temperature1);
                    TempDischarProtectLevel(batteryTotalBases.Series[seriesIndex].Batteries[batteryIndex].Temperature2);
                    break;
            }
        }

        //充放电电流告警逻辑和SOC告警逻辑
        private void ProcessCurrentAndSOC(BatteryTotalBase batteryTotalBases)
        {
            switch (batteryTotalBases.StateBCMU)
            {
                case 1:
                    CurrentCharLevel(batteryTotalBases.TotalCurrent);
                    break;
                case 2:
                    CurrentDischarLevel(batteryTotalBases.TotalCurrent);
                    break;
            }
            SocProtectLevel(batteryTotalBases.TotalSOC);
        }


        //组端电压保护等级
        private void ClusterVolLevel(double clustervoltages)
        {
            switch (true)
            {
                case bool _ when clustervoltages >= 596 && clustervoltages < 617:
                    INFOAS.Add("电池组高压1级");
                    break;
                case bool _ when clustervoltages >= 617 && clustervoltages < 638:
                    INFOAS.Add("电池组高压2级");
                    break;
                case bool _ when clustervoltages >= 638:
                    INFOAS.Add("电池组高压3级");
                    break;
                case bool _ when clustervoltages > 474 && clustervoltages <= 495:
                    INFOAS.Add("电池组低压1级");
                    break;
                case bool _ when clustervoltages > 453 && clustervoltages <= 474:
                    INFOAS.Add("电池组低压2级");
                    break;
                case bool _ when clustervoltages <= 453:
                    INFOAS.Add("电池组低压3级");
                    break;
            }
        }

        //单体电压保护等级
        private void SingleVolLevel(double singlevoltage)
        {
            switch (true)
            {
                case bool _ when singlevoltage >= 14.3 && singlevoltage < 14.8:
                    INFOAS.Add("单体电池高压1级");
                    break;
                case bool _ when singlevoltage >= 14.8 && singlevoltage < 15:
                    INFOAS.Add("单体电池高压2级");
                    break;
                case bool _ when singlevoltage >= 15:
                    INFOAS.Add("单体电池高压3级");
                    break;
                case bool _ when singlevoltage > 11 && singlevoltage <= 11.5:
                    INFOAS.Add("单体电池低压1级");
                    break;
                case bool _ when singlevoltage > 10.6 && singlevoltage <= 11:
                    INFOAS.Add("单体电池低压2级");
                    break;
                case bool _ when singlevoltage <= 10.6:
                    INFOAS.Add("单体电池低压3级");
                    break;
            }
        }

        //充电电流保护等级
        private void CurrentCharLevel(double currentlevel)
        {
            switch (true)
            {
                case bool _ when currentlevel >= 120 && currentlevel < 130:
                    INFOAS.Add("电池组充电过流1级");
                    break;
                case bool _ when currentlevel >= 130 && currentlevel < 140:
                    INFOAS.Add("电池组充电过流2级");
                    break;
                case bool _ when currentlevel >= 140:
                    INFOAS.Add("电池组充电过流3级");
                    break;
            }
        }

        //放电电流保护等级
        private void CurrentDischarLevel(double currentlevel)
        {
            switch (true)
            {
                case bool _ when currentlevel >= 120 && currentlevel < 130:
                    INFOAS.Add("电池组放电过流1级");
                    break;
                case bool _ when currentlevel >= 130 && currentlevel < 140:
                    INFOAS.Add("电池组放电过流2级");
                    break;
                case bool _ when currentlevel >= 140:
                    INFOAS.Add("电池组放电过流3级");
                    break;
            }
        }

        //充电温度保护等级
        private void TempCharProtectLevel(double tempcharprotectlevel)
        {
            switch (true)
            {
                case bool _ when tempcharprotectlevel >= 45 && tempcharprotectlevel < 50:
                    INFOAS.Add("充电高温1级");
                    break;
                case bool _ when tempcharprotectlevel >= 50 && tempcharprotectlevel < 55:
                    INFOAS.Add("充电高温2级");
                    break;
                case bool _ when tempcharprotectlevel >= 55:
                    INFOAS.Add("充电高温3级");
                    break;
                case bool _ when tempcharprotectlevel > -2 && tempcharprotectlevel <= 0:
                    INFOAS.Add("充电低温1级");
                    break;
                case bool _ when tempcharprotectlevel > -5 && tempcharprotectlevel <= -2:
                    INFOAS.Add("充电低温2级");
                    break;
                case bool _ when tempcharprotectlevel <= -5:
                    INFOAS.Add("充电低温3级");
                    break;
            }
        }

        //放电温度保护等级
        private void TempDischarProtectLevel(double tempdischarprotectlevel)
        {
            switch (true)
            {
                case bool _ when tempdischarprotectlevel >= 45 && tempdischarprotectlevel < 50:
                    INFOAS.Add("放电高温1级");
                    break;
                case bool _ when tempdischarprotectlevel >= 50 && tempdischarprotectlevel < 55:
                    INFOAS.Add("放电高温2级");
                    break;
                case bool _ when tempdischarprotectlevel >= 55:
                    INFOAS.Add("放电高温3级");
                    break;
            }
        }

        //SOC保护等级
        private void SocProtectLevel(double socprotectlevel)
        {
            switch (true)
            {
                case bool _ when socprotectlevel > 5 && socprotectlevel <= 10:
                    INFOAS.Add("低SOC1级");
                    break;
                case bool _ when socprotectlevel > 1 && socprotectlevel <= 5:
                    INFOAS.Add("低SOC2级");
                    break;
                case bool _ when socprotectlevel > 0 && socprotectlevel <= 1:
                    INFOAS.Add("低SOC3级");
                    break;
            }
        }

        //单体压差保护
        private void SingleVolDiffProtectLevel(double socprotectlevel)
        {
            switch (true)
            {
                case bool _ when socprotectlevel >= 1.2 && socprotectlevel < 1.5:
                    INFOAS.Add("单体压差1级");
                    break;
                case bool _ when socprotectlevel >= 1.5 && socprotectlevel < 1.8:
                    INFOAS.Add("单体压差2级");
                    break;
                case bool _ when socprotectlevel >= 1.8:
                    INFOAS.Add("单体压差3级");
                    break;
            }
        }

    }
}