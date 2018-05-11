using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 11:24
    /// 描 述：地方彩-辽宁35选7
    /// </summary>
    public class DF35x7LiaoNingMap : EntityTypeConfiguration<DF35x7LiaoNingEntity>
    {
        public DF35x7LiaoNingMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_35x7_LiaoNing");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
