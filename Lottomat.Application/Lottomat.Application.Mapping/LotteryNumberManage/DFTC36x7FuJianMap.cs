using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 15:48
    /// 描 述：地方彩-福建体彩36选7
    /// </summary>
    public class DFTC36x7FuJianMap : EntityTypeConfiguration<DFTC36x7FuJianEntity>
    {
        public DFTC36x7FuJianMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_TC36x7_FuJian");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
