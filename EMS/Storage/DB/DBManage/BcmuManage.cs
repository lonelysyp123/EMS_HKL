using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class BcmuManage : IManage<BcmuModel>
    {
        public bool Insert(BcmuModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.BcmuModels.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(BcmuModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.BcmuModels.Attach(entity);
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

        public bool Delete(BcmuModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.BcmuModels.Where(p => p.Ip == entity.Ip).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.BcmuModels.Remove(result[i]);
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
                    var result = db.BcmuModels.RemoveRange(db.BcmuModels);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<BcmuModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.BcmuModels.ToList();
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
