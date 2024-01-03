using EMS.ViewModel.NewEMSViewModel;
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

namespace EMS.View.NewEMSView
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    /// 
    public class PersonItem
    {
        public string StrategyNumber { get; set; }
    }
    public partial class Strategy_AnalysisPage : Page
    {
        public Strategy_AnalysisPage()
        {
            InitializeComponent();
        }

        public Strategy_AnalysisPage(Strategy_AnalysisPageModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }
    }
}
