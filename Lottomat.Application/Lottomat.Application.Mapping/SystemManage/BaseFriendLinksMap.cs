using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-06 13:49
    /// �� ������������
    /// </summary>
    public class BaseFriendLinksMap : EntityTypeConfiguration<BaseFriendLinksEntity>
    {
        public BaseFriendLinksMap()
        {
            #region ������
            //��
            this.ToTable("Base_FriendLinks");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
