using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EMS.Storage.DB.DBManage
{
    public  class PCSInfoManage:IManage<PCSInfoModel>
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
}
}

