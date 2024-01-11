using EMS.Model;
using EMS.MyControl;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using EMS.View;
using EMS.ViewModel;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EMS
{
    /// <summary>
    /// MainWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private PCSSettingViewModel pcsviewmodel;
        private MainViewModel viewmodel;
        DevTest_CollectView devTest_Daq;
        DataAnalysis_OptimizeView dataAnalysis_Optimize;
        DevControlView devControlView;
        ParameterSettingView parameterSettingView;
        
        PCSMainViewModel pCSMainViewModel;
        StrategyControl strategyControlMainView;
        PCSModel pcsModel;
        public MainWindow()
        {
            InitializeComponent();
            //初始化log配置文件
            XmlConfigurator.Configure();

            EnergyManagementSystem.Initialization(new EnergyManagementSystem());

            viewmodel = new MainViewModel();
            pCSMainViewModel = new PCSMainViewModel();
            pcsModel = new PCSModel();

            this.DataContext = viewmodel;
            DevListView.DataContext = viewmodel.DisplayContent;
            DaqDataRaBtn.IsChecked = true;

            PCS_ConncetState.DataContext = pCSMainViewModel;
            PCS_ConnectColor.DataContext = pCSMainViewModel;
            PCS_IP.DataContext = pCSMainViewModel;
            SelectedPage("DaqDataRaBtn");
            EnergyManagementSystem.GlobalInstance.Initialization(null, null, null, null, null);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                case "StrategyControlRaBtn":
                    if (strategyControlMainView == null)
                    {
                        strategyControlMainView = new StrategyControl();

                    }
                    //simulationSettingView.SyncContent(viewmodel.DisplayContent.OnlineBatteryTotalList.ToList(), viewmodel.DisplayContent.ClientList);
                    Mainbody.Content = new Frame() { Content = strategyControlMainView };
                    break;
                default:
                    break;
            }
        }

        private void OperationManual_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "About");
            string filePath = System.IO.Path.Combine(folderPath, "OperationManual.pdf");
            System.Diagnostics.Process.Start(filePath);
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            view.ShowDialog();
        }

        private void OpenPCSWindow_Click(object sender, RoutedEventArgs e)
        {
            PCSMainWindow mainwindow = new PCSMainWindow(pCSMainViewModel);
            mainwindow.Show();
        }

        private void DevList_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if(e.OriginalSource.GetType() == typeof(ScrollViewer))
            //{
            //    RemoveMenu.Visibility = Visibility.Collapsed;
            //    ConnectMenu.Visibility = Visibility.Collapsed;
            //    DisconnectMenu.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    RemoveMenu.Visibility = Visibility.Visible;
            //    ConnectMenu.Visibility = Visibility.Visible;
            //    DisconnectMenu.Visibility = Visibility.Visible;
            //}
            //e.Handled = true;
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
