using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 10:46
    /// �� �����ط���-�ӱ����˲�2
    /// </summary>
    public class DFHYC2HeBeiMap : EntityTypeConfiguration<DFHYC2HeBeiEntity>
    {
        public DFHYC2HeBeiMap()
        {
            #region ������
            //��
            this.ToTable("DF_HYC2_HeBei");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
