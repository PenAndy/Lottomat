namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 获取专题文章列表
    /// </summary>
    public class ThematicArticleListArg : BaseParameterEntity
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 获取专题文章详细
    /// </summary>
    public class ThematicArticleInfoArg : BaseParameterEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CId { get; set; }
    }

    /// <summary>
    /// 获取推荐专题文章列表
    /// </summary>
    public class RecommendThematicArticleArg : BaseParameterEntity
    {
        /// <summary>
        /// 总记录数，默认30条
        /// </summary>
        public int TotalRecord { get; set; } = 30;
        /// <summary>
        /// 彩种枚举码
        /// </summary>
        public string EnumCode { get; set; }
    }
}