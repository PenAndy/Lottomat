using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 14:40
    /// �� �����ط���-�㽭6+1
    /// </summary>
    public class DF6J1ZheJiangMap : EntityTypeConfiguration<DF6J1ZheJiangEntity>
    {
        public DF6J1ZheJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_6J1_ZheJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
