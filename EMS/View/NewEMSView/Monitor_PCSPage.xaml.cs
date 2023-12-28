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

namespace EMS.View.NewEMSView
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor_PCSPage : Page

    {
        public ObservableCollection<Item> Items { get; set; }
        public Monitor_PCSPage()
        {
            InitializeComponent();

            // 初始化数据源
            Items = new ObservableCollection<Item>
            {
                new Item { OrderNumber = 1, Name = "DC模块组：模块1", Status = "在线" },
                new Item { OrderNumber = 2, Name = "DC模块组：模块2", Status = "运行" },
                new Item { OrderNumber = 3, Name = "DC模块组：模块3", Status = "告警" },
                new Item { OrderNumber = 4, Name = "DC模块组：模块4", Status = "故障" },
                new Item { OrderNumber = 5, Name = "DC模块组：模块5", Status = "离线" },
                new Item { OrderNumber = 6, Name = "DC模块组：模块6", Status = "在线" },
                new Item { OrderNumber = 7, Name = "DC模块组：模块7", Status = "运行" },
                new Item { OrderNumber = 8, Name = "DC模块组：模块8", Status = "告警" },
                new Item { OrderNumber = 9, Name = "DC模块组：模块9", Status = "故障" },
                new Item { OrderNumber = 10, Name = "DC模块组：模块10", Status = "离线" }
            };

            DCDataGrid.ItemsSource = Items;
        }
    }

    public class Item
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}