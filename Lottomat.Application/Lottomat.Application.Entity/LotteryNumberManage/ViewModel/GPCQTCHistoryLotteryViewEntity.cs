namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 高频彩其他彩历史开奖记录
    /// </summary>
    public class GPCQTCHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 和值
        /// </summary>
        public string SumValue { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
        /// <summary>
        /// 质合比
        /// </summary>
        public string PrimeAndNumberRatio { get; set; }
        /// <summary>
        /// 跨度
        /// </summary>
        public string Span { get; set; }
    }

    /// <summary>
    /// 快乐扑克3历史开奖记录
    /// </summary>
    public class GPCKLPK3HistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 和值
        /// </summary>
        public string SumValue { get; set; }
    }
}