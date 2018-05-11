using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-27 10:34
    /// 描 述：Zx_Label
    /// </summary>
    public class LabelMap : EntityTypeConfiguration<LabelEntity>
    {
        public LabelMap()
        {
            #region 表、主键
            //表
            this.ToTable("Zx_Label");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
