using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Common.StrategyManage
{
    public class EmsController
    {
        private bool _isAutomaticMode;
        private bool _isFaultMode;
        private bool _hasDailyPatternEnabled;
        private bool _hasMaxDemandControlEnabled;
        private bool _hasReversePowerflowProtectionEnabled;
        private bool _hasContigencyCheckEnabled;
        private BessCommand _currentCommand;
        private IntraDayScheduler _scheduler;
        private ContingencyStatusEnum _contingencyStatus;
        private DateTime _lastActiveTimestamp; // used to indicate the system operation thread is still alive.

        public bool IsFaultMode { get {return this._contingencyStatus == ContingencyStatusEnum.Level2 || this._contingencyStatus == ContingencyStatusEnum.Level3; } }
        public EmsController()
        {
            _isAutomaticMode = false;
            _hasDailyPatternEnabled = false;
            _hasMaxDemandControlEnabled = false;
            _hasReversePowerflowProtectionEnabled = false;
            _hasContigencyCheckEnabled = true;
            _currentCommand = null;
            _scheduler = new IntraDayScheduler();
            _contingencyStatus = ContingencyStatusEnum.Normal;

        }

        public void ContinueOperation()
        {
            ContingencyCheck();
            NormalOperation();
            
            _lastActiveTimestamp = DateTime.Now;
            Thread.Sleep(StrategyManager.Instance.GetSystemSamplePeriod());
        }
        private void NormalOperation()
        {
            if(!IsFaultMode)
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
                        double netPowerInjection = StrategyManager.Instance.GetACSmartMeterPower();
                        double reversePowerThreshold = StrategyManager.Instance.GetReversePowerThreshold();
                        double pcsPower = StrategyManager.Instance.GetPcsPower();
                        double load = netPowerInjection + pcsPower;
                        double tolerance = StrategyManager.Instance.GetAutomaticControlTolerance();
                        double capacity = StrategyManager.Instance.GetTransformerCapacity();

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
                            StrategyManager.Instance.SendPcsCommand(newCommand);
                        }
                    }
                }
            }
            else
            {
                BessCommand manualCommand = StrategyManager.Instance.GetManualCommand();
                if (manualCommand != _currentCommand)
                {
                    _currentCommand = manualCommand;
                    StrategyManager.Instance.SendPcsCommand(manualCommand);
                }
            }


        }

        private BessCommand ContingencyAdjustment(BessCommand command)
        {
            switch (_contingencyStatus)
            {
                case ContingencyStatusEnum.Level1:
                    double discount = 1;
                    if (command.BatteryStrategy == BatteryStrategyEnum.ConstantCurrentCharge || command.BatteryStrategy == BatteryStrategyEnum.ConstantPowerCharge) discount = StrategyManager.Instance.GetChargingDiscount();
                    else discount = StrategyManager.Instance.GetDischargingDiscount();
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
            if (!_hasContigencyCheckEnabled) return; //未启用则直接return
            ///获取全部故障告警
            List<string>bmsErrors = StrategyManager.Instance.GetBMSAlarmandFaultInfo();
            List<string>pcsErrors = StrategyManager.Instance.GetPCSFaultInfo();
            List<string>systemErrors=StrategyManager.Instance.GetSystemErrors();
            
            List<int> levels = new List<int>();//等级数组
                ///如果PCS没故障
                if (pcsErrors.Count == 0&&systemErrors.Count==0)
                {
                    if (bmsErrors.Count > 0)
                    {                 
                        foreach (var error in bmsErrors)
                        {
                            if (error.Contains("异常") && (error.Contains("三级保护")))
                            {
                                levels.Add(3);
                                StrategyManager.Instance.SetPCSHalt();
                            }
                            else if (error.Contains("二级保护"))
                            { 
                                BessCommand bessCommand = new BessCommand(0, BatteryStrategyEnum.Standby);
                                StrategyManager.Instance.SendPcsCommand(bessCommand);
                                levels.Add(2);
                            }
                            else if (error.Contains("一级保护"))
                            {
                                levels.Add(1);
                            }
                        }
                    }                   
                }
                else //如果pcs有故障
                {
                    levels.Add(3);
                }
            _contingencyStatus = (ContingencyStatusEnum)(levels.Max());
            BessCommand command = new BessCommand(0, BatteryStrategyEnum.Standby);
            switch (_contingencyStatus)
            {
                case ContingencyStatusEnum.Level2:
                    StrategyManager.Instance.SendPcsCommand(command);//待机
                    break;
                case ContingencyStatusEnum.Level3:
                    StrategyManager.Instance.SendPcsCommand(command);//待机
                    StrategyManager.Instance.SetPCSHalt();//停机
                    break;
            }

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
