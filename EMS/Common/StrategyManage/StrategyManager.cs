using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class StrategyManager
    {
        private static object syncRoot = new Object();

        private static StrategyManager instance;
        public static StrategyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StrategyManager();
                        }
                    }
                }
                return instance;
            }
        }


        private StrategyManager()
        {

        }

        public List<string> GetBMSAlarmandFaultInfo() { return null ; }
        public List<string>GetSystemErrors() { return null ; }
    }
}
