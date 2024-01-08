using EMS.MyControl;
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
    public partial class Monitor_BMS_BCMUPage : Page
    {
        public Monitor_BMS_BCMUPage()
        {
            InitializeComponent();
        }

        public Monitor_BMS_BCMUPage(Monitor_BMS_BCMUPageModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
            InitView(viewmodel);
        }

        private void InitView(Monitor_BMS_BCMUPageModel viewmodel)
        {
            for (int l = 0; l < 14; l++)
            {
                Battery battery = new Battery();
                Grid.SetRow(battery, l / 7 + 1);
                Grid.SetColumn(battery, l % 7 + 1);
                
                battery.Margin = new Thickness(5);
                Binding binding = new Binding() { Path = new PropertyPath("SOC") };
                battery.SetBinding(Battery.SOCProperty, binding);
                battery.DataContext = viewmodel.BatteryViewModelList[l];
                BMU_Battery.Children.Add(battery);
            }
        }
    }
}
