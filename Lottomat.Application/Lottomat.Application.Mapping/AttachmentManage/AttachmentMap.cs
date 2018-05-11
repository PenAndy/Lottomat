using Lottomat.Application.Entity.AttachmentManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.AttachmentManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-06-05 15:31
    /// 描 述：附件
    /// </summary>
    public class AttachmentMap : EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("Agric_Attachment");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
