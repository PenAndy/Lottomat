using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Application.Service.SystemManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using Lottomat.Application.Code;

namespace Lottomat.Application.Busines.SystemManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-06 13:49
    /// �� ������������
    /// </summary>
    public class BaseFriendLinksBLL
    {
        private IBaseFriendLinksService service = new BaseFriendLinksService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(Expression<Func<BaseFriendLinksEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public BaseFriendLinksEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
                Cache.Factory.CacheFactory.Cache().RemoveCache(CacheKeyEnum.FriendLink.ToString());
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
        public void SaveForm(string keyValue, BaseFriendLinksEntity entity,string code)
        {
            try
            {
                service.SaveForm(keyValue, entity, code);
                Cache.Factory.CacheFactory.Cache().RemoveCache(CacheKeyEnum.FriendLink.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
