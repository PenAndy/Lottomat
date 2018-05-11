namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 基础历史开奖号
    /// </summary>
    public class BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string OpenTime { get; set; }
        /// <summary>
        /// 期号
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 开奖号
        /// <para>蓝球：<span class="ball-list blue">05</span></para>
        /// <para>正常球：<span class="ball-list red">05</span></para>
        /// </summary>
        public string NormalOpenCode { get; set; }
    }
}