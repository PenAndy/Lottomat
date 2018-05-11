using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 遗漏常用
    /// </summary>
    public class TrendMissUtils
    {
        /// <summary>
        /// 返回走势图遗漏项处理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IMissItem<T> GetTrendMissClassName<T>(ChartItemClassName className) where T : LotteryOpenCode
        {
            switch (className)
            {
                case ChartItemClassName.SingleValue:
                    return new SingleValueItem<T>();
                case ChartItemClassName.MultiValue:
                    return new MultiValueItem<T>();
            }
            return null;
        }
    }
}
