using System;
using Lottomat.Application.Code;

using Lottomat.Util;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-27 10:34
    /// 描 述：Zx_Label
    /// </summary>
    public class LabelEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// 标签主键ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        /// <returns></returns>
        public string CategoryId { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        /// <returns></returns>
        public string LabelName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// SortCode
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
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
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.IsDelete = false;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
    }
}