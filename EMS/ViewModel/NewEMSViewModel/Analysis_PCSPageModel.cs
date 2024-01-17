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
using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Analysis_PCSPageModel : ViewModelBase
    {
        #region Property
        private PlotModel _pcsDisplayDataModel;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel PCSDisplayDataModel
        {
            get => _pcsDisplayDataModel;
            set
            {
                if (_pcsDisplayDataModel != value)
                {
                    _pcsDisplayDataModel = value;
                    OnPropertyChanged(nameof(PCSDisplayDataModel));
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
                    SwitchPCSData();
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



        public Analysis_PCSPageModel()
        {
            StartTime1 = DateTime.Today.ToString();
            EndTime1 = DateTime.Today.ToString();
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            TimeList = new List<DateTime[]>();
            ExportCommand = new RelayCommand(Export);
            QueryCommand = new RelayCommand(Query);
            PCSDisplayDataModel = new PlotModel();
            DisplayDataList = new List<List<double[]>>();
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
                List<double[]> pcsInfoArray = PCSInfo(StartTime, EndTime);

                if (pcsInfoArray != null && pcsInfoArray.Any())
                {
                    DisplayDataList.Add(pcsInfoArray);
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
                List<double[]> PCSData = PCSInfo(startTime, endTime);
                // 检查是否有数据
                if (PCSData == null || !PCSData.Any())
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
                        ExportPCSInfoToCsv(PCSData, timeList, filePath);
                        MessageBox.Show("PCS数据已成功导出至 " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出PCS数据时发生错误: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择正确时间");
            }
        }

        /// <summary>
        /// 导出PCS数据到CSV文件的方法
        /// </summary>
        /// <param name="pcsData"></param>
        /// <param name="timeList"></param>
        /// <param name="filePath"></param>
        private void ExportPCSInfoToCsv(List<double[]> pcsData, List<DateTime> timeList, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                sw.WriteLine("DCPower,DCVol,DCCurrent,TotalCharCap,BusVol,ModuleTemp,EnvTemp,时间");

                for (int i = 0; i < timeList.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < pcsData.Count; j++)
                    {
                        sb.Append(pcsData[j][i]);
                        if (j < pcsData.Count - 1) // 在最后一个数据项前添加逗号分隔符
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
        /// 查询PCS数据
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        private List<double[]> PCSInfo(DateTime startTime, DateTime endTime)
        {
            PCSInfoManage SeriesManage = new PCSInfoManage();
            var SeriesList = SeriesManage.Find(startTime, endTime);
            List<double[]> obj = new List<double[]>();
            // 查询PCS数据
            List<double> dcPowerList = new List<double>();
            List<double> dcVolList = new List<double>();
            List<double> dcCurrentList = new List<double>();
            List<double> totalCharCapList = new List<double>();
            List<double> busVolList = new List<double>();
            List<double> moduleTemp1List = new List<double>();
            List<double> envTempList = new List<double>();
            List<DateTime> times = new List<DateTime>();
            if (SeriesList == null || !SeriesList.Any())
            {
                return null;
            }
            else 
            {
                for (int i = 0; i < SeriesList.Count; i++)
                {
                    var item0 = typeof(PCSInfoModel).GetProperty("DCPower").GetValue(SeriesList[i]);
                    if (double.TryParse(item0.ToString(), out double dcPower))
                    {
                        dcPowerList.Add(dcPower);
                    }

                    var item1 = typeof(PCSInfoModel).GetProperty("DCVol").GetValue(SeriesList[i]);
                    if (double.TryParse(item1.ToString(), out double dcVol))
                    {
                        dcVolList.Add(dcVol);
                    }

                    var item2 = typeof(PCSInfoModel).GetProperty("DCCurrent").GetValue(SeriesList[i]);
                    if (double.TryParse(item2.ToString(), out double dcCurrent))
                    {
                        dcCurrentList.Add(dcCurrent);
                    }

                    var item3 = typeof(PCSInfoModel).GetProperty("TotalCharCap").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double totalCharCap))
                    {
                        totalCharCapList.Add(totalCharCap);
                    }

                    var item4 = typeof(PCSInfoModel).GetProperty("BusVol").GetValue(SeriesList[i]);
                    if (double.TryParse(item4.ToString(), out double busVol))
                    {
                        busVolList.Add(busVol);
                    }

                    var item5 = typeof(PCSInfoModel).GetProperty("ModuleTemp").GetValue(SeriesList[i]);
                    if (double.TryParse(item5.ToString(), out double moduleTemp))
                    {
                        moduleTemp1List.Add(moduleTemp);
                    }

                    var item6 = typeof(PCSInfoModel).GetProperty("EnvTemp").GetValue(SeriesList[i]);
                    if (double.TryParse(item6.ToString(), out double envTemp))
                    {
                        envTempList.Add(envTemp);
                    }
                    times.Add(SeriesList[i].HappenTime);
                }
            }
            obj.Add(dcPowerList.ToArray());
            obj.Add(dcVolList.ToArray());
            obj.Add(dcCurrentList.ToArray());
            obj.Add(totalCharCapList.ToArray());
            obj.Add(busVolList.ToArray());
            obj.Add(moduleTemp1List.ToArray());
            obj.Add(envTempList.ToArray());
            TimeList.Add(times.ToArray());
            return obj;
        }

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        public void SwitchPCSData()
        {
            InitChart();
            PCSDisplayDataModel.Series.Clear();
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
                    PCSDisplayDataModel.Series.Add(lineSeries);
                }

            PCSDisplayDataModel.InvalidatePlot(true);
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
            PCSDisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.White,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            PCSDisplayDataModel.Legends.Add(l);

            //! Axes
            PCSDisplayDataModel.Axes.Clear();

            PCSDisplayDataModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = SelectedType.Content.ToString(),

            });
            PCSDisplayDataModel.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",

            });
        }
    }
}
