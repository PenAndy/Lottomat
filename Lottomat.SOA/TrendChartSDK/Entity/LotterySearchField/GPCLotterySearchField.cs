using System;

namespace TrendChartSDK.Entity.LotterySearchField
{
    /// <summary>
    /// 生成|预览走势图查询高频彩种数据条件
    /// </summary>
    public class GPCLotterySearchField
    {
        /// <summary>
        /// 前N条数据，0不启用
        /// </summary>    
        public int TopSize { get; set; }
        /// <summary>
        /// 起始期数，与EndTerm合用，0不启用
        /// </summary>
        [Obsolete("目前不使用")]
        public int StartTerm { get; set; }
        /// <summary>
        /// 结束期数，与StartTerm合用，0不启用
        /// </summary>
        [Obsolete("目前不使用")]
        public int EndTerm { get; set; }
        /// <summary>
        /// 年份，查询年份，0不启用
        /// </summary>
        [Obsolete("目前不使用")]
        public int Year { get; set; }
        /// <summary>
        /// 查询指定日期，0不启用
        /// </summary>
        public SearchDay SearchDay { get; set; }
        /// <summary>
        /// 查询指定日期，空值不启用，默认为当天
        /// </summary>
        public DateTime? SearchDate { get; set; }
    }

    /// <summary>
    /// 查询指定哪天的数据
    /// </summary>
    [Obsolete("目前不使用")]
    public enum SearchDay
    {
        Ignore = 0,
        ThreeDaysAgo = -3,
        BeforeYesterday = -2,
        Yesterday = -1,
        Today = 1
    }
}