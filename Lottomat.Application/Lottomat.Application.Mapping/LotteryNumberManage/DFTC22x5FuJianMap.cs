using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-21 15:15
    /// �� �����ط���-�������22ѡ5
    /// </summary>
    public class DFTC22x5FuJianMap : EntityTypeConfiguration<DFTC22x5FuJianEntity>
    {
        public DFTC22x5FuJianMap()
        {
            #region ������
            //��
            this.ToTable("DF_TC22x5_FuJian");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
