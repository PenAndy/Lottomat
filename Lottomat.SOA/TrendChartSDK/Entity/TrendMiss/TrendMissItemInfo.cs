using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;

namespace TrendChartSDK.Entity.TrendMiss
{
    /// <summary>
    /// 遗漏配置
    /// </summary>
    public class TrendMissItemInfo : BaseEntity
    {
        /// <summary>
        /// 走势图、遗漏、工具ID
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// 项周期
        /// </summary>
        public string[] ItemCycle { get; set; }
        /// <summary>
        /// 项值范围最小值
        /// 若项值为整形数据则必填，否为-1
        /// </summary>
        public int ItemMinValue { get; set; }
        /// <summary>
        /// 项值范围最大值
        /// 若项值为整形数据则必填，否为-1
        /// </summary>
        public int ItemMaxValue { get; set; }
        /// <summary>
        /// 项个数
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// 项字符串数据
        /// </summary>
        public string[] ItemString { get; set; }
        /// <summary>
        /// 中间值以区别大小
        /// 大于等于splitNumber算大数
        /// </summary>
        public int SplitNumberOfDX { get; set; }
        /// <summary>
        /// 起始索引
        /// 计算项值时的起始索引号
        /// 特殊值-1表示IndexStart无效
        /// 注：单值项时仅IndexStart有效
        public int IndexStart { get; set; }
        /// <summary>
        /// 结束索引
        /// 计算项值时的结束索引号
        /// 特殊值-1表示IndexEnd无效
        /// </summary>
        public int IndexEnd { get; set; }
        /// <summary>
        /// 项值函数类型(决定项值的计算方式)
        /// </summary>
        public MissItemType FuntionType { get; set; }
        /// <summary>
        /// 项处理类类型
        /// </summary>
        public ChartItemClassName ClassName { get; set; }
    }

    /// <summary>
    /// 项值函数类型(决定项值的计算方式)
    /// </summary>
    public enum MissItemType
    {
        /// <summary>
        /// 多值开奖号项(排列类彩种)
        /// </summary>
        MultiValue_OpenCodeItem = 1,
        /// <summary>
        /// 单值和值项
        /// </summary>
        SingleValue_Sum = 2,
        /// <summary>
        /// 单值奇偶项
        /// </summary>
        SingleValue_JOItem = 3,
        /// <summary>
        /// 单值大小项
        /// </summary>
        SingleValue_DXItem = 4,
        /// <summary>
        /// 单值质合项
        /// </summary>
        SingleValue_ZHItem = 5,
        /// <summary>
        /// 单值跨度项
        /// </summary>
        SingleValue_KDItem = 6,
        /// <summary>
        /// 单值012路项
        /// </summary>
        SingleValue_012Item = 7,
        /// <summary>
        /// 单值（和尾）项
        /// </summary>
        SingleValue_HWItem = 8
    }
}