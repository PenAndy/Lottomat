using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-22 11:39
    /// 描 述：地方彩-广东深圳风采35选7
    /// </summary>
    public class DFSZFCGuangDongMap : EntityTypeConfiguration<DFSZFCGuangDongEntity>
    {
        public DFSZFCGuangDongMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_SZFC_GuangDong");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
