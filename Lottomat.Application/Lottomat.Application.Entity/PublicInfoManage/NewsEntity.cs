using Lottomat.Application.Code;
using System;
using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        public int PK { get; set; }
        /// <summary>
        /// 新闻主键
        /// </summary>
        /// <returns></returns>
        public string NewsId { get; set; }
        /// <summary>
        /// 类型（1-新闻2-公告）
        /// </summary>
        /// <returns></returns>
        public int? TypeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// 所属类别Id
        /// </summary>
        /// <returns></returns>
        public string CategoryId { get; set; }
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
        /// <summary>
        /// 作者
        /// </summary>
        /// <returns></returns>
        public string AuthorName { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public string CompileName { get; set; }
        /// <summary>
        /// Tag词
        /// </summary>
        /// <returns></returns>
        public string TagWord { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <returns></returns>
        public string Keyword { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        /// <returns></returns>
        public string SourceName { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>
        /// <returns></returns>
        public string SourceAddress { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        /// <returns></returns>
        public string NewsContent { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        /// <returns></returns>
        public int? PV { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        /// <returns></returns>
        public bool? IsHot { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        /// <returns></returns>
        public bool? IsStick { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        /// <returns></returns>
        public bool? IsRecommend { get; set; }
        /// <summary>
        /// 三要素-Title
        /// </summary>
        /// <returns></returns>
        public string TitleElement { get; set; }
        /// <summary>
        /// 三要素-Description
        /// </summary>
        /// <returns></returns>
        public string DescriptionElement { get; set; }
        /// <summary>
        /// 三要素-Keyword
        /// </summary>
        /// <returns></returns>
        public string KeywordElement { get; set; }
        /// <summary>
        /// 静态文件地址
        /// </summary>
        /// <returns></returns>
        public string StaticFileSourceUrl { get; set; }
        /// <summary>
        /// 是否生成了静态文件
        /// </summary>
        /// <returns></returns>
        public bool? IsGenerated { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        public string LabelId { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        /// <returns></returns>
        public string PeriodsNumber { get; set; }
        #endregion
        
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.NewsId = CommonHelper.GetGuid().ToString();
            this.CreateDate = DateTimeHelper.Now;
            this.ReleaseTime = DateTimeHelper.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            this.EnabledMark = (int)EnabledMarkEnum.Enabled;
            this.IsDelete = false;
            this.SortCode = 99;
            this.IsGenerated = false;
            this.PV = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.NewsId = keyValue;
            this.ModifyDate = DateTimeHelper.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
