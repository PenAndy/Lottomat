using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-20 16:02
    /// 描 述：全国彩-七乐彩
    /// </summary>
    public class QGFCQLCMap : EntityTypeConfiguration<QGFCQLCEntity>
    {
        public QGFCQLCMap()
        {
            #region 表、主键
            //表
            this.ToTable("QG_FCQLC");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
