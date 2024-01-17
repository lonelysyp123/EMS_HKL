using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Util
{
    public static class DateTimeUtil
    {
        /// <summary>
        /// DateTime --> long
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ConvertDateTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }


        /// <summary>
        /// long --> DateTime
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ConvertLongToDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
    }
}
