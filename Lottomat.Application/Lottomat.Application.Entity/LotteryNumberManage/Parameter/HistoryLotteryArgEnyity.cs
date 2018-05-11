namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 历史开奖
    /// </summary>
    public class HistoryLotteryArgEnyity : BaseParameterEntity
    {
        /// <summary>
        /// 枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 要返回的总记录数
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 开始时间 格式：yyyy-MM-dd
        /// </summary>
        public string StartTime { get; set; }
    }
}