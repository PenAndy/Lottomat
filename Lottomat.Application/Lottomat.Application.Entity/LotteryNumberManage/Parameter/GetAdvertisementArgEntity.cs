namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 获取广告列表
    /// </summary>
    public class GetAdvertisementArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 0-主站 1-开奖号 2-手机站
        /// </summary>
        public string Which { get; set; }
    }
}