using System.Collections.Generic;

namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 视图模型，推荐彩种和热门彩种
    /// </summary>
    public class RecommendationAndHotLotteryViewEntity
    {
        /// <summary>
        /// 0-推荐 1-热门
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 彩种列表
        /// </summary>
        public List<LotteryItems> LotteryItemses { get; set; }
    }

    public class LotteryItems
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 简码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 枚举码，备用
        /// </summary>
        public string EnumCode { get; set; }
    }
}