using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_PCSPageViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        private PCSDataService pcsservice;
        public Monitor_PCSPageViewModel()
        {
            Items = new ObservableCollection<Item> { };
            pcsservice = new PCSDataService();
        }

        //    private void RefreshDataTh()
        //    {
        //        while (IsDaqData)
        //        {
        //            if (pcsservice.pcsModels.TryTake(out PCSModel CurrentPCSModel))
        //            {
        //                var model = (PCSModel)CurrentPCSModel.Clone();
        //                SmartMeterModelList.Add(model);
        //                // 把数据分发给需要显示的内容
        //                App.Current.Dispatcher.Invoke(() =>
        //                {
        //                    RefreshData(CurrentSmartMeterModel);
        //                });

        //                if (IsRecordData)
        //                {
        //                    SaveData(CurrentSmartMeterModel);
        //                }
        //            }
        //            else
        //            {
        //                Thread.Sleep(500);
        //            }
        //        }
        //    }
        //}
    }
    public class Item
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
