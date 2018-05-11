using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 10:55
    /// 描 述：意见反馈
    /// </summary>
    public class FeedbackEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        public string IP { get; set; }
        /// <summary>
        /// 来自
        /// </summary>
        /// <returns></returns>
        public string From { get; set; }
        /// <summary>
        /// 是否回复
        /// </summary>
        /// <returns></returns>
        public bool? IsReply { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ReplyTime { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        /// <returns></returns>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复人
        /// </summary>
        /// <returns></returns>
        public string ReplyUserName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        /// <returns></returns>
        public bool? IsPublic { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.IsDelete = false;
            this.AddTime = DateTimeHelper.Now;
            this.IsReply = false;
            this.IsPublic = true;
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