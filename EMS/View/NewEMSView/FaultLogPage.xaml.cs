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
        public FaultLogPage()
        {
            InitializeComponent();

            List<Person> list = new List<Person>();

            list.Add(
                new Person
                {
                    faultNumber = "1",
                    faultDevice = "BCMU",
                    faultId = "1",
                    faultModule = "故障",
                    faultName = "直流高压侧过压",
                    faultGrade = "1",
                    faultTime = "2023/12/19 13:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "2",
                    faultDevice = "PCS",
                    faultId = "2",
                    faultModule = "故障",
                    faultName = "环境温度过高",
                    faultGrade = "2",
                    faultTime = "2023/12/19 12:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "3",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "4",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "5",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "6",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "7",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "8",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "9",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "10",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "11",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            list.Add(
                new Person
                {
                    faultNumber = "12",
                    faultDevice = "BCMU",
                    faultId = "5",
                    faultModule = "故障",
                    faultName = "紧急停机",
                    faultGrade = "3",
                    faultTime = "2023/12/19 10:00:00"
                }
            );
            Datagrid.ItemsSource = list;
        }
    }
}
