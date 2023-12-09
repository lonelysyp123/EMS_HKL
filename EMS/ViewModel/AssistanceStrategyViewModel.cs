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
            List<double> singleVoltages1 = new List<double>();
            List<double> singleVoltages2 = new List<double>();
            List<double> singleVoltages3 = new List<double>();
            List<double> singleVoltages4 = new List<double>();
            List<double> singleVoltages5 = new List<double>();
            List<double> singleVoltages6 = new List<double>();
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

            //电池簇1告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases1.TotalVoltage);
            for (int i = 0; i < batteryTotalBases1.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases1.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases1.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases1.Series[i].Batteries[j].Voltage;
                    singleVoltages1.Add(singlevolvalue);
                    if (batteryTotalBases1.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases1.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases1.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases1.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases1.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases1.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue1 = singleVoltages1.Max();
            double minSingleVolValue1 = singleVoltages1.Max();
            double voltagesDiff1 = maxSingleVolValue1 - minSingleVolValue1;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff1);
            if (batteryTotalBases1.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases1.TotalCurrent);
            }
            else if (batteryTotalBases1.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases1.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases1.TotalSOC);

            //电池簇2告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases2.TotalVoltage);
            for (int i = 0; i < batteryTotalBases2.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases2.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases2.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases2.Series[i].Batteries[j].Voltage;
                    singleVoltages2.Add(singlevolvalue);
                    if (batteryTotalBases2.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases2.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases2.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases2.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases2.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases2.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue2 = singleVoltages2.Max();
            double minSingleVolValue2 = singleVoltages2.Max();
            double voltagesDiff2 = maxSingleVolValue2 - minSingleVolValue2;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff2);
            if (batteryTotalBases2.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases2.TotalCurrent);
            }
            else if (batteryTotalBases2.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases2.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases2.TotalSOC);

            //电池簇3告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases3.TotalVoltage);
            for (int i = 0; i < batteryTotalBases3.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases3.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases3.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases3.Series[i].Batteries[j].Voltage;
                    singleVoltages3.Add(singlevolvalue);
                    if (batteryTotalBases3.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases3.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases3.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases3.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases3.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases3.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue3 = singleVoltages3.Max();
            double minSingleVolValue3 = singleVoltages3.Max();
            double voltagesDiff3 = maxSingleVolValue3 - minSingleVolValue3;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff3);
            if (batteryTotalBases3.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases3.TotalCurrent);
            }
            else if (batteryTotalBases3.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases3.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases3.TotalSOC);

            //电池簇4告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases4.TotalVoltage);
            for (int i = 0; i < batteryTotalBases4.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases4.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases4.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases4.Series[i].Batteries[j].Voltage;
                    singleVoltages4.Add(singlevolvalue);
                    if (batteryTotalBases4.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases4.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases4.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases4.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases4.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases4.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue4 = singleVoltages4.Max();
            double minSingleVolValue4 = singleVoltages4.Max();
            double voltagesDiff4 = maxSingleVolValue4 - minSingleVolValue4;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff4);
            if (batteryTotalBases4.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases4.TotalCurrent);
            }
            else if (batteryTotalBases4.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases4.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases4.TotalSOC);

            //电池簇5告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases5.TotalVoltage);
            for (int i = 0; i < batteryTotalBases5.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases5.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases5.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases5.Series[i].Batteries[j].Voltage;
                    singleVoltages5.Add(singlevolvalue);
                    if (batteryTotalBases5.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases5.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases5.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases5.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases5.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases5.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue5 = singleVoltages5.Max();
            double minSingleVolValue5 = singleVoltages5.Max();
            double voltagesDiff5 = maxSingleVolValue5 - minSingleVolValue5;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff5);
            if (batteryTotalBases5.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases5.TotalCurrent);
            }
            else if (batteryTotalBases5.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases5.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases5.TotalSOC);

            //电池簇6告警信息汇总
            AssistanceStrategyViewModel.ClusterVolLevel(batteryTotalBases6.TotalVoltage);
            for (int i = 0; i < batteryTotalBases6.SeriesCount; i++)
            {
                for (int j = 0; j < batteryTotalBases1.BatteriesCountInSeries; j++)
                {
                    AssistanceStrategyViewModel.SingleVolLevel(batteryTotalBases6.Series[i].Batteries[j].Voltage);
                    double singlevolvalue = batteryTotalBases6.Series[i].Batteries[j].Voltage;
                    singleVoltages6.Add(singlevolvalue);
                    if (batteryTotalBases6.StateBCMU == 1)
                    {
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases6.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempCharProtectLevel(batteryTotalBases6.Series[i].Batteries[j].Temperature2);
                    }
                    else if (batteryTotalBases6.StateBCMU == 2)
                    {
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases6.Series[i].Batteries[j].Temperature1);
                        AssistanceStrategyViewModel.TempDischarProtectLevel(batteryTotalBases6.Series[i].Batteries[j].Temperature2);
                    }
                    else
                    {

                    }
                }
            }
            double maxSingleVolValue6 = singleVoltages6.Max();
            double minSingleVolValue6 = singleVoltages6.Max();
            double voltagesDiff6 = maxSingleVolValue6 - minSingleVolValue6;
            AssistanceStrategyViewModel.SingleVolDiffProtectLevel(voltagesDiff6);
            if (batteryTotalBases6.StateBCMU == 1)
            {
                AssistanceStrategyViewModel.CurrentCharLevel(batteryTotalBases6.TotalCurrent);
            }
            else if (batteryTotalBases6.StateBCMU == 2)
            {
                AssistanceStrategyViewModel.CurrentDischarLevel(batteryTotalBases6.TotalCurrent);
            }
            else
            {
            }
            AssistanceStrategyViewModel.SocProtectLevel(batteryTotalBases6.TotalSOC);

            return INFOAS;
        }


        //组端电压保护等级
        private static void ClusterVolLevel(double clustervoltages)
        {
            if (clustervoltages >= 596 && clustervoltages < 617)
            {
                INFOAS.Add("电池组高压1级");
            }
            else if (clustervoltages >= 617 && clustervoltages < 638)
            {
                INFOAS.Add("电池组高压2级");
            }
            else if (clustervoltages >= 638)
            {
                INFOAS.Add("电池组高压3级");
            }
            else if (clustervoltages > 474 && clustervoltages <= 495)
            {
                INFOAS.Add("电池组低压1级");
            }
            else if (clustervoltages > 453 && clustervoltages <= 474)
            {
                INFOAS.Add("电池组低压2级");
            }
            else if (clustervoltages <= 453)
            {
                INFOAS.Add("电池组低压3级");
            }
            else
            {
            }
        }

        //单体电压保护等级
        public static void SingleVolLevel(double singlevoltage)
        {
            if (singlevoltage >= 14.3 && singlevoltage < 14.8)
            {
                INFOAS.Add("单体电池高压1级");
            }
            else if (singlevoltage >= 14.8 && singlevoltage < 15)
            {
                INFOAS.Add("单体电池高压2级");
            }
            else if (singlevoltage >= 15)
            {
                INFOAS.Add("单体电池高压3级");
            }
            else if (singlevoltage > 11 && singlevoltage <= 11.5)
            {
                INFOAS.Add("单体电池低压1级");
            }
            else if (singlevoltage > 10.6 && singlevoltage <= 11)
            {
                INFOAS.Add("单体电池低压2级");
            }
            else if (singlevoltage <= 10.6)
            {
                INFOAS.Add("单体电池低压3级");
            }
            else
            {
            }
        }

        //充电电流保护等级
        public static void CurrentCharLevel(double currentlevel)
        {
            if (currentlevel >= 120 && currentlevel < 130)
            {
                INFOAS.Add("电池组充电过流1级");
            }
            else if (currentlevel >= 130 && currentlevel < 140)
            {
                INFOAS.Add("电池组充电过流2级");
            }
            else if (currentlevel >= 140)
            {
                INFOAS.Add("电池组充电过流3级");
            }
            else
            {
            }
        }

        //放电电流保护等级
        public static void CurrentDischarLevel(double currentlevel)
        {
            if (currentlevel >= 120 && currentlevel < 130)
            {
                INFOAS.Add("电池组放电过流1级");
            }
            else if (currentlevel >= 130 && currentlevel < 140)
            {
                INFOAS.Add("电池组放电过流2级");
            }
            else if (currentlevel >= 140)
            {
                INFOAS.Add("电池组放电过流3级");
            }
            else
            {
            }
        }

        //充电温度保护等级
        public static void TempCharProtectLevel(double tempcharprotectlevel)
        {
            if (tempcharprotectlevel >= 45 && tempcharprotectlevel < 50)
            {
                INFOAS.Add("充电高温1级");
            }
            else if (tempcharprotectlevel >= 50 && tempcharprotectlevel < 55)
            {
                INFOAS.Add("充电高温2级");
            }
            else if (tempcharprotectlevel >= 55)
            {
                INFOAS.Add("充电高温3级");
            }
            else if (tempcharprotectlevel > -2 && tempcharprotectlevel <= 0)
            {
                INFOAS.Add("充电低温1级");
            }
            else if (tempcharprotectlevel > -5 && tempcharprotectlevel <= -2)
            {
                INFOAS.Add("充电低温2级");
            }
            else if (tempcharprotectlevel <= -5)
            {
                INFOAS.Add("充电低温3级");
            }
            else
            {
            }

        }

        //放电温度保护等级
        public static void TempDischarProtectLevel(double tempdischarprotectlevel)
        {
            if (tempdischarprotectlevel >= 45 && tempdischarprotectlevel < 50)
            {
                INFOAS.Add("放电低温1级");
            }
            else if (tempdischarprotectlevel >= 50 && tempdischarprotectlevel < 55)
            {
                INFOAS.Add("放电低温2级");
            }
            else if (tempdischarprotectlevel >= 55)
            {
                INFOAS.Add("放电低温3级");
            }
            else
            {
            }

        }

        //SOC保护等级
        public static void SocProtectLevel(double socprotectlevel)
        {
            if (socprotectlevel > 5 && socprotectlevel <= 10)
            {
                INFOAS.Add("低SOC1级");
            }
            else if (socprotectlevel > 1 && socprotectlevel <= 5)
            {
                INFOAS.Add("低SOC2级");
            }
            else if (socprotectlevel > 0 && socprotectlevel <= 1)
            {
                INFOAS.Add("低SOC3级");
            }
            else
            {
            }
        }

        //单体压差保护
        public static void SingleVolDiffProtectLevel(double socprotectlevel)
        {
            if (socprotectlevel >= 1.2 && socprotectlevel < 1.5)
            {
                INFOAS.Add("单体压差1级");
            }
            else if (socprotectlevel >= 1.5 && socprotectlevel < 1.8)
            {
                INFOAS.Add("单体压差2级");
            }
            else if (socprotectlevel >= 1.8)
            {
                INFOAS.Add("单体压差3级");
            }
            else
            {
            }
        }

    }
}