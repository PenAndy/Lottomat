using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 14:09
    /// 描 述：地方彩-东方6+1
    /// </summary>
    public class DFDF6J1Map : EntityTypeConfiguration<DFDF6J1Entity>
    {
        public DFDF6J1Map()
        {
            #region 表、主键
            //表
            this.ToTable("DF_DF6J1");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
