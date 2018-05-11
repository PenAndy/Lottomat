using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface.Base;

namespace TrendChartSDK.Interface.TrendMiss
{
    /// <summary>
    /// 遗漏配置
    /// </summary>
    public interface ITrendMissItemService : IRepository<TrendMissItemInfo>
    {
        /// <summary>
        /// 根据走势图ID和期号获取实体
        /// </summary>
        /// <param name="chartId">走势图ID</param>
        /// <returns></returns>
        TrendMissItemInfo GetMissEntity(int chartId);
    }
}