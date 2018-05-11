using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-22 14:28
    /// 描 述：地方彩-广西快乐双彩
    /// </summary>
    public class DFKLSCGuangXiMap : EntityTypeConfiguration<DFKLSCGuangXiEntity>
    {
        public DFKLSCGuangXiMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_KLSC_GuangXi");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
