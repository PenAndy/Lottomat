using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 13:11
    /// �� �������ո���25ѡ5
    /// </summary>
    public class DFFC25x5AnHuiMap : EntityTypeConfiguration<DFFC25x5AnHuiEntity>
    {
        public DFFC25x5AnHuiMap()
        {
            #region ������
            //��
            this.ToTable("DF_FC25x5_AnHui");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
