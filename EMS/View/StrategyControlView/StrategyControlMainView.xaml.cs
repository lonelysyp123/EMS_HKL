using EMS.Properties;
using EMS.ViewModel;
using EMS.ViewModel.NewEMSViewModel;
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
using System.Windows.Shapes;

namespace EMS.View
{
    /// <summary>
    /// StrategyControl.xaml 的交互逻辑
    /// </summary>
    public partial class StrategyControl : Page
    {

        public Strategy_AnalysisPageModel strategy_AnalysisPageModel;
        public StrategyControl()
        {
            InitializeComponent();
            strategy_AnalysisPageModel = new Strategy_AnalysisPageModel();
            this.DataContext = strategy_AnalysisPageModel;

        }          
    }


        

        
    }
}
