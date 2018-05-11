using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.DFC
{
    /// <summary>
    /// 广东深圳风采
    /// </summary>
  public class DF_SZFC_GuangDong : LotteryOpenCode
    {
        /// <summary>
        /// 号码1
        /// </summary>
        public int OpenCode1 { get; set; }
        /// <summary>
        /// 号码2
        /// </summary>
        public int OpenCode2 { get; set; }
        /// <summary>
        /// 号码3
        /// </summary>
        public int OpenCode3 { get; set; }
        /// <summary>
        /// 号码4
        /// </summary>
        public int OpenCode4 { get; set; }
        /// <summary>
        /// 号码5
        /// </summary>
        public int OpenCode5 { get; set; }
        /// <summary>
        /// 号码6
        /// </summary>
        public int OpenCode6 { get; set; }
        /// <summary>
        /// 号码7
        /// </summary>
        public int OpenCode7 { get; set; }
        /// <summary>
        /// 号码8
        /// </summary>
        public int OpenCode8 { get; set; }
    }
}
