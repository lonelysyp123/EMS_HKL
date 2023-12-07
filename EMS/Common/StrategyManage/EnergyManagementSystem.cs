using EMS.Api;
using EMS.Common.StrategyManage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Model
{

    public class SmartMeter
    {
        private double _voltage;
        public double Voltage { get; set; }

        public void readVoltage() { }

        public double GetVoltage() { return _voltage; }
    }





    public class SmartMeterManager
    {
        private List<object> smart_meters;
    }
    public class BmsManager
    {
        //
        private List<BatteryTotalBase> _bmsTotalList;
        public List<BatteryTotalBase> BmsTotalList { get { return _bmsTotalList; } } //封装，不能set



        public void SetBMSList(List<BatteryTotalBase> totallist)
        {
            _bmsTotalList = totallist;
        }



    }


    public class EnergyManagementSystem
    {
        private Thread _operationThread;
        private EmsController _controller;
        private object _pcs_manager;
        private object _smart_meter_manager;
        private BmsManager _bms_manager;
        private object _database_manager;
        private object _cloud_manager;
        




        private static EnergyManagementSystem _globalInstance;

        public static EnergyManagementSystem GlobalInstance { get { return _globalInstance; } }

        public static void Initialization()
        {
            _globalInstance = new EnergyManagementSystem();
        }

        public EmsController Controller { get { return _controller; } }
        public BmsManager BmsManager { get { return _bms_manager; } }
        public object PcsManager { get { return _pcs_manager; } }
        public EnergyManagementSystem()
        {
           
            _operationThread = null;
            _bms_manager = new BmsManager();
            _controller = new EmsController();

        }

        public void Initialization(object _pcs_manager, object _smart_meter_manager, object _database_manager, object _cloud_manager)
        {

            //return;
        }

        public void RestartOperationThread()
        {
            _controller.Scheduler.ResetPattern();
            if (_operationThread != null) _operationThread.Abort();
            _operationThread = new Thread(_controller.ContinueOperation);

        }

    }
}
