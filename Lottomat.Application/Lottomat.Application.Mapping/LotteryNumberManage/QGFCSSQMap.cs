using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-20 12:54
    /// �� ����ȫ����-˫ɫ��
    /// </summary>
    public class QGFCSSQMap : EntityTypeConfiguration<QGFCSSQEntity>
    {
        public QGFCSSQMap()
        {
            #region ������
            //��
            this.ToTable("QG_FCSSQ");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
