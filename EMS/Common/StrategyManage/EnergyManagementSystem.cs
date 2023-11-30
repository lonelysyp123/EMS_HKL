using EMS.Api
using EMS.Common.StrategyManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Model
{

    public class SmartMeter {
        private double _voltage;
        public double Voltage { get; set; }

        public void readVoltage() {}

        public double GetVoltage() { return _voltage; }
    }

    public class SmartMeterManager {
        private List<object> smart_meters;
    }
    public class EnergyManagementSystem
    {
        private Thread _operationThread;
        private EmsController _controller;
        private object _pcs_manager;
        private object _smart_meter_manager;
        private object _bms_manager;
        private object _database_manager;
        private object _cloud_manager;

        

        private static EnergyManagementSystem _globalInstance;

        public static EnergyManagementSystem GlobalInstance { get { return _globalInstance; } }

        public object PcsManager {  get { return _pcs_manager; } }
        public EnergyManagementSystem()
        {
            _controller = new EmsController();
            _operationThread = null;

        }

        public void Initialization(object _pcs_manager, object _smart_meter_manager, object _bms_manager, object _database_manager, object _cloud_manager) {
            return;
        }

        public void RestartOperationThread()
        {
            if (_operationThread != null) _operationThread.Abort();
            _operationThread = new Thread(_controller.ContinueOperation);

        }

    }
}
