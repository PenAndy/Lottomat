using System;

namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 今日开奖彩种
    /// </summary>
    public class TodaysLotteryViewEntity
    {
        /// <summary>
        /// 彩种名称
        /// </summary>
        public string LotteryName { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 开机时间
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// 参考地址
        /// </summary>
        public string MainUrl { get; set; }
        /// <summary>
        /// 枚举码
        /// </summary>
        public string EnumCode { get; set; }
    }
}