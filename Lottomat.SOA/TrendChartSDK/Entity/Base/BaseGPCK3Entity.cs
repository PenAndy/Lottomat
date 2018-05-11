namespace TrendChartSDK.Entity.Base
{
    /// <summary>
    /// 快3基类
    /// </summary>
    public class BaseGPCK3Entity : LotteryOpenCode
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