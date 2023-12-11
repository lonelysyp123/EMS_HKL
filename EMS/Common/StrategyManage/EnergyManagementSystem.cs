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
    public class SmartMeterManager 
    {
        private List<ElectricityMeterModel> _smartMeters;
        public List<ElectricityMeterModel> SmartMeters { get { return _smartMeters; } } //封装，不能set

        public void AddDev(ElectricityMeterModel item)
        {
            if (_smartMeters == null)
            {
                _smartMeters = new List<ElectricityMeterModel>();
            }
            _smartMeters.Add(item);
        }

        public void RemoveDev(ElectricityMeterModel item)
        {
            if (_smartMeters != null && _smartMeters.Count > 0)
            {
                if (_smartMeters.Contains(item))
                {
                    _smartMeters.Remove(item);
                }
            }
        }
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
        private SmartMeterManager _smart_meter_manager;
        private BmsManager _bms_manager;
        private object _database_manager;
        private object _cloud_manager;

        

        private static EnergyManagementSystem _globalInstance;

        public static EnergyManagementSystem GlobalInstance { get { return _globalInstance; } }

        public static void Initialization()
        {
            _globalInstance = new EnergyManagementSystem();
        }

        public BmsManager BmsManager { get { return _bms_manager; } }
        public object PcsManager {  get { return _pcs_manager; } }
        public SmartMeterManager SmartMeterManager { get { return _smart_meter_manager; } }
        public EnergyManagementSystem()
        {
            _controller = new EmsController();
            _operationThread = null;
            _bms_manager = new BmsManager();
            _smart_meter_manager = new SmartMeterManager();
        }
       
        public void Initialization(object _pcs_manager, object _smart_meter_manager,  object _database_manager, object _cloud_manager) 
        {
            
            //return;
        }

        public void RestartOperationThread()
        {
            if (_operationThread != null) _operationThread.Abort();
            _operationThread = new Thread(_controller.ContinueOperation);

        }

    }
}
