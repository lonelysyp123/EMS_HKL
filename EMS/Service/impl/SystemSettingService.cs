using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.impl
{
    public class SystemSettingService
    {
        public SystemSettingService() { }
        public bool AddBcmu(string bcmuId, string ip, int port, int acquisitionCycle) {
            BcmuModel bcmuModel = new BcmuModel();
            bcmuModel.BcmuId = bcmuId;
            bcmuModel.Ip = ip;
            bcmuModel.Port = port;
            bcmuModel.AcquisitionCycle = acquisitionCycle;
            BcmuManage bmsConfigInfo = new BcmuManage();
            bmsConfigInfo.Insert(bcmuModel);
            return true;
        }

        public List<BcmuModel> GetBcmu()
        {
            BcmuManage bmsConfigInfo = new BcmuManage();
            return bmsConfigInfo.Get();
        }
    }
}
