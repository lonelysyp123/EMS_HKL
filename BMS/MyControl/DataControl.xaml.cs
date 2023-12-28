using BMS.Common.Modbus.ModbusTCP;
using BMS.Model;
using BMS.View;
using BMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace BMS.MyControl
{
    /// <summary>
    /// DataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataControl : UserControl
    {
        public DataControl(BatteryTotalViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SeriesBatteryView view = new SeriesBatteryView((BatteryTotalViewModel)this.DataContext);
            view.ShowDialog();
        }
    }
}
