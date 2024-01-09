using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service
{
    public interface IDataService
    {
        void StartDaqData();
        void StopDaqData();
        void StartSaveData();
        void StopSaveData();
    }
}
