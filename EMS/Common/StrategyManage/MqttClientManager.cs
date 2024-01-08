using EMS.Api;
using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Mqtt;
using TNCN.EMS.Model;
using TNCN.EMS.Service;
using Newtonsoft.Json;
using System.Threading;
using EMS.Properties;
using EMS.Service;
using EMS.Service.impl;
using EMS.Storage.DB.Models;

namespace TNCN.EMS.Common.StrategyManage
{
    public class MqttClientManager
    {
        IMqttClientService mqttClientService;

        public IMqttClientService MqttClientService { get { return mqttClientService; } }

        ISystemSettingService systemSettingService;

        public MqttClientManager() {

            mqttClientService.ConnectMqtt();
            Task task = new Task(() => { PublishAsync(); });
            task.Start();
        }

        private void PublishBmsData() {
            BatteryTotalModel[] batteryTotalModels = BmsApi.GetNextBMSData();
            string batteryPostTopic = "hkl2/ems/bms/bcmu/post";
            if (batteryTotalModels != null && batteryTotalModels.Length > 0)
            {
                foreach (BatteryTotalModel batteryTotalModel in batteryTotalModels)
                {
                    if (batteryTotalModel != null)
                    {
                        BCMU bcmu = new BCMU(batteryTotalModel);
                        byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bcmu));
                        mqttClientService.PublishAsync(batteryPostTopic, data);
                    }
                }
                Thread.Sleep(30000);
            }
            else
            {
                Thread.Sleep(10);
            }
        }

        private void PublishPcsData() { 
        }

        private void PublishSmartMeterData() { 
        }

        private void PublishAsync() {
            while (true)
            {
                PublishBmsData();
                PublishPcsData();
                PublishSmartMeterData();
            }
        }
    }
}
