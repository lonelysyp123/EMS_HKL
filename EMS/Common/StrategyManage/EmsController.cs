using EMS.Api;
using EMS.Model;
using EMS.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNCN.EMS.Common.Mqtt;
using TNCN.EMS.Common.Util;

namespace EMS.Common.StrategyManage
{
    public class EmsController
    {
        /// <summary>
        /// 策略是否开启
        /// </summary>
        private bool _isAutomaticMode;
        private bool _hasDailyPatternEnabled;
        private bool _hasMaxDemandControlEnabled;
        private bool _hasReversePowerflowProtectionEnabled;
        private bool _hasContigencyCheckEnabled;
        private ILog _logger;
        public bool IsAutomaticMode { get { return _isAutomaticMode; } }
        public bool HasDailyPatternEnabled { get { return _hasDailyPatternEnabled; } }
        public bool HasMaxDemandControlEnabled { get { return _hasMaxDemandControlEnabled; } }
        public bool HasReversePowerflowProtectionEnabled { get { return _hasReversePowerflowProtectionEnabled; } }

        private double _maxDemandPower; //负载侧最大功率极限：通常为负载侧变压器额定功率
        private double _maxDemandPowerDescendRate;
        private double _reversePowerActionThreshold;
        private double _reversePowerStopThreshold;
        private double _reversePowerDescendRate;
        private double _dcBusConnectionChargingPowerFactor;
        private double _maxSoc;
        private double _minSoc;
        private double _maxChargingPower;
        private double _maxDischargingPower;
        private double _thresholdTolerance;
        private double _chargingSecurityFactor;
        private double _dischargingSecurityFactor;
        private int _controlPeriod; // 系统控制周期 unit: ms

        public double MaxSoc { get { return _maxSoc; } }
        public void SetMaxSoc(double maxSoc) { _maxSoc = maxSoc; }
        public double MinSoc { get { return _minSoc; } }
        public void SetMinSoc(double minSoc) { _minSoc = minSoc; }
        public double MaxChargingPower { get { return _maxChargingPower; } }
        public void SetMaxChargingPower(double maxChargingPower) { _maxChargingPower = maxChargingPower; }
        public double MaxDischargingPower { get { return _maxDischargingPower; } }
        public void SetMaxDischargingPower(double maxDischargingPower) { _maxChargingPower = maxDischargingPower; }
        public ContingencyStatusEnum ContingencyStatus { get; private set; }

        private BessCommand _currentCommand;
        private BessCommand _manualCommand;
        private IntraDayScheduler _scheduler;
        private ContingencyStatusEnum _contingencyStatus;
        private DateTime _lastActiveTimestamp; // used to indicate the system operation thread is still alive.

        public BessCommand ManualCommand { get { return _manualCommand; } }
        public void SetManualCommand(BessCommand command) { _manualCommand = command; }
        public List<BatteryStrategyModel> DailyPattern { get; set; }

        public void SetMode(bool automationMode, bool maxDemandpowermode, bool reversePowermode, bool dailyPatternMode)
        {
            _hasMaxDemandControlEnabled = maxDemandpowermode;
            _isAutomaticMode = automationMode;
            _hasReversePowerflowProtectionEnabled = reversePowermode;
            _hasDailyPatternEnabled = dailyPatternMode;
        }

        public void SetMaxDemandThreshold(double maxdemandpower, double descendrate)
        {
            _maxDemandPower = maxdemandpower;
            _maxDemandPowerDescendRate = descendrate;
        }
        public void GetMaxDemandThreshhold(out double maxdemandpower, out double descendrate)
        {
            descendrate = _maxDemandPowerDescendRate;
            maxdemandpower = _maxDemandPower;
        }

