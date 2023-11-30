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
                    lock(syncRoot)
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

        /// <summary>
        /// 获取DC侧支路1输出功率
        /// </summary>
        /// <param name="PCSIndex">PCS序号，默认为0</param>
        /// <returns>输出功率</returns>
        public double GetDC1Power(int PCSIndex=0)
        {
            if (PCSIndex == 0)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 设置DC侧支路1输出功率
        /// </summary>
        /// <param name="PCSIndex">PCS序号，默认为0</param>
        /// <param name="Power">功率设定值</param>
        /// <returns>是否设定成功</returns>
        public bool SetDC1Power(double Power, int PCSIndex= 0)
        {
            return false;
        }

    }
}
