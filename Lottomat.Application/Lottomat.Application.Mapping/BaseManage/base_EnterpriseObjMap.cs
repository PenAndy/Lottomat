using Lottomat.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-05-25 14:15
    /// �� ������ҵ����
    /// </summary>
    public class base_EnterpriseObjMap : EntityTypeConfiguration<base_EnterpriseObjEntity>
    {
        public base_EnterpriseObjMap()
        {
            #region ������
            //��
            this.ToTable("base_EnterpriseObj");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
