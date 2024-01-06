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
    public class SmartMeterManage : IManage<SmartMeterDBModel>
    {
        public bool Insert(SmartMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartMeterModels.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(SmartMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartMeterModels.Attach(entity);
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

        public bool Delete(SmartMeterDBModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartMeterModels.Where(p => p.Id == entity.Id).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.SmartMeterModels.Remove(result[i]);
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
                    var result = db.SmartMeterModels.RemoveRange(db.SmartMeterModels);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<SmartMeterDBModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SmartMeterModels.ToList();
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
