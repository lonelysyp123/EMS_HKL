using CommunityToolkit.Mvvm.Input;
using EMS.Storage.DB.DBManage;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OxyPlot.Series;
using static System.Net.Mime.MediaTypeNames;
using EMS.Model;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows;
using EMS.Storage.DB.Models;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Analysis_SmartMeterPageModel : ViewModelBase
    {
        #region Property
        private PlotModel _smDisplayDataModel;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel SMDisplayDataModel
        {
            get => _smDisplayDataModel;
            set
            {
                if (_smDisplayDataModel != value)
                {
                    _smDisplayDataModel = value;
                    OnPropertyChanged(nameof(SMDisplayDataModel));
                }
            }
        }

        private string _startTime1;
        /// <summary>
        /// 开始时间-日期
        /// </summary>
        public string StartTime1
        {
            get => _startTime1;
            set
            {
                SetProperty(ref _startTime1, value);
            }
        }

        private string _startTime2;
        /// <summary>
        /// 开始时间-时分秒
        /// </summary>
        public string StartTime2
        {
            get => _startTime2;
            set
            {
                SetProperty(ref _startTime2, value);
            }
        }

        private string _endTime1;
        /// <summary>
        /// 结束时间-日期
        /// </summary>
        public string EndTime1
        {
            get => _endTime1;
            set
            {
                SetProperty(ref _endTime1, value);
            }
        }

        private string _endTime2;
        /// <summary>
        /// 结束时间-时分秒
        /// </summary>
        public string EndTime2
        {
            get => _endTime2;
            set
            {
                SetProperty(ref _endTime2, value);
            }
        }

        private ListBoxItem _selectedType;
        /// <summary>
        /// 选中的数据类型
        /// </summary>
        public ListBoxItem SelectedType
        {
            get => _selectedType;
            set
            {
                if(SetProperty(ref _selectedType, value) && _hasValidData)
                {
                    SwitchSmartMeterData();
                }
            }
        }

        private int _selectedTypeIndex;
        /// <summary>
        /// 选中的数据类型序号
        /// </summary>
        public int SelectedTypeIndex
        {
            get => _selectedTypeIndex;
            set
            {
                SetProperty(ref _selectedTypeIndex, value);
            }
        }

        #endregion

        #region Command
        /// <summary>
        /// 查询
        /// </summary>
        public RelayCommand QueryCommand { set; get; }

        /// <summary>
        /// 导出
        /// </summary>
        public RelayCommand ExportCommand { set; get; }

        #endregion

        #region List
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public List<List<double[]>> DisplayDataList;

        public List<DateTime[]> TimeList;
        #endregion
        public Analysis_SmartMeterPageModel()
        {
            QueryCommand = new RelayCommand(Query);
            ExportCommand = new RelayCommand(Export);
            SMDisplayDataModel = new PlotModel();
            DisplayDataList = new List<List<double[]>>();
            TimeList = new List<DateTime[]>();
            StartTime1 = DateTime.Today.ToString();
            EndTime1 = DateTime.Today.ToString();
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
        }


        private bool _hasValidData = false;
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DisplayDataList.Clear();
            TimeList.Clear();


            if (TryCombinTime(StartTime1, StartTime2, out DateTime StartTime) && TryCombinTime(EndTime1, EndTime2, out DateTime EndTime))
            {
                List<double[]> smartMeterInfoArray = SmartMeterInfo(StartTime, EndTime);

                if (smartMeterInfoArray != null && smartMeterInfoArray.Any())
                {
                    DisplayDataList.Add(smartMeterInfoArray);
                    _hasValidData = true;
                }
                else
                {
                    MessageBox.Show("暂无数据");
                    _hasValidData = false;
                }
            }
            else
            {
                MessageBox.Show("请选择正确时间");
                _hasValidData = false;
            }
        }


        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            DateTime startTime, endTime;
            if (TryCombinTime(StartTime1, StartTime2, out startTime) && TryCombinTime(EndTime1, EndTime2, out endTime))
            {
                List<double[]> smartMeterData = SmartMeterInfo(startTime, endTime);
                // 检查是否有数据
                if (smartMeterData == null || !smartMeterData.Any())
                {
                    MessageBox.Show("暂无数据可供导出");
                    return;
                }
                List<DateTime> timeList = TimeList[0].ToList();// 假设时间列表在查询后只有一组数据

                // 使用SaveFileDialog获取用户选择的保存路径
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv";
                saveFileDialog.Title = "选择保存位置";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    try
                    {
                        ExportSmartMeterInfoToCsv(smartMeterData, timeList, filePath);
                        MessageBox.Show("电表数据已成功导出至 " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出电表数据时发生错误: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择正确时间");
            }
        }

        /// <summary>
        /// 导出电表数据到CSV文件的方法
        /// </summary>
        /// <param name="smartMeterData"></param>
        /// <param name="timeList"></param>
        /// <param name="filePath"></param>
        private void ExportSmartMeterInfoToCsv(List<double[]> smartMeterData, List<DateTime> timeList, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                sw.WriteLine("Voltage_A,Voltage_B,Voltage_C,Current_A,Current_B,Current_C,ActivePower_A,ActivePower_B,ActivePower_C,ActivePower_Total,ReactivePower_A,ReactivePower_B,ReactivePower_C,ReactivePower_Total,时间");

                for (int i = 0; i < timeList.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int j = 0; j < smartMeterData.Count; j++)
                    {
                        sb.Append(smartMeterData[j][i]);
                        if (j < smartMeterData.Count - 1) // 在最后一个数据项前添加逗号分隔符
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append(",");
                    sb.Append(timeList[i].ToString("yyyy-MM-dd HH:mm:ss")); // 格式化日期时间

                    sw.WriteLine(sb.ToString());
                }
            }
        }

        /// <summary>
        /// 查询电表数据
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        private List<double[]> SmartMeterInfo(DateTime startTime, DateTime endTime)
        {
            ElectricMeterInfoManage SeriesManage = new ElectricMeterInfoManage();
            var SeriesList = SeriesManage.Find(startTime, endTime);
            List<double[]> obj = new List<double[]>();
            // 查询电表数据
            List<double> voltage_AList = new List<double>();
            List<double> voltage_BList = new List<double>();
            List<double> voltage_CList = new List<double>();
            List<double> current_AList = new List<double>();
            List<double> current_BList = new List<double>();
            List<double> current_CList = new List<double>();
            List<double> activePower_AList = new List<double>();
            List<double> activePower_BList = new List<double>();
            List<double> activePower_CList = new List<double>();
            List<double> activePower_TotalList = new List<double>();
            List<double> reactivePower_AList = new List<double>();
            List<double> reactivePower_BList = new List<double>();
            List<double> reactivePower_CList = new List<double>();
            List<double> reactivePower_TotalList = new List<double>();
            List<DateTime> times = new List<DateTime>();
            if (SeriesList == null || !SeriesList.Any())
            {
                return null;
            }
            else
            {
                for (int i = 0; i < SeriesList.Count; i++)
                {
                    var item0 = typeof(ElectricMeterInfoModel).GetProperty("Voltage_A").GetValue(SeriesList[i]);
                    if (double.TryParse(item0.ToString(), out double voltage_A))
                    {
                        voltage_AList.Add(voltage_A);
                    }
                    var item1 = typeof(ElectricMeterInfoModel).GetProperty("Voltage_B").GetValue(SeriesList[i]);
                    if (double.TryParse(item1.ToString(), out double voltage_B))
                    {
                        voltage_BList.Add(voltage_B);
                    }
                    var item2 = typeof(ElectricMeterInfoModel).GetProperty("Voltage_C").GetValue(SeriesList[i]);
                    if (double.TryParse(item2.ToString(), out double voltage_C))
                    {
                        voltage_CList.Add(voltage_C);
                    }

                    var item3 = typeof(ElectricMeterInfoModel).GetProperty("Current_A").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double current_A))
                    {
                        current_AList.Add(current_A);
                    }
                    var item4 = typeof(ElectricMeterInfoModel).GetProperty("Current_B").GetValue(SeriesList[i]);
                    if (double.TryParse(item4.ToString(), out double current_B))
                    {
                        current_BList.Add(current_B);
                    }
                    var item5 = typeof(ElectricMeterInfoModel).GetProperty("Current_C").GetValue(SeriesList[i]);
                    if (double.TryParse(item5.ToString(), out double current_C))
                    {
                        current_CList.Add(current_C);
                    }

                    var item6 = typeof(ElectricMeterInfoModel).GetProperty("ActivePowerA").GetValue(SeriesList[i]);
                    if (double.TryParse(item6.ToString(), out double activePowerA))
                    {
                        activePower_AList.Add(activePowerA);
                    }
                    var item7 = typeof(ElectricMeterInfoModel).GetProperty("ActivePowerB").GetValue(SeriesList[i]);
                    if (double.TryParse(item7.ToString(), out double activePowerB))
                    {
                        activePower_BList.Add(activePowerB);
                    }
                    var item8 = typeof(ElectricMeterInfoModel).GetProperty("ActivePowerC").GetValue(SeriesList[i]);
                    if (double.TryParse(item8.ToString(), out double activePowerC))
                    {
                        activePower_CList.Add(activePowerC);
                    }
                    var item9 = typeof(ElectricMeterInfoModel).GetProperty("ActivePower_Total").GetValue(SeriesList[i]);
                    if (double.TryParse(item9.ToString(), out double activePower_Total))
                    {
                        activePower_TotalList.Add(activePower_Total);
                    }

                    var item10 = typeof(ElectricMeterInfoModel).GetProperty("ReactivePowerA").GetValue(SeriesList[i]);
                    if (double.TryParse(item10.ToString(), out double reactivePowerA))
                    {
                        reactivePower_AList.Add(reactivePowerA);
                    }
                    var item11 = typeof(ElectricMeterInfoModel).GetProperty("ReactivePowerB").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double reactivePowerB))
                    {
                        reactivePower_BList.Add(reactivePowerB);
                    }
                    var item12 = typeof(ElectricMeterInfoModel).GetProperty("ReactivePowerC").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double reactivePowerC))
                    {
                        reactivePower_CList.Add(reactivePowerC);
                    }
                    var item13 = typeof(ElectricMeterInfoModel).GetProperty("ReactivePower_Total").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double reactivePower_Total))
                    {
                        reactivePower_TotalList.Add(reactivePower_Total);
                    }
                    times.Add(SeriesList[i].HappenTime);
                }
            }
            obj.Add(voltage_AList.ToArray());
            obj.Add(voltage_BList.ToArray());
            obj.Add(voltage_CList.ToArray());
            obj.Add(current_AList.ToArray());
            obj.Add(current_BList.ToArray());
            obj.Add(current_CList.ToArray());
            obj.Add(activePower_AList.ToArray());
            obj.Add(activePower_BList.ToArray());
            obj.Add(activePower_CList.ToArray());
            obj.Add(activePower_TotalList.ToArray());
            obj.Add(reactivePower_AList.ToArray());
            obj.Add(reactivePower_BList.ToArray());
            obj.Add(reactivePower_CList.ToArray());
            obj.Add(reactivePower_TotalList.ToArray());
            TimeList.Add(times.ToArray());
            return obj;
        }

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        public void SwitchSmartMeterData()
        {
            InitChart();
            SMDisplayDataModel.Series.Clear();
            for (int i = 0; i < DisplayDataList.Count; i++)
            {
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = SelectedType.Content.ToString();
                lineSeries.MarkerSize = 3;
                lineSeries.MarkerType = MarkerType.Circle;
                for (int j = 0; j < DisplayDataList[i][SelectedTypeIndex].Length; j++)
                {
                    lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[i][j], DisplayDataList[i][SelectedTypeIndex][j]));
                }
                SMDisplayDataModel.Series.Add(lineSeries);
            }
            SMDisplayDataModel.InvalidatePlot(true);
        }

        /// <summary>
        /// 合成时间
        /// </summary>
        /// <param name="obj1">时间1</param>
        /// <param name="obj2">时间2</param>
        /// <param name="CombinTime">合成后的时间</param>
        /// <returns>是否合成成功</returns>
        private bool TryCombinTime(string obj1, string obj2, out DateTime CombinTime)
        {
            if (obj1 != null && obj1 != "")
            {
                DateTime time1 = DateTime.Parse(obj1);
                if (TimeSpan.TryParse(obj2, out TimeSpan time2))
                {
                    CombinTime = time1 + time2;
                }
                else
                {
                    CombinTime = DateTime.Now;
                    return false;
                }
            }
            else
            {
                CombinTime = DateTime.Now;
                return false;
            }
            return true;
        }




        /// <summary>
        /// 初始化图表控件（定义X，Y轴）
        /// </summary>
        private void InitChart()
        {
            //! Legend
            SMDisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.White,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            SMDisplayDataModel.Legends.Add(l);

            //! Axes
            SMDisplayDataModel.Axes.Clear();

            SMDisplayDataModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = SelectedType.Content.ToString(),

            });
            SMDisplayDataModel.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",

            });
        }
    }
}
