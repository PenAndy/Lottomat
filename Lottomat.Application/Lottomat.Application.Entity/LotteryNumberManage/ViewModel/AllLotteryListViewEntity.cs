using System.Collections.Generic;

namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 所有彩种列表
    /// </summary>
    public class AllLotteryListViewEntity
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 彩种列表
        /// </summary>
        public List<LotteryItems> LotteryItemses { get; set; }
    }
}