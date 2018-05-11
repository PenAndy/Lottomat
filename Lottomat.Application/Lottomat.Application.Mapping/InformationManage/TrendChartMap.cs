using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-26 10:53
    /// 描 述：彩吧走势图
    /// </summary>
    public class TrendChartMap : EntityTypeConfiguration<TrendChartEntity>
    {
        public TrendChartMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_TrendChart");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
