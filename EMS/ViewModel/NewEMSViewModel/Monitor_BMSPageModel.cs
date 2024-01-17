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
                bmuViewModels[i] = new Monitor_BMS_BCMUPageModel((i+1).ToString());
            }
        }
    }
}
