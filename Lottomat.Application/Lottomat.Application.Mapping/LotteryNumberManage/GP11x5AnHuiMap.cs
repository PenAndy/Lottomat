using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-05 10:15
    /// 描 述：高频彩-安徽11选5
    /// </summary>
    public class GP11x5AnHuiMap : EntityTypeConfiguration<GP11x5AnHuiEntity>
    {
        public GP11x5AnHuiMap()
        {
            #region 表、主键
            //表
            this.ToTable("GP_11x5_AnHui");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
