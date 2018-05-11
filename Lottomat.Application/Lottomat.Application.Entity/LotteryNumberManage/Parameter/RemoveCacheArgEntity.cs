namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// API缓存管理
    /// </summary>
    public class RemoveCacheArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 缓存Key
        /// <para>广告：Advertisement_Html_{0}，0-主站 1-开奖号 2-手机站</para>
        /// <para>友情链接：FriendLink</para>
        /// <para>玩法、规则：IntroductionOfLottery</para>
        /// <para>系统字典：dataItemCache</para>
        /// <para>推荐彩种：__IsRecommend___{0}，QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种</para>
        /// <para>热门彩种：__IsHot__{0}，QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种</para>
        /// <para>二级页面显示彩种：__IsShowHomePage__{0}，QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种</para>
        /// <para>专题文章：__ThematicArticleClassify</para>
        /// <para>彩吧工具（暂时废弃）：__ColorBarTools__</para>
        /// <para>首页推荐5条新闻：HttpResponseMessage_GetHomeNewsList</para>
        /// <para>彩种类型编码：TypeCodeAndEnumCodeDict</para>
        /// <para>特殊球字典：SpecialLotteryBallDict</para>
        /// </summary>
        public string CacheKey { get; set; }
    }
}