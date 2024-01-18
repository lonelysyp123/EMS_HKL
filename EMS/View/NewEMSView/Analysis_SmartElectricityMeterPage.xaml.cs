﻿using EMS.ViewModel;
using EMS.ViewModel.NewEMSViewModel;
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

namespace EMS.View.NewEMSView
{
    public partial class Analysis_SmartElectricityMeterPage : Page
    {
        public Analysis_SmartElectricityMeterPage(Analysis_SmartElectricityMeterPageModel viewmodel)
        {
            InitializeComponent();
            viewmodel = new Analysis_SmartElectricityMeterPageModel();
            this.DataContext = viewmodel;
        }
    }
}