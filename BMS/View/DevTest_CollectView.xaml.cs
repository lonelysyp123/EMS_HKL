﻿using BMS.Common.Modbus.ModbusTCP;
using BMS.Model;
using BMS.MyControl;
using BMS.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BMS.View
{
    /// <summary>
    /// DevTest_CollectView.xaml 的交互逻辑
    /// </summary>
    public partial class DevTest_CollectView : Page
    {
        public DevTest_CollectView()
        {
            InitializeComponent();
        }

        public void Test_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    AddDevIntoView(item as BatteryTotalViewModel);
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    RemoveDevIntoView(item as BatteryTotalViewModel);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                MainBody.Children.Clear();
            }
        }

        public void AddDevIntoView(BatteryTotalViewModel viewmodel)
        {
            DataControl control = new DataControl(viewmodel);
            control.Margin = new Thickness(10, 10, 20, 10);
            string pattern = @"\d+";
            Match match = Regex.Match(viewmodel.TotalID, pattern);
            int.TryParse(match.Value, out int value);
            int index = value - 1;
            // int index = MainBody.Children.Count;
            Grid.SetColumn(control, index % 3);
            Grid.SetRow(control, index / 3);
            MainBody.Children.Add(control);
        }

        public void RemoveDevIntoView(BatteryTotalViewModel viewmodel)
        {
            foreach (var item in MainBody.Children)
            {
                if(((item as DataControl).DataContext as BatteryTotalViewModel).TotalID == viewmodel.TotalID)
                {
                    MainBody.Children.Remove(item as DataControl);
                    break;
                }
            }
        }

        internal void InitView(ObservableCollection<BatteryTotalViewModel> viewmodels)
        {
            for (int i = 0; i < viewmodels.Count; i++)
            {
                AddDevIntoView(viewmodels[i]);
            }
        }
    }
}