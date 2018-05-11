namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 根据彩种编码获取对应推荐的彩种开奖详情
    /// </summary>
    public class HomeLotteryListByTypeCodeArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 彩种类型码
        /// <para>QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种</para>
        /// </summary>
        public string TypeCode { get; set; }
    }
}