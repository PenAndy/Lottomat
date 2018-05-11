using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 15:15
    /// 描 述：地方彩-福建体彩22选5
    /// </summary>
    public class DFTC22x5FuJianMap : EntityTypeConfiguration<DFTC22x5FuJianEntity>
    {
        public DFTC22x5FuJianMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_TC22x5_FuJian");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
