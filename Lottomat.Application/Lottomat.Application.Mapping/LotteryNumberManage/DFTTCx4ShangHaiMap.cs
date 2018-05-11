using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 13:01
    /// 描 述：地方彩-上海天天彩选4
    /// </summary>
    public class DFTTCx4ShangHaiMap : EntityTypeConfiguration<DFTTCx4ShangHaiEntity>
    {
        public DFTTCx4ShangHaiMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_TTCx4_ShangHai");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
