using TNCN.EMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Model;
using log4net;
using TNCN.EMS.Common.Util;
using TNCN.EMS.Common.Mqtt;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers.Interfaces;

namespace TestEMS.ApiTest
{
    [TestClass]
    public class MqttTest
    {
        //创建log对象
        ILog ilog = LogManager.GetLogger(typeof(MqttTest));

        [TestMethod]
        public void TestPublishMessage()
        {
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

            for (int i = 0; i < 3; i++)
            {
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

        [TestMethod]
        public void TestIniWrite()
        {
            string filePath = "E:\\project\\temp\\EMS_HKL\\EMS\\Config\\SystemConfig.ini";
            bool exists = File.Exists(filePath);
            Assert.IsTrue(exists);
            IniFileHelper iniFile = new IniFileHelper(filePath);
            if (exists)
            {
                //IniFileHelper.Write("MQTT", "IP", "127.0.0.1", filePath);
                //IniFileHelper.Write("MQTT", "Port", "1883", filePath);
                //IniFileHelper.Write("MQTT", "UserName", "admin", filePath);
                //IniFileHelper.Write("MQTT", "Password", "zhny2020", filePath);
                //IniFileHelper.Write("MQTT", "ClientId", "tncn.ems.local", filePath);

                string[] keys = { "IP", "Port", "UserName", "Password", "ClientId" };
                string[] values = { "127.0.0.1", "1883", "admin", "zhny2020", "tncn.ems.local" };

                iniFile.AddSectionWithKeyValues("MQTT", keys.ToList(), values.ToList());
            }
        }

        [TestMethod]
        public void TestIniRead()
        {
            string filePath = "E:\\project\\temp\\EMS_HKL\\EMS\\Config\\SystemConfig.ini";
            bool exists = File.Exists(filePath);
            IniFileHelper iniFile = new IniFileHelper(filePath);
            Assert.IsTrue(exists);
            if (exists) {
                string ip = iniFile.ReadString(IniSectionEnum.MQTT, "IP");
                Assert.AreEqual(ip, "127.0.0.1", true);

                string port = iniFile.ReadString(IniSectionEnum.MQTT, "Port");
                Assert.AreEqual(port, port, "1883");

                string userName = iniFile.ReadString(IniSectionEnum.MQTT, "UserName");
                Assert.AreEqual(userName, "admin", true);

                string password = iniFile.ReadString(IniSectionEnum.MQTT, "Password");
                Assert.AreEqual(password, "zhny2020", true);

                string clientId = iniFile.ReadString(IniSectionEnum.MQTT, "ClientId");
                Assert.AreEqual(clientId, "tncn.ems.local", true);
            }

        }

        
    }
}
