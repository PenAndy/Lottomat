using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 14:22
    /// �� �����ط���-����30ѡ5
    /// </summary>
    public class DF30x5HuBeiMap : EntityTypeConfiguration<DF30x5HuBeiEntity>
    {
        public DF30x5HuBeiMap()
        {
            #region ������
            //��
            this.ToTable("DF_30x5_HuBei");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
