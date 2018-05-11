using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 16:34
    /// 描 述：地方彩-广东26选5
    /// </summary>
    public class DF26x5GuangDongMap : EntityTypeConfiguration<DF26x5GuangDongEntity>
    {
        public DF26x5GuangDongMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_26x5_GuangDong");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
