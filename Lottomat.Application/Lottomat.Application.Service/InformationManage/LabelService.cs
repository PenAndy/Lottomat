using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-27 10:34
    /// �� ����Zx_Label
    /// </summary>
    public class LabelService : RepositoryFactory<LabelEntity>, ILabelService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<LabelEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<LabelEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["LabelName"].IsEmpty())
                {
                    string LabelName = queryParam["LabelName"].ToString();
                    expression = expression.And(t => t.LabelName.Contains(LabelName));
                }
                if (!queryParam["TitleElement"].IsEmpty())
                {
                    string TitleElement = queryParam["TitleElement"].ToString();
                    expression = expression.And(t => t.TitleElement == TitleElement);
                }
                if (!queryParam["DescriptionElement"].IsEmpty())
                {
                    string DescriptionElement = queryParam["DescriptionElement"].ToString();
                    expression = expression.And(t => t.DescriptionElement == DescriptionElement);
                }
                if (!queryParam["KeywordElement"].IsEmpty())
                {
                    string KeywordElement = queryParam["KeywordElement"].ToString();
                    expression = expression.And(t => t.KeywordElement == KeywordElement);
                }
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId == CategoryId);
                }
            }

            expression = expression.And(t => t.IsDelete == false);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<LabelEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public LabelEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<LabelEntity> GetList(Expression<Func<LabelEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
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
        public void SaveForm(string keyValue, LabelEntity entity)
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
