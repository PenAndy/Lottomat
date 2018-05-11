namespace TrendChartSDK.Entity.LotterySearchField
{
    /// <summary>
    /// 生成|预览走势图查询彩种数据条件
    /// </summary>
    public class LotterySearchField
    {
        /// <summary>
        /// (组选)号码重复个数；1:组三;2:豹子;
        /// </summary>
        public int NumRepeat { get; set; }
        /// <summary>
        /// 星期几,默认0；1:星期日；2:星期一；3:星期二；4:星期三；5:星期四；6:星期五；7:星期六
        /// </summary>
        public int Week { get; set; }
        /// <summary>
        /// TopSize
        /// </summary>
        public int TopSize { get; set; }
        /// <summary>
        /// 起始期数
        /// </summary>
        public int StartTerm { get; set; }
        /// <summary>
        /// 结束期数
        /// </summary>
        public int EndTerm { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

    }
}