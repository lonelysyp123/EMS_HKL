using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class PublishMessageModel
    {
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

        public PublishMessageModel(string topic, byte[] payload) { 
            this.topic = topic;
            this.payload = payload;
        }
    }
}
