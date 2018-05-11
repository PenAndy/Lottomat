using System;

namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 保存广告
    /// </summary>
    public class SaveAdvertisementArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 0-主站 1-开奖号 2-手机站
        /// </summary>
        public string Which { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime OverTime { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public int Position { get; set; }

    }
}