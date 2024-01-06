using CommunityToolkit.Mvvm.Input;
using EMS.Service.impl;
using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel.NewEMSViewModel
{
	public class System_MqttSetterPageModel : ViewModelBase
	{
		#region ObservableObject
		private string _ip;
		/// <summary>
		/// MQTT-IP
		/// </summary>
		public string IP
		{
			get => _ip;
			set
			{
				SetProperty(ref _ip, value);
			}
		}


		private int _port;
		/// <summary>
		/// Mqtt-port
		/// </summary>
		public int Port
		{
			get => _port;
			set
			{
				SetProperty(ref _port, value);
			}
		}

		private string _clientId;
		/// <summary>
		/// 客户端ID
		/// </summary>
		public string ClientId
		{
			get => _clientId;
			set
			{
				SetProperty(ref _clientId, value);
			}
		}

		private string _username;
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username
		{
			get => _username;
			set
			{
				SetProperty(ref _username, value);
			}
		}

		private string _password;
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get => _password;
			set
			{
				SetProperty(ref _password, value);
			}
		}
		#endregion

		#region Command
		public RelayCommand MQTTConfigSaveCommand { get; private set; }
        public SystemSettingService SystemSettingService { get; set; }
        #endregion

        public System_MqttSetterPageModel()
		{
			MQTTConfigSaveCommand = new RelayCommand(MQTTConfigSave);
            SystemSettingService = new SystemSettingService();
			InitMqtt();

        }

        private void InitMqtt()
        {
            List<MqttModel> mqttModels = SystemSettingService.GetMqttInfo();
            if (mqttModels != null && mqttModels.Count > 0)
            {
                MqttModel mqttModel = mqttModels.Find(item => item.Id == 1);
                if (mqttModel != null)
                {
					IP = mqttModel.Ip;
					Port = mqttModel.Port;
                    ClientId = mqttModel.ClientId;
					Username = mqttModel.UserName; 
					Password = mqttModel.Password;
                }
            }
        }

        private void MQTTConfigSave()
		{
			SystemSettingService.AddMqtt(1, _ip, _port, _clientId, _username, _password);
        }
    }
}
