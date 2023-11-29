using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.StrategyManage
{
    public class IntraDayScheduler
    {
        private List<BatteryStrategyModel> _dailyPattern;
        private int _currentOverridePointer;
        private bool _hasCrossing12AM;

        public IntraDayScheduler()
        {
            _currentOverridePointer = 0;
            _hasCrossing12AM = false;
            ResetPattern();
        }
        public BatteryStrategyModel GetNextOverride()
        {
            if (_currentOverridePointer == _dailyPattern.Count - 1) throw new Exception("_currentOverridePointer is pointing at the end of _dailyPattern.");
            if (!NeedUpdate()) throw new Exception("Currently, it doesn't need to update the BatteryStrategy.");
            _currentOverridePointer++;
            return _dailyPattern[_currentOverridePointer];
        }

        public bool NeedUpdate()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan crossing12AM = new TimeSpan(0, 1, 0);
            if (_hasCrossing12AM && now > crossing12AM) _hasCrossing12AM = false;
            if (_currentOverridePointer == _dailyPattern.Count - 1)
            {
                if (now < crossing12AM && !_hasCrossing12AM)
                {
                    _currentOverridePointer = 0; //reset the timestamp pionter to the beginning
                    _hasCrossing12AM = true;
                }
                return false;
            }
            if (_dailyPattern[_currentOverridePointer + 1].StartTime <= now) return true;
            return false;
        }

        private void ResetPattern()
        {
            List<BatteryStrategyModel> userDailyPattern = StrategyManager.Instance.GetDailyPattern();
            BatteryStrategyModel overrideAt12AM = userDailyPattern.Count == 0 ? new BatteryStrategyModel() : userDailyPattern.Last();
            overrideAt12AM.StartTime = TimeSpan.Zero;
            _dailyPattern.Clear();
            _dailyPattern.Append(overrideAt12AM);
            _dailyPattern.AddRange(userDailyPattern);

            for (int i = 0; i < _dailyPattern.Count - 1; i++) // check to make sure _dailyPattern is sorted.
            {
                if (_dailyPattern[i].StartTime > _dailyPattern[i + 1].StartTime) throw new Exception("GetDailyPattern()'s output should be sorted.");
            }
            _currentOverridePointer = 0; //reset the timestamp pionter to the beginning
        }
    }
}
