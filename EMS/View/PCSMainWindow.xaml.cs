using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EMS.View;
using System.Web.UI.WebControls;
using EMS.Model;

namespace EMS.View
{
    /// <summary>
    /// PCSMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PCSMainWindow : Window
    {
        PCSMainViewModel viewModel;
        public PCSMainWindow(PCSMainViewModel viewmodel)
        {
            InitializeComponent();
            

            viewModel = viewmodel;
            this.DataContext = viewModel;

            PCSMonitorView.DataContext = viewModel.PCSModel.MonitorModel;
            DCStatusView.DataContext = viewModel.PCSModel.MonitorModel;
            PCSSettingView.DataContext = viewModel.PCSModel.ParSettingModel;

            button1.DataContext = viewModel;
            button2.DataContext = viewModel;
            button3.DataContext = viewModel;
            button4.DataContext = viewModel;
            button5.DataContext = viewModel;
            button6.DataContext = viewModel;

            Image1.DataContext = viewModel.PCSModel;
            Image2.DataContext = viewModel.PCSModel;

            //viewModel = new PCSMainViewModel();
            //viewModel = viewModel1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.IsRead)
            {
                MessageBox.Show("请停止采集");
                e.Cancel = true;
            }
            else if (viewModel.IsConnected)
            {
                MessageBox.Show("请断开连接");
                e.Cancel = true;
            }
            else if(!viewModel.IsConnected && !viewModel.IsRead)
            {
                if (viewModel.DataAcquisitionThread != null)
                {
                    if (viewModel.DataAcquisitionThread.ThreadState == ThreadState.Stopped)
                    {
                        viewModel.DataAcquisitionThread.Abort();
                    }
                }
            }
        }
    }
}
