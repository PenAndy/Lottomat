using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendChart
{
    /// <summary>
    /// 走势图基本信息
    /// </summary>
    public class TrendChartInfo : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int Cid { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public int Tid { get; set; }
        /// <summary>
        /// 走势图状态
        /// </summary>
        public TrendChartStatus Status { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 页面标题
        /// </summary>
        public string hTitle { get; set; }
        /// <summary>
        /// 页面关键字
        /// </summary>
        public string hKeywords { get; set; }
        /// <summary>
        /// 页面描述
        /// </summary>
        public string hDescription { get; set; }
        /// <summary>
        /// 对应的URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 对应ChartId计算类型
        /// </summary>
        public TrendChartIdType Type { get; set; }
        /// <summary>
        /// 方向(横屏/竖屏) 0:横屏(普通电脑)；1:竖屏(电视)
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateId { get; set; }
    }


    /// <summary>
    /// 走势图状态
    /// </summary>
    public enum TrendChartStatus
    {
        /// <summary>
        /// 测试 预览
        /// </summary>
        Test = -1,
        /// <summary>
        /// 正常
        /// </summary>
        Default = 0,
        /// <summary>
        /// 新走势图
        /// </summary>
        New = 1,
        /// <summary>
        /// 热门走势图
        /// </summary>
        Hot = 2
    }

    /// <summary>
    /// ChartId计算类型
    /// </summary>
    public enum TrendChartIdType
    {
        /// <summary>
        /// 通用配置:直接取结果集数据
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 实时计算：通过项的配置生成实时数据(特殊项)
        /// </summary>
        Abnormal = 1,
        /// <summary>
        /// 静态数据
        /// </summary>
        Static = 2,
        /// <summary>
        /// 高频动态计算数据
        /// </summary>
        Dynamic = 3
    }
}