namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 根据彩票枚举码获取最新一期开奖详情
    /// </summary>
    public class TheLatestAwardArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 彩票枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string Term { get; set; }
    }
}