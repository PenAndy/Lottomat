using Lottomat.Util.Extension;

namespace Lottomat.Application.Code
{
    /// <summary>
    /// 缓存Key
    /// </summary>
    public enum CacheKeyEnum
    {
        /// <summary>
        /// 友情链接
        /// </summary>
        [Text("友情链接")]
        FriendLink,

        /// <summary>
        /// 彩票的玩法、介绍、中奖规则
        /// </summary>
        [Text("彩票的玩法、介绍、中奖规则")]
        IntroductionOfLottery,

        /// <summary>
        /// 查询最新一期开奖信息Sql语句，根据表名
        /// </summary>
        [Text("查询最新一期开奖信息SQL语句，根据表名")]
        QueryNewestLotteryDataSQLByTableName,

        /// <summary>
        /// 查询历史期开奖信息SQL语句，根据表名
        /// </summary>
        [Text("查询历史期开奖信息SQL语句，根据表名")]
        QueryHistoryLotteryDataSQLByTableName,

        /// <summary>
        /// 查询最新一期开奖信息Sql语句，根据期数
        /// </summary>
        [Text("查询最新一期开奖信息SQL语句，根据期数")]
        QueryNewestLotteryDataSQLByTerm,

        /// <summary>
        /// 彩种类型码-枚举码字典
        /// </summary>
        [Text("彩种类型码-枚举码字典")]
        TypeCodeAndEnumCodeDict,
        /// <summary>
        /// 特殊球字典
        /// </summary>
        [Text("特殊球字典")]
        SpecialLotteryBallDict,
        /// <summary>
        /// 系统三要素
        /// </summary>
        [Text("系统三要素")]
        SystemThreeElements,
    }
}