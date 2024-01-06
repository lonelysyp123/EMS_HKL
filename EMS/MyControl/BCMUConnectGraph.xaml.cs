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
using static EMS.MyControl.BCMUConnectGraph;

namespace EMS.MyControl
{
    /// <summary>
    /// BCMUConnectGraph.xaml 的交互逻辑
    /// </summary>
    public partial class BCMUConnectGraph : UserControl
    {
        public BCMUConnectGraph()
        {
            InitializeComponent();
        }

        #region Property
        //Visible_SwitchOn并网图标
        public Visibility Visible_SwitchOn
        {
            get { return (Visibility)GetValue(Visible_SwitchOnProperty); }
            set { SetValue(Visible_SwitchOnProperty, value); }
        }
        public static readonly DependencyProperty Visible_SwitchOnProperty =
            DependencyProperty.Register("Visible_SwitchOn", typeof(Visibility), typeof(BCMUConnectGraph), new PropertyMetadata(Visibility.Collapsed));

        //Visible_SwitchOff离网图标
        public Visibility Visible_SwitchOff
        {
            get { return (Visibility)GetValue(Visible_SwitchOffProperty); }
            set { SetValue(Visible_SwitchOffProperty, value); }
        }
        public static readonly DependencyProperty Visible_SwitchOffProperty =
            DependencyProperty.Register("Visible_SwitchOff", typeof(Visibility), typeof(BCMUConnectGraph), new PropertyMetadata(Visibility.Visible));

        //Visible_UpArrow上箭头图标
        public Visibility Visible_UpArrow
        {
            get { return (Visibility)GetValue(Visible_UpArrowProperty); }
            set { SetValue(Visible_UpArrowProperty, value); }
        }
        public static readonly DependencyProperty Visible_UpArrowProperty =
            DependencyProperty.Register("Visible_UpArrow", typeof(Visibility), typeof(BCMUConnectGraph), new PropertyMetadata(Visibility.Collapsed));

        //Visible_DownArrow下箭头图标
        public Visibility Visible_DownArrow
        {
            get { return (Visibility)GetValue(Visible_DownArrowProperty); }
            set { SetValue(Visible_DownArrowProperty, value); }
        }
        public static readonly DependencyProperty Visible_DownArrowProperty =
            DependencyProperty.Register("Visible_DownArrow", typeof(Visibility), typeof(BCMUConnectGraph), new PropertyMetadata(Visibility.Collapsed));

        //Alarmcolor告警颜色
        public Brush Alarmcolor
        {
            get { return (Brush)GetValue(AlarmcolorProperty); }
            set { SetValue(AlarmcolorProperty, value); }
        }
        public static readonly DependencyProperty AlarmcolorProperty =
            DependencyProperty.Register("Alarmcolor", typeof(Brush), typeof(BCMUConnectGraph), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5"))));

        //IsConnectColor离并网连接线颜色
        public Brush IsConnectColor
        {
            get { return (Brush)GetValue(IsConnectColorProperty); }
            set { SetValue(IsConnectColorProperty, value); }
        }
        public static readonly DependencyProperty IsConnectColorProperty =
            DependencyProperty.Register("IsConnectColor", typeof(Brush), typeof(BCMUConnectGraph), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D81E06"))));

        // 自定义属性：AlertLevel
        public FaultLevels AlarmtLevel
        {
            get { return (FaultLevels)GetValue(AlarmtLevelProperty); }
            set { SetValue(AlarmtLevelProperty, value); }
        }
        public static readonly DependencyProperty AlarmtLevelProperty =
            DependencyProperty.Register("AlarmtLevel", typeof(FaultLevels), typeof(BCMUConnectGraph), new PropertyMetadata(FaultLevels.NoAlarm, OnAlarmtLevelChanged));

        // 自定义属性：IsConnect
        public bool IsConnect
        {
            get { return (bool)GetValue(IsConnectProperty); }
            set { SetValue(IsConnectProperty, value); }
        }
        public static readonly DependencyProperty IsConnectProperty =
            DependencyProperty.Register("IsConnect", typeof(bool), typeof(BCMUConnectGraph), new PropertyMetadata(false, OnIsConnectChanged));

        // 自定义属性：BatteryClusterStatus
        public BCMUStatus BatteryClusterStatus
        {
            get { return (BCMUStatus)GetValue(BatteryClusterStatusProperty); }
            set { SetValue(BatteryClusterStatusProperty, value); }
        }
        public static readonly DependencyProperty BatteryClusterStatusProperty =
            DependencyProperty.Register("BatteryClusterStatus", typeof(BCMUStatus), typeof(BCMUConnectGraph), new PropertyMetadata(BCMUStatus.Stand, OnBatteryClusterStatusChanged));

        #endregion



        private static void OnAlarmtLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BCMUConnectGraph)d).OnAlarmtLevelChanged((FaultLevels)e.NewValue);
        }

