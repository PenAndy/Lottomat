using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.GalleryManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:24
    /// �� ����ͼ������
    /// </summary>
    public class Tk_GalleryMap : EntityTypeConfiguration<Tk_Gallery>
    {
        public Tk_GalleryMap()
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
