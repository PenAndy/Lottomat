using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.DFC
{
    /// <summary>
    /// 河北好运彩3
    /// </summary>
   public class DF_HYC3_HeBei : LotteryOpenCode
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
    }
}
