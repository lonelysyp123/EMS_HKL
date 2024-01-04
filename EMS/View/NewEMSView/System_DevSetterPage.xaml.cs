using EMS.ViewModel.NewEMSViewModel;
using Microsoft.Win32;
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
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class System_DevSetterPage : Page
    {
        public System_DevSetterPage()
        {
            InitializeComponent();
        }

        public System_DevSetterPage(System_DevSetterPageModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }

        private void UploadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // 设置文件筛选器

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                // 在这里处理选中的文件路径
                //this.FilePathTextBox.Text = filePath;
            }
        }
    }
}
