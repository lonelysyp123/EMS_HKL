using EMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;
using TNCN.EMS.Model;

namespace TNCN.EMS.Api
{
    public static class MqttClientApi
    {
        public static bool IsConnected() { 
            return EnergyManagementSystem.GlobalInstance.MqttClientManager.MqttClientService.IsConnected(); 
        }

        public static bool IsAlarm()
        { 
            return EnergyManagementSystem.GlobalInstance.MqttClientManager.MqttClientService.IsAlarm(); 
        }
        public static ConcurrentQueueLength<SubscribeMessageModel> GetSubscribeMessageModels() {
            return EnergyManagementSystem.GlobalInstance.MqttClientManager.MqttClientService.GetSubscribeMessageModels();
        }


    }
}
