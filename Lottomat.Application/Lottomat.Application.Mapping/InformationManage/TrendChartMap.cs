using Lottomat.Application.Entity.InformationManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-26 10:53
    /// �� �����ʰ�����ͼ
    /// </summary>
    public class TrendChartMap : EntityTypeConfiguration<TrendChartEntity>
    {
        public TrendChartMap()
        {
            #region ������
            //��
            this.ToTable("Base_TrendChart");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
