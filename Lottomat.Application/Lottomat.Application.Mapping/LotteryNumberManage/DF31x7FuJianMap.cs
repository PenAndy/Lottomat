using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 15:11
    /// �� �����ط���-����31ѡ7
    /// </summary>
    public class DF31x7FuJianMap : EntityTypeConfiguration<DF31x7FuJianEntity>
    {
        public DF31x7FuJianMap()
        {
            #region ������
            //��
            this.ToTable("DF_31x7_FuJian");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
