using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.InformationManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-01-05 14:54
    /// �� ����������
    /// </summary>
    public interface IAdvertisementService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<AdvertisementEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="query">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<AdvertisementEntity> GetList(Expression<Func<AdvertisementEntity, bool>> query);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        AdvertisementEntity GetEntity(string keyValue);

        AdvertisementEntity GetEntity(Expression<Func<AdvertisementEntity, bool>> query);
        #endregion

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
        void SaveForm(string keyValue, AdvertisementEntity entity);
        #endregion
    }
}
