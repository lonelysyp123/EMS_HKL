using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class SmartElectricityMeterInfoManage : IManage<SmartElectricityMeterInfoModel>
    {
        public bool Delete(SmartElectricityMeterInfoModel entity)
        {
            return false;
        }

        public bool DeleteAll()
        {
            return false;
        }

        public List<SmartElectricityMeterInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns>数据列表</returns>
        public List<SmartElectricityMeterInfoModel> Find(DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterInfos.Where(p => p.HappenTime >= StartTime && p.HappenTime <= EndTime).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool Insert(SmartElectricityMeterInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(SmartElectricityMeterInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterInfos.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
