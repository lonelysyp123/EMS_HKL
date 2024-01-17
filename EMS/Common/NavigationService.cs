using EMS.View.NewEMSView;
using EMS.ViewModel.NewEMSViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EMS.Common
{
    public static class NavigationService
    {
        public static Frame m_Frame;

        public static void Navigation_BMU(Monitor_BMS_BCMUPageModel pagemodel)
        {
            m_Frame.Content = new Monitor_BMS_BCMUPage(pagemodel);
        }
    }
}
