using System;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Lottery.QTC
{
    /// <summary>
    /// 山东快乐PK3
    /// </summary>
    public class GP_KLPK3_ShanDong : LotteryOpenCode
    {
        #region 字段
        /// <summary>
        /// 扑克牌花色
        /// </summary>
        private static readonly string[] _pokerSuitName = new string[] { "", "fangpian", "meihua", "hongtao", "heitao" };

        #endregion
        
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

        #region 扩展

        public static Tuple<int, int> SplitOpenCodeTo(int openCode)
        {
            if (openCode < 100) throw new ArgumentOutOfRangeException("openCode");
            return Tuple.Create<int, int>(openCode / 100, openCode % 100);
        }

        internal static Tuple<string, string> GetPokerOpenCode(int openCode)
        {
            Tuple<int, int> ptp = SplitOpenCodeTo(openCode);
            return Tuple.Create(_pokerSuitName[ptp.Item1], PokerNumToName((ptp.Item2)));
        }

        internal static string PokerNumToName(int num)
        {
            switch (num)
            {
                case 1: return "A";
                case 11: return "J";
                case 12: return "Q";
                case 13: return "K";
                default: return num.ToString();
            }
        }

        /// <summary>
        /// 判断是否为豹子
        /// </summary>
        /// <param name="opencodes"></param>
        /// <returns></returns>
        public static bool IsAllSame(params int[] opencodes)
        {
            var _feed = -1;
            foreach (var op in opencodes)
            {
                if (_feed == -1)
                {
                    _feed = op;
                    continue;
                }
                if (op != _feed)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为对子
        /// </summary>
        /// <param name="opencodes"></param>
        /// <returns></returns>
        public static bool IsPair(params int[] opencodes)
        {
            for (int i = 0, n = opencodes.Length; i < n; i++)
            {
                var op1 = opencodes[i];
                for (int j = i + 1; j < n; j++)
                {
                    var op2 = opencodes[j];
                    if (op1 == op2) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否为顺子（前提是不重复数）
        /// </summary>
        /// <param name="opencodes"></param>
        /// <returns></returns>
        public static bool IsStraight(params int[] opencodes)
        {
            int min = 0, max = 0;
            var len = opencodes.Length;
            for (int i = 0; i < len; i++)
            {
                var op = opencodes[i];
                if (i == 0)
                {
                    min = max = op;
                    continue;
                }
                if (min > op)
                {
                    min = op;
                    continue;
                }
                if (max < op)
                    max = op;
            }
            return (max - min) == len - 1;
        }

        public static string GetPokerReleaseName(int[] openCodes, bool isAllSameSuit)
        {
            if (IsAllSame(openCodes))
                return "豹子";

            if (IsPair(openCodes))
                return "对子";

            if (IsStraight(openCodes))
            {
                if (isAllSameSuit)
                    return "同花顺";
                return "顺子";
            }
            if (isAllSameSuit)
                return "同花";

            return "散牌";
        }

        /// <summary>
        /// 出牌大小名称
        /// </summary>
        /// <returns></returns>
        public string PokerShowName()
        {
            var tp1 = SplitOpenCodeTo(OpenCode1);
            var tp2 = SplitOpenCodeTo(OpenCode2);
            var tp3 = SplitOpenCodeTo(OpenCode3);
            var openCodes = new int[] { tp1.Item2, tp2.Item2, tp3.Item2 };
            var isAllSameSuit = (tp1.Item1 == tp2.Item1 && tp1.Item1 == tp3.Item1);
            return GetPokerReleaseName(openCodes, isAllSameSuit);
        }

        /// <summary>
        /// 开奖号码（花色/排号）
        /// </summary>
        public Tuple<string, string> TpOpenCode1 { get { return GetPokerOpenCode(OpenCode1); } }
        public Tuple<string, string> TpOpenCode2 { get { return GetPokerOpenCode(OpenCode2); } }
        public Tuple<string, string> TpOpenCode3 { get { return GetPokerOpenCode(OpenCode3); } }

        #endregion
    }
}
