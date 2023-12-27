using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class HomePageModel : ViewModelBase
    {
        #region Property

        private SolidColorBrush stateFill_Normal;
        public SolidColorBrush StateFill_Normal
        {
            get { return stateFill_Normal; }
            set
            {
                SetProperty(ref stateFill_Normal, value);
            }
        }

        private SolidColorBrush stateFill_Offline;
        public SolidColorBrush StateFill_Offline
        {
            get { return stateFill_Offline; }
            set
            {
                SetProperty(ref stateFill_Offline, value);
            }
        }

        private string installedPower;
        public string InstalledPower
        {
            get { return installedPower; }
            set
            {
                SetProperty(ref installedPower, value);
            }
        }

        #endregion

        #region Command



        #endregion

        public HomePageModel()
        {

        }
    }
}
