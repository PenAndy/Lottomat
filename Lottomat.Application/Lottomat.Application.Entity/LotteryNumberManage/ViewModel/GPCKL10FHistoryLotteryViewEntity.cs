﻿namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 高频彩快乐10分历史开奖记录
    /// </summary>
    public class GPCKL10FHistoryLotteryViewEntity : BaseHistoryLotteryViewEntity
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
        /// 大小，形如：大|大|小|大|小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 大小比
        /// </summary>
        public string SizeRatio { get; set; }
        /// <summary>
        /// 奇偶比
        /// </summary>
        public string ParityRatio { get; set; }
        /// <summary>
        /// 质合比
        /// </summary>
        public string PrimeAndNumberRatio { get; set; }
        /// <summary>
        /// 跨度
        /// </summary>
        public string Span { get; set; }
    }
}