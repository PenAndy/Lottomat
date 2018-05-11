using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.QGC
{
    /// <summary>
    /// 福彩3D
    /// </summary>
    public class QG_FC3D : LotteryOpenCode
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
