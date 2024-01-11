using EMS.Api;
using EMS.Common.StrategyManage;
using EMS.Service;
using EMS.ViewModel;
using MQTTnet.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNCN.EMS.Common.StrategyManage;
using TNCN.EMS.Common.Util;

namespace EMS.Model
{
    public class SmartMeterManager 
    {
        private List<ElectricMeterViewModel> _smartMeters;
        public List<ElectricMeterViewModel> SmartMeters { get { return _smartMeters; } } //封装，不能set

        public void AddDev(ElectricMeterViewModel item)
        {
            if (_smartMeters == null)
            {
                _smartMeters = new List<ElectricMeterViewModel>();
            }
            _smartMeters.Add(item);
        }

        public void RemoveDev(ElectricMeterViewModel item)
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

    public class SEMManager
    {
        private SmartElectricityMeterDataService _semDataService;
        public SmartElectricityMeterDataService SEMDataService { get { return _semDataService; } }
    }

    public class PCSManager
    {
        private PCSDataService _pcsDataService;
        public PCSDataService PCSDataService { get { return _pcsDataService; } }
        public void SetPCS(PCSDataService pcsdataservice)
        {
            _pcsDataService = pcsdataservice;
        }
    }


  
    public class EnergyManagementSystem
    {
        private Thread _operationThread;
        private EmsController _controller;
        private SmartMeterManager _smart_meter_manager;
        private PCSManager _pcs_manager;
        private SEMManager _sem_manager;
        private BMSManager _bms_manager;
        private object _database_manager;
        private object _cloud_manager;
        private MqttClientManager mqttClientManager;
        private IniFileHelper _configuration;

        private double _chargingEfficiency;
        private double _dischargingEfficiency;
        private double _initialEnergy;
        private double _energyCapacity;

        public double ChargingEfficiency { get { return _chargingEfficiency; } }
        public double DischargingEfficiency { get { return _dischargingEfficiency; } }
        public double InitialEnergy { get { return _initialEnergy; } }
        public double EnergyCapacity { get { return _energyCapacity; } }
        public void SetChargingEfficiency(double efficiency) { _chargingEfficiency = efficiency; }
        public void SetDischargingEfficiency(double efficiency) { _dischargingEfficiency = efficiency; }
        public void SetInitialEnergy(double  energy) { _initialEnergy = energy; }
        public void SetEnergyCapacity(double capacity) { _energyCapacity = capacity; }

        public IniFileHelper Configuration { get { return _configuration; } }

        private static EnergyManagementSystem _globalInstance;

        public static EnergyManagementSystem GlobalInstance { get { return _globalInstance; } }

        public static void Initialization(EnergyManagementSystem globalInstance)
        {
            _globalInstance = globalInstance;
        }

        public EmsController Controller { get { return _controller; } }
        public BMSManager BMSManager { get { return _bms_manager; } }
        public PCSManager PcsManager { get { return _pcs_manager; } }
        public SmartMeterManager SmartMeterManager { get { return _smart_meter_manager; } }
        public SEMManager SemManager { get { return _sem_manager; } }
        public MqttClientManager MqttClientManager { get { return mqttClientManager; } }
        public EnergyManagementSystem()
        {
            _operationThread = null;
            _bms_manager = new BMSManager();
            _controller = new EmsController();
            _pcs_manager =new PCSManager();
            _smart_meter_manager = new SmartMeterManager();
            _sem_manager = new SEMManager();
            mqttClientManager = new MqttClientManager();
        }

        public void Initialization(object _pcs_manager, object _smart_meter_manager, object _sem_manager, object _database_manager, object _cloud_manager)
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
