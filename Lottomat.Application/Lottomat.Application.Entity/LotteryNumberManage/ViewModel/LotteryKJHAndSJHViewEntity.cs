using System.Collections.Generic;

namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 开机号、试机号
    /// </summary>
    public class LotteryKJHAndSJHViewEntity
    {
        /// <summary>
        /// 期数
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 彩种枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Addtime { get; set; }
        /// <summary>
        /// 开机号、试机号
        /// </summary>
        public List<KJHAndSJHItem> KjhAndSjhItems { get; set; }

    }

    public class KJHAndSJHItem
    {
        /// <summary>
        /// 1-开机号 2-试机号 3-开奖号
        /// </summary>
        public int Which { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 开机号、试机号列表
        /// </summary>
        public List<LotteryBallItem> LotteryBallItems { get; set; }
        
        /// <summary>
        /// 大小比
        /// </summary>
        public string SizeRatio { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
        /// <summary>
        /// 和值
        /// </summary>
        public string SumValue { get; set; }
    }
}