using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 13:55
    /// 描 述：地方彩-香港六合彩
    /// </summary>
    public class DFSMHLHCHongKongMap : EntityTypeConfiguration<DFSMHLHCHongKongEntity>
    {
        public DFSMHLHCHongKongMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_SMHLHC_HongKong");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
