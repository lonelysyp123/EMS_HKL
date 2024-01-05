using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class PcsModel
    {
        /// <summary>
        /// 簇id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 采集周期
        /// </summary>
        public int AcquisitionCycle { get; set; }

        public PcsModel()
        {
        }
    }
}
