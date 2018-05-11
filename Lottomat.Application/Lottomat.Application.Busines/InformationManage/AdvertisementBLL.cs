using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Application.Service.InformationManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace Lottomat.Application.Busines.InformationManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-01-05 14:54
    /// �� ����������
    /// </summary>
    public class AdvertisementBLL
    {
        private IAdvertisementService service = new AdvertisementService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<AdvertisementEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="query">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<AdvertisementEntity> GetList(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return service.GetList(query);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public AdvertisementEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="query">����</param>
        /// <returns></returns>
        public AdvertisementEntity GetEntity(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return service.GetEntity(query);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AdvertisementEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        #endregion
    }
}
