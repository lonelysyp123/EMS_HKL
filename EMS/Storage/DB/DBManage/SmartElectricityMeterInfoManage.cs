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

        public bool Insert(SmartElectricityMeterInfoModel[] entities)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    for (int i = 0; i < entities.Length; i++)
                    {
                        var result = db.SmartElectricityMeterInfos.Add(entities[i]);
                    }
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
