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
    /// 日 期：2017-10-25 15:58
    /// 描 述：系统API接口
    /// </summary>
    public class InterfaceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        /// <returns></returns>
        public string InterfaceName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        /// <returns></returns>
        public string RequestType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
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
            this.Id = CommonHelper.GetGuid().ToString();
            this.CreateDate = DateTimeHelper.Now;
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
            this.Id = keyValue;
        }
        #endregion
    }
}