using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-01-02 15:17
    /// �� ����ר�����·���
    /// </summary>
    public class ZTColumnMap : EntityTypeConfiguration<ZTColumnEntity>
    {
        public ZTColumnMap()
        {
            #region ��������
            //��
            this.ToTable("ZT_Column");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}