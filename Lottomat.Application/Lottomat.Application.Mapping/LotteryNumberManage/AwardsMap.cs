using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-17 15:47
    /// �� ������Ʊ����
    /// </summary>
    public class AwardsMap : EntityTypeConfiguration<AwardsEntity>
    {
        public AwardsMap()
        {
            #region ������
            //��
            this.ToTable("B_Awards");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
