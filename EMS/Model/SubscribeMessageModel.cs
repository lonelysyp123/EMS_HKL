using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Model
{
    public class SubscribeMessageModel
    {
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get { return clientId; } }
        private string clientId;
        /// <summary>
        /// 消息的主题
        /// </summary>
        public string Topic { get { return topic; } }
        private string topic;
        /// <summary>
        /// 消息的字节流
        /// </summary>
        public byte[] Payload { get { return payload; } }
        private byte[] payload;

        public SubscribeMessageModel(string clientId, string topic, byte[] payload) {
            this.clientId = clientId;
            this.topic = topic;
            this.payload = payload;
        }
    }
}
