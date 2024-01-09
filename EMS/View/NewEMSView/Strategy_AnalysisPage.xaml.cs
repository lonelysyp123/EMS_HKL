using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EMS.ViewModel;
using EMS.ViewModel.NewEMSViewModel;

namespace EMS.View.NewEMSView
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class Strategy_AnalysisPage : Page
    {
        private DataAnalysisViewModel viewmodel;
        public Strategy_AnalysisPage()
        {
            InitializeComponent();
            c1Chart1.View.AxisY.Min = -1600;
            c1Chart1.View.AxisY.Max = 1600;
            c1Chart1.View.AxisX.Scale += 1;
            c1Chart1.View.AxisY.Scale += 1;
            this.DataContext = new Strategy_AnalysisPageModel();
        }
        private void chart_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta == -120)
            {
                c1Chart1.View.AxisX.Scale += 0.1;
                c1Chart1.View.AxisY.Scale += 0.1;
            }
            else if (e.Delta == 120)
            {
                c1Chart1.View.AxisX.Scale -= 0.1;
                c1Chart1.View.AxisY.Scale -= 0.1;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
