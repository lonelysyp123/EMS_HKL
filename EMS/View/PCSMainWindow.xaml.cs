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

namespace EMS.View
{
    /// <summary>
    /// PCSMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PCSMainWindow : Window
    {
        PCSMainViewModel viewModel;
        public PCSMainWindow()
        {
            InitializeComponent();

            viewModel = new PCSMainViewModel();
            this.DataContext = viewModel;
            
            PCSMonitorView.DataContext = viewModel.pCSMonitorViewModel;
            DCStatusView.DataContext = viewModel.dCStatusViewModel;
            PCSSettingView.DataContext = viewModel.pCSParSettingViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.isRead = false;

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
