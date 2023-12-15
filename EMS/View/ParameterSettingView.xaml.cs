using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.ViewModel;
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

namespace EMS.View
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
                textBlock.Text = batteryTotalViewModelList[i].TotalID;
                textBlock.Foreground = new SolidColorBrush(Colors.White);

                ListBox listBox = new ListBox();
                listBox.Items.Add(image);
                listBox.Items.Add(textBlock);

                RadioButton radioButton = new RadioButton();
                radioButton.Name = batteryTotalViewModelList[i].TotalID;
                radioButton.Click += RadioButton_Click;
                radioButton.Content = listBox;

                BCMUInfo2.Items.Add(radioButton);

                if (isFirst)
                {
                    radioButton.IsChecked = true;
                    this.DataContext = batteryTotalViewModelList[i].parameterSettingViewModel;
                    isFirst = false;
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < batteryTotalViewModelList.Count; i++)
            {
                if (batteryTotalViewModelList[i].TotalID == (sender as RadioButton).Name)
                {
                    this.DataContext = batteryTotalViewModelList[i].parameterSettingViewModel;
                }
            }
        }
    }
}

