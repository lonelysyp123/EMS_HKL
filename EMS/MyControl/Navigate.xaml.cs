using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Navigate.xaml 的交互逻辑
    /// </summary>
    public partial class Navigate : UserControl
    {
        public Navigate()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                MonitorViewMenu_SubBMS.Visibility = Visibility.Visible;
                MonitorViewMenu_SubPCS.Visibility = Visibility.Visible;
                MonitorViewMenu_SubSM.Visibility = Visibility.Visible;
            }
            else
            {
                MonitorViewMenu_SubBMS.Visibility = Visibility.Collapsed;
                MonitorViewMenu_SubPCS.Visibility = Visibility.Collapsed;
                MonitorViewMenu_SubSM.Visibility = Visibility.Collapsed;
            }
        }

        private void MonitorViewMenu_SubBMS_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                BMS_SubBMU1.Visibility = Visibility.Visible;
                BMS_SubBMU2.Visibility = Visibility.Visible;
                BMS_SubBMU3.Visibility = Visibility.Visible;
            }
            else
            {
                BMS_SubBMU1.Visibility = Visibility.Collapsed;
                BMS_SubBMU2.Visibility = Visibility.Collapsed;
                BMS_SubBMU3.Visibility = Visibility.Collapsed;
            }
        }

        private void AnalysisViewMenu_SubBMS_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AnalysisViewMenu_SubPCS_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AnalysisViewMenu_SubSmartMeter_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void StrategyViewMenu_Setter_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void StrategyViewMenu_Analysis_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AnalysisViewMenu_ProtectSetter_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
