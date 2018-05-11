using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 14:22
    /// 描 述：地方彩-湖北30选5
    /// </summary>
    public class DF30x5HuBeiMap : EntityTypeConfiguration<DF30x5HuBeiEntity>
    {
        public DF30x5HuBeiMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_30x5_HuBei");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
