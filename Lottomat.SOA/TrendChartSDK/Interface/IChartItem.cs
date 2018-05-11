using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendChartData;

namespace TrendChartSDK.Interface
{
    /// <summary>
    /// 走势图项接口
    /// </summary>
    /// <typeparam name="TEntity">彩种开奖数据</typeparam>
    public interface IChartItem<TEntity> where TEntity : LotteryOpenCode
    {
        /// <summary>
        /// 初始化项
        /// </summary>
        /// <param name="cssConfig">fomart格式</param>
        /// <param name="itemConfig">项配置信息</param>
        void Init(ChartCssConfigInfo cssConfig, TrendChartItemInfo itemConfig);

        /// <summary>
        /// 遗漏相关数据初使化
        /// </summary>
        /// <param name="trendChartCofig">上期遗漏数据</param>
        /// <param name="i">对应计算项</param>
        void MissDataInit(TrendChartData trendChartCofig, int i);

        /// <summary>
        /// 字符串返回遗漏数据(逗号分隔数组)
        /// </summary>
        /// <param name="missDataType">遗漏数据类型</param>
        /// <returns></returns>
        string GetMissData(MissDataType missDataType);

        /// <summary>
        /// 根据彩种开奖数据计算当前项的值(可以是配置项的数组索引号)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SetItemValue(TEntity entity);
        /// <summary>
        /// 根据彩种开奖数据计算当前项和下一项的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nextentity"></param>
        /// <returns></returns>
        bool SetItemValue(TEntity entity, TEntity nextentity);
        /// <summary>
        /// 返回格式化字符串
        /// </summary>
        /// <param name="fomart">Fomart格式</param>
        /// <param name="cssConfig">CSS配置</param>
        /// <returns></returns>
        string GetFomartString(string fomart, ChartCssConfigInfo cssConfig = null);
    }
}