        public void SetReversePowerThreshold(double threshold, double lowestthreshhold, double descendrate)
        {
            _reversePowerActionThreshold = threshold;
            _reversePowerStopThreshold = lowestthreshhold;
            _reversePowerDescendRate = descendrate;
        }
        public void GetReversePowerThreshold(out double threshold, out double lowestthreshold, out double descendrate)
        {
            threshold = _reversePowerActionThreshold;
            lowestthreshold = _reversePowerStopThreshold;
            descendrate = _reversePowerDescendRate;
        }
        public Dictionary<int, List<BatteryStrategyModel>> DailyPatterns { get; set; }
        public bool IsFaultMode { get { return _contingencyStatus == ContingencyStatusEnum.Level2 || _contingencyStatus == ContingencyStatusEnum.Level3; } }
        public EmsController()
        {
            _currentCommand = null;
            _scheduler = new IntraDayScheduler();
            _contingencyStatus = ContingencyStatusEnum.Normal;
            _logger = LogManager.GetLogger(GetType());
            IniFileHelper.Read(IniSectionEnum.Strategy, "ChargingSecurityFactor", out _chargingSecurityFactor);
            IniFileHelper.Read(IniSectionEnum.Strategy, "ControlPeriod", out _controlPeriod);
            IniFileHelper.Read(IniSectionEnum.Strategy, "DcBusConnectionChargingPowerFactor", out _dcBusConnectionChargingPowerFactor);
            IniFileHelper.Read(IniSectionEnum.Strategy, "DischargingSecurityFactor", out _dischargingSecurityFactor);
            IniFileHelper.Read(IniSectionEnum.Strategy, "HasContigencyCheckEnabled", out _hasContigencyCheckEnabled);
            IniFileHelper.Read(IniSectionEnum.Strategy, "HasDailyPatternEnabled", out _hasDailyPatternEnabled);
            IniFileHelper.Read(IniSectionEnum.Strategy, "HasMaxDemandControlEnabled", out _hasMaxDemandControlEnabled);
            IniFileHelper.Read(IniSectionEnum.Strategy, "HasReversePowerflowProtectionEnabled", out _hasReversePowerflowProtectionEnabled);
            IniFileHelper.Read(IniSectionEnum.Strategy, "IsAutomaticMode", out _isAutomaticMode);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MaxChargingPower", out _maxChargingPower);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MaxDemandPower", out _maxDemandPower);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MaxDemandPowerDescendRate", out _maxDemandPowerDescendRate);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MaxDischargingPower", out _maxDischargingPower);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MaxSoc", out _maxSoc);
            IniFileHelper.Read(IniSectionEnum.Strategy, "MinSoc", out _minSoc);
            IniFileHelper.Read(IniSectionEnum.Strategy, "ReversePowerActionThreshold", out _reversePowerActionThreshold);
            IniFileHelper.Read(IniSectionEnum.Strategy, "ReversePowerDescendRate", out _reversePowerDescendRate);
            IniFileHelper.Read(IniSectionEnum.Strategy, "ReversePowerStopThreshold", out _reversePowerStopThreshold);
            IniFileHelper.Read(IniSectionEnum.Strategy, "ThresholdTolerance", out _thresholdTolerance);

        }

        public IntraDayScheduler Scheduler { get { return _scheduler; } }

        public void ContinueOperation()
        {
            ContingencyCheck();
            NormalOperation();
            _lastActiveTimestamp = DateTime.Now;
            Thread.Sleep(_controlPeriod);
        }
        private void NormalOperation()
        {
            if (!IsFaultMode)
            {
                BessCommand newCommand;
                double controlValue = 0;

                double maxPowerOutput = 0;
                BatteryStrategyEnum strategy = BatteryStrategyEnum.Standby;
                if (_isAutomaticMode)
                {
                    if (_hasDailyPatternEnabled)
                    {
                        if (_scheduler.NeedUpdate())
                        {
                            newCommand = _scheduler.GetNextOverride().Command;
                            controlValue = newCommand.Value;
                            strategy = newCommand.BatteryStrategy;
                        }
                        double netPowerInjection = ElectricityMeterApi.GetRealPowerTotal();

                        double pcsPower = PcsApi.PcsGetDcSidePower();
                        double load = netPowerInjection + pcsPower;
                        double tolerance = _thresholdTolerance;

                        //获取需量控制参数
                        StrategyApi.GetMaxDemandThreshhold(out double capacity, out double demanddescendrate);
                        //获取逆转保护参数
                        StrategyApi.GetReversePowerThreshold(out double reversePowerflowProtectionThreshold,
                            out double reversePowerLowestThreshold, out double reversePowerDescendRate);
                        if (_hasReversePowerflowProtectionEnabled && (strategy == BatteryStrategyEnum.ConstantCurrentDischarge || strategy == BatteryStrategyEnum.ConstantPowerDischarge))
                        {
                            maxPowerOutput = load - reversePowerflowProtectionThreshold * (1 + tolerance);
                            controlValue = Math.Min(controlValue, maxPowerOutput);
                            if (controlValue < 0)
                            {
                                controlValue *= -1;
                                strategy = BatteryStrategyEnum.ConstantPowerCharge;
                            }
                        }
                        else if (_hasMaxDemandControlEnabled && (strategy == BatteryStrategyEnum.ConstantCurrentCharge || strategy == BatteryStrategyEnum.ConstantPowerCharge))
                        {
                            maxPowerOutput = (capacity - load);
                            maxPowerOutput = maxPowerOutput > 0 ? maxPowerOutput * (1 - tolerance) : maxPowerOutput * (1 + tolerance);
                            controlValue = Math.Min(controlValue, maxPowerOutput);
                            if (controlValue < 0)
                            {
                                controlValue *= -1;
                                strategy = BatteryStrategyEnum.ConstantPowerDischarge;
                            }
                        }
                        else maxPowerOutput = controlValue;


                        controlValue = Math.Min(controlValue, maxPowerOutput);
                        newCommand = ContingencyAdjustment(new BessCommand(controlValue, strategy));
                        if (newCommand != _currentCommand)
                        {
                            _currentCommand = newCommand;
                            PcsApi.SendPcsCommand(newCommand);
                        }
                    }
                }
            }
            else
            {
                if (_manualCommand != _currentCommand)
                {
                    _currentCommand = _manualCommand;
                    PcsApi.SendPcsCommand(_manualCommand);
                }
            }
        }

        public void BmsConnect2DcBus(List<string> bcmuIds)
        {
            double chargingPower = 0;
            BatteryStrategyEnum strategy = BatteryStrategyEnum.Standby;
            if (!PcsApi.IsPcsNormal()) throw new Exception("PCS处于故障状态无法并网"); // 检查PCS状态，确保PCS通信连接，无故障，
            int numClusters = bcmuIds.Count;// 得到目前有几簇电池
            List<Tuple<double, string>> voltageBcmuIdPairs = new List<Tuple<double, string>>();
            foreach (string bcmuId in bcmuIds)
            {
                double voltage = BmsApi.GetNextBMSData(bcmuId).TotalVoltage; // 得到每一簇的电压
                Tuple<double, string> voltageBcmuIdPair = new Tuple<double, string>(voltage, bcmuId); // 将电压和bcmuId以Tuple的形式组成List
                voltageBcmuIdPairs.Add(voltageBcmuIdPair);
            }
            voltageBcmuIdPairs.Sort((x, y) => x.Item1.CompareTo(y.Item1));// 将电压从低到高排序
            for (int i = 0; i < numClusters - 1; i++)
            {
                chargingPower = 0;
                strategy = BatteryStrategyEnum.Standby;
                PcsApi.SendPcsCommand(new BessCommand(chargingPower, strategy)); // 将PCS设置成待机状态。
                string currentBcmuId = voltageBcmuIdPairs[i].Item2;
                BmsApi.Connect2DcBus(currentBcmuId);// 将当前簇并网
                string nextBcmuId = voltageBcmuIdPairs[i + 1].Item2;
                strategy = BatteryStrategyEnum.ConstantPowerCharge; //设定并网充电方式为恒功率充电
                chargingPower = BmsApi.GetNormalPowerCapacity() * _dcBusConnectionChargingPowerFactor; //计算并网充电功率
                PcsApi.SendPcsCommand(new BessCommand(chargingPower, strategy)); // 对PCS下发指令进行并网充电
                while (BmsApi.GetNextBMSData(currentBcmuId).TotalVoltage < BmsApi.GetNextBMSData(nextBcmuId).TotalVoltage) // 对其充电将DC母线电压增加到第二簇的水平
                {
                    Thread.Sleep(_controlPeriod);
                }

            }
            BmsApi.Connect2DcBus(voltageBcmuIdPairs[numClusters - 1].Item2);// 将最后一簇并网
            chargingPower = 0;
            strategy = BatteryStrategyEnum.Standby;
            PcsApi.SendPcsCommand(new BessCommand(chargingPower, strategy)); // 并网完成后将PCS恢复成待机状态
        }

        private BessCommand ContingencyAdjustment(BessCommand command)
        {
            switch (_contingencyStatus)
            {
                case ContingencyStatusEnum.Level1:
                    double discount = 1;
                    if (command.BatteryStrategy == BatteryStrategyEnum.ConstantCurrentCharge || command.BatteryStrategy == BatteryStrategyEnum.ConstantPowerCharge) discount = _chargingSecurityFactor;
                    else discount = _dischargingSecurityFactor;
                    return new BessCommand(command.Value * discount, command.BatteryStrategy);
                case ContingencyStatusEnum.Level2:
                    return new BessCommand(0, BatteryStrategyEnum.Standby);
                case ContingencyStatusEnum.Level3:
                    return new BessCommand(0, BatteryStrategyEnum.Standby);
            }
            return command;
        }

        private void ContingencyCheck()
        {
            _logger.Info("故障检测开始");
            if (!_hasContigencyCheckEnabled) return; //未启用则直接return
            ///获取全部故障告警
            bool bmsFault = false;
            int bmsAlarm = 0;
            var bmsResult = BmsApi.GetTotalAlarmInfo();
            bmsFault = bmsResult.Item2;
            bmsAlarm = bmsResult.Item1;

            List<string> pcsErrors = PcsApi.GetPCSFaultInfo();
            List<string> systemErrors = StrategyManager.Instance.GetSystemErrors();



            if (pcsErrors.Count == 0 && systemErrors.Count == 0 && (!bmsFault))
            {
                if (bmsAlarm == 0)
                {
                    _contingencyStatus = ContingencyStatusEnum.Normal; //全没故障
                    _logger.Info("当前轮故障检测没有发现任何异常情况，BMS,PCS,EMS均正常。");
                }
                else
                {
                    switch (bmsAlarm) //bmsAlarm有告警
                    {
                        case 1:
                            _contingencyStatus = ContingencyStatusEnum.Level1;
                            _logger.Info("BMS发生一级告警。");
                            break;
                        case 2:
                            _contingencyStatus = ContingencyStatusEnum.Level2;
                            _logger.Info("BMS发生二级告警。");
                            break;
                        case 3:
                            _contingencyStatus = ContingencyStatusEnum.Level3;
                            _logger.Info("BMS发生三级告警。");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("bmsAlarm数值异常。");
                    }
                }
            }
            else   //有故障
            {
                _contingencyStatus = ContingencyStatusEnum.Level3;
                _logger.Info("存在系统故障。");
            }
            BessCommand command = new BessCommand(0, BatteryStrategyEnum.Standby);
            switch (_contingencyStatus)
            {
                case ContingencyStatusEnum.Level2:
                    PcsApi.SendPcsCommand(command);//待机
                    break;
                case ContingencyStatusEnum.Level3:
                    PcsApi.SendPcsCommand(command);//待机
                    PcsApi.SetPCSHalt();//停机
                    break;
            }
            _logger.Info("故障检测完成");
        }

    }
    public enum ContingencyStatusEnum
    {
        [Description("正常运行")]
        Normal = 0,
        [Description("一级告警")]
        Level1 = 1,
        [Description("二级告警")]
        Level2 = 2,
        [Description("三级告警")]
        Level3 = 3,
    }
}
