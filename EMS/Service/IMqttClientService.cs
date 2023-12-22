using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;
using TNCN.EMS.Model;

namespace EMS.Service
{
    public interface IMqttClientService
    {
        /// <summary>
        /// 创建mqtt客户端
        /// </summary>
        /// <param name="mqttConnectInfo">配置类</param>
        void StartMqttClient(MqttConnectInfoModel mqttConnectInfo);
        /// <summary>
        /// 异步发布消息
        /// </summary>
        /// <returns></returns>
        bool PublishAsync(string topic, byte[] data);
        /// <summary>
        /// 获取订阅的消息列表
        /// </summary>
        /// <returns></returns>
        ConcurrentQueueLength<SubscribeMessageModel> GetSubscribeMessageModels();
        /// <summary>
        /// 是否报警，云端通信中断则报警(连接成功再断开)，未连接成功过则不会报警
        /// </summary>
        /// <returns></returns>
        bool IsAlarm();
        /// <summary>
        /// 是否成功连接MQTT Brocker
        /// </summary>
        /// <returns></returns>
        bool IsConnected();
        /// <summary>
        /// 断开mqtt客户端连接
        /// </summary>
        void DisconnectMqttClient();
    }
}
