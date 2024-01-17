using EMS.ViewModel.NewEMSViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using EMS.ViewModel;
using EMS.ViewModel.NewEMSViewModel;

namespace EMS.View.NewEMSView
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor_PCSPage : Page
    {
        Monitor_PCSPageModel viewModel;
        
        public Monitor_PCSPage()
        {
            InitializeComponent();

            viewModel = new Monitor_PCSPageModel();
            this.DataContext = viewModel;

            // 初始化数据源
            //DCDataGrid.ItemsSource = Items;
        }

        public Monitor_PCSPage(Monitor_PCSPageModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }
    }

    public class Item
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}