using Lottomat.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-04-09 14:48
    /// �� ����վ��TDK����
    /// </summary>
    public class SiteTDKMap : EntityTypeConfiguration<SiteTDKEntity>
    {
        public SiteTDKMap()
        {
            #region ������
            //��
            this.ToTable("Base_SiteTDK");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
