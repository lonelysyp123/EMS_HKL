﻿using EMSCommon.Storage.DB.Models;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMSCommon.Storage.DB
{
    public  class ORMContext : DbContext
    {
        public ORMContext()
            : base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = "LocalDb.db",
                    ForeignKeys = true
                }.ConnectionString
            }, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // 如果不存在数据库，则创建之
            Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<ORMContext>(modelBuilder));
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<DaqConfigurationModel> SystemConfigurations { get; set; }
        /// <summary>
        /// 设备连接信息
        /// </summary>
        public DbSet<DevConnectInfoModel> DevConnectInfos { get; set; }
        /// <summary>
        /// 电池总簇电池
        /// </summary>
        public DbSet<TotalBatteryInfoModel> TotalBatteryInfos { get; set; }
        /// <summary>
        /// 电池串电池
        /// </summary>
        public DbSet<SeriesBatteryInfoModel> SeriesBatteryModelInfos { get; set; }
        public DbSet<AlarmParameterSettingModel> AlarmParameterSettingInfos { get; set; }
        public DbSet<AlarmandFaultInfoModel> AlarmandFaultInfos { get;set; }
        public DbSet<PCSControlSettingModel> PCSControlSettingInfos { get; set; }
        public DbSet<PCSInfoModel> PCSInfos { get; set; }
        public DbSet<PCSStrategyDailyPatternInfoModel> pCSStrategyDailyPatternInfos { get; set; }
       
    }
}