using System.Collections.Generic;

namespace TrendChartSDK.Entity
{
    /// <summary>
    /// 走势图大厅视图模型
    /// </summary>
    public class TrendChartListViewEnyity
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 彩种列表
        /// </summary>
        public List<TrendChartItems> LotteryItemses { get; set; }
    }

    /// <summary>
    /// 项目
    /// </summary>
    public class TrendChartItems
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 走势图名称
        /// </summary>
        public string TrendChartName { get; set; }
        /// <summary>
        /// 枚举码，备用
        /// </summary>
        public string EnumCode { get; set; }
    }
}