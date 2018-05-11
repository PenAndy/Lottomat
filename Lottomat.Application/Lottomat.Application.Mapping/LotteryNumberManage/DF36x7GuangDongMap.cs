using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-22 09:38
    /// 描 述：地方彩-广东36选7
    /// </summary>
    public class DF36x7GuangDongMap : EntityTypeConfiguration<DF36x7GuangDongEntity>
    {
        public DF36x7GuangDongMap()
        {
            #region 表、主键
            //表
            this.ToTable("DF_36x7_GuangDong");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
