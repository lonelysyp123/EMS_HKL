using EMS.Model;
using EMS.ViewModel.NewEMSViewModel;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private double _originalWidth; // 假设原始设计宽度
        private double _originalHeight; // 假设原始设计高度


        private EMSMainViewModel viewmodel;
        public EMSMainView()
        {
            InitializeComponent();
            viewmodel = new EMSMainViewModel();
            Navigation(new HomePage(viewmodel.HomePageModel), null);

            // 初始化原始设计尺寸
            _originalWidth = 1920;
            _originalHeight = 1080;
        }

        private void Navigation(Page page, ToggleButton button, ToggleButton parent = null, ToggleButton grandpa = null)
        {
            UnCheckedOperation(button, parent, grandpa);
            PageContent.Content = page;
        }

        private void UnCheckedOperation(ToggleButton button, ToggleButton parent = null, ToggleButton grandpa = null)
        {
            foreach(var item in NavigationTool.Children)
            {
                if ((item as ToggleButton) != button && (item as ToggleButton) != parent && (item as ToggleButton) != grandpa)
                {
                    (item as ToggleButton).IsChecked = false;
                }
            }
        }

        private void HomeViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            //UnCheckedOperation(sender as ToggleButton);
            Navigation(new HomePage(viewmodel.HomePageModel), sender as ToggleButton);
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
            Navigation(new Monitor_BMSPage(viewmodel.Monitor_BMSPageModel), sender as ToggleButton);
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
            Navigation(new Monitor_BMSPage(viewmodel.Monitor_BMSPageModel), sender as ToggleButton, MonitorViewMenu);
        }
        private void BMS_SubBMU1_Checked(object sender, RoutedEventArgs e)
        {
            var menu = sender as ToggleButton;
            string str_index = menu.Name.Replace("BMS_SubBMU", "");
            int index = int.Parse(str_index);
            Navigation(new Monitor_BMS_BCMUPage(viewmodel.Monitor_BMSPageModel.bmuViewModels[index-1]), sender as ToggleButton, MonitorViewMenu_BMS, MonitorViewMenu);
        }

        private void MonitorViewMenu_PCS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_PCSPage(viewmodel.Monitor_PCSPageModel), sender as ToggleButton, MonitorViewMenu);
        }

        private void MonitorViewMenu_SM_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_SmartMeterPage(viewmodel.Monitor_SmartMeterPageModel), sender as ToggleButton, MonitorViewMenu);

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
            Navigation(new Analysis_BMSPage(), sender as ToggleButton);
        }

        private void FaultViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new FaultLogPage(viewmodel.FaultLogPageModel), sender as ToggleButton);
        }

        private void StrategyViewMenu_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                StrategyViewMenu_Setter.Visibility = Visibility.Visible;
                StrategyViewMenu_Analysis.Visibility = Visibility.Visible;
                StrategyViewMenu_ProtectSetter.Visibility = Visibility.Visible;
            }
            else
            {
                StrategyViewMenu_Setter.Visibility = Visibility.Collapsed;
                StrategyViewMenu_Analysis.Visibility = Visibility.Collapsed;
                StrategyViewMenu_ProtectSetter.Visibility = Visibility.Collapsed;
            }
            Navigation(new Strategy_SetterPage(viewmodel.Strategy_SetterPageModel), sender as ToggleButton);
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
            Navigation(new System_DevInfoPage(viewmodel.System_DevInfoPageModel), sender as ToggleButton);
        }

        private void AnalysisViewMenu_BMS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_BMSPage(), sender as ToggleButton, AnalysisViewMenu);
        }

        private void AnalysisViewMenu_PCS_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_PCSPage(), sender as ToggleButton, AnalysisViewMenu);
        }

        private void AnalysisViewMenu_SmartMeter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Analysis_SmartMeterPage(), sender as ToggleButton, AnalysisViewMenu);
        }

        private void StrategyViewMenu_Setter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_SetterPage(viewmodel.Strategy_SetterPageModel), sender as ToggleButton, StrategyViewMenu);
        }

        private void StrategyViewMenu_Analysis_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_AnalysisPage(viewmodel.Strategy_AnalysisPageModel), sender as ToggleButton, StrategyViewMenu);
        }

        private void StrategyViewMenu_ProtectSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new Strategy_ProtectSetterPage(viewmodel.Strategy_ProtectSetterPageModel), sender as ToggleButton, StrategyViewMenu);
        }

        private void SystemViewMenu_DevInfo_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_DevInfoPage(viewmodel.System_DevInfoPageModel), sender as ToggleButton, SystemViewMenu);
        }

        private void SystemViewMenu_DevSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_DevSetterPage(viewmodel.System_DevSetterPageModel), sender as ToggleButton, SystemViewMenu);
        }

        private void SystemViewMenu_MqttSetter_Checked(object sender, RoutedEventArgs e)
        {
            Navigation(new System_MqttSetterPage(viewmodel.System_MqttSetterPageModel), sender as ToggleButton, SystemViewMenu);
        }

        //只展开一个下拉框
        private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)sender;
            toggleButton.Checked += ToggleButton_Checked;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
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
            StrategyViewMenu_ProtectSetter.Visibility = Visibility.Collapsed;
            SystemViewMenu_DevInfo.Visibility = Visibility.Collapsed;
            SystemViewMenu_DevSetter.Visibility = Visibility.Collapsed;
            SystemViewMenu_MqttSetter.Visibility = Visibility.Collapsed;
        }

        private void ToggleButton_LoadedForLevel2(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)sender;
            toggleButton.Unchecked += ToggleButton_UncheckedForLevel2;
        }

        private void ToggleButton_UncheckedForLevel2(object sender, RoutedEventArgs e)
        {
            BMS_SubBMU1.Visibility = Visibility.Collapsed;
            BMS_SubBMU2.Visibility = Visibility.Collapsed;
            BMS_SubBMU3.Visibility = Visibility.Collapsed;
            BMS_SubBMU4.Visibility = Visibility.Collapsed;
            BMS_SubBMU5.Visibility = Visibility.Collapsed;
            BMS_SubBMU6.Visibility = Visibility.Collapsed;
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
        //private ToggleButton _currentSelectedSubMenu = null;
        //private void SubMenuItem_Checked(object sender, RoutedEventArgs e)
        //{
        //    var button = (ToggleButton)sender;

        //    if (_currentSelectedSubMenu != null && _currentSelectedSubMenu != button)
        //    {
        //        // 重置之前选中的按钮颜色
        //        _currentSelectedSubMenu.Foreground = Brushes.White;
        //        _currentSelectedSubMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#405673"));
        //    }
        //    // 设置当前选中的按钮颜色为蓝色
        //    button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1809FF"));
        //    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
        //    _currentSelectedSubMenu = button;
        //}
    }
}
