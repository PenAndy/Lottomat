using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 09:31
    /// �� �����ط���-�½�25ѡ7
    /// </summary>
    public class DF35x7XinJiangMap : EntityTypeConfiguration<DF35x7XinJiangEntity>
    {
        public DF35x7XinJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_35x7_XinJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
