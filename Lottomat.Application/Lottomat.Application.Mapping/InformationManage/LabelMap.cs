using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-27 10:34
    /// �� ����Zx_Label
    /// </summary>
    public class LabelMap : EntityTypeConfiguration<LabelEntity>
    {
        public LabelMap()
        {
            #region ������
            //��
            this.ToTable("Zx_Label");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
