using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.Common
{
    /// <summary>
    /// BCMU接线图
    /// </summary>
    public static class BCMUColors
    {
        public static Color Alarmcolor_F = Color.FromRgb(244, 244, 244);//告警图标：无告警#FAFAFA
        public static Color Alarmcolor_Alarm = Color.FromRgb(255, 165, 0);//告警图标：告警#FFA500
        public static Color Alarmcolor_Fault = Color.FromRgb(216, 30, 6);//告警图标：故障#D81E06
        public static Color IsConnect_T = Color.FromRgb(27, 160, 33);//连网#1BA021
        public static Color IsConnect_F = Color.FromRgb(191,55,40);//断网#BF3728
    }

    /// <summary>
    /// 连接状态:灯颜色显示
    /// </summary>
    public static class LightColors
    {
        /// <summary>
        /// 参数运用：首页（正常、云端通讯、BMS运行、PCS运行、电表运行、启停状态），
        /// BCMU(电网连接状态：并网、电池簇状态：静置、充电、放电)，
        /// PCS（DC侧支路1启停状态、PCS控制状态*3）
        /// </summary>
        public static Color Open_Green = Color.FromRgb(0, 155, 10);// 开启or灯亮or连接：#009B0A

        /// <summary>
        /// 参数运用：首页（离线、预警、轻故障、重故障、危机故障、故障状态）,
        /// BCMU（电网连接状态：离网、电池簇状态：离网），
        /// PCS（PCS状态*3）
        /// </summary>
        public static Color Open_Red = Color.FromRgb(255, 165, 0);// 开启or灯亮or连接：#D81E06
        public static Color Close = Color.FromRgb(187, 187, 187);// 关闭or灯灭or断开：#BBBBBB
    }


}
          