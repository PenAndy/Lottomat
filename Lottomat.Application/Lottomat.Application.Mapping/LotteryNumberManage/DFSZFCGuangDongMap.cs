using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 11:39
    /// �� �����ط���-�㶫���ڷ��35ѡ7
    /// </summary>
    public class DFSZFCGuangDongMap : EntityTypeConfiguration<DFSZFCGuangDongEntity>
    {
        public DFSZFCGuangDongMap()
        {
            #region ������
            //��
            this.ToTable("DF_SZFC_GuangDong");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
