using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class PCSControlInfoManage :IManage<PCSControlSettingModel>
    { 
    public bool Delete(PCSControlSettingModel entity)
    {
        return false;
    }

    public bool DeleteAll()
    {
        return false;
    }

    public List<PCSControlSettingModel> Get()
    {
        try
        {
            using (var db = new ORMContext())
            {
                var result = db.PCSControlSettingInfos.ToList();
                return result;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="StartTime">开始时间</param>
    /// <param name="EndTime">结束时间</param>
    /// <returns>数据列表</returns>
    public List<PCSControlSettingModel> Find(int id)
    {
        try
        {
            using (var db = new ORMContext())
            {
                var result = db.PCSControlSettingInfos.Where(p => p.id ==id).ToList();
                return result;
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="BCMUID">BCMUID</param>
    /// <param name="BMUID">BMUID</param>
    /// <param name="StartTime">开始时间</param>
    /// <param name="EndTime">结束时间</param>
    /// <returns>数据列表</returns>
   
    

    public bool Insert(PCSControlSettingModel entity)
    {
        try
        {
            using (var db = new ORMContext())
            {
                var result = db.PCSControlSettingInfos.Add(entity);
                db.SaveChanges();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool Update(PCSControlSettingModel entity)
    {
        try
        {
            using (var db = new ORMContext())
            {
                var result = db.PCSControlSettingInfos.Attach(entity);
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

