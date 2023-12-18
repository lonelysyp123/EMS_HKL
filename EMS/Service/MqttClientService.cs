using EMS.Model;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNCN.EMS.Model;

namespace TNCN.EMS.Service
{
    public class MqttClientService
    {
        private static IMqttClient mqttClient;
        private bool isConnected = false;
        private MqttConnectInfoModel mqttConnectInfo;
        private ConcurrentQueue<SubscribeMessageModel> subscribeMessageModels;

        public MqttClientService() {
            subscribeMessageModels = new ConcurrentQueue<SubscribeMessageModel>();
        }

        /// <summary>
        /// 创建mqtt客户端
        /// </summary>
        /// <param name="mqttConnectInfo">配置类</param>
        public void StartMqttClient(MqttConnectInfoModel mqttConnectInfo)
        {
            this.mqttConnectInfo = mqttConnectInfo;
            var optionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttConnectInfo.Ip, mqttConnectInfo.Port) // 要访问的mqtt服务端的 ip 和 端口号
                .WithCredentials(mqttConnectInfo.UserName, mqttConnectInfo.Password) // 要访问的mqtt服务端的用户名和密码
                .WithClientId(mqttConnectInfo.ClientId) // 设置客户端id
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(mqttConnectInfo.KeepAlivePeriod))
                .WithCleanSession()
                .WithTls(new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = false  // 是否使用 tls加密
                });

            var clientOptions = optionsBuilder.Build();
            mqttClient = new MqttFactory().CreateMqttClient();

            mqttClient.ConnectedAsync += MqttClientConnectedAsync; // 客户端连接成功事件
            mqttClient.DisconnectedAsync += MqttClientDisconnectedAsync; // 客户端连接关闭事件
            mqttClient.ApplicationMessageReceivedAsync += MqttClientApplicationMessageReceivedAsync; // 收到消息事件

            mqttClient.ConnectAsync(clientOptions);
        }

        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task MqttClientDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已断开与服务端的连接……");
            this.isConnected = false;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task MqttClientConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已连接服务端……");
            this.isConnected = true;
            // 订阅消息主题
            mqttConnectInfo.Topics.ForEach(topic => {
                mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task MqttClientApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine($"ApplicationMessageReceivedAsync：客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            subscribeMessageModels.Enqueue(new SubscribeMessageModel(arg.ClientId, arg.ApplicationMessage.Topic, arg.ApplicationMessage.Payload));
            return Task.CompletedTask;
        }

        /// <summary>
        /// 发布消息到指定topic
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data">发布消息的字节流</param>
        public bool Publish(string topic, byte[] data, MqttQualityOfServiceLevel mqttQualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce)
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.isConnected)
                {
                    var message = new MqttApplicationMessage
                    {
                        Topic = topic,
                        Payload = data,
                        QualityOfServiceLevel = mqttQualityOfServiceLevel,
                        Retain = false  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
                    };
                    mqttClient.PublishAsync(message);
                    return true;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            return false;
        }
    }
}
