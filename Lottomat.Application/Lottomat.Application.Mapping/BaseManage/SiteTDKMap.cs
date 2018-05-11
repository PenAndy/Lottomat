using Lottomat.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-04-09 14:48
    /// 描 述：站点TDK管理
    /// </summary>
    public class SiteTDKMap : EntityTypeConfiguration<SiteTDKEntity>
    {
        public SiteTDKMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_SiteTDK");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
