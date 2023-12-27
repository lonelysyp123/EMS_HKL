using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BMS.Model
{
    public class BatterySeriesModel
    {
        public string BMUID { get; set; }
        public int AlarmStateFlagBMU {  get; set; }
        public int FaultyStateFlagBMU { get; set; }
        public string ChargeChannelStateNumber {  get; set; }
        public ushort ChargeChannelState {  get; set; }
        public double ChargeCapacitySum { get; set; }
        public double MinVoltage {  get; set; }
        public int MinVoltageIndex {  get; set; }
        public double MaxVoltage {  get; set; }
        public int MaxVoltageIndex {  get; set; }
        public double MinTemperature {  get; set; }
        public int MinTemperatureIndex {  get; set; }
        public double MaxTemperature {  get; set; }
        public int MaxTemperatureIndex {  get; set; }

        public List<BatteryModel> Batteries { get; set; }

        public BatterySeriesModel(int count)
        {
            Batteries = new List<BatteryModel>();
            for (int i = 0; i < count; i++)
            {
                Batteries.Add(new BatteryModel());
            }
        }
    }
}
