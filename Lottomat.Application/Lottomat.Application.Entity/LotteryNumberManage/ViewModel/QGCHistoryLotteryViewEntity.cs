namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 全国彩历史记录实体
    /// </summary>
    public class QGCHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 总和特征字符串，形如：14|单|大
        /// </summary>
        public string TheSum { get; set; }
        /// <summary>
        /// 号码奇偶特征字符串，形如：奇|奇|偶|奇|奇|偶|偶
        /// </summary>
        public string Parity { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
    }

    /// <summary>
    /// 福彩3D历史记录实体
    /// </summary>
    public class FC3DHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 试机号
        /// </summary>
        public string ShiJiHao { get; set; }
        /// <summary>
        /// 开机号
        /// </summary>
        public string KaiJiHao { get; set; }
        /// <summary>
        /// 总和特征字符串，形如：14|单|大
        /// </summary>
        public string TheSum { get; set; }
        /// <summary>
        /// 号码奇偶特征字符串，形如：奇|奇|偶|奇|奇|偶|偶
        /// </summary>
        public string Parity { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
        /// <summary>
        /// 大小比
        /// </summary>
        public string SizeRatio { get; set; }
    }

    /// <summary>
    /// 双色球历史记录实体
    /// </summary>
    public class SSQHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 开机号
        /// </summary>
        public string KaiJiHao { get; set; }
        /// <summary>
        /// 总和特征字符串，形如：14|单|大
        /// </summary>
        public string TheSum { get; set; }
        /// <summary>
        /// 号码奇偶特征字符串，形如：奇|奇|偶|奇|奇|偶|偶
        /// </summary>
        public string Parity { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
        /// <summary>
        /// 大小比
        /// </summary>
        public string SizeRatio { get; set; }
    }

    /// <summary>
    /// 七乐彩历史记录实体
    /// </summary>
    public class QLCHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
    {
        /// <summary>
        /// 总和特征字符串，形如：14|单|大
        /// </summary>
        public string TheSum { get; set; }
        /// <summary>
        /// 大小比
        /// </summary>
        public string SizeRatio { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
    }

}