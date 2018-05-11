using System;
using System.Collections.Generic;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.LotterySearchField;

namespace TrendChartSDK.Interface.Base
{
    /// <summary>
    /// 基类服务
    /// </summary>
    public interface IBaseService<T> where T : class ,new ()
    {
        /// <summary>
        /// 最新开奖号码-限数据抓取使用
        /// </summary>
        /// <returns></returns>
        LotteryOpenCode GetLatestOpenCodeForCatch();
        /// <summary>
        /// 倒序取TOP多少条数据
        /// </summary>
        /// <param name="fields">彩种开奖数据额外查询条件</param>
        /// <returns></returns>
        IList<T> GetTopListDesc(LotterySearchField fields);
        /// <summary>
        /// 获取小于等于term的最近两期开奖号数据
        /// </summary>
        /// <param name="term">期数</param>
        /// <param name="fields">彩种开奖数据额外查询条件</param>
        /// <returns></returns>
        IList<T> ToListForTrend(long term, LotterySearchField fields);
        /// <summary>
        /// 获取term期以及后面数据 
        /// </summary>
        /// <param name="term">期数</param>
        /// <param name="fields">彩种开奖数据额外查询条件</param>
        /// <returns></returns>
        IList<T> GetListToEnd(long term, LotterySearchField fields);
        /// <summary>
        /// 根据年份查询
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        IList<T> GetListByYear(int year);
        /// <summary>
        /// 获取指定日期的开奖数据
        /// </summary>
        /// <param name="date">指定日期(如2016-08-01)</param>
        /// <returns></returns>
        IList<T> GetListByDate(DateTime date);
    }
}