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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.View.NewEMSView
{
    public class Person
    {
        public string faultNumber { get; set; }
        public string faultDevice { get; set; }

        public string faultId { get; set; }
        public string faultModule { get; set; }
        public string faultName { get; set; }
        public string faultGrade { get; set; }
        public string faultTime { get; set; }
    }

    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class FaultLogPage : Page
    {
        FaultLogPageModel viewmodel;
        public FaultLogPage()
        {
            InitializeComponent();
            viewmodel = new FaultLogPageModel();
            this.DataContext = viewmodel;
        }
    }
}
