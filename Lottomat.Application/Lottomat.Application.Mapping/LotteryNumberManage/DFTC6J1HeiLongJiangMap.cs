using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 09:24
    /// �� �����ط���-���������6+1
    /// </summary>
    public class DFTC6J1HeiLongJiangMap : EntityTypeConfiguration<DFTC6J1HeiLongJiangEntity>
    {
        public DFTC6J1HeiLongJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_TC6J1_HeiLongJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
