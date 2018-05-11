using System;
using Lottomat.Application.Code;

using Lottomat.Util;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public class Tk_Gallery : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 图库编号
        /// </summary>
        /// <returns></returns>
        public int? GalleryNumber { get; set; }
        /// <summary>
        /// 图库名称
        /// </summary>
        /// <returns></returns>
        public string GalleryName { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        /// <returns></returns>
        public string CategoryId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 是否压缩
        /// </summary>
        /// <returns></returns>
        public bool? IsPicZip { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Reamrk { get; set; }
        /// <summary>
        /// SEO关键字
        /// </summary>
        /// <returns></returns>
        public string SeoKey { get; set; }
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
        /// 区域编码，值为A-B-C
        /// </summary>
        /// <returns></returns>
        public string AreaCode { get; set; }
        /// <summary>
        /// 热门期数
        /// </summary>
        /// <returns></returns>
        public int? HotNumber { get; set; }
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
            this.CreateTime = DateTime.Now;
            this.HotNumber = 0;
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