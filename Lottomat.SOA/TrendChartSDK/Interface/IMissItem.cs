using System.Collections.Generic;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendMiss;

namespace TrendChartSDK.Interface
{
    /// <summary>
    /// 遗漏
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IMissItem<TEntity> where TEntity : LotteryOpenCode
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="itemConfig">遗漏配置</param>
        /// <param name="missData">遗漏数据</param>
        void Init(TrendMissItemInfo itemConfig, IList<TrendMissDataInfo> missData);
        /// <summary>
        /// 项值及遗漏计算
        /// </summary>
        /// <param name="entity"></param>
        bool SetItemValue(TEntity entity);
        /// <summary>
        /// 保存项值及遗漏数据
        /// </summary>
        bool SaveData();
        /// <summary>
        /// 获取上期或本期遗漏数据
        /// </summary>
        /// <returns></returns>
        IList<TrendMissDataInfo> GetMissDataList();
    }
}