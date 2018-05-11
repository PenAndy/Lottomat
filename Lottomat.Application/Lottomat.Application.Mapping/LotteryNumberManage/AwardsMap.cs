using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-17 15:47
    /// 描 述：彩票规则
    /// </summary>
    public class AwardsMap : EntityTypeConfiguration<AwardsEntity>
    {
        public AwardsMap()
        {
            #region 表、主键
            //表
            this.ToTable("B_Awards");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
