using CommunityToolkit.Mvvm.Input;
using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
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
using System.Diagnostics;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TNCN.EMS.Common.Mqtt;


namespace EMS.ViewModel.NewEMSViewModel
{
    public class Analysis_BMSPageModel : ViewModelBase
    {
        #region Property
        private PlotModel _bmsDisplayDataModel;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel BMSDisplayDataModel
        {
            get => _bmsDisplayDataModel;
            set
            {
                //SetProperty(ref _bmsDisplayData, value);
                if (_bmsDisplayDataModel != value)
                {
                    _bmsDisplayDataModel = value;
                    OnPropertyChanged(nameof(BMSDisplayDataModel));
                }
            }
        }

        private string _selectedTotal;
        /// <summary>
        /// BCMU序列号
        /// </summary>
        public string SelectedTotal
        {
            get => _selectedTotal;
            set
            {
                SetProperty(ref _selectedTotal, value);
            }
        }

        private string _selectedSeries;
        /// <summary>
        /// BMU序列号
        /// </summary>
        public string SelectedSeries
        {
            get => _selectedSeries;
            set
            {
                SetProperty(ref _selectedSeries, value);
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

                if (SetProperty(ref _selectedType, value) && _hasValidBatteryData)
                    //if (SetProperty(ref _selectedType, value) )
                {
                    SwitchBatteryData();
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

        
        private string _selectedNumber;
        /// <summary>
        /// 选中的电池序号
        /// </summary>
        public string SelectedNumber
        {
            get => _selectedNumber;
            set
            {
                if (SetProperty(ref _selectedNumber, value))
                {
                    SwitchBatteryData();
                }
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

        /// <summary>
        /// 查询数据集合
        /// </summary>
        public List<List<double[]>> DisplayDataList;

        public List<DateTime[]> TimeList;


        /// <summary>
        /// 参数选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public Analysis_BMSPageModel()
        {
            StartTime1 = DateTime.Today.ToString();
            EndTime1 = DateTime.Today.ToString();
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            QueryCommand = new RelayCommand(Query);
            ExportCommand = new RelayCommand(Export);
            BMSDisplayDataModel = new PlotModel();
            DisplayDataList = new List<List<double[]>>();
            TimeList = new List<DateTime[]>();

        }


        private bool _hasValidBatteryData = false;
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DisplayDataList.Clear();
            TimeList.Clear();

            if (SelectedTotal != null && SelectedSeries != null)
            {
                if (TryCombinTime(StartTime1, StartTime2, out DateTime StartTime) && TryCombinTime(EndTime1, EndTime2, out DateTime EndTime))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        string totalID = $"BCMU({SelectedTotal})";
                        var batteryInfo = QueryBatteryInfo(totalID, SelectedSeries, (i + 1).ToString(), StartTime, EndTime);

                        // 检查是否有有效数据
                        if (batteryInfo != null && batteryInfo.Any())
                        {
                            DisplayDataList.Add(batteryInfo);
                        }
                    }
                    // 如果至少有一组有效数据，则设置为true
                    _hasValidBatteryData = DisplayDataList.Any(list => list != null && list.Any());
                    if (!_hasValidBatteryData)
                    {
                        MessageBox.Show("暂无数据");
                    }
                }
                else
                {
                    MessageBox.Show("请选择正确时间");
                    _hasValidBatteryData = false;
                }
            }
            else
            {
                MessageBox.Show("请选择正确信息");
                _hasValidBatteryData = false;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            string BCMUID = $"BCMU({SelectedTotal})"; // 假设SelectedTotal是BCMUID
            string BMUID = SelectedSeries; // 获取或设置对应的BMUID值，这里假设已获取
            DateTime startTime, endTime;
            if (TryCombinTime(StartTime1, StartTime2, out startTime) && TryCombinTime(EndTime1, EndTime2, out endTime))
            {
                List<List<double[]>> allBMSData = new List<List<double[]>>();
                for (int i = 0; i < 14; i++)
                {
                    string sort = (i + 1).ToString();
                    List<double[]> batteryInfo = QueryBatteryInfo(BCMUID, BMUID, sort, startTime, endTime);
                    if (batteryInfo != null)
                    {
                        allBMSData.Add(batteryInfo);
                    }
                }
                // 打印 allBMSData 内容
                string objContent = string.Join(Environment.NewLine, allBMSData.Select(subArray =>
                {
                    return "[" + string.Join(", ", subArray.Select(innerArray => $"[{string.Join(", ", innerArray)}]")) + "]";
                }));
                Console.WriteLine("333333333333" + Environment.NewLine + objContent);
                // 检查是否有数据
                if (allBMSData == null || !allBMSData.Any())
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
                        ExportBMSInfoToCsv(allBMSData, timeList, filePath);
                        MessageBox.Show("BMS数据已成功导出至 " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出BMS数据时发生错误: {ex.Message}");
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
        /// <param name="allBMSData"></param>
        /// <param name="timeList"></param>
        /// <param name="filePath"></param>
        private void ExportBMSInfoToCsv(List<List<double[]>> allBMSData, List<DateTime> timeList, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                sw.WriteLine("BCMUID,BMUID,sort,Voltage,Capacity,SOC,Resistance,Temperature1,Temperature2,时间");
                for (int i = 0; i < timeList.Count; i++)
                {
                    Console.WriteLine("1111个数:"+timeList.Count);
                    for (int j = 0; j < allBMSData.Count; j++)
                    {
                        Console.WriteLine("2222个数:" + allBMSData.Count);
                        StringBuilder sb = new StringBuilder();

                        // 添加固定信息（BCMUID和BMUID不需要每次循环都写入）
                        sb.Append($"BCMU({SelectedTotal})");
                        sb.Append(",");
                        sb.Append(SelectedSeries);
                        sb.Append(",");
                        sb.Append((j + 1).ToString()); // 序号
                        for(int k = 0; k < allBMSData[j].Count; k++)
                        {
                            Console.WriteLine("333个数:" + allBMSData[j].Count);
                            sb.Append(",");
                            sb.Append(allBMSData[j][k][i]);
                        }
                        sb.Append(",");
                        sb.Append(timeList[i].ToString("yyyy-MM-dd HH:mm:ss")); // 格式化日期时间

                        // 每完成一个sort的数据就换行写入
                        sw.WriteLine(sb.ToString());

                        // 重置StringBuilder，准备写入下一个sort的数据
                        sb.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// 查询单体电池数据
        /// </summary>
        /// <param name="BCMUID">BCMUID</param>
        /// <param name="BMUID">BMUID</param>
        /// <param name="sort">电池序号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        private List<double[]> QueryBatteryInfo(string BCMUID, string BMUID, string sort, DateTime startTime, DateTime endTime)
        {
            SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
            var SeriesList = SeriesManage.Find(BCMUID, BMUID, startTime, endTime);
            List<double[]> obj = new List<double[]>();
            if (int.TryParse(sort, out int Sort))
            {
                // 查询Battery数据
                List<double> vols = new List<double>();
                List<double> caps = new List<double>();
                List<double> socList = new List<double>();
                List<double> resistances = new List<double>();
                List<double> temperature1List = new List<double>();
                List<double> temperature2List = new List<double>();
                List<DateTime> times = new List<DateTime>();
                if (SeriesList == null || !SeriesList.Any())
                {
                    return null;
                }
                else
                {
                    for (int i = 0; i < SeriesList.Count; i++)
                    {
                        Console.WriteLine("SeriesList.Count:"+SeriesList.Count);
                        var item0 = typeof(SeriesBatteryInfoModel).GetProperty("Voltage" + (Sort - 1)).GetValue(SeriesList[i]);
                        if (double.TryParse(item0.ToString(), out double vol))
                        {
                            vols.Add(vol);
                        }

                        var item1 = typeof(SeriesBatteryInfoModel).GetProperty("Capacity" + (Sort - 1)).GetValue(SeriesList[i]);
                        if (double.TryParse(item1.ToString(), out double cap))
                        {
                            caps.Add(cap);
                        }

                        var item2 = typeof(SeriesBatteryInfoModel).GetProperty("SOC" + (Sort - 1)).GetValue(SeriesList[i]);
                        if (double.TryParse(item2.ToString(), out double soc))
                        {
                            socList.Add(soc);
                        }

                        var item3 = typeof(SeriesBatteryInfoModel).GetProperty("Resistance" + (Sort - 1)).GetValue(SeriesList[i]);
                        if (double.TryParse(item3.ToString(), out double resistance))
                        {
                            resistances.Add(resistance);
                        }

                        var item4 = typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + (Sort - 1) * 2).GetValue(SeriesList[i]);
                        if (double.TryParse(item4.ToString(), out double temperature1))
                        {
                            temperature1List.Add(temperature1);
                        }

                        var item5 = typeof(SeriesBatteryInfoModel).GetProperty("Temperature" + ((Sort - 1) * 2 + 1)).GetValue(SeriesList[i]);
                        if (double.TryParse(item5.ToString(), out double temperature2))
                        {
                            temperature2List.Add(temperature2);
                        }

                        times.Add(SeriesList[i].HappenTime);
                    }
                }
                obj.Add(vols.ToArray());
                obj.Add(caps.ToArray());
                obj.Add(socList.ToArray());
                obj.Add(resistances.ToArray());
                obj.Add(temperature1List.ToArray());
                obj.Add(temperature2List.ToArray());
                TimeList.Add(times.ToArray());
            }
            string objContent = string.Join(Environment.NewLine, obj.Select(array => $"[{string.Join(", ", array)}]"));
            Console.WriteLine("wwwwwww" + objContent);
            return obj;
        }

        public void SwitchBatteryData()
        {
            InitChart();
            BMSDisplayDataModel.Series.Clear();
            LineSeries lineSeries = new LineSeries();
            lineSeries.MarkerSize = 3;
            lineSeries.MarkerType = MarkerType.Circle;
            int index = 0;
            if (SelectedNumber != null)
            {
                MatchCollection matches = Regex.Matches(SelectedNumber, @"\d+(\.\d+)?");
                foreach (Match match in matches)
                {
                    lineSeries.Title = match.Value;
                    index = int.Parse(match.Value);
                }
                if (DisplayDataList.Count > 0 && DisplayDataList.Count > index - 1)
                {
                    if (DisplayDataList[index - 1].Count > 0)
                    {
                        if (DisplayDataList[index - 1][SelectedTypeIndex].Length > 0)
                        {
                            for (int j = 0; j < DisplayDataList[index - 1][SelectedTypeIndex].Length; j++)
                            {
                                Debug.WriteLine(TimeList[index - 1][j], "11111111111");
                                Debug.WriteLine(DisplayDataList[index - 1][SelectedTypeIndex][j], "22222222");
                                lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[index - 1][j], DisplayDataList[index - 1][SelectedTypeIndex][j]));
                            }
                            BMSDisplayDataModel.Series.Add(lineSeries);
                        }
                    }
                }
            }

            BMSDisplayDataModel.InvalidatePlot(true);
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
            BMSDisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.White,

                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            BMSDisplayDataModel.Legends.Add(l);

            //! Axes
            BMSDisplayDataModel.Axes.Clear();

            BMSDisplayDataModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = SelectedType.Content.ToString(),

            });
            BMSDisplayDataModel.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",

            });
        }
    }
}
