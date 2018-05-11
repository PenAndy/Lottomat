using System;

namespace Lottomat.Application.Entity.ViewModel
{
    /// <summary>
    /// 专题文章列表
    /// </summary>
    public class ZTArticleViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int Cid { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        /// <returns></returns>
        public string ShortDetail { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? Addtime { get; set; }
    }
}