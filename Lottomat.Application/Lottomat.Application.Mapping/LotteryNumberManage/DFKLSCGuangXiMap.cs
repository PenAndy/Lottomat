using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 14:28
    /// �� �����ط���-��������˫��
    /// </summary>
    public class DFKLSCGuangXiMap : EntityTypeConfiguration<DFKLSCGuangXiEntity>
    {
        public DFKLSCGuangXiMap()
        {
            #region ������
            //��
            this.ToTable("DF_KLSC_GuangXi");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
