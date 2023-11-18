using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class StateGridModel : ObservableObject
    {
        private int _id;
        public int ID 
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private ElectrovalenceEnum _electrovalence;
        public ElectrovalenceEnum Electrovalence 
        {
            get => _electrovalence;
            set
            {
                SetProperty(ref _electrovalence, value);
            }
        }

        private double _price;
        public double Price 
        {
            get => _price;
            set
            {
                SetProperty(ref _price, value);
            }
        }

        private string _startTime = "00:00:00";
        public string StartTime 
        {
            get => _startTime;
            set
            {
                SetProperty(ref _startTime, value);
            }
        }

        private string _endTime = "00:00:00";
        public string EndTime 
        {
            get => _endTime; 
            set
            {
                SetProperty(ref _endTime, value);
            }
        }

        public StateGridModel()
        {

        }
    }

    public enum ElectrovalenceEnum
    {
        Peak = 0,       // 峰
        Valley = 1,     // 谷
        Flat = 2,       // 平
        Sharp = 3,      // 尖
    }
}
