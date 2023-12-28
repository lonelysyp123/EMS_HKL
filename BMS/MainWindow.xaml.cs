using BMS.Model;
using BMS.MyControl;
using BMS.Storage.DB.DBManage;
using BMS.Storage.DB.Models;
using BMS.View;
using BMS.ViewModel;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BMS
{
    /// <summary>
    /// MainWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewmodel;
        DevTest_CollectView devTest_Daq;
        DataAnalysis_OptimizeView dataAnalysis_Optimize;
        DevControlView devControlView;
        ParameterSettingView parameterSettingView;
        public MainWindow()
        {
            InitializeComponent();
            //初始化log配置文件
            XmlConfigurator.Configure();

            viewmodel = new MainViewModel();

            this.DataContext = viewmodel;
            DevListView.DataContext = viewmodel.DisplayContent;
            DaqDataRaBtn.IsChecked = true;
            SelectedPage("DaqDataRaBtn");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            SelectedPage(radioButton.Name);
        }

        private void SelectedPage(string PageName)
        {
            switch (PageName)
            {
                case "DaqDataRaBtn":
                    if (devTest_Daq == null)
                    {
                        devTest_Daq = new DevTest_CollectView();
                        devTest_Daq.InitView(viewmodel.DisplayContent.BatteryTotalViewModelList);
                        viewmodel.DisplayContent.BatteryTotalViewModelList.CollectionChanged += devTest_Daq.Test_CollectionChanged;
                    }
                    Mainbody.Content = new Frame() { Content = devTest_Daq };
                    break;
                case "AnalysisDataRaBtn":
                    if (dataAnalysis_Optimize == null)
                    {
                        dataAnalysis_Optimize = new DataAnalysis_OptimizeView();
                    }
                    Mainbody.Content = new Frame() { Content = dataAnalysis_Optimize };
                    break;
                case "ControlRaBtn":
                    if (devControlView == null)
                    {
                        devControlView = new DevControlView();

                    }
                    devControlView.SyncContent(viewmodel.DisplayContent.BatteryTotalViewModelList.ToList());
                    Mainbody.Content = new Frame() { Content = devControlView };
                    break;

                case "ValueSettingRaBtn":
                    if (parameterSettingView == null)
                    {
                        parameterSettingView = new ParameterSettingView();
                    }
                    parameterSettingView.SyncContent(viewmodel.DisplayContent.BatteryTotalViewModelList.ToList());
                    Mainbody.Content = new Frame() { Content = parameterSettingView };
                    break;
                default:
                    break;
            }
        }

        bool isSelectedItem = false;
        private void DevList_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(ScrollViewer))
            {
                if (!isSelectedItem) 
                {
                    isSelectedItem = true;
                    RemoveMenu.Visibility = Visibility.Collapsed;
                    ConnectMenu.Visibility = Visibility.Collapsed;
                    DisconnectMenu.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (isSelectedItem)
                {
                    isSelectedItem = false;
                    RemoveMenu.Visibility = Visibility.Visible;
                    ConnectMenu.Visibility = Visibility.Visible;
                    DisconnectMenu.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
