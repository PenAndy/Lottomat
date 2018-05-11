using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 10:39
    /// 描 述：地方彩-江苏7位数
    /// </summary>
    public class DFTC7WSJiangSuMap : EntityTypeConfiguration<DFTC7WSJiangSuEntity>
    {
        public DFTC7WSJiangSuMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_TC7WS_JiangSu");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
