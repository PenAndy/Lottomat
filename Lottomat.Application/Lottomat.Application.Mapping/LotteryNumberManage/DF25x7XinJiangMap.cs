using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 14:11
    /// 描 述：地方彩-新疆25选7
    /// </summary>
    public class DF25x7XinJiangMap : EntityTypeConfiguration<DF25x7XinJiangEntity>
    {
        public DF25x7XinJiangMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_25x7_XinJiang");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
