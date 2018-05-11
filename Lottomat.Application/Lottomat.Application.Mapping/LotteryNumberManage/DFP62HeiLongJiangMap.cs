using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-28 09:59
    /// 描 述：地方彩-黑龙江P62
    /// </summary>
    public class DFP62HeiLongJiangMap : EntityTypeConfiguration<DFP62HeiLongJiangEntity>
    {
        public DFP62HeiLongJiangMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_P62_HeiLongJiang");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
