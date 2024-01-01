using EMS.Common;
using EMS.Model;
using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class Monitor_BMSPageModel : ViewModelBase
    {
        #region ObservableObject
        

        #endregion

        public Monitor_BMS_BCMUPageModel[] bmuViewModels;

        public Monitor_BMSPageModel()
        {
            bmuViewModels = new Monitor_BMS_BCMUPageModel[6];
            for (int i = 0; i < bmuViewModels.Length; i++)
            {
                bmuViewModels[i] = new Monitor_BMS_BCMUPageModel();
            }
        }

        public void ServiceDataCallBack(BatteryTotalModel model, bool IsConnected)
        {
            int index = -1;
            if (model.BCMUID == "BCMU1") index = 1;
            else if (model.BCMUID == "BCMU2") index = 2;
            else if (model.BCMUID == "BCMU3") index = 3;
            else if (model.BCMUID == "BCMU4") index = 4;
            else if (model.BCMUID == "BCMU5") index = 5;
            else if (model.BCMUID == "BCMU6") index = 6;

            bmuViewModels[index-1].DataDistribution(model, IsConnected);
        }
    }
}
