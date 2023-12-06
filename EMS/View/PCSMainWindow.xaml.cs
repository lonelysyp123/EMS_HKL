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

            PCSMonitorView.DataContext = viewModel.pCSModel.MonitorModel;
            DCStatusView.DataContext = viewModel.pCSModel.MonitorModel;
            PCSSettingView.DataContext = viewModel.pCSModel.ParSettingModel;
            button1.DataContext = viewModel;
            button2.DataContext = viewModel;
            button3.DataContext = viewModel;
            button4.DataContext = viewModel;
            button5.DataContext = viewModel;
            button6.DataContext = viewModel;

            //viewModel = new PCSMainViewModel();
            //viewModel = viewModel1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.isRead)
            {
                MessageBox.Show("请停止采集");
                e.Cancel = true;
            }
            else if (viewModel.IsConnected)
            {
                MessageBox.Show("请断开连接");
                e.Cancel = true;
            }
            else if(viewModel.IsConnected==false& viewModel.isRead==false)
            {
                if (viewModel.thread != null)
                {
                    if (viewModel.thread.ThreadState == ThreadState.Stopped)
                    {
                        viewModel.thread = null;
                    }
                }
            }
        }
    }
}
