using System.Collections.Generic;
using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface.Base;

namespace TrendChartSDK.Interface.TrendMiss
{
    /// <summary>
    /// 遗漏
    /// </summary>
    public interface ITrendMissDataService : IRepository<TrendMissDataInfo>
    {
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool BatchSave(IList<TrendMissDataInfo> list);

        /// <summary>
        /// 获取本期遗漏结果集
        /// </summary>
        /// <param name="id">走势图ID</param>
        /// <param name="term">期号</param>
        /// <returns></returns>
        IList<TrendMissDataInfo> GetMissDataList(int id, long term);
    }
}