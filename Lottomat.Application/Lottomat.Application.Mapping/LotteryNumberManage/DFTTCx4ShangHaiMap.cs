using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 13:01
    /// �� �����ط���-�Ϻ������ѡ4
    /// </summary>
    public class DFTTCx4ShangHaiMap : EntityTypeConfiguration<DFTTCx4ShangHaiEntity>
    {
        public DFTTCx4ShangHaiMap()
        {
            #region ������
            //��
            this.ToTable("DF_TTCx4_ShangHai");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
