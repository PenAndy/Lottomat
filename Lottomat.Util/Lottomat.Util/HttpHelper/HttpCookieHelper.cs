using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lottomat.Utils.HttpHelper
{
     /// <summary>
    /// Cookie����������
    /// </summary>
    public static class HttpCookieHelper
    {
        /// <summary>
        /// �����ַ�����Cookie�б�
        /// </summary>
        /// <param name="cookie">Cookie�ַ���</param>
        /// <returns></returns>
        public static List<CookieItem> GetCookieList(string cookie)
        {
            List<CookieItem> cookielist = new List<CookieItem>();
            foreach (string item in cookie.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Regex.IsMatch(item, @"([\s\S]*?)=([\s\S]*?)$"))
                {
                    Match m = Regex.Match(item, @"([\s\S]*?)=([\s\S]*?)$");
                    cookielist.Add(new CookieItem() { Key = m.Groups[1].Value, Value = m.Groups[2].Value });
                }
            }
            return cookielist;
        }

        /// <summary>
        /// ����Keyֵ�õ�Cookieֵ,Key�����ִ�Сд
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="cookie">�ַ���Cookie</param>
        /// <returns></returns>
        public static string GetCookieValue(string Key, string cookie)
        {
            foreach (CookieItem item in GetCookieList(cookie))
            {
                if (item.Key == Key)
                    return item.Value;
            }
            return "";
        }
        /// <summary>
        /// ��ʽ��CookieΪ��׼��ʽ
        /// </summary>
        /// <param name="key">Keyֵ</param>
        /// <param name="value">Valueֵ</param>
        /// <returns></returns>
        public static string CookieFormat(string key, string value)
        {
            return string.Format("{0}={1};", key, value);
        }
    }

    /// <summary>
    /// Cookie����
    /// </summary>
    public class CookieItem
    {
        /// <summary>
        /// ��
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// ֵ
        /// </summary>
        public string Value { get; set; }
    }
}
