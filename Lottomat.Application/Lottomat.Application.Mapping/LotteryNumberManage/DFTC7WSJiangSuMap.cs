using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 10:39
    /// �� �����ط���-����7λ��
    /// </summary>
    public class DFTC7WSJiangSuMap : EntityTypeConfiguration<DFTC7WSJiangSuEntity>
    {
        public DFTC7WSJiangSuMap()
        {
            #region ������
            //��
            this.ToTable("DF_TC7WS_JiangSu");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
