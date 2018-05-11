using System.Collections.Generic;

namespace Lottomat.Application.Entity.LotteryNumberManage
{
    /// <summary>
    /// 类似福彩3D开奖详情实体
    /// <para>44521734 ^直选|13362|1040,组三|12052|346,组六|0|173 ^5| 7~3~5|1~4~7</para>
    /// </summary>
    public class LikeFC3DKaiJiangDetailEntity
    {
        /// <summary>
        /// 本期投注
        /// </summary>
        public string Bqtz { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public List<KaiJiangItem> KaiJiangItems { get; set; }
        /// <summary>
        /// 金码
        /// </summary>
        public string Jm { get; set; }
        /// <summary>
        /// 关注码
        /// </summary>
        public List<string> Gzm { get; set; }
        /// <summary>
        /// 对应码
        /// </summary>
        public List<string> Dym { get; set; }
    }

    /// <summary>
    /// 类似福彩双色球开奖详情实体
    /// 401556928,614278265^一等奖|19|5952821,二等奖|216|104766,三等奖|2130|3000,四等奖|101551|200,五等奖|1711907|10,六等奖|12485113|5
    /// </summary>
    public class LikeFCSSQKaiJiangDetailEntity
    {
        /// <summary>
        /// 投入金额
        /// </summary>
        public string Trje { get; set; }
        /// <summary>
        /// 滚动金额
        /// </summary>
        public string Gdje { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public List<KaiJiangItem> KaiJiangItems { get; set; }
    }

    /// <summary>
    /// 公共项目
    /// </summary>
    public class KaiJiangItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 注数
        /// </summary>
        public string Total { get; set; }
        /// <summary>
        /// 投注金额
        /// </summary>
        public string TotalMoney { get; set; }
    }
}