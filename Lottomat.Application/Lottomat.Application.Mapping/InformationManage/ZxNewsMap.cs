using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-17 09:58
    /// 描 述：55128资讯内容
    /// </summary>
    public class ZxNewsMap : EntityTypeConfiguration<ZxNewsEntity>
    {
        public ZxNewsMap()
        {
            #region 表、主键
            //表
            this.ToTable("newManagerModels");
            //主键
            this.HasKey(t => t.guid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
