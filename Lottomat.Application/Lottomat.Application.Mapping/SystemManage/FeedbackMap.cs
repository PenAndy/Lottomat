using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 10:55
    /// 描 述：意见反馈
    /// </summary>
    public class FeedbackMap : EntityTypeConfiguration<FeedbackEntity>
    {
        public FeedbackMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Feedback");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
