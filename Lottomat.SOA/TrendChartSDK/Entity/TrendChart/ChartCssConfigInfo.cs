using System.Collections.Generic;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendChart
{
    /// <summary>
    /// 走势图样式
    /// </summary>
    public class ChartCssConfigInfo : BaseEntity
    {
        /// <summary>
        /// 样式名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 项对应的样式组
        /// </summary>		
        public int FuntionTypeCss { get; set; }

        /// <summary>
        /// 是否有子样式
        /// 0表示取子样式
        /// -1表示没有子样式（取自身）
        /// </summary>		
        public int ParentId { get; set; }

        /// <summary>
        /// 样式开始下标
        /// </summary>		
        public int startNum { get; set; }

        /// <summary>
        /// 样式结束下标
        /// </summary>		
        public int endNum { get; set; }

        /// <summary>
        /// 遗漏样式名称
        /// </summary>		
        public string MissCssName { get; set; }

        /// <summary>
        /// 选中样式名称
        /// </summary>		
        public string NumberCssName { get; set; }

        /// <summary>
        /// 画线样式名称
        /// </summary>		
        public string LineColor { get; set; }

        /// <summary>
        /// 数据分析样式
        /// </summary>		
        public string DataAnalysisCssName { get; set; }

        /// <summary>
        /// 数据分析(出现次数)图片
        /// </summary>		
        public string DataAnalysisImgName { get; set; }

        /// <summary>
        /// 扩展1
        /// </summary>		
        public string Extend1 { get; set; }

        /// <summary>
        /// 扩展2
        /// </summary>		
        public string Extend2 { get; set; }

        /// <summary>
        /// 扩展3
        /// </summary>		
        public string Extend3 { get; set; }

        /// <summary>
        /// 扩展4
        /// </summary>		
        public string Extend4 { get; set; }

        /// <summary>
        /// 扩展5
        /// </summary>		
        public string Extend5 { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string Descript { get; set; }
        /// <summary>
        /// 子样式列表(ParentId=0有数据)
        /// </summary>
        public List<ChartCssConfigInfo> ChildList { get; set; }
    }
}