using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-27 13:01
    /// �� �����ط���-�ӱ�����7
    /// </summary>
    public class DFPL7HeBeiMap : EntityTypeConfiguration<DFPL7HeBeiEntity>
    {
        public DFPL7HeBeiMap()
        {
            #region ������
            //��
            this.ToTable("DF_PL7_HeBei");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
