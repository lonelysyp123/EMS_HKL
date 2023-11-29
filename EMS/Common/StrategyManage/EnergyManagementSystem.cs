using EMS.Common.StrategyManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class EnergyManagementSystem
    {
        private Thread _operationThread;
        private EmsController _controller;
        public EnergyManagementSystem()
        {
            _controller = new EmsController();
            _operationThread = null;

        }

        public void RestartOperationThread()
        {
            if (_operationThread != null) _operationThread.Abort();
            _operationThread = new Thread(_controller.ContinueOperation);

        }

    }
}
