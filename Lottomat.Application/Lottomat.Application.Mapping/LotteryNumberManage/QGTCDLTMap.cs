using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 10:16
    /// �� ����ȫ����-����͸
    /// </summary>
    public class QGTCDLTMap : EntityTypeConfiguration<QGTCDLTEntity>
    {
        public QGTCDLTMap()
        {
            #region ������
            //��
            this.ToTable("QG_TCDLT");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
