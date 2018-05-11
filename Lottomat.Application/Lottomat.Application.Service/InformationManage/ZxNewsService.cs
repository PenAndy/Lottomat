using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-17 09:58
    /// �� ����55128��Ѷ����
    /// </summary>
    public class ZxNewsService : RepositoryFactory<ZxNewsEntity>, IZxNewsService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ZxNewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZxNewsEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["title"].IsEmpty())
                {
                    string title = queryParam["title"].ToString();
                    expression = expression.And(t => t.title.Contains(title));
                }
            }

            expression = expression.And(t => t.isDelete == false);
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(expression, pagination);

            //return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<ZxNewsEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).IQueryable().ToList();
        }
        
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<ZxNewsEntity> GetList(Expression<Func<ZxNewsEntity, bool>> condition)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(condition);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ZxNewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.InformationBase).Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZxNewsEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DatabaseLinksEnum.InformationBase).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DatabaseLinksEnum.InformationBase).Insert(entity);
            }
        }
        #endregion
    }
}
