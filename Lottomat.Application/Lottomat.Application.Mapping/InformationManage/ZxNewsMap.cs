using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-17 09:58
    /// �� ����55128��Ѷ����
    /// </summary>
    public class ZxNewsMap : EntityTypeConfiguration<ZxNewsEntity>
    {
        public ZxNewsMap()
        {
            #region ������
            //��
            this.ToTable("newManagerModels");
            //����
            this.HasKey(t => t.guid);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
