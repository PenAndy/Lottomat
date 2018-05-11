using System;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Application.Entity.AttachmentManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-06-05 15:31
    /// 描 述：附件
    /// </summary>
    public class AttachmentEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 项目目录ID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// 主项目ID
        /// </summary>
        /// <returns></returns>
        public string MasterProjectID { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        public string FileType { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        /// <returns></returns>
        public string FilePath { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 上传人ID
        /// </summary>
        /// <returns></returns>
        public string UploadPersonID { get; set; }
        /// <summary>
        /// 上传人姓名
        /// </summary>
        /// <returns></returns>
        public string UploadPersonName { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }


        /// <summary>
        /// 来源
        /// </summary>
        public int? Source { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
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