namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 彩票的玩法、介绍、中奖规则实体
    /// </summary>
    public class IntroductionOfLotteryViewEntity
    {
        /// <summary>
        /// 玩法介绍
        /// </summary>
        /// <returns></returns>
        public string Gameplay { get; set; }
        /// <summary>
        /// 开奖规则
        /// </summary>
        /// <returns></returns>
        public string LotteryRule { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        /// <returns></returns>
        public string LotteryTime { get; set; }
        /// <summary>
        /// 中奖规则
        /// </summary>
        /// <returns></returns>
        public string Winning { get; set; }
    }
}