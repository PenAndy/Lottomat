using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-28 10:44
    /// �� �����ط���-������36ѡ7
    /// </summary>
    public class DF36x7HeiLongJiangMap : EntityTypeConfiguration<DF36x7HeiLongJiangEntity>
    {
        public DF36x7HeiLongJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_36x7_HeiLongJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
