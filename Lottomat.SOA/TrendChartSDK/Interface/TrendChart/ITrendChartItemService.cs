using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Interface.Base;

namespace TrendChartSDK.Interface.TrendChart
{
    /// <summary>
    /// 走势图配置
    /// </summary>
    public interface ITrendChartItemService : IRepository<TrendChartItemInfo>
    {
        /// <summary>
        /// 根据ChartId删除走势图对应配置列表
        /// </summary>
        /// <param name="chartId"></param>
        /// <returns></returns>
        bool DeleteList(int chartId);
    }
}