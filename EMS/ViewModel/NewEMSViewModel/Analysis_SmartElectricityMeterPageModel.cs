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
    public class Analysis_SmartElectricityMeterPageModel : ViewModelBase
    {
        #region Property
        private PlotModel _electricitySMDisplayDataModel;
        /// <summary>
        /// 图表数据
        /// </summary>
        public PlotModel ElectricitySMDisplayDataModel
        {
            get => _electricitySMDisplayDataModel;
            set
            {
                if (_electricitySMDisplayDataModel != value)
                {
                    _electricitySMDisplayDataModel = value;
                    OnPropertyChanged(nameof(ElectricitySMDisplayDataModel));
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

        private string _selectedType;
        /// <summary>
        /// 选中的数据类型
        /// </summary>
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                if (SetProperty(ref _selectedType, value) && _hasValidData)
                {
                    SwitchSmartElectricityMeterData();
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

        public Analysis_SmartElectricityMeterPageModel()
        {
            QueryCommand = new RelayCommand(Query);
            ExportCommand = new RelayCommand(Export);
            ElectricitySMDisplayDataModel = new PlotModel();
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
                List<double[]> smartElectricityMeterInfoArray = SmartElectricityMeterInfo(StartTime, EndTime);

                if (smartElectricityMeterInfoArray != null && smartElectricityMeterInfoArray.Any())
                {
                    DisplayDataList.Add(smartElectricityMeterInfoArray);
                    _hasValidData = true;
                    SelectedTypeIndex = 0;
                    SwitchSmartElectricityMeterData();
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
                List<double[]> smartElectricityMeterData = SmartElectricityMeterInfo(startTime, endTime);
                // 检查是否有数据
                if (smartElectricityMeterData == null || !smartElectricityMeterData.Any())
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
                        ExportSmartElectricityMeterInfoToCsv(smartElectricityMeterData, timeList, filePath);
                        MessageBox.Show("电量计量电表数据已成功导出至 " + filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出电量计量电表数据时发生错误: {ex.Message}");
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
        /// <param name="smartElectricityMeterData"></param>
        /// <param name="timeList"></param>
        /// <param name="filePath"></param>
        private void ExportSmartElectricityMeterInfoToCsv(List<double[]> smartElectricityMeterData, List<DateTime> timeList, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                sw.WriteLine("总正向一次侧电能,总反向一次侧电能,电压,电流,功率,总正向有功电能,总尖正向有功电能,总峰正向有功电能,总平正向有功电能,总谷正向有功电能," +
                    "当前月总正向有功电能,当前月尖正向有功电能,当前月峰正向有功电能,当前月平正向有功电能,当前月谷正向有功电能,总反向有功电能,总尖反向有功电能," +
                    "总峰反向有功电能,总平反向有功电能,总谷反向有功电能,当前月总反向有功电能,当前月尖反向有功电能,当前月峰反向有功电能,当前月平反向有功电能,当前月谷反向有功电能,时间");

                for (int i = 0; i < timeList.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int j = 0; j < smartElectricityMeterData.Count; j++)
                    {
                        sb.Append(smartElectricityMeterData[j][i]);
                        if (j < smartElectricityMeterData.Count - 1) // 在最后一个数据项前添加逗号分隔符
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
        private List<double[]> SmartElectricityMeterInfo(DateTime startTime, DateTime endTime)
        {
            SmartElectricityMeterInfoManage SeriesManage = new SmartElectricityMeterInfoManage();
            var SeriesList = SeriesManage.Find(startTime, endTime);
            List<double[]> obj = new List<double[]>();
            // 查询电表数据
            //总正、反向一次侧电能，电压，电流，功率
            List<double> totalForwardPrimaryEnergyList = new List<double>();
            List<double> totalReversePrimaryEnergyList = new List<double>();
            List<double> voltageList = new List<double>();
            List<double> currentList = new List<double>();
            List<double> powerList = new List<double>();
            //总正向有功电能
            List<double> totalActiveEnergyList = new List<double>();
            List<double> totalSpikesActiveEnergyList = new List<double>();
            List<double> totalPeakActiveEnergyList = new List<double>();
            List<double> totalFlatActiveEnergyList = new List<double>();
            List<double> totalValleyActiveEnergyList = new List<double>();
            //当月正向有功电能
            List<double> currMonthTotalActiveEnergyList = new List<double>();
            List<double> currMonthSpikesActiveEnergyList = new List<double>();
            List<double> currMonthPeakActiveEnergyList = new List<double>();
            List<double> currMonthFlatRateActiveEnergyList = new List<double>();
            List<double> currMonthValleyActiveEnergyList = new List<double>();
            //总反向有功电能
            List<double> totalReverseActiveEnergyList = new List<double>();
            List<double> totalSpikesReverseActiveEnergyList = new List<double>();
            List<double> totalPeakReverseActiveEnergyList = new List<double>();
            List<double> totalFlatReverseActiveEnergyList = new List<double>();
            List<double> totalValleyReverseActiveEnergyList = new List<double>();
            //当月反向有功电能
            List<double> currMonthTotalReverseActiveEnergyList = new List<double>();
            List<double> currMonthSpikesReverseActiveEnergyList = new List<double>();
            List<double> currMonthPeakReverseActiveEnergyList = new List<double>();
            List<double> currMonthFlatReverseActiveEnergyList = new List<double>();
            List<double> currMonthValleyReverseActiveEnergyList = new List<double>();
            List<DateTime> times = new List<DateTime>();
            if (SeriesList == null || !SeriesList.Any())
            {
                return null;
            }
            else
            {
                for (int i = 0; i < SeriesList.Count; i++)
                {
                    var item0 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalForwardPrimaryEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item0.ToString(), out double totalForwardPrimaryEnergy))
                    {
                        totalForwardPrimaryEnergyList.Add(totalForwardPrimaryEnergy);
                    }
                    var item1 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalReversePrimaryEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item1.ToString(), out double totalReversePrimaryEnergy))
                    {
                        totalReversePrimaryEnergyList.Add(totalReversePrimaryEnergy);
                    }
                    var item2 = typeof(SmartElectricityMeterInfoModel).GetProperty("Voltage").GetValue(SeriesList[i]);
                    if (double.TryParse(item2.ToString(), out double voltage))
                    {
                        voltageList.Add(voltage);
                    }
                    var item3 = typeof(SmartElectricityMeterInfoModel).GetProperty("Current").GetValue(SeriesList[i]);
                    if (double.TryParse(item3.ToString(), out double current))
                    {
                        currentList.Add(current);
                    }
                    var item4 = typeof(SmartElectricityMeterInfoModel).GetProperty("Power").GetValue(SeriesList[i]);
                    if (double.TryParse(item4.ToString(), out double power))
                    {
                        powerList.Add(power);
                    }
                    var item5 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item5.ToString(), out double totalActiveEnergy))
                    {
                        totalActiveEnergyList.Add(totalActiveEnergy);
                    }
                    var item6 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalSpikesActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item6.ToString(), out double totalSpikesActiveEnergy))
                    {
                        totalSpikesActiveEnergyList.Add(totalSpikesActiveEnergy);
                    }
                    var item7 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalPeakActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item7.ToString(), out double totalPeakActiveEnergy))
                    {
                        totalPeakActiveEnergyList.Add(totalPeakActiveEnergy);
                    }
                    var item8 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalFlatActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item8.ToString(), out double totalFlatActiveEnergy))
                    {
                        totalFlatActiveEnergyList.Add(totalFlatActiveEnergy);
                    }
                    var item9 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalValleyActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item9.ToString(), out double totalValleyActiveEnergy))
                    {
                        totalValleyActiveEnergyList.Add(totalValleyActiveEnergy);
                    }
                    var item10 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthTotalActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item10.ToString(), out double currMonthTotalActiveEnergy))
                    {
                        currMonthTotalActiveEnergyList.Add(currMonthTotalActiveEnergy);
                    }
                    var item11 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthSpikesActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item11.ToString(), out double currMonthSpikesActiveEnergy))
                    {
                        currMonthSpikesActiveEnergyList.Add(currMonthSpikesActiveEnergy);
                    }
                    var item12 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthPeakActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item12.ToString(), out double currMonthPeakActiveEnergy))
                    {
                        currMonthPeakActiveEnergyList.Add(currMonthPeakActiveEnergy);
                    }
                    var item13 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthFlatRateActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item13.ToString(), out double currMonthFlatRateActiveEnergy))
                    {
                        currMonthFlatRateActiveEnergyList.Add(currMonthFlatRateActiveEnergy);
                    }
                    var item14 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthValleyActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item14.ToString(), out double currMonthValleyActiveEnergy))
                    {
                        currMonthValleyActiveEnergyList.Add(currMonthValleyActiveEnergy);
                    }
                    var item15 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item15.ToString(), out double totalReverseActiveEnergy))
                    {
                        totalReverseActiveEnergyList.Add(totalReverseActiveEnergy);
                    }
                    var item16 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalSpikesReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item16.ToString(), out double totalSpikesReverseActiveEnergy))
                    {
                        totalSpikesReverseActiveEnergyList.Add(totalSpikesReverseActiveEnergy);
                    }
                    var item17 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalPeakReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item17.ToString(), out double totalPeakReverseActiveEnergy))
                    {
                        totalPeakReverseActiveEnergyList.Add(totalPeakReverseActiveEnergy);
                    }
                    var item18 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalFlatReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item18.ToString(), out double totalFlatReverseActiveEnergy))
                    {
                        totalFlatReverseActiveEnergyList.Add(totalFlatReverseActiveEnergy);
                    }
                    var item19 = typeof(SmartElectricityMeterInfoModel).GetProperty("TotalValleyReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item19.ToString(), out double totalValleyReverseActiveEnergy))
                    {
                        totalValleyReverseActiveEnergyList.Add(totalValleyReverseActiveEnergy);
                    }
                    var item20 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthTotalReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item20.ToString(), out double currMonthTotalReverseActiveEnergy))
                    {
                        currMonthTotalReverseActiveEnergyList.Add(currMonthTotalReverseActiveEnergy);
                    }
                    var item21 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthSpikesReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item21.ToString(), out double currMonthSpikesReverseActiveEnergy))
                    {
                        currMonthSpikesReverseActiveEnergyList.Add(currMonthSpikesReverseActiveEnergy);
                    }
                    var item22 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthPeakReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item22.ToString(), out double currMonthPeakReverseActiveEnergy))
                    {
                        currMonthPeakReverseActiveEnergyList.Add(currMonthPeakReverseActiveEnergy);
                    }
                    var item23 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthFlatReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item23.ToString(), out double currMonthFlatReverseActiveEnergy))
                    {
                        currMonthFlatReverseActiveEnergyList.Add(currMonthFlatReverseActiveEnergy);
                    }
                    var item24 = typeof(SmartElectricityMeterInfoModel).GetProperty("CurrMonthValleyReverseActiveEnergy").GetValue(SeriesList[i]);
                    if (double.TryParse(item24.ToString(), out double currMonthValleyReverseActiveEnergy))
                    {
                        currMonthValleyReverseActiveEnergyList.Add(currMonthValleyReverseActiveEnergy);
                    }
                    times.Add(SeriesList[i].HappenTime);

                }
            }
            obj.Add(totalForwardPrimaryEnergyList.ToArray());
            obj.Add(totalReversePrimaryEnergyList.ToArray());
            obj.Add(voltageList.ToArray());
            obj.Add(currentList.ToArray());
            obj.Add(powerList.ToArray());

            obj.Add(totalActiveEnergyList.ToArray());
            obj.Add(totalSpikesActiveEnergyList.ToArray());
            obj.Add(totalPeakActiveEnergyList.ToArray());
            obj.Add(totalFlatActiveEnergyList.ToArray());
            obj.Add(totalValleyActiveEnergyList.ToArray());

            obj.Add(currMonthTotalActiveEnergyList.ToArray());
            obj.Add(currMonthSpikesActiveEnergyList.ToArray());
            obj.Add(currMonthPeakActiveEnergyList.ToArray());
            obj.Add(currMonthFlatRateActiveEnergyList.ToArray());
            obj.Add(currMonthValleyActiveEnergyList.ToArray());

            obj.Add(totalReverseActiveEnergyList.ToArray());
            obj.Add(totalSpikesReverseActiveEnergyList.ToArray());
            obj.Add(totalPeakReverseActiveEnergyList.ToArray());
            obj.Add(totalFlatReverseActiveEnergyList.ToArray());
            obj.Add(totalValleyReverseActiveEnergyList.ToArray());

            obj.Add(currMonthTotalReverseActiveEnergyList.ToArray());
            obj.Add(currMonthSpikesReverseActiveEnergyList.ToArray());
            obj.Add(currMonthPeakReverseActiveEnergyList.ToArray());
            obj.Add(currMonthFlatReverseActiveEnergyList.ToArray());
            obj.Add(currMonthValleyReverseActiveEnergyList.ToArray());
            TimeList.Add(times.ToArray());
            return obj;
        }

        /// <summary>
        /// 选择数据类型
        /// </summary>
        /// <param name="type">数据类型</param>
        public void SwitchSmartElectricityMeterData()
        {
            InitChart();
            ElectricitySMDisplayDataModel.Series.Clear();
            for (int i = 0; i < DisplayDataList.Count; i++)
            {
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = SelectedType;
                lineSeries.MarkerSize = 3;
                lineSeries.MarkerType = MarkerType.Circle;
                for (int j = 0; j < DisplayDataList[i][SelectedTypeIndex].Length; j++)
                {
                    lineSeries.Points.Add(DateTimeAxis.CreateDataPoint(TimeList[i][j], DisplayDataList[i][SelectedTypeIndex][j]));
                }
                ElectricitySMDisplayDataModel.Series.Add(lineSeries);
            }
            ElectricitySMDisplayDataModel.InvalidatePlot(true);
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
            ElectricitySMDisplayDataModel.Legends.Clear();
            var l = new Legend
            {
                LegendBorder = OxyColors.White,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Inside,
                LegendOrientation = LegendOrientation.Vertical,
            };
            ElectricitySMDisplayDataModel.Legends.Add(l);

            //! Axes
            ElectricitySMDisplayDataModel.Axes.Clear();

            ElectricitySMDisplayDataModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = SelectedType,

            });
            ElectricitySMDisplayDataModel.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "时间",
                IntervalType = DateTimeIntervalType.Seconds,
                StringFormat = "HH:mm:ss",

            });
        }
    }
}