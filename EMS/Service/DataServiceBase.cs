using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Service
{
    public class DataServiceBase<TModel>
    {
        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            private set
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
            private set
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
            private set
            {
                if (_isSaveDaq != value)
                {
                    _isSaveDaq = value;
                    OnChangeState(this, _isConnected, _isDaqData, _isSaveDaq);
                }
            }
        }

        private static int reconnectInterval = 150; // ms
        private static int maxReconnectTimes = 3;
        private int MaxReconnectCount = 3;
        private int DaqTimeSpan = 1;
        private static int reconnectIntervalLong = 60 * 1000 * 5; // ms

        private Action<object, bool, bool, bool> OnChangeState;
        private Action<object, object> OnChangeData;
        private BlockingCollection<TModel> Models;
        private bool IsCommunicationProtectState = false;

        public TModel CurrentModel { get; private set; }
        public string ID { get; private set; }
        

        public DataServiceBase()
        {
            Models = new BlockingCollection<TModel>(new ConcurrentQueue<TModel>(), 300);
        }


    }
}
