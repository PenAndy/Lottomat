using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 14:09
    /// �� �����ط���-����6+1
    /// </summary>
    public class DFDF6J1Map : EntityTypeConfiguration<DFDF6J1Entity>
    {
        public DFDF6J1Map()
        {
            #region ������
            //��
            this.ToTable("DF_DF6J1");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
