using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Model
{
    public class MqttConnectInfoModel
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get { return ip; } }
        private string ip;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get { return port; } }
        private int port;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get { return userName; } }
        private string userName;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get { return password; } }
        private string password;
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get { return clientId; } }
        private string clientId;
        /// <summary>
        /// 心跳间隔
        /// </summary>
        public int KeepAlivePeriod { get { return keepAlivePeriod; } }
        private int keepAlivePeriod;
        /// <summary>
        /// 订阅的topic列表
        /// </summary>
        public List<string> Topics { 
            get { return topics; }
            set { topics = value; } 
        }
        private List<string> topics;

        public MqttConnectInfoModel(string ip, int port, string userName, string password, string clientId, int keepAlivePeriod) { 
            this.ip = ip;
            this.port = port;
            this.userName = userName;
            this.password = password;
            this.clientId = clientId;
            this.keepAlivePeriod = keepAlivePeriod;
        }
    }
}
