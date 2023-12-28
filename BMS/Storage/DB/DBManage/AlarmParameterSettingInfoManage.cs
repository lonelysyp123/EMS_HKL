using BMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Storage.DB.DBManage
{
    public class AlarmParameterSettingInfoManage:IManage<AlarmParameterSettingModel>
    {
        public bool Insert(AlarmParameterSettingModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmParameterSettingInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(AlarmParameterSettingModel entity)
        {
            return false;
        }

        public bool DeleteAll()
        {
            return false;
        }

        public bool Update(AlarmParameterSettingModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmParameterSettingInfos.Attach(entity);
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

        public List<AlarmParameterSettingModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmParameterSettingInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<AlarmParameterSettingModel> Find(string bcmuid)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.AlarmParameterSettingInfos.Where(p => p.BCMUID==bcmuid).ToList();
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


  