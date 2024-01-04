using CommunityToolkit.Mvvm.Input;
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

		private int _userID;
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserID
		{
			get => _userID;
			set
			{
				SetProperty(ref _userID, value);
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
		public RelayCommand MQTTConnectCommand { get; private set; }
		#endregion

		public System_MqttSetterPageModel()
		{
            MQTTConnectCommand=new RelayCommand(MQTTConnect);
			MQTTConfigSaveCommand=new RelayCommand(MQTTConfigSave);
        }

        private void MQTTConnect()
		{

		}

		private void MQTTConfigSave()
		{

		}
    }
}
