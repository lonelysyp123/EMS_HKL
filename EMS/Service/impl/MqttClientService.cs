using EMS.Model;
using EMS.Service;
using log4net;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;
using TNCN.EMS.Model;

namespace TNCN.EMS.Service
{
    public class MqttClientService : IMqttClientService
    {
        //创建log对象
        private ILog ilog = LogManager.GetLogger(typeof(MqttClientService));
        private const int MAX_QUEUE_SZIE = 300;
        private static IMqttClient mqttClient;
        private bool isConnected = false;
        private MqttConnectInfoModel mqttConnectInfo;
        private bool isAlarm = false;
        /// <summary>
        /// 订阅的消息队列
        /// </summary>
        private ConcurrentQueueLength<SubscribeMessageModel> subscribeMessageModels;
        /// <summary>
        /// 发布的消息队列
        /// </summary>
        private ConcurrentQueueLength<PublishMessageModel> publishMessageModels;

        public MqttClientService() {
            this.subscribeMessageModels = new ConcurrentQueueLength<SubscribeMessageModel>(MAX_QUEUE_SZIE);
            this.publishMessageModels = new ConcurrentQueueLength<PublishMessageModel>(MAX_QUEUE_SZIE);
        }

        public ConcurrentQueueLength<SubscribeMessageModel> GetSubscribeMessageModels() { 
            return subscribeMessageModels;
        }
        public void DisconnectMqttClient() {
            mqttClient.DisconnectAsync();
        }
        private void StartMqttClient(MqttConnectInfoModel mqttConnectInfo)
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
            ilog.Info($"客户端已断开与服务端的连接……");
            this.isConnected = false;
            this.isAlarm = true;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task MqttClientConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            ilog.Info($"客户端已连接服务端……");
            this.isConnected = true;
            this.isAlarm = false;

            // 订阅消息主题
            mqttConnectInfo.Topics.ForEach(topic => {
                mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
            });

            Task task = new Task(Publish);
            task.Start();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 接收订阅的消息
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task MqttClientApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            ilog.Debug($"ApplicationMessageReceivedAsync：客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            subscribeMessageModels.Enqueue(new SubscribeMessageModel(arg.ClientId, arg.ApplicationMessage.Topic, arg.ApplicationMessage.Payload));
            return Task.CompletedTask;
        }

        public bool PublishAsync(string topic, byte[] data) {
            if (publishMessageModels.Count < MAX_QUEUE_SZIE) {
                publishMessageModels.Enqueue(new PublishMessageModel(topic, data));
                return true;
            }
            
            return false;
        }


        /// <summary>
        /// 从本地线程安全队列，发布消息到MQTT指定topic
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data">发布消息的字节流</param>
        private void Publish()
        {
            PublishMessageModel publishMessageModel;
            if (publishMessageModels.TryDequeue(out publishMessageModel)) {
                for (int i = 0; i < 3; i++)
                {
                    if (this.isConnected)
                    {
                        var message = new MqttApplicationMessage
                        {
                            Topic = publishMessageModel.Topic,
                            Payload = publishMessageModel.Payload,
                            QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                            Retain = false  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
                        };

                        try
                        {
                            mqttClient.PublishAsync(message);
                        }
                        catch (Exception ex)
                        {
                            ilog.Error(ex.Message);
                            if (ex is MqttCommunicationException) {
                                this.isConnected = false;
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (! this.isConnected)
                {
                    mqttClient.ReconnectAsync();
                    publishMessageModels.Enqueue(publishMessageModel);
                }
            }
        }

        public bool IsAlarm()
        {
            return this.isAlarm;
        }

        public bool IsConnected()
        {
            return this.isConnected;
        }
    }
}
