using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-05 14:54
    /// 描 述：广告管理
    /// </summary>
    public class AdvertisementMap : EntityTypeConfiguration<AdvertisementEntity>
    {
        public AdvertisementMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Advertisement");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
