using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 11:25
    /// �� �����ط���-�ӱ����˲�3
    /// </summary>
    public class DFHYC3HeBeiMap : EntityTypeConfiguration<DFHYC3HeBeiEntity>
    {
        public DFHYC3HeBeiMap()
        {
            #region ������
            //��
            this.ToTable("DF_HYC3_HeBei");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
