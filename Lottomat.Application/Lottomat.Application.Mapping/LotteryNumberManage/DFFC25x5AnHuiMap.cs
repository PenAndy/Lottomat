using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 13:11
    /// 描 述：安徽福彩25选5
    /// </summary>
    public class DFFC25x5AnHuiMap : EntityTypeConfiguration<DFFC25x5AnHuiEntity>
    {
        public DFFC25x5AnHuiMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_FC25x5_AnHui");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
