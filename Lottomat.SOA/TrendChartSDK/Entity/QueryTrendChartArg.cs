using System;

namespace TrendChartSDK.Entity
{
    /// <summary>
    /// 请求走势图参数
    /// </summary>
    public class QueryTrendChartArg
    {
        /// <summary>
        /// 走势图ID
        /// </summary>
        public int ChatrId { get; set; }

        /// <summary>
        /// 年份，默认当前年份
        /// </summary>
        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// 总记录数，默认30条
        /// </summary>
        public int TotalRecord { get; set; } = 30;

        /// <summary>
        /// 开始期数
        /// </summary>
        public string StartTerm { get; set; }

        /// <summary>
        /// 结束期数
        /// </summary>
        public string EndTerm { get; set; }

        /// <summary>
        /// 走势类型 1-电脑端 2-手机端，默认电脑端
        /// </summary>
        public int ChartType { get; set; } = 1;
    }
}