using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-05 14:54
    /// 描 述：广告管理
    /// </summary>
    public class AdvertisementEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// 广告ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        /// <returns></returns>
        public string CategoryId { get; set; }
        /// <summary>
        /// 位置，PC端为4X8，移动端为2X3
        /// </summary>
        /// <returns></returns>
        public int Position { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        /// <returns></returns>
        public string Href { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        /// <returns></returns>
        public DateTime? TermOfValidity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <returns></returns>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.AddTime = DateTimeHelper.Now;
            this.IsDelete = false;
            this.IsEnable = false;
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