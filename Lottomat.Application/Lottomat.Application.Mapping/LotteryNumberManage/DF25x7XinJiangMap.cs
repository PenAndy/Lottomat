using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 14:11
    /// �� �����ط���-�½�25ѡ7
    /// </summary>
    public class DF25x7XinJiangMap : EntityTypeConfiguration<DF25x7XinJiangEntity>
    {
        public DF25x7XinJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_25x7_XinJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
