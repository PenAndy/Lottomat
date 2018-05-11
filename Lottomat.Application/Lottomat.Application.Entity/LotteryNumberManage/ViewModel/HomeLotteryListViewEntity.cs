using System;
using System.Collections.Generic;
using Lottomat.Application.Code;

namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 获取首页展示详细开奖信息的彩种
    /// </summary>
    public class HomeLotteryListViewEntity
    {
        /// <summary>
        /// 类型码
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 彩票名称
        /// </summary>
        public string LotteryName { get; set; }
        /// <summary>
        /// 当前期数
        /// </summary>
        public string CurrentTerm { get; set; }
        /// <summary>
        /// 当天总共期数
        /// </summary>
        //public int CurrentTotalTerm { get; set; }
        /// <summary>
        /// 每周那几天开奖
        /// </summary>
        //public string[] OpenThePrizeOnTheDayOfTheWeek { get; set; }
        /// <summary>
        /// 每天开始开奖时间
        /// </summary>
        //public string StartThePrizeTimeEveryDay { get; set; }
        /// <summary>
        /// 当前开奖时间
        /// </summary>
        public string CurrentOpenTime { get; set; }
        /// <summary>
        /// 下一次开奖时间
        /// </summary>
        public string NextOpenTime { get; set; }
        /// <summary>
        /// 服务器当前时间
        /// </summary>
        public string ServerTime { get; set; }
        /// <summary>
        /// 开奖间隔时间，单位：分钟
        /// </summary>
        //public int Interval { get; set; }
        /// <summary>
        /// 开奖频率
        /// </summary>
        public string KJRate { get; set; }
        /// <summary>
        /// 开奖号列表
        /// </summary>
        public List<LotteryBallItem> LotteryBallItems { get; set; }

        /// <summary>
        /// 开奖详情三要素->Keywords
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 开奖详情三要素->Description
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 开奖详情
        /// </summary>
        public string Spare { get; set; }

        ///// <summary>
        ///// 开奖详情
        ///// </summary>
        //public LikeFCSSQKaiJiangDetailEntity KaiJiangDetailItem { get; set; }
    }

    /// <summary>
    /// 开奖号
    /// </summary>
    public class LotteryBallItem
    {
        /// <summary>
        /// 开奖号
        /// </summary>
        public string OpenCode { get; set; }
        /// <summary>
        /// 球类型
        /// </summary>
        public LotteryBallType BallType { get; set; }
    }
}