        protected virtual void OnAlarmtLevelChanged(FaultLevels newValue)
        {
            UpdateColors_AlarmtLevels(newValue);
        }


        private static void OnIsConnectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BCMUConnectGraph)d).OnIsConnectChanged((bool)e.NewValue);
        }

        protected virtual void OnIsConnectChanged(bool newValue)
        {
            BCMUStatus status = BatteryClusterStatus;
            UpdateColors_IsConnect(newValue, status);
        }

        private static void OnBatteryClusterStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BCMUConnectGraph)d).OnBatteryClusterStatusChanged((BCMUStatus)e.NewValue);
        }

        protected virtual void OnBatteryClusterStatusChanged(BCMUStatus bCMUStatus)
        {
            if (IsConnect)
            {
                UpdateColors_IsConnect(IsConnect, bCMUStatus);
            }
        }

        /// <summary>
        /// 告警级别
        /// </summary>
        /// <param name="alarmtLevel"></param>
        private void UpdateColors_AlarmtLevels(FaultLevels alarmtLevel)
        {
            switch (alarmtLevel)
            {
                case FaultLevels.NoAlarm:
                    Alarmcolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5"));
                    break;
                case FaultLevels.Info:
                    Alarmcolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE94F"));
                    break;
                case FaultLevels.Warning:
                    Alarmcolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                    break;
                case FaultLevels.Error:
                    Alarmcolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE6666"));
                    break;
                default:
                    Alarmcolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5"));
                    break;
            }
        }

        /// <summary>
        /// 告警
        /// </summary>
        public enum FaultLevels
        {
            NoAlarm,
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// 离并网
        /// </summary>
        /// <param name="newValue"></param>
        private void UpdateColors_IsConnect(bool IsConnect, BCMUStatus bCMUStatus)
        {
            if (IsConnect)
            {
                IsConnectColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009B0A"));
                Visible_SwitchOn = Visibility.Visible;
                Visible_SwitchOff = Visibility.Collapsed;

                switch (bCMUStatus)
                {
                    case BCMUStatus.Discharge:
                        Visible_UpArrow = Visibility.Visible;
                        Visible_DownArrow = Visibility.Collapsed;
                        break;
                    case BCMUStatus.Charge:
                        Visible_UpArrow = Visibility.Collapsed;
                        Visible_DownArrow = Visibility.Visible;
                        break;
                    case BCMUStatus.Stand:
                        Visible_UpArrow = Visibility.Collapsed;
                        Visible_DownArrow = Visibility.Collapsed;
                        break;
                    default:
                        Visible_UpArrow = Visibility.Collapsed;
                        Visible_DownArrow = Visibility.Collapsed;
                        break;
                }
            }
            else
            {
                IsConnectColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D81E06"));
                Visible_SwitchOn = Visibility.Collapsed;
                Visible_SwitchOff = Visibility.Visible;
            }
        }

        public enum BCMUStatus
        {
            Discharge,
            Charge,
            Stand,
        }

        //控件绑定值
        //AlarmtLevel:NoAlarm、Info、Warning、Error；
        //IsConnect:True、False；
        //BatteryClusterStatus：Charge、Discharge、Stand
    }
}
