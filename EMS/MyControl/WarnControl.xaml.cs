using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.MyControl
{
    /// <summary>
    /// WarnControl.xaml 的交互逻辑
    /// </summary>
    public partial class WarnControl : UserControl
    {
        public WarnControl()
        {
            InitializeComponent();
        }

        // 自定义属性：AlertLevel
        public AlarmtLevels AlertLevel
        {
            get { return (AlarmtLevels)GetValue(AlertLevelProperty); }
            set { SetValue(AlertLevelProperty, value); }
        }
        public static readonly DependencyProperty AlertLevelProperty =
            DependencyProperty.Register("AlertLevel", typeof(AlarmtLevels), typeof(WarnControl), new PropertyMetadata(AlarmtLevels.NoAlarm, OnAlertLevelChanged));

        private static void OnAlertLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WarnControl)d).OnAlertLevelChanged((AlarmtLevels)e.NewValue);
        }

        protected virtual void OnAlertLevelChanged(AlarmtLevels newValue)
        {
            UpdateColors(newValue);
        }

        // 自定义属性：AlertText
        public string AlertText
        {
            get { return (string)GetValue(AlertTextProperty); }
            set { SetValue(AlertTextProperty, value); }
        }
        public static readonly DependencyProperty AlertTextProperty =
            DependencyProperty.Register("AlertText", typeof(string), typeof(WarnControl), new PropertyMetadata(""));

        // 自定义属性：BorderColor
        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(WarnControl), new PropertyMetadata(Brushes.Transparent));

        // 自定义属性：TextColor
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(WarnControl), new PropertyMetadata(Brushes.Black));

        // 自定义属性：BorderBackground
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(WarnControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F4"))));

        private void UpdateColors(AlarmtLevels alertLevel)
        {
            switch (alertLevel)
            {
                case AlarmtLevels.NoAlarm:
                    BorderColor = Brushes.Transparent;
                    BorderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F4"));
                    TextColor = Brushes.Black;
                    break;
                case AlarmtLevels.Info:
                    BorderBackground = Brushes.Transparent;
                    TextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE94F"));
                    BorderColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE94F"));
                    break;
                    //BorderBackground = Brushes.Transparent;
                    //TextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                    //BorderColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                    //break;
                case AlarmtLevels.Warning:
                    BorderBackground = Brushes.Transparent;
                    TextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                    BorderColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                    break;
                case AlarmtLevels.Error:
                    BorderBackground = Brushes.Transparent;
                    TextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE6666"));
                    BorderColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE6666"));
                    break;
                default:
                    BorderColor = Brushes.Transparent;
                    BorderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F4"));
                    TextColor = Brushes.Black;
                    break;
            }
        }
    }

    //public enum AlertLevels
    public enum AlarmtLevels
    {
        NoAlarm,
        Info,
        Warning,
        Error
    }
}

