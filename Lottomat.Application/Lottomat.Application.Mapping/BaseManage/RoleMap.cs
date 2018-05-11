using Lottomat.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.4 14:31
    /// 描 述：角色管理
    /// </summary>
    public class RoleMap : EntityTypeConfiguration<RoleEntity>
    {
        public RoleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Role");
            //主键
            this.HasKey(t => t.RoleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
