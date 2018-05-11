using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.GalleryManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:29
    /// �� ����ͼ�������
    /// </summary>
    public interface ITk_GalleryDetailIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<Tk_GalleryDetail> GetList(string queryJson);
      
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        Tk_GalleryDetail GetEntity(string keyValue);
        #endregion
        IEnumerable<Tk_GalleryDetail> GetList(Expression<Func<Tk_GalleryDetail, bool>> condition);
        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Tk_GalleryDetail entity);
        IEnumerable<Tk_GalleryDetail> GetPageList(Pagination pagination, string queryJson);
        int MenuNewPeriodsNumber(string menuname);
        List<Tk_GalleryDetail> QueryDetailByGalleryId(List<string> galleryIds, int periodsNumber);
         int NewPeriodsNumber();
        #endregion
    }
}
