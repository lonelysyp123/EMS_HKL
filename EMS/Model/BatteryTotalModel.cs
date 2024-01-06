using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using EMS.Common.Modbus.ModbusTCP;
using System.Threading;
using System.Collections.Concurrent;
using EMS.Storage.DB.Models;
using System.Security.Cryptography;

namespace EMS.Model
{
    public class BatteryTotalModel
    {
        /// <summary>
        /// 总簇电压    0.1V 
        /// </summary>
        public double TotalVoltage { get; set; }

        /// <summary>
        /// 总簇电流    0.1A
        /// </summary>
        public double TotalCurrent { get; set; }

        /// <summary>
        /// 总簇SOC   0.1%
        /// </summary>
        public double TotalSOC { get; set; }

        /// <summary>
        /// 总簇SOH   0.1%
        /// </summary>
        public double TotalSOH { get; set; }

        /// <summary>
        /// 平均温度    0.1℃
        /// </summary>
        public double AverageTemperature { get; set; }

        /// <summary>
        /// 单体最低电压
        /// </summary>
        public double MinVoltage { get; set; }

        /// <summary>
        /// 单体最低电压编号
        /// </summary>
        public int MinVoltageIndex { get; set; }

        /// <summary>
        /// 单体最高电压
        /// </summary>
        public double MaxVoltage { get; set; }

        /// <summary>
        /// 单体最高电压编号
        /// </summary>
        public int MaxVoltageIndex { get; set; }

        /// <summary>
        /// 单体最低温度
        /// </summary>
        public double MinTemperature { get; set; }

        /// <summary>
        /// 单体最低温度编号
        /// </summary>
        public int MinTemperatureIndex { get; set; }

        /// <summary>
        /// 单体最高温度
        /// </summary>
        public double MaxTemperature { get; set; }

        /// <summary>
        /// 单体最高温度编号
        /// </summary>
        public int MaxTemperatureIndex { get; set; }

        /// <summary>
        /// 高压箱温度
        /// </summary>
        public double VolContainerTemperature1 { get; set; }
        public double VolContainerTemperature2 { get; set; }
        public double VolContainerTemperature3 { get; set; }
        public double VolContainerTemperature4 { get; set; }

        /// <summary>
        /// BCMU软件版本号
        /// </summary>
        public int VersionSWBCMU { get; set; }

        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public int IResistanceRP { get; set; }
        public int IResistanceRN { get; set; }

        /// <summary>
        /// BCMU告警
        /// </summary>
        public int AlarmStateBCMUFlag1 { get; set; }
        public int AlarmStateBCMUFlag2 { get; set; }
        public int AlarmStateBCMUFlag3 { get; set; }
        public List<string> AlarmStateBCMU { get; set; }

        /// <summary>
        /// BCMU故障
        /// </summary>
        public int FaultStateBCMUTotalFlag { get; set; }
        public int FaultStateBCMUFlag1 { get; set; }
        public int FaultStateBCMUFlag2 { get; set; }
        public List<string> FaultyStateBCMU { get; set; }


        /// <summary>
        /// BCMU状态颜色 充电、静置、放电、离网
        /// </summary>
        public int StateBCMU { get; set; }

        /// <summary>
        /// 循环次数
        /// </summary>
        public int BatteryCycles { get; set; }

        /// <summary>
        /// BCMU硬件版本
        /// </summary>
        public int HWVersionBCMU { get; set; }

        /// <summary>
        /// DC母线电压
        /// </summary>
        public int DCVoltage { get; set; }

        /// <summary>
        /// 最大充电功率
        /// </summary>
        public double BatMaxChgPower { get; set; }

        /// <summary>
        /// 最大放电攻略
        /// </summary>
        public double BatMaxDischgPower { get; set; }

        /// <summary>
        /// 单次充电量
        /// </summary>
        public double OneChgCoulomb { get; set; }

        /// <summary>
        /// 单次放电量
        /// </summary>
        public double OneDischgCoulomb { get; set; }

        /// <summary>
        /// 累计充电量
        /// </summary>
        public double TotalChgCoulomb { get; set; }

        /// <summary>
        /// 累计放电量
        /// </summary>
        public double TotalDischgCoulomb { get; set; }

        /// <summary>
        /// 剩余电量
        /// </summary>
        public double RestCoulomb { get; set; }

        /// <summary>
        /// 最大压差
        /// </summary>
        public double MaxVolDiff { get; set; }
        /// <summary>
        /// 平均电压
        /// </summary>
        public double AvgVol { get; set; }

        /// <summary>
        /// 额定容量
        /// </summary>
        public int NomCapacity { get; set; }
        /// <summary>
        /// 额定电压
        /// </summary>
        public int NomVoltage { get; set; }
        /// <summary>
        /// 电池数量
        /// </summary>
        public int BatteryCount { set; get; }
        public ushort SeriesCount = 3;
        public ushort BatteriesCountInSeries = 14;
        public DateTime CurrentTime;
        public string BCMUID;

        public List<BatterySeriesModel> Series { get; set; }

        public BatteryTotalModel()
        {
            Series = new List<BatterySeriesModel>();
            for (int i = 0; i < SeriesCount; i++)
            {
                BatterySeriesModel series = new BatterySeriesModel(BatteriesCountInSeries);
                Series.Add(series);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
