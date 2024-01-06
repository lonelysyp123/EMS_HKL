using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class MqttManage : IManage<MqttModel>
    {
        public bool Insert(MqttModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.MqttModels.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(MqttModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.MqttModels.Attach(entity);
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

        public bool Delete(MqttModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.MqttModels.Where(p => p.Ip == entity.Ip).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.MqttModels.Remove(result[i]);
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
                    var result = db.MqttModels.RemoveRange(db.MqttModels);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<MqttModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.MqttModels.ToList();
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
