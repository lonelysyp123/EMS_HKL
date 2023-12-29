using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace EMS.View.NewEMSView
{
    /// <summary>
    /// EMSMainView.xaml 的交互逻辑
    /// </summary>
    public partial class EMSMainView : System.Windows.Window
    {
        public EMSMainView()
        {
            InitializeComponent();
        }


        private Page CurrentPage
        {
            get => PageContent.Content as Page;
            set
            {
                PageContent.Content = value;
            }
        }
        private void Navigation(Page page)
        {
            CurrentPage = page;
        }


        private void HomeViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new HomePage());
        }

        private void MonitorViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                MonitorViewMenu_BMS.Visibility = Visibility.Visible;
                MonitorViewMenu_PCS.Visibility = Visibility.Visible;
                MonitorViewMenu_SM.Visibility = Visibility.Visible;
            }
            else
            {
                MonitorViewMenu_BMS.Visibility = Visibility.Collapsed;
                MonitorViewMenu_PCS.Visibility = Visibility.Collapsed;
                MonitorViewMenu_SM.Visibility = Visibility.Collapsed;
            }
            Navigation(new Monitor_BMSPage());
        }

        private void MonitorViewMenu_BMS_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                BMS_SubBMU1.Visibility = Visibility.Visible;
                BMS_SubBMU2.Visibility = Visibility.Visible;
                BMS_SubBMU3.Visibility = Visibility.Visible;
                BMS_SubBMU4.Visibility = Visibility.Visible;
                BMS_SubBMU5.Visibility = Visibility.Visible;
                BMS_SubBMU6.Visibility = Visibility.Visible;
            }
            else
            {
                BMS_SubBMU1.Visibility = Visibility.Collapsed;
                BMS_SubBMU2.Visibility = Visibility.Collapsed;
                BMS_SubBMU3.Visibility = Visibility.Collapsed;
                BMS_SubBMU4.Visibility = Visibility.Collapsed;
                BMS_SubBMU5.Visibility = Visibility.Collapsed;
                BMS_SubBMU6.Visibility = Visibility.Collapsed;
            }
            Navigation(new Monitor_BMSPage());
        }
        private void BMS_SubBMU1_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_BMS_BCMUPage());
        }

        private void MonitorViewMenu_PCS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_PCSPage());
        }

        private void MonitorViewMenu_SM_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_SmartMeterPage());

        }

        private void AnalysisViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                AnalysisViewMenu_BMS.Visibility = Visibility.Visible;
                AnalysisViewMenu_PCS.Visibility = Visibility.Visible;
                AnalysisViewMenu_SmartMeter.Visibility = Visibility.Visible;
            }
            else
            {
                AnalysisViewMenu_BMS.Visibility = Visibility.Collapsed;
                AnalysisViewMenu_PCS.Visibility = Visibility.Collapsed;
                AnalysisViewMenu_SmartMeter.Visibility = Visibility.Collapsed;
            }
            Navigation(new Analysis_BMSPage());
        }

        private void FaultViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new FaultLogPage());
        }

        private void StrategyViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                StrategyViewMenu_Setter.Visibility = Visibility.Visible;
                StrategyViewMenu_Analysis.Visibility = Visibility.Visible;
                AnalysisViewMenu_ProtectSetter.Visibility = Visibility.Visible;
            }
            else
            {
                StrategyViewMenu_Setter.Visibility = Visibility.Collapsed;
                StrategyViewMenu_Analysis.Visibility = Visibility.Collapsed;
                AnalysisViewMenu_ProtectSetter.Visibility = Visibility.Collapsed;
            }
            Navigation(new Strategy_SetterPage());
        }

        private void SystemViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                SystemViewMenu_DevInfo.Visibility = Visibility.Visible;
                SystemViewMenu_DevSetter.Visibility = Visibility.Visible;
                SystemViewMenu_MqttSetter.Visibility = Visibility.Visible;
            }
            else
            {
                SystemViewMenu_DevInfo.Visibility = Visibility.Collapsed;
                SystemViewMenu_DevSetter.Visibility = Visibility.Collapsed;
                SystemViewMenu_MqttSetter.Visibility = Visibility.Collapsed;
            }
            Navigation(new Strategy_SetterPage());
        }

        private void AnalysisViewMenu_BMS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_BMSPage());
        }

        private void AnalysisViewMenu_PCS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_PCSPage());
        }

        private void AnalysisViewMenu_SmartMeter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_SmartMeterPage());
        }

        private void StrategyViewMenu_Setter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_SetterPage());
        }

        private void StrategyViewMenu_Analysis_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_AnalysisPage());
        }

        private void AnalysisViewMenu_ProtectSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_ProtectSetterPage());
        }

        private void SystemViewMenu_DevInfo_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_DevInfoPage());
        }

        private void SystemViewMenu_DevSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_DevSetterPage());
        }

        private void SystemViewMenu_MqttSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_MqttSetterPage());
        }

        //只展开一个下拉框
        private List<ToggleButton> _toggleButtons = new List<ToggleButton>();
        private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)sender;
            _toggleButtons.Add(toggleButton);
            toggleButton.Checked += ToggleButton_Checked;
            toggleButton.Unchecked += ToggleButton_Unchecked;
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            
            foreach (var toggleButton in _toggleButtons)
            {
                if (toggleButton != sender)
                {
                    toggleButton.IsChecked = false;
                    MonitorViewMenu_BMS.Visibility = Visibility.Collapsed;
                    MonitorViewMenu_PCS.Visibility = Visibility.Collapsed;
                    MonitorViewMenu_SM.Visibility = Visibility.Collapsed;
                    BMS_SubBMU1.Visibility = Visibility.Collapsed;
                    BMS_SubBMU2.Visibility = Visibility.Collapsed;
                    BMS_SubBMU3.Visibility = Visibility.Collapsed;
                    BMS_SubBMU4.Visibility = Visibility.Collapsed;
                    BMS_SubBMU5.Visibility = Visibility.Collapsed;
                    BMS_SubBMU6.Visibility = Visibility.Collapsed;
                    AnalysisViewMenu_BMS.Visibility = Visibility.Collapsed;
                    AnalysisViewMenu_PCS.Visibility = Visibility.Collapsed;
                    AnalysisViewMenu_SmartMeter.Visibility = Visibility.Collapsed;
                    StrategyViewMenu_Setter.Visibility = Visibility.Collapsed;
                    StrategyViewMenu_Analysis.Visibility = Visibility.Collapsed;
                    AnalysisViewMenu_ProtectSetter.Visibility = Visibility.Collapsed;
                    SystemViewMenu_DevInfo.Visibility = Visibility.Collapsed;
                    SystemViewMenu_DevSetter.Visibility = Visibility.Collapsed;
                    SystemViewMenu_MqttSetter.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // 不需要特别处理，因为当其他ToggleButton被选中时，这个ToggleButton会自动变为未选中状态
        }

        //退出
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //用户手册
        private void OperationManual_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "About");
            string filePath = System.IO.Path.Combine(folderPath, "OperationManual.pdf");
            System.Diagnostics.Process.Start(filePath);
        }

        //开发人员
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            view.ShowDialog();
        }


        //标题变色
        private ToggleButton _currentSelectedSubMenu = null;
        private void SubMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            var button = (ToggleButton)sender;

            if (_currentSelectedSubMenu != null && _currentSelectedSubMenu != button)
            {
                // 重置之前选中的按钮颜色
                _currentSelectedSubMenu.Foreground = Brushes.White;
                _currentSelectedSubMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#405673"));
            }
            // 设置当前选中的按钮颜色为蓝色
            button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1809FF"));
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
            _currentSelectedSubMenu = button;
        }
    }
}
