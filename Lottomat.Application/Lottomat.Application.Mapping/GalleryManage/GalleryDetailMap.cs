using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-25 09:54
    /// 描 述：GalleryDetail
    /// </summary>
    public class GalleryDetailMap : EntityTypeConfiguration<GalleryDetailEntity>
    {
        public GalleryDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("Tk_GalleryDetail");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
