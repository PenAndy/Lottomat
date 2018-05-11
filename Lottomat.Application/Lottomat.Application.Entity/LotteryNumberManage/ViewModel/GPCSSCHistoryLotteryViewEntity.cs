namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 高频彩时时彩历史开奖记录
    /// </summary>
    public class GPCSSCHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 跨度
        /// </summary>
        public string Span { get; set; }
        /// <summary>
        /// 和值
        /// </summary>
        public string SumValue { get; set; }
    }
}