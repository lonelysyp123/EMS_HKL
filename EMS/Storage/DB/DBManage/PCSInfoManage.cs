using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EMS.Storage.DB.DBManage
{
    public class PCSInfoManage : IManage<PCSInfoModel>
    {
        public bool Delete(PCSInfoModel entity)
        {
            return false;
        }

        public bool DeleteAll()
        {
            return false;
        }

        public List<PCSInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PCSInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }



        public bool Insert(PCSInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PCSInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Insert(PCSInfoModel[] entities)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    for (int i = 0; i < entities.Length; i++)
                    {
                        var result = db.PCSInfos.Add(entities[i]);
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

        public bool Update(PCSInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PCSInfos.Attach(entity);
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

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public List<PCSInfoModel> Find(DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.PCSInfos.Where(p => p.HappenTime >= StartTime && p.HappenTime <= EndTime).ToList();
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

