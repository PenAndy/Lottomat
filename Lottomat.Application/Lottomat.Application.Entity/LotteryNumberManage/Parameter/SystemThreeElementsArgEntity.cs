namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 三要素
    /// </summary>
    public class SystemThreeElementsArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 彩票枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 类型 1-二级页面 2-主页面
        /// </summary>
        public string Type { get; set; }
    }
}