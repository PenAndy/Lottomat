using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendChart
{
    /// <summary>
    /// 走势图类型
    /// </summary>
    public class TrendTypeInfo : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public string Path { get; set; }
    }
}