﻿using EMS.Model;
using EMS.ViewModel;
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
using System.Windows.Shapes;

namespace EMS.View
{
    /// <summary>
    /// ElectricMeterView.xaml 的交互逻辑
    /// </summary>
    public partial class ElectricMeterView : Window
    {
        ElectricMeterViewModel viewmodel;
        public ElectricMeterView()
        {
            InitializeComponent();

            viewmodel = new ElectricMeterViewModel();
            EnergyManagementSystem.GlobalInstance.SmartMeterManager.AddDev(viewmodel);
            this.DataContext = viewmodel;
            CommConfiguaration.DataContext = viewmodel.Configuaration;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            EnergyManagementSystem.GlobalInstance.SmartMeterManager.RemoveDev(viewmodel);
            viewmodel.CloseSerialPort();
        }
    }
}
