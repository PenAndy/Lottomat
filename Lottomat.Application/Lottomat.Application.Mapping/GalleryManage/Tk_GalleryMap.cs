using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public class Tk_GalleryMap : EntityTypeConfiguration<Tk_Gallery>
    {
        public Tk_GalleryMap()
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
