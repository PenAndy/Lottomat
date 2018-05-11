using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-28 09:12
    /// 描 述：地方彩-浙江20选5
    /// </summary>
    public class DF20x5ZheJiangMap : EntityTypeConfiguration<DF20x5ZheJiangEntity>
    {
        public DF20x5ZheJiangMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_20x5_ZheJiang");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
