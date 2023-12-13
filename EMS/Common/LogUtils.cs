using EMS.Api;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common
{
    public class LogUtils
    {
        //private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ILog log = LogManager.GetLogger("loginfo");

        public static void Debug(string debug)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug);
            }
        }

        public static void Debug(string debug, Exception exp)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug, exp);
            }
        }

        public static void Info(string info)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info);
            }
        }

        public static void Warn(string warning)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warning);
            }
        }

        public static void Warn(string warning, Exception exp)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warning, exp);
            }
        }

        public static void Error(string error)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(error);
            }
        }

        public static void Error(string error, Exception exp)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(error, exp);
            }
        }

        public static void Fatal(string error)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(error);
            }
        }

        public static void Fatal(string error, Exception exp)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(error, exp);
            }
        }
    }
}
