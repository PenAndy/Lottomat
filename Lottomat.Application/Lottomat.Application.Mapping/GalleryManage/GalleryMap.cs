using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-25 09:38
    /// �� ����Tk_Gallery
    /// </summary>
    public class GalleryMap : EntityTypeConfiguration<GalleryEntity>
    {
        public GalleryMap()
        {
            #region ������
            //��
            this.ToTable("Tk_Gallery");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
