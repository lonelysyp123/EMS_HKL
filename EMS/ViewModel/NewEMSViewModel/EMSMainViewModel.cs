﻿using EMS.Model;
using EMS.Service;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;

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
        public Strategy_AnalysisPageModel Strategy_AnalysisPageModel {  get; private set; }
        public Strategy_ProtectSetterPageModel Strategy_ProtectSetterPageModel {  get; private set; }
        public Strategy_SetterPageModel Strategy_SetterPageModel { get; private set; }
        public System_DevInfoPageModel System_DevInfoPageModel {  get; private set; }
        public System_DevSetterPageModel System_DevSetterPageModel { get; private set; }
        public System_MqttSetterPageModel System_MqttSetterPageModel { get; private set; }
        public BMSDataService[] bmsServices { get; private set; }
        public SmartMeterDataService smService { get; private set; }
        public PCSDataService pcsService { get; private set; }
        public SmartElectricityMeterDataService semService { get; private set; }

        private static int BCMUCount = 6;
        public EMSMainViewModel()
        {

            EnergyManagementSystem.Initialization(new EnergyManagementSystem());
            bmsServices = new BMSDataService[BCMUCount];
            for (int i = 0; i < BCMUCount; i++)
            {
                BMSDataService service = new BMSDataService((i + 1).ToString());
                bmsServices[i] = service;
                bmsServices[i].RegisterState(DataCallBack_BMS);
                bmsServices[i].RegisterState(StateCallBack_BMS);
                EnergyManagementSystem.GlobalInstance.BMSManager.AddBMSDev(service);
            }

            pcsService = new PCSDataService("1");
            pcsService.RegisterState(DataCallBack_PCS);
            pcsService.RegisterState(StateCallBack_PCS);
            EnergyManagementSystem.GlobalInstance.PcsManager.SetPCS(pcsService);

            smService = new SmartMeterDataService("1");
            smService.RegisterState(DataCallBack_SM);
            smService.RegisterState(StateCallBack_SM);

            //semService = new SmartElectricityMeterDataService();
            //semService.RegisterState(DataCallBack_SEM);
            //semService.RegisterState(StateCallBack_SEM);

            HomePageModel = new HomePageModel();
            Monitor_BMSPageModel = new Monitor_BMSPageModel();
            Monitor_PCSPageModel = new Monitor_PCSPageModel();
            Monitor_SmartMeterPageModel = new Monitor_SmartMeterPageModel();
            Analysis_BMSPageModel = new Analysis_BMSPageModel();
            Analysis_PCSPageModel = new Analysis_PCSPageModel();
            Analysis_SmartMeterPageModel = new Analysis_SmartMeterPageModel();
            Strategy_AnalysisPageModel = new Strategy_AnalysisPageModel();
            Strategy_ProtectSetterPageModel = new Strategy_ProtectSetterPageModel();
            Strategy_SetterPageModel = new Strategy_SetterPageModel();
            System_DevInfoPageModel = new System_DevInfoPageModel();
            System_DevSetterPageModel = new System_DevSetterPageModel();
            System_MqttSetterPageModel = new System_MqttSetterPageModel();
        }

        private void StateCallBack_SM(object sender, bool isConnected, bool isDaqData, bool isSaveData)
        {
            HomePageModel.DataDisPlaySM(isConnected);
        }

        private void DataCallBack_SM(object sender, object model)
        {
            Monitor_SmartMeterPageModel.DataRefresh(model as SmartMeterModel);
        }

        private void DataCallBack_BMS(object sender, object model)
        {
            var service = sender as BMSDataService;
            int index = -1;
            if (service.ID == "1")
            {
                index = 1; 
                HomePageModel.BMSDataRefreshFromAPI();
            }
            else if (service.ID == "2") index = 2;
            else if (service.ID == "3") index = 3;
            else if (service.ID == "4") index = 4;
            else if (service.ID == "5") index = 5;
            else if (service.ID == "6") index = 6;

            Monitor_BMSPageModel.bmuViewModels[index - 1].DataDistribution(model as BatteryTotalModel);
        }

        private void StateCallBack_BMS(object sender, bool isConnected, bool isDaqData, bool isSaveData)
        {
            var service = sender as BMSDataService;
            int index = -1;
            if (service.ID == "1") index = 1;
            else if (service.ID == "2") index = 2;
            else if (service.ID == "3") index = 3;
            else if (service.ID == "4") index = 4;
            else if (service.ID == "5") index = 5;
            else if (service.ID == "6") index = 6;
            HomePageModel.StateDisPlayCloud();
            HomePageModel.StateDisPlayFault();
            Monitor_BMSPageModel.bmuViewModels[index - 1].StateDistribution(isConnected, isDaqData, isSaveData);
        }

        private void DataCallBack_PCS(object sender, object model)
        {
            HomePageModel.DataDisPlayPCS(model as PCSModel);
            Monitor_PCSPageModel.PCSDataDistribution(model as PCSModel);
        }

        private void StateCallBack_PCS(object sender, bool isConnected, bool isDaqData, bool isSaveData)
        {
            HomePageModel.StateDisPlayPCS(isConnected);
            Monitor_PCSPageModel.PCSStateDistribution(isConnected, isDaqData, isSaveData);
        }

        private void DataCallBack_SEM(object sender, object model)
        {
            HomePageModel.DataRefresh_SEM(model as SmartElectricityMeterModel);
        }

        private void StateCallBack_SEM(object sender, bool isConnected, bool isDaqData, bool isSaveData)
        {
            HomePageModel.StateDisPlayPCS(isConnected);
            Monitor_PCSPageModel.PCSStateDistribution(isConnected, isDaqData, isSaveData);
        }
    }
}
