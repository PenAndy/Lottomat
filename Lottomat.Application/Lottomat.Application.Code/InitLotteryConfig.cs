using System;
using System.Collections.Generic;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Code
{
    /// <summary>
    /// 初始化开奖号配置文件
    /// </summary>
    public class InitLotteryConfig
    {
        private static List<SCCConfig> cache = null;
        private static readonly object _lock = new object();

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <returns></returns>
        public static List<SCCConfig> Init()
        {
            if (cache == null)
            {
                lock (_lock)
                {
                    if (cache == null)
                    {
                        string configFile = AppDomain.CurrentDomain.BaseDirectory + "XmlConfig\\SCCConfig.xml";
                        cache = configFile.ConvertXMLToObject<SCCConfig>("SCCSettings");
                    }
                }
            }
            return cache;
        }
    }
}