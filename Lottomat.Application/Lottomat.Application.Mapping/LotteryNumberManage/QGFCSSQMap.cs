using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-20 12:54
    /// 描 述：全国彩-双色球
    /// </summary>
    public class QGFCSSQMap : EntityTypeConfiguration<QGFCSSQEntity>
    {
        public QGFCSSQMap()
        {
            #region 表、主键
            //表
            this.ToTable("QG_FCSSQ");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
