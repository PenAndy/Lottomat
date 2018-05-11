using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.QTC
{
    /// <summary>
    /// 湖南幸运赛车
    /// </summary>
  public  class GP_XYSC_HuNan : LotteryOpenCode
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
