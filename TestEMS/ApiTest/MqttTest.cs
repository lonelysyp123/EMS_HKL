using TNCN.EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Model;

namespace TestEMS.ApiTest
{
    [TestClass]
    public class MqttTest
    {
        [TestMethod]
        public void TestMqttClient() {
            List<string> topics = new List<string>();
            topics.Add("/tncn/test/cf/thing/event/property/post_reply");
            topics.Add("/tncn/test/cf/thing/service/property/set");
            
            MqttConnectInfoModel mqttConnectInfo = new MqttConnectInfoModel("116.62.159.155", 1883, "admin", "zhny2020", "cftest", 55);
            mqttConnectInfo.Topics = topics;
            MqttClientService mqttClientService = new MqttClientService();
            mqttClientService.StartMqttClient(mqttConnectInfo);
            bool result = mqttClientService.Publish("/tncn/test/cf/thing/event/property/post", Encoding.UTF8.GetBytes("cftest"));
            Assert.IsTrue(result);
        }

    }
}
