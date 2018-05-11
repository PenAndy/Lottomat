using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-02 15:17
    /// 描 述：专题文章
    /// </summary>
    public class ZTArticleMap : EntityTypeConfiguration<ZTArticleEntity>
    {
        public ZTArticleMap()
        {
            #region 表、主键
            //表
            this.ToTable("ZT_Article");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
