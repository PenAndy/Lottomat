using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 09:38
    /// �� �����ط���-�㶫36ѡ7
    /// </summary>
    public class DF36x7GuangDongMap : EntityTypeConfiguration<DF36x7GuangDongEntity>
    {
        public DF36x7GuangDongMap()
        {
            #region ������
            //��
            this.ToTable("DF_36x7_GuangDong");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
