using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-25 16:17
    /// �� ����ϵͳ�ӿ���Կ����
    /// </summary>
    public class AppKeyMap : EntityTypeConfiguration<AppKeyEntity>
    {
        public AppKeyMap()
        {
            #region ������
            //��
            this.ToTable("Sys_AppKey");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
