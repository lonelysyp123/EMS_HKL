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

namespace EMS.View.StrategyControlView
{
    /// <summary>
    /// ManualRunningView.xaml 的交互逻辑
    /// </summary>
    public partial class ManualRunningView : Window
    {
        public class Person
        {
            public string Name { get; set; }
            public string Address { get; set; }

        }
        public ManualRunningView()
        {
            InitializeComponent();

            List<Person> list = new List<Person>();

            list.Add(new Person { Name = "待机" });
            list.Add(new Person { Name = "恒电流充电", Address = "计划电流（A）" });
            list.Add(new Person { Name = "恒电流放电", Address = "计划功率（kW）" });
            list.Add(new Person { Name = "恒功率充电", Address = "计划电流（A）" });
            list.Add(new Person { Name = "恒功率放电", Address = "计划功率（kW）" });

            combobox2.ItemsSource = list;
        }

        private void combobox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox == null) return;

            var person = combobox.SelectedItem as Person;
            if (person == null) return;

            //_TextBlockName.Text = person.Name;
            _PlanParameter.Text = person.Address;
        }
    }
}
