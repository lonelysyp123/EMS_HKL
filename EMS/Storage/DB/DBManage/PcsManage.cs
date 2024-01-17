using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class PcsManage : IManage<PcsModel>
    {
        public bool Insert(PcsModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PcsModels.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(PcsModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PcsModels.Attach(entity);
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

        public bool Delete(PcsModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PcsModels.Where(p => p.Ip == entity.Ip).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.PcsModels.Remove(result[i]);
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
                    var result = db.PcsModels.RemoveRange(db.PcsModels);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<PcsModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PcsModels.ToList();
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
