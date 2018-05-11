using System;

namespace Lottomat.Application.Entity.ViewModel
{
    /// <summary>
    /// 专题详细
    /// </summary>
    public class ZTArticleDetailViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        public string Editor { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 彩种枚举码
        /// </summary>
        public string EnumCode { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
    }
}