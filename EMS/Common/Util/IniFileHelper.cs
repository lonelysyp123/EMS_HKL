using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TNCN.EMS.Common.Util
{
    public class IniFileHelper
    {
        private const int MAX_BUFFER = 32767;
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileInt", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

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

