using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-17 12:51
    /// 描 述：全国彩-福彩3D
    /// </summary>
    public class QGFC3DMap : EntityTypeConfiguration<QGFC3DEntity>
    {
        public QGFC3DMap()
        {
            #region 表、主键
            //表
            this.ToTable("QG_FC3D");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
