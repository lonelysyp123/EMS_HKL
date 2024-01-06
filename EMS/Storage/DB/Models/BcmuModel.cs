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
        public int Id { set; get; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { set; get; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { set; get; }

        /// <summary>
        /// 采集周期
        /// </summary>
        public int AcquisitionCycle { set; get; }

        public BcmuModel() { }
    }
}
