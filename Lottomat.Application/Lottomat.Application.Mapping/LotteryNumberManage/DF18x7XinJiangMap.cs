using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 13:40
    /// �� �����ط���-�½�18ѡ7
    /// </summary>
    public class DF18x7XinJiangMap : EntityTypeConfiguration<DF18x7XinJiangEntity>
    {
        public DF18x7XinJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_18x7_XinJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
