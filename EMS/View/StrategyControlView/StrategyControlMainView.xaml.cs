using EMS.Properties;
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
    /// StrategyControl.xaml 的交互逻辑
    /// </summary>
    public partial class StrategyControl : Window
    {
        public class MonthList
        {
            public string Month { get; set; }
        }
        public class Setting
        {
            public string timeTemplates { get; set; }
        }
        public class Price
        {
            public string elePriceTemplates { get; set; }
        }
        public StrategyControl()
        {
            InitializeComponent();

            List<MonthList> test = new List<MonthList>();
            test.Add(new MonthList() { Month = "1月" });
            test.Add(new MonthList() { Month = "2月" });
            test.Add(new MonthList() { Month = "3月" });
            test.Add(new MonthList() { Month = "4月" });
            test.Add(new MonthList() { Month = "5月" });
            test.Add(new MonthList() { Month = "6月" });
            test.Add(new MonthList() { Month = "7月" });
            test.Add(new MonthList() { Month = "8月" });
            test.Add(new MonthList() { Month = "9月" });
            test.Add(new MonthList() { Month = "10月" });
            test.Add(new MonthList() { Month = "11月" });
            test.Add(new MonthList() { Month = "12月" });
            elePriceGrid.ItemsSource = test;
        }

        private void SelectedTimeTemplates(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox == null) return;

            var Setting = combobox.SelectedItem as Setting;
            if (Setting == null) return;
        }

        private void SelectedElePriceTemplates(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox == null) return;

            var Price = combobox.SelectedItem as Price;
            if (Price == null) return;
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void elePriceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
