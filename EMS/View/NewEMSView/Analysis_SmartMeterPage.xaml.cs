using EMS.ViewModel;
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
    public partial class Analysis_SmartMeterPage : Page
    {
        private Analysis_SmartMeterPageModel vm;
        public Analysis_SmartMeterPage()
        {
            InitializeComponent();

            vm = new Analysis_SmartMeterPageModel();
            this.DataContext = vm;
        }

        private void DataTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SwitchSmartMeterData();
        }
    }
}
