using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-28 09:12
    /// �� �����ط���-�㽭20ѡ5
    /// </summary>
    public class DF20x5ZheJiangMap : EntityTypeConfiguration<DF20x5ZheJiangEntity>
    {
        public DF20x5ZheJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_20x5_ZheJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
