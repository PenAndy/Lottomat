using System;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendChartData
{
    /// <summary>
    /// 走势图数据基类
    /// </summary>
    public class TrendChartData : BaseEntity
    {
        /// <summary>
        /// 走势图ChartId
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public long Term { get; set; }
        /// <summary>
        /// 历史最大遗漏
        /// </summary>
        public string[] AllMaxMiss { get; set; }
        /// <summary>
        /// 历史出现次数
        /// </summary>
        public string[] AllTimes { get; set; }
        /// <summary>
        /// 开奖记录
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 历史平均遗漏
        /// </summary>
        public string[] AllAvgMiss { get; set; }
        /// <summary>
        /// 上期遗漏
        /// </summary>
        public string[] LastMiss { get; set; }
        /// <summary>
        /// 当前遗漏
        /// </summary>
        public string[] LocalMiss { get; set; }
        /// <summary>
        /// HTML代码
        /// </summary>
        public string HtmlData { get; set; }
        /// <summary>
        /// 走势图类型
        /// </summary>
        public TrendChartType ChartType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
    }
}