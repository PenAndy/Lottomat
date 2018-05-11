using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.TrendChartModel
{
    public class TrendChart_Preview
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public string TrendChartType { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 文章集合表
        /// </summary>
        public List<TempTrendChartPreviewItem> TrendChartPreviewItems { get; set; }
    }

    public class TempTrendChartPreviewItem
    {
        public string TrendChartChildType { get; set; }
        public string TrendChartChildName { get; set; }

        public List<TrendChartPreviewItem> TrendChartPreviewItem { get; set; }
    }

    public class TrendChartPreviewItem
    {
        /// <summary>
        /// 走势图
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 走势图连接的url
        /// </summary>
        /// <returns></returns>
        public string TrendChartUrl { get; set; }
    }
}
