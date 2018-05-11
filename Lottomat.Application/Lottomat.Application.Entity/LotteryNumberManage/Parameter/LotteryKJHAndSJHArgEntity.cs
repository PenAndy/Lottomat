namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 开机号、试机号
    /// </summary>
    public class LotteryKJHAndSJHArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 彩种枚举码
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

    /// <summary>
    /// 查询开机号、试机号
    /// </summary>
    public class QueryLotteryKJHAndSJHArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 彩种枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 要返回的总记录数
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 期号
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 开始期数
        /// </summary>
        public string StartTerm { get; set; }
        /// <summary>
        /// 结束期数
        /// </summary>
        public string EndTerm { get; set; }

    }
}