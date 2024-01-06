using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel
{
    public class BatteryViewModel : ViewModelBase
    {
        #region DependencyProperty
        private double _voltage;
        public double Voltage
        {
            get => _voltage;
            set
            {
                SetProperty(ref _voltage, value);
            }
        }

        private SolidColorBrush _voltageColor;
        public SolidColorBrush VoltageColor
        {
            get => _voltageColor;
            set
            {
                SetProperty(ref _voltageColor, value);
            }
        }

        private double _temperature1;
        public double Temperature1
        {
            get => _temperature1;
            set
            {
                SetProperty(ref _temperature1, value);
            }
        }

        private SolidColorBrush _temperatureColor;
        public SolidColorBrush TemperatureColor
        {
            get => _temperatureColor;
            set
            {
                SetProperty(ref _temperatureColor, value);
            }
        }

        private double _temperature2;
        public double Temperature2
        {
            get => _temperature2;
            set
            {
                SetProperty(ref _temperature2, value);
            }
        }

        private double _soc;
        public double SOC
        {
            get => _soc;
            set
            {
                SetProperty(ref _soc, value);
            }
        }

        private int _resistance;
        public int Resistance
        {
            get => _resistance;
            set
            {
                SetProperty(ref _resistance, value);
            }
        }

        private int _soh;
        public int SOH
        {
            get => _soh;
            set
            {
                SetProperty(ref _soh, value);
            }
        }


        private double _capacity;
        public double Capacity
        {
            get => _capacity;
            set
            {
                SetProperty(ref _capacity, value);
            }
        }

        private int _batteryNumber;
        public int BatteryNumber
        {
            get => _batteryNumber;
            set
            {
                SetProperty(ref _batteryNumber, value);
            }
        }

        #endregion

        public BatteryViewModel()
        {
            Voltage = 100;
        }

        public void MarkMinVoltage()
        {
            VoltageColor = new SolidColorBrush(Colors.LightBlue);
        }

        public void MarkMaxVoltage()
        {
            VoltageColor = new SolidColorBrush(Colors.Red);
        }

        public void MarkMinTemperature()
        {
            TemperatureColor = new SolidColorBrush(Colors.LightBlue);
        }

        public void MarkMaxTemperature()
        {
            TemperatureColor = new SolidColorBrush(Colors.Red);
        }
    }
}
