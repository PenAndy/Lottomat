using TrendChartSDK.Entity.TrendChartData;

namespace TrendChartSDK.Entity.LotterySearchField
{
    /// <summary>
    /// 走势图数据查询基类(用于前端)
    /// </summary>
    public class TrendChartSearchField
    {
        /// <summary>
        /// 走势图ID
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 条数 小于等于0时无效
        /// </summary>
        public int Record { get; set; }
        /// <summary>
        /// 起始期数
        /// </summary>
        public int StartTerm { get; set; }
        /// <summary>
        /// 结束期数
        /// </summary>
        public int EndTerm { get; set; }
        /// <summary>
        /// 走势图类型
        /// </summary>
        public TrendChartType ChartType { get; set; }
        /// <summary>
        /// 年份查询
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// (组选)号码重复个数；1:组三;2:豹子;
        /// </summary>
        public int NumRepeat { get; set; }
        /// <summary>
        /// 星期几,默认0；1:星期日；2:星期一；3:星期二；4:星期三；5:星期四；6:星期五；7:星期六
        /// </summary>
        public int Week { get; set; }
    }
}