using EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
    public class EMSMainViewModel : ViewModelBase
    {
        public HomePageModel HomePageModel { get; private set; }
        public Monitor_BMSPageModel Monitor_BMSPageModel { get; private set;}
        public Monitor_PCSPageModel Monitor_PCSPageModel { get; private set; }
        public Monitor_SmartMeterPageModel Monitor_SmartMeterPageModel { get;private set; }
        public Analysis_BMSPageModel Analysis_BMSPageModel { get; private set;}
        public Analysis_PCSPageModel Analysis_PCSPageModel { get; private set;}
        public Analysis_SmartMeterPageModel Analysis_SmartMeterPageModel { get; private set; }
        public FaultLogPageModel FaultLogPageModel {  get; private set; }
        public Strategy_AnalysisPageModel Strategy_AnalysisPageModel {  get; private set; }
        public Strategy_ProtectSetterPageModel Strategy_ProtectSetterPageModel {  get; private set; }
        public Strategy_SetterPageModel Strategy_SetterPageModel { get; private set; }
        public System_DevInfoPageModel System_DevInfoPageModel {  get; private set; }
        public System_DevSetterPageModel System_DevSetterPageModel { get; private set; }
        public System_MqttSetterPageModel System_MqttSetterPageModel { get; private set; }

        public BMSDataService[] services { get; private set; }

        private static int TotalCount = 6;
        public EMSMainViewModel()
        {
            Monitor_BMSPageModel = new Monitor_BMSPageModel(services);

            services = new BMSDataService[TotalCount];
            for (int i = 0; i < services.Length; i++)
            {
                services[i] = new BMSDataService();
            }
        }

    }
}
