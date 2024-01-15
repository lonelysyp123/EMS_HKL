using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public  class AlarmandFaultInfoManage:IManage<AlarmandFaultInfoModel>
    { 
        public bool Insert(AlarmandFaultInfoModel entity)
    {
        try
        {
            using (var db = new ORMContext())
            {
                var result = db.AlarmandFaultInfos.Add(entity);
                db.SaveChanges();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

        public bool Delete(AlarmandFaultInfoModel entity)
        {
            return false;
        }

        public bool DeleteAll() 
        {
            return false;
        }

        public bool Update(AlarmandFaultInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmandFaultInfos.Attach(entity);
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

        public List<AlarmandFaultInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmandFaultInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
    }

