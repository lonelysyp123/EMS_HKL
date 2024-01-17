using EMS.Model;
using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class SmartElectricityMeterManage : IManage<SmartElectricityMeterDBModel>
    {
        public bool Insert(SmartElectricityMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterModels.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(SmartElectricityMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterModels.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(SmartElectricityMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterModels.Where(p => p.Id == entity.Id).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.SmartElectricityMeterModels.Remove(result[i]);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteAll()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterModels.RemoveRange(db.SmartElectricityMeterModels);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<SmartElectricityMeterDBModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartElectricityMeterModels.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
