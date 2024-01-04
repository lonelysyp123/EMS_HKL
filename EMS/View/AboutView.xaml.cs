﻿using System;
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
using System.IO;

namespace EMS.View
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : Window
    {
        public AboutView()
        {
            InitializeComponent();
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string filePath = System.IO.Path.Combine(folderPath, "commit.log");
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    branchinfo.Text = sr.ReadToEnd();
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
           this.DialogResult = true;
            if (this.DialogResult == true)
            {
                this.Close();
            }
        }
    }
}
