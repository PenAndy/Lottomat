using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-24 14:24
    /// �� �����ʰɹ���
    /// </summary>
    public class ToolsMap : EntityTypeConfiguration<ToolsEntity>
    {
        public ToolsMap()
        {
            #region ������
            //��
            this.ToTable("Base_Tools");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
