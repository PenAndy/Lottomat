using System;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// 专题文章详细
    /// </summary>
    public class ZTArticleDetailEntity : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 编辑者ID
        /// </summary>
        public int EditId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
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
    }
}