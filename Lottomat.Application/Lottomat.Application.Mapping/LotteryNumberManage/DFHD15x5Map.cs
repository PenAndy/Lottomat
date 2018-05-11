using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-22 15:24
    /// 描 述：地方彩-华东15选5
    /// </summary>
    public class DFHD15x5Map : EntityTypeConfiguration<DFHD15x5Entity>
    {
        public DFHD15x5Map()
        {
            #region 表、主键
            //表
            this.ToTable("DF_HD15x5");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
