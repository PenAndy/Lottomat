using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 15:48
    /// �� �����ط���-�������36ѡ7
    /// </summary>
    public class DFTC36x7FuJianMap : EntityTypeConfiguration<DFTC36x7FuJianEntity>
    {
        public DFTC36x7FuJianMap()
        {
            #region ������
            //��
            this.ToTable("DF_TC36x7_FuJian");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
