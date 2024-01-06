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

        private SolidColorBrush _temperature1Color;
        public SolidColorBrush Temperature1Color
        {
            get => _temperature1Color;
            set
            {
                SetProperty(ref _temperature1Color, value);
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

        private SolidColorBrush _temperature2Color;
        public SolidColorBrush Temperature2Color
        {
            get => _temperature2Color;
            set
            {
                SetProperty(ref _temperature2Color, value);
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
            VoltageColor = new SolidColorBrush(Colors.White);
            Temperature1Color = new SolidColorBrush(Colors.White);
            Temperature2Color = new SolidColorBrush(Colors.White);
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
            Temperature1Color = new SolidColorBrush(Colors.LightBlue);
        }

        public void MarkMaxTemperature()
        {
            Temperature1Color = new SolidColorBrush(Colors.Red);
        }
    }
}
