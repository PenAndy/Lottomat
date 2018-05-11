using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.QGC
{
    /// <summary>
    /// 排列三
    /// </summary>
   public class QG_TCP3 : LotteryOpenCode
    {
        /// <summary>
        /// 号码1
        /// </summary>
        public int OpenCode1 { get; set; }
        /// <summary>
        /// 号码2
        /// </summary>
        public int OpenCode2 { get; set; }
        /// <summary>
        /// 号码3
        /// </summary>
        public int OpenCode3 { get; set; }
    }
}
