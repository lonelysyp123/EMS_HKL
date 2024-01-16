using EMS.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TNCN.EMS.Common.Util;

namespace EMS.Service
{
    public class DataServiceBase<TModel> : IDataService where TModel : class
    {
        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            protected set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        private bool _isDaqData;
        public bool IsDaqData
        {
            get => _isDaqData;
            protected set
            {
                if (_isDaqData != value)
                {
                    _isDaqData = value;
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        private bool _isSaveDaq;
        public bool IsSaveDaq
        {
            get => _isSaveDaq;
            protected set
            {
                if (_isSaveDaq != value)
                {
                    _isSaveDaq = value;
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        protected int ReconnectInterval;
        protected int MaxReconnectTimes;
        protected int DaqTimeSpan;
        protected int ReconnectIntervalLong;
        protected int SaveSamplingInterval;
        protected int ModelsLength;
        protected int SaveModelsLength;
        protected string DevType;

        protected Action<object, bool, bool, bool> OnChangeState;
        protected Action<object, object> OnChangeData;
        protected BlockingCollection<TModel> Models;
        protected BlockingCollection<TModel> SaveModels;
        protected bool IsCommunicationProtectState = false;

        public TModel CurrentModel { get; protected set; }
        public string ID { get;protected set; }
        

        public DataServiceBase(string id)
        {
            ID = id;
            InitSystemConfig();
            Models = new BlockingCollection<TModel>(new ConcurrentQueue<TModel>(), ModelsLength);
            SaveModels = new BlockingCollection<TModel>(new ConcurrentQueue<TModel>(), SaveModelsLength);
            StartDataService();

        }

        protected virtual void InitSystemConfig()
        {
            IniFileHelper.Read(IniSectionEnum.EMS, "ReconnectInterval", out ReconnectInterval);
            IniFileHelper.Read(IniSectionEnum.EMS, "MaxReconnectTimes", out MaxReconnectTimes);
            IniFileHelper.Read(IniSectionEnum.EMS, "DaqTimeSpan", out DaqTimeSpan);
            IniFileHelper.Read(IniSectionEnum.EMS, "ReconnectIntervalLong", out ReconnectIntervalLong);
            IniFileHelper.Read(IniSectionEnum.EMS, "SaveSamplingInterval", out SaveSamplingInterval);
            IniFileHelper.Read(IniSectionEnum.EMS, "ModelsLength", out ModelsLength);
            IniFileHelper.Read(IniSectionEnum.EMS, "SaveModelsLength", out SaveModelsLength);
        }

        public void StartDaqData()
        {
            if (IsConnected)
            {
                IsDaqData = true;
                Thread th = new Thread(DaqDataTh);
                th.IsBackground = true;
                th.Start();
                LogUtils.Debug(DevType + " ID:" + ID + " 开始采集数据");
            }
        }

        protected virtual void DaqDataTh()
        {

        }

        public void StopDaqData()
        {
            IsDaqData = false;
            LogUtils.Debug(DevType+" ID:" + ID + " 停止采集数据");
        }

        public void StartSaveData()
        {
            IsSaveDaq = true;
            Thread th = new Thread(SaveDataThread);
            th.IsBackground = true;
            th.Start();
            LogUtils.Debug(DevType + " ID:" + ID + " 开始保存数据");
        }

        protected void SaveDataThread()
        {
            while (IsConnected && IsDaqData && IsSaveDaq)
            {
                if (SaveModels.Count > 200)
                {
                    List<TModel> items = new List<TModel>();
                    for (int i = 0; i < 200; i++)
                    {
                        if (SaveModels.TryTake(out TModel model))
                        {
                            items.Add(model);
                        }
                    }
                    SaveData(items.ToArray());
                }

                Thread.Sleep(500);
            }

            List<TModel> models = new List<TModel>();
            while (SaveModels.TryTake(out TModel model))
            {
                models.Add(model);
            }
            SaveData(models.ToArray());
        }

        protected virtual void SaveData(TModel[] models)
        {

        }

        public void StopSaveData()
        {
            IsSaveDaq = false;
            LogUtils.Debug(DevType + " ID:" + ID + " 停止保存数据");
        }

        protected void StartDataService()
        {
            Thread thread = new Thread(TryConnect);
            thread.IsBackground = true;
            thread.Start();
        }

        protected virtual void TryConnect()
        {
            // 底层尝试连接函数
        }

        public void RegisterState(Action<object, bool, bool, bool> action)
        {
            OnChangeState = action;
        }

        public void RegisterState(Action<object, object> action)
        {
            OnChangeData = action;
        }

        public TModel GetCurrentData()
        {
            if (Models.TryTake(out TModel item))
            {
                return item;
            }
            return null;
        }
    }
}
