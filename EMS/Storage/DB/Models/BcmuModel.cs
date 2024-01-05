using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class BcmuModel
    {
        /// <summary>
        /// 簇id
        /// </summary>
        [Key]
        public string BcmuId { set; get; }
        private string bcmuId;
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { set; get; }
        private string ip;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { set; get; }
        private int port;

        /// <summary>
        /// 采集周期
        /// </summary>
        public int AcquisitionCycle { set; get; }
        private int acquisitionCycle;

        public BcmuModel() { }

        public BcmuModel(string bcmuId, string ip, int port, int acquisitionCycle)
        {
            this.bcmuId = bcmuId;
            this.ip = ip;
            this.port = port;
            this.acquisitionCycle = acquisitionCycle;
        }
    }
}
