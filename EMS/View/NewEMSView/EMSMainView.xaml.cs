﻿using HandyControl.Controls;
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

        private void MonitorViewMenu_SubPCS_Click(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_PCSPage());
        }

        private void MonitorViewMenu_SubSM_Click(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_SmartMeterPage());
        }

        private void BMS_SubBMU1_Click(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_BMS_BCMUPage());
        }

        private void BMS_SubBMU2_Click(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_BMS_BCMUPage());
        }

        private void BMS_SubBMU3_Click(object sender, RoutedEventArgs e)
        {
            Navigation(new Monitor_BMS_BCMUPage());
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
            Navigation(new Monitor_BMSPage());
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
            Navigation(new Monitor_BMSPage());
        }
    }
}
