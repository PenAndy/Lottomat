using Lottomat.Application.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 12:20
    /// �� ������Դ����
    /// </summary>
    public class ResourcesMap : EntityTypeConfiguration<ResourcesEntity>
    {
        public ResourcesMap()
        {
            #region ������
            //��
            this.ToTable("Base_Resources");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
