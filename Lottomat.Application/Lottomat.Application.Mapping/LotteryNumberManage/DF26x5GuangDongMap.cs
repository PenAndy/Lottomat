using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 16:34
    /// �� �����ط���-�㶫26ѡ5
    /// </summary>
    public class DF26x5GuangDongMap : EntityTypeConfiguration<DF26x5GuangDongEntity>
    {
        public DF26x5GuangDongMap()
        {
            #region ������
            //��
            this.ToTable("DF_26x5_GuangDong");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
