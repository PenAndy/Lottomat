using Lottomat.Application.Entity.LotteryNumberManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-05 11:46
    /// �� ������Ƶ��-����11ѡ5
    /// </summary>
    public class GP11x5BeiJingMap : EntityTypeConfiguration<GP11x5BeiJingEntity>
    {
        public GP11x5BeiJingMap()
        {
            #region ������
            //��
            this.ToTable("GP_11x5_BeiJing");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
