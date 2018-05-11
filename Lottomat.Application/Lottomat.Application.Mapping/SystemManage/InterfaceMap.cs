using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-25 15:58
    /// 描 述：系统API接口
    /// </summary>
    public class InterfaceMap : EntityTypeConfiguration<InterfaceEntity>
    {
        public InterfaceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sys_Interface");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
