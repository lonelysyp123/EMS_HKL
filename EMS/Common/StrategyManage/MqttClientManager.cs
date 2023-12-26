using EMS.Model;
using EMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Model;
using TNCN.EMS.Service;

namespace TNCN.EMS.Common.StrategyManage
{
    public class MqttClientManager
    {
        IMqttClientService mqttClientService;

        public IMqttClientService MqttClientService { get { return mqttClientService; } }
        public MqttClientManager() {
            //TODO （MQTT brocker配置从配置文件获取）
            List<string> topics = new List<string>();
            topics.Add("/tncn/ems/strategy/thing/event/property/set");
            MqttConnectInfoModel mqttConnectInfo = new MqttConnectInfoModel("116.62.159.155", 1883, "admin", "zhny2020", "cftest", 55);
            mqttConnectInfo.Topics = topics;
            mqttClientService = new MqttClientService();
            mqttClientService.StartMqttClient(mqttConnectInfo);

            BatteryTotalModel batteryTotalModel = new BatteryTotalModel();
            string publishTopic = "";
            byte[] publishData = Encoding.UTF8.GetBytes();
            EnergyManagementSystem.GlobalInstance.MqttClientManager.MqttClientService.PublishAsync(publishTopic, data);
        }
        
    }
}
