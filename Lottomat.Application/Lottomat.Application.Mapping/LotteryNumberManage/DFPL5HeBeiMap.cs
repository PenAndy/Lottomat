using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 11:45
    /// �� �����ط���-�ӱ�����5
    /// </summary>
    public class DFPL5HeBeiMap : EntityTypeConfiguration<DFPL5HeBeiEntity>
    {
        public DFPL5HeBeiMap()
        {
            #region ������
            //��
            this.ToTable("DF_PL5_HeBei");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
