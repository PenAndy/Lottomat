using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 15:24
    /// �� �����ط���-����15ѡ5
    /// </summary>
    public class DFHD15x5Map : EntityTypeConfiguration<DFHD15x5Entity>
    {
        public DFHD15x5Map()
        {
            #region ������
            //��
            this.ToTable("DF_HD15x5");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
