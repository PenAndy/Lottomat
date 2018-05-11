using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-25 09:54
    /// �� ����GalleryDetail
    /// </summary>
    public class GalleryDetailMap : EntityTypeConfiguration<GalleryDetailEntity>
    {
        public GalleryDetailMap()
        {
            #region ������
            //��
            this.ToTable("Tk_GalleryDetail");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
