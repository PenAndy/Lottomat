using TrendChartSDK.Entity;

namespace TrendChartSDK.TrendChartManager
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class BaseManager
    {
        #region 彩种分类编码
        /// <summary>
        /// 彩种分类编码
        /// </summary>
        protected const string LotteryTypeCodeStr = "QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种";
        #endregion

        /// <summary>
        /// 走势图彩种大厅
        /// </summary>
        /// <returns>Json字符串</returns>
        public abstract string GetTrendChartList();

        /// <summary>
        /// 获取走势图Html
        /// </summary>
        /// <param name="arg">查询参数</param>
        /// <returns></returns>
        public abstract string GetTrendChartHtml(QueryTrendChartArg arg);
    }
}