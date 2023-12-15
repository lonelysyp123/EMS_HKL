using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Api;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", ConfigFileExtension = "config", Watch = true)]
namespace TestEMS.ApiTest
{

    [TestClass]
    public class PcsApiTest
    {
        [TestMethod]
        public void TestPcsSetAction()
        {
            log4net.Config.XmlConfigurator.Configure(); // log4net
            Assert.AreEqual(true, PcsApi.SetPCSHalt());
        }
    }
}
