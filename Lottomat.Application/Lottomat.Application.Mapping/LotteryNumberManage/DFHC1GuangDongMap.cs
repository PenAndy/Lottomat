using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 10:35
    /// �� �����ط���-�㶫�ò�1
    /// </summary>
    public class DFHC1GuangDongMap : EntityTypeConfiguration<DFHC1GuangDongEntity>
    {
        public DFHC1GuangDongMap()
        {
            #region ������
            //��
            this.ToTable("DF_HC1_GuangDong");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
