using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;
using Lottomat.Util.Extension;
using Lottomat.Utils.HttpHelper;
using Newtonsoft.Json.Linq;

namespace Lottomat.Utils.Web
{
    /// <summary>
    /// 网络操作
    /// </summary>
    public class NetHelper
    {
        #region Ip(获取Ip)

        /// <summary>
        /// 获取Ip
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                    result = GetWebClientIp();
                if (result.IsEmpty())
                    result = GetLanIp();
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端的Ip
        /// </summary>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        public static string GetWebRemoteIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        #endregion

        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名
        /// </summary>
        public static string Host
        {
            get
            {
                return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName();
            }
        }

        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal)
                return string.Empty;
            var ip = GetWebRemoteIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }

        #endregion

        #region Browser(获取浏览器信息)

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }

        #endregion

        #region 通过IP得到IP所在地省市

        /// <summary>        
        /// 通过IP得到IP所在地省市       
        /// </summary>        
        /// <param name="ip"></param>        
        /// <returns></returns>        
        public static string GetAddressByIP(string ip)
        {
            string res = String.Empty;
            try
            {
                string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ip;

                HttpItem item = new HttpItem
                {
                    Url = url,
                    Method = "POST",
                    Accept = "application/json",
                    ContentType = "application/x-www-form-urlencoded"
                };

                HttpHelper.HttpHelper helper = new HttpHelper.HttpHelper();
                //得到请求结果
                string result = helper.GetHtml(item).Html;
                result = ConvertUnicode2Chinese(result);

                JObject obj = (JObject)result.JsonToObject();
                if (obj != null)
                {
                    //{ "ret":1,"start":"115.28.0.0","end":"115.29.255.255","country":"中国","province":"北京","city":"北京","district":"","isp":"电信","type":"机房","desc":"中国万网机房电信"}

                    res = obj["country"].ToString() + obj["province"].ToString() + obj["city"].ToString();
                }
            }
            catch (Exception)
            {
                res = "";
            }
            return res;
        }

        /// <summary>
        /// 将Unicode编码转换成中文
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string ConvertUnicode2Chinese(string result)
        {
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            return reg.Replace(result, delegate (Match m)
            {
                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
            });
        } 
        #endregion
    }
}
