using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class PCSFaultViewModel
    {


        public PCSFaultViewModel() 
        {

        }

        public void LogFault()
        {
            BlockingCollection<string> logfault = new BlockingCollection<string>(new ConcurrentQueue<string>());
        }
    }
}
