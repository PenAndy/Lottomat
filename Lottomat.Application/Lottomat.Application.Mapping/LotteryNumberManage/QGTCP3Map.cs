using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-21 09:28
    /// 描 述：全国彩-排列三、排列五
    /// </summary>
    public class QGTCP3Map : EntityTypeConfiguration<QGTCP3Entity>
    {
        public QGTCP3Map()
        {
            #region 表、主键
            //表
            this.ToTable("QG_TCP3");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
