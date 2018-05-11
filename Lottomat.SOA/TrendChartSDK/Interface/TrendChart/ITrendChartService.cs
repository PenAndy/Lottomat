using System.Collections.Generic;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Interface.Base;

namespace TrendChartSDK.Interface.TrendChart
{
    /// <summary>
    /// 走势图基本信息
    /// </summary>
    public interface ITrendChartService : IRepository<TrendChartInfo>
    {
        /// <summary>
        /// 根据部分走势图信息获取完整实体类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TrendChartInfo GetEntity(TrendChartInfo entity);
        /// <summary>
        /// 更新走势图表智能推荐关联
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        void UpdateTrendSmart(int startId, int endId);
        /// <summary>
        /// 获取关联的走势图表信息
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="topSize"></param>
        /// <returns></returns>
        IList<TrendChartInfo> GetTrendSmartList(int chartId, int topSize);
    }
}