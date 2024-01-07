using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace TNCN.EMS.Common.Util
{
    public enum IniSectionEnum
    {
        BMS = 1,
        EMS = 2,
        MQTT = 3,
        PCS = 4,
        SmartMeter = 5,
        Strategy = 6,
    }

    public class IniFileHelper
    {
        private string _path; //配置文件路径
        private const string DefaultStringValue = "";
        private const int DefaultIntegerValue = 0;
        private const double DefaultDoubleValue = 0;
        private const bool DefaultBoolValue = false;

        public IniFileHelper(string path)
        {
            _path = path;
            _sectionEnum2String = new Dictionary<IniSectionEnum, string>();
            _sectionEnum2String.Add(IniSectionEnum.BMS, "BMS");
            _sectionEnum2String.Add(IniSectionEnum.EMS, "EMS");
            _sectionEnum2String.Add(IniSectionEnum.MQTT, "MQTT");
            _sectionEnum2String.Add(IniSectionEnum.PCS, "PCS");
            _sectionEnum2String.Add(IniSectionEnum.SmartMeter, "SmartMeter");
            _sectionEnum2String.Add(IniSectionEnum.Strategy, "Strategy");
        }
        public void WriteString(IniSectionEnum section, string key, string value)
        {
            string sectionName = _sectionEnum2String[section];
            WritePrivateProfileString(sectionName, key, value, _path);
        }

        public void WriteInteger(IniSectionEnum section, string key, int value)
        {
            string sectionName = _sectionEnum2String[section];
            WriteInteger(sectionName, key, value, _path);
        }
        public void WriteDouble(IniSectionEnum section, string key, double value)
        {
            string sectionName = _sectionEnum2String[section];
            WritePrivateProfileString(sectionName, key, value.ToString(), _path);
        }

        public void WriteBoolean(IniSectionEnum section, string key, bool value)
        {
            string sectionName = _sectionEnum2String[section];
            WriteBoolean(sectionName, key, value, _path);
        }

        public string ReadString(IniSectionEnum section, string key) {
            string sectionName = _sectionEnum2String[section];
            return ReadString(sectionName, key, DefaultStringValue, _path);
        }

        public int ReadInteger(IniSectionEnum section, string key)
        {
            string sectionName = _sectionEnum2String[section];
            return ReadInteger(sectionName, key, DefaultIntegerValue, _path);
        }

        public bool ReadBoolean(IniSectionEnum section, string key)
        {
            string sectionName = _sectionEnum2String[section];
            return ReadBoolean(sectionName, key, DefaultBoolValue, _path);
        }

        public double ReadDouble(IniSectionEnum section, string key)
        {
            string sectionName = _sectionEnum2String[section];
            StringBuilder stringBuffer = new StringBuilder(255);
            GetPrivateProfileString(sectionName, key, "", stringBuffer, 255, _path);
            double result = DefaultDoubleValue;
            try {
                result = Convert.ToDouble(stringBuffer.ToString());
            }catch (Exception e) {
                Console.WriteLine(e.ToString());
                result = DefaultDoubleValue;
            }
            return result;
        }

        private const int MAX_BUFFER = 32767;
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileInt", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

        private Dictionary<IniSectionEnum, string> _sectionEnum2String;


        /// <summary>
        /// 向INI写入数据
        /// </summary>
        /// <PARAM name="section">节点名</PARAM>
        /// <PARAM name="key">键名</PARAM>
        /// <PARAM name="value">值（字符串）</PARAM>
        public static void Write(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        /// <summary>
        /// 读取INI数据
        /// </summary>
        /// <PARAM name="Section">节点名</PARAM>
        /// <PARAM name="Key">键名</PARAM>
        /// <PARAM name="Path">值名</PARAM>
        /// <returns>值（字符串）</returns>
        public static string Read(string section, string key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue" />
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadString(string sectionName, string keyName, string defaultValue, string path)
        {
            const int MAXSIZE = 255;
            StringBuilder temp = new StringBuilder(MAXSIZE);
            GetPrivateProfileString(sectionName, keyName, defaultValue, temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteString(string sectionName, string keyName, string value, string path)
        {
            WritePrivateProfileString(sectionName, keyName, value, path);
        }

        /// <summary>
        /// 读取整数
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int ReadInteger(string sectionName, string keyName, int defaultValue, string path)
        {
            return GetPrivateProfileInt(sectionName, keyName, defaultValue, path);
        }

        /// <summary>
        /// 写入整数
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteInteger(string sectionName, string keyName, int value, string path)
        {
            WritePrivateProfileString(sectionName, keyName, value.ToString(), path);
        }

        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ReadBoolean(string sectionName, string keyName, bool defaultValue, string path)
        {
            int temp = defaultValue ? 1 : 0;
            int result = GetPrivateProfileInt(sectionName, keyName, temp, path);
            return (result == 0 ? false : true);
        }

        /// <summary>
        /// 写入布尔值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteBoolean(string sectionName, string keyName, bool value, string path)
        {
            string temp = value ? "1 " : "0 ";
            WritePrivateProfileString(sectionName, keyName, temp, path);
        }

        /// <summary>
        /// 删除指定项
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="path"></param>
        public static void DeleteKey(string sectionName, string keyName, string path)
        {
            WritePrivateProfileString(sectionName, keyName, null, path);
        }

        /// <summary>
        /// 删除指定段下的所有项
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="path"></param>
        public static void EraseSection(string sectionName, string path)
        {
            WritePrivateProfileString(sectionName, null, null, path);
        }

        /// <summary>
        /// 批量写入多条配置
        /// </summary>
        /// <param name="section"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void AddSectionWithKeyValues(string section, List<string> keys, List<string> values, string path)
        {
            //添加配置信息
            for (int i = 0; i < keys.Count; i++)
            {
                WriteString(section, keys[i], values[i], path);
            }
        }
    }
}

