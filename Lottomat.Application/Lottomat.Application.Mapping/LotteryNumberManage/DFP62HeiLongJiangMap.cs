using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-28 09:59
    /// �� �����ط���-������P62
    /// </summary>
    public class DFP62HeiLongJiangMap : EntityTypeConfiguration<DFP62HeiLongJiangEntity>
    {
        public DFP62HeiLongJiangMap()
        {
            #region ������
            //��
            this.ToTable("DF_P62_HeiLongJiang");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
