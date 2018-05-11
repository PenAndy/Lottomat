using System;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-05-25 14:15
    /// 描 述：企业对象
    /// </summary>
    public class base_EnterpriseObjEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        /// <returns></returns>
        public string RegNum { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// 住所
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 法定代表人
        /// </summary>
        /// <returns></returns>
        public string Person { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        /// <returns></returns>
        public string RegCapital { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        /// <returns></returns>
        public DateTime? RegDate { get; set; }
        /// <summary>
        /// 营业期限
        /// </summary>
        /// <returns></returns>
        public string BusnissTerm { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        /// <returns></returns>
        public string BusinessScope { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        /// <returns></returns>
        public string RegOrg { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        /// <returns></returns>
        public DateTime? RecordDate { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        /// <returns></returns>
        public int? Source { get; set; }
        /// <summary>
        /// 录入机构ID
        /// </summary>
        /// <returns></returns>
        public string RecordOrgID { get; set; }
        /// <summary>
        /// 删除
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