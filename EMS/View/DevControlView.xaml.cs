using EMS.Common.Modbus.ModbusTCP;
using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.View
{
    /// <summary>
    /// DevControlView.xaml 的交互逻辑
    /// </summary>
    public partial class DevControlView : Page
    {
        private List<BatteryTotalViewModel> batteryTotalViewModelList;
        public DevControlView()
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
            BCMUInfo.Items.Clear();
            // 初始化BCMU列表
            bool isFirst = true;
            for (int i = 0; i < batteryTotalViewModelList.Count; i++)
            {
                if (batteryTotalViewModelList[i].IsConnected)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Image/Online.png"));
                    image.Height = 50;

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

                    BCMUInfo.Items.Add(radioButton);

                    if (isFirst)
                    {
                        radioButton.IsChecked = true;
                        this.DataContext = batteryTotalViewModelList[i].devControlViewModel;
                        isFirst = false;
                    }
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < batteryTotalViewModelList.Count; i++)
            {
                if(batteryTotalViewModelList[i].TotalID == (sender as RadioButton).Name)
                {
                    this.DataContext = batteryTotalViewModelList[i].devControlViewModel;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            string pattern = @"^[\x00-\x7F]*$";
            if (textBox.Text.Length ==16)
            {
                e.Handled = true;
                return;
            }
            if (!Regex.IsMatch(text, pattern))
            {
                MessageBox.Show("请输入正确字符");
            }
        }
    }
}
