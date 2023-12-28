using TNCN.EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Model;
using log4net;
using TNCN.EMS.Common.Util;

namespace TestEMS.ApiTest
{
    [TestClass]
    public class MqttTest
    {
        //创建log对象
        ILog ilog = LogManager.GetLogger(typeof(MqttTest));

        [TestMethod]
        public void TestPublishMessage() {
            List<string> topics = new List<string>();
            topics.Add("/tncn/test/cf/thing/event/property/post_reply");
            topics.Add("/tncn/test/cf/thing/service/property/set");
            MqttConnectInfoModel mqttConnectInfo = new MqttConnectInfoModel("116.62.159.155", 1883, "admin", "zhny2020", "cftest", 55);
            mqttConnectInfo.Topics = topics;
            MqttClientService mqttClientService = new MqttClientService();
            mqttClientService.StartMqttClient(mqttConnectInfo);
            bool result = mqttClientService.PublishAsync("/tncn/test/cf/thing/event/property/post_reply", Encoding.UTF8.GetBytes("post message"));
            Assert.IsTrue(result == true);
            Thread.Sleep(1000);
            ConcurrentQueueLength<SubscribeMessageModel> subscribeMessageModels = mqttClientService.GetSubscribeMessageModels();
            Assert.IsTrue(subscribeMessageModels.Count > 0);
        }

        [TestMethod]
        public void TestAlarm()
        {
            MqttConnectInfoModel mqttConnectInfo = new MqttConnectInfoModel("116.62.159.155", 1883, "admin", "zhny2020", "cftest", 55);
            mqttConnectInfo.Topics = new List<string>();
            MqttClientService mqttClientService = new MqttClientService();
            mqttClientService.StartMqttClient(mqttConnectInfo);

            for (int i=0;i < 3;i++) {
                if (mqttClientService.IsConnected())
                {
                    mqttClientService.DisconnectMqttClient();
                    Thread.Sleep(3000);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            Assert.IsTrue(mqttClientService.IsAlarm() == true);
        }
    }
}
