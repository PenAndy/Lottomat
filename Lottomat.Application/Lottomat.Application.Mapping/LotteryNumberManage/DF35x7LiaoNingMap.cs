using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 11:24
    /// �� �����ط���-����35ѡ7
    /// </summary>
    public class DF35x7LiaoNingMap : EntityTypeConfiguration<DF35x7LiaoNingEntity>
    {
        public DF35x7LiaoNingMap()
        {
            #region ������
            //��
            this.ToTable("DF_35x7_LiaoNing");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
