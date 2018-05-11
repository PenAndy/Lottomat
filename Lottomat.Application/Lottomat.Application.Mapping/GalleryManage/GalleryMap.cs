using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-25 09:38
    /// 描 述：Tk_Gallery
    /// </summary>
    public class GalleryMap : EntityTypeConfiguration<GalleryEntity>
    {
        public GalleryMap()
        {
            #region 表、主键
            //表
            this.ToTable("Tk_Gallery");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
