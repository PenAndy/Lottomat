using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-28 09:34
    /// �� �����ط���-����4+1
    /// </summary>
    public class DF4J1HaiNanMap : EntityTypeConfiguration<DF4J1HaiNanEntity>
    {
        public DF4J1HaiNanMap()
        {
            #region ������
            //��
            this.ToTable("DF_4J1_HaiNan");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
