using EMS.Storage.DB.DBManage;
using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Controls;

namespace EMS.Service.impl
{
    public class SystemSettingService : ISystemSettingService
    {
        public SystemSettingService() { }

        public List<BcmuModel> GetBcmuList() {
            BcmuManage bcmuManage = new BcmuManage();
            return bcmuManage.Get();
        }
        public bool AddBcmu(int id, string ip, int port, int acquisitionCycle) {
            try
            {
                BcmuModel bcmuModel = new BcmuModel();
                bcmuModel.Id = id;
                bcmuModel.Ip = ip;
                bcmuModel.Port = port;
                bcmuModel.AcquisitionCycle = acquisitionCycle;
                BcmuManage bcmuManage = new BcmuManage();
                List<BcmuModel> bcmuModels = bcmuManage.Get();
                if (bcmuModels != null && bcmuModels.Count > 0)
                {
                    BcmuModel bcmuModel1 = bcmuModels.Find(item => item.Id == id);
                    if (bcmuModel1 == null)
                    {
                        bcmuManage.Insert(bcmuModel);
                    }
                    else
                    {
                        bcmuManage.Update(bcmuModel);
                    }
                }
                else {
                    bcmuManage.Insert(bcmuModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<PcsModel> GetPcsList()
        {
            PcsManage pcsManage = new PcsManage();
            return pcsManage.Get();
        }

        public bool AddPcs(int id, string ip, int port, int acquisitionCycle) {
            try
            {
                PcsModel pcsModel = new PcsModel();
                pcsModel.Id = id;
                pcsModel.Ip = ip;
                pcsModel.Port = port;
                pcsModel.AcquisitionCycle = acquisitionCycle;
                PcsManage pcsManage = new PcsManage();
                List<PcsModel> pcsModels = pcsManage.Get();
                if (pcsModels != null && pcsModels.Count > 0)
                {
                    PcsModel pcsModel1 = pcsModels.Find(item => item.Id == id);
                    if (pcsModel1 == null)
                    {
                        pcsManage.Insert(pcsModel);
                    }
                    else
                    {
                        pcsManage.Update(pcsModel);
                    }
                }
                else {
                    pcsManage.Insert(pcsModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<MqttModel> GetMqttInfo() {
            MqttManage mqttManage = new MqttManage();
            return mqttManage.Get();
        }

        public bool AddMqtt(int id, string ip, int port, string clientId, string userName, string password) {
            try
            {
                MqttModel mqttModel = new MqttModel();
                mqttModel.Id = id;
                mqttModel.Ip = ip; 
                mqttModel.Port = port; 
                mqttModel.ClientId = clientId; 
                mqttModel.UserName = userName; 
                mqttModel.Password = password;
                MqttManage mqttManage = new MqttManage();
                List<MqttModel> mqttModels = mqttManage.Get();
                if (mqttModels != null && mqttModels.Count > 0)
                {
                    MqttModel mqttModel1 = mqttModels.Find(item => item.Id == id);
                    if (mqttModel1 == null)
                    {
                        mqttManage.Insert(mqttModel);
                    }
                    else
                    {
                        mqttManage.Update(mqttModel);
                    }
                }
                else
                {
                    mqttManage.Insert(mqttModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<SmartMeterDBModel> GetSmartMeterList()
        {
            SmartMeterManage smartMeterManage = new SmartMeterManage();
            return smartMeterManage.Get();
        }

        public bool AddSmartMeter(int id, string selectedCommPort, int selectedBaudRate, int selectedStopBits, int selectedDataBits, int selectedParity, int acquisitionCycle)
        {
            try
            {
                SmartMeterDBModel smartMeterDBModel = new SmartMeterDBModel();
                smartMeterDBModel.Id = id;
                smartMeterDBModel.SelectedCommPort = selectedCommPort;
                smartMeterDBModel.SelectedBaudRate = selectedBaudRate;
                smartMeterDBModel.SelectedStopBits = selectedStopBits;
                smartMeterDBModel.SelectedDataBits = selectedDataBits;
                smartMeterDBModel.SelectedParity = selectedParity;
                smartMeterDBModel.AcquisitionCycle = acquisitionCycle;

                SmartMeterManage smartMeterManage = new SmartMeterManage();
                List<SmartMeterDBModel> smartMeterDBModels = smartMeterManage.Get();
                if (smartMeterDBModels != null && smartMeterDBModels.Count > 0)
                {
                    SmartMeterDBModel smartMeterDBModel1 = smartMeterDBModels.Find(item => item.Id == id);
                    if (smartMeterDBModel1 == null)
                    {
                        smartMeterManage.Insert(smartMeterDBModel);
                    }
                    else
                    {
                        smartMeterManage.Update(smartMeterDBModel);
                    }
                }
                else
                {
                    smartMeterManage.Insert(smartMeterDBModel);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
