namespace Lottomat.Application.Entity.ViewModel
{
    /// <summary>
    /// 专题文章分类
    /// </summary>
    public class ZTColumnViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 彩种
        /// </summary>
        public string Lottery { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string About { get; set; }
        /// <summary>
        /// 三要素-Title
        /// </summary>
        public string hTitle { get; set; }
        /// <summary>
        /// 三要素-关键字
        /// </summary>
        public string hKeywords { get; set; }
        /// <summary>
        /// 三要素-描述
        /// </summary>
        public string hDescription { get; set; }
        /// <summary>
        /// url重写路径
        /// </summary>
        public string RewriteUrl { get; set; }
    }
}