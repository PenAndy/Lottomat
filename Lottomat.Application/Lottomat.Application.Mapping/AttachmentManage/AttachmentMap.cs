using Lottomat.Application.Entity.AttachmentManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.AttachmentManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-06-05 15:31
    /// �� ��������
    /// </summary>
    public class AttachmentMap : EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentMap()
        {
            #region ������
            //��
            this.ToTable("Agric_Attachment");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
