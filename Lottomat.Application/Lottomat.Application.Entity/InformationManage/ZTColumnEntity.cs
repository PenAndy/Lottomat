using System;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// 专题分类
    /// </summary>
    public class ZTColumnEntity : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
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
        /// 所属分类
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Words { get; set; }
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
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// url重写路径
        /// </summary>
        public string RewriteUrl { get; set; }
    }
}