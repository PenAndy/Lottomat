using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-01-05 14:54
    /// �� ����������
    /// </summary>
    public class AdvertisementService : RepositoryFactory<AdvertisementEntity>, IAdvertisementService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<AdvertisementEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<AdvertisementEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId.Contains(CategoryId));
                }
            }

            expression = expression.And(t => t.IsDelete == false);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="query">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<AdvertisementEntity> GetList(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return this.BaseRepository().FindList(query);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public AdvertisementEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        public AdvertisementEntity GetEntity(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return this.BaseRepository().FindEntity(query);
        }

        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AdvertisementEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
