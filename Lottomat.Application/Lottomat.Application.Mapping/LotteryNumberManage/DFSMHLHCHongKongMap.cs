using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 13:55
    /// �� �����ط���-������ϲ�
    /// </summary>
    public class DFSMHLHCHongKongMap : EntityTypeConfiguration<DFSMHLHCHongKongEntity>
    {
        public DFSMHLHCHongKongMap()
        {
            #region ������
            //��
            this.ToTable("DF_SMHLHC_HongKong");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
