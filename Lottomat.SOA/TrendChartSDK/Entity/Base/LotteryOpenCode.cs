using System;
using System.Collections.Generic;

namespace TrendChartSDK.Entity.Base
{
    /// <summary>
    /// 彩票开奖彩种基类
    /// </summary>
    public class LotteryOpenCode : BaseEntity
    {
        /// <summary>
        /// 期数
        /// </summary>
        public long Term { get; set; }
        /// <summary>
        /// 开奖号码
        /// </summary>
        public List<int> OpenCode { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// 试机号
        /// </summary>
        public string ShiJiHao { get; set; }
        /// <summary>
        /// 开机号
        /// </summary>
        public string KaiJiHao { get; set; }
        /// <summary>
        /// 开奖详细
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
    }

    /// <summary>
    /// 开奖号型码类：开机号、试机号、开奖号
    /// </summary>
    public enum OpenCodeType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 开机号
        /// </summary>
        KaiJiHao,
        /// <summary>
        /// 试机号
        /// </summary>
        ShiJiHao,
        /// <summary>
        /// 开奖号
        /// </summary>
        KaiJiangHao
    }
}