using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 16:17
    /// �� �����ط���-������25ѡ5
    /// </summary>
    public class DFLJFC22x5HeiLongJiangMap : EntityTypeConfiguration<DFLJFC22x5HeiLongJiangEntity>
    {
        public DFLJFC22x5HeiLongJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_LJFC22x5_HeiLongJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
