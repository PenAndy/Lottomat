using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendChartManager
{
    /// <summary>
    /// 走势图常用
    /// </summary>
    public class TrendChartUtils
    {
        /// <summary>
        /// 返回走势图项处理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IChartItem<T> GetTrendChartInterface<T>(ChartItemClassName className) where T : LotteryOpenCode
        {
            switch (className)
            {
                case ChartItemClassName.SingleValue:
                    return new SingleValueItem<T>();
                case ChartItemClassName.MultiValue:
                    return new MultiValueItem<T>();
                case ChartItemClassName.SpecialValue:
                    return new SpecialValueItem<T>();
            }
            return null;
        }
    }
}
