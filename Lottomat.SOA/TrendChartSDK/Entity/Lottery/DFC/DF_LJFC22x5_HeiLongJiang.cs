using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.DFC
{
    /// <summary>
    /// 黑龙江22选5
    /// </summary>
   public class DF_LJFC22x5_HeiLongJiang : LotteryOpenCode
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
    }
}
