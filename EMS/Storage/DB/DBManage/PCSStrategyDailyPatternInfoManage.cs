using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    namespace EMS.Storage.DB.DBManage
    {
        public class PCSStrategyDailyPatternInfoManage : IManage<PCSStrategyDailyPatternInfoModel> 
        {
            public bool Insert(PCSStrategyDailyPatternInfoModel entity)
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.Add(entity);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            }

            public bool Delete(PCSStrategyDailyPatternInfoModel entity)
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.Where(p => p.Series == entity.Series).ToList();
                        for (int i = 0; i < result.Count; i++)
                        {
                            db.pCSStrategyDailyPatternInfos.Remove(result[i]);
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

            public bool Delete(int series)
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.Where(p => p.Series == series).ToList();
                        for (int i = 0; i < result.Count; i++)
                        {
                            db.pCSStrategyDailyPatternInfos.Remove(result[i]);
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

            public bool DeleteAll()
            {
                return false;
            }

            public bool Update(PCSStrategyDailyPatternInfoModel entity)
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.Attach(entity);
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

            public List<PCSStrategyDailyPatternInfoModel> Get()
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.ToList();
                        return result;
                    }
                }
                catch
                {
                    return null;
                }
            }

            public List<PCSStrategyDailyPatternInfoModel> Find(int seriesnumber)
            {
                try
                {
                    using (var db = new ORMContext())
                    {
                        var result = db.pCSStrategyDailyPatternInfos.Where(p => p.Series ==seriesnumber).ToList();
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
}

