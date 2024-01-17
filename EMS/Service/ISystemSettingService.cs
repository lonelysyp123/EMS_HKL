using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service
{
    public interface ISystemSettingService
    {
        List<BcmuModel> GetBcmuList();
        bool AddBcmu(int id, string ip, int port, int acquisitionCycle);
        List<PcsModel> GetPcsList();
        bool AddPcs(int id, string ip, int port, int acquisitionCycle);
        List<MqttModel> GetMqttInfo();
        bool AddMqtt(int id, string ip, int port, string clientId, string userName, string password);
        List<SmartMeterDBModel> GetSmartMeterList();
        bool AddSmartMeter(int id, string selectedCommPort, int selectedBaudRate, int selectedStopBits, int selectedDataBits, int selectedParity, int acquisitionCycle);
    }
}
