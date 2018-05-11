using System;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 12:20
    /// 描 述：资源分享
    /// </summary>
    public class ResourcesEntity : BaseEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 资源类型ID
        /// </summary>
        /// <returns></returns>
        public string TypeId { get; set; }
        /// <summary>
        /// 资源类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 上传人ID
        /// </summary>
        /// <returns></returns>
        public string UploadUserId { get; set; }
        /// <summary>
        /// 上传人姓名
        /// </summary>
        public string UploadUserName { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 资源截图地址
        /// </summary>
        /// <returns></returns>
        public string Pic { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        /// <returns></returns>
        public int? DownloadCount { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 资源预览地址
        /// </summary>
        /// <returns></returns>
        public string PreviewUrl { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 资源大小
        /// </summary>
        /// <returns></returns>
        public float? Size { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 可用标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = CommonHelper.GetGuid().ToString();
            this.UploadUserId = OperatorProvider.Provider.Current().UserId;
            this.UploadUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            this.EnabledMark = (int)EnabledMarkEnum.Enabled;
            this.UploadTime = DateTimeHelper.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}