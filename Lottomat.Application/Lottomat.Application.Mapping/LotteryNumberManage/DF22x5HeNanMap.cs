using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 13:31
    /// �� �����ط���-����22ѡ5
    /// </summary>
    public class DF22x5HeNanMap : EntityTypeConfiguration<DF22x5HeNanEntity>
    {
        public DF22x5HeNanMap()
        {
            #region ������
            //��
            this.ToTable("DF_22x5_HeNan");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
