using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Utils;
using Lottomat.Utils.HttpHelper;

namespace Lottomat.Application.SystemAutoJob
{
    /// <summary>
    /// 抓取指定彩种最新期号
    /// </summary>
    public class GrabTheLatestAwardManager
    {
        private static HttpHelper helper = new HttpHelper();
        /// <summary>
        /// 缓存
        /// </summary>
        private static Dictionary<string, string[]> cacheDictionary = new Dictionary<string, string[]>();

        /// <summary>
        /// 获取最新期数
        /// </summary>
        /// <param name="scc"></param>
        /// <returns></returns>
        public static string GetTheLatestAward(SCCLottery scc)
        {
            string res = String.Empty;
            //读取配置
            string[] urlAndXPath = GetRequestUrlAndXPath(scc);
            if (!string.IsNullOrEmpty(urlAndXPath[0]) && !string.IsNullOrEmpty(urlAndXPath[1]))
            {
                //组装参数
                HttpItem item = new HttpItem
                {
                    Url = urlAndXPath[0],
                    Method = "GET",
                    ContentType = "text/html",
                    Timeout = 90 * 1000,
                    ReadWriteTimeout = 90 * 1000,
                    Encoding = Encoding.UTF8
                };
                //开始请求
                HttpResult result = helper.GetHtml(item);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    string html = result.Html;
                    if (!string.IsNullOrEmpty(html))
                    {
                        HtmlDocument doc = new HtmlDocument();

                        doc.LoadHtml(html);
                        HtmlNode node = doc.DocumentNode.SelectSingleNode(urlAndXPath[1]);
                        //获取最终想要的数据
                        string text = string.IsNullOrEmpty(node.InnerText) ? node.InnerHtml.ReplaceHtmlTag() : node.InnerText;
                        //只获取数字部分
                        List<string> temp = text.GetValueByRegex("-?[1-9]\\d*");

                        res = temp.Count > 0 ? temp[0] : "";
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 获取抓取地址以及XPath
        /// </summary>
        /// <param name="scc"></param>
        /// <returns></returns>
        public static string[] GetRequestUrlAndXPath(SCCLottery scc)
        {
            if (!cacheDictionary.ContainsKey(scc.ToString()))
            {
                cacheDictionary.Add(scc.ToString(), new[] { ConfigHelper.GetValue("__" + scc.ToString() + "__URL__"), ConfigHelper.GetValue("__" + scc.ToString() + "__XPATH__") });
            }
            else
            {
                cacheDictionary[scc.ToString()] = new[]
                {
                    ConfigHelper.GetValue("__" + scc.ToString() + "__URL__"),
                    ConfigHelper.GetValue("__" + scc.ToString() + "__XPATH__")
                };
            }

            return cacheDictionary[scc.ToString()];
        }
    }
}