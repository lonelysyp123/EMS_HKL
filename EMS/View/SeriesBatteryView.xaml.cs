using EMS.Model;
using EMS.MyControl;
using EMS.ViewModel;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// SeriesBatteryView.xaml 的交互逻辑
    /// </summary>
    public partial class SeriesBatteryView : Window
    {
        public SeriesBatteryView(BatteryTotalViewModel viewmodel)
        {
            InitializeComponent();

            this.DataContext = viewmodel;
            InitView(viewmodel);
        }

        private void InitView(BatteryTotalViewModel item)
        {
            for (int i = 0; i < item.batterySeriesViewModelList.Count; i++)
            {
                Grid grid;
                Grid gridb;
                if (i == 0)
                {
                    grid = BMUA;
                    gridb = BMUA_Battery;
                }
                else if (i == 1)
                {
                    grid = BMUB;
                    gridb = BMUB_Battery;
                }
                else
                {
                    grid = BMUC;
                    gridb = BMUC_Battery;
                }
                for (int l = 0;l < item.batterySeriesViewModelList[i].BatteryViewModelList.Count; l++)
                {
                    Battery battery = new Battery();
                    Grid.SetRow(battery, l/7);
                    Grid.SetColumn(battery, l%7);
                    battery.Margin = new Thickness(5);
                    Binding binding = new Binding() { Path = new PropertyPath("SOC")};
                    battery.SetBinding(Battery.SOCProperty, binding);
                    battery.DataContext = item.batterySeriesViewModelList[i].BatteryViewModelList[l];                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                    gridb.Children.Add(battery);
                }
                grid.DataContext = item.batterySeriesViewModelList[i];
            }
        }
    }
}
