using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 09:24
    /// 描 述：地方彩-黑龙江体彩6+1
    /// </summary>
    public class DFTC6J1HeiLongJiangMap : EntityTypeConfiguration<DFTC6J1HeiLongJiangEntity>
    {
        public DFTC6J1HeiLongJiangMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_TC6J1_HeiLongJiang");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
