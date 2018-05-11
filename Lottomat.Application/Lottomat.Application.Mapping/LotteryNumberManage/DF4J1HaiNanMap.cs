using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-28 09:34
    /// 描 述：地方彩-海南4+1
    /// </summary>
    public class DF4J1HaiNanMap : EntityTypeConfiguration<DF4J1HaiNanEntity>
    {
        public DF4J1HaiNanMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_4J1_HaiNan");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
