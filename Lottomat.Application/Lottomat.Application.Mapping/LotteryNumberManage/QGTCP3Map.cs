using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 09:28
    /// �� ����ȫ����-��������������
    /// </summary>
    public class QGTCP3Map : EntityTypeConfiguration<QGTCP3Entity>
    {
        public QGTCP3Map()
        {
            #region ������
            //��
            this.ToTable("QG_TCP3");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
