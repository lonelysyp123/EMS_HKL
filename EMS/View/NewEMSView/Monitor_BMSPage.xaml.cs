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
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor_BMSPage : Page
    {
        public Monitor_BMSPage(Monitor_BMSPageModel viewmodel)
        {
            InitializeComponent();

            this.DataContext = viewmodel;
            BMU1Flag.DataContext = viewmodel.bmuViewModels[0];
            BMU2Flag.DataContext = viewmodel.bmuViewModels[1];
            BMU3Flag.DataContext = viewmodel.bmuViewModels[2];
            BMU4Flag.DataContext = viewmodel.bmuViewModels[3];
            BMU5Flag.DataContext = viewmodel.bmuViewModels[4];
            BMU6Flag.DataContext = viewmodel.bmuViewModels[5];
            BMU1Info.DataContext = viewmodel.bmuViewModels[0];
            BMU2Info.DataContext = viewmodel.bmuViewModels[1];
            BMU3Info.DataContext = viewmodel.bmuViewModels[2];
            BMU4Info.DataContext = viewmodel.bmuViewModels[3];
            BMU5Info.DataContext = viewmodel.bmuViewModels[4];
            BMU6Info.DataContext = viewmodel.bmuViewModels[5];
        }

    }
}
