using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 10:46
    /// 描 述：地方彩-河北好运彩2
    /// </summary>
    public class DFHYC2HeBeiMap : EntityTypeConfiguration<DFHYC2HeBeiEntity>
    {
        public DFHYC2HeBeiMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_HYC2_HeBei");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
