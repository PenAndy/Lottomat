using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendMiss
{
    /// <summary>
    /// 遗漏
    /// </summary>
    public class TrendMissDataInfo : BaseEntity
    {
        /// <summary>
        /// 走势图ID
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public long Term { get; set; }
        /// <summary>
        /// 项值
        /// </summary>
        public string ItemValue { get; set; }
        /// <summary>
        /// 项值对应期数的开奖号的值或形态是否出现
        /// true:中;false:未中
        /// </summary>
        public bool ItemSelect { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 出现次数
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 理论次数
        /// </summary>
        public int TimesTheory { get; set; }
        /// <summary>
        /// 循环周期
        /// </summary>
        public double Cycle { get; set; }
        /// <summary>
        /// 最大遗漏
        /// </summary>
        public int MaxMiss { get; set; }
        /// <summary>
        /// 平均遗漏
        /// </summary>
        public double AvgMiss { get; set; }
        /// <summary>
        /// 上期遗漏
        /// </summary>
        public int LastMiss { get; set; }
        /// <summary>
        /// 本期遗漏
        /// </summary>
        public int LocalMiss { get; set; }
        /// <summary>
        /// 历史最大遗漏
        /// </summary>
        public int LastMaxMiss { get; set; }

        /// <summary>
        /// 连出次数
        /// </summary>
        public int ContinuousTimes { get; set; }
        /// <summary>
        /// 当前连出次数
        /// </summary>
        public int ContinuousLocalTimes { get; set; }
        /// <summary>
        /// 最大连出次数
        /// </summary>
        public int ContinuousMaxTimes { get; set; }
        /// <summary>
        /// 当前连出遗漏
        /// </summary>
        public int ContinuousLocalMiss { get; set; }
        /// <summary>
        /// 最大连出遗漏
        /// </summary>
        public int ContinuousMaxMiss { get; set; }
        /// <summary>
        /// 当前连出几率
        /// </summary>
        public double ContinuousLocalProbability { get; set; }
        /// <summary>
        /// 连出概率
        /// </summary>
        public double ContinuousProbability { get; set; }

        /// <summary>
        /// 出现概率
        /// </summary>
        public double Probability { get; set; }
        /// <summary>
        /// 欲出几率
        /// </summary>
        public double AppearingProbability { get; set; }
        /// <summary>
        /// 投资价值
        /// </summary>
        public double InvestmentValue { get; set; }
        /// <summary>
        /// 回补几率
        /// </summary>
        public double CoveringProbability { get; set; }
    }
}