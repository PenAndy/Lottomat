using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-17 12:51
    /// �� ����ȫ����-����3D
    /// </summary>
    public class QGFC3DMap : EntityTypeConfiguration<QGFC3DEntity>
    {
        public QGFC3DMap()
        {
            #region ������
            //��
            this.ToTable("QG_FC3D");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
