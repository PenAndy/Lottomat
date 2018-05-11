using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:29
    /// 描 述：图库详情表
    /// </summary>
    public class Tk_GalleryDetail : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
       // public int? PK { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 图库ID
        /// </summary>
        /// <returns></returns>
        public string GalleryId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        /// <returns></returns>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        /// <returns></returns>
        public string PeriodsNumber { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
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
            this.AddTime = DateTimeHelper.Now;
            this.CreateTime = DateTimeHelper.Now;
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