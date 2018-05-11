using Lottomat.Application.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 12:20
    /// 描 述：资源分享
    /// </summary>
    public class ResourcesMap : EntityTypeConfiguration<ResourcesEntity>
    {
        public ResourcesMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Resources");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
