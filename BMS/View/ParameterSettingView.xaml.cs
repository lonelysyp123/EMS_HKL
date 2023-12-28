using BMS.Common.Modbus.ModbusTCP;
using BMS.Model;
using BMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BMS.View
{
    /// <summary>
    /// ParameterSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterSettingView : Page
    {
        private List<BatteryTotalViewModel> batteryTotalViewModelList;
        public ParameterSettingView()
        {
            InitializeComponent();
        }

        public void SyncContent(List<BatteryTotalViewModel> TotalList)
        {
            batteryTotalViewModelList = TotalList;
            InitDevList();
        }

        private void InitDevList()
        {
            BCMUInfo2.Items.Clear();
            bool isFirst = true;
            // 初始化BCMU列表
            for (int i = 0; i < batteryTotalViewModelList.Count; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Online.png"));
                image.Height = 50;
                image.Margin=new Thickness(5,0,0,0);

                TextBlock textBlock = new TextBlock();
                textBlock.Margin = new Thickness(5, 0, 10, 0);
                textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                Binding binding = new Binding() { Path = new PropertyPath("TotalID") };
                textBlock.SetBinding(TextBlock.TextProperty, binding);
                textBlock.Foreground = new SolidColorBrush(Colors.White);

                ListBox listBox = new ListBox();
                listBox.Items.Add(image);
                listBox.Items.Add(textBlock);

                RadioButton radioButton = new RadioButton();
                radioButton.Click += RadioButton_Click;
                radioButton.Content = listBox;
                radioButton.DataContext = batteryTotalViewModelList[i];
                BCMUInfo2.Items.Add(radioButton);

                if (isFirst)
                {
                    radioButton.IsChecked = true;
                    this.DataContext = (radioButton.DataContext as BatteryTotalViewModel).parameterSettingViewModel;
                    isFirst = false;
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((sender as RadioButton).DataContext as BatteryTotalViewModel).parameterSettingViewModel;
        }
    }
}

