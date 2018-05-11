using Lottomat.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-05-25 14:15
    /// 描 述：企业对象
    /// </summary>
    public class base_EnterpriseObjMap : EntityTypeConfiguration<base_EnterpriseObjEntity>
    {
        public base_EnterpriseObjMap()
        {
            #region 表、主键
            //表
            this.ToTable("base_EnterpriseObj");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
