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

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Analysis_SmartMeterPageModel : ViewModelBase
    {
        #region Property
        private PlotModel _displayData;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel DisplayDataModel
        {
            get => _displayData;
            set
            {
                SetProperty(ref _displayData, value);
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
                SetProperty(ref _selectedType, value);
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

        /// <summary>
        /// ListBox选择改变
        /// </summary>
        public RelayCommand DataTypeList_SelectionChanged { get; private set; }
        #endregion

        #region List
        public List<string> SelectedSmartMeterList;
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
            DisplayDataModel = new PlotModel();
            DisplayDataList = new List<List<double[]>>();
            TimeList = new List<DateTime[]>();
            StartTime1 = DateTime.Today.ToString();
            EndTime1 = DateTime.Today.ToString();
            StartTime2 = "00:00:00";
            EndTime2 = "00:00:00";
            SelectedSmartMeterList = new List<string>();
            DataTypeList_SelectionChanged = new RelayCommand(SwitchSmartMeterData);

        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DisplayDataList.Clear();
            TimeList.Clear();


            if (TryCombinTime(StartTime1, StartTime2, out DateTime StartTime) && TryCombinTime(EndTime1, EndTime2, out DateTime EndTime))
            {
                DisplayDataList.Add(QuerySmartMeterInfo(StartTime, EndTime));
            }
            else
            {
                MessageBox.Show("请选择正确时间");
            }
        }


        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {

        }

        /// <summary>
        /// 查询单体电池数据
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">停止时间</param>
        private List<double[]> QuerySmartMeterInfo(DateTime startTime, DateTime endTime)
        {
            SeriesBatteryInfoManage SeriesManage = new SeriesBatteryInfoManage();
            //SmartMeterManage SeriesManage = new SmartMeterManage();
            var SeriesList = SeriesManage.Find(startTime, endTime);
            List<double[]> obj = new List<double[]>();
            {
                // 查询SmartMeter数据
                List<double> selectedBaudRateList = new List<double>();
                List<double> selectedParityList = new List<double>();
                List<double> selectedStopBitsList = new List<double>();
                List<double> selectedDataBitsList = new List<double>();
                List<double> acquisitionCycleList = new List<double>();
                List<DateTime> times = new List<DateTime>();
                for (int i = 1; i < SeriesList.Count; i++)
                {
                    var item0 = typeof(SmartMeterDBModel).GetProperty("SelectedBaudRate").GetValue(SeriesList[i]);
                    if (double.TryParse(item0.ToString(), out double selectedBaudRate))
                    {
                        selectedBaudRateList.Add(selectedBaudRate);
                    }

                    var item1 = typeof(SmartMeterDBModel).GetProperty("SelectedParity").GetValue(SeriesList[i]);
                    if (double.TryParse(item1.ToString(), out double selectedParity))
                    {
                        selectedParityList.Add(selectedParity);
                    }

                    var item2 = typeof(SmartMeterDBModel).GetProperty("SelectedStopBits").GetValue(SeriesList[i]);
                    if (double.TryParse(item2.ToString(), out double selectedStopBits))
                    {
                        selectedStopBitsList.Add(selectedStopBits);
                    }

                    var item3 = typeof(SmartMeterDBModel).GetProperty("SelectedDataBits").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double selectedDataBits))
                    {
                        selectedDataBitsList.Add(selectedDataBits);
                    }

                    var item4 = typeof(SmartMeterDBModel).GetProperty("AcquisitionCycle").GetValue(SeriesList[i]);
                    if (double.TryParse(item4.ToString(), out double acquisitionCycle))
                    {
                        acquisitionCycleList.Add(acquisitionCycle);
                    }
                }
                obj.Add(selectedBaudRateList.ToArray());
                obj.Add(selectedParityList.ToArray());
                obj.Add(selectedStopBitsList.ToArray());
                obj.Add(selectedDataBitsList.ToArray());
                obj.Add(acquisitionCycleList.ToArray());
                TimeList.Add(times.ToArray());
            }
            return obj;
        }

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        public void SwitchSmartMeterData()
        {
            InitChart();
            DisplayDataModel.Series.Clear();
            for (int i = 0; i < SelectedSmartMeterList.Count; i++)
            {
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = SelectedSmartMeterList[i];
                lineSeries.MarkerSize = 3;
                lineSeries.MarkerType = MarkerType.Circle;
                if (int.TryParse(SelectedSmartMeterList[i], out int index))
                {
                    if (DisplayDataList.Count > 0 && DisplayDataList.Count > index - 1)
                    {
                        if (DisplayDataList[index - 1].Count > 0)
                        {
                            if (DisplayDataList[index - 1][SelectedTypeIndex].Length > 0)
                            {
                                for (int j = 0; j < DisplayDataList[index - 1][SelectedTypeIndex].Length; j++)
                                {
                                    lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[index - 1][j], DisplayDataList[index - 1][SelectedTypeIndex][j]));
                                }
                                DisplayDataModel.Series.Add(lineSeries);
                            }
                        }
                    }
                }
            }
            DisplayDataModel.InvalidatePlot(true);
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
            DisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.White,

                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            DisplayDataModel.Legends.Add(l);

            //! Axes
            DisplayDataModel.Axes.Clear();

            DisplayDataModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = SelectedType.Content.ToString(),

            });
            DisplayDataModel.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",

            });
        }
    }
}
