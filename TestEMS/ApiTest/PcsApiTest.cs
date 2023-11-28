using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Api;

namespace TestEMS.ApiTest
{
    [TestClass]
    public class PcsApiTest
    {
        [TestMethod]
        public void TestPcsSetAction()
        {
            
            Assert.AreEqual(true, PcsApi.PcsSetAction(1, 1, null));
        }
    }
}
