using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-20 16:02
    /// �� ����ȫ����-���ֲ�
    /// </summary>
    public class QGFCQLCMap : EntityTypeConfiguration<QGFCQLCEntity>
    {
        public QGFCQLCMap()
        {
            #region ������
            //��
            this.ToTable("QG_FCQLC");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
