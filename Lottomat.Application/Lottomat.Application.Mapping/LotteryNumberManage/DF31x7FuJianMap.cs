using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 15:11
    /// 描 述：地方彩-福建31选7
    /// </summary>
    public class DF31x7FuJianMap : EntityTypeConfiguration<DF31x7FuJianEntity>
    {
        public DF31x7FuJianMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_31x7_FuJian");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
