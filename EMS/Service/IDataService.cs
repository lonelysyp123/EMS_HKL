using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service
{
    public interface IDataService<T>
    {
        /// <summary>
        /// 同步链接
        /// </summary>
        /// <returns>是否链接成功</returns>
        bool Connect();

        /// <summary>
        /// 异步链接
        /// </summary>
        /// <returns>是否链接成功</returns>
        Task<bool> ConnectAsync();

        /// <summary>
        /// 同步断开链接
        /// </summary>
        /// <returns>是否断开成功</returns>
        bool Disconnect();

        /// <summary>
        /// 开始采集数据
        /// </summary>
        void StartDaqData();

        /// <summary>
        /// 停止采集数据
        /// </summary>
        void StopDaqData();

        /// <summary>
        /// 设置采集间隔
        /// </summary>
        /// <param name="interval">间隔时间/ms</param>
        void SetDaqTimeSpan(int interval);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据包</returns>
        T GetNextData();
    }
}
