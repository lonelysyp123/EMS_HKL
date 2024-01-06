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

        public List<BcmuModel> GetBcmuList() {
            BcmuManage bcmuManage = new BcmuManage();
            return bcmuManage.Get();
        }
        public bool AddBcmu(int id, string ip, int port, int acquisitionCycle) {
            try
            {
                BcmuModel bcmuModel = new BcmuModel();
                bcmuModel.Id = id;
                bcmuModel.Ip = ip;
                bcmuModel.Port = port;
                bcmuModel.AcquisitionCycle = acquisitionCycle;
                BcmuManage bcmuManage = new BcmuManage();
                List<BcmuModel> bcmuModels = bcmuManage.Get();
                if (bcmuModels != null && bcmuModels.Count > 0)
                {
                    BcmuModel bcmuModel1 = bcmuModels.Find(item => item.Id == id);
                    if (bcmuModel1 == null)
                    {
                        bcmuManage.Insert(bcmuModel);
                    }
                    else
                    {
                        bcmuManage.Update(bcmuModel);
                    }
                }
                else {
                    bcmuManage.Insert(bcmuModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<PcsModel> GetPcsList()
        {
            PcsManage pcsManage = new PcsManage();
            return pcsManage.Get();
        }

        public bool AddPcs(int id, string ip, int port, int acquisitionCycle) {
            try
            {
                PcsModel pcsModel = new PcsModel();
                pcsModel.Id = id;
                pcsModel.Ip = ip;
                pcsModel.Port = port;
                pcsModel.AcquisitionCycle = acquisitionCycle;
                PcsManage pcsManage = new PcsManage();
                List<PcsModel> pcsModels = pcsManage.Get();
                if (pcsModels != null && pcsModels.Count > 0)
                {
                    PcsModel pcsModel1 = pcsModels.Find(item => item.Id == id);
                    if (pcsModel1 == null)
                    {
                        pcsManage.Insert(pcsModel);
                    }
                    else
                    {
                        pcsManage.Update(pcsModel);
                    }
                }
                else {
                    pcsManage.Insert(pcsModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<BcmuModel> GetBcmu()
        {
            BcmuManage bmsConfigInfo = new BcmuManage();
            return bmsConfigInfo.Get();
        }
    }
}
