﻿namespace Lottomat.Application.Code
{
    /// <summary>
    /// 数据同步服务配置项(字段名需与配置文件中节点名一致)
    /// </summary>
    public class SCCConfig
    {
        /// <summary>
        /// 设置项名称(唯一标识)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 枚举码(唯一标识)
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 所属地区
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 参考地址
        /// </summary>
        public string MainUrl { get; set; }
        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryName { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string KJTime { get; set; }
        /// <summary>
        /// 开始小时数
        /// </summary>
        public int StartHour { get; set; }
        /// <summary>
        /// 开始分钟数
        /// </summary>
        public int StartMinute { get; set; }
        /// <summary>
        /// 开奖间隔分钟数
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 每天开奖期数
        /// </summary>
        public int TimesPerDay { get; set; }
        /// <summary>
        /// 开奖频率
        /// </summary>
        public string KJRate { get; set; }
        /// <summary>
        /// 跳过日期(如节假日不开奖的彩种设置此属性)
        /// </summary>
        public string SkipDate { get; set; }
    }
}
