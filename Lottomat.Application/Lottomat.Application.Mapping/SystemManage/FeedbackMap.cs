using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 10:55
    /// �� �����������
    /// </summary>
    public class FeedbackMap : EntityTypeConfiguration<FeedbackEntity>
    {
        public FeedbackMap()
        {
            #region ������
            //��
            this.ToTable("Base_Feedback");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
