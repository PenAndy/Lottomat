using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 13:31
    /// 描 述：地方彩-河南22选5
    /// </summary>
    public class DF22x5HeNanMap : EntityTypeConfiguration<DF22x5HeNanEntity>
    {
        public DF22x5HeNanMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_22x5_HeNan");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
