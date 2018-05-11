namespace Lottomat.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// 系统公告
    /// </summary>
    public class SystemNoticeEntity
    {
        /// <summary>
        /// 完整标题
        /// </summary>
        /// <returns></returns>
        public string FullHead { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>
        /// <returns></returns>
        public string FullHeadColor { get; set; }
        /// <summary>
        /// 简略标题
        /// </summary>
        /// <returns></returns>
        public string BriefHead { get; set; }
    }
}