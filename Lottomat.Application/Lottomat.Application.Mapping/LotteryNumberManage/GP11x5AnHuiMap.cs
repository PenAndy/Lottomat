using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-05 10:15
    /// �� ������Ƶ��-����11ѡ5
    /// </summary>
    public class GP11x5AnHuiMap : EntityTypeConfiguration<GP11x5AnHuiEntity>
    {
        public GP11x5AnHuiMap()
        {
            #region ������
            //��
            this.ToTable("GP_11x5_AnHui");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
