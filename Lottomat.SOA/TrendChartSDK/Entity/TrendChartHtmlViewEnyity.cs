using System.Collections.Generic;

namespace TrendChartSDK.Entity
{
    /// <summary>
    /// 走势图html返回实体
    /// </summary>
    public class TrendChartHtmlViewEnyity
    {
        /// <summary>
        /// 走势图名称
        /// </summary>
        public string ChartName { get; set; }
        /// <summary>
        /// 三要素标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 三要素关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 三要素描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 年份，多个用|隔开
        /// </summary>
        public string SearchYear { get; set; }
        /// <summary>
        /// 走势图表格Html
        /// </summary>
        public string TrendChartHtml { get; set; }
        /// <summary>
        /// 帮助
        /// </summary>
        public string Help { get; set; }
        /// <summary>
        /// 智能推荐内容
        /// </summary>
        public string TrendSmart { get; set; }
    }